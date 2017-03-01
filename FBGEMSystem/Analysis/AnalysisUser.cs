using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using plotfft;  //求FFTdll
using getip;   //瞬时相位分析dll
using preprocess;   //预处理dll
using mfdfa;    //MFDFA_dll
using wavelet_S;//小波分析_dll

namespace FBGEMSystem
{
    public class AnalysisUser
    {
        private const int CH_NUM = 4; //通道数
        private const int MAX_POINT = 12; //每通道，布光纤光栅点数
        public int currentChannel = 0;
        public int currentPoint = 0;
        private const int WINSIZE = 2000; //滑动窗口长度
        private const int MAX_CH_POINT_LEN = 100; //采集点数据缓存长度

        public Message_FBG pre_msg = new Message_FBG();//出队等待处理

        public double[,] temp_ch1_point_signal = new double[MAX_POINT, WINSIZE];
        public double[,] temp_ch2_point_signal = new double[MAX_POINT, WINSIZE];
        public double[,] temp_ch3_point_signal = new double[MAX_POINT, WINSIZE];
        public double[,] temp_ch4_point_signal = new double[MAX_POINT, WINSIZE]; //用于解析包后保存数据，便于后面入队

        //保存每个通道内每个光纤光栅采集数据队列
        public Queue<double[]>[,] ch_point_signal = new Queue<double[]>[CH_NUM, MAX_POINT];
        private static object[,] sync_ch_point_signal = new object[CH_NUM, MAX_POINT];//对应的同步操作对象

        //保存要处理的相应通道相应点的采集数据队列
        public Queue<double[]> analysis_signal = new Queue<double[]>();
        private static object sync_analysis_signal = new object();//对应的同步操作对象

        public int recv_cnt = 0; //已接收帧计数
        public int pro_cnt = 0; //已处理计数
        public int aband = 0; //丢弃包计数

        public int real_pro = 0; //真实处理帧数
        public int un_pro = 0;

        private static object sync_sample = new object(); //采样间隔点同步操作对象
        public int sample = 0; //采样间隔点

        Cpreprocess prepro = new Cpreprocess();   //预处理
        Cplotfft fft_process = new Cplotfft();           //频谱
        Cgetip ip_process = new Cgetip();           //瞬时相位
        Cmfdfa mfdfa_process = new Cmfdfa();               //MFDFA
        Cwavelet wave_process = new Cwavelet();         //小波

        /*
         * 删除数据帧队列，同步操作，返回出队数据帧
         * 输入：
         *      无
         * 输出：
         *      返回待处理的数据帧
         */
        public Message_FBG del_msg()
        {
            Message_FBG msg = new Message_FBG();
            msg = Receiver.process_all_msg_FBG.Buffer;
            return msg;
        }

        /*
        * 解析帧函数，将msg解析，保存至各通道中各点的缓存区
        * 输入：
        *    Message msg ： 待解析包
        *    int j : 第j帧数据
        * 输出：
        *      无
        */
        private void decode_process(Message_FBG msg, int j)
        {
            for (int k = 0; k < MAX_POINT; k++)
            {
                for (int i = 0; i < Data.FBG_numPackage; i++)
                {
                    temp_ch1_point_signal[k, j * Data.FBG_numPackage + i] = msg.CH1[i * 64 + k];
                    temp_ch2_point_signal[k, j * Data.FBG_numPackage + i] = msg.CH2[i * 64 + k];
                    temp_ch3_point_signal[k, j * Data.FBG_numPackage + i] = msg.CH3[i * 64 + k];
                    temp_ch4_point_signal[k, j * Data.FBG_numPackage + i] = msg.CH4[i * 64 + k];
                }
            }
        }

        /*
         * 函数说明：
         *      解包一个滑动窗口数据并入队，队列长度为10个窗口长度。解析后，每个通道的滑动窗口为400个点。后面信号的处理以一个滑动窗口为最小单位。
         * 输入：
         *      无
         * 输出：
         *      无
         */
        public void decode_fun()
        {
            int count = 0;
            lock (sync_sample) //当接收缓存满后，在此会进行抽样，直至缓存区剩余一半时，sample=0
            {
                count = sample;
            }
            for (int i = 0; i < count; i++)  //清空缓存区，缓存区向前滑动
            {
                if (Receiver.process_all_msg_FBG.BufferSize >= WINSIZE / Data.FBG_numPackage)
                {
                    del_msg();
                    pro_cnt++;
                }
            }
            for (int j = 0; j < WINSIZE / Data.FBG_numPackage; j++) //填充滑动窗口
            {
                pre_msg = del_msg();
                decode_process(pre_msg, j);
                pro_cnt++;
            }
            un_pro++;
            for (int i = 0; i < CH_NUM; i++)
            {

                if (i != currentChannel)
                {
                    continue;
                }
                for (int j = 0; j < MAX_POINT; j++)
                {
                    if (j != currentPoint)
                    {
                        continue;
                    }
                    if (analysis_signal.Count > MAX_CH_POINT_LEN)
                    {
                        while (analysis_signal.Count > MAX_CH_POINT_LEN) //4个通道，每个通道40个点，每个点滑动窗口400个数据，每个点缓存为10
                        //个窗口，当缓存满了，等待process线程处理数据，直至缓存有余量
                        {
                            Thread.Sleep(5);
                        }
                    }
                    double[] temp = new double[WINSIZE];
                    for (int k = 0; k < WINSIZE; k++)
                    {
                        switch (i)
                        {
                            case 0:
                                {
                                    temp[k] = temp_ch1_point_signal[j, k];
                                    break;
                                }
                            case 1:
                                {
                                    temp[k] = temp_ch2_point_signal[j, k];
                                    break;
                                }
                            case 2:
                                {
                                    temp[k] = temp_ch3_point_signal[j, k];
                                    break;
                                }
                            case 3:
                                {
                                    temp[k] = temp_ch4_point_signal[j, k];
                                    break;

                                }
                            default: break;
                        }
                    }
                    lock (sync_analysis_signal)
                    {
                        analysis_signal.Enqueue(temp); 
                    }
                }
            }
        }
        //process线程中,获取处理数据
        public double[] GetProcessSignal()
        {
            double[] temp = new double[WINSIZE];
            lock (sync_analysis_signal)
            {
                temp = analysis_signal.Dequeue();
            }
            return temp;
        }

        public void IP_Process(double[] processSignal,ref double[] t ,ref double[] th)
        {
            //double[] result = new double[0];
            //double[] IP_Input = new double[0];
            //预处理数据
            MWNumericArray pre = new MWNumericArray(processSignal);
            MWNumericArray IP_Input = (MWNumericArray) prepro.preprocess(pre);
            
            MWArray[] result_IP = new MWArray[2];               //存放输出的数据数组，有两个输出数据
            MWArray[] input_IP = new MWArray[] { IP_Input, Data.SamplingRate_FBG}; //存放输入数据：原始信号和采样率
            //瞬时相位分析函数
            result_IP = ip_process.getip(2, IP_Input, Data.SamplingRate_FBG);

            MWNumericArray t_temp = result_IP[0] as MWNumericArray;   //第一个输出为时间t，
            MWNumericArray th_temp = result_IP[1] as MWNumericArray;   //第二个输出为瞬时相位th，
            t = (double[])t_temp.ToVector(MWArrayComponent.Real);      
            th = (double[])th_temp.ToVector(MWArrayComponent.Real);
        }

        public void FFT_Process(double[] processSignal, ref double[] f, ref double[] fftdata)
        {
            //double[] result = new double[0];
            //double[] IP_Input = new double[0];
            //预处理数据
            MWNumericArray pre = new MWNumericArray(processSignal);
            MWNumericArray FFT_Input = (MWNumericArray)prepro.preprocess(pre);

            MWArray[] result_FFT = new MWArray[2];               //存放输出的数据数组，有两个输出数据
            //瞬时相位分析函数
            result_FFT = fft_process.plotfft(2, FFT_Input, Data.SamplingRate_FBG);

            MWNumericArray f_temp = result_FFT[0] as MWNumericArray;   //第一个输出为频率f，
            MWNumericArray fftdata_temp = result_FFT[1] as MWNumericArray;   //第二个输出为幅值fftdata，
            f = (double[])f_temp.ToVector(MWArrayComponent.Real);
            fftdata = (double[])fftdata_temp.ToVector(MWArrayComponent.Real);
        }
        public void MFDFA_Process(double[] processSignal, ref double[] Hq, ref double[] tq, ref double[] alpha, ref double[] f, ref double[] q)
        {
            //double[] result = new double[0];
            //double[] IP_Input = new double[0];
            //预处理数据
            MWNumericArray pre = new MWNumericArray(processSignal);
            MWNumericArray MFDFA_Input = (MWNumericArray)prepro.preprocess(pre);

            MWArray[] result_MFDFA = new MWArray[5];               //存放输出的数据数组，有5个输出数据
            //瞬时相位分析函数
            result_MFDFA = mfdfa_process.mfdfa(5, MFDFA_Input);

            MWNumericArray Hq_temp = result_MFDFA[0] as MWNumericArray;
            MWNumericArray tq_temp = result_MFDFA[1] as MWNumericArray;
            MWNumericArray alpha_temp = result_MFDFA[2] as MWNumericArray;
            MWNumericArray f_temp = result_MFDFA[3] as MWNumericArray;
            MWNumericArray q_temp = result_MFDFA[4] as MWNumericArray;
            Hq = (double[])Hq_temp.ToVector(MWArrayComponent.Real);
            tq = (double[])tq_temp.ToVector(MWArrayComponent.Real);
            alpha = (double[])alpha_temp.ToVector(MWArrayComponent.Real);
            f = (double[])f_temp.ToVector(MWArrayComponent.Real);
            q = (double[])q_temp.ToVector(MWArrayComponent.Real);
        }
        public void WAVE_Process(double[] processSignal, int let_c,ref double[] a1, ref double[] a2, 
                                    ref double[] a3, ref double[] a4,ref double[] a5, ref double[] a6,
                                    ref double[] a7, ref double[] d1, ref double[] d2, ref double[] d3, 
                                    ref double[] d4, ref double[] d5, ref double[] d6, ref double[] d7 )
        {
            //double[] result = new double[0];
            //预处理数据
            MWNumericArray pre = new MWNumericArray(processSignal);
            MWNumericArray let = new MWNumericArray(let_c);
            MWNumericArray wave_Input = (MWNumericArray)prepro.preprocess(pre);
            MWArray[] input = new MWArray[] { wave_Input, let };
            MWArray[] agrsOut_wave = new MWArray[14];               //存放输出的数据数组，有两个输出数据
            //瞬时相位分析函数
            wave_process.wavelet_S(14, ref agrsOut_wave, input);

            MWNumericArray a1_temp = agrsOut_wave[0] as MWNumericArray;   // 
            MWNumericArray a2_temp = agrsOut_wave[1] as MWNumericArray;   //
            MWNumericArray a3_temp = agrsOut_wave[2] as MWNumericArray;
            MWNumericArray a4_temp = agrsOut_wave[3] as MWNumericArray;
            MWNumericArray a5_temp = agrsOut_wave[4] as MWNumericArray;
            MWNumericArray d1_temp = agrsOut_wave[5] as MWNumericArray;   //
            MWNumericArray d2_temp = agrsOut_wave[6] as MWNumericArray;   //
            MWNumericArray d3_temp = agrsOut_wave[7] as MWNumericArray;
            MWNumericArray d4_temp = agrsOut_wave[8] as MWNumericArray;
            MWNumericArray d5_temp = agrsOut_wave[9] as MWNumericArray;
            MWNumericArray a6_temp = agrsOut_wave[10] as MWNumericArray;
            MWNumericArray a7_temp = agrsOut_wave[11] as MWNumericArray;
            MWNumericArray d6_temp = agrsOut_wave[12] as MWNumericArray;
            MWNumericArray d7_temp = agrsOut_wave[13] as MWNumericArray;


            a1 = (double[])a1_temp.ToVector(MWArrayComponent.Real);      //转为一维数组用ToVector，转为二维数组用ToArray
            a2 = (double[])a2_temp.ToVector(MWArrayComponent.Real);
            a3 = (double[])a3_temp.ToVector(MWArrayComponent.Real);
            a4 = (double[])a4_temp.ToVector(MWArrayComponent.Real);
            a5 = (double[])a5_temp.ToVector(MWArrayComponent.Real);
            d1 = (double[])d1_temp.ToVector(MWArrayComponent.Real);
            d2 = (double[])d2_temp.ToVector(MWArrayComponent.Real);
            d3 = (double[])d3_temp.ToVector(MWArrayComponent.Real);
            d4 = (double[])d4_temp.ToVector(MWArrayComponent.Real);
            d5 = (double[])d5_temp.ToVector(MWArrayComponent.Real);
            a6 = (double[])a6_temp.ToVector(MWArrayComponent.Real);
            a7 = (double[])a7_temp.ToVector(MWArrayComponent.Real);
            d6 = (double[])d6_temp.ToVector(MWArrayComponent.Real);
            d7 = (double[])d7_temp.ToVector(MWArrayComponent.Real);
        }

    }
}

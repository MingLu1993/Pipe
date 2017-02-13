using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Windows.Forms;

using ZedGraph;
using FBGEMSystem.OnlineAnalysis;

namespace FBGEMSystem
{
    public class user
    {
        private const int CH_NUM = 4; //通道数
        private const int MAX_POINT = 12; //每通道，布光纤光栅点数
        public int currentChannel = 0;
        public int CH { get { return CH_NUM; } }//通道数
        public int center_fre1 = 0;
        public int center_fre2 = 0;
        public int center_fre3 = 0;
        public int center_fre4 = 0;
        public double cen_rela_amp1 = 0;
        public double cen_rela_amp2 = 0;
        public double cen_rela_amp3 = 0;
        public double cen_rela_amp4 = 0;
        public double fft_energy1 = 0;
        public double fft_energy2 = 0;
        public double fft_energy3 = 0;
        public double fft_energy4 = 0;
        public double fft_fre_value1 = 0;
        public double fft_max_value1 = 0;
        public double fft_fre_value2 = 0;
        public double fft_max_value2 = 0;
        public double fft_fre_value3 = 0;
        public double fft_max_value3 = 0;
        public double fft_fre_value4 = 0;
        public double fft_max_value4 = 0;
        private const int N = 1024;
        public int length { get { return N; } }//一帧数据N个点，只读属性
        private byte _status = 0;
        public byte status { get { return _status; } set { _status = value; } } //指示当前主机状态，作为可读写属性
        public Queue<double[]>[] msg_signal = new Queue<double[]>[CH_NUM]; //对于每个通道数据建立相应队列
        private const int MAX_MSG_QUENEN = 1024;
        private static object[] sync_msg = new object[CH_NUM];  //udp接收原始数据锁定对象，用于同步

        private const int WINSIZE = 2000; //滑动窗口长度
        private const int MAX_CH_POINT_LEN = 100; //采集点数据缓存长度

        public double[,] temp_ch1_point_signal = new double[MAX_POINT, WINSIZE];
        public double[,] temp_ch2_point_signal = new double[MAX_POINT, WINSIZE];
        public double[,] temp_ch3_point_signal = new double[MAX_POINT, WINSIZE];
        public double[,] temp_ch4_point_signal = new double[MAX_POINT, WINSIZE]; //用于解析包后保存数据，便于后面入队

        public Queue<double[]>[,] ch_point_signal = new Queue<double[]>[CH_NUM, MAX_POINT];//保存每个通道内每个光纤光栅采集数据队列
        private static object[,] sync_ch_point_signal = new object[CH_NUM, MAX_POINT];//对应的同步操作对象
        //存fft处理的数据
        public Queue<double[]>[,] ch_point_fft_signal = new Queue<double[]>[CH_NUM, MAX_POINT];//保存每个通道内每个光纤光栅采集数据队列fft
        private static object[,] sync_ch_point_fft_signal = new object[CH_NUM, MAX_POINT];//对应的同步操作对象
        //样本熵的数据
        public Queue<double>[,] ch_point_kur_signal = new Queue<double>[CH_NUM, MAX_POINT];//保存每个通道内每个光纤光栅采集数据队列sampleEntropy
        private static object[,] sync_ch_point_kur_signal = new object[CH_NUM, MAX_POINT];//对应的同步操作对象
        public fft_Transform fft_tran = new fft_Transform();//新建fft处理类
        public wavedec wavelet_de = new wavedec();
        public waverec wavelet_rec = new waverec();
        public Kurtosis kurtosis = new Kurtosis();
        public string wavename = "db4";
        public int level = 4;
        public string method = "sqtwolog";
        public object sync_wavename = new object();
        public object sync_level = new object();
        public object sync_method = new object();
        public Queue<double[]>[,] ch_point_denoise = new Queue<double[]>[CH_NUM, MAX_POINT]; //小波处理后的数据队列
        private static object[,] sync_ch_point_denoise = new object[CH_NUM, MAX_POINT];//对应的同步操作对象


        public Queue<Message_FBG> all_msg = new Queue<Message_FBG>(); //接收数据帧缓存队列
        private static object sync_all_msg = new object(); //对应的同步操作对象

        //recvbytes用于接收udp字节数据，之后转化成temp_msg结构体入队。
        public Message_FBG temp_msg = new Message_FBG();

        public Message_FBG pre_msg = new Message_FBG();//出队等待处理

        private bool _isfull;
        public bool isfull { get { return _isfull; } set { _isfull = value; } } //接收缓存溢出标志属性


        public struct pro_message
        {
            public double[] c;
            public int[] l;
            public int level;
        };

        public Queue<pro_message>[,] pro_result = new Queue<pro_message>[CH_NUM,MAX_POINT];
        private object[,] sync_pro_result = new object[CH_NUM, MAX_POINT];


        public int recv_cnt = 0; //已接收帧计数
        public int pro_cnt = 0; //已处理计数
        public int aband = 0; //丢弃包计数

        public int real_pro = 0; //真实处理帧数
        public int un_pro = 0;

        private static object sync_sample = new object(); //采样间隔点同步操作对象
        public int sample = 0; //采样间隔点

        /*
         * 构造函数：实例化时，执行此函数，完成一些变量初始化
         * 输入：无
         * 输出：无
         */
        public user()
        {
            for (int i = 0; i < CH_NUM; i++)
            {
                //if (i!=currentChannel)
                //{
                //    continue;
                //}
                sync_msg[i] = new object();
                msg_signal[i] = new Queue<double[]>();
            }

            for (int i = 0; i < CH_NUM; i++)
            {
                //if (i != currentChannel)
                //{
                //    continue;
                //}
                for (int j = 0; j < MAX_POINT; j++)
                {
                    ch_point_signal[i, j] = new Queue<double[]>();
                    sync_ch_point_signal[i, j] = new object();

                    ch_point_fft_signal[i, j] = new Queue<double[]>();
                    sync_ch_point_fft_signal[i, j] = new object();

                    ch_point_denoise[i, j] = new Queue<double[]>();
                    sync_ch_point_denoise[i, j] = new object();

                    ch_point_kur_signal[i, j] = new Queue<double>();
                    sync_ch_point_kur_signal[i, j] = new object();
                }
            }
            for (int i = 0; i < CH_NUM; i++)
            {
                for (int j = 0; j < MAX_POINT; j++)
                {
                    pro_result[i,j] = new Queue<pro_message>();
                    sync_pro_result[i,j] = new object();
                }
            }
            //_isfull = false;
        }

        /*
         * 保存接收数据，同步操作数据帧队列
         * 输入：
         *      Message msg ： 待保存数据帧
         * 输出：
         *      无
         */
        public void save_msg(Message_FBG msg)
        {
            lock (sync_all_msg)
            {
                if (all_msg.Count > MAX_MSG_QUENEN)
                {
                    _isfull = true;
                }
                all_msg.Enqueue(msg);
                recv_cnt++;
            }
        }

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
        public void process(int channel, int point, ref double[] result)
        {
            double[] temp = new double[WINSIZE];
            lock (sync_ch_point_denoise[channel, point])
            {
                temp = ch_point_denoise[channel, point].Dequeue();
            }
            switch (channel)
            {
                case 0:
                    result = temp;
                    break;
                case 1:
                    result = temp;
                    break;
                case 2:
                    result = temp;
                    break;
                case 3:
                    result = temp;
                    break;
            }
        }
        /*
         * 预处理信号函数，包括解析数据帧，信号去噪，保存去噪后信号至队列
         * 输入：
         *      int channel ： 通道数
         *      int point ： 通道中的采集点
         * 输出：
         *      无
         */
        public void denoise(int channel, int point, ref double[] result)
        {
            double[] temp_process = new double[WINSIZE];
            lock (sync_ch_point_signal[channel, point])
            {
                temp_process = ch_point_signal[channel, point].Dequeue(); //出队，处理数据
            }
            denoise(temp_process, wavename, level, ref result);
            /* switch (channel)
             {
                 case 0:
                     result = temp_process;
                     break;
                 case 1:
                     result = temp_process;
                     break;
                 case 2:
                     result = temp_process;
                     break;
                 case 3:
                     {
                        
                     }
                     break;
             }*/

            if (ch_point_denoise[channel, point].Count > 5) //只保存10个元素，当元素超过10个，等待
            {
                while (ch_point_denoise[channel, point].Count > 5)
                {
                    Thread.Sleep(5);
                }
            }
            lock (sync_ch_point_denoise[channel, point])
            {
                ch_point_denoise[channel, point].Enqueue(result);
            }
        }
        private void denoise(double[] signal, string wavename, int level, ref double[] result)
        {
            double[] c = new double[0];
            int[] l = new int[0];
            wavelet_de.wdec(signal, level, wavename, ref c, ref l);
            wavelet_de.threshold(ref c, ref l, method);
            wavelet_rec.wrec(c, l, wavename, ref result, 0);
        }

        public void wavelet_process(double[] signal, string wavename, int level,int channel, int point)
        {
            pro_message temp_pro_message = new pro_message();
            double[] c = new double[0];
            int[] l = new int[0];
            wavelet_de.wdec(signal, level, wavename, ref c, ref l);

            temp_pro_message.c = new double[c.Length];
            temp_pro_message.l = new int[7];

            temp_pro_message.level = level;
            for (int i = 0; i < WINSIZE; i++)
            {
                temp_pro_message.c[i] = c[i];
            }
            int j = 0;
            for (int i = l.Length; i > 0; i--)
            {
                temp_pro_message.l[j++] = l[i - 1];
            }


            while (pro_result[channel,point].Count > 10)
            {
                Thread.Sleep(5);
            }

            lock (sync_pro_result[channel,point])
            {
                pro_result[channel,point].Enqueue(temp_pro_message);
            }
        }


        public double[] extract(int channel,int point, ref double[] a, ref double[] d1, ref double[] d2, ref double[] d3, ref double[] d4, ref double[] d5, string type)
        {
            double[] energy = new double[6];
            double sum = 0;
            pro_message temp_pro_message = new pro_message();
            temp_pro_message.c = new double[WINSIZE];
            temp_pro_message.l = new int[7];
            if ("kurtosis" == type)
            {
                lock (sync_pro_result[channel,point])
                {
                    temp_pro_message = pro_result[channel,point].Dequeue();
                }

            }
            else if ("energy" == type)
            {
                lock (sync_pro_result[channel,point])
                {
                    temp_pro_message = pro_result[channel,point].Dequeue();
                }

            }

            int e = 0;
            for (int i = 1; i < 7; i++)
            {
                e += temp_pro_message.l[i];
            }

            sum = re_energy(temp_pro_message.c, 0, e);
            double ca = new double();
            double cd1 = new double();
            double cd2 = new double();
            double cd3 = new double();
            double cd4 = new double();
            double cd5 = new double();

            switch (temp_pro_message.level)
            {
                case 1:// c : CA1        CD1              l : L CD1 CA1
                    //     0:l[2]-1   l[2]:l[1]-1
                    if (type == "energy")
                    {
                        ca = re_energy(temp_pro_message.c, 0, temp_pro_message.l[2] - 1);
                        cd1 = re_energy(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1);
                    }
                    else if (type == "kurtosis")
                    {
                        ca = kurtosis.kur_get_len(temp_pro_message.c, 0, temp_pro_message.l[2] - 1, 0);
                        cd1 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1, 0);
                    }
                    a = new double[temp_pro_message.l[2] - 1];
                    d1 = new double[temp_pro_message.l[1] - 1];
                    for (int i = 0; i < temp_pro_message.l[2] - 1; i++)
                    {
                        a[i] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[2]; i < temp_pro_message.l[2] + temp_pro_message.l[1] - 1; i++)
                    {
                        d1[i - temp_pro_message.l[2]] = temp_pro_message.c[i];
                    }
                    if (type == "energy")
                    {
                        ca = ca / sum;
                        cd1 = cd1 / sum;
                    }
                    energy[0] = ca;
                    energy[1] = cd1;
                    break;
                case 2:
                    if (type == "energy")
                    {
                        ca = re_energy(temp_pro_message.c, 0, temp_pro_message.l[3] - 1);
                        cd2 = re_energy(temp_pro_message.c, temp_pro_message.l[3], temp_pro_message.l[2] - 1);
                        cd1 = re_energy(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1);
                    }
                    else if (type == "kurtosis")
                    {
                        ca = kurtosis.kur_get_len(temp_pro_message.c, 0, temp_pro_message.l[3] - 1, 0);
                        cd2 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[3], temp_pro_message.l[2] - 1, 0);
                        cd1 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1, 0);
                    }
                    a = new double[temp_pro_message.l[3] - 1];
                    d2 = new double[temp_pro_message.l[2] - 1];
                    d1 = new double[temp_pro_message.l[1] - 1];
                    for (int i = 0; i < temp_pro_message.l[3] - 1; i++)
                    {
                        a[i] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[3]; i < temp_pro_message.l[3] + temp_pro_message.l[2] - 1; i++)
                    {
                        d2[i - temp_pro_message.l[3]] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[2]; i < temp_pro_message.l[2] + temp_pro_message.l[1] - 1; i++)
                    {
                        d1[i - temp_pro_message.l[2]] = temp_pro_message.c[i];
                    }
                    if (type == "energy")
                    {
                        ca = ca / sum;
                        cd1 = cd1 / sum;
                        cd2 = cd2 / sum;
                    }
                    energy[0] = ca;
                    energy[1] = cd1;
                    energy[2] = cd2;
                    break;
                case 3:
                    if (type == "energy")
                    {
                        ca = re_energy(temp_pro_message.c, 0, temp_pro_message.l[4] - 1);
                        cd3 = re_energy(temp_pro_message.c, temp_pro_message.l[4], temp_pro_message.l[3] - 1);
                        cd2 = re_energy(temp_pro_message.c, temp_pro_message.l[3], temp_pro_message.l[2] - 1);
                        cd1 = re_energy(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1);
                    }
                    else if (type == "kurtosis")
                    {
                        ca = kurtosis.kur_get_len(temp_pro_message.c, 0, temp_pro_message.l[4] - 1, 0);
                        cd3 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[4], temp_pro_message.l[3] - 1, 0);
                        cd2 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[3], temp_pro_message.l[2] - 1, 0);
                        cd1 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1, 0);
                    }
                    a = new double[temp_pro_message.l[4] - 1];
                    d3 = new double[temp_pro_message.l[3] - 1];
                    d2 = new double[temp_pro_message.l[2] - 1];
                    d1 = new double[temp_pro_message.l[1] - 1];
                    for (int i = 0; i < temp_pro_message.l[4] - 1; i++)
                    {
                        a[i] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[4]; i < temp_pro_message.l[4] + temp_pro_message.l[3] - 1; i++)
                    {
                        d3[i - temp_pro_message.l[4]] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[3]; i < temp_pro_message.l[3] + temp_pro_message.l[2] - 1; i++)
                    {
                        d2[i - temp_pro_message.l[3]] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[2]; i < temp_pro_message.l[2] + temp_pro_message.l[1] - 1; i++)
                    {
                        d1[i - temp_pro_message.l[2]] = temp_pro_message.c[i];
                    }
                    if (type == "energy")
                    {
                        ca = ca / sum;
                        cd1 = cd1 / sum;
                        cd2 = cd2 / sum;
                        cd3 = cd3 / sum;
                    }
                    energy[0] = ca;
                    energy[1] = cd1;
                    energy[2] = cd2;
                    energy[3] = cd3;
                    break;
                case 4:  // c :  CA4          CD4          CD3          CD2         CD1        l : L CD1 CD2 CD3 CD4 CA4
                    //    0:l[5]-1   l[5]:l[4]-1   l[4]:l[3]-1   l[3]:l[2]-1  l[2]:l[1]-1      0  1   2   3   4   5
                    if (type == "energy")
                    {
                        ca = re_energy(temp_pro_message.c, 0, temp_pro_message.l[5] - 1);
                        cd4 = re_energy(temp_pro_message.c, temp_pro_message.l[5], temp_pro_message.l[4] - 1);
                        cd3 = re_energy(temp_pro_message.c, temp_pro_message.l[4], temp_pro_message.l[3] - 1);
                        cd2 = re_energy(temp_pro_message.c, temp_pro_message.l[3], temp_pro_message.l[2] - 1);
                        cd1 = re_energy(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1);
                    }
                    else if (type == "kurtosis")
                    {
                        ca = kurtosis.kur_get_len(temp_pro_message.c, 0, temp_pro_message.l[5] - 1, 0);
                        cd4 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[5], temp_pro_message.l[4] - 1, 0);
                        cd3 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[4], temp_pro_message.l[3] - 1, 0);
                        cd2 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[3], temp_pro_message.l[2] - 1, 0);
                        cd1 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1, 0);
                    }
                    a = new double[temp_pro_message.l[5] - 1];
                    d4 = new double[temp_pro_message.l[4] - 1];
                    d3 = new double[temp_pro_message.l[3] - 1];
                    d2 = new double[temp_pro_message.l[2] - 1];
                    d1 = new double[temp_pro_message.l[1] - 1];

                    for (int i = 0; i < temp_pro_message.l[5] - 1; i++)
                    {
                        a[i] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[5]; i < temp_pro_message.l[5] + temp_pro_message.l[4] - 1; i++)
                    {
                        d4[i - temp_pro_message.l[5]] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[4]; i < temp_pro_message.l[4] + temp_pro_message.l[3] - 1; i++)
                    {
                        d3[i - temp_pro_message.l[4]] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[3]; i < temp_pro_message.l[3] + temp_pro_message.l[2] - 1; i++)
                    {
                        d2[i - temp_pro_message.l[3]] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[2]; i < temp_pro_message.l[2] + temp_pro_message.l[1] - 1; i++)
                    {
                        d1[i - temp_pro_message.l[2]] = temp_pro_message.c[i];
                    }
                    if (type == "energy")
                    {
                        ca = ca / sum;
                        cd1 = cd1 / sum;
                        cd2 = cd2 / sum;
                        cd3 = cd3 / sum;
                        cd4 = cd4 / sum;
                    }

                    energy[0] = ca;
                    energy[1] = cd1;
                    energy[2] = cd2;
                    energy[3] = cd3;
                    energy[4] = cd4;
                    break;
                case 5:
                    if (type == "energy")
                    {
                        ca = re_energy(temp_pro_message.c, 0, temp_pro_message.l[6] - 1);
                        cd5 = re_energy(temp_pro_message.c, temp_pro_message.l[6], temp_pro_message.l[5] - 1);
                        cd4 = re_energy(temp_pro_message.c, temp_pro_message.l[5], temp_pro_message.l[4] - 1);
                        cd3 = re_energy(temp_pro_message.c, temp_pro_message.l[4], temp_pro_message.l[3] - 1);
                        cd2 = re_energy(temp_pro_message.c, temp_pro_message.l[3], temp_pro_message.l[2] - 1);
                        cd1 = re_energy(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1);
                    }
                    else if (type == "kurtosis")
                    {
                        ca = kurtosis.kur_get_len(temp_pro_message.c, 0, temp_pro_message.l[6] - 1, 0);
                        cd5 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[6], temp_pro_message.l[5] - 1, 0);
                        cd4 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[5], temp_pro_message.l[4] - 1, 0);
                        cd3 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[4], temp_pro_message.l[3] - 1, 0);
                        cd2 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[3], temp_pro_message.l[2] - 1, 0);
                        cd1 = kurtosis.kur_get_len(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1, 0);
                    }
                    a = new double[temp_pro_message.l[6] - 1];
                    d5 = new double[temp_pro_message.l[5] - 1];
                    d4 = new double[temp_pro_message.l[4] - 1];
                    d3 = new double[temp_pro_message.l[3] - 1];
                    d2 = new double[temp_pro_message.l[2] - 1];
                    d1 = new double[temp_pro_message.l[1] - 1];
                    for (int i = 0; i < temp_pro_message.l[5] - 1; i++)
                    {
                        a[i] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[6]; i < temp_pro_message.l[6] + temp_pro_message.l[5] - 1; i++)
                    {
                        d5[i - temp_pro_message.l[6]] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[5]; i < temp_pro_message.l[5] + temp_pro_message.l[4] - 1; i++)
                    {
                        d4[i - temp_pro_message.l[5]] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[4]; i < temp_pro_message.l[4] + temp_pro_message.l[3] - 1; i++)
                    {
                        d3[i - temp_pro_message.l[4]] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[3]; i < temp_pro_message.l[3] + temp_pro_message.l[2] - 1; i++)
                    {
                        d2[i - temp_pro_message.l[3]] = temp_pro_message.c[i];
                    }
                    for (int i = temp_pro_message.l[2]; i < temp_pro_message.l[2] + temp_pro_message.l[1] - 1; i++)
                    {
                        d1[i - temp_pro_message.l[2]] = temp_pro_message.c[i];
                    }
                    if (type == "energy")
                    {
                        ca = ca / sum;
                        cd1 = cd1 / sum;
                        cd2 = cd2 / sum;
                        cd3 = cd3 / sum;
                        cd4 = cd4 / sum;
                        cd5 = cd5 / sum;
                    }
                    energy[0] = ca;
                    energy[1] = cd1;
                    energy[2] = cd2;
                    energy[3] = cd3;
                    energy[4] = cd4;
                    energy[5] = cd5;
                    break;
            }
            return energy;
        }
        private double re_energy(double[] signal, int start, int end)
        {
            double sum = new double();
            for (int i = start; i < start + end; i++)
            {
                sum += Math.Pow(signal[i], 2);
            }
            return sum;
        }
        /*
        * FFT变换
        * 输入：
        * int channel：通道数
        * int point:通道中的采集点
        * 输出：
        * 无
        */
        public void FFT_Transform(int channel, int point, ref double[] result1)
        {
            double[] temp_process = new double[WINSIZE];
            for (int i = 0; i < WINSIZE; i++)
            {
                temp_process[i] = result1[i];
            }
            fft_tran.Dit2FFTAmplitude(temp_process, ref result1);
            if (ch_point_fft_signal[channel, point].Count > 10) //只保存10个元素，当元素超过10个，等待
            {
                Thread.Sleep(5);
            }
            lock (sync_ch_point_fft_signal[channel, point])
            {
                ch_point_fft_signal[channel, point].Enqueue(result1);
            }
        }


        /*
         * 调整采样率函数，根据接受缓存状态调节采样间隔点数
         * 输入：
         *      int flag ： 0 表示增加间隔点
         *                  1 表示减小间隔点
         * 输出：
         *      无
         */
        public void range_sample(int flag)
        {
            if (flag == 0) //隔点采样数增加
            {
                if (sample == 0)
                {
                    lock (sync_sample)
                    {
                        sample = 0 + 5 * 8;
                    }
                }

                if (recv_cnt - pro_cnt > MAX_MSG_QUENEN)
                {
                    if (sample > 500)
                        sample = 1000;
                    else
                        sample = 2 * sample;
                }
            }
            if (flag == 1)
            {
                sample = 0;
                if (sample > 0)
                {
                    lock (sync_sample)
                    {
                        sample--;
                    }
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
                    if (ch_point_signal[i, j].Count > MAX_CH_POINT_LEN)
                    {
                        while (ch_point_signal[i, j].Count > MAX_CH_POINT_LEN) //4个通道，每个通道40个点，每个点滑动窗口400个数据，每个点缓存为10
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
                    lock (sync_ch_point_signal[i, j])
                    {
                        ch_point_signal[i, j].Enqueue(temp);
                    }
                }
            }
        }
        public double get_kurtosis(int channel, int point)
        {
            double temp = new double();

            lock (sync_ch_point_kur_signal[channel - 1, point])
            {
                if (ch_point_kur_signal[channel - 1, point].Count > 1)
                {
                    temp = ch_point_kur_signal[channel - 1, point].Dequeue();
                }
            }

            return temp;
        }
        /*
         * 函数说明：
         *      获取通道中的采集点的滑动窗口数据，显示
         *  输入：
         *      int channel ： 选择通道数
         *  输出：
         *      返回 PointPairList[] 形式的alist，包含channel中每个采集点的滑动窗口数据
         */
        public PointPairList[] Get_PointPair(int channel, string type)
        {
            double[] temp = new double[WINSIZE];
            //  double[] temp_fft = new double[WINSIZE / 2 + 1];
            PointPairList[] alist = new PointPairList[MAX_POINT];
            if (type == "original")
            {
                for (int i = 0; i < MAX_POINT; i++)
                {
                    alist[i] = new PointPairList();
                    lock (sync_ch_point_signal[channel - 1, i])
                    {
                        if (ch_point_signal[channel - 1, i].Count > 0)
                        {
                            temp = ch_point_signal[channel - 1, i].ElementAt(ch_point_signal[channel - 1, i].Count - 1);
                        }
                    }
                    for (int x = 0; x < WINSIZE; x++)
                    {
                        alist[i].Add(x, temp[x]);
                    }
                }
            }
            else if (type == "denoise")
            {
                for (int i = 0; i < MAX_POINT; i++)
                {
                    alist[i] = new PointPairList();
                    lock (sync_ch_point_denoise[channel - 1, i])
                    {
                        if (ch_point_denoise[channel - 1, i].Count >0)
                        {
                            //temp= ch_point_denoise[channel - 1, i].Dequeue();
                            temp = ch_point_denoise[channel - 1, i].ElementAt(ch_point_denoise[channel - 1, i].Count - 1);
                        }
                    }
                    for (int x = 0; x < WINSIZE; x++)
                    {
                        alist[i].Add(x, temp[x]);
                    }
                }
            }
            return alist;
        }

        public PointPairList[] Get_PointPair_FFT(int channel, int point,bool com)
        {
            double[] temp_fft = new double[WINSIZE / 2 + 1];
            PointPairList[] alist = new PointPairList[MAX_POINT];
            for (int i = 0; i < MAX_POINT; i++)
            {
                alist[i] = new PointPairList();
                double[,] temp_double2 = new double[MAX_POINT, WINSIZE / 2 + 1];
                lock (sync_ch_point_fft_signal[channel - 1, i])
                {
                    if (ch_point_fft_signal[channel - 1, i].Count > 1)
                    {
                        temp_fft = ch_point_fft_signal[channel - 1, i].Dequeue();
                        //     temp_fft = ch_point_fft_signal[channel - 1, i].ElementAt(ch_point_fft_signal[channel - 1, i].Count - 1);
                    }
                }
                //这里有问题，判断的是最后一个点的FFT，需要修改
                for (int m = 0; m <= WINSIZE / 2; m++)
                {
                    temp_double2[i, m] = temp_fft[m];
                }
                if ((i + 1) == point)//假设传进来的是第三个点
                {
                    double fft_max_value = 0;
                    double fft_fre_value = 0;
                    double max = temp_double2[i, 1];
                    int max_log = 0;
                    int n = 0;
                    double[] temp_energy = new double[WINSIZE / 2];
                    double energy = 0;
                    double cen_rela_amp = 0;
                    int center_fre = 0;
                    for (int k = 1; k <= WINSIZE / 2; k++)
                    {
                        temp_energy[k - 1] = temp_double2[i, k];
                        if (temp_double2[i, k] > max)
                        {
                            n = k;
                            max = temp_double2[i, k];
                            max_log = k;
                        }
                        fft_max_value = max;
                        fft_fre_value = max_log;
                    }
                    energy = CalEnergy(temp_energy);
                    center_fre = CalCenFre(temp_energy);
                    if (center_fre>=1)
                    {
                        cen_rela_amp = temp_energy[center_fre - 1];
                        if (com == true)
                        {
                            fft_fre_value1 = fft_fre_value;
                            fft_max_value1 = fft_max_value;
                            fft_energy1 = energy;
                            center_fre1 = center_fre;
                            cen_rela_amp1 = cen_rela_amp;
                        }
                        if (com == true)
                        {
                            fft_fre_value2 = fft_fre_value;
                            fft_max_value2 = fft_max_value;
                            fft_energy2 = energy;
                            center_fre2 = center_fre;
                            cen_rela_amp2 = cen_rela_amp;
                        }
                        if (com == true)
                        {
                            fft_fre_value3 = fft_fre_value;
                            fft_max_value3 = fft_max_value;
                            fft_energy3 = energy;
                            center_fre3 = center_fre;
                            cen_rela_amp3 = cen_rela_amp;
                        }
                        if (com == true)
                        {
                            fft_fre_value4 = fft_fre_value;
                            fft_max_value4 = fft_max_value;
                            fft_energy4 = energy;
                            center_fre4 = center_fre;
                            cen_rela_amp4 = cen_rela_amp;
                        }
                    }
                   
                }
                for (int k = 1; k <= WINSIZE / 2; k++)
                {

                    double j = (double)k / (WINSIZE / 2);
                    //double m = 1000 * j;
                    //double n = Math.Log10(m);
                    double m = temp_fft[k];
                    alist[i].Add(j, m);
                    //alist[i].Add(n, temp_fft[k-1]);
                }


            }
            return alist;
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
                    temp_ch1_point_signal[k, j * Data.FBG_numPackage + i] = msg.CH1[i * Data.FBG_numPackage + k];
                    temp_ch2_point_signal[k, j * Data.FBG_numPackage + i] = msg.CH2[i * Data.FBG_numPackage + k];
                    temp_ch3_point_signal[k, j * Data.FBG_numPackage + i] = msg.CH3[i * Data.FBG_numPackage + k];
                    temp_ch4_point_signal[k, j * Data.FBG_numPackage + i] = msg.CH4[i * Data.FBG_numPackage + k];
                }
            }
        }

        public void change_wave(string name, int lev)
        {
            lock (sync_wavename)
            {
                wavename = name;
            }
            lock (sync_level)
            {
                level = lev;
            }

        }

        /*
        * 样本熵
       * int channel：通道数
        * int point:通道中的采集点
        * 输出：
        * 无
     */
        public void CalKurtosis(int channel, int point, ref double[] result2)
        {
            double kurvalue = 0;
            double[] temp_process = new double[WINSIZE];
            for (int i = 0; i < WINSIZE; i++)
            {
                temp_process[i] = result2[i];
            }
            //if (channel == 3)
            //{
                double[,] temp_process2 = new double[temp_process.Length,1];
                for (int i = 0; i < temp_process.Length; i++)
                {
                    temp_process2[i, 0] = temp_process[i];
                }
                double[] kurt = kurtosis.kur(temp_process2, 0);
                kurvalue = kurt[0];
          //  }
            //超过了10个就出队，止到小于等于10个为止
            if (ch_point_kur_signal[channel, point].Count > 10) //只保存10个元素，当元素超过10个，等待
            {
                Thread.Sleep(5);
            }
            lock (sync_ch_point_kur_signal[channel, point])
            {
                ch_point_kur_signal[channel, point].Enqueue(kurvalue);
            }
        }
        //计算double数组的能量
        public double CalEnergy(double[] signal)
        {
            double sum_energy = 0;
            int len = signal.Length;
            for (int i = 0; i < len; i++)
            {
                sum_energy = sum_energy + Math.Pow(signal[i], 2);
            }
            return sum_energy;
        }
        //计算中心频率
        public int CalCenFre(double[] signal)
        {
            double sig_energy = CalEnergy(signal);
            double temp = 0;
            int k = 0;
            while (temp < sig_energy / 2 && k < signal.Length)
            {
                temp = temp + Math.Pow(signal[k], 2);
                k = k + 1;

            }
            return k;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Windows.Forms;

using ZedGraph;

namespace WindowsFormsApplication2_20130422
{
    public class user
    {
        private const int CH_NUM = 4; //通道数
        private const int MAX_POINT = 64; //每通道，布光纤光栅点数
        public int CH { get { return CH_NUM; } }//通道数
        

        private const int N = 1024;
        public int length { get { return N; } }//一帧数据N个点，只读属性
        private byte _status = 0;
        public byte status { get { return _status; } set { _status = value; } } //指示当前主机状态，作为可读写属性
        public Queue<double []>[] msg_signal = new Queue<double []>[CH_NUM]; //对于每个通道数据建立相应队列
        private const int MAX_MSG_QUENEN = 1024;
        private static object[] sync_msg = new object[CH_NUM];  //udp接收原始数据锁定对象，用于同步

        private const int WINSIZE = 4000; //滑动窗口长度
        private const int MAX_CH_POINT_LEN = 100; //采集点数据缓存长度

        public double[,] temp_ch1_point_signal = new double[MAX_POINT, WINSIZE];
        public double[,] temp_ch2_point_signal = new double[MAX_POINT, WINSIZE];
        public double[,] temp_ch3_point_signal = new double[MAX_POINT, WINSIZE];
        public double[,] temp_ch4_point_signal = new double[MAX_POINT, WINSIZE]; //用于解析包后保存数据，便于后面入队

        public Queue<double[]>[,] ch_point_signal = new Queue<double[]>[CH_NUM, MAX_POINT];//保存每个通道内每个光纤光栅采集数据队列
        private static object[,] sync_ch_point_signal = new object[CH_NUM, MAX_POINT];//对应的同步操作对象


        public wavedec wavelet_de = new wavedec();
        public waverec wavelet_rec = new waverec();
        public string wavename = "db4";
        public int level = 4;
        public string method = "sqtwolog";
        public object sync_wavename = new object();
        public object sync_level = new object();
        public object sync_method = new object();
        public Queue<double[]>[,] ch_point_denoise = new Queue<double[]>[CH_NUM, MAX_POINT]; //小波处理后的数据队列
        private static object[,] sync_ch_point_denoise = new object[CH_NUM, MAX_POINT];//对应的同步操作对象


        public Queue<Message> all_msg = new Queue<Message>(); //接收数据帧缓存队列
        private static object sync_all_msg = new object(); //对应的同步操作对象

        //recvbytes用于接收udp字节数据，之后转化成temp_msg结构体入队。
        public Message temp_msg = new Message();

        public Message pre_msg = new Message();//出队等待处理

        private bool _isfull; 
        public bool isfull { get { return _isfull; } set { _isfull = value; } } //接收缓存溢出标志属性


        public struct pro_message 
        {
            public double [] c;
            public int [] l;
            public int level;
        };

        public Queue<pro_message> [] pro_result = new Queue<pro_message>[MAX_POINT];
        private object [] sync_pro_result = new object[MAX_POINT];


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
                sync_msg[i] = new object();
                msg_signal[i] = new Queue<double[]>();
            }

            for (int i = 0; i < CH_NUM; i++)
            {
                for (int j = 0; j < MAX_POINT; j++)
                {
                    ch_point_signal[i, j] = new Queue<double[]>();
                    sync_ch_point_signal[i, j] = new object();

                    ch_point_denoise[i, j] = new Queue<double[]>();
                    sync_ch_point_denoise[i, j] = new object();
                }
            }

            for (int i = 0; i < MAX_POINT; i++)
            {
                pro_result[i] = new Queue<pro_message>();
                sync_pro_result[i] = new object();
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
        public void save_msg(Message msg)  
        {     
            lock(sync_all_msg)
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
        public Message del_msg()
        {
            lock (sync_all_msg)
            {
                if ((all_msg.Count < MAX_MSG_QUENEN / 2) && (_isfull == true))
                {
                    _isfull = false;
                }
                // pro_cnt++;
                return all_msg.Dequeue();
            }
        }
        public void process(int channel, int point, ref double [] result)
        { 
            double [] temp = new double[WINSIZE];
            lock(sync_ch_point_denoise[channel,point])
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
                case 2 :
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
        public void denoise(int channel, int point, ref double [] result)
        {
            double [] temp_process = new double[WINSIZE];
            //double [] result = new double[0];
            lock (sync_ch_point_signal[channel, point])
            {
                temp_process = ch_point_signal[channel, point].Dequeue(); //出队，处理数据
            }

            switch (channel)
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
                        denoise(temp_process, wavename, level, ref result);
                    }
                    break;
            }

            if (ch_point_denoise[channel, point].Count > 10) //只保存10个元素，当元素超过10个，等待
            {
                while (ch_point_denoise[channel, point].Count > 10)
                {
                    Thread.Sleep (5);
                }
                /*lock (sync_ch_point_denoise[channel, point])
                {
                    ch_point_denoise[channel, point].Dequeue();
                }*/
            }
            lock (sync_ch_point_denoise[channel, point])
            {
                ch_point_denoise[channel, point].Enqueue(result);
            }



            //Thread.Sleep(20); //模拟处理时间
        }
        private void denoise(double[] signal, string wavename, int level, ref double[] result)
        {
            double [] c = new double[0];
            int[] l = new int[0];
            wavelet_de.wdec(signal, level, wavename, ref c, ref l);
            wavelet_de.threshold(ref c, ref l, method);
            wavelet_rec.wrec(c, l, wavename, ref result, 0);
        }

        public void wavelet_process(double [] signal, string wavename, int level, int point)
        {
            pro_message temp_pro_message = new pro_message();
           // temp_pro_message.c = new double[WINSIZE];
           // temp_pro_message.l = new int[7];
            double [] c = new double[0];
            int [] l = new int[0];
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


            while(pro_result[point].Count > 10)
            {
                Thread.Sleep(5); 
            }

            lock(sync_pro_result[point])
            {
                pro_result[point].Enqueue(temp_pro_message);
            }
        }

        public double [] extract(int point,ref double [] a, ref double[] d1, ref double [] d2, ref double [] d3, ref double [] d4, ref double [] d5)
        {
            double[] energy = new double[6];

            pro_message temp_pro_message = new pro_message();
            temp_pro_message.c = new double[WINSIZE];
            temp_pro_message.l = new int[7];

            lock(sync_pro_result[point])
            {
                temp_pro_message = pro_result[point].Dequeue ();
            }

            int e = 0;
            for(int i = 1; i < 7; i++)
            {
                e += temp_pro_message.l[i];
            }
            double sum = re_energy(temp_pro_message.c, 0, e);
            double ca = new double();
            double cd1 = new double();
            double cd2 = new double();
            double cd3 = new double();
            double cd4 = new double();
            double cd5 = new double();

            switch(temp_pro_message.level)
            {
                case 1:// c : CA1        CD1              l : L CD1 CA1
                       //     0:l[2]-1   l[2]:l[1]-1
                    ca = re_energy(temp_pro_message.c, 0, temp_pro_message.l[2]-1);
                    cd1 = re_energy(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1);
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
                    ca = ca / sum;
                    cd1 = cd1 / sum;
                    energy[0] = ca;
                    energy[1] = cd1;
                    break;
                case 2:
                    ca = re_energy(temp_pro_message.c, 0, temp_pro_message.l[3] - 1);
                    cd2 = re_energy(temp_pro_message.c, temp_pro_message.l[3], temp_pro_message.l[2] - 1);
                    cd1 = re_energy(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1);
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
                    ca = ca / sum;
                    cd1 = cd1 / sum;
                    cd2 = cd2 / sum;

                    energy[0] = ca;
                    energy[1] = cd1;
                    energy[2] = cd2;
                    break;
                case 3:
                    ca = re_energy(temp_pro_message.c, 0, temp_pro_message.l[4] - 1);
                    cd3 = re_energy(temp_pro_message.c, temp_pro_message.l[4], temp_pro_message.l[3] - 1);
                    cd2 = re_energy(temp_pro_message.c, temp_pro_message.l[3], temp_pro_message.l[2] - 1);
                    cd1 = re_energy(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1);
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
                    ca = ca / sum;
                    cd1 = cd1 / sum;
                    cd2 = cd2 / sum;
                    cd3 = cd3 / sum;

                    energy[0] = ca;
                    energy[1] = cd1;
                    energy[2] = cd2;
                    energy[3] = cd3;
                    break;
                case 4:  // c :  CA4          CD4          CD3          CD2         CD1        l : L CD1 CD2 CD3 CD4 CA4
                        //    0:l[5]-1   l[5]:l[4]-1   l[4]:l[3]-1   l[3]:l[2]-1  l[2]:l[1]-1      0  1   2   3   4   5
                    ca = re_energy(temp_pro_message.c, 0, temp_pro_message.l[5] - 1);
                    cd4 = re_energy(temp_pro_message.c, temp_pro_message.l[5], temp_pro_message.l[4] - 1);
                    cd3 = re_energy(temp_pro_message.c, temp_pro_message.l[4], temp_pro_message.l[3] - 1);
                    cd2 = re_energy(temp_pro_message.c, temp_pro_message.l[3], temp_pro_message.l[2] - 1);
                    cd1 = re_energy(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1);
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
                    ca = ca / sum;
                    cd1 = cd1 / sum;
                    cd2 = cd2 / sum;
                    cd3 = cd3 / sum;
                    cd4 = cd4 / sum;

                    energy[0] = ca;
                    energy[1] = cd1;
                    energy[2] = cd2;
                    energy[3] = cd3;
                    energy[4] = cd4;
                    break;
                case 5: 
                    ca = re_energy(temp_pro_message.c, 0, temp_pro_message.l[6] - 1);
                    cd5 = re_energy(temp_pro_message.c, temp_pro_message.l[6], temp_pro_message.l[5] - 1);
                    cd4 = re_energy(temp_pro_message.c, temp_pro_message.l[5], temp_pro_message.l[4] - 1);
                    cd3 = re_energy(temp_pro_message.c, temp_pro_message.l[4], temp_pro_message.l[3] - 1);
                    cd2 = re_energy(temp_pro_message.c, temp_pro_message.l[3], temp_pro_message.l[2] - 1);
                    cd1 = re_energy(temp_pro_message.c, temp_pro_message.l[2], temp_pro_message.l[1] - 1);
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
                    ca = ca / sum;
                    cd1 = cd1 / sum;
                    cd2 = cd2 / sum;
                    cd3 = cd3 / sum;
                    cd4 = cd4 / sum;
                    cd5 = cd5 / sum;

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
        private double re_energy(double [] signal, int start, int end)
        {
            double sum = new double();
            for (int i = start; i < start + end; i++)
            {
                sum += Math.Pow(signal[i],2);
            }
                return sum;
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
               /* if (sample < 0 + 100 * 8)
                {
                    lock (sync_sample)
                    {
                        sample = sample + 8;
                    }
                }

                if (sample > 0 + 100 * 8)
                {
                    lock (sync_sample)
                    {
                        sample = 0 + 100 * 8;
                    }
                }*/

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
                       // sample = 0;
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
                if (all_msg.Count > WINSIZE / 40)
                {
                    del_msg();
                    pro_cnt++;
                }
            }
            for (int j = 0; j < WINSIZE/40; j++) //填充滑动窗口
            {
                pre_msg = del_msg();
                decode_process(pre_msg,j);
                //real_pro++;
                pro_cnt++;
            }
            //real_pro += WINSIZE / 40;
            //pro_cnt += WINSIZE / 40;
            un_pro++;
            for (int i = 0; i < CH_NUM; i++)
            {
                for (int j = 0; j < MAX_POINT; j++)
                {
                    if (ch_point_signal[i, j].Count > MAX_CH_POINT_LEN)
                    { 
                       /* lock(sync_ch_point_signal[i,j])
                        {
                            ch_point_signal[i,j].Dequeue ();
                        }*/
                        while (ch_point_signal[i, j].Count > MAX_CH_POINT_LEN) //4个通道，每个通道64个点，每个点滑动窗口400个数据，每个点缓存为10
                        //个窗口，当缓存满了，等待process线程处理数据，直至缓存有余量
                        {
                            Thread.Sleep(5);
                        }
                        //continue;
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
            //Thread.Sleep(100);
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
            double [] temp = new double[WINSIZE];
            PointPairList [] alist = new PointPairList[MAX_POINT];
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
                        if (ch_point_denoise[channel - 1, i].Count > 0)
                        {
                            temp = ch_point_denoise[channel - 1, i].ElementAt(ch_point_denoise[channel - 1, i].Count - 1);
                            //temp = ch_point_denoise[channel - 1, i].Dequeue();
                            //ch_point_denoise[channel - 1, i].Dequeue();
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

        /*
         * 解析帧函数，将msg解析，保存至各通道中各点的缓存区
         * 输入：
         *    Message msg ： 待解析包
         *    int j : 第j帧数据
         * 输出：
         *      无
         */
        private void decode_process(Message msg, int j)
        {
            for (int k = 0; k < MAX_POINT; k++)
            {
                for (int i = 0; i < 40; i++)
                {
                    temp_ch1_point_signal[k, j * 40 + i] = msg.CH1[i * 64 + k];
                    temp_ch2_point_signal[k, j * 40 + i] = msg.CH2[i * 64 + k];
                    temp_ch3_point_signal[k, j * 40 + i] = msg.CH3[i * 64 + k];
                    temp_ch4_point_signal[k, j * 40 + i] = msg.CH4[i * 64 + k];
                }
            }
        }

        public void change_wave(string name, int lev)
        { 
            lock(sync_wavename)
            {
                wavename = name;
            }
            lock (sync_level)
            {
                level = lev;
            }

        }
    }
}

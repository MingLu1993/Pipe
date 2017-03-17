using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.IO;
using System.Timers;
using ZedGraph;

using System.Net;
using System.Net.Sockets;

using System.Runtime.InteropServices;
using FBGEMSystem.OnlineAnalysis;


namespace FBGEMSystem
{
    //public partial class Form1 : Form
    public partial class Form1 : Form
    {

        #region 初始化全局变量
        //public static user global = new user();
        public user global = new user();
     
        System.Timers.Timer atimer = new System.Timers.Timer();
        //process.global;
        FontSpec myFont = new FontSpec("仿宋体", 20, Color.Black, true, false, false);
        public int flag = 0;
        public List<double> time_value_list_1 = new List<double>();
        public List<double> time_value_list1 = new List<double>();
        public List<double> time_value_list_2 = new List<double>();
        public List<double> time_value_list2 = new List<double>();
        public List<double> time_value_list_3 = new List<double>();
        public List<double> time_value_list3 = new List<double>();
        public List<double> time_value_list_4 = new List<double>();
        public List<double> time_value_list4 = new List<double>();
        public string content = "图形分别显示小波降噪信号以及降噪信号的分段峭度值图";
        public string content1 = "图形分别显示各点的频谱图";
        public string content2 = "图形分别显示单点的小波分解时域图和节点能量，峭度图";
        public double[] time_value1 = new double[10];
        const int maxpoint = 12;
        const int maxchannel = 4;
        public int currentChannel = 0;
        public bool isSelectmy = false;
        const int winsize = 2000;
        public bool isRuning = false;
        public int channel_show = 0;
        public int point = 0; //用户界面选择通道数和采集点时，改变这两个变量
        private static object sync_select_index = new object(); //同步操作的对象
        private static object sync_select_index1 = new object(); //同步操作的对象
        //FFT绘图
        PointPairList[] pplist_fft4 = new PointPairList[maxpoint];   //PointPairList是ZedGraph插件提供的类
        PointPairList[] pplist_fft1 = new PointPairList[maxpoint];
        PointPairList[] pplist_fft2 = new PointPairList[maxpoint];
        PointPairList[] pplist_fft3 = new PointPairList[maxpoint];
        PointPairList[] pplist_denoise1 = new PointPairList[maxpoint];
        PointPairList[] pplist_denoise2 = new PointPairList[maxpoint];
        PointPairList[] pplist_denoise3 = new PointPairList[maxpoint];
        PointPairList[] pplist_denoise4 = new PointPairList[maxpoint];
        //  PointPairList[] pplist_original = new PointPairList[maxpoint];

        PointPairList[] pplist_energy = new PointPairList[maxpoint];
        PointPairList[] pplist_kurtosis = new PointPairList[maxpoint];
        PointPairList[] pplist_time = new PointPairList[maxpoint];
        PointPairList list_energy = new PointPairList();
        PointPairList list_kurtosis = new PointPairList();
        PointPairList list_time1 = new PointPairList();
        PointPairList list_time2 = new PointPairList();
        PointPairList list_time3 = new PointPairList();
        PointPairList list_time4 = new PointPairList();
        PointPairList list_app = new PointPairList();
        PointPairList list_det = new PointPairList();
        bool com1 = false;
        bool com2 = false;
        bool com3 = false;
        bool com4 = false;
        bool com5 = false;
        bool com6 = false;
        bool com7 = false;
        bool com8 = false;
        int detail = 0;
        int com1_value = 0;
        int com2_value = 0;
        int com3_value = 0;
        int com4_value = 0;
        int com5_value = 0;
        int com6_value = 0;
        int com7_value = 0;
        int com8_value = 0;
        int ch = 0;
        private static object sync_detail = new object();
        private static object sync_com1 = new object();
        private static object sync_com2 = new object();
        private static object sync_com3 = new object();
        private static object sync_com4 = new object();
        private static object sync_com5 = new object();
        private static object sync_com6 = new object();
        private static object sync_com7 = new object();
        private static object sync_com8 = new object();
        private static object sync_com9 = new object();

        #endregion
        public Form1()
        {
            InitializeComponent();

            Initial();
            global.currentChannel = currentChannel;
            //global.all_msg;
            atimer.Elapsed += new ElapsedEventHandler(showcnt_handler);
            atimer.Interval = 50;
            atimer.Start();
            zedgrapStateControl.IsControl1 = true;
            zedgrapStateControl.IsControl2 = true;
            zedgrapStateControl.IsControl3 = true;
            zedgrapStateControl.IsControl4 = true;
            zedgrapStateControl.IsControl5 = true;
            zedgrapStateControl.IsControl6 = true;
            zedgrapStateControl.IsControl7 = true;
        }

        private void Initial()
        {
            //Thread test = new Thread(test_thread);
            //test.IsBackground = true;
            //test.Start();

          
            for (int i = 0; i < maxpoint; i++)
            {
                //         pplist_original[i] = new PointPairList();
                pplist_denoise1[i] = new PointPairList();
                pplist_denoise2[i] = new PointPairList();
                pplist_denoise3[i] = new PointPairList();
                pplist_denoise4[i] = new PointPairList();
                pplist_time[i] = new PointPairList();
                pplist_energy[i] = new PointPairList();
                pplist_fft4[i] = new PointPairList();
                pplist_fft1[i] = new PointPairList();
                pplist_fft2[i] = new PointPairList();
                pplist_fft3[i] = new PointPairList();
                pplist_kurtosis[i] = new PointPairList();
            }
            /*
            Thread decode = new Thread(decode_thread);
            decode.IsBackground = true;
            decode.Start();

            Thread denoise = new Thread(denoise_thread);
            denoise.IsBackground = true;
            denoise.Start();

            Thread process = new Thread(process_thread);
            process.IsBackground = true;
            process.Start();

            Thread extract = new Thread(extract_thread);
            extract.IsBackground = true;
            extract.Start();
            */
            this.zedGraphControl2.GraphPane.XAxis.Title.Text = "序号";
            this.zedGraphControl2.GraphPane.YAxis.Title.Text = "小波能量";
            this.zedGraphControl2.GraphPane.Title.FontSpec = myFont;
            this.zedGraphControl2.GraphPane.XAxis.Title.FontSpec = myFont;
            this.zedGraphControl2.GraphPane.YAxis.Title.FontSpec = myFont;
            BarItem mybar = this.zedGraphControl2.GraphPane.AddBar("能量", list_energy, Color.Blue);
            this.zedGraphControl6.GraphPane.XAxis.Title.Text = "序号";
            this.zedGraphControl6.GraphPane.YAxis.Title.Text = "小波峭度";
            this.zedGraphControl6.GraphPane.Title.FontSpec = myFont;
            this.zedGraphControl6.GraphPane.XAxis.Title.FontSpec = myFont;
            this.zedGraphControl6.GraphPane.YAxis.Title.FontSpec = myFont;
            BarItem mybar1 = this.zedGraphControl6.GraphPane.AddBar("峭度", list_kurtosis, Color.Orange);

            this.zedGraphControl7.GraphPane.XAxis.Title.Text = "序号";
            this.zedGraphControl7.GraphPane.YAxis.Title.Text = "峭度";
            this.zedGraphControl7.GraphPane.Title.FontSpec = myFont;
            this.zedGraphControl7.GraphPane.XAxis.Title.FontSpec = myFont;
            this.zedGraphControl7.GraphPane.YAxis.Title.FontSpec = myFont;
            BarItem mybar2 = this.zedGraphControl7.GraphPane.AddBar("峭度", list_time1, Color.Red);

            this.zedGraphControl11.GraphPane.XAxis.Title.Text = "序号";
            this.zedGraphControl11.GraphPane.YAxis.Title.Text = "峭度";
            this.zedGraphControl11.GraphPane.Title.FontSpec = myFont;
            this.zedGraphControl11.GraphPane.XAxis.Title.FontSpec = myFont;
            this.zedGraphControl11.GraphPane.YAxis.Title.FontSpec = myFont;
            BarItem mybar3 = this.zedGraphControl11.GraphPane.AddBar("峭度", list_time2, Color.Blue);

            this.zedGraphControl12.GraphPane.XAxis.Title.Text = "序号";
            this.zedGraphControl12.GraphPane.YAxis.Title.Text = "峭度";
            this.zedGraphControl12.GraphPane.Title.FontSpec = myFont;
            this.zedGraphControl12.GraphPane.XAxis.Title.FontSpec = myFont;
            this.zedGraphControl12.GraphPane.YAxis.Title.FontSpec = myFont;
            BarItem mybar4 = this.zedGraphControl12.GraphPane.AddBar("峭度", list_time3, Color.Fuchsia);

            this.zedGraphControl13.GraphPane.XAxis.Title.Text = "序号";
            this.zedGraphControl13.GraphPane.YAxis.Title.Text = "峭度";
            this.zedGraphControl13.GraphPane.Title.FontSpec = myFont;
            this.zedGraphControl13.GraphPane.XAxis.Title.FontSpec = myFont;
            this.zedGraphControl13.GraphPane.YAxis.Title.FontSpec = myFont;
            BarItem mybar5 = this.zedGraphControl13.GraphPane.AddBar("峭度", list_time4, Color.Purple);
        }
        
        //解包线程
        private void decode_thread()
        {
            int ii = 0;
            while (true&&isSelectmy)
            {
                //while (global.all_msg.Count > winsize / 40)
                while ((Receiver.process_all_msg_FBG.BufferSize > winsize / Data.FBG_numPackage) && (channel_show > 0))
                {
                    global.decode_fun(); //解析包，并填充一个滑动窗口，入队
                    System.Console.WriteLine();
                    System.Console.WriteLine("decode"+ii++);
                    System.Console.WriteLine();
                }
                //Thread.Sleep(5);
            }
        }

        //去噪线程  画时域图
        private void denoise_thread()
        {
            double[] result = new double[0];
            bool isprocess = false;
            if (channel_show > 0)
            {
                ch = channel_show - 1;
            }
            while (true && isSelectmy)
            {
                for (int j = 0; j < maxpoint; j++) //各通道内采集点数
                {
                    if (global.ch_point_signal[currentChannel, j].Count > 1)
                    {
                        global.denoise(currentChannel, j, ref result);
                        isprocess = true;
                    }
                    else
                    {
                        isprocess = false;
                    }
                }

                //处理完所有采集点的一个窗口数据后，画图
                if (isprocess)
                {
                    #region 绘制时域降噪信号
                    //选取的通道-1
                    if (com1_value != 0)
                    {
                        // com1 = true;
                        int temp1 = com1_value;
                        if (global.ch_point_denoise[ch, temp1 - 1].Count > 0)
                        {
                            pplist_denoise1 = global.Get_PointPair((ch + 1), "denoise");
                            draw_denoised1(pplist_denoise1, (ch + 1), temp1);
                        }
                    }
                    if (com2_value != 0)
                    {
                        // com2 = true;
                        int temp2 = com2_value;
                        if (global.ch_point_denoise[ch, temp2 - 1].Count > 0)
                        {
                            pplist_denoise2 = global.Get_PointPair((ch + 1), "denoise");
                            draw_denoised2(pplist_denoise2, (ch + 1), temp2);
                        }
                    }
                    if (com3_value != 0)
                    {
                        // com3 = true;
                        int temp3 = com3_value;
                        if (global.ch_point_denoise[ch, temp3 - 1].Count > 0)
                        {
                            pplist_denoise3 = global.Get_PointPair((ch + 1), "denoise");
                            draw_denoised3(pplist_denoise3, (ch + 1), temp3);
                        }
                    }
                    if (com4_value != 0)
                    {
                        //  com4 = true;
                        int temp4 = com4_value;
                        if (global.ch_point_denoise[ch, temp4 - 1].Count > 0)
                        {
                            pplist_denoise4 = global.Get_PointPair((ch + 1), "denoise");
                            draw_denoised4(pplist_denoise4, (ch + 1), temp4);
                        }
                    }
                    #endregion

                   // Thread.Sleep(5);
                }
            }
        }
        private void process_thread()
        {
            double[] result = new double[0];
            double[] result1 = new double[0];
            double[] result2 = new double[0];
            // int chan=0;
            //int count = 0;
            while (true && isSelectmy)
            {

                for (int j = 0; j < maxpoint; j++)
                {
                    if (global.ch_point_denoise[currentChannel, j].Count > 1)
                    {
                        global.process(currentChannel, j, ref result); //降噪后的信号放入result中
                        result1 = result;
                        result2 = result;
                        //这里通道四的所有点进行处理

                        global.wavelet_process(result, global.wavename, global.level, currentChannel, j); //这里的result不传递值，直接放在前面

                        //此处添加fft处理程序

                        global.FFT_Transform(currentChannel, j, ref result1);

                        //绘制时域参量峭度
                        // zedgrapStateControl.IsControl2 = true;
                        global.CalKurtosis(currentChannel, j, ref result2);
                        // }
                    }
                }

                //Thread.Sleep(5);
            }
        }
        private void extract_thread()
        {
            double[] energy = new double[6];
            double[] energy1 = new double[6];
            double[] ca = new double[0];
            double[] cd1 = new double[0];
            double[] cd2 = new double[0];
            double[] cd3 = new double[0];
            double[] cd4 = new double[0];
            double[] cd5 = new double[0];

            double[] ea = new double[0];
            double[] ed1 = new double[0];
            double[] ed2 = new double[0];
            double[] ed3 = new double[0];
            double[] ed4 = new double[0];
            double[] ed5 = new double[0];
            while (true && isSelectmy)
            {
                #region 时域峭度程序
                //                 峭度绘图 
                double[] time_value1 = new double[10];
                double[] time_value2 = new double[10];
                double[] time_value3 = new double[10];
                double[] time_value4 = new double[10];
                for (int i = 0; i < 10; i++)
                {
                    time_value1[i] = 0;
                    time_value2[i] = 0;
                    time_value3[i] = 0;
                    time_value4[i] = 0;
                }
                //一点
                //if (zedgrapStateControl.IsControl2 == true)
                //{
                if (channel_show > 0)
                    ch = channel_show - 1;//这里是选取的通道数-1
                if (com1_value != 0)
                {
                    int temp1 = com1_value;
                    if (global.ch_point_kur_signal[ch, temp1 - 1].Count > 1)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (global.ch_point_kur_signal[ch, temp1 - 1].Count > 1)
                            {
                                time_value1[i] = global.get_kurtosis((ch + 1), temp1 - 1);
                                time_value_list_1.Add(time_value1[i]);
                            }
                        }
                        double[] time_value_array1 = new double[time_value_list_1.Count];
                        time_value_array1 = time_value_list_1.ToArray();
                        showcnt(value1, time_value_array1[time_value_list_1.Count - 1].ToString("f2"));
                        if (time_value_list1.Count > 0)
                            time_value_list1.Clear();
                        if (time_value_list_1.Count > 10)
                        {
                            for (int i = time_value_list_1.Count - 10; i < (time_value_list_1.Count); i++)
                            {
                                time_value_list1.Add(time_value_array1[i]);
                            }
                            draw_time1(time_value_list1.ToArray(), temp1, (ch + 1));
                        }
                        else
                        {
                            draw_time1(time_value_list_1.ToArray(), temp1, (ch + 1));
                        }
                    }
                }
                //二点
                if (com2_value != 0)
                {
                    int temp2 = com2_value;
                    if (global.ch_point_kur_signal[ch, temp2 - 1].Count > 1)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (global.ch_point_kur_signal[ch, temp2 - 1].Count > 1)
                            {
                                time_value2[i] = global.get_kurtosis((ch + 1), temp2 - 1);
                                time_value_list_2.Add(time_value2[i]);
                            }
                        }
                        double[] time_value_array2 = new double[time_value_list_2.Count];
                        time_value_array2 = time_value_list_2.ToArray();
                        showcnt(value2, time_value_array2[time_value_list_2.Count - 1].ToString("f2"));
                        if (time_value_list2.Count > 0)
                            time_value_list2.Clear();
                        if (time_value_list_2.Count > 10)
                        {
                            for (int i = time_value_list_2.Count - 10; i < (time_value_list_2.Count); i++)
                            {
                                time_value_list2.Add(time_value_array2[i]);
                            }
                            draw_time2(time_value_list2.ToArray(), temp2, (ch + 1));
                        }
                        else
                        {
                            draw_time2(time_value_list_2.ToArray(), temp2, (ch + 1));
                        }
                    }
                }
                //三点
                if (com3_value != 0)
                {
                    int temp3 = com3_value;
                    if (global.ch_point_kur_signal[ch, temp3 - 1].Count > 1)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (global.ch_point_kur_signal[ch, temp3 - 1].Count > 1)
                            {
                                time_value3[i] = global.get_kurtosis((ch + 1), temp3 - 1);
                                time_value_list_3.Add(time_value3[i]);
                            }
                        }
                        double[] time_value_array3 = new double[time_value_list_3.Count];
                        time_value_array3 = time_value_list_3.ToArray();
                        showcnt(value3, time_value_array3[time_value_list_3.Count - 1].ToString("f2"));
                        if (time_value_list3.Count > 0)
                            time_value_list3.Clear();
                        if (time_value_list_3.Count > 10)
                        {
                            for (int i = time_value_list_3.Count - 10; i < (time_value_list_3.Count); i++)
                            {
                                time_value_list3.Add(time_value_array3[i]);
                            }
                            draw_time3(time_value_list3.ToArray(), temp3, (ch + 1));
                        }
                        else
                        {
                            draw_time3(time_value_list_3.ToArray(), temp3, (ch + 1));
                        }
                    }
                }

                //四点
                if (com4_value != 0)
                {
                    int temp4 = com4_value;
                    if (global.ch_point_kur_signal[ch, temp4 - 1].Count > 1)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (global.ch_point_kur_signal[ch, temp4 - 1].Count > 1)
                            {
                                time_value4[i] = global.get_kurtosis((ch + 1), temp4 - 1);
                                time_value_list_4.Add(time_value4[i]);
                            }
                        }
                        double[] time_value_array4 = new double[time_value_list_4.Count];
                        time_value_array4 = time_value_list_4.ToArray();
                        showcnt(value4, time_value_array4[time_value_list_4.Count - 1].ToString("f2"));
                        if (time_value_list4.Count > 0)
                            time_value_list4.Clear();
                        if (time_value_list_4.Count > 10)
                        {
                            for (int i = time_value_list_4.Count - 10; i < (time_value_list_4.Count); i++)
                            {
                                time_value_list4.Add(time_value_array4[i]);
                            }
                            draw_time4(time_value_list4.ToArray(), temp4, (ch + 1));
                        }
                        else
                        {
                            draw_time4(time_value_list_4.ToArray(), temp4, (ch + 1));
                        }
                    }
                }

                //}
                #endregion
                #region  FFT频谱程序
                //FFT多点显示
                //1点
                // checkBox1.Enabled = true;
                if (channel_show > 0)
                {
                    ch = channel_show - 1;
                }
                if (com5_value != 0)
                {
                    com5 = true;
                    int temp5 = com5_value;
                    if (global.ch_point_fft_signal[ch, temp5 - 1].Count > 1)
                    {
                        pplist_fft1 = global.Get_PointPair_FFT((ch + 1), temp5, com5);
                        draw_fft_1(pplist_fft1, temp5, (ch + 1));
                        showcnt(max_amp1, global.fft_max_value1.ToString("f2"));
                        showcnt(relate_freq1, global.fft_fre_value1.ToString());
                        showcnt(energy_1, global.fft_energy1.ToString("f2"));
                        showcnt(cen_freq1, global.center_fre1.ToString());
                    }
                }
                //2
                if (com6_value != 0)
                {
                    com6 = true;
                    int temp6 = com6_value;
                    if (global.ch_point_fft_signal[ch, temp6 - 1].Count > 1)
                    {
                        pplist_fft2 = global.Get_PointPair_FFT((ch + 1), temp6, com6);
                        draw_fft_2(pplist_fft2, temp6, (ch + 1));
                        showcnt(max_amp2, global.fft_max_value2.ToString("f2"));
                        showcnt(relate_freq2, global.fft_fre_value2.ToString());
                        showcnt(energy_2, global.fft_energy2.ToString("f2"));
                        showcnt(cen_freq2, global.center_fre2.ToString());
                    }
                }
                //3点
                if (com7_value != 0)
                {
                    com7 = true;
                    int temp7 = com7_value;
                    if (global.ch_point_fft_signal[ch, temp7 - 1].Count > 1)
                    {
                        pplist_fft3 = global.Get_PointPair_FFT((ch + 1), temp7, com7);
                        draw_fft_3(pplist_fft3, temp7, (ch + 1));
                        showcnt(max_amp3, global.fft_max_value3.ToString("f2"));
                        showcnt(relate_freq3, global.fft_fre_value3.ToString());
                        showcnt(energy_3, global.fft_energy3.ToString("f2"));
                        showcnt(cen_freq3, global.center_fre3.ToString());
                    }
                }
                //4点
                if (com8_value != 0)
                {
                    com8 = true;
                    int temp8 = com8_value;
                    if (global.ch_point_fft_signal[ch, temp8 - 1].Count > 1)
                    {
                        pplist_fft4 = global.Get_PointPair_FFT((ch + 1), temp8, com8);
                        draw_fft_4(pplist_fft4, temp8, (ch + 1));
                        showcnt(max_amp4, global.fft_max_value4.ToString("f2"));
                        showcnt(relate_freq4, global.fft_fre_value4.ToString());
                        showcnt(energy_4, global.fft_energy4.ToString("f2"));
                        showcnt(cen_freq4, global.center_fre4.ToString());
                    }
                }
                #endregion
                #region 小波分解特征参量
                if (channel_show > 0)
                    ch = channel_show - 1;
                for (int k = 0; k < maxpoint; k++)
                {
                    if (global.pro_result[ch, k].Count > 1)
                    {
                        energy = global.extract(ch, k, ref ca, ref cd1, ref cd2, ref cd3, ref cd4, ref cd5, "energy");
                        energy1 = global.extract(ch, k, ref ea, ref ed1, ref ed2, ref ed3, ref ed4, ref ed5, "kurtosis");
                        /*
                         * 这里添加神经网络判定程序
                         */
                        if ((k + 1) == point)
                        {
                            draw_bar(energy, (ch + 1), k, global.level, "energy");
                            draw_bar(energy1, (ch + 1), k, global.level, "kurtosis");
                            draw_detail(ca, "app");
                            if (detail != 0)
                            {
                                switch (detail)
                                {
                                    case 1: draw_detail(cd1, "det"); break;
                                    case 2: draw_detail(cd2, "det"); break;
                                    case 3: draw_detail(cd3, "det"); break;
                                    case 4: draw_detail(cd4, "det"); break;
                                    case 5: draw_detail(cd5, "det"); break;
                                }
                            }

                        }

                    }
                }
                #endregion

                //Thread.Sleep(5);
            }
        }
        private void draw_detail(double[] signal, string type)
        {
            if (zedgrapStateControl.IsControl6 == true)
            {
                Color[] color = { Color.Red, Color.Chartreuse, Color.MediumBlue, Color.Aqua, Color.DarkGreen, Color.Indigo, Color.Black };

                switch (type)
                {
                    case "app":
                        list_app.Clear();
                        for (int i = 0; i < signal.Count(); i++)
                        {
                            list_app.Add(i, signal[i]);
                        }
                        this.zedGraphControl3.GraphPane.Title.Text = "近似系数";
                        this.zedGraphControl3.GraphPane.XAxis.Title.Text = "时间";
                        this.zedGraphControl3.GraphPane.YAxis.Title.Text = "幅度";
                        this.zedGraphControl3.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                        this.zedGraphControl3.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                        this.zedGraphControl3.GraphPane.Title.FontSpec = myFont;
                        this.zedGraphControl3.GraphPane.XAxis.Title.FontSpec = myFont;
                        this.zedGraphControl3.GraphPane.YAxis.Title.FontSpec = myFont;
                        this.zedGraphControl3.GraphPane.XAxis.Type = ZedGraph.AxisType.Ordinal;
                        this.zedGraphControl3.GraphPane.CurveList.Clear();
                        zedGraphControl3.GraphPane.AddCurve("第" + point + "点", list_app, color[(point - 1) % 7], SymbolType.None);
                        zedGraphControl3.AxisChange();
                        zedGraphControl3.Invalidate();
                        break;

                    case "det":
                        list_det.Clear();
                        for (int i = 0; i < signal.Count(); i++)
                        {
                            list_det.Add(i, signal[i]);
                        }
                        this.zedGraphControl4.GraphPane.Title.Text = "cd" + detail.ToString() + "细节系数";
                        this.zedGraphControl4.GraphPane.XAxis.Title.Text = "时间";
                        this.zedGraphControl4.GraphPane.YAxis.Title.Text = "幅度";
                        this.zedGraphControl4.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                        this.zedGraphControl4.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                        this.zedGraphControl4.GraphPane.Title.FontSpec = myFont;
                        this.zedGraphControl4.GraphPane.XAxis.Title.FontSpec = myFont;
                        this.zedGraphControl4.GraphPane.YAxis.Title.FontSpec = myFont;
                        this.zedGraphControl4.GraphPane.XAxis.Type = ZedGraph.AxisType.Ordinal;
                        this.zedGraphControl4.GraphPane.CurveList.Clear();
                        zedGraphControl4.GraphPane.AddCurve("第" + point + "点", list_det, color[(point - 1) % 7], SymbolType.None);
                        zedGraphControl4.AxisChange();
                        zedGraphControl4.Invalidate();
                        break;
                }
            }
        }
        private void draw_bar(double[] signal, int channel, int point, int level, string type)
        {
            if (zedgrapStateControl.IsControl4 == true)
            {
                Color[] color = { Color.Red, Color.Chartreuse, Color.MediumBlue, Color.Aqua, Color.DarkGreen, Color.Indigo, Color.Black };
                if (type == "energy")
                {
                    list_energy.Clear();
                    this.zedGraphControl2.GraphPane.Title.Text = "第" + channel + "通道第" + (point + 1).ToString() + "点";
                    for (int i = 0; i <= level; i++)
                    {
                        list_energy.Add(i, signal[i]);
                    }
                    this.zedGraphControl2.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl2.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl2.GraphPane.Chart.Fill = new Fill(Color.White,
                         Color.FromArgb(255, 255, 166), 45.0F);
                    this.zedGraphControl2.AxisChange();
                    this.zedGraphControl2.Invalidate();
                }
                else if (type == "kurtosis")
                {
                    list_kurtosis.Clear();
                    this.zedGraphControl6.GraphPane.Title.Text = "第" + channel + "通道第" + (point + 1).ToString() + "点";
                    for (int i = 0; i <= level; i++)
                    {
                        list_kurtosis.Add(i, signal[i]);
                    }
                    this.zedGraphControl6.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl6.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl6.GraphPane.Chart.Fill = new Fill(Color.White,
                         Color.FromArgb(255, 255, 166), 45.0F);
                    this.zedGraphControl6.AxisChange();
                    this.zedGraphControl6.Invalidate();
                }
            }

        }
        # region 绘制降噪峭度图-图形显示
        public void draw_time1(double[] signal, int point, int channel)
        {
            if (zedgrapStateControl.IsControl5 == true)
            {
                if (com1 == true && com1_value != 0)
                {
                    list_time1.Clear();
                    this.zedGraphControl7.GraphPane.Title.Text = "第" + channel + "通道第" + (point).ToString() + "点";
                    for (int i = 0; i < signal.Length; i++)
                    {
                        list_time1.Add(i + 1, signal[i]);
                    }
                    this.zedGraphControl7.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl7.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                    //  this.zedGraphControl7.GraphPane.Title.FontSpec = myFont;
                    //  this.zedGraphControl7.GraphPane.XAxis.Title.FontSpec = myFont;
                    //  this.zedGraphControl7.GraphPane.YAxis.Title.FontSpec = myFont;
                    this.zedGraphControl7.GraphPane.Chart.Fill = new Fill(Color.White,
                         Color.FromArgb(255, 255, 166), 45.0F);
                    this.zedGraphControl7.AxisChange();
                    this.zedGraphControl7.Invalidate();
                }
            }
        }
        public void draw_time2(double[] signal, int point, int channel)
        {
            if (zedgrapStateControl.IsControl5 == true)
            {
                if (com2 == true && com2_value != 0)
                {
                    list_time2.Clear();
                    for (int i = 0; i < signal.Length; i++)
                    {
                        list_time2.Add(i + 1, signal[i]);
                    }
                    this.zedGraphControl11.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl11.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                    //  this.zedGraphControl11.GraphPane.Title.FontSpec = myFont;
                    // this.zedGraphControl11.GraphPane.XAxis.Title.FontSpec = myFont;
                    //  this.zedGraphControl11.GraphPane.YAxis.Title.FontSpec = myFont;
                    this.zedGraphControl11.GraphPane.Title.Text = "第" + channel + "通道第" + (point).ToString() + "点";
                    this.zedGraphControl11.GraphPane.Chart.Fill = new Fill(Color.White,
                     Color.FromArgb(255, 255, 166), 45.0F);
                    this.zedGraphControl11.AxisChange();
                    this.zedGraphControl11.Invalidate();
                }
            }
        }
        public void draw_time3(double[] signal, int point, int channel)
        {
            if (zedgrapStateControl.IsControl5 == true)
            {
                if (com3 == true && com3_value != 0)
                {
                    list_time3.Clear();
                    for (int i = 0; i < signal.Length; i++)
                    {
                        list_time3.Add(i + 1, signal[i]);
                    }
                    this.zedGraphControl12.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl12.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                    //   this.zedGraphControl12.GraphPane.Title.FontSpec = myFont;
                    //this.zedGraphControl12.GraphPane.XAxis.Title.FontSpec = myFont;
                    //  this.zedGraphControl12.GraphPane.YAxis.Title.FontSpec = myFont;
                    this.zedGraphControl12.GraphPane.Title.Text = "第" + channel + "通道第" + (point).ToString() + "点";
                    this.zedGraphControl12.GraphPane.Chart.Fill = new Fill(Color.White,
                     Color.FromArgb(255, 255, 166), 45.0F);
                    this.zedGraphControl12.AxisChange();
                    this.zedGraphControl12.Invalidate();
                }
            }
        }
        public void draw_time4(double[] signal, int point, int channel)
        {
            if (zedgrapStateControl.IsControl5 == true)
            {
                if (com4 == true && com4_value != 0)
                {
                    list_time4.Clear();
                    for (int i = 0; i < signal.Length; i++)
                    {
                        list_time4.Add(i + 1, signal[i]);
                    }
                    this.zedGraphControl13.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl13.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                    //   this.zedGraphControl13.GraphPane.Title.FontSpec = myFont;
                    //   this.zedGraphControl13.GraphPane.XAxis.Title.FontSpec = myFont;
                    //  this.zedGraphControl13.GraphPane.YAxis.Title.FontSpec = myFont;
                    this.zedGraphControl13.GraphPane.Title.Text = "第" + channel + "通道第" + (point).ToString() + "点";
                    this.zedGraphControl13.GraphPane.Chart.Fill = new Fill(Color.White,
                     Color.FromArgb(255, 255, 166), 45.0F);
                    this.zedGraphControl13.AxisChange();
                    this.zedGraphControl13.Invalidate();
                }
            }
        }
        #endregion
        /*
 * 函数说明：
 *      画图函数
 *  输入：
 *      PointPairList [] pplist ： 待画图的点，包含一个通道64个采集点的数据，每个 采集点有一个滑动窗口个数据点
 *      int point ： 选择采集点
 *  输出：
 *      无
 */
        # region 绘制降噪波形图-图形显示
        private void draw_denoised1(PointPairList[] pplist, int channel, int point)
        {
            //  Color[] color = { Color.Red, Color.Blue, Color.Aqua, Color.MediumAquamarine, Color.DarkGreen, Color.Indigo, Color.Black };
            if (zedgrapStateControl.IsControl1 == true)
            {
                if (com1_value != 0 && com1 == true)
                {
                    this.zedGraphControl14.GraphPane.Title.Text = "第" + channel + "通道-降噪信号";
                    this.zedGraphControl14.GraphPane.XAxis.Title.Text = "时间";
                    this.zedGraphControl14.GraphPane.YAxis.Title.Text = "幅度";
                    this.zedGraphControl14.GraphPane.Title.FontSpec = myFont;
                    this.zedGraphControl14.GraphPane.XAxis.Title.FontSpec = myFont;
                    this.zedGraphControl14.GraphPane.YAxis.Title.FontSpec = myFont;
                    this.zedGraphControl14.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl14.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl14.GraphPane.XAxis.Type = ZedGraph.AxisType.Ordinal;
                    this.zedGraphControl14.GraphPane.CurveList.Clear();
                    this.zedGraphControl14.GraphPane.AddCurve(Get_Title(point), pplist[(point - 1) % 64], Color.Red, SymbolType.None);
                    zedGraphControl14.AxisChange();
                    zedGraphControl14.Invalidate();
                }
            }
        }
        private void draw_denoised2(PointPairList[] pplist, int channel, int point)
        {
            //  Color[] color = { Color.Red, Color.Blue, Color.Aqua, Color.MediumAquamarine, Color.DarkGreen, Color.Indigo, Color.Black };
            if (zedgrapStateControl.IsControl1 == true)
            {
                if (com2_value != 0 && com2 == true)
                {
                    this.zedGraphControl15.GraphPane.Title.Text = "第" + channel + "通道-降噪信号";
                    this.zedGraphControl15.GraphPane.XAxis.Title.Text = "时间";
                    this.zedGraphControl15.GraphPane.YAxis.Title.Text = "幅度";
                    this.zedGraphControl15.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl15.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl15.GraphPane.Title.FontSpec = myFont;
                    this.zedGraphControl15.GraphPane.XAxis.Title.FontSpec = myFont;
                    this.zedGraphControl15.GraphPane.YAxis.Title.FontSpec = myFont;
                    this.zedGraphControl15.GraphPane.XAxis.Type = ZedGraph.AxisType.Ordinal;
                    this.zedGraphControl15.GraphPane.CurveList.Clear();
                    this.zedGraphControl15.GraphPane.AddCurve(Get_Title(point), pplist[(point - 1) % 64], Color.Blue, SymbolType.None);
                    zedGraphControl15.AxisChange();
                    zedGraphControl15.Invalidate();
                }
            }
        }
        private void draw_denoised3(PointPairList[] pplist, int channel, int point)
        {
            //  Color[] color = { Color.Red, Color.Blue, Color.Aqua, Color.MediumAquamarine, Color.DarkGreen, Color.Indigo, Color.Black };
            if (zedgrapStateControl.IsControl1 == true)
            {
                if (com3_value != 0 && com3 == true)
                {
                    this.zedGraphControl16.GraphPane.Title.Text = "第" + channel + "通道-降噪信号";
                    this.zedGraphControl16.GraphPane.XAxis.Title.Text = "时间";
                    this.zedGraphControl16.GraphPane.YAxis.Title.Text = "幅度";
                    this.zedGraphControl16.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl16.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl16.GraphPane.Title.FontSpec = myFont;
                    this.zedGraphControl16.GraphPane.XAxis.Title.FontSpec = myFont;
                    this.zedGraphControl16.GraphPane.YAxis.Title.FontSpec = myFont;
                    this.zedGraphControl16.GraphPane.XAxis.Type = ZedGraph.AxisType.Ordinal;
                    this.zedGraphControl16.GraphPane.CurveList.Clear();
                    this.zedGraphControl16.GraphPane.AddCurve(Get_Title(point), pplist[(point - 1) % 64], Color.Fuchsia, SymbolType.None);
                    zedGraphControl16.AxisChange();
                    zedGraphControl16.Invalidate();
                }
            }
        }
        private void draw_denoised4(PointPairList[] pplist, int channel, int point)
        {
            //  Color[] color = { Color.Red, Color.Blue, Color.Aqua, Color.MediumAquamarine, Color.DarkGreen, Color.Indigo, Color.Black };
            if (zedgrapStateControl.IsControl1 == true)
            {
                if (com4_value != 0 && com4 == true)
                {
                    this.zedGraphControl17.GraphPane.Title.Text = "第" + channel + "通道-降噪信号";
                    this.zedGraphControl17.GraphPane.XAxis.Title.Text = "时间";
                    this.zedGraphControl17.GraphPane.YAxis.Title.Text = "幅度";
                    this.zedGraphControl17.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl17.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                    this.zedGraphControl17.GraphPane.Title.FontSpec = myFont;
                    this.zedGraphControl17.GraphPane.XAxis.Title.FontSpec = myFont;
                    this.zedGraphControl17.GraphPane.YAxis.Title.FontSpec = myFont;
                    this.zedGraphControl17.GraphPane.XAxis.Type = ZedGraph.AxisType.Ordinal;
                    this.zedGraphControl17.GraphPane.CurveList.Clear();
                    this.zedGraphControl17.GraphPane.AddCurve(Get_Title(point), pplist[(point - 1) % 64], Color.Purple, SymbolType.None);
                    zedGraphControl17.AxisChange();
                    zedGraphControl17.Invalidate();
                }
            }
        }
        # endregion
        # region 绘制FFT各通道谱-图形显示
        private void draw_fft_1(PointPairList[] pplist, int point, int channel)
        {
            if (zedgrapStateControl.IsControl3 == true)
            {
                if (com5_value != 0 && com5 == true)
                {
                    Color[] color = { Color.OliveDrab, Color.LimeGreen, Color.MediumTurquoise, Color.DodgerBlue, Color.DarkGreen, Color.Indigo, Color.Black };
                    PointPairList max_p1 = new PointPairList();
                    PointPairList cen_p1 = new PointPairList();
                    for (int i = 1; i < global.fft_fre_value1; i++)
                    {
                        max_p1.Add(i, 0);
                    }
                    max_p1.Add(global.fft_fre_value1, global.fft_max_value1);

                    for (int i = 1; i < global.center_fre1; i++)
                    {
                        cen_p1.Add(i, 0);
                    }
                    cen_p1.Add(global.center_fre1, global.cen_rela_amp1);

                    if (channel != 0 && point != 0)
                    {
                        this.zedGraphControl1.GraphPane.Title.Text = "第" + channel + "通道-幅度谱";
                        this.zedGraphControl1.GraphPane.XAxis.Title.Text = "频率";
                        this.zedGraphControl1.GraphPane.YAxis.Title.Text = "幅度";
                        this.zedGraphControl1.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                        this.zedGraphControl1.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                        this.zedGraphControl1.GraphPane.Title.FontSpec = myFont;
                        this.zedGraphControl1.GraphPane.XAxis.Title.FontSpec = myFont;
                        this.zedGraphControl1.GraphPane.YAxis.Title.FontSpec = myFont;
                        this.zedGraphControl1.GraphPane.XAxis.Type = ZedGraph.AxisType.Ordinal;
                        this.zedGraphControl1.GraphPane.CurveList.Clear();
                        this.zedGraphControl1.GraphPane.AddCurve(Get_Title(point), pplist[(point - 1) % 64], Color.OliveDrab, SymbolType.None);
                        LineItem curve = this.zedGraphControl1.GraphPane.AddCurve("峰值频率", max_p1, Color.Black, SymbolType.Diamond);
                        curve.Line.Width = 4.0F;
                        curve.Symbol.Size = 15;
                        LineItem curve1 = this.zedGraphControl1.GraphPane.AddCurve("中值频率", cen_p1, Color.Red);
                        curve1.Line.Width = 4.0F;
                        curve1.Symbol.Size = 15;
                        //this.zedGraphControl1.GraphPane.YAxis.Scale.FontSpec.Family = "Cambria";
                        //x线字体
                        //   this.zedGraphControl1.GraphPane.AddCurve(Get_Title(point), max_p1,color[(point - 1) % 7], SymbolType.None);
                        zedGraphControl1.AxisChange();
                        zedGraphControl1.Invalidate();
                    }
                }
            }
        }
        private void draw_fft_2(PointPairList[] pplist, int point, int channel)
        {
            if (zedgrapStateControl.IsControl3 == true)
            {
                if (com6_value != 0 && com6 == true)
                {
                    Color[] color = { Color.OliveDrab, Color.LimeGreen, Color.MediumTurquoise, Color.DodgerBlue, Color.DarkGreen, Color.Indigo, Color.Black };
                    PointPairList max_p1 = new PointPairList();
                    for (int i = 1; i < global.fft_fre_value2; i++)
                    {
                        max_p1.Add(i, 0);
                    }
                    max_p1.Add(global.fft_fre_value2, global.fft_max_value2);
                    PointPairList cen_p1 = new PointPairList();
                    for (int i = 1; i < global.center_fre2; i++)
                    {
                        cen_p1.Add(i, 0);
                    }
                    cen_p1.Add(global.center_fre2, global.cen_rela_amp2);
                    if (channel != 0 && point != 0)
                    {
                        this.zedGraphControl10.GraphPane.Title.Text = "第" + channel + "通道-幅度谱";
                        this.zedGraphControl10.GraphPane.XAxis.Title.Text = "频率";
                        this.zedGraphControl10.GraphPane.YAxis.Title.Text = "幅度";
                        this.zedGraphControl10.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                        this.zedGraphControl10.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                        this.zedGraphControl10.GraphPane.Title.FontSpec = myFont;
                        this.zedGraphControl10.GraphPane.XAxis.Title.FontSpec = myFont;
                        this.zedGraphControl10.GraphPane.YAxis.Title.FontSpec = myFont;
                        this.zedGraphControl10.GraphPane.XAxis.Type = ZedGraph.AxisType.Ordinal;
                        this.zedGraphControl10.GraphPane.CurveList.Clear();
                        this.zedGraphControl10.GraphPane.AddCurve(Get_Title(point), pplist[(point - 1) % 64], Color.LimeGreen, SymbolType.None);
                        LineItem curve = this.zedGraphControl10.GraphPane.AddCurve("峰值频率", max_p1, Color.Black, SymbolType.Diamond);
                        curve.Line.Width = 4.0F;
                        curve.Symbol.Size = 15;
                        LineItem curve1 = this.zedGraphControl10.GraphPane.AddCurve("中值频率", cen_p1, Color.Red);
                        curve1.Line.Width = 4.0F;
                        curve1.Symbol.Size = 15;
                        zedGraphControl10.AxisChange();
                        zedGraphControl10.Invalidate();
                    }
                }
            }
        }
        private void draw_fft_3(PointPairList[] pplist, int point, int channel)
        {
            if (zedgrapStateControl.IsControl3 == true)
            {
                if (com7_value != 0 && com7 == true)
                {
                    Color[] color = { Color.OliveDrab, Color.LimeGreen, Color.MediumTurquoise, Color.DodgerBlue, Color.DarkGreen, Color.Indigo, Color.Black };
                    PointPairList max_p1 = new PointPairList();
                    for (int i = 1; i < global.fft_fre_value3; i++)
                    {
                        max_p1.Add(i, 0);
                    }
                    max_p1.Add(global.fft_fre_value3, global.fft_max_value3);

                    PointPairList cen_p1 = new PointPairList();
                    for (int i = 1; i < global.center_fre3; i++)
                    {
                        cen_p1.Add(i, 0);
                    }
                    cen_p1.Add(global.center_fre3, global.cen_rela_amp3);

                    if (channel != 0 && point != 0)
                    {
                        this.zedGraphControl9.GraphPane.Title.Text = "第" + channel + "通道-幅度谱";
                        this.zedGraphControl9.GraphPane.XAxis.Title.Text = "频率";
                        this.zedGraphControl9.GraphPane.YAxis.Title.Text = "幅度";
                        this.zedGraphControl9.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                        this.zedGraphControl9.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                        this.zedGraphControl9.GraphPane.Title.FontSpec = myFont;
                        this.zedGraphControl9.GraphPane.XAxis.Title.FontSpec = myFont;
                        this.zedGraphControl9.GraphPane.YAxis.Title.FontSpec = myFont;
                        this.zedGraphControl9.GraphPane.XAxis.Type = ZedGraph.AxisType.Ordinal;
                        this.zedGraphControl9.GraphPane.CurveList.Clear();
                        this.zedGraphControl9.GraphPane.AddCurve(Get_Title(point), pplist[(point - 1) % 64], Color.MediumAquamarine, SymbolType.None);
                        LineItem curve = this.zedGraphControl9.GraphPane.AddCurve("峰值频率", max_p1, Color.Black, SymbolType.Diamond);
                        curve.Line.Width = 4.0F;
                        curve.Symbol.Size = 15;
                        LineItem curve1 = this.zedGraphControl9.GraphPane.AddCurve("中值频率", cen_p1, Color.Red);
                        curve1.Line.Width = 4.0F;
                        curve1.Symbol.Size = 15;
                        zedGraphControl9.AxisChange();
                        zedGraphControl9.Invalidate();
                    }
                }
            }
        }
        private void draw_fft_4(PointPairList[] pplist, int point, int channel)
        {
            if (zedgrapStateControl.IsControl3 == true)
            {
                if (com8_value != 0 && com8 == true)
                {
                    Color[] color = { Color.OliveDrab, Color.LimeGreen, Color.MediumTurquoise, Color.DodgerBlue, Color.DarkGreen, Color.Indigo, Color.Black };
                    PointPairList max_p1 = new PointPairList();
                    for (int i = 1; i < global.fft_fre_value4; i++)
                    {
                        max_p1.Add(i, 0);
                    }
                    max_p1.Add(global.fft_fre_value4, global.fft_max_value4);

                    PointPairList cen_p1 = new PointPairList();
                    for (int i = 1; i < global.center_fre4; i++)
                    {
                        cen_p1.Add(i, 0);
                    }
                    cen_p1.Add(global.center_fre4, global.cen_rela_amp4);

                    if (channel != 0 && point != 0)
                    {
                        this.zedGraphControl8.GraphPane.Title.Text = "第" + channel + "通道-幅度谱";
                        this.zedGraphControl8.GraphPane.XAxis.Title.Text = "频率";
                        // this.zedGraphControl8.GraphPane.IsFontsScaled = true;
                        this.zedGraphControl8.GraphPane.YAxis.Title.Text = "幅度";
                        this.zedGraphControl8.GraphPane.XAxis.Scale.FontSpec.Size = 20;
                        this.zedGraphControl8.GraphPane.YAxis.Scale.FontSpec.Size = 20;
                        this.zedGraphControl8.GraphPane.Title.FontSpec = myFont;
                        this.zedGraphControl8.GraphPane.XAxis.Title.FontSpec = myFont;
                        this.zedGraphControl8.GraphPane.YAxis.Title.FontSpec = myFont;
                        this.zedGraphControl8.GraphPane.XAxis.Type = ZedGraph.AxisType.Ordinal;
                        this.zedGraphControl8.GraphPane.CurveList.Clear();
                        this.zedGraphControl8.GraphPane.AddCurve(Get_Title(point), pplist[(point - 1) % 64], Color.DodgerBlue, SymbolType.None);
                        LineItem curve = this.zedGraphControl8.GraphPane.AddCurve("峰值频率", max_p1, Color.Black, SymbolType.Diamond);
                        curve.Line.Width = 4.0F;
                        curve.Symbol.Size = 15;
                        LineItem curve1 = this.zedGraphControl8.GraphPane.AddCurve("中值频率", cen_p1, Color.Red);
                        curve1.Line.Width = 4.0F;
                        curve1.Symbol.Size = 15;
                        zedGraphControl8.AxisChange();
                        zedGraphControl8.Invalidate();
                    }
                }
            }
        }
        # endregion
        private string Get_Title(int i)
        {
            return ((i) % 65).ToString();
        }
        /*
 * 函数说明：
 *      以下几个函数统一完成：从不是创建UI的线程访问UI，以刷新数据。
 */
        delegate void showcountcallback(System.Windows.Forms.Label label, string count);
        public void showcnt(System.Windows.Forms.Label label, string count)
        {
            if (label.InvokeRequired)
            {
                showcountcallback showcntcb = showcnt;
                label.BeginInvoke(showcntcb, new object[] { label, count });
            }
            else
            {
                label.Text = count;
            }
        }

        //delegate void showcomcallback(System.Windows.Forms.ComboBox com);
        //public void showcom(System.Windows.Forms.ComboBox com)
        //{
        //    if (com.InvokeRequired)
        //    {
        //        showcomcallback showone = showcom;
        //        com.BeginInvoke(showone, new object[] { com });
        //    }
        //    else
        //    {

        //    }

        private void showcnt_handler(object sender, ElapsedEventArgs e)
        {
            string temp = Receiver.sharedLocation_FBG.BufferSize.ToString() + "-" + Receiver.sharedLocation1_FBG.BufferSize.ToString() + "-" + Receiver.process_all_msg_FBG.BufferSize.ToString() + "-" + Receiver.index.ToString();
        }

        //界面下拉框等初始设置
        private void Form1_Load(object sender, EventArgs e)
        {
            cbpoint.Items.Clear();
            cbbasicwave.Items.Clear();
            cbbasicwavenum.Items.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            comboBox7.Items.Clear();
            comboBox8.Items.Clear();
            comboBox9.Items.Clear();
            for (int i = 1; i < 12; i++)
            {
                comboBox1.Items.Add((object)(i));
                comboBox2.Items.Add((object)i);
                comboBox3.Items.Add((object)(i));
                comboBox4.Items.Add((object)i);
                comboBox5.Items.Add((object)(i));
                comboBox6.Items.Add((object)i);
                comboBox7.Items.Add((object)(i));
                comboBox8.Items.Add((object)i);
                cbpoint.Items.Add((object)i);
            }
            for (int i = 1; i <= maxchannel; i++)
            {
                comboBox9.Items.Add((object)(i));
            }
            cbbasicwave.Items.Add((object)"db");
            cbbasicwave.Items.Add((object)"coif");
            cbbasicwave.Items.Add((object)"sym");

            for (int i = 1; i < 6; i++)
                cblevel.Items.Add((object)i);

            lblwave.Text = global.wavename;
            lbllevel.Text = global.level.ToString();
        }
        private void cbbasicwave_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cbbasicwavenum.Items.Clear();
            string basicwave = cbbasicwave.SelectedItem.ToString();
            switch (basicwave)
            {
                case "db":
                    for (int i = 1; i < 11; i++)
                        cbbasicwavenum.Items.Add((object)i);
                    break;
                case "coif":
                    for (int i = 1; i < 6; i++)
                        cbbasicwavenum.Items.Add((object)i);
                    break;
                case "sym":
                    for (int i = 1; i < 8; i++)
                        cbbasicwavenum.Items.Add((object)i);
                    break;
            }
            btnconfirm.Enabled = true;
        }
        private void btnconfirm_Click_1(object sender, EventArgs e)
        {
            btnconfirm.Enabled = false;
            if (cbbasicwave.SelectedItem == null || cbbasicwavenum.SelectedItem == null)
            {
                MessageBox.Show("请选择小波基！");
            }
            else if (cblevel.SelectedItem == null)
            {
                MessageBox.Show("请选择分解层数！");
            }
            else
            {
                string wave = cbbasicwave.SelectedItem.ToString();
                wave = wave + cbbasicwavenum.SelectedItem.ToString();
                int level = Convert.ToInt32(cblevel.SelectedItem.ToString());
                global.change_wave(wave, level);
                lblwave.Text = global.wavename;
                lbllevel.Text = global.level.ToString();

                cbdetail.Items.Clear();
                for (int i = 1; i <= global.level; i++)
                {
                    cbdetail.Items.Add((object)i);
                }
            }
        }
        private void cblevel_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            btnconfirm.Enabled = true;
        }
        private void cbdetail_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            lock (sync_detail)
            {
                detail = Convert.ToInt32(cbdetail.SelectedItem.ToString());
            }
        }
        private void cbpoint_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            lock (sync_select_index)
            {
                point = Convert.ToInt32(cbpoint.SelectedItem.ToString());
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            zedgrapStateControl.IsControl1 = false;
            zedgrapStateControl.IsControl2 = false;
            zedgrapStateControl.IsControl3 = false;
            zedgrapStateControl.IsControl4 = false;
            zedgrapStateControl.IsControl5 = false;
            zedgrapStateControl.IsControl6 = false;
            atimer.Close();
            //this.Close();
        }
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            Font f = new System.Drawing.Font("黑体", 10, FontStyle.Bold);
            StringFormat sf = new StringFormat();
            sf.FormatFlags = StringFormatFlags.NoClip;

            e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, new PointF(0, 0), sf);
        }
        private void tabPage2_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip1.SetToolTip(tabPage2, content1);
        }
        private void tabPage4_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip1.SetToolTip(tabPage4, content);
        }
        private void tabPage3_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip1.SetToolTip(tabPage3, content2);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                lock (sync_com1)
                {
                    com1_value = Convert.ToInt32(comboBox1.SelectedItem.ToString());
                    com1 = true;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                lock (sync_com2)
                {
                    com2_value = Convert.ToInt32(comboBox2.SelectedItem.ToString());
                    com2 = true;
                }

            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                lock (sync_com3)
                {
                    com3_value = Convert.ToInt32(comboBox3.SelectedItem.ToString());
                    com3 = true;
                }
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem != null)
            {
                lock (sync_com4)
                {
                    com4_value = Convert.ToInt32(comboBox4.SelectedItem.ToString());
                    com4 = true;
                }
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedItem != null)
            {
                lock (sync_com5)
                {
                    com5_value = Convert.ToInt32(comboBox5.SelectedItem.ToString());
                }
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.SelectedItem != null)
            {
                lock (sync_com6)
                {
                    com6_value = Convert.ToInt32(comboBox6.SelectedItem.ToString());
                }
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox7.SelectedItem != null)
            {
                lock (sync_com7)
                {
                    com7_value = Convert.ToInt32(comboBox7.SelectedItem.ToString());
                }
            }
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox8.SelectedItem != null)
            {
                lock (sync_com8)
                {
                    com8_value = Convert.ToInt32(comboBox8.SelectedItem.ToString());
                }
            }
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)  //通道选择下拉框
        {
            if (comboBox9.SelectedItem != null)
            {
                lock (sync_com9)
                {
                    channel_show = Convert.ToInt32(comboBox9.SelectedItem.ToString());
                    currentChannel = Convert.ToInt32(comboBox9.SelectedItem.ToString())-1;
                    isSelectmy = true;
                    global.currentChannel = channel_show - 1;
                    if(!isRuning)
                    {
                        Thread decode = new Thread(decode_thread);
                        decode.IsBackground = true;
                        decode.Start();

                        Thread denoise = new Thread(denoise_thread);
                        denoise.IsBackground = true;
                        denoise.Start();

                        Thread process = new Thread(process_thread);
                        process.IsBackground = true;
                        process.Start();

                        Thread extract = new Thread(extract_thread);
                        extract.IsBackground = true;
                        extract.Start();
                        isRuning = true;
                    }
                }
            }
        }

        private void zedGraphControl14_Load(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
    }
}

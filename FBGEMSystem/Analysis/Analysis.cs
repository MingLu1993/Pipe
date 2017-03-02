using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZedGraph;

namespace FBGEMSystem
{
    public partial class Analysis : Form
    {

        const int winsize = 2000;    //一次处理的点数
        public AnalysisUser global = new AnalysisUser();

        float XWidth;           //用于控件随窗口变化自动缩放，窗体的宽度
        float YHeight;          //用于控件随窗口变化自动缩放，窗体的高度

        //GraphPane PaneIPCurve;    //用于瞬时相位分析波形图
        //GraphPane PaneIPScatter;  //用于瞬时相位分析散点图

        int channelshow = 0;      //第几通道
        int currentChannel = -1;  //通道的索引
        int pointshow = 0;        //第几测点

        bool isThreadRunning = false;  //用于判断各线程是否已经开启
        bool isCHSelected = false;   //用于判断是否选中通道

        string AnalysisMethod = "";        //选择的tabcontrol控件的Text，既选择的方法
        Thread decode;    //decode线程
        Thread process;    //process线程

        int cmb_waveindex = 0;//小波基选择下拉框索引
        int let_c = 0;         //matlab 小波基选择参数
        int cmb_wave_ad = 0; //小波分析中显示系数

        delegate void SetTextCallback(string a,string b,string c);


        public Analysis()
        {
            InitializeComponent();
            init();
            
        }

        private void init()
        {
            comboBox_CH.Items.Add("1");
            comboBox_CH.Items.Add("2");
            comboBox_CH.Items.Add("3");
            comboBox_CH.Items.Add("4");

            
            for (int i = 1; i < 11; i++)
            {
                comboBox_Point.Items.Add(i.ToString());
            }

            AnalysisMethod = "时频域波形";       //默认时频域波形分析

            comboBox_wavelet.SelectedIndex = 2;   //默认是haar小波基
            cmb_waveindex = 2;
            let_c = 18;

            comboBox_ad.SelectedIndex = 0;  //默认近似系数


            zedGraph_Time.GraphPane.Title.Text = "时域波形";
            zedGraph_Time.GraphPane.XAxis.Title.Text = "点";   //横坐标
            zedGraph_Time.GraphPane.YAxis.Title.Text = "波长";     //纵坐标

            zedGraph_FFT.GraphPane.Title.Text = "频谱";
            zedGraph_FFT.GraphPane.XAxis.Title.Text = "频率（Hz）";   //横坐标
            zedGraph_FFT.GraphPane.YAxis.Title.Text = "幅值";     //纵坐标

            //PaneIPCurve = zedGraph_IPCurve.GraphPane;
            zedGraph_IPCurve.GraphPane.Title.Text = "瞬时相位";        //标题
            zedGraph_IPCurve.GraphPane.XAxis.Title.Text = "Time(s)";   //横坐标
            zedGraph_IPCurve.GraphPane.YAxis.Title.Text = "Phase";     //纵坐标

            //PaneIPScatter = zedGraph_IPScatter.GraphPane;
            zedGraph_IPScatter.GraphPane.Title.Text = "瞬时相位自相关散点图";          //标题
            zedGraph_IPScatter.GraphPane.XAxis.Title.Text = "IP(n)";                   //横坐标
            zedGraph_IPScatter.GraphPane.YAxis.Title.Text = "IP(n+1)";                 //纵坐标

            zedGraph_Hq.GraphPane.Title.Text = "广义Hurst指数H(q)";
            zedGraph_Hq.GraphPane.XAxis.Title.Text = "q";   //横坐标
            zedGraph_Hq.GraphPane.YAxis.Title.Text = "H(q)";     //纵坐标

            zedGraph_tq.GraphPane.Title.Text = "尺度指数τ(q)";
            zedGraph_tq.GraphPane.XAxis.Title.Text = "q";   //横坐标
            zedGraph_tq.GraphPane.YAxis.Title.Text = "τ(q)";     //纵坐标

            zedGraph_f.GraphPane.Title.Text = "多重分形谱";
            zedGraph_f.GraphPane.XAxis.Title.Text = "α";   //横坐标
            zedGraph_f.GraphPane.YAxis.Title.Text = "f(α)";     //纵坐标

            textBox_alpha0.ReadOnly = true;
            textBox_d_alpha.ReadOnly = true;
            textBox_d_f.ReadOnly = true;


        }

        private void Analysis_FormClosed(object sender, FormClosedEventArgs e)
        {
            //使线程跳出while循环
            isThreadRunning = false;
            Data.IsControlFBG = false;
        }

        //tabcontrol控件页面切换，页面Text放入AnalysisMethod，用于判断使用哪种分析方法  
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(tabControl1.SelectedTab.Text);
            AnalysisMethod = tabControl1.SelectedTab.Text;
        }
        #region  控件随窗口变化自动缩放
        private void Analysis_Load(object sender, EventArgs e)
        {
            this.Resize += new EventHandler(Analysis_ResizeBegin);//窗体调整大小时引发事件
            XWidth = this.Width;//获取窗体的宽度
            YHeight = this.Height;//获取窗体的高度
            setTag(this);//调用方法
        }

        //获取控件的width、height、left、top、字体大小的值，
        private void setTag(Control cons)
        {
            //遍历窗体中的控件
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }

        //根据窗体大小调整控件大小，添加一下代码：
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//获取控件的Tag属性值，并分割后存储字符串数组
                float a = Convert.ToSingle(mytag[0]) * newx;//根据窗体缩放比例确定控件的值，宽度
                con.Width = (int)a;//宽度
                a = Convert.ToSingle(mytag[1]) * newy;//高度
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;//左边距离
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;//上边缘距离
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * newy;//字体大小
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }

        private void Analysis_ResizeBegin(object sender, EventArgs e)
        {
            float newx = (this.Width) / XWidth; //窗体宽度缩放比例
            float newy = this.Height / YHeight;//窗体高度缩放比例
            setControls(newx, newy, this);//随窗体改变控件大小
        }
        #endregion

        //通道下拉框选择变化
        private void comboBox_CH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_CH.SelectedItem != null)
            {
                channelshow = Convert.ToInt32(comboBox_CH.SelectedItem.ToString());
                currentChannel = channelshow - 1;
                isCHSelected = true;

                //通道改变，清空待处理队列
                global.analysis_signal.Clear();

                global.currentChannel = currentChannel;
                
                if (isThreadRunning == false)  //如果已经开启，则不执行
                {
                    isThreadRunning = true;
                    //加入开启处理线程代码
                    //开启decode线程
                    decode = new Thread(decode_thread);
                    decode.IsBackground = true;
                    decode.Start();
                    //开启process线程
                    process = new Thread(process_thread);
                    process.IsBackground = true;
                    process.Start();
                }

            }
        }

        //测点下拉框选择变化
        private void comboBox_Point_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Point.SelectedItem != null)
            {
                if(Data.IsControlFBG==false)
                {
                    Data.IsControlFBG = true;
                }
                
                pointshow = Convert.ToInt32(comboBox_Point.SelectedItem.ToString());
                //测点改变，清空待处理队列
                global.analysis_signal.Clear();
                global.currentPoint = pointshow - 1;
            }
        }

        //小波分析中选择小波基
        private void comboBox1_wavelet_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_waveindex = comboBox_wavelet.SelectedIndex;
            if (comboBox_wavelet.SelectedIndex == 0)
            {
                //comboBox_db.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox_db.Items.Clear();
                comboBox_db.Items.Add("db2");
                comboBox_db.Items.Add("db3");
                comboBox_db.Items.Add("db4");
                comboBox_db.Items.Add("db5");
                comboBox_db.Items.Add("db6");
                comboBox_db.Items.Add("db7");
                comboBox_db.Items.Add("db8");
                comboBox_db.Items.Add("db9");
                comboBox_db.Items.Add("db10");

                comboBox_db.SelectedIndex = 0;
                let_c = 2;
            }
            else if (comboBox_wavelet.SelectedIndex == 1)
            {
                comboBox_db.Items.Clear();
                comboBox_db.Items.Add("sym2");
                comboBox_db.Items.Add("sym3");
                comboBox_db.Items.Add("sym4");
                comboBox_db.Items.Add("sym5");
                comboBox_db.Items.Add("sym6");
                comboBox_db.Items.Add("sym7");
                comboBox_db.Items.Add("sym8");

                comboBox_db.SelectedIndex = 0;
                let_c = 11;
            }
            else if (comboBox_wavelet.SelectedIndex == 2)
            {
                comboBox_db.Items.Clear();
                comboBox_db.Text = "";
                let_c = 18;//选择小波基'haar'
            }
            else if (comboBox_wavelet.SelectedIndex == 3)
            {
                comboBox_db.Items.Clear();
                comboBox_db.Items.Add("coif1");
                comboBox_db.Items.Add("coif2");
                comboBox_db.Items.Add("coif3");
                comboBox_db.Items.Add("coif4");
                comboBox_db.Items.Add("coif5");

                comboBox_db.SelectedIndex = 0;
                let_c = 20;
            }
            else
            {
                comboBox_db.Items.Clear();
            }
        }

        private void comboBox_db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_waveindex == 0)
            {
                if (comboBox_db.SelectedIndex == 0 && cmb_waveindex == 0)
                { let_c = 2; }//选择小波基db2
                if (comboBox_db.SelectedIndex == 1 && cmb_waveindex == 0)
                { let_c = 3; }//选择小波基db3
                if (comboBox_db.SelectedIndex == 2 && cmb_waveindex == 0)
                { let_c = 4; }//选择小波基db4
                if (comboBox_db.SelectedIndex == 3 && cmb_waveindex == 0)
                { let_c = 5; }//选择小波基db5
                if (comboBox_db.SelectedIndex == 4 && cmb_waveindex == 0)
                { let_c = 6; }//选择小波基db6
                if (comboBox_db.SelectedIndex == 5 && cmb_waveindex == 0)
                { let_c = 7; }//选择小波基db7
                if (comboBox_db.SelectedIndex == 6 && cmb_waveindex == 0)
                { let_c = 8; }//选择小波基db8
                if (comboBox_db.SelectedIndex == 7 && cmb_waveindex == 0)
                { let_c = 9; }//选择小波基db9
                if (comboBox_db.SelectedIndex == 8 && cmb_waveindex == 0)
                { let_c = 10; }//选择小波基db10
            }
            else if (cmb_waveindex == 1)
            {
                if (comboBox_db.SelectedIndex == 0 && cmb_waveindex == 1)
                { let_c = 11; }//选择小波基sym2
                if (comboBox_db.SelectedIndex == 1 && cmb_waveindex == 1)
                { let_c = 12; }//选择小波基sym3
                if (comboBox_db.SelectedIndex == 2 && cmb_waveindex == 1)
                { let_c = 13; }//选择小波基sym4
                if (comboBox_db.SelectedIndex == 3 && cmb_waveindex == 1)
                { let_c = 14; }//选择小波基sym5
                if (comboBox_db.SelectedIndex == 4 && cmb_waveindex == 1)
                { let_c = 15; }//选择小波基sym6
                if (comboBox_db.SelectedIndex == 5 && cmb_waveindex == 1)
                { let_c = 16; }//选择小波基sym7
                if (comboBox_db.SelectedIndex == 6 && cmb_waveindex == 1)
                { let_c = 17; }//选择小波基sym8
            }
            else if (cmb_waveindex == 3)
            {
                if (comboBox_db.SelectedIndex == 0)
                { let_c = 20; }//选择小波基'coif1'
                if (comboBox_db.SelectedIndex == 1)
                { let_c = 21; }//选择小波基'coif2'
                if (comboBox_db.SelectedIndex == 2)
                { let_c = 22; }//选择小波基'coif3'
                if (comboBox_db.SelectedIndex == 3)
                { let_c = 23; }//选择小波基'coif4'
                if (comboBox_db.SelectedIndex == 4)
                { let_c = 24; }//选择小波基'coif5'
            }
            else
            {
                comboBox_db.Items.Clear();
            }
        }

        //小波分析中显示系数选择
        private void comboBox_ad_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_wave_ad = comboBox_ad.SelectedIndex;
        }

        //decode后的数据放在AnalysisUser类中的analysis_signal队列的数组中 
        private void decode_thread()
        {
            while (isThreadRunning && isCHSelected)
            {
                //如果测点已经选择
                if (pointshow != 0)
                {
                    //while (global.all_msg.Count > winsize / 40)
                    while ((Receiver.process_all_msg_FBG.BufferSize > winsize / Data.FBG_numPackage) && (channelshow > 0))
                    //channel_show通道选择下拉
                    {
                        global.decode_fun(); //解析包，并填充一个滑动窗口，入队
                    }
                    //Thread.Sleep(5);
                }
            }
        }

        private void process_thread()
        {
            double[] process_signal = new double[0];
            while (isThreadRunning && isCHSelected)
            {
                if (global.analysis_signal.Count > 1)
                {
                    process_signal = global.GetProcessSignal();
                    switch (AnalysisMethod)
                    {
                        case "时频域波形":
                            TimeDomain(process_signal);
                            break;
                        case "瞬时相位分析":
                            IP(process_signal);
                            break;
                        case "MFDFA":
                            MFDFA(process_signal);
                            break;
                        case "小波分析":
                            WAVELET(process_signal);
                            break;
                        default: break;
                    }

                }
            }
        }

        //显示时域波形，可加入时序特征显示
        private void TimeDomain(double[] input)
        {
            double x1, y1;
            PointPairList list = new PointPairList();
            //画原始曲线
            for (int i = 0; i < input.Length; i++)
            {
                x1 = i;
                y1 = input[i];
                list.Add(x1, y1);
            }
            PaintDraw(list, zedGraph_Time, "时域波形图", "曲线图");

            // 画频谱
            double[] f = new double[0];  //横坐标
            double[] fftdata = new double[0]; //纵坐标
            global.FFT_Process(input, ref f, ref fftdata);
            double x2, y2;
            PointPairList listFFT = new PointPairList();
            //画原始曲线
            for (int i = 0; i < f.Length; i++)
            {
                x2 = f[i];
                y2 = fftdata[i];
                listFFT.Add(x2, y2);
            }
            PaintDraw(listFFT, zedGraph_FFT, "频谱", "曲线图");

        }
        //瞬时相位处理，输入待处理的数据，得到横纵坐标，画图
        private void IP(double[] input)
        {
            double[] t = new double[0];  //横坐标
            double[] th = new double[0]; //纵坐标
            global.IP_Process(input, ref t, ref th);
            //画曲线图
            double x1, y1;
            PointPairList listCurve = new PointPairList();
            for (int i = 0; i < t.Length; i++)
            {
                x1 = t[i];
                y1 = th[i];
                listCurve.Add(x1, y1);
            }
            //画原始曲线
            //for (int i = 0; i < input.Length; i++)
            //{
            //    x1 = i;
            //    y1 = input[i];
            //    listCurve.Add(x1, y1);
            //}
            PaintDraw(listCurve, zedGraph_IPCurve, "瞬时相位曲线图", "曲线图");

            //画散点图
            double[] th_1 = new double[th.Length - 1];
            double[] th_2 = new double[th.Length - 1];
            for (int i = 0; i < th.Length - 1; i++)
            {
                th_1[i] = th[i];
                th_2[i] = th[i + 1];
            }
            double x2, y2;
            PointPairList listScatter = new PointPairList();
            for (int i = 0; i < th_1.Length; i++)
            {
                x2 = th_1[i];
                y2 = th_2[i];
                listScatter.Add(x2, y2);
            }
            PaintDraw(listScatter, zedGraph_IPScatter, "瞬时相位散点图", "散点图");
        }
        //MFDFA，画图
        private void MFDFA(double[] input)
        {
            double[] Hq = new double[0];
            double[] tq = new double[0];
            double[] alpha = new double[0];
            double[] f = new double[0];
            double[] q = new double[0];
            global.MFDFA_Process(input, ref Hq, ref tq, ref alpha, ref f, ref q);
            //画曲线图
            double x1, y1;
            PointPairList listHq = new PointPairList();
            for (int i = 0; i < Hq.Length; i++)
            {
                x1 = q[i];
                y1 = Hq[i];
                listHq.Add(x1, y1);
            }
            PaintDraw(listHq, zedGraph_Hq, "广义Hurst指数H(q)", "曲线图");
            double x2, y2;
            PointPairList listtq = new PointPairList();
            for (int i = 0; i < tq.Length; i++)
            {
                x2 = q[i];
                y2 = tq[i];
                listtq.Add(x2, y2);
            }
            PaintDraw(listtq, zedGraph_tq, "尺度指数τ(q)", "曲线图");
            double x3, y3;
            PointPairList listf = new PointPairList();
            for (int i = 0; i < f.Length; i++)
            {
                x3 = alpha[i];
                y3 = f[i];
                listf.Add(x3, y3);
            }
            PaintDraw(listf, zedGraph_f, "多重分形谱", "曲线图");
            double alpha0 = alpha[alpha.Length / 2];
            double d_alpha = alpha[0] - alpha[alpha.Length - 1];
            double d_f = f[0] - f[f.Length - 1];
            //double d_f=Dq[hq.Length/2]-Math.Min(hq[hq.Length-1],hq[0]);
            //textBox_alpha0.Text = alpha0.ToString();
            //textBox_d_alpha.Text = d_alpha.ToString();
            //textBox_d_f.Text = d_f.ToString();
            this.setMFDFA_text(alpha0.ToString(), d_alpha.ToString(), d_f.ToString());
        }

        private void setMFDFA_text(string ALPHA0, string D_ALPHA, string D_F)
        {
            if (this.textBox_alpha0.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.textBox_alpha0.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.textBox_alpha0.Disposing || this.textBox_alpha0.IsDisposed)
                        return;
                }
                SetTextCallback d = new SetTextCallback(setMFDFA_text);
                this.textBox_alpha0.Invoke(d, new object[] { ALPHA0,D_ALPHA,D_F });
            }
            else
            {
                this.textBox_alpha0.Text = ALPHA0;
            }

            if (this.textBox_d_alpha.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.textBox_d_alpha.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.textBox_d_alpha.Disposing || this.textBox_d_alpha.IsDisposed)
                        return;
                }
                SetTextCallback d = new SetTextCallback(setMFDFA_text);
                this.textBox_d_alpha.Invoke(d, new object[] { ALPHA0, D_ALPHA, D_F });
            }
            else
            {
                this.textBox_d_alpha.Text = D_ALPHA;
            }

            if (this.textBox_d_f.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.textBox_d_f.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.textBox_d_f.Disposing || this.textBox_d_f.IsDisposed)
                        return;
                }
                SetTextCallback d = new SetTextCallback(setMFDFA_text);
                this.textBox_d_f.Invoke(d, new object[] { ALPHA0, D_ALPHA, D_F });
            }
            else
            {
                this.textBox_d_f.Text = D_F;
            }
        }
       
        //小波分析
        private void WAVELET(double[] input)
        {

            double[] a1 = new double[0];
            double[] a2 = new double[0]; //
            double[] a3 = new double[0]; //
            double[] a4 = new double[0]; //
            double[] a5 = new double[0]; //
            double[] a6 = new double[0]; //
            double[] a7 = new double[0]; //
            double[] d1 = new double[0]; //
            double[] d2 = new double[0]; //
            double[] d3 = new double[0]; //
            double[] d4 = new double[0]; //
            double[] d5 = new double[0]; //
            double[] d6 = new double[0];
            double[] d7 = new double[0];//

            global.WAVE_Process(input,let_c, ref a1, ref a2, ref a3, ref a4, ref a5, ref a6, ref a7, ref d1, ref d2, ref d3, ref d4, ref d5, ref d6, ref d7);
            //画曲线图
            if(cmb_wave_ad == 0) 
            {
                zedGraph_a1.GraphPane.Title.Text = "第一层";
                zedGraph_a1.GraphPane.XAxis.Title.Text = "样本序数";   //横坐标
                zedGraph_a1.GraphPane.YAxis.Title.Text = "a1";     //纵坐标

                zedGraph_a2.GraphPane.Title.Text = "第二层";
                zedGraph_a2.GraphPane.XAxis.Title.Text = "样本序数";   //横坐标
                zedGraph_a2.GraphPane.YAxis.Title.Text = "a2";     //纵坐标

                zedGraph_a3.GraphPane.Title.Text = "第三层";
                zedGraph_a3.GraphPane.XAxis.Title.Text = "样本序数";   //横坐标
                zedGraph_a3.GraphPane.YAxis.Title.Text = "a3";     //纵坐标

                zedGraph_a4.GraphPane.Title.Text = "第四层";
                zedGraph_a4.GraphPane.XAxis.Title.Text = "样本序数";   //横坐标
                zedGraph_a4.GraphPane.YAxis.Title.Text = "a4";     //纵坐标


                zedGraph_a5.GraphPane.Title.Text = "第五层";
                zedGraph_a5.GraphPane.XAxis.Title.Text = "样本序数";   //横坐标
                zedGraph_a5.GraphPane.YAxis.Title.Text = "a5";     //纵坐标

                zedGraph_a6.GraphPane.Title.Text = "第六层";
                zedGraph_a6.GraphPane.XAxis.Title.Text = "样本序数";   //横坐标
                zedGraph_a6.GraphPane.YAxis.Title.Text = "a6";     //纵坐标


                zedGraph_a7.GraphPane.Title.Text = "第七层";
                zedGraph_a7.GraphPane.XAxis.Title.Text = "样本序数";   //横坐标
                zedGraph_a7.GraphPane.YAxis.Title.Text = "a7";     //纵坐标

                PaintWave(a1, zedGraph_a1, "近似系数", "曲线图");
                PaintWave(a2, zedGraph_a2, "近似系数", "曲线图");
                PaintWave(a3, zedGraph_a3, "近似系数", "曲线图");
                PaintWave(a4, zedGraph_a4, "近似系数", "曲线图");
                PaintWave(a5, zedGraph_a5, "近似系数", "曲线图");
                PaintWave(a6, zedGraph_a6, "近似系数", "曲线图");
                PaintWave(a7, zedGraph_a7, "近似系数", "曲线图");
            }
            if (cmb_wave_ad == 1)
            {
                zedGraph_a1.GraphPane.Title.Text = "第一层";
                zedGraph_a1.GraphPane.XAxis.Title.Text = "样本序数";   //横坐标
                zedGraph_a1.GraphPane.YAxis.Title.Text = "d1";     //纵坐标

                zedGraph_a2.GraphPane.Title.Text = "第二层";
                zedGraph_a2.GraphPane.XAxis.Title.Text = "样本序数";   //横坐标
                zedGraph_a2.GraphPane.YAxis.Title.Text = "d2";     //纵坐标

                zedGraph_a3.GraphPane.Title.Text = "第三层";
                zedGraph_a3.GraphPane.XAxis.Title.Text = "样本序数";   //横坐标
                zedGraph_a3.GraphPane.YAxis.Title.Text = "d3";     //纵坐标

                zedGraph_a4.GraphPane.Title.Text = "第四层";
                zedGraph_a4.GraphPane.XAxis.Title.Text = "样本序数";   //横坐标
                zedGraph_a4.GraphPane.YAxis.Title.Text = "d4";     //纵坐标


                zedGraph_a5.GraphPane.Title.Text = "第五层";
                zedGraph_a5.GraphPane.XAxis.Title.Text = "样本序数";   //横坐标
                zedGraph_a5.GraphPane.YAxis.Title.Text = "d5";     //纵坐标

                zedGraph_a6.GraphPane.Title.Text = "第六层";
                zedGraph_a6.GraphPane.XAxis.Title.Text = "样本序数";   //横坐标
                zedGraph_a6.GraphPane.YAxis.Title.Text = "d6";     //纵坐标


                zedGraph_a7.GraphPane.Title.Text = "第七层";
                zedGraph_a7.GraphPane.XAxis.Title.Text = "样本序数";   //横坐标
                zedGraph_a7.GraphPane.YAxis.Title.Text = "d7";     //纵坐标
                PaintWave(d1, zedGraph_a1, "细节系数", "曲线图");
                PaintWave(d2, zedGraph_a2, "细节系数", "曲线图");
                PaintWave(d3, zedGraph_a3, "细节系数", "曲线图");
                PaintWave(d4, zedGraph_a4, "细节系数", "曲线图");
                PaintWave(d5, zedGraph_a5, "细节系数", "曲线图");
                PaintWave(d6, zedGraph_a6, "细节系数", "曲线图");
                PaintWave(d7, zedGraph_a7, "细节系数", "曲线图");
            }
            
        }
        private void PaintWave(double[] a_d, ZedGraph.ZedGraphControl zed, string label, string mode)
        {
            double x1, y1;
            PointPairList listWave = new PointPairList();
            for (int i = 0; i < a_d.Length; i++)
            {
                x1 = i + 1;
                y1 = a_d[i];
                listWave.Add(x1, y1);
            }
            PaintDraw(listWave, zed, label, mode);
        }

        //画图函数
        //pList:画图数据；
        //zed:控件名
        //label:标签；
        //mode:模式，曲线图or散点图。
        private void PaintDraw(PointPairList pList, ZedGraph.ZedGraphControl zed, string label, string mode)
        {
            LineItem myCurve;
            switch (mode)
            {
                case ("曲线图"):
                    zed.GraphPane.CurveList.Clear();//清空曲线
                                                    //创建曲线
                    myCurve = zed.GraphPane.AddCurve(label, pList, Color.Red, SymbolType.None);
                    zed.IsShowPointValues = true;//当鼠标经过时，显示点的坐标。
                    zed.GraphPane.AxisChange();  // 在数据变化时绘制图形;
                    zed.Invalidate();//刷新
                    break;
                case ("散点图"):
                    zed.GraphPane.CurveList.Clear();//清空曲线
                                                    //创建曲线
                    myCurve = zed.GraphPane.AddCurve(label, pList, Color.Red, SymbolType.Circle);
                    myCurve.Symbol.Size = 2.0f;
                    myCurve.Symbol.Fill = new Fill(Color.Red);
                    myCurve.Line.IsVisible = false;
                    //zedGraph1.IsShowPointValues = true;//当鼠标经过时，显示点的坐标。
                    zed.GraphPane.AxisChange();  // 在数据变化时绘制图形;
                    zed.Invalidate();
                    break;
                default: break;
            }
        }

    }
}


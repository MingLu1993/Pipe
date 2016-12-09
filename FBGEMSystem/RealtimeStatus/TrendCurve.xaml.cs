using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;
using Visifire.Charts;
using FBGEMSystem.DataStorage;
using System.IO;
using System.Xml;

namespace FBGEMSystem.RealtimeStatus
{
    /// <summary>
    /// TrendCurve.xaml 的交互逻辑
    /// </summary>
    public partial class TrendCurve : Window
    {
        private DispatcherTimer dispatcherTimer = null;
        private DispatcherTimer dispatcherTimer1 = null;

        int a = 0; int b = 0; int c = 0; int d = 0;

        GlobalVar gloVar = new GlobalVar();
        SystemConfig[] p;

        float[] s1 = new float[4000];
        float[] s2 = new float[4000];
        float[] s3 = new float[4000];
        float[] s4 = new float[4000];
        float[] s5 = new float[4000];
        float[] s6 = new float[4000];
        float[] s7 = new float[4000];
        float[] s8 = new float[4000];
        float[] s9 = new float[4000];
        float[] s10 = new float[4000];
        float[] s11 = new float[4000];
      //  float[] s12 = new float[4000];




        static int num = 0;
        bool isDraw1 = false;
        bool isDraw2 = false;
        bool isDraw3 = false;
        bool isDraw4 = false;
        bool isDraw5 = false;
        bool isDraw6 = false;
        bool isDraw7 = false;
        bool isDraw8 = false;
        bool isDraw9 = false;
        bool isDraw10 = false;
        bool isDraw11 = false;
     //   bool isDraw12 = false;

        public TrendCurve()
        {
            InitializeComponent();
            //a = Data.Chnum1;
            //b = Data.Chnum2;
            //c = Data.Chnum3;
            //d = Data.Chnum4;
            a = 3;
            b = 6;
            c = 9;
            d = 12;

            Data.IsControl1 = true;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000);
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Start();
            dispatcherTimer1 = new DispatcherTimer();
            dispatcherTimer1.Interval = TimeSpan.FromMilliseconds(100);
            dispatcherTimer1.Tick += new EventHandler(OnTimedEvent1);
            dispatcherTimer1.Start();

            p = new SystemConfig[gloVar.getTotalSensorNum()];
            ReadConfig readConfig = new ReadConfig();
            p = readConfig.readConfig("SystemConfig.xml");

            Axis[] ay = new Axis[11];
            ay[0] = this.Ay;
            ay[1] = this.Ay1;
            ay[2] = this.Ay2;
            ay[3] = this.Ay3;
            ay[4] = this.Ay4;
            ay[5] = this.Ay5;
            ay[6] = this.Ay6;
            ay[7] = this.Ay7;
            ay[8] = this.Ay8;
            ay[9] = this.Ay9;
            ay[10] = this.Ay10;
            //ay[11] = this.Ay11;

            for (int i = 0; i < 11; i++)
            {
                ay[i].AxisMaximum = p[1].getYRange().getX();
                ay[i].AxisMinimum = p[1].getYRange().getY();
            }
            //数据包数40替换为Data.num_Package
            Ax.AxisMaximum = Data.num_Package; ;
            Ax.AxisMinimum = 0;
            Ax1.AxisMaximum = Data.num_Package; ;
            Ax1.AxisMinimum = 0;
            Ax2.AxisMaximum = Data.num_Package; ;
            Ax2.AxisMinimum = 0;
            Ax3.AxisMaximum = Data.num_Package; ;
            Ax3.AxisMinimum = 0;
            Ax4.AxisMaximum = Data.num_Package; ;
            Ax4.AxisMinimum = 0;
            Ax5.AxisMaximum = Data.num_Package; ;
            Ax5.AxisMinimum = 0;
            Ax6.AxisMaximum = Data.num_Package; ;
            Ax6.AxisMinimum = 0;
            Ax7.AxisMaximum = Data.num_Package; ;
            Ax7.AxisMinimum = 0;
            Ax8.AxisMaximum = Data.num_Package; ;
            Ax8.AxisMinimum = 0;
            Ax9.AxisMaximum = Data.num_Package; ;
            Ax9.AxisMinimum = 0;
            Ax10.AxisMaximum = Data.num_Package; ;
            Ax10.AxisMinimum = 0;
            //Ax11.AxisMaximum = 40 ; ; 
            //Ax11.AxisMinimum = 0;

        }


        private void OnTimedEvent1(object sender, EventArgs e)
        {

            if (num < 1)
            {
                for (int i = 0; i < Data.num_Package; i++)
                {
                    //全部测点曲线绘制Data.num_Package Data.num_Sensor修改
                    s1[i + num * Data.num_Package] = Data.Ch1[i * Data.num_Sensor + 0];
                    s2[i + num * Data.num_Package] = Data.Ch1[i * Data.num_Sensor + 1];
                    s3[i + num * Data.num_Package] = Data.Ch1[i * Data.num_Sensor + 2];
                    s4[i + num * Data.num_Package] = Data.Ch1[i * Data.num_Sensor + 3];
                    s5[i + num * Data.num_Package] = Data.Ch1[i * Data.num_Sensor + 5];
                    s6[i + num * Data.num_Package] = Data.Ch1[i * Data.num_Sensor + 6];
                    s7[i + num * Data.num_Package] = Data.Ch1[i * Data.num_Sensor + 7];
                  //  s8[i + num * Data.num_Package] = Data.Ch1[i * Data.num_Sensor + 8];
                    //s9[i + num * Data.num_Package] = Data.Ch1[i * Data.num_Sensor + 9];
                    //s10[i + num * Data.num_Package] = Data.Ch1[i * Data.num_Sensor + 10];
                    //s11[i + num * Data.num_Package] = Data.Ch1[i * Data.num_Sensor + 11];
                   // s12[i + num * Data.num_Package] = Data.Ch1[i * 64 + 12];

                }
                num++;
            }
            if (num >= 1)
            {
                isDraw1 = true;
                isDraw2 = true;
                isDraw3 = true;
                isDraw4 = true;
                isDraw5 = true;
                isDraw6 = true;
                isDraw7 = true;
                isDraw8 = true;
                isDraw9 = true;
                isDraw10 = true;
                isDraw11 = true;
                //isDraw12 = true;
                num = 0;
            }
        }


        private void OnTimedEvent(object sender, EventArgs e)
        {
            //fs.DataPoints.Clear();
            //for (int i = 0; i < a; i++)
            //{
            //    fs.DataPoints.Add(new DataPoint { AxisXLabel = (i + 1).ToString() + "节点" });
            //    for (int j = 0; j < 40; j++)
            //    {
            //        fs.DataPoints.Add(new DataPoint { XValue = j + i * 40, YValue = Data.Ch1[j * 64 + i] });
            //    }
            //}


            fs.DataPoints.Clear();
            if (isDraw1)
            {
                //图1对每一包数据进行绘制Data.num_Package修改
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs.DataPoints.Add(new DataPoint { YValue = s1[j] });

                }
                isDraw1 = false;

            }

            fs1.DataPoints.Clear();
            if (isDraw2)
            {
                //图2对每一包数据进行绘制Data.num_Package修改
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs1.DataPoints.Add(new DataPoint { YValue = s2[j] });

                }
                isDraw2 = false;

            }

            fs2.DataPoints.Clear();
            if (isDraw3)
            {
                //图3对每一包数据进行绘制Data.num_Package修改
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs2.DataPoints.Add(new DataPoint { YValue = s3[j] });

                }
                isDraw3 = false;

            }

            fs3.DataPoints.Clear();
            if (isDraw4)
            {
                //图4对每一包数据进行绘制Data.num_Package修改
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs3.DataPoints.Add(new DataPoint { YValue = s4[j] });

                }
                isDraw4 = false;

            }
            fs4.DataPoints.Clear();
            if (isDraw5)
            {
                //图5对每一包数据进行绘制Data.num_Package修改
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs4.DataPoints.Add(new DataPoint { YValue = s1[j] });

                }
                isDraw5 = false;

            }

            fs5.DataPoints.Clear();
            if (isDraw6)
            {
                //图6对每一包数据进行绘制Data.num_Package修改
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs5.DataPoints.Add(new DataPoint { YValue = s2[j] });

                }
                isDraw6 = false;

            }

            fs6.DataPoints.Clear();
            if (isDraw7)
            {
                //图7对每一包数据进行绘制Data.num_Package修改
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs6.DataPoints.Add(new DataPoint { YValue = s3[j] });

                }
                isDraw7 = false;

            }

            fs7.DataPoints.Clear();
            if (isDraw8)
            {
                //图8对每一包数据进行绘制Data.num_Package修改
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs7.DataPoints.Add(new DataPoint { YValue = s4[j] });

                }
                isDraw8 = false;

            }
            fs8.DataPoints.Clear();
            if (isDraw9)
            {
                //图9对每一包数据进行绘制Data.num_Package修改
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs8.DataPoints.Add(new DataPoint { YValue = s1[j] });

                }
                isDraw9 = false;

            }

            fs9.DataPoints.Clear();
            if (isDraw10)
            {
                //图10对每一包数据进行绘制Data.num_Package修改
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs9.DataPoints.Add(new DataPoint { YValue = s2[j] });

                }
                isDraw10 = false;

            }

            fs10.DataPoints.Clear();
            if (isDraw11)
            {
               // 图11对每一包数据进行绘制Data.num_Package修改
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs10.DataPoints.Add(new DataPoint { YValue = s3[j] });

                }
                isDraw11 = false;

            }

            //fs11.DataPoints.Clear();
            //if (isDraw12)
            //{

            //    for (int j = 0; j < 40; j++)
            //    {
            //        fs11.DataPoints.Add(new DataPoint { YValue = s4[j] });

            //    }
            //    isDraw12 = false;

            //}



            //fs1.DataPoints.Clear();
            //for (int i = a; i < b; i++)
            //{
            //    fs1.DataPoints.Add(new DataPoint {  AxisXLabel = (i + 1).ToString() + "节点" });
            //    for (int j = 0; j < 40; j++)
            //    {
            //        fs1.DataPoints.Add(new DataPoint { XValue = j + i * 40, YValue = Data.Ch1[j * 64 + i] });
            //    }
            //}

            //fs2.DataPoints.Clear();
            //for (int i = b; i < c; i++)
            //{   
            //    fs2.DataPoints.Add(new DataPoint { AxisXLabel = (i + 1).ToString() + "节点" });
            //    for (int j = 0; j < 40; j++)
            //    {
            //        fs2.DataPoints.Add(new DataPoint { XValue = j + i * 40, YValue = Data.Ch1[j * 64 + i] });                  
            //    }
            //}

            //fs3.DataPoints.Clear();
            //for (int i = c; i < d; i++)
            //{           
            //    fs3.DataPoints.Add(new DataPoint { AxisXLabel = (i + 1).ToString() + "节点" });
            //    for (int j = 0; j < 40; j++)
            //    {
            //        fs3.DataPoints.Add(new DataPoint { XValue = j + i * 40, YValue = Data.Ch1[j * 64 + i] });             
            //    }             
            //}
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Data.IsControl1 = false;
            dispatcherTimer.Stop();
            dispatcherTimer1.Stop();
            fs.DataPoints.Clear();
            fs1.DataPoints.Clear();
            fs2.DataPoints.Clear();
            fs3.DataPoints.Clear();
            fs4.DataPoints.Clear();
            fs5.DataPoints.Clear();
            fs6.DataPoints.Clear();
            fs7.DataPoints.Clear();
            fs8.DataPoints.Clear();
            fs9.DataPoints.Clear();
            fs10.DataPoints.Clear();
            //fs11.DataPoints.Clear();
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;
using Visifire.Charts;
using Visifire.Commons; 
using System.Threading;
namespace FBGEMSystem.RealtimeStatus
{
    /// <summary>
    /// Status.xaml 的交互逻辑
    /// </summary>
    public partial class Status : Window
    {
       public Status()
        {
            InitializeComponent();
            Initial();
            thread = new Thread(new ThreadStart(decode_thread));
            thread.IsBackground = true;
            thread.Start();
            //thread2.Start();
            //启动定时器
            dispatcherTimer1 = new DispatcherTimer();
            dispatcherTimer1.Interval = TimeSpan.FromMilliseconds(5);
            dispatcherTimer1.Tick += new EventHandler(OnTimedEvent1);
            dispatcherTimer1.Start();
        }

       //ChannelCurve
        private DispatcherTimer dispatcherTimer1 = null;
        private int k = 1500;
        Thread thread;
        private int interval = 3000;//控制横轴的间距
        int channel1 = 0;
        private QueueData queue = new QueueData(2001);
        private Queue<float> que = new Queue<float>();
        private object ob = new object();
        Message msg = new Message();
        string FBGtime = "";

        //CHBar
        private DispatcherTimer dispatcherTimer2 = null;
        int a = 0;

        //TrendCurve
        private DispatcherTimer dispatcherTimer3 = null;

        int a3 = 0; int b3 = 0; int c3 = 0; int d3 = 0;

        private void Label_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
             
            //启动解析线程
            thread = new Thread(new ThreadStart(decode_thread));
            thread.IsBackground = true;
            thread.Start();
            //thread2.Start();
            //启动定时器
            dispatcherTimer1 = new DispatcherTimer();
            dispatcherTimer1.Interval = TimeSpan.FromMilliseconds(5);
            dispatcherTimer1.Tick += new EventHandler(OnTimedEvent1);
            dispatcherTimer1.Start();
        }

        private void Initial()
        {
            Data.IsControl2 = true;
            for (int i = 0; i < 12; i++)
            {
                comboBox_CH1Num.Items.Add(i);
            }

            //for (int i = 1; i < 7; i++)
            //{
            //    comboBox_Axis.Items.Add(1000 * i);
            //} 
            
           

        }

        private void decode_thread()
        {
            while (true)
            {
                while (Receiver.sharedLocation1.BufferSize > 0)
                {
                    ProcessData();
                }
            }
        }


        private void ProcessData()
        {
            msg = Receiver.sharedLocation1.Buffer;
            FBGtime = msg.dataTime.Substring(9);
            try
            {
                //realTimeStatus 数据点修改Data.num_Package
                for (int i = 0; i < Data.num_Package; i++)
                {
                    que.Enqueue(msg.CH1[i * 64 + channel1]);
                }

            }

            catch (Exception err)
            {
                ////MessageBox.Show(err.ToString());
            }

        }
        private void OnTimedEvent1(object sender, EventArgs e)
        {
            OnDraw(que, Singlefs, SingleAx);

        }
        private void OnDraw(Queue<float> qu, DataSeries dataSeries, Axis ax)
        {
            if (qu.Count > 0)
            {
                int realcount = 0;
                int realcount1 = 0;
                ax.AxisMaximum = dataSeries.DataPoints.Count;
                ax.AxisMinimum = dataSeries.DataPoints.Count - interval;
                if (dataSeries.DataPoints.Count > 3000)
                {
                    if (qu.Count >= k)
                    {
                        for (int i = 0; i < k; i++)
                        {
                            dataSeries.DataPoints.Add(new DataPoint { AxisXLabel = FBGtime, YValue = qu.Dequeue() });
                            dataSeries.DataPoints.RemoveAt(i);
                        }
                        qu.Clear();
                    }
                    if (qu.Count < k)
                    {
                        realcount = qu.Count;
                        for (int i = 0; i < realcount; i++)
                        {

                            dataSeries.DataPoints.Add(new DataPoint { AxisXLabel = FBGtime, YValue = qu.Dequeue() });
                            dataSeries.DataPoints.RemoveAt(i);

                        }
                        //qu.Clear();
                    }

                }
                if (dataSeries.DataPoints.Count <= 3000)
                {
                    if (qu.Count >= k)
                    {
                        for (int i = 0; i < k; i++)
                        {

                            dataSeries.DataPoints.Add(new DataPoint { AxisXLabel = FBGtime, YValue = qu.Dequeue() });

                        }
                        //qu.Clear();
                    }
                    if (qu.Count < k)
                    {
                        realcount1 = qu.Count;
                        for (int i = 0; i < realcount1; i++)
                        {

                            dataSeries.DataPoints.Add(new DataPoint { AxisXLabel = FBGtime, YValue = qu.Dequeue() });

                        }
                        //qu.Clear();
                    }
                }
            }
            GC.Collect();
        }

        
        private void CH1Num_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.comboBox_CH1Num.SelectedIndex != -1)
            {
                string selectedText = comboBox_CH1Num.SelectedValue.ToString();
                channel1 = int.Parse(selectedText);
            }
            this.Singlefs.Color = Brushes.Black;
        }



 

        private void Label_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            if (Data.IsControl2 == true)
            {
                Data.IsControl2 = false;
                que.Clear();   
                dispatcherTimer1.Stop();
            }
            if (Data.IsControl1 == true)
            {
            
                Data.IsControl1 = false;
                dispatcherTimer3.Stop();
            }
            a = Data.Chnum1;
            Data.IsControl1 = true;
            dispatcherTimer2 = new DispatcherTimer();
            dispatcherTimer2.Interval = TimeSpan.FromMilliseconds(10);
            dispatcherTimer2.Tick += new EventHandler(OnTimedEvent2);
            dispatcherTimer2.Start(); 
        }

        private void OnTimedEvent2(object sender, EventArgs e)
        {
            LineChannel.DataPoints.Clear();

            for (int i = 0; i < a; i++)
            {
                LineChannel.DataPoints.Add(new DataPoint { XValue = i, AxisXLabel = (i + 1).ToString() + "节点", YValue = Data.Ch1[i] });
            } 
            //GC.Collect();
        }

      

        private void Label_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            if (Data.IsControl2 == true)
            {
                Data.IsControl2 = false;
                que.Clear();
                dispatcherTimer1.Stop();
            }
            if (Data.IsControl1 == true)
            {
                Data.IsControl1 = false;
                dispatcherTimer2.Stop();
            }

            a3 = 3;
            b3 = 6;
            c3 = 9;
            d3 = 12;

            Data.IsControl1 = true;
            dispatcherTimer3 = new DispatcherTimer();
            dispatcherTimer3.Interval = TimeSpan.FromMilliseconds(10);
            dispatcherTimer3.Tick += new EventHandler(OnTimedEvent3);
            dispatcherTimer3.Start();
        }

        private void OnTimedEvent3(object sender, EventArgs e)
        {
            fs.DataPoints.Clear();
            for (int i = 0; i < a3; i++)
            {
                //realtime通道1 Data.num_Package修改
                fs.DataPoints.Add(new DataPoint { XValue = i * Data.num_Package, AxisXLabel = (i + 1).ToString() + "节点" });
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs.DataPoints.Add(new DataPoint { XValue = j + i * Data.num_Package, YValue = Data.Ch1[j * 12 + i] });
                }
            }

            fs1.DataPoints.Clear();
            for (int i = a3; i < b3; i++)
            {
                //realtime通道2 Data.num_Package修改
                fs1.DataPoints.Add(new DataPoint { XValue = i * Data.num_Package, AxisXLabel = (i + 1).ToString() + "节点" });
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs1.DataPoints.Add(new DataPoint { XValue = j + i * Data.num_Package, YValue = Data.Ch1[j * 12 + i] });
                }
            }

            fs2.DataPoints.Clear();
            for (int i = b3; i < c3; i++)
            {
                //realtime通道3 Data.num_Package修改
                fs2.DataPoints.Add(new DataPoint { XValue = i * Data.num_Package, AxisXLabel = (i + 1).ToString() + "节点" });
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs2.DataPoints.Add(new DataPoint { XValue = j + i * Data.num_Package, YValue = Data.Ch1[j * 12 + i] });
                }
            }

            fs3.DataPoints.Clear();
            for (int i = c3; i < d3; i++)
            {
                //realtime通道4 Data.num_Package修改
                fs3.DataPoints.Add(new DataPoint { XValue = i * Data.num_Package, AxisXLabel = (i + 1).ToString() + "节点" });
                for (int j = 0; j < Data.num_Package; j++)
                {
                    fs3.DataPoints.Add(new DataPoint { XValue = j + i * Data.num_Package, YValue = Data.Ch1[j * 12 + i] });
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Data.IsControl = false;
            Data.IsControl1 = false;
            Data.IsControl2 = false;
            dispatcherTimer1.Stop();
            dispatcherTimer2.Stop();
            dispatcherTimer3.Stop();
        }

        private void chart_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            //24,24,24,116
            if (chart.Height == 104)
            {
                chart.Height = 360;
            }
            else { chart.Height = 104; }
       
        }

        private void chart1_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (chart1.Height == 104)
            {
                chart1.Height = 360;
            }
            else { chart1.Height = 104; }
        }

        private void chart2_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (chart2.Height == 104)
            {
                chart2.Height = 360;
            }
            else { chart2.Height = 104; }
        }

        private void chart3_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (chart3.Height == 104)
            {
                chart3.Height = 360;
            }
            else { chart3.Height = 104; }
        }

        
        


        


    }
}
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
using System.Threading;
using Visifire.Charts;
using Visifire.Commons;
using FBGEMSystem.SystemSetting;
using FBGEMSystem.DataStorage;
using System.IO;
using System.Xml;


namespace FBGEMSystem.RealtimeStatus
{
    /// <summary>
    /// ChannelCurve.xaml 的交互逻辑
    /// </summary>
    public partial class ChannelCurve : Window
    {
        private DispatcherTimer dispatcherTimer = null;     
        private int k = 1500;
        Thread thread; 
        private int interval = 3000;//控制横轴的间距
        int channel1 = 0; 

//        private QueueData queue = new QueueData(2001); 

        private Queue<float> que = new Queue<float>(); 
//        private object ob = new object();

        Message msg = new Message();
        string FBGtime = "";

        GlobalVar gloVar = new GlobalVar();
        SystemConfig[] p;

        public ChannelCurve()
        {
            InitializeComponent();
            Initial();
            //启动解析线程
            thread = new Thread(new ThreadStart(decode_thread));
            thread.IsBackground = true;
            thread.Start();  
            //启动定时器
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(5);
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Start();

            p = new SystemConfig[gloVar.getTotalSensorNum()];
            ReadConfig readConfig = new ReadConfig();
            p = readConfig.readConfig("SystemConfig.xml");

            SingleAy.AxisMaximum = p[1].getYRange().getX();
            SingleAy.AxisMinimum = p[1].getYRange().getY();
        }


        private void Initial()
        {
            Data.IsControl2 = true;
            for (int i = 1; i <= 11; i++)
            {
                comboBox_CH1Num.Items.Add(i);
            }
            comboBox_CH1Num.SelectedItem = 1;

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
           
            try
            {             
                for (int i = 0; i < Data.num_Package; i++)
                {
                    que.Enqueue(msg.CH1[i * Data.num_Sensor + (channel1-1)]); 
                } 
            } 
            catch (Exception err)
            {
                //MessageBox.Show(err.ToString());
            }  
        }
     
        private void OnTimedEvent(object sender, EventArgs e)
        {
            OnDraw(que,ds, SingleAx);   
        }


        private void OnDraw(Queue<float> qu,DataSeries dataSeries, Axis ax)
        {
            if (qu.Count > 0)
            {
                int realcount = 0;
                int realcount1 = 0;
                ax.AxisMaximum = dataSeries.DataPoints.Count;
                ax.AxisMinimum = dataSeries.DataPoints.Count - interval;
                if(msg.dataTime.Length >0) FBGtime = msg.dataTime.Substring(msg.dataTime.Length - 12);
                         
                    if (dataSeries.DataPoints.Count > 3000)
                    {
                        if (qu.Count >= k)
                        {
                            for (int i = 0; i < k; i++)
                            {
                                dataSeries.DataPoints.Add(new DataPoint { AxisXLabel = FBGtime, YValue = qu.Dequeue() });
                                dataSeries.DataPoints.Remove(dataSeries.DataPoints[0]);
                            }
                            qu.Clear();
                        }
                        if (qu.Count < k)
                        {
                            realcount = qu.Count;
                            for (int i = 0; i < realcount; i++)
                            {

                                dataSeries.DataPoints.Add(new DataPoint { AxisXLabel = FBGtime, YValue = qu.Dequeue() });
                                dataSeries.DataPoints.Remove(dataSeries.DataPoints[0]);
                            }
                            qu.Clear();
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
                            qu.Clear();

                        }
                        if (qu.Count < k)
                        {
                            realcount1 = qu.Count;
                            for (int i = 0; i < realcount1; i++)
                            {

                                dataSeries.DataPoints.Add(new DataPoint { AxisXLabel = FBGtime, YValue = qu.Dequeue() });

                            }
                            qu.Clear();
                        }
                    }


                }
                GC.Collect();
            
        }

        private void Window_Closed(object sender, EventArgs e)
        { 
            thread.Abort();
            Data.IsControl2 = false;
            que.Clear();           
            dispatcherTimer.Stop();
            ds.DataPoints.Clear();
        }

        private void CH1Num_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.comboBox_CH1Num.SelectedIndex != -1)
            {
                string selectedText = comboBox_CH1Num.SelectedValue.ToString();
                channel1 = int.Parse(selectedText);
            } 
        }
         
    }
}

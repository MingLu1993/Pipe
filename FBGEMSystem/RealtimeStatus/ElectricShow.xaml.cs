﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Visifire.Charts;

namespace FBGEMSystem.RealtimeStatus
{
    /// <summary>
    /// ElectricShow.xaml 的交互逻辑
    /// </summary>
    public partial class ElectricShow : Window
    {
        private Queue<float> que = new Queue<float>();
        Message_Electric msg = new Message_Electric();
        private int k = 1500;
        int channel1 = 0;
        int type = 0;

        private int interval = 3000;//控制横轴的间距
        private DispatcherTimer dispatcherTimer = null;
        string Eletime = "";

        Thread thread;
        public ElectricShow()
        {
            InitializeComponent();
            Initial();  //设置电类传感器类型选择下拉选项
            SingleAy.StartFromZero = false;//坐标自动化
            //启动解析线程
            thread = new Thread(new ThreadStart(decodeEle_thread));
            thread.IsBackground = true;
            thread.Start();
            //启动定时器
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(5);
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            
            dispatcherTimer.Start();
        }

        private void Initial()
        {
            Data.IsControl2 = true;

            comboBox_typeNum.Items.Add("压力传感器");
            comboBox_typeNum.Items.Add("温度传感器");
            comboBox_typeNum.Items.Add("振动传感器");

            comboBox_typeNum.SelectedIndex = 0;  //默认设置为压力传感器
        }

        private void decodeEle_thread()
        {
            while (true)
            {
                while (Receiver.sharedLocation1Ele.BufferSize > 0)
                {
                    ProcessDataEle();
                }
            }
        }

        private void ProcessDataEle()
        {
            msg = Receiver.sharedLocation1Ele.Buffer;

            try
            {
                for (int i = 0; i < Data.num_Package; i++)
                {
                    que.Enqueue(msg.CH1[i * Data.num_Sensor * 3 + type * 8 + (channel1 - 1)]); 
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            OnDraw(que, ds, SingleAx);
        }

        private void OnDraw(Queue<float> qu, DataSeries dataSeries, Axis ax)
        {
            if (qu.Count > 0)
            {
                int realcount = 0;
                int realcount1 = 0;
                ax.AxisMaximum = dataSeries.DataPoints.Count;
                ax.AxisMinimum = dataSeries.DataPoints.Count - interval;
                if (msg.dataTime.Length > 0)
                    Eletime = msg.dataTime.Substring(msg.dataTime.Length - 12);

                if (dataSeries.DataPoints.Count > 3000)
                {
                    if (qu.Count >= k)
                    {
                        for (int i = 0; i < k; i++)
                        {
                            dataSeries.DataPoints.Add(new DataPoint { AxisXLabel = Eletime, YValue = qu.Dequeue() });
                            dataSeries.DataPoints.Remove(dataSeries.DataPoints[0]);
                        }
                        qu.Clear();
                    }
                    if (qu.Count < k)
                    {
                        realcount = qu.Count;
                        for (int i = 0; i < realcount; i++)
                        {

                            dataSeries.DataPoints.Add(new DataPoint { AxisXLabel = Eletime, YValue = qu.Dequeue() });
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

                            dataSeries.DataPoints.Add(new DataPoint { AxisXLabel = Eletime, YValue = qu.Dequeue() });

                        }
                        qu.Clear();

                    }
                    if (qu.Count < k)
                    {
                        realcount1 = qu.Count;
                        for (int i = 0; i < realcount1; i++)
                        {

                            dataSeries.DataPoints.Add(new DataPoint { AxisXLabel = Eletime, YValue = qu.Dequeue() });

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

        private void CHNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_CHNum.SelectedIndex != -1)
            {
                string selectedCH = comboBox_CHNum.SelectedValue.ToString();
                channel1 = int.Parse(selectedCH);
            }
            
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.comboBox_typeNum.SelectedIndex != -1)
            {
                string selectedType = comboBox_typeNum.SelectedIndex.ToString();
                type = int.Parse(selectedType);
            }
            //根据设置的各类传感器通道来设置item
            comboBox_CHNum.Items.Clear();
            if (comboBox_typeNum.SelectedIndex == 0)
            {
                for (int i = 0; i < Data.PressureIndex.Count; i++)
                {
                    comboBox_CHNum.Items.Add(Data.PressureIndex[i] + 1);
                }
            }
            else if (comboBox_typeNum.SelectedIndex == 1)
            {
                for (int i = 0; i < Data.TemperatureIndex.Count; i++)
                {
                    comboBox_CHNum.Items.Add(Data.TemperatureIndex[i] + 1);
                }
            }
            else
            {
                for (int i = 0; i < Data.VibrationIndex.Count; i++)
                {
                    comboBox_CHNum.Items.Add(Data.VibrationIndex[i] + 1);
                }
            }

            comboBox_CHNum.SelectedIndex = 0;   //默认设置为第一项
            string selectedCH = comboBox_CHNum.SelectedValue.ToString();
            channel1 = int.Parse(selectedCH);
        }
    }
}
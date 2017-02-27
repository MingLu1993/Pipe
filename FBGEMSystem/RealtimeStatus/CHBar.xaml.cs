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
using FBGEMSystem.DataStorage;
using System.IO;
using System.Xml;

namespace FBGEMSystem.RealtimeStatus
{
    /// <summary>
    /// Bar.xaml 的交互逻辑
    /// </summary>
    public partial class CHBar : Window
    {
        private DispatcherTimer dispatcherTimer = null;
        int a = 0; 
        //int b = 0; int c = 0; int d = 0;

        GlobalVar gloVar = new GlobalVar();
        SystemConfig[] p;

        public CHBar()
        {
            InitializeComponent();
            a = Data.Chnum1;  
            Data.IsControl1 = true; 
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(10);
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Start();

            p = new SystemConfig[gloVar.getTotalSensorNum()];
            ReadConfig readConfig = new ReadConfig();
            p = readConfig.readConfig("SystemConfig.xml");

            AyChannel.AxisMaximum = p[1].getYRange().getX();
            AyChannel.AxisMinimum = p[1].getYRange().getY();

        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            LineChannel.DataPoints.Clear();

            for (int i = 0; i < a; i++)
            {
                LineChannel.DataPoints.Add(new DataPoint {  AxisXLabel = (i + 1).ToString() + "节点", YValue = Data.Ch1[i] });
            }

             

            //GC.Collect();
        }

        private void Windows_Closed(object sender, EventArgs e)
        {
            Data.IsControl1 = false;
            dispatcherTimer.Stop();
            LineChannel.DataPoints.Clear();
        }

    }
}

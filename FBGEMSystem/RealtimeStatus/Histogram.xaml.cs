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

namespace FBGEMSystem.RealtimeStatus
{
    /// <summary>
    /// Histogram.xaml 的交互逻辑
    /// </summary>
    public partial class Histogram : Page
    {
        private DispatcherTimer dispatcherTimer = null;
        int a = 0; int b = 0; int c = 0; int d = 0;
       
        public Histogram()
        {
            InitializeComponent();

            a = Data.Chnum1;
            b = Data.Chnum2;
            c = Data.Chnum3;
            d = Data.Chnum4;

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(50);
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Start();    
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            LineChannel.DataPoints.Clear();

            for (int i = 0; i < a; i++)
            {
                LineChannel.DataPoints.Add(new DataPoint { XValue = i, AxisXLabel = (i + 1).ToString() + "节点", YValue = Data.Ch1[i] });
         
            }

            LineChannel1.DataPoints.Clear();
            for (int i = 0; i < b; i++)
            {
                LineChannel1.DataPoints.Add(new DataPoint { XValue =  i , AxisXLabel = (i + 1).ToString() + "节点", YValue = Data.Ch2[i] });
            }

            LineChannel2.DataPoints.Clear();
            for (int i = 0; i < c; i++)
            {
                LineChannel2.DataPoints.Add(new DataPoint { XValue = i , AxisXLabel = (i + 1).ToString() + "节点", YValue = Data.Ch3[i] });
            }

            LineChannel3.DataPoints.Clear();
            for (int i = 0; i < d; i++)
            {
                LineChannel3.DataPoints.Add(new DataPoint { XValue = i, AxisXLabel = (i + 1).ToString() + "节点", YValue = Data.Ch4[i] });
            }

            GC.Collect();
        }

    }
}

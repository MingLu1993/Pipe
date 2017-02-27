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
using System.Collections.ObjectModel;
using FBGEMSystem.LiveDataShow;
using System.ComponentModel;
using System.Threading;
using System.Web;
using System.Windows.Forms;


namespace FBGEMSystem.LiveDataShow
{
    /// <summary>
    /// ElecSensorData.xaml 的交互逻辑
    /// </summary>
    public partial class ElecSensorData : Window
    {

        ViewElecData EltecticData = new ViewElecData();
        public ElecSensorData()
        {
            InitializeComponent();
            //线程开
            EltecticData.datathread.Start();
            this.DataContext = EltecticData;
        }

        public void ESDClosed(object sender, EventArgs e)
        {
            //线程关，避免重复开线程
            //EltecticData.datathread.Abort();
            //线程中while（isrunning），改变标志位使线程执行完毕，不使用Abort().
            EltecticData.isThreadRun = false;
        }
        
    }
}

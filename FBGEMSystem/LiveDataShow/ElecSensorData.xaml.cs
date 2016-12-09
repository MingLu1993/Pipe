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
            this.DataContext = EltecticData;
        }
    }
}

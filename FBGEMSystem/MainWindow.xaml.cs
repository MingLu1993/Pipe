using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
//using System.Net;
//using System.Net.Sockets;
//using System.Runtime.InteropServices;
//using System.Configuration;
//using System.Diagnostics;
//using System.Data;
//using System.Data.Sql;
//using System.Data.SqlClient;
using FBGEMSystem.RealtimeStatus;
using System.Windows.Forms;
//using ZedGraph;
using FBGEMSystem.SystemSetting;
using FBGEMSystem.DataStorage;
using FBGEMSystem.LiveDataShow;
using FBGEMSystem.Set;


namespace FBGEMSystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //只使用了一个线程
        
        Thread[] threRecvs = new Thread[1];
        Thread[] threStor = new Thread[1];
        public static MainWindow pCurrentWin = null;

        public MainWindow()
        {
            pCurrentWin = this;   //子窗口可以使用MainWindow.pCurrenWin    来访问主窗口变量及方法

            Storer storer = new Storer();
            Receiver receiver = new Receiver();
            receiver.TCPClient_Initi();
            storer.GetConfig();
            storer.InitiTb();
            
            for (int i = 0; i < threRecvs.Length; i++)
            {
                threRecvs[i] = new Thread(new ThreadStart(receiver.Recv_Electric));
            }
            for (int i = 0; i < threRecvs.Length; i++)
            {
                threRecvs[i].Start();
            }
            //for (int j = 0; j < threStor.Length; j++)
            //{

            //    threStor[j] = new Thread(new ThreadStart(storer.Stor));
            //}
            //for (int j = 0; j < threStor.Length; j++)
            //{

            //    threStor[j].Start();
            //}
              
            //create system config
            ReadConfig readConfig = new ReadConfig();
            string result =readConfig.CreateConfigFile(); 

        }


        private void MenuItemTDZT_Click(object sender, RoutedEventArgs e)
        {

            ChannelCurve channleCurve = new ChannelCurve();
            channleCurve.ShowDialog();

        }

        private void MenuItemZTQS_Click(object sender, RoutedEventArgs e)
        {
            TrendCurve trendCurve = new TrendCurve();
            trendCurve.ShowDialog();

        }

        private void MenuItemZZT_Click(object sender, RoutedEventArgs e)
        {
            CHBar chbar = new CHBar();
            chbar.ShowDialog();
        }


        private void MenuItemXB_Click(object sender, RoutedEventArgs e)
        {
            Form1 f2 = new Form1();
            f2.Visible = true;
        }


        private void MenuItemQXPL_Click(object sender, RoutedEventArgs e)
        {
            axisYSet AxisYSET = new axisYSet();
            AxisYSET.ShowDialog();
            

        }

        private void MenuItemTXSZ_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.MessageBox.Show("其它功能界面弹出显示");

        }

        private void MenuItemDataSet_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.MessageBox.Show("其它功能界面弹出显示");

        }

        private void MenuItemSysHelp_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.MessageBox.Show("其它功能界面弹出显示");

        }

        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            ///System.Windows.MessageBox.Show("其它功能界面弹出显示");

        }


        private void ButtonMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;//最小化
        }

        //private void ButtonMax_Click(object sender, RoutedEventArgs e)
        //{
        //    if (WindowState == WindowState.Maximized)
        //        WindowState = WindowState.Normal;
        //    else
        //        WindowState = WindowState.Maximized;//最大化
        //}

        private void ButtonQuit_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win != this)
                {
                    win.Close();
                }
            }
            Environment.Exit(0);
            this.Close();
        }
         
        private void Window_Closed(object sender, EventArgs e)
        {
            
            Environment.Exit(0);
          
        }

        private void BackGround_MouseMove(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void MenuItemEle_Click(object sender, RoutedEventArgs e)
        {
            ElectricShow electricShow = new ElectricShow();
            electricShow.Show();

        }
        private void MenuItemCiucitSensor_Click(object sender, RoutedEventArgs e)
        {
            ElecSensorData ElectricData = new ElecSensorData();
            ElectricData.ShowDialog();

        }
        private void MenuItemFBGSensor_Click(object sender, RoutedEventArgs e)
        {
            FBGData fbgdata = new FBGData();
            //label1.Foreground = new SolidColorBrush(Colors.Red);
            fbgdata.ShowDialog();

        }

        private void MenuItemFBGSensor1_Click(object sender, RoutedEventArgs e)
        {
            FBGData1 fbgdata1= new FBGData1();
            fbgdata1.ShowDialog();
        }

        private void MenuItemChoosePicture_Click(object sender, RoutedEventArgs e)
        {
            Setting setting = new Setting();
            setting.Show();

        }

        public bool Picture(string path)
        {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri(path, UriKind.Absolute));
            imageBrush.Stretch = Stretch.Fill;//设置图像的显示格式为  扩展（填充整个画布）
            canvas.Background= imageBrush;
            if (canvas.Background != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point dp = new Point(); //定义一个坐标结构
            dp= Mouse.GetPosition(canvas);
            //System.Windows.MessageBox.Show( "X: " + dp.X +"\n" +"Y: " + dp.Y);

            //Rectangle rect;//创建一个方块
            //rect = new Rectangle();

            //TextBlock rect = new TextBlock();
            //rect.Text = "111";
            //rect.Width = 40;
            //rect.Height = 25;
  
            //canvas.Children.Add(rect);
            //Canvas.SetLeft(rect, dp.X);
            //Canvas.SetTop(rect, dp.Y);

            AddLabelInfo addLabelInfo = new AddLabelInfo(dp.X,dp.Y);
            addLabelInfo.Show();
        }

        public void Add_info(string info,double X,double Y)
        {
            TextBlock LabelInfo = new TextBlock();
            LabelInfo.Text = info;
            LabelInfo.Width = 50;
            LabelInfo.Height = 30;
            LabelInfo.Foreground = Brushes.Red;
            LabelInfo.FontSize = 20;

            canvas.Children.Add(LabelInfo);
            Canvas.SetLeft(LabelInfo, X);
            Canvas.SetTop(LabelInfo, Y);
        }

    }
}

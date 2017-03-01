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
using System.Net;
using getip;
using preprocess;
using plotfft;  //求FFTdll
using mfdfa;    //MFDFA_dll

namespace FBGEMSystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Cgetip iptmp = new Cgetip();
        Cpreprocess pretmp = new Cpreprocess();
        Cplotfft plotfft = new Cplotfft();
        Cmfdfa mf = new Cmfdfa();


        //只使用了一个线程
        private bool isrecvThreadInit = false;
        Thread threRecvsFBG;
        Thread threRecvsEle;
        Thread threStor;
        //Thread thredecode;不需要电类解包缓存


        public static MainWindow pCurrentWin = null;
        Storer storer;
        Receiver receiver;

        public MainWindow()
        {
            iptmp = null;
            pretmp = null;

            pCurrentWin = this;   //子窗口可以使用MainWindow.pCurrenWin    来访问主窗口变量及方法
            IPAddress.TryParse("192.168.1.10", out Data.remoteIP);  //remoteIP赋初值

           
            receiver = new Receiver();
            receiver.Client_Initi();
            

            //thredecode = new Thread(new ThreadStart(receiver.decode_Electric));

            //for (int i = 0; i < threRecvs.Length; i++)
            //{
            //    threRecvs[i] = new Thread(new ThreadStart(receiver.Recv_Electric));
            //}
            //for (int i = 0; i < threRecvs.Length; i++)
            //{
            //    threRecvs[i].Start();
            //}
            //thredecode.Start();

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

        private void MenuItemAnalysis_Click(object sender, RoutedEventArgs e)
        {
            Analysis analysis = new Analysis();
            analysis.ShowDialog();
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
            //如果未进行通道设置，提示先设置通道
            if (Data.isChannelSetting == false)
            {
                Data.IsControl2 = false;
                System.Windows.Forms.MessageBox.Show("请先设置通道", "帮助");
                Setting setting = new Setting();
                setting.Show();
                return;
            }

            ElectricShow electricShow = new ElectricShow();
            electricShow.Show();
        }
        private void MenuItemCiucitSensor_Click(object sender, RoutedEventArgs e)
        {
            ElecSensorData ElectricData = new ElecSensorData();
            ElectricData.Show();

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
            LabelInfo.FontSize = 12;

            canvas.Children.Add(LabelInfo);
            Canvas.SetLeft(LabelInfo, X);
            Canvas.SetTop(LabelInfo, Y);
        }

        private void MenuItemDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void MenuItemConnect_Click(object sender, RoutedEventArgs e)
        {
            receiver.SocketConnect();
        }
        private void MenuItemStart_Click(object sender, RoutedEventArgs e)
        {
            
            if (receiver.streamtoserver != null)      //必须先连接才能初始化receiver.streamtoserver
            {
                receiver.SocketStart();
                if (isrecvThreadInit == false)
                {
                    storer = new Storer();
                    storer.GetConfig();
                    storer.InitiTb();
                    threStor = new Thread(new ThreadStart(storer.Stor));
                    // storer.Stor();
                    threStor.Start();
                    threStor.IsBackground = true;

                    //开启接收线程
                    threRecvsFBG = new Thread(new ThreadStart(receiver.Recv_FBG));
                    threRecvsEle = new Thread(new ThreadStart(receiver.Recv_Electric));
                    threRecvsFBG.IsBackground = true;
                    threRecvsEle.IsBackground = true;
                    threRecvsFBG.Start();
                    //threRecvsEle.Start();
                    isrecvThreadInit = true;

                }
            }
            else
            {
              
                storer = new Storer();
                storer.GetConfig();
                storer.InitiTb();
                threStor = new Thread(new ThreadStart(storer.Stor));
                // storer.Stor();
                threStor.Start();
                threStor.IsBackground = true;

                System.Windows.MessageBox.Show("请先建立连接！", "警告");
                //调试udp
                threRecvsEle = new Thread(new ThreadStart(receiver.Recv_Electric));
                threRecvsEle.IsBackground = true;
                threRecvsEle.Start();
                //调试udp   end
                return;
            }
        }
        private void MenuItemStop_Click(object sender, RoutedEventArgs e)
        {
            //还需加入
        }
    }
}

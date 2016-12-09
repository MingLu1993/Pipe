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
using System.Configuration;
using System.IO;
using System.Xml;
using FBGEMSystem.DataStorage;
namespace FBGEMSystem.SystemSetting
{
    /// <summary>
    /// axisYSet.xaml 的交互逻辑
    /// </summary>
    public partial class axisYSet : Window
    {
        public axisYSet()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string ymax=yMax.Text.ToString(); //get the text from interface
            string ymin = yMin.Text.ToString();
            ReadConfig readConfig = new ReadConfig();

            if (Double.Parse(ymax) <= Double.Parse(ymin))
            {
                MessageBox.Show("最大值不能小于等于最小值！请修改！");
                return;
            }

            if (!File.Exists("SystemConfig.xml"))
            {
                MessageBox.Show("配置文件不存在，请创建");
            }
            else
            {
                try
                {
                    readConfig.updateConfig(ymax,ymin);   
                }
                catch
                {
                    MessageBox.Show("update systemconfig error!");
                }
            }
            this.Close();
        }
    }
}

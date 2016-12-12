using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FBGEMSystem.Set
{
    public partial class AddLabelInfo : Form
    {
        private double pointX;
        private double pointY;

        public AddLabelInfo(double X,double Y)
        {
            InitializeComponent();
            pointX = X;
            pointY = Y;
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            string LabelInfo = textBox_labelInfo.Text;
            MainWindow.pCurrentWin.Add_info(LabelInfo, pointX, pointY);  //可改用回调函数
            this.Close();
        }
    }
}

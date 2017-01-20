using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace FBGEMSystem
{
    public partial class Analysis : Form
    {
        float XWidth;           //用于控件随窗口变化自动缩放，窗体的宽度
        float YHeight;          //用于控件随窗口变化自动缩放，窗体的高度

        GraphPane PaneIPCurve;    //用于瞬时相位分析波形图
        GraphPane PaneIPScatter;  //用于瞬时相位分析散点图

        int channelshow = 0;      //第几通道
        int currentChannel = -1;  //通道的索引
        bool isThreadRunning = false;  //用于判断各线程是否已经开启
        bool isSelectCH = false;   //用于判断是否选中通道

        public Analysis()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            comboBox_CH.Items.Add("1");
            comboBox_CH.Items.Add("2");
            comboBox_CH.Items.Add("3");
            comboBox_CH.Items.Add("4");

            PaneIPCurve = zedGraph_IPCurve.GraphPane;
            PaneIPCurve.Title.Text = "瞬时相位";        //标题
            PaneIPCurve.XAxis.Title.Text = "Time(s)";   //横坐标
            PaneIPCurve.YAxis.Title.Text = "Phase";     //纵坐标

            PaneIPScatter = zedGraph_IPScatter.GraphPane;
            PaneIPScatter.Title.Text = "瞬时相位自相关散点图";          //标题
            PaneIPScatter.XAxis.Title.Text = "IP(n)";                   //横坐标
            PaneIPScatter.YAxis.Title.Text = "IP(n+1)";                 //纵坐标
        }
        #region  控件随窗口变化自动缩放
        private void InstantaneousPhase_Load(object sender, EventArgs e)
        {
            this.Resize += new EventHandler(InstantaneousPhase_ResizeBegin);//窗体调整大小时引发事件
            XWidth = this.Width;//获取窗体的宽度
            YHeight = this.Height;//获取窗体的高度
            setTag(this);//调用方法
        }

        //获取控件的width、height、left、top、字体大小的值，
        private void setTag(Control cons)
        {
            //遍历窗体中的控件
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }

        //根据窗体大小调整控件大小，添加一下代码：
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//获取控件的Tag属性值，并分割后存储字符串数组
                float a = Convert.ToSingle(mytag[0]) * newx;//根据窗体缩放比例确定控件的值，宽度
                con.Width = (int)a;//宽度
                a = Convert.ToSingle(mytag[1]) * newy;//高度
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;//左边距离
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;//上边缘距离
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * newy;//字体大小
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }

        private void InstantaneousPhase_ResizeBegin(object sender, EventArgs e)
        {
            float newx = (this.Width) / XWidth; //窗体宽度缩放比例
            float newy = this.Height / YHeight;//窗体高度缩放比例
            setControls(newx, newy, this);//随窗体改变控件大小
        }
        #endregion

        private void comboBox_CH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_CH.SelectedItem != null)
            {
                channelshow = Convert.ToInt32(comboBox_CH.SelectedItem.ToString());
                currentChannel = channelshow - 1;
                isSelectCH = true;
                if(isThreadRunning==false)  //如果已经开启，则不执行
                {

                    isThreadRunning = true;
                }

            }
        }

    }
}

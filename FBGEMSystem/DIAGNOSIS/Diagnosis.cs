using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Runtime.InteropServices;
using System.Threading;

namespace FBGEMSystem
{
    public partial class Diagnosis : Form
    {

        double[,] TrainSamples;
        bool isSampleRead; //是否读取完毕
        
        public Diagnosis()
        {
            InitializeComponent();
            textBox_T.Text = DiagnosisUser.T.ToString();
            if (DiagnosisUser.K > 0 && button_TOK.Enabled == true)
            {
                button_TOK.Enabled = false;   //进行过训练后，T值不可改变
            }
        }

        private void button_TOK_Click(object sender, EventArgs e)
        {
            if( (textBox_T.Text!="") && (int.Parse(textBox_T.Text) != 0) )
            {
                DiagnosisUser.T = int.Parse(textBox_T.Text);
            }
            else
            {
                MessageBox.Show("请输入有效T值！T取正整数");
            }
        }

        //T输入只能为整数
        private void textBox_T_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void button_chooseNew_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog_sample.ShowDialog())
            {
                textBox_newsamples.Text = openFileDialog_sample.FileName;
            }
        }

        private void button_ReadSample_Click(object sender, EventArgs e)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(textBox_newsamples.Text);
            string line;

            line = file.ReadLine();    //读一行，为属性名
            String[] strs = line.Split(',');   //为了得到列数
            int num = strs.Count();
            List<double[]> TrainSamplesList = new List<double[]>();

            while ((line = file.ReadLine()) != null)
            {
                strs = line.Split(',');
                double[] tmpLine = new double[num];
                for(int i=0;i< num;i++)
                {
                    tmpLine[i] = double.Parse(strs[i]);
                }
                TrainSamplesList.Add(tmpLine);
            }
            file.Close();//关闭文件读取流
            TrainSamples = new double[TrainSamplesList.Count(), num];
            for (int j=0;j< TrainSamplesList.Count();j++)
            {
                for(int x= 0;x< num;x++)
                {
                    TrainSamples[j,x] = TrainSamplesList[j][x];
                }
            }
            isSampleRead = true;
            DiagnosisUser.TargetsNum = 5;  //!!!!!!!!!!!!!
            MessageBox.Show("读取完毕！");
        }

        private void button_learn_Click(object sender, EventArgs e)
        {
            if(DiagnosisUser.T==0)
            {
                MessageBox.Show("请先设置T！");
            }
            else
            {
                if (isSampleRead==true)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Reset();
                    stopwatch.Start(); //  开始监视代码运行时间

                    DiagnosisUser.TRAIN(TrainSamples);
                    TrainSamples = null;
                    isSampleRead = false;
                    stopwatch.Stop(); //  停止监视
                    TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
                    MessageBox.Show("经过"+ timespan.ToString()+"s"+"第"+DiagnosisUser.K.ToString()+"次训练成功");

                    if (DiagnosisUser.K > 0  && button_TOK.Enabled ==true)
                    {
                        button_TOK.Enabled = false;   //进行过训练后，T值不可改变
                    }
                }
                else
                {
                    MessageBox.Show("请先读取样本集！");
                }
            }
        }
        //[DllImport("kernel32.dll")]
        //static extern int Beep(int dwFreq, int dwDuration);
        bool warning;
        void beep()
        {
            while(warning)
            {
                int a = 5000;
                int b = 1000;
                //Beep(a, b);
                Console.Beep(a, b);
            }   
        }

        [DllImport("winmm.dll")]
        public static extern bool PlaySound(String Filename, int Mod, int Flags);
        void beep1()
        {
            while (warning)
            {
                PlaySound("at20157171155.mp3", 0, 1);      //把1替换成9，可连续播放 
            }
     
        }

        private void button_test_Click(object sender, EventArgs e)
        {
            double[,] test = { { 9, 2, 18, 0.222000000000000, 0.611000000000000, 0.722000000000000, 11, 11, 13, 1 } };
            if(DiagnosisUser.K==0)
            {
                MessageBox.Show("请先进行训练！");
            }
            else
            {
               int result= DiagnosisUser.PREDICT(test);
                warning = true;
                Thread trd = new Thread(beep);
                trd.Start();
                //MessageBox.Show("预测结果为"+result.ToString());
                MessageBox.Show("出现卡箍松动故障，请尽快检修！","警告！");
                warning = false;
            }
        }
    }
}

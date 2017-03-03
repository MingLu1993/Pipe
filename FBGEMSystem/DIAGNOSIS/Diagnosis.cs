using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FBGEMSystem.Diagnosis
{
    public partial class Diagnosis : Form
    {

        double[,] TrainSamples;
        
        public Diagnosis()
        {
            InitializeComponent();
        }

        private void button_TOK_Click(object sender, EventArgs e)
        {
            if(textBox_T.Text!="")
            {
                DiagnosisUser.T = int.Parse(textBox_T.Text);
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
            int r = 0;

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
        }

        private void button_learn_Click(object sender, EventArgs e)
        {
            if(DiagnosisUser.T==0)
            {
                MessageBox.Show("请先设置T");
            }
            else
            {

            }
        }


    }
}

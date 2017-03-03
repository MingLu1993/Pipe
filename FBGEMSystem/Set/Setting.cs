using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;


namespace FBGEMSystem
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
            Initialize();
            textBox_IP.Text = "192.168.1.10";
            textBox_UDPPort.Text = "8";
            textBox_TCPPort.Text = "7"; 
        }

        private void Initialize()
        {
            PressureSensor1.Checked = false;
            PressureSensor2.Checked = false;
            PressureSensor3.Checked = false;
            PressureSensor4.Checked = false;
            PressureSensor5.Checked = false;
            PressureSensor6.Checked = false;
            PressureSensor7.Checked = false;
            PressureSensor8.Checked = false;
            Temperature1.Checked = false;
            Temperature2.Checked = false;
            Temperature3.Checked = false;
            Temperature4.Checked = false;
            Temperature5.Checked = false;
            Temperature6.Checked = false;
            Temperature7.Checked = false;
            Temperature8.Checked = false;
            Vibration1.Checked = false;
            Vibration2.Checked = false;
            Vibration3.Checked = false;
            Vibration4.Checked = false;
            Vibration5.Checked = false;
            Vibration6.Checked = false;
            Vibration7.Checked = false;
            Vibration8.Checked = false;


        }

        private void Button_Choose_Click(object sender, EventArgs e)
        {
            bool isPictureChoosen=false;
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                textBox_choosePicture.Text = openFileDialog1.FileName;
                isPictureChoosen = MainWindow.pCurrentWin.Picture(textBox_choosePicture.Text);
            }
            //if (isPictureChoosen == true)
            //    this.Close();
        }

        //传感器设置确定
        private void Sure_Button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Data.num_Sensor; i++)
            {
                Data.Pressure[i].is_Choose = false;
                Data.Temperature[i].is_Choose = false;
                Data.Vibration[i].is_Choose =false;
            }

            Data.PressureIndex.Clear();
            Data.TemperatureIndex.Clear();
            Data.VibrationIndex.Clear();
            Data.isChannelSetting = false;
            Data.FBGChannelIndex.Clear();
            CheckSensor();
            GetSensitivity();
            GetRange();
            GetFBGChannelNum();

            //获取各传感器通道使用索引
            for (int i=0;i<Data.num_Sensor;i++)
            {
                if(Data.Pressure[i].is_Choose == true)
                {
                    Data.PressureIndex.Add(i);
                }
                if (Data.Temperature[i].is_Choose == true)
                {
                    Data.TemperatureIndex.Add(i);
                }
                if (Data.Vibration[i].is_Choose == true)
                {
                    Data.VibrationIndex.Add(i);
                }
            }

            Data.Chnum1 = Data.PressureIndex.Count;
            Data.Chnum2 = Data.TemperatureIndex.Count;
            Data.Chnum3 = Data.VibrationIndex.Count;

            Data.isChannelSetting = true;
            MessageBox.Show("通道设置完毕");
           
        }

        private void GetFBGChannelNum()
        {
            
            Data.FBGCH1 = int.Parse(FBGChannel1_Num.Text);
            Data.FBGCH2 = int.Parse(FBGChannel2_Num.Text);
            Data.FBGCH3 = int.Parse(FBGChannel3_Num.Text);
            Data.FBGCH4 = int.Parse(FBGChannel4_Num.Text);

        }

        //选择各类传感器的通道号
        private void CheckSensor()
        {
            if (PressureSensor1.Checked == true)
            {
                Data.Pressure[0].is_Choose = true;
            }
            if (PressureSensor2.Checked == true)
            {
                Data.Pressure[1].is_Choose = true;
            }
            if (PressureSensor3.Checked == true)
            {
                Data.Pressure[2].is_Choose = true;
            }
            if (PressureSensor4.Checked == true)
            {
                Data.Pressure[3].is_Choose = true;
            }
            if (PressureSensor5.Checked == true)
            {
                Data.Pressure[4].is_Choose = true;
            }
            if (PressureSensor6.Checked == true)
            {
                Data.Pressure[5].is_Choose = true;
            }
            if (PressureSensor7.Checked == true)
            {
                Data.Pressure[6].is_Choose = true;
            }
            if (PressureSensor8.Checked == true)
            {
                Data.Pressure[7].is_Choose = true;
            }

            if (Temperature1.Checked == true)
            {
                Data.Temperature[0].is_Choose = true;
            }
            if (Temperature2.Checked == true)
            {
                Data.Temperature[1].is_Choose = true;
            }
            if (Temperature3.Checked == true)
            {
                Data.Temperature[2].is_Choose = true;
            }
            if (Temperature4.Checked == true)
            {
                Data.Temperature[3].is_Choose = true;
            }
            if (Temperature5.Checked == true)
            {
                Data.Temperature[4].is_Choose = true;
            }
            if (Temperature6.Checked == true)
            {
                Data.Temperature[5].is_Choose = true;
            }
            if (Temperature7.Checked == true)
            {
                Data.Temperature[6].is_Choose = true;
            }
            if (Temperature8.Checked == true)
            {
                Data.Temperature[7].is_Choose = true;
            }

            if (Vibration1.Checked == true)
            {
                Data.Vibration[0].is_Choose = true;
            }
            if (Vibration2.Checked == true)
            {
                Data.Vibration[1].is_Choose = true;
            }
            if (Vibration3.Checked == true)
            {
                Data.Vibration[2].is_Choose = true;
            }
            if (Vibration4.Checked == true)
            {
                Data.Vibration[3].is_Choose = true;
            }
            if (Vibration5.Checked == true)
            {
                Data.Vibration[4].is_Choose = true;
            }
            if (Vibration6.Checked == true)
            {
                Data.Vibration[5].is_Choose = true;
            }
            if (Vibration7.Checked == true)
            {
                Data.Vibration[6].is_Choose = true;
            }
            if (Vibration8.Checked == true)
            {
                Data.Vibration[7].is_Choose = true;
            }
        }

        //获取各通道传感器的灵敏度
        private void GetSensitivity()
        {
            Data.Pressure[0].Sensitivity = float.Parse(Pressure_Sensitivity1.Text);
            Data.Pressure[1].Sensitivity = float.Parse(Pressure_Sensitivity2.Text);
            Data.Pressure[2].Sensitivity = float.Parse(Pressure_Sensitivity3.Text);
            Data.Pressure[3].Sensitivity = float.Parse(Pressure_Sensitivity4.Text);
            Data.Pressure[4].Sensitivity = float.Parse(Pressure_Sensitivity5.Text);
            Data.Pressure[5].Sensitivity = float.Parse(Pressure_Sensitivity6.Text);
            Data.Pressure[6].Sensitivity = float.Parse(Pressure_Sensitivity7.Text);
            Data.Pressure[7].Sensitivity = float.Parse(Pressure_Sensitivity8.Text);

            Data.Temperature[0].Sensitivity = float.Parse(Temperature_Sensitivity1.Text);
            Data.Temperature[1].Sensitivity = float.Parse(Temperature_Sensitivity2.Text);
            Data.Temperature[2].Sensitivity = float.Parse(Temperature_Sensitivity3.Text);
            Data.Temperature[3].Sensitivity = float.Parse(Temperature_Sensitivity4.Text);
            Data.Temperature[4].Sensitivity = float.Parse(Temperature_Sensitivity5.Text);
            Data.Temperature[5].Sensitivity = float.Parse(Temperature_Sensitivity6.Text);
            Data.Temperature[6].Sensitivity = float.Parse(Temperature_Sensitivity7.Text);
            Data.Temperature[7].Sensitivity = float.Parse(Temperature_Sensitivity8.Text);

            Data.Vibration[0].Sensitivity = float.Parse(Vibration_Sensitivity1.Text);
            Data.Vibration[1].Sensitivity = float.Parse(Vibration_Sensitivity2.Text);
            Data.Vibration[2].Sensitivity = float.Parse(Vibration_Sensitivity3.Text);
            Data.Vibration[3].Sensitivity = float.Parse(Vibration_Sensitivity4.Text);
            Data.Vibration[4].Sensitivity = float.Parse(Vibration_Sensitivity5.Text);
            Data.Vibration[5].Sensitivity = float.Parse(Vibration_Sensitivity6.Text);
            Data.Vibration[6].Sensitivity = float.Parse(Vibration_Sensitivity7.Text);
            Data.Vibration[7].Sensitivity = float.Parse(Vibration_Sensitivity8.Text);
        }

        //获取各通道传感器的量程
        private void GetRange()
        {
            Data.Pressure[0].range_low = float.Parse(PressureRange_low1.Text);
            Data.Pressure[1].range_low = float.Parse(PressureRange_low2.Text);
            Data.Pressure[2].range_low = float.Parse(PressureRange_low3.Text);
            Data.Pressure[3].range_low = float.Parse(PressureRange_low4.Text);
            Data.Pressure[4].range_low = float.Parse(PressureRange_low5.Text);
            Data.Pressure[5].range_low = float.Parse(PressureRange_low6.Text);
            Data.Pressure[6].range_low = float.Parse(PressureRange_low7.Text);
            Data.Pressure[7].range_low = float.Parse(PressureRange_low8.Text);

            Data.Temperature[0].range_low = float.Parse(TemperatureRange_low1.Text);
            Data.Temperature[1].range_low = float.Parse(TemperatureRange_low2.Text);
            Data.Temperature[2].range_low = float.Parse(TemperatureRange_low3.Text);
            Data.Temperature[3].range_low = float.Parse(TemperatureRange_low4.Text);
            Data.Temperature[4].range_low = float.Parse(TemperatureRange_low5.Text);
            Data.Temperature[5].range_low = float.Parse(TemperatureRange_low6.Text);
            Data.Temperature[6].range_low = float.Parse(TemperatureRange_low7.Text);
            Data.Temperature[7].range_low = float.Parse(TemperatureRange_low8.Text);

            Data.Vibration[0].range_low = float.Parse(VibrationRange_low1.Text);
            Data.Vibration[1].range_low = float.Parse(VibrationRange_low2.Text);
            Data.Vibration[2].range_low = float.Parse(VibrationRange_low3.Text);
            Data.Vibration[3].range_low = float.Parse(VibrationRange_low4.Text);
            Data.Vibration[4].range_low = float.Parse(VibrationRange_low5.Text);
            Data.Vibration[5].range_low = float.Parse(VibrationRange_low6.Text);
            Data.Vibration[6].range_low = float.Parse(VibrationRange_low7.Text);
            Data.Vibration[7].range_low = float.Parse(VibrationRange_low8.Text);

            Data.Pressure[0].range_high = float.Parse(PressureRange_high1.Text);
            Data.Pressure[1].range_high = float.Parse(PressureRange_high2.Text);
            Data.Pressure[2].range_high = float.Parse(PressureRange_high3.Text);
            Data.Pressure[3].range_high = float.Parse(PressureRange_high4.Text);
            Data.Pressure[4].range_high = float.Parse(PressureRange_high5.Text);
            Data.Pressure[5].range_high = float.Parse(PressureRange_high6.Text);
            Data.Pressure[6].range_high = float.Parse(PressureRange_high7.Text);
            Data.Pressure[7].range_high = float.Parse(PressureRange_high8.Text);

            Data.Temperature[0].range_high = float.Parse(TemperatureRange_high1.Text);
            Data.Temperature[1].range_high = float.Parse(TemperatureRange_high2.Text);
            Data.Temperature[2].range_high = float.Parse(TemperatureRange_high3.Text);
            Data.Temperature[3].range_high = float.Parse(TemperatureRange_high4.Text);
            Data.Temperature[4].range_high = float.Parse(TemperatureRange_high5.Text);
            Data.Temperature[5].range_high = float.Parse(TemperatureRange_high6.Text);
            Data.Temperature[6].range_high = float.Parse(TemperatureRange_high7.Text);
            Data.Temperature[7].range_high = float.Parse(TemperatureRange_high8.Text);

            Data.Vibration[0].range_high = float.Parse(VibrationRange_high1.Text);
            Data.Vibration[1].range_high = float.Parse(VibrationRange_high2.Text);
            Data.Vibration[2].range_high = float.Parse(VibrationRange_high3.Text);
            Data.Vibration[3].range_high = float.Parse(VibrationRange_high4.Text);
            Data.Vibration[4].range_high = float.Parse(VibrationRange_high5.Text);
            Data.Vibration[5].range_high = float.Parse(VibrationRange_high6.Text);
            Data.Vibration[6].range_high = float.Parse(VibrationRange_high7.Text);
            Data.Vibration[7].range_high = float.Parse(VibrationRange_high8.Text);
        }

        //通信设置确定
        private void button_SocketSure_Click(object sender, EventArgs e)
        {
            string IPstr = textBox_IP.Text;
            IPAddress ip;
            if (IPAddress.TryParse(IPstr, out ip))
            {
                Data.remoteIP = ip;
            }
            else
            {
                textBox_IP.Text = "192.168.1.10";
                MessageBox.Show("请输入合法的IP地址");
            }
            Data.UDPPort = int.Parse(textBox_UDPPort.Text);
            Data.TCPPort = int.Parse(textBox_TCPPort.Text);
            MessageBox.Show("通信设置完毕");
        }

        #region //设置光栅个数选择textbox中只能输入整数
        private void FBGtextboxLimit(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != '.')

            {
                e.Handled = true;
            }
        }
        #endregion



        //设置电类传感器textbox中只能输  "-",数字,"." 
        private void ELETextboxLimit(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0)) e.Handled = true;
            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0) e.Handled = true;
        }

        //设置textbox点击时若text为"0"，则清空
        private void TextBox_MouseDown(object sender, MouseEventArgs e)
        {

            //TextBox textbox = sender as TextBox;
            //if (textbox.Text == "0")
            //{
            //    textbox.Text = "";
            //}
        }


        //端口号textbox只能输入数字
        private void textBox_UDPPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }
        //端口号textbox只能输入数字
        private void textBox_TCPPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }
        //IPtextbox只能输入数字和.
        private void textBox_IP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 &&  e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace FBGEMSystem
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
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

        private void Sure_Button_Click(object sender, EventArgs e)
        {
            CheckSensor();
            GetSensitivity();
            GetRange();
            this.Close();
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



        #region  //设置灵敏度textbox中只能输  "-",数字,"." 
        private void TextboxLimit(object sender, KeyPressEventArgs e)
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

        private void Pressure_Sensitivity1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Pressure_Sensitivity2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Pressure_Sensitivity3_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Pressure_Sensitivity4_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Pressure_Sensitivity5_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Pressure_Sensitivity6_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Pressure_Sensitivity7_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Pressure_Sensitivity8_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Temperature_Sensitivity1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Temperature_Sensitivity2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Temperature_Sensitivity3_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Temperature_Sensitivity4_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Temperature_Sensitivity5_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Temperature_Sensitivity6_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Temperature_Sensitivity7_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Temperature_Sensitivity8_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Vibration_Sensitivity1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Vibration_Sensitivity2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Vibration_Sensitivity3_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Vibration_Sensitivity4_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Vibration_Sensitivity5_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Vibration_Sensitivity6_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Vibration_Sensitivity7_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void Vibration_Sensitivity8_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_low1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_high1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_low2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_high2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_low3_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_high3_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_low4_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_high4_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_low5_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_high5_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_low6_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_high6_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_low7_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_high7_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_low8_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void PressureRange_high8_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_low1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_high1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_low2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_high2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_low3_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_high3_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_low4_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_high4_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_low5_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_high5_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_low6_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_high6_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_low7_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_high7_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_low8_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void TemperatureRange_high8_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_low1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_high1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_low2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_high2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_low3_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_high3_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_low4_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_high4_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_low5_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_high5_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_low6_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_high6_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_low7_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_high7_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_low8_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }

        private void VibrationRange_high8_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextboxLimit(sender, e);
        }
        #endregion
    }
}

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
            this.Close();
        }

        //选择各类传感器的通道号
        private void CheckSensor()
        {
            if (PressureSensor1.Checked == true)
            {
                Data.is_PressureSensor[0] = true;
            }
            if (PressureSensor2.Checked == true)
            {
                Data.is_PressureSensor[1] = true;
            }
            if (PressureSensor3.Checked == true)
            {
                Data.is_PressureSensor[2] = true;
            }
            if (PressureSensor4.Checked == true)
            {
                Data.is_PressureSensor[3] = true;
            }
            if (PressureSensor5.Checked == true)
            {
                Data.is_PressureSensor[4] = true;
            }
            if (PressureSensor6.Checked == true)
            {
                Data.is_PressureSensor[5] = true;
            }
            if (PressureSensor7.Checked == true)
            {
                Data.is_PressureSensor[6] = true;
            }
            if (PressureSensor8.Checked == true)
            {
                Data.is_PressureSensor[7] = true;
            }

            if (Temperature1.Checked == true)
            {
                Data.is_Temperature[0] = true;
            }
            if (Temperature2.Checked == true)
            {
                Data.is_Temperature[1] = true;
            }
            if (Temperature3.Checked == true)
            {
                Data.is_Temperature[2] = true;
            }
            if (Temperature4.Checked == true)
            {
                Data.is_Temperature[3] = true;
            }
            if (Temperature5.Checked == true)
            {
                Data.is_Temperature[4] = true;
            }
            if (Temperature6.Checked == true)
            {
                Data.is_Temperature[5] = true;
            }
            if (Temperature7.Checked == true)
            {
                Data.is_Temperature[6] = true;
            }
            if (Temperature8.Checked == true)
            {
                Data.is_Temperature[7] = true;
            }

            if (Vibration1.Checked == true)
            {
                Data.is_Vibration[0] = true;
            }
            if (Vibration2.Checked == true)
            {
                Data.is_Vibration[1] = true;
            }
            if (Vibration3.Checked == true)
            {
                Data.is_Vibration[2] = true;
            }
            if (Vibration4.Checked == true)
            {
                Data.is_Vibration[3] = true;
            }
            if (Vibration5.Checked == true)
            {
                Data.is_Vibration[4] = true;
            }
            if (Vibration6.Checked == true)
            {
                Data.is_Vibration[5] = true;
            }
            if (Vibration7.Checked == true)
            {
                Data.is_Vibration[6] = true;
            }
            if (Vibration8.Checked == true)
            {
                Data.is_Vibration[7] = true;
            }
        }

        //获取各通道传感器的灵敏度
        private void GetSensitivity()
        {
            Data.Pressure_Sensitivity[0] = float.Parse(Pressure_Sensitivity1.Text);
            Data.Pressure_Sensitivity[1] = float.Parse(Pressure_Sensitivity2.Text);
            Data.Pressure_Sensitivity[2] = float.Parse(Pressure_Sensitivity3.Text);
            Data.Pressure_Sensitivity[3] = float.Parse(Pressure_Sensitivity4.Text);
            Data.Pressure_Sensitivity[4] = float.Parse(Pressure_Sensitivity5.Text);
            Data.Pressure_Sensitivity[5] = float.Parse(Pressure_Sensitivity6.Text);
            Data.Pressure_Sensitivity[6] = float.Parse(Pressure_Sensitivity7.Text);
            Data.Pressure_Sensitivity[7] = float.Parse(Pressure_Sensitivity8.Text);

            Data.Temperature_Sensitivity[0] = float.Parse(Temperature_Sensitivity1.Text);
            Data.Temperature_Sensitivity[1] = float.Parse(Temperature_Sensitivity2.Text);
            Data.Temperature_Sensitivity[2] = float.Parse(Temperature_Sensitivity3.Text);
            Data.Temperature_Sensitivity[3] = float.Parse(Temperature_Sensitivity4.Text);
            Data.Temperature_Sensitivity[4] = float.Parse(Temperature_Sensitivity5.Text);
            Data.Temperature_Sensitivity[5] = float.Parse(Temperature_Sensitivity6.Text);
            Data.Temperature_Sensitivity[6] = float.Parse(Temperature_Sensitivity7.Text);
            Data.Temperature_Sensitivity[7] = float.Parse(Temperature_Sensitivity8.Text);

            Data.Vibration_Sensitivity[0] = float.Parse(Vibration_Sensitivity1.Text);
            Data.Vibration_Sensitivity[1] = float.Parse(Vibration_Sensitivity2.Text);
            Data.Vibration_Sensitivity[2] = float.Parse(Vibration_Sensitivity3.Text);
            Data.Vibration_Sensitivity[3] = float.Parse(Vibration_Sensitivity4.Text);
            Data.Vibration_Sensitivity[4] = float.Parse(Vibration_Sensitivity5.Text);
            Data.Vibration_Sensitivity[5] = float.Parse(Vibration_Sensitivity6.Text);
            Data.Vibration_Sensitivity[6] = float.Parse(Vibration_Sensitivity7.Text);
            Data.Vibration_Sensitivity[7] = float.Parse(Vibration_Sensitivity8.Text);
        }


        #region  //设置灵敏度textbox中只能输  "-",数字,"." 

        private void Pressure_Sensitivity1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Char.IsControl(e.KeyChar))
            //    return;
            //if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
            //    return;
            //if (Char.IsDigit(e.KeyChar) && ((e.KeyChar & 0xFF) == e.KeyChar))
            //    return;
            //if (e.KeyChar == 46)
            //{
            //    if (Pressure_Sensitivity1.Text.Split('.').Length < 2)
            //        return;
            //}
            //e.Handled = true;
            //数字
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0)) e.Handled = true;
            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0) e.Handled = true;
        }

        private void Pressure_Sensitivity2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Pressure_Sensitivity3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Pressure_Sensitivity4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Pressure_Sensitivity5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Pressure_Sensitivity6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Pressure_Sensitivity7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Pressure_Sensitivity8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Temperature_Sensitivity1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Temperature_Sensitivity2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Temperature_Sensitivity3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Temperature_Sensitivity4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Temperature_Sensitivity5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Temperature_Sensitivity6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Temperature_Sensitivity7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Temperature_Sensitivity8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Vibration_Sensitivity1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Vibration_Sensitivity2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Vibration_Sensitivity3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Vibration_Sensitivity4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Vibration_Sensitivity5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Vibration_Sensitivity6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Vibration_Sensitivity7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        private void Vibration_Sensitivity8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;

            //输入为小数点时，只能输入一次且只能输入一次
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }

        #endregion
    }
}

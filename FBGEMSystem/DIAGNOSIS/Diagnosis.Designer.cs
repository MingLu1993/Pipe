namespace FBGEMSystem
{
    partial class Diagnosis
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label42 = new System.Windows.Forms.Label();
            this.textBox_T = new System.Windows.Forms.TextBox();
            this.textBox_newsamples = new System.Windows.Forms.TextBox();
            this.button_learn = new System.Windows.Forms.Button();
            this.button_chooseNew = new System.Windows.Forms.Button();
            this.openFileDialog_sample = new System.Windows.Forms.OpenFileDialog();
            this.button_TOK = new System.Windows.Forms.Button();
            this.button_ReadSample = new System.Windows.Forms.Button();
            this.button_test = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label42.Location = new System.Drawing.Point(33, 71);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(192, 16);
            this.label42.TabIndex = 7;
            this.label42.Text = "设置增量学习迭代次数T：";
            // 
            // textBox_T
            // 
            this.textBox_T.Location = new System.Drawing.Point(231, 71);
            this.textBox_T.Name = "textBox_T";
            this.textBox_T.Size = new System.Drawing.Size(81, 21);
            this.textBox_T.TabIndex = 5;
            this.textBox_T.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_T_KeyPress);
            // 
            // textBox_newsamples
            // 
            this.textBox_newsamples.Location = new System.Drawing.Point(167, 146);
            this.textBox_newsamples.Name = "textBox_newsamples";
            this.textBox_newsamples.Size = new System.Drawing.Size(277, 21);
            this.textBox_newsamples.TabIndex = 6;
            // 
            // button_learn
            // 
            this.button_learn.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_learn.Location = new System.Drawing.Point(36, 232);
            this.button_learn.Name = "button_learn";
            this.button_learn.Size = new System.Drawing.Size(115, 32);
            this.button_learn.TabIndex = 3;
            this.button_learn.Text = "增量学习";
            this.button_learn.UseVisualStyleBackColor = true;
            this.button_learn.Click += new System.EventHandler(this.button_learn_Click);
            // 
            // button_chooseNew
            // 
            this.button_chooseNew.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_chooseNew.Location = new System.Drawing.Point(34, 140);
            this.button_chooseNew.Name = "button_chooseNew";
            this.button_chooseNew.Size = new System.Drawing.Size(115, 30);
            this.button_chooseNew.TabIndex = 4;
            this.button_chooseNew.Text = "选择新样本集";
            this.button_chooseNew.UseVisualStyleBackColor = true;
            this.button_chooseNew.Click += new System.EventHandler(this.button_chooseNew_Click);
            // 
            // openFileDialog_sample
            // 
            this.openFileDialog_sample.FileName = "openFileDialog1";
            // 
            // button_TOK
            // 
            this.button_TOK.Location = new System.Drawing.Point(369, 69);
            this.button_TOK.Name = "button_TOK";
            this.button_TOK.Size = new System.Drawing.Size(75, 23);
            this.button_TOK.TabIndex = 8;
            this.button_TOK.Text = "确定";
            this.button_TOK.UseVisualStyleBackColor = true;
            this.button_TOK.Click += new System.EventHandler(this.button_TOK_Click);
            // 
            // button_ReadSample
            // 
            this.button_ReadSample.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_ReadSample.Location = new System.Drawing.Point(34, 183);
            this.button_ReadSample.Name = "button_ReadSample";
            this.button_ReadSample.Size = new System.Drawing.Size(117, 35);
            this.button_ReadSample.TabIndex = 8;
            this.button_ReadSample.Text = "读取样本集";
            this.button_ReadSample.UseVisualStyleBackColor = true;
            this.button_ReadSample.Click += new System.EventHandler(this.button_ReadSample_Click);
            // 
            // button_test
            // 
            this.button_test.Location = new System.Drawing.Point(712, 232);
            this.button_test.Name = "button_test";
            this.button_test.Size = new System.Drawing.Size(75, 23);
            this.button_test.TabIndex = 9;
            this.button_test.Text = "训练测试";
            this.button_test.UseVisualStyleBackColor = true;
            this.button_test.Click += new System.EventHandler(this.button_test_Click);
            // 
            // Diagnosis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 354);
            this.Controls.Add(this.button_test);
            this.Controls.Add(this.button_ReadSample);
            this.Controls.Add(this.button_TOK);
            this.Controls.Add(this.label42);
            this.Controls.Add(this.textBox_T);
            this.Controls.Add(this.textBox_newsamples);
            this.Controls.Add(this.button_learn);
            this.Controls.Add(this.button_chooseNew);
            this.Name = "Diagnosis";
            this.Text = "故障诊断增量训练";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label42;
        public System.Windows.Forms.TextBox textBox_T;
        public System.Windows.Forms.TextBox textBox_newsamples;
        private System.Windows.Forms.Button button_learn;
        private System.Windows.Forms.Button button_chooseNew;
        private System.Windows.Forms.OpenFileDialog openFileDialog_sample;
        private System.Windows.Forms.Button button_TOK;
        private System.Windows.Forms.Button button_ReadSample;
        private System.Windows.Forms.Button button_test;
    }
}
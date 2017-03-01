namespace FBGEMSystem
{
    partial class Analysis
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
            this.components = new System.ComponentModel.Container();
            this.comboBox_CH = new System.Windows.Forms.ComboBox();
            this.comboBox_Point = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.zedGraph_FFT = new ZedGraph.ZedGraphControl();
            this.zedGraph_Time = new ZedGraph.ZedGraphControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.zedGraph_IPCurve = new ZedGraph.ZedGraphControl();
            this.zedGraph_IPScatter = new ZedGraph.ZedGraphControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox_d_f = new System.Windows.Forms.TextBox();
            this.textBox_d_alpha = new System.Windows.Forms.TextBox();
            this.textBox_alpha0 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label_d_f = new System.Windows.Forms.Label();
            this.label_d_alpha = new System.Windows.Forms.Label();
            this.label_alpha0 = new System.Windows.Forms.Label();
            this.zedGraph_f = new ZedGraph.ZedGraphControl();
            this.zedGraph_tq = new ZedGraph.ZedGraphControl();
            this.zedGraph_Hq = new ZedGraph.ZedGraphControl();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_CH
            // 
            this.comboBox_CH.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_CH.FormattingEnabled = true;
            this.comboBox_CH.Location = new System.Drawing.Point(334, 16);
            this.comboBox_CH.Name = "comboBox_CH";
            this.comboBox_CH.Size = new System.Drawing.Size(121, 24);
            this.comboBox_CH.TabIndex = 0;
            this.comboBox_CH.SelectedIndexChanged += new System.EventHandler(this.comboBox_CH_SelectedIndexChanged);
            // 
            // comboBox_Point
            // 
            this.comboBox_Point.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_Point.FormattingEnabled = true;
            this.comboBox_Point.Location = new System.Drawing.Point(706, 17);
            this.comboBox_Point.Name = "comboBox_Point";
            this.comboBox_Point.Size = new System.Drawing.Size(121, 24);
            this.comboBox_Point.TabIndex = 0;
            this.comboBox_Point.SelectedIndexChanged += new System.EventHandler(this.comboBox_Point_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(592, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "选择测点：";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(12, 58);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1353, 611);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.zedGraph_FFT);
            this.tabPage3.Controls.Add(this.zedGraph_Time);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1345, 585);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "时频域波形";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // zedGraph_FFT
            // 
            this.zedGraph_FFT.Location = new System.Drawing.Point(654, 70);
            this.zedGraph_FFT.Name = "zedGraph_FFT";
            this.zedGraph_FFT.ScrollGrace = 0D;
            this.zedGraph_FFT.ScrollMaxX = 0D;
            this.zedGraph_FFT.ScrollMaxY = 0D;
            this.zedGraph_FFT.ScrollMaxY2 = 0D;
            this.zedGraph_FFT.ScrollMinX = 0D;
            this.zedGraph_FFT.ScrollMinY = 0D;
            this.zedGraph_FFT.ScrollMinY2 = 0D;
            this.zedGraph_FFT.Size = new System.Drawing.Size(561, 405);
            this.zedGraph_FFT.TabIndex = 1;
            // 
            // zedGraph_Time
            // 
            this.zedGraph_Time.Location = new System.Drawing.Point(58, 70);
            this.zedGraph_Time.Name = "zedGraph_Time";
            this.zedGraph_Time.ScrollGrace = 0D;
            this.zedGraph_Time.ScrollMaxX = 0D;
            this.zedGraph_Time.ScrollMaxY = 0D;
            this.zedGraph_Time.ScrollMaxY2 = 0D;
            this.zedGraph_Time.ScrollMinX = 0D;
            this.zedGraph_Time.ScrollMinY = 0D;
            this.zedGraph_Time.ScrollMinY2 = 0D;
            this.zedGraph_Time.Size = new System.Drawing.Size(560, 405);
            this.zedGraph_Time.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.zedGraph_IPCurve);
            this.tabPage1.Controls.Add(this.zedGraph_IPScatter);
            this.tabPage1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1345, 585);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "   瞬时相位分析   ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // zedGraph_IPCurve
            // 
            this.zedGraph_IPCurve.Location = new System.Drawing.Point(38, 69);
            this.zedGraph_IPCurve.Margin = new System.Windows.Forms.Padding(4);
            this.zedGraph_IPCurve.Name = "zedGraph_IPCurve";
            this.zedGraph_IPCurve.ScrollGrace = 0D;
            this.zedGraph_IPCurve.ScrollMaxX = 0D;
            this.zedGraph_IPCurve.ScrollMaxY = 0D;
            this.zedGraph_IPCurve.ScrollMaxY2 = 0D;
            this.zedGraph_IPCurve.ScrollMinX = 0D;
            this.zedGraph_IPCurve.ScrollMinY = 0D;
            this.zedGraph_IPCurve.ScrollMinY2 = 0D;
            this.zedGraph_IPCurve.Size = new System.Drawing.Size(606, 417);
            this.zedGraph_IPCurve.TabIndex = 1;
            // 
            // zedGraph_IPScatter
            // 
            this.zedGraph_IPScatter.Location = new System.Drawing.Point(729, 69);
            this.zedGraph_IPScatter.Margin = new System.Windows.Forms.Padding(4);
            this.zedGraph_IPScatter.Name = "zedGraph_IPScatter";
            this.zedGraph_IPScatter.ScrollGrace = 0D;
            this.zedGraph_IPScatter.ScrollMaxX = 0D;
            this.zedGraph_IPScatter.ScrollMaxY = 0D;
            this.zedGraph_IPScatter.ScrollMaxY2 = 0D;
            this.zedGraph_IPScatter.ScrollMinX = 0D;
            this.zedGraph_IPScatter.ScrollMinY = 0D;
            this.zedGraph_IPScatter.ScrollMinY2 = 0D;
            this.zedGraph_IPScatter.Size = new System.Drawing.Size(579, 417);
            this.zedGraph_IPScatter.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox_d_f);
            this.tabPage2.Controls.Add(this.textBox_d_alpha);
            this.tabPage2.Controls.Add(this.textBox_alpha0);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label_d_f);
            this.tabPage2.Controls.Add(this.label_d_alpha);
            this.tabPage2.Controls.Add(this.label_alpha0);
            this.tabPage2.Controls.Add(this.zedGraph_f);
            this.tabPage2.Controls.Add(this.zedGraph_tq);
            this.tabPage2.Controls.Add(this.zedGraph_Hq);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1345, 585);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "MFDFA";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox_d_f
            // 
            this.textBox_d_f.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_d_f.Location = new System.Drawing.Point(758, 447);
            this.textBox_d_f.Name = "textBox_d_f";
            this.textBox_d_f.Size = new System.Drawing.Size(83, 29);
            this.textBox_d_f.TabIndex = 10;
            // 
            // textBox_d_alpha
            // 
            this.textBox_d_alpha.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_d_alpha.Location = new System.Drawing.Point(602, 447);
            this.textBox_d_alpha.Name = "textBox_d_alpha";
            this.textBox_d_alpha.Size = new System.Drawing.Size(83, 29);
            this.textBox_d_alpha.TabIndex = 9;
            // 
            // textBox_alpha0
            // 
            this.textBox_alpha0.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_alpha0.Location = new System.Drawing.Point(445, 447);
            this.textBox_alpha0.Name = "textBox_alpha0";
            this.textBox_alpha0.Size = new System.Drawing.Size(83, 29);
            this.textBox_alpha0.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(291, 450);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "特征参数：";
            // 
            // label_d_f
            // 
            this.label_d_f.AutoSize = true;
            this.label_d_f.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_d_f.Location = new System.Drawing.Point(713, 450);
            this.label_d_f.Name = "label_d_f";
            this.label_d_f.Size = new System.Drawing.Size(39, 21);
            this.label_d_f.TabIndex = 6;
            this.label_d_f.Text = "Δf=";
            // 
            // label_d_alpha
            // 
            this.label_d_alpha.AutoSize = true;
            this.label_d_alpha.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_d_alpha.Location = new System.Drawing.Point(553, 450);
            this.label_d_alpha.Name = "label_d_alpha";
            this.label_d_alpha.Size = new System.Drawing.Size(43, 21);
            this.label_d_alpha.TabIndex = 5;
            this.label_d_alpha.Text = "Δα=";
            // 
            // label_alpha0
            // 
            this.label_alpha0.AutoSize = true;
            this.label_alpha0.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_alpha0.Location = new System.Drawing.Point(399, 450);
            this.label_alpha0.Name = "label_alpha0";
            this.label_alpha0.Size = new System.Drawing.Size(40, 21);
            this.label_alpha0.TabIndex = 4;
            this.label_alpha0.Text = "α0=";
            // 
            // zedGraph_f
            // 
            this.zedGraph_f.Location = new System.Drawing.Point(886, 83);
            this.zedGraph_f.Name = "zedGraph_f";
            this.zedGraph_f.ScrollGrace = 0D;
            this.zedGraph_f.ScrollMaxX = 0D;
            this.zedGraph_f.ScrollMaxY = 0D;
            this.zedGraph_f.ScrollMaxY2 = 0D;
            this.zedGraph_f.ScrollMinX = 0D;
            this.zedGraph_f.ScrollMinY = 0D;
            this.zedGraph_f.ScrollMinY2 = 0D;
            this.zedGraph_f.Size = new System.Drawing.Size(404, 310);
            this.zedGraph_f.TabIndex = 2;
            // 
            // zedGraph_tq
            // 
            this.zedGraph_tq.Location = new System.Drawing.Point(464, 83);
            this.zedGraph_tq.Name = "zedGraph_tq";
            this.zedGraph_tq.ScrollGrace = 0D;
            this.zedGraph_tq.ScrollMaxX = 0D;
            this.zedGraph_tq.ScrollMaxY = 0D;
            this.zedGraph_tq.ScrollMaxY2 = 0D;
            this.zedGraph_tq.ScrollMinX = 0D;
            this.zedGraph_tq.ScrollMinY = 0D;
            this.zedGraph_tq.ScrollMinY2 = 0D;
            this.zedGraph_tq.Size = new System.Drawing.Size(404, 310);
            this.zedGraph_tq.TabIndex = 1;
            // 
            // zedGraph_Hq
            // 
            this.zedGraph_Hq.Location = new System.Drawing.Point(44, 83);
            this.zedGraph_Hq.Name = "zedGraph_Hq";
            this.zedGraph_Hq.ScrollGrace = 0D;
            this.zedGraph_Hq.ScrollMaxX = 0D;
            this.zedGraph_Hq.ScrollMaxY = 0D;
            this.zedGraph_Hq.ScrollMaxY2 = 0D;
            this.zedGraph_Hq.ScrollMinX = 0D;
            this.zedGraph_Hq.ScrollMinY = 0D;
            this.zedGraph_Hq.ScrollMinY2 = 0D;
            this.zedGraph_Hq.Size = new System.Drawing.Size(404, 310);
            this.zedGraph_Hq.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(221, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择通道：";
            // 
            // Analysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 697);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_Point);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_CH);
            this.Name = "Analysis";
            this.Text = "信号分析";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Analysis_FormClosed);
            this.Load += new System.EventHandler(this.Analysis_Load);
            this.ResizeBegin += new System.EventHandler(this.Analysis_ResizeBegin);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_CH;
        private System.Windows.Forms.ComboBox comboBox_Point;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ZedGraph.ZedGraphControl zedGraph_IPCurve;
        private ZedGraph.ZedGraphControl zedGraph_IPScatter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private ZedGraph.ZedGraphControl zedGraph_Time;
        private ZedGraph.ZedGraphControl zedGraph_FFT;
        private ZedGraph.ZedGraphControl zedGraph_f;
        private ZedGraph.ZedGraphControl zedGraph_tq;
        private ZedGraph.ZedGraphControl zedGraph_Hq;
        private System.Windows.Forms.Label label_d_f;
        private System.Windows.Forms.Label label_d_alpha;
        private System.Windows.Forms.Label label_alpha0;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_d_f;
        private System.Windows.Forms.TextBox textBox_d_alpha;
        private System.Windows.Forms.TextBox textBox_alpha0;
    }
}
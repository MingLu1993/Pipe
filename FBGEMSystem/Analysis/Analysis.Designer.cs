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
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.comboBox_ad = new System.Windows.Forms.ComboBox();
            this.comboBox_db = new System.Windows.Forms.ComboBox();
            this.comboBox_wavelet = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.设置小波基 = new System.Windows.Forms.Label();
            this.zedGraph_a7 = new ZedGraph.ZedGraphControl();
            this.zedGraph_a6 = new ZedGraph.ZedGraphControl();
            this.zedGraph_a5 = new ZedGraph.ZedGraphControl();
            this.zedGraph_a4 = new ZedGraph.ZedGraphControl();
            this.zedGraph_a3 = new ZedGraph.ZedGraphControl();
            this.zedGraph_a2 = new ZedGraph.ZedGraphControl();
            this.zedGraph_a1 = new ZedGraph.ZedGraphControl();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.ItemSize = new System.Drawing.Size(96, 25);
            this.tabControl1.Location = new System.Drawing.Point(12, 58);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1353, 611);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.zedGraph_FFT);
            this.tabPage3.Controls.Add(this.zedGraph_Time);
            this.tabPage3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1345, 578);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "时频域波形";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // zedGraph_FFT
            // 
            this.zedGraph_FFT.Location = new System.Drawing.Point(654, 31);
            this.zedGraph_FFT.Name = "zedGraph_FFT";
            this.zedGraph_FFT.ScrollGrace = 0D;
            this.zedGraph_FFT.ScrollMaxX = 0D;
            this.zedGraph_FFT.ScrollMaxY = 0D;
            this.zedGraph_FFT.ScrollMaxY2 = 0D;
            this.zedGraph_FFT.ScrollMinX = 0D;
            this.zedGraph_FFT.ScrollMinY = 0D;
            this.zedGraph_FFT.ScrollMinY2 = 0D;
            this.zedGraph_FFT.Size = new System.Drawing.Size(561, 444);
            this.zedGraph_FFT.TabIndex = 1;
            // 
            // zedGraph_Time
            // 
            this.zedGraph_Time.Location = new System.Drawing.Point(58, 31);
            this.zedGraph_Time.Name = "zedGraph_Time";
            this.zedGraph_Time.ScrollGrace = 0D;
            this.zedGraph_Time.ScrollMaxX = 0D;
            this.zedGraph_Time.ScrollMaxY = 0D;
            this.zedGraph_Time.ScrollMaxY2 = 0D;
            this.zedGraph_Time.ScrollMinX = 0D;
            this.zedGraph_Time.ScrollMinY = 0D;
            this.zedGraph_Time.ScrollMinY2 = 0D;
            this.zedGraph_Time.Size = new System.Drawing.Size(560, 444);
            this.zedGraph_Time.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.zedGraph_IPCurve);
            this.tabPage1.Controls.Add(this.zedGraph_IPScatter);
            this.tabPage1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1345, 578);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "瞬时相位分析";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // zedGraph_IPCurve
            // 
            this.zedGraph_IPCurve.Location = new System.Drawing.Point(38, 42);
            this.zedGraph_IPCurve.Margin = new System.Windows.Forms.Padding(4);
            this.zedGraph_IPCurve.Name = "zedGraph_IPCurve";
            this.zedGraph_IPCurve.ScrollGrace = 0D;
            this.zedGraph_IPCurve.ScrollMaxX = 0D;
            this.zedGraph_IPCurve.ScrollMaxY = 0D;
            this.zedGraph_IPCurve.ScrollMaxY2 = 0D;
            this.zedGraph_IPCurve.ScrollMinX = 0D;
            this.zedGraph_IPCurve.ScrollMinY = 0D;
            this.zedGraph_IPCurve.ScrollMinY2 = 0D;
            this.zedGraph_IPCurve.Size = new System.Drawing.Size(606, 444);
            this.zedGraph_IPCurve.TabIndex = 1;
            // 
            // zedGraph_IPScatter
            // 
            this.zedGraph_IPScatter.Location = new System.Drawing.Point(729, 42);
            this.zedGraph_IPScatter.Margin = new System.Windows.Forms.Padding(4);
            this.zedGraph_IPScatter.Name = "zedGraph_IPScatter";
            this.zedGraph_IPScatter.ScrollGrace = 0D;
            this.zedGraph_IPScatter.ScrollMaxX = 0D;
            this.zedGraph_IPScatter.ScrollMaxY = 0D;
            this.zedGraph_IPScatter.ScrollMaxY2 = 0D;
            this.zedGraph_IPScatter.ScrollMinX = 0D;
            this.zedGraph_IPScatter.ScrollMinY = 0D;
            this.zedGraph_IPScatter.ScrollMinY2 = 0D;
            this.zedGraph_IPScatter.Size = new System.Drawing.Size(579, 444);
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
            this.tabPage2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1345, 578);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "MFDFA";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox_d_f
            // 
            this.textBox_d_f.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_d_f.Location = new System.Drawing.Point(658, 531);
            this.textBox_d_f.Name = "textBox_d_f";
            this.textBox_d_f.Size = new System.Drawing.Size(162, 29);
            this.textBox_d_f.TabIndex = 10;
            // 
            // textBox_d_alpha
            // 
            this.textBox_d_alpha.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_d_alpha.Location = new System.Drawing.Point(659, 476);
            this.textBox_d_alpha.Name = "textBox_d_alpha";
            this.textBox_d_alpha.Size = new System.Drawing.Size(159, 29);
            this.textBox_d_alpha.TabIndex = 9;
            // 
            // textBox_alpha0
            // 
            this.textBox_alpha0.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_alpha0.Location = new System.Drawing.Point(658, 428);
            this.textBox_alpha0.Name = "textBox_alpha0";
            this.textBox_alpha0.Size = new System.Drawing.Size(162, 29);
            this.textBox_alpha0.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(504, 431);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "特征参数：";
            // 
            // label_d_f
            // 
            this.label_d_f.AutoSize = true;
            this.label_d_f.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_d_f.Location = new System.Drawing.Point(613, 534);
            this.label_d_f.Name = "label_d_f";
            this.label_d_f.Size = new System.Drawing.Size(39, 21);
            this.label_d_f.TabIndex = 6;
            this.label_d_f.Text = "Δf=";
            // 
            // label_d_alpha
            // 
            this.label_d_alpha.AutoSize = true;
            this.label_d_alpha.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_d_alpha.Location = new System.Drawing.Point(609, 479);
            this.label_d_alpha.Name = "label_d_alpha";
            this.label_d_alpha.Size = new System.Drawing.Size(43, 21);
            this.label_d_alpha.TabIndex = 5;
            this.label_d_alpha.Text = "Δα=";
            // 
            // label_alpha0
            // 
            this.label_alpha0.AutoSize = true;
            this.label_alpha0.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_alpha0.Location = new System.Drawing.Point(612, 431);
            this.label_alpha0.Name = "label_alpha0";
            this.label_alpha0.Size = new System.Drawing.Size(40, 21);
            this.label_alpha0.TabIndex = 4;
            this.label_alpha0.Text = "α0=";
            // 
            // zedGraph_f
            // 
            this.zedGraph_f.Location = new System.Drawing.Point(886, 16);
            this.zedGraph_f.Name = "zedGraph_f";
            this.zedGraph_f.ScrollGrace = 0D;
            this.zedGraph_f.ScrollMaxX = 0D;
            this.zedGraph_f.ScrollMaxY = 0D;
            this.zedGraph_f.ScrollMaxY2 = 0D;
            this.zedGraph_f.ScrollMinX = 0D;
            this.zedGraph_f.ScrollMinY = 0D;
            this.zedGraph_f.ScrollMinY2 = 0D;
            this.zedGraph_f.Size = new System.Drawing.Size(404, 377);
            this.zedGraph_f.TabIndex = 2;
            // 
            // zedGraph_tq
            // 
            this.zedGraph_tq.Location = new System.Drawing.Point(464, 16);
            this.zedGraph_tq.Name = "zedGraph_tq";
            this.zedGraph_tq.ScrollGrace = 0D;
            this.zedGraph_tq.ScrollMaxX = 0D;
            this.zedGraph_tq.ScrollMaxY = 0D;
            this.zedGraph_tq.ScrollMaxY2 = 0D;
            this.zedGraph_tq.ScrollMinX = 0D;
            this.zedGraph_tq.ScrollMinY = 0D;
            this.zedGraph_tq.ScrollMinY2 = 0D;
            this.zedGraph_tq.Size = new System.Drawing.Size(404, 377);
            this.zedGraph_tq.TabIndex = 1;
            // 
            // zedGraph_Hq
            // 
            this.zedGraph_Hq.Location = new System.Drawing.Point(44, 16);
            this.zedGraph_Hq.Name = "zedGraph_Hq";
            this.zedGraph_Hq.ScrollGrace = 0D;
            this.zedGraph_Hq.ScrollMaxX = 0D;
            this.zedGraph_Hq.ScrollMaxY = 0D;
            this.zedGraph_Hq.ScrollMaxY2 = 0D;
            this.zedGraph_Hq.ScrollMinX = 0D;
            this.zedGraph_Hq.ScrollMinY = 0D;
            this.zedGraph_Hq.ScrollMinY2 = 0D;
            this.zedGraph_Hq.Size = new System.Drawing.Size(404, 377);
            this.zedGraph_Hq.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.comboBox_ad);
            this.tabPage4.Controls.Add(this.comboBox_db);
            this.tabPage4.Controls.Add(this.comboBox_wavelet);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.设置小波基);
            this.tabPage4.Controls.Add(this.zedGraph_a7);
            this.tabPage4.Controls.Add(this.zedGraph_a6);
            this.tabPage4.Controls.Add(this.zedGraph_a5);
            this.tabPage4.Controls.Add(this.zedGraph_a4);
            this.tabPage4.Controls.Add(this.zedGraph_a3);
            this.tabPage4.Controls.Add(this.zedGraph_a2);
            this.tabPage4.Controls.Add(this.zedGraph_a1);
            this.tabPage4.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1345, 578);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "小波分析";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // comboBox_ad
            // 
            this.comboBox_ad.FormattingEnabled = true;
            this.comboBox_ad.Items.AddRange(new object[] {
            "近似系数",
            "细节系数"});
            this.comboBox_ad.Location = new System.Drawing.Point(68, 176);
            this.comboBox_ad.Name = "comboBox_ad";
            this.comboBox_ad.Size = new System.Drawing.Size(121, 24);
            this.comboBox_ad.TabIndex = 16;
            this.comboBox_ad.SelectedIndexChanged += new System.EventHandler(this.comboBox_ad_SelectedIndexChanged);
            // 
            // comboBox_db
            // 
            this.comboBox_db.FormattingEnabled = true;
            this.comboBox_db.Location = new System.Drawing.Point(233, 79);
            this.comboBox_db.Name = "comboBox_db";
            this.comboBox_db.Size = new System.Drawing.Size(121, 24);
            this.comboBox_db.TabIndex = 16;
            this.comboBox_db.SelectedIndexChanged += new System.EventHandler(this.comboBox_db_SelectedIndexChanged);
            // 
            // comboBox_wavelet
            // 
            this.comboBox_wavelet.AutoCompleteCustomSource.AddRange(new string[] {
            "Daubechies",
            "Symlets",
            "haar",
            "Coiflets"});
            this.comboBox_wavelet.FormattingEnabled = true;
            this.comboBox_wavelet.Items.AddRange(new object[] {
            "Daubechies",
            "Symlets",
            "haar",
            "Coiflets"});
            this.comboBox_wavelet.Location = new System.Drawing.Point(68, 79);
            this.comboBox_wavelet.Name = "comboBox_wavelet";
            this.comboBox_wavelet.Size = new System.Drawing.Size(121, 24);
            this.comboBox_wavelet.TabIndex = 15;
            this.comboBox_wavelet.SelectedIndexChanged += new System.EventHandler(this.comboBox1_wavelet_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(64, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 19);
            this.label4.TabIndex = 14;
            this.label4.Text = "选择显示系数";
            // 
            // 设置小波基
            // 
            this.设置小波基.AutoSize = true;
            this.设置小波基.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.设置小波基.Location = new System.Drawing.Point(64, 48);
            this.设置小波基.Name = "设置小波基";
            this.设置小波基.Size = new System.Drawing.Size(104, 19);
            this.设置小波基.TabIndex = 14;
            this.设置小波基.Text = "设置小波基";
            // 
            // zedGraph_a7
            // 
            this.zedGraph_a7.Location = new System.Drawing.Point(1025, 282);
            this.zedGraph_a7.Name = "zedGraph_a7";
            this.zedGraph_a7.ScrollGrace = 0D;
            this.zedGraph_a7.ScrollMaxX = 0D;
            this.zedGraph_a7.ScrollMaxY = 0D;
            this.zedGraph_a7.ScrollMaxY2 = 0D;
            this.zedGraph_a7.ScrollMinX = 0D;
            this.zedGraph_a7.ScrollMinY = 0D;
            this.zedGraph_a7.ScrollMinY2 = 0D;
            this.zedGraph_a7.Size = new System.Drawing.Size(304, 257);
            this.zedGraph_a7.TabIndex = 11;
            // 
            // zedGraph_a6
            // 
            this.zedGraph_a6.Location = new System.Drawing.Point(705, 282);
            this.zedGraph_a6.Name = "zedGraph_a6";
            this.zedGraph_a6.ScrollGrace = 0D;
            this.zedGraph_a6.ScrollMaxX = 0D;
            this.zedGraph_a6.ScrollMaxY = 0D;
            this.zedGraph_a6.ScrollMaxY2 = 0D;
            this.zedGraph_a6.ScrollMinX = 0D;
            this.zedGraph_a6.ScrollMinY = 0D;
            this.zedGraph_a6.ScrollMinY2 = 0D;
            this.zedGraph_a6.Size = new System.Drawing.Size(304, 257);
            this.zedGraph_a6.TabIndex = 10;
            // 
            // zedGraph_a5
            // 
            this.zedGraph_a5.Location = new System.Drawing.Point(381, 282);
            this.zedGraph_a5.Name = "zedGraph_a5";
            this.zedGraph_a5.ScrollGrace = 0D;
            this.zedGraph_a5.ScrollMaxX = 0D;
            this.zedGraph_a5.ScrollMaxY = 0D;
            this.zedGraph_a5.ScrollMaxY2 = 0D;
            this.zedGraph_a5.ScrollMinX = 0D;
            this.zedGraph_a5.ScrollMinY = 0D;
            this.zedGraph_a5.ScrollMinY2 = 0D;
            this.zedGraph_a5.Size = new System.Drawing.Size(304, 257);
            this.zedGraph_a5.TabIndex = 4;
            // 
            // zedGraph_a4
            // 
            this.zedGraph_a4.Location = new System.Drawing.Point(50, 282);
            this.zedGraph_a4.Name = "zedGraph_a4";
            this.zedGraph_a4.ScrollGrace = 0D;
            this.zedGraph_a4.ScrollMaxX = 0D;
            this.zedGraph_a4.ScrollMaxY = 0D;
            this.zedGraph_a4.ScrollMaxY2 = 0D;
            this.zedGraph_a4.ScrollMinX = 0D;
            this.zedGraph_a4.ScrollMinY = 0D;
            this.zedGraph_a4.ScrollMinY2 = 0D;
            this.zedGraph_a4.Size = new System.Drawing.Size(304, 257);
            this.zedGraph_a4.TabIndex = 3;
            // 
            // zedGraph_a3
            // 
            this.zedGraph_a3.Location = new System.Drawing.Point(1025, 16);
            this.zedGraph_a3.Name = "zedGraph_a3";
            this.zedGraph_a3.ScrollGrace = 0D;
            this.zedGraph_a3.ScrollMaxX = 0D;
            this.zedGraph_a3.ScrollMaxY = 0D;
            this.zedGraph_a3.ScrollMaxY2 = 0D;
            this.zedGraph_a3.ScrollMinX = 0D;
            this.zedGraph_a3.ScrollMinY = 0D;
            this.zedGraph_a3.ScrollMinY2 = 0D;
            this.zedGraph_a3.Size = new System.Drawing.Size(304, 236);
            this.zedGraph_a3.TabIndex = 2;
            // 
            // zedGraph_a2
            // 
            this.zedGraph_a2.Location = new System.Drawing.Point(705, 16);
            this.zedGraph_a2.Name = "zedGraph_a2";
            this.zedGraph_a2.ScrollGrace = 0D;
            this.zedGraph_a2.ScrollMaxX = 0D;
            this.zedGraph_a2.ScrollMaxY = 0D;
            this.zedGraph_a2.ScrollMaxY2 = 0D;
            this.zedGraph_a2.ScrollMinX = 0D;
            this.zedGraph_a2.ScrollMinY = 0D;
            this.zedGraph_a2.ScrollMinY2 = 0D;
            this.zedGraph_a2.Size = new System.Drawing.Size(304, 236);
            this.zedGraph_a2.TabIndex = 1;
            // 
            // zedGraph_a1
            // 
            this.zedGraph_a1.Location = new System.Drawing.Point(381, 16);
            this.zedGraph_a1.Name = "zedGraph_a1";
            this.zedGraph_a1.ScrollGrace = 0D;
            this.zedGraph_a1.ScrollMaxX = 0D;
            this.zedGraph_a1.ScrollMaxY = 0D;
            this.zedGraph_a1.ScrollMaxY2 = 0D;
            this.zedGraph_a1.ScrollMinX = 0D;
            this.zedGraph_a1.ScrollMinY = 0D;
            this.zedGraph_a1.ScrollMinY2 = 0D;
            this.zedGraph_a1.Size = new System.Drawing.Size(304, 236);
            this.zedGraph_a1.TabIndex = 0;
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
            this.ClientSize = new System.Drawing.Size(1362, 697);
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
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
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
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label 设置小波基;
        private ZedGraph.ZedGraphControl zedGraph_a7;
        private ZedGraph.ZedGraphControl zedGraph_a6;
        private ZedGraph.ZedGraphControl zedGraph_a5;
        private ZedGraph.ZedGraphControl zedGraph_a4;
        private ZedGraph.ZedGraphControl zedGraph_a3;
        private ZedGraph.ZedGraphControl zedGraph_a2;
        private ZedGraph.ZedGraphControl zedGraph_a1;
        private System.Windows.Forms.ComboBox comboBox_wavelet;
        private System.Windows.Forms.ComboBox comboBox_db;
        private System.Windows.Forms.ComboBox comboBox_ad;
        private System.Windows.Forms.Label label4;
    }
}
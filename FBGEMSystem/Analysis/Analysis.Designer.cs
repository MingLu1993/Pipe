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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.zedGraph_IPCurve = new ZedGraph.ZedGraphControl();
            this.zedGraph_IPScatter = new ZedGraph.ZedGraphControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.zedGraph_IPCurve.Location = new System.Drawing.Point(38, 90);
            this.zedGraph_IPCurve.Margin = new System.Windows.Forms.Padding(4);
            this.zedGraph_IPCurve.Name = "zedGraph_IPCurve";
            this.zedGraph_IPCurve.ScrollGrace = 0D;
            this.zedGraph_IPCurve.ScrollMaxX = 0D;
            this.zedGraph_IPCurve.ScrollMaxY = 0D;
            this.zedGraph_IPCurve.ScrollMaxY2 = 0D;
            this.zedGraph_IPCurve.ScrollMinX = 0D;
            this.zedGraph_IPCurve.ScrollMinY = 0D;
            this.zedGraph_IPCurve.ScrollMinY2 = 0D;
            this.zedGraph_IPCurve.Size = new System.Drawing.Size(591, 396);
            this.zedGraph_IPCurve.TabIndex = 1;
            // 
            // zedGraph_IPScatter
            // 
            this.zedGraph_IPScatter.Location = new System.Drawing.Point(734, 90);
            this.zedGraph_IPScatter.Margin = new System.Windows.Forms.Padding(4);
            this.zedGraph_IPScatter.Name = "zedGraph_IPScatter";
            this.zedGraph_IPScatter.ScrollGrace = 0D;
            this.zedGraph_IPScatter.ScrollMaxX = 0D;
            this.zedGraph_IPScatter.ScrollMaxY = 0D;
            this.zedGraph_IPScatter.ScrollMaxY2 = 0D;
            this.zedGraph_IPScatter.ScrollMinX = 0D;
            this.zedGraph_IPScatter.ScrollMinY = 0D;
            this.zedGraph_IPScatter.ScrollMinY2 = 0D;
            this.zedGraph_IPScatter.Size = new System.Drawing.Size(574, 396);
            this.zedGraph_IPScatter.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1345, 585);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.ClientSize = new System.Drawing.Size(1377, 697);
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
            this.tabPage1.ResumeLayout(false);
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
    }
}
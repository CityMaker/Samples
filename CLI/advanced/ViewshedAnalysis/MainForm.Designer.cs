namespace ViewshedAnalysis
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStopAnalyse = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numHorizontalAngle = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFlyToTargetPoint = new System.Windows.Forms.Button();
            this.endZ = new System.Windows.Forms.TextBox();
            this.endY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.endX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFlyToSourcePoint = new System.Windows.Forms.Button();
            this.startZ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.startY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.startX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartAnalyse = new System.Windows.Forms.Button();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numZOffset = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHorizontalAngle)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(881, 585);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.btnStopAnalyse);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnStartAnalyse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(664, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 579);
            this.panel1.TabIndex = 1;
            // 
            // btnStopAnalyse
            // 
            this.btnStopAnalyse.Location = new System.Drawing.Point(120, 201);
            this.btnStopAnalyse.Name = "btnStopAnalyse";
            this.btnStopAnalyse.Size = new System.Drawing.Size(85, 39);
            this.btnStopAnalyse.TabIndex = 6;
            this.btnStopAnalyse.Text = "停止分析";
            this.btnStopAnalyse.UseVisualStyleBackColor = true;
            this.btnStopAnalyse.Click += new System.EventHandler(this.btnStopAnalyse_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(7, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "按F1显示操作指南";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.numHorizontalAngle);
            this.groupBox3.Location = new System.Drawing.Point(4, 34);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(207, 67);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "视锥张角（度）";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "水平";
            // 
            // numHorizontalAngle
            // 
            this.numHorizontalAngle.Location = new System.Drawing.Point(48, 25);
            this.numHorizontalAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numHorizontalAngle.Name = "numHorizontalAngle";
            this.numHorizontalAngle.Size = new System.Drawing.Size(120, 21);
            this.numHorizontalAngle.TabIndex = 0;
            this.numHorizontalAngle.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnFlyToTargetPoint);
            this.groupBox2.Controls.Add(this.endZ);
            this.groupBox2.Controls.Add(this.endY);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.endX);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(3, 426);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(207, 150);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "目标点";
            // 
            // btnFlyToTargetPoint
            // 
            this.btnFlyToTargetPoint.Location = new System.Drawing.Point(117, 121);
            this.btnFlyToTargetPoint.Name = "btnFlyToTargetPoint";
            this.btnFlyToTargetPoint.Size = new System.Drawing.Size(75, 23);
            this.btnFlyToTargetPoint.TabIndex = 7;
            this.btnFlyToTargetPoint.Text = "飞入目标点";
            this.btnFlyToTargetPoint.UseVisualStyleBackColor = true;
            this.btnFlyToTargetPoint.Click += new System.EventHandler(this.btnFlyToTargetPoint_Click);
            // 
            // endZ
            // 
            this.endZ.Location = new System.Drawing.Point(30, 94);
            this.endZ.Name = "endZ";
            this.endZ.Size = new System.Drawing.Size(147, 21);
            this.endZ.TabIndex = 11;
            // 
            // endY
            // 
            this.endY.Location = new System.Drawing.Point(30, 60);
            this.endY.Name = "endY";
            this.endY.Size = new System.Drawing.Size(147, 21);
            this.endY.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Z:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "X:";
            // 
            // endX
            // 
            this.endX.Location = new System.Drawing.Point(30, 32);
            this.endX.Name = "endX";
            this.endX.Size = new System.Drawing.Size(147, 21);
            this.endX.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Y:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFlyToSourcePoint);
            this.groupBox1.Controls.Add(this.startZ);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.startY);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.startX);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 284);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 138);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "观察点";
            // 
            // btnFlyToSourcePoint
            // 
            this.btnFlyToSourcePoint.Location = new System.Drawing.Point(120, 110);
            this.btnFlyToSourcePoint.Name = "btnFlyToSourcePoint";
            this.btnFlyToSourcePoint.Size = new System.Drawing.Size(75, 23);
            this.btnFlyToSourcePoint.TabIndex = 6;
            this.btnFlyToSourcePoint.Text = "飞入观察点";
            this.btnFlyToSourcePoint.UseVisualStyleBackColor = true;
            this.btnFlyToSourcePoint.Click += new System.EventHandler(this.btnFlyToSourcePoint_Click);
            // 
            // startZ
            // 
            this.startZ.Location = new System.Drawing.Point(31, 83);
            this.startZ.Name = "startZ";
            this.startZ.Size = new System.Drawing.Size(147, 21);
            this.startZ.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Z:";
            // 
            // startY
            // 
            this.startY.Location = new System.Drawing.Point(31, 49);
            this.startY.Name = "startY";
            this.startY.Size = new System.Drawing.Size(147, 21);
            this.startY.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y:";
            // 
            // startX
            // 
            this.startX.Location = new System.Drawing.Point(31, 21);
            this.startX.Name = "startX";
            this.startX.Size = new System.Drawing.Size(147, 21);
            this.startX.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // btnStartAnalyse
            // 
            this.btnStartAnalyse.Location = new System.Drawing.Point(9, 201);
            this.btnStartAnalyse.Name = "btnStartAnalyse";
            this.btnStartAnalyse.Size = new System.Drawing.Size(85, 39);
            this.btnStartAnalyse.TabIndex = 1;
            this.btnStartAnalyse.Text = "开始分析";
            this.btnStartAnalyse.UseVisualStyleBackColor = true;
            this.btnStartAnalyse.Click += new System.EventHandler(this.btnStartAnalyse_Click);
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(655, 579);
            this.axRenderControl1.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.numZOffset);
            this.groupBox4.Location = new System.Drawing.Point(4, 107);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(207, 54);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "观察点z值偏移（米）";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "垂直偏移";
            // 
            // numZOffset
            // 
            this.numZOffset.Location = new System.Drawing.Point(66, 25);
            this.numZOffset.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numZOffset.Name = "numZOffset";
            this.numZOffset.Size = new System.Drawing.Size(102, 21);
            this.numZOffset.TabIndex = 0;
            this.numZOffset.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 585);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewshedAnalysis";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHorizontalAngle)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZOffset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.Button btnStartAnalyse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox endZ;
        private System.Windows.Forms.TextBox endY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox endX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox startZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox startY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox startX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button btnFlyToSourcePoint;
        private System.Windows.Forms.Button btnFlyToTargetPoint;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numHorizontalAngle;
        private System.Windows.Forms.Button btnStopAnalyse;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numZOffset;

    }
}


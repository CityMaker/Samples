namespace ShadowMapApplication
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.trackBarOpacity = new System.Windows.Forms.TrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.colorBox = new System.Windows.Forms.TextBox();
            this.btnChangeColor = new System.Windows.Forms.Button();
            this.comboBoxSunMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnNow = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.numMinute = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numHour = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numMonth = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numYear = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numDay = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.btnStopAnalyse = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numTilt = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numHeading = new System.Windows.Forms.NumericUpDown();
            this.btnStartAnalyse = new System.Windows.Forms.Button();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDay)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTilt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeading)).BeginInit();
            
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(881, 729);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trackBarOpacity);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.colorBox);
            this.panel1.Controls.Add(this.btnChangeColor);
            this.panel1.Controls.Add(this.comboBoxSunMode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.btnStopAnalyse);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnStartAnalyse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(664, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 723);
            this.panel1.TabIndex = 1;
            // 
            // trackBarOpacity
            // 
            this.trackBarOpacity.Location = new System.Drawing.Point(54, 65);
            this.trackBarOpacity.Maximum = 255;
            this.trackBarOpacity.Name = "trackBarOpacity";
            this.trackBarOpacity.Size = new System.Drawing.Size(151, 45);
            this.trackBarOpacity.TabIndex = 20;
            this.trackBarOpacity.Scroll += new System.EventHandler(this.trackBarOpacity_Scroll);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "透明度";
            // 
            // colorBox
            // 
            this.colorBox.Location = new System.Drawing.Point(100, 40);
            this.colorBox.Name = "colorBox";
            this.colorBox.ReadOnly = true;
            this.colorBox.Size = new System.Drawing.Size(100, 21);
            this.colorBox.TabIndex = 13;
            // 
            // btnChangeColor
            // 
            this.btnChangeColor.Location = new System.Drawing.Point(9, 36);
            this.btnChangeColor.Name = "btnChangeColor";
            this.btnChangeColor.Size = new System.Drawing.Size(85, 25);
            this.btnChangeColor.TabIndex = 12;
            this.btnChangeColor.Text = "ChangeColor";
            this.btnChangeColor.UseVisualStyleBackColor = true;
            this.btnChangeColor.Click += new System.EventHandler(this.btnChangeColor_Click);
            // 
            // comboBoxSunMode
            // 
            this.comboBoxSunMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSunMode.FormattingEnabled = true;
            this.comboBoxSunMode.Items.AddRange(new object[] {
            "gviSunModeFollowCamera",
            "gviSunModeAccordingToGMT",
            "gviSunModeUserDefined"});
            this.comboBoxSunMode.Location = new System.Drawing.Point(7, 139);
            this.comboBoxSunMode.Name = "comboBoxSunMode";
            this.comboBoxSunMode.Size = new System.Drawing.Size(155, 20);
            this.comboBoxSunMode.TabIndex = 11;
            this.comboBoxSunMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxSunMode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "SunCalculateMode";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnNow);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.btnPlay);
            this.groupBox2.Controls.Add(this.numMinute);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.numHour);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numMonth);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numYear);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numDay);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(7, 275);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(207, 195);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "GMT UTC时间";
            // 
            // btnNow
            // 
            this.btnNow.Location = new System.Drawing.Point(167, 158);
            this.btnNow.Name = "btnNow";
            this.btnNow.Size = new System.Drawing.Size(31, 31);
            this.btnNow.TabIndex = 15;
            this.btnNow.Text = "Now";
            this.btnNow.UseVisualStyleBackColor = true;
            this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(101, 158);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(43, 31);
            this.btnStop.TabIndex = 14;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(41, 158);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(43, 31);
            this.btnPlay.TabIndex = 12;
            this.btnPlay.Text = "播放";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // numMinute
            // 
            this.numMinute.Location = new System.Drawing.Point(66, 128);
            this.numMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numMinute.Name = "numMinute";
            this.numMinute.Size = new System.Drawing.Size(120, 21);
            this.numMinute.TabIndex = 12;
            this.numMinute.ValueChanged += new System.EventHandler(this.numHour_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "Minute";
            // 
            // numHour
            // 
            this.numHour.Location = new System.Drawing.Point(66, 101);
            this.numHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numHour.Name = "numHour";
            this.numHour.Size = new System.Drawing.Size(120, 21);
            this.numHour.TabIndex = 10;
            this.numHour.ValueChanged += new System.EventHandler(this.numHour_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "Hour";
            // 
            // numMonth
            // 
            this.numMonth.Location = new System.Drawing.Point(66, 47);
            this.numMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMonth.Name = "numMonth";
            this.numMonth.Size = new System.Drawing.Size(120, 21);
            this.numMonth.TabIndex = 8;
            this.numMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMonth.ValueChanged += new System.EventHandler(this.numHour_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Month";
            // 
            // numYear
            // 
            this.numYear.Location = new System.Drawing.Point(66, 20);
            this.numYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numYear.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numYear.Name = "numYear";
            this.numYear.Size = new System.Drawing.Size(120, 21);
            this.numYear.TabIndex = 6;
            this.numYear.Value = new decimal(new int[] {
            2013,
            0,
            0,
            0});
            this.numYear.ValueChanged += new System.EventHandler(this.numHour_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Year";
            // 
            // numDay
            // 
            this.numDay.Location = new System.Drawing.Point(66, 74);
            this.numDay.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.numDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDay.Name = "numDay";
            this.numDay.Size = new System.Drawing.Size(120, 21);
            this.numDay.TabIndex = 4;
            this.numDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDay.ValueChanged += new System.EventHandler(this.numHour_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "Day";
            // 
            // btnStopAnalyse
            // 
            this.btnStopAnalyse.Location = new System.Drawing.Point(108, 494);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numTilt);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numHeading);
            this.groupBox1.Location = new System.Drawing.Point(7, 179);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 80);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SunDirection";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "tilt";
            // 
            // numTilt
            // 
            this.numTilt.Location = new System.Drawing.Point(66, 47);
            this.numTilt.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numTilt.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.numTilt.Name = "numTilt";
            this.numTilt.Size = new System.Drawing.Size(120, 21);
            this.numTilt.TabIndex = 2;
            this.numTilt.Value = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.numTilt.ValueChanged += new System.EventHandler(this.numSunDirection_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "heading";
            // 
            // numHeading
            // 
            this.numHeading.Location = new System.Drawing.Point(66, 20);
            this.numHeading.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numHeading.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numHeading.Name = "numHeading";
            this.numHeading.Size = new System.Drawing.Size(120, 21);
            this.numHeading.TabIndex = 0;
            this.numHeading.ValueChanged += new System.EventHandler(this.numSunDirection_ValueChanged);
            // 
            // btnStartAnalyse
            // 
            this.btnStartAnalyse.Location = new System.Drawing.Point(15, 494);
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
            this.axRenderControl1.Size = new System.Drawing.Size(655, 723);
            this.axRenderControl1.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 729);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShadowMapApplication";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDay)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTilt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeading)).EndInit();
            
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.Button btnStartAnalyse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numHeading;
        private System.Windows.Forms.Button btnStopAnalyse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numTilt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numDay;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxSunMode;
        private System.Windows.Forms.NumericUpDown numMinute;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numHour;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numMonth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnNow;
        private System.Windows.Forms.TextBox colorBox;
        private System.Windows.Forms.Button btnChangeColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TrackBar trackBarOpacity;
        private System.Windows.Forms.Label label10;

    }
}


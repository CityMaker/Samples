namespace DynamicViewshed
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numTurnSpeed = new System.Windows.Forms.NumericUpDown();
            this.cbAutoRepeat = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboxMotionStyle = new System.Windows.Forms.ComboBox();
            this.cbDrawLine = new System.Windows.Forms.CheckBox();
            this.cbFlyMode = new System.Windows.Forms.ComboBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnFlyToObject = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numAspectRatio = new System.Windows.Forms.NumericUpDown();
            this.numFarClip = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numFieldOfView = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.cbShowProjectionLines = new System.Windows.Forms.CheckBox();
            this.cbShowProjector = new System.Windows.Forms.CheckBox();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTurnSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAspectRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFarClip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldOfView)).BeginInit();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(565, 617);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 623);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.cbDrawLine);
            this.panel1.Controls.Add(this.cbFlyMode);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnPause);
            this.panel1.Controls.Add(this.btnFlyToObject);
            this.panel1.Controls.Add(this.btnPlay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(574, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(108, 617);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbShowProjector);
            this.groupBox1.Controls.Add(this.cbShowProjectionLines);
            this.groupBox1.Controls.Add(this.numFieldOfView);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numFarClip);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numAspectRatio);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numTurnSpeed);
            this.groupBox1.Controls.Add(this.cbAutoRepeat);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboxMotionStyle);
            this.groupBox1.Location = new System.Drawing.Point(2, 234);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 338);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数设置";
            // 
            // numTurnSpeed
            // 
            this.numTurnSpeed.Location = new System.Drawing.Point(6, 108);
            this.numTurnSpeed.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numTurnSpeed.Name = "numTurnSpeed";
            this.numTurnSpeed.Size = new System.Drawing.Size(94, 21);
            this.numTurnSpeed.TabIndex = 10;
            this.numTurnSpeed.ValueChanged += new System.EventHandler(this.numTurnSpeed_ValueChanged);
            // 
            // cbAutoRepeat
            // 
            this.cbAutoRepeat.AutoSize = true;
            this.cbAutoRepeat.Checked = true;
            this.cbAutoRepeat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoRepeat.Location = new System.Drawing.Point(4, 27);
            this.cbAutoRepeat.Name = "cbAutoRepeat";
            this.cbAutoRepeat.Size = new System.Drawing.Size(72, 16);
            this.cbAutoRepeat.TabIndex = 6;
            this.cbAutoRepeat.Text = "循环播放";
            this.cbAutoRepeat.UseVisualStyleBackColor = true;
            this.cbAutoRepeat.CheckedChanged += new System.EventHandler(this.cbAutoRepeat_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "TurnSpeed";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "MotionStyle";
            // 
            // comboxMotionStyle
            // 
            this.comboxMotionStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboxMotionStyle.FormattingEnabled = true;
            this.comboxMotionStyle.Items.AddRange(new object[] {
            "GroundVehicle",
            "Airplane",
            "Helicopter",
            "Hover"});
            this.comboxMotionStyle.Location = new System.Drawing.Point(4, 65);
            this.comboxMotionStyle.Name = "comboxMotionStyle";
            this.comboxMotionStyle.Size = new System.Drawing.Size(96, 20);
            this.comboxMotionStyle.TabIndex = 8;
            this.comboxMotionStyle.SelectedIndexChanged += new System.EventHandler(this.comboxMotionStyle_SelectedIndexChanged);
            // 
            // cbDrawLine
            // 
            this.cbDrawLine.AutoSize = true;
            this.cbDrawLine.Checked = true;
            this.cbDrawLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDrawLine.Location = new System.Drawing.Point(8, 592);
            this.cbDrawLine.Name = "cbDrawLine";
            this.cbDrawLine.Size = new System.Drawing.Size(72, 16);
            this.cbDrawLine.TabIndex = 5;
            this.cbDrawLine.Text = "显示路线";
            this.cbDrawLine.UseVisualStyleBackColor = true;
            this.cbDrawLine.CheckedChanged += new System.EventHandler(this.cbDrawLine_CheckedChanged);
            // 
            // cbFlyMode
            // 
            this.cbFlyMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFlyMode.FormattingEnabled = true;
            this.cbFlyMode.Items.AddRange(new object[] {
            "正后方跟随",
            "后上方跟随",
            "正上方跟随",
            "正下方跟随 ",
            "左侧跟随",
            "右侧跟随 ",
            "座舱跟随"});
            this.cbFlyMode.Location = new System.Drawing.Point(3, 163);
            this.cbFlyMode.Name = "cbFlyMode";
            this.cbFlyMode.Size = new System.Drawing.Size(96, 20);
            this.cbFlyMode.TabIndex = 4;
            // 
            // btnStop
            // 
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(26, 107);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(43, 29);
            this.btnStop.TabIndex = 3;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(26, 62);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(43, 29);
            this.btnPause.TabIndex = 2;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnFlyToObject
            // 
            this.btnFlyToObject.Location = new System.Drawing.Point(3, 189);
            this.btnFlyToObject.Name = "btnFlyToObject";
            this.btnFlyToObject.Size = new System.Drawing.Size(96, 23);
            this.btnFlyToObject.TabIndex = 1;
            this.btnFlyToObject.Text = "FlyToObject";
            this.btnFlyToObject.UseVisualStyleBackColor = true;
            this.btnFlyToObject.Click += new System.EventHandler(this.btnFlyToObject_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(26, 18);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(43, 29);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "相机纵横比";
            // 
            // numAspectRatio
            // 
            this.numAspectRatio.Location = new System.Drawing.Point(6, 169);
            this.numAspectRatio.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numAspectRatio.Name = "numAspectRatio";
            this.numAspectRatio.Size = new System.Drawing.Size(94, 21);
            this.numAspectRatio.TabIndex = 12;
            this.numAspectRatio.ValueChanged += new System.EventHandler(this.numAspectRatio_ValueChanged);
            // 
            // numFarClip
            // 
            this.numFarClip.Location = new System.Drawing.Point(6, 211);
            this.numFarClip.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numFarClip.Name = "numFarClip";
            this.numFarClip.Size = new System.Drawing.Size(94, 21);
            this.numFarClip.TabIndex = 14;
            this.numFarClip.ValueChanged += new System.EventHandler(this.numFarClip_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "相机远裁距离";
            // 
            // numFieldOfView
            // 
            this.numFieldOfView.Location = new System.Drawing.Point(6, 251);
            this.numFieldOfView.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numFieldOfView.Name = "numFieldOfView";
            this.numFieldOfView.Size = new System.Drawing.Size(94, 21);
            this.numFieldOfView.TabIndex = 16;
            this.numFieldOfView.ValueChanged += new System.EventHandler(this.numFieldOfView_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "相机纵向广角";
            // 
            // cbShowProjectionLines
            // 
            this.cbShowProjectionLines.AutoSize = true;
            this.cbShowProjectionLines.Checked = true;
            this.cbShowProjectionLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowProjectionLines.Location = new System.Drawing.Point(6, 278);
            this.cbShowProjectionLines.Name = "cbShowProjectionLines";
            this.cbShowProjectionLines.Size = new System.Drawing.Size(96, 16);
            this.cbShowProjectionLines.TabIndex = 17;
            this.cbShowProjectionLines.Text = "显示投影射线";
            this.cbShowProjectionLines.UseVisualStyleBackColor = true;
            this.cbShowProjectionLines.CheckedChanged += new System.EventHandler(this.cbShowProjectionLines_CheckedChanged);
            // 
            // cbShowProjector
            // 
            this.cbShowProjector.AutoSize = true;
            this.cbShowProjector.Checked = true;
            this.cbShowProjector.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowProjector.Location = new System.Drawing.Point(6, 300);
            this.cbShowProjector.Name = "cbShowProjector";
            this.cbShowProjector.Size = new System.Drawing.Size(84, 16);
            this.cbShowProjector.TabIndex = 18;
            this.cbShowProjector.Text = "显示放映机";
            this.cbShowProjector.UseVisualStyleBackColor = true;
            this.cbShowProjector.CheckedChanged += new System.EventHandler(this.cbShowProjector_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 623);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DynamicViewshed";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTurnSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAspectRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFarClip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldOfView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFlyToObject;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ComboBox cbFlyMode;
        private System.Windows.Forms.CheckBox cbDrawLine;
        private System.Windows.Forms.CheckBox cbAutoRepeat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboxMotionStyle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numTurnSpeed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numAspectRatio;
        private System.Windows.Forms.NumericUpDown numFarClip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numFieldOfView;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbShowProjectionLines;
        private System.Windows.Forms.CheckBox cbShowProjector;

    }
}


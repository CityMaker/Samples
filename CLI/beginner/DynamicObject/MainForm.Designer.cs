namespace MotionPath
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
            this.btnClose = new System.Windows.Forms.Button();
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
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTurnSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.BackColor = System.Drawing.Color.Transparent;
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.EnableMultiTouch = false;
            this.axRenderControl1.FullScreen = false;
            this.axRenderControl1.InteractMode = Gvitech.CityMaker.RenderControl.gviInteractMode.gviInteractNormal;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.MouseSelectObjectMask = Gvitech.CityMaker.RenderControl.gviMouseSelectObjectMask.gviSelectNone;
            this.axRenderControl1.MouseSnapMode = Gvitech.CityMaker.RenderControl.gviMouseSnapMode.gviMouseSnapDisable;
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(565, 542);
            this.axRenderControl1.TabIndex = 0;
            this.axRenderControl1.UseEarthOrbitManipulator = Gvitech.CityMaker.RenderControl.gviManipulatorMode.gviCityMakerManipulator;
            this.axRenderControl1.Load += new System.EventHandler(this.axRenderControl1_Load);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 548);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
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
            this.panel1.Size = new System.Drawing.Size(108, 542);
            this.panel1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(5, 479);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 41);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "关闭窗口";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numTurnSpeed);
            this.groupBox1.Controls.Add(this.cbAutoRepeat);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboxMotionStyle);
            this.groupBox1.Location = new System.Drawing.Point(2, 234);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 179);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数设置";
            // 
            // numTurnSpeed
            // 
            this.numTurnSpeed.Location = new System.Drawing.Point(6, 141);
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
            this.label2.Location = new System.Drawing.Point(4, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "TurnSpeed";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 64);
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
            this.comboxMotionStyle.Location = new System.Drawing.Point(4, 79);
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
            this.cbDrawLine.Location = new System.Drawing.Point(5, 444);
            this.cbDrawLine.Name = "cbDrawLine";
            this.cbDrawLine.Size = new System.Drawing.Size(72, 16);
            this.cbDrawLine.TabIndex = 5;
            this.cbDrawLine.Text = "显示航线";
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 548);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DynamicObject";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTurnSpeed)).EndInit();
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
        private System.Windows.Forms.Button btnClose;

    }
}


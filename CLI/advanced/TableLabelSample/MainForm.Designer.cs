namespace TableLabelSample
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRotateMotionObject = new System.Windows.Forms.Button();
            this.btnInsertWaypoint = new System.Windows.Forms.Button();
            this.btnComplete = new System.Windows.Forms.Button();
            this.btnMoveMotionObject = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLoadLabelBindOnCar = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnThirdLabel = new System.Windows.Forms.Button();
            this.cbSelectTableLabel = new System.Windows.Forms.CheckBox();
            this.btnSecondLabel = new System.Windows.Forms.Button();
            this.btnFirstLabel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDepthTestMode = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(775, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 667);
            this.panel1.TabIndex = 37;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(8, 610);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 41);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "关闭窗口";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRotateMotionObject);
            this.groupBox3.Controls.Add(this.btnInsertWaypoint);
            this.groupBox3.Controls.Add(this.btnComplete);
            this.groupBox3.Controls.Add(this.btnMoveMotionObject);
            this.groupBox3.Location = new System.Drawing.Point(8, 363);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 222);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "编辑路径";
            // 
            // btnRotateMotionObject
            // 
            this.btnRotateMotionObject.Location = new System.Drawing.Point(7, 74);
            this.btnRotateMotionObject.Name = "btnRotateMotionObject";
            this.btnRotateMotionObject.Size = new System.Drawing.Size(116, 23);
            this.btnRotateMotionObject.TabIndex = 3;
            this.btnRotateMotionObject.Text = "运动物体旋转";
            this.btnRotateMotionObject.UseVisualStyleBackColor = true;
            this.btnRotateMotionObject.Click += new System.EventHandler(this.btnRotateMotionObject_Click);
            // 
            // btnInsertWaypoint
            // 
            this.btnInsertWaypoint.Location = new System.Drawing.Point(7, 125);
            this.btnInsertWaypoint.Name = "btnInsertWaypoint";
            this.btnInsertWaypoint.Size = new System.Drawing.Size(75, 23);
            this.btnInsertWaypoint.TabIndex = 2;
            this.btnInsertWaypoint.Text = "插入路径点";
            this.btnInsertWaypoint.UseVisualStyleBackColor = true;
            this.btnInsertWaypoint.Click += new System.EventHandler(this.btnInsertWaypoint_Click);
            // 
            // btnComplete
            // 
            this.btnComplete.Location = new System.Drawing.Point(7, 172);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(75, 23);
            this.btnComplete.TabIndex = 1;
            this.btnComplete.Text = "完成";
            this.btnComplete.UseVisualStyleBackColor = true;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // btnMoveMotionObject
            // 
            this.btnMoveMotionObject.Location = new System.Drawing.Point(7, 30);
            this.btnMoveMotionObject.Name = "btnMoveMotionObject";
            this.btnMoveMotionObject.Size = new System.Drawing.Size(116, 23);
            this.btnMoveMotionObject.TabIndex = 0;
            this.btnMoveMotionObject.Text = "运动物体平移";
            this.btnMoveMotionObject.UseVisualStyleBackColor = true;
            this.btnMoveMotionObject.Click += new System.EventHandler(this.btnMoveMotionObject_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLoadLabelBindOnCar);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.btnPause);
            this.groupBox2.Controls.Add(this.btnPlay);
            this.groupBox2.Location = new System.Drawing.Point(8, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 115);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "绑在车上的标签";
            // 
            // btnLoadLabelBindOnCar
            // 
            this.btnLoadLabelBindOnCar.Location = new System.Drawing.Point(7, 31);
            this.btnLoadLabelBindOnCar.Name = "btnLoadLabelBindOnCar";
            this.btnLoadLabelBindOnCar.Size = new System.Drawing.Size(75, 23);
            this.btnLoadLabelBindOnCar.TabIndex = 3;
            this.btnLoadLabelBindOnCar.Text = "加载";
            this.btnLoadLabelBindOnCar.UseVisualStyleBackColor = true;
            this.btnLoadLabelBindOnCar.Click += new System.EventHandler(this.btnLoadLabelBindOnCar_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStop.BackgroundImage")));
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStop.Location = new System.Drawing.Point(99, 73);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(24, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPause.BackgroundImage")));
            this.btnPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPause.Location = new System.Drawing.Point(51, 73);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(24, 23);
            this.btnPause.TabIndex = 1;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPlay.BackgroundImage")));
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPlay.Location = new System.Drawing.Point(7, 73);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(24, 23);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbDepthTestMode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnThirdLabel);
            this.groupBox1.Controls.Add(this.cbSelectTableLabel);
            this.groupBox1.Controls.Add(this.btnSecondLabel);
            this.groupBox1.Controls.Add(this.btnFirstLabel);
            this.groupBox1.Location = new System.Drawing.Point(8, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 196);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "静态指示标牌";
            // 
            // btnThirdLabel
            // 
            this.btnThirdLabel.Location = new System.Drawing.Point(7, 72);
            this.btnThirdLabel.Name = "btnThirdLabel";
            this.btnThirdLabel.Size = new System.Drawing.Size(75, 23);
            this.btnThirdLabel.TabIndex = 4;
            this.btnThirdLabel.Text = "ThirdLabel";
            this.btnThirdLabel.UseVisualStyleBackColor = true;
            this.btnThirdLabel.Click += new System.EventHandler(this.btnThirdLabel_Click);
            // 
            // cbSelectTableLabel
            // 
            this.cbSelectTableLabel.AutoSize = true;
            this.cbSelectTableLabel.Location = new System.Drawing.Point(7, 111);
            this.cbSelectTableLabel.Name = "cbSelectTableLabel";
            this.cbSelectTableLabel.Size = new System.Drawing.Size(108, 16);
            this.cbSelectTableLabel.TabIndex = 3;
            this.cbSelectTableLabel.Text = "拾取TableLabel";
            this.cbSelectTableLabel.UseVisualStyleBackColor = true;
            this.cbSelectTableLabel.CheckedChanged += new System.EventHandler(this.cbSelectTableLabel_CheckedChanged);
            // 
            // btnSecondLabel
            // 
            this.btnSecondLabel.Location = new System.Drawing.Point(99, 33);
            this.btnSecondLabel.Name = "btnSecondLabel";
            this.btnSecondLabel.Size = new System.Drawing.Size(85, 23);
            this.btnSecondLabel.TabIndex = 2;
            this.btnSecondLabel.Text = "SecondLabel";
            this.btnSecondLabel.UseVisualStyleBackColor = true;
            this.btnSecondLabel.Click += new System.EventHandler(this.btnSecondLabel_Click);
            // 
            // btnFirstLabel
            // 
            this.btnFirstLabel.Location = new System.Drawing.Point(7, 33);
            this.btnFirstLabel.Name = "btnFirstLabel";
            this.btnFirstLabel.Size = new System.Drawing.Size(75, 23);
            this.btnFirstLabel.TabIndex = 1;
            this.btnFirstLabel.Text = "FirstLabel";
            this.btnFirstLabel.UseVisualStyleBackColor = true;
            this.btnFirstLabel.Click += new System.EventHandler(this.btnFirstLabel_Click);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(992, 673);
            this.tableLayoutPanel1.TabIndex = 38;
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(766, 667);
            this.axRenderControl1.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "DepthTestMode";
            // 
            // cbDepthTestMode
            // 
            this.cbDepthTestMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepthTestMode.FormattingEnabled = true;
            this.cbDepthTestMode.Items.AddRange(new object[] {
            "gviDepthTestEnable",
            "gviDepthTestDisable",
            "gviDepthTestAdvance"});
            this.cbDepthTestMode.Location = new System.Drawing.Point(10, 162);
            this.cbDepthTestMode.Name = "cbDepthTestMode";
            this.cbDepthTestMode.Size = new System.Drawing.Size(174, 20);
            this.cbDepthTestMode.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 673);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TabelLabel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnFirstLabel;
        private System.Windows.Forms.Button btnLoadLabelBindOnCar;
        private System.Windows.Forms.Button btnSecondLabel;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnInsertWaypoint;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnMoveMotionObject;
        private System.Windows.Forms.Button btnRotateMotionObject;
        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.CheckBox cbSelectTableLabel;
        private System.Windows.Forms.Button btnThirdLabel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDepthTestMode;
    }
}
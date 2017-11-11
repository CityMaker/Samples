namespace OverlayLabel
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
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.linkLabel_Height = new System.Windows.Forms.LinkLabel();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.linkLabel_Width = new System.Windows.Forms.LinkLabel();
            this.txtY = new System.Windows.Forms.TextBox();
            this.linkLabel_Y = new System.Windows.Forms.LinkLabel();
            this.txtX = new System.Windows.Forms.TextBox();
            this.linkLabel_X = new System.Windows.Forms.LinkLabel();
            this.linkLabel_ImageName = new System.Windows.Forms.LinkLabel();
            this.numDepth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numRotation = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtImageName = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLabelText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFullScreenLabel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRotation)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 152F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 548);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(527, 542);
            this.axRenderControl1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFullScreenLabel);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.linkLabel_Height);
            this.groupBox1.Controls.Add(this.txtWidth);
            this.groupBox1.Controls.Add(this.linkLabel_Width);
            this.groupBox1.Controls.Add(this.txtY);
            this.groupBox1.Controls.Add(this.linkLabel_Y);
            this.groupBox1.Controls.Add(this.txtX);
            this.groupBox1.Controls.Add(this.linkLabel_X);
            this.groupBox1.Controls.Add(this.linkLabel_ImageName);
            this.groupBox1.Controls.Add(this.numDepth);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numRotation);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtImageName);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtLabelText);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(536, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(146, 542);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(51, 357);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.ReadOnly = true;
            this.txtHeight.Size = new System.Drawing.Size(87, 21);
            this.txtHeight.TabIndex = 19;
            // 
            // linkLabel_Height
            // 
            this.linkLabel_Height.AutoSize = true;
            this.linkLabel_Height.Location = new System.Drawing.Point(6, 360);
            this.linkLabel_Height.Name = "linkLabel_Height";
            this.linkLabel_Height.Size = new System.Drawing.Size(41, 12);
            this.linkLabel_Height.TabIndex = 18;
            this.linkLabel_Height.TabStop = true;
            this.linkLabel_Height.Text = "Height";
            this.linkLabel_Height.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Height_LinkClicked);
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(51, 320);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.ReadOnly = true;
            this.txtWidth.Size = new System.Drawing.Size(87, 21);
            this.txtWidth.TabIndex = 17;
            // 
            // linkLabel_Width
            // 
            this.linkLabel_Width.AutoSize = true;
            this.linkLabel_Width.Location = new System.Drawing.Point(6, 323);
            this.linkLabel_Width.Name = "linkLabel_Width";
            this.linkLabel_Width.Size = new System.Drawing.Size(35, 12);
            this.linkLabel_Width.TabIndex = 16;
            this.linkLabel_Width.TabStop = true;
            this.linkLabel_Width.Text = "Width";
            this.linkLabel_Width.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Width_LinkClicked);
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(51, 283);
            this.txtY.Name = "txtY";
            this.txtY.ReadOnly = true;
            this.txtY.Size = new System.Drawing.Size(87, 21);
            this.txtY.TabIndex = 15;
            // 
            // linkLabel_Y
            // 
            this.linkLabel_Y.AutoSize = true;
            this.linkLabel_Y.Location = new System.Drawing.Point(6, 286);
            this.linkLabel_Y.Name = "linkLabel_Y";
            this.linkLabel_Y.Size = new System.Drawing.Size(11, 12);
            this.linkLabel_Y.TabIndex = 14;
            this.linkLabel_Y.TabStop = true;
            this.linkLabel_Y.Text = "Y";
            this.linkLabel_Y.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Y_LinkClicked);
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(51, 247);
            this.txtX.Name = "txtX";
            this.txtX.ReadOnly = true;
            this.txtX.Size = new System.Drawing.Size(87, 21);
            this.txtX.TabIndex = 13;
            // 
            // linkLabel_X
            // 
            this.linkLabel_X.AutoSize = true;
            this.linkLabel_X.Location = new System.Drawing.Point(6, 250);
            this.linkLabel_X.Name = "linkLabel_X";
            this.linkLabel_X.Size = new System.Drawing.Size(11, 12);
            this.linkLabel_X.TabIndex = 12;
            this.linkLabel_X.TabStop = true;
            this.linkLabel_X.Text = "X";
            this.linkLabel_X.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_X_LinkClicked);
            // 
            // linkLabel_ImageName
            // 
            this.linkLabel_ImageName.AutoSize = true;
            this.linkLabel_ImageName.Location = new System.Drawing.Point(9, 99);
            this.linkLabel_ImageName.Name = "linkLabel_ImageName";
            this.linkLabel_ImageName.Size = new System.Drawing.Size(59, 12);
            this.linkLabel_ImageName.TabIndex = 11;
            this.linkLabel_ImageName.TabStop = true;
            this.linkLabel_ImageName.Text = "ImageName";
            this.linkLabel_ImageName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_ImageName_LinkClicked);
            // 
            // numDepth
            // 
            this.numDepth.Location = new System.Drawing.Point(65, 208);
            this.numDepth.Name = "numDepth";
            this.numDepth.Size = new System.Drawing.Size(62, 21);
            this.numDepth.TabIndex = 10;
            this.numDepth.ValueChanged += new System.EventHandler(this.numDepth_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "Depth";
            // 
            // numRotation
            // 
            this.numRotation.Location = new System.Drawing.Point(65, 175);
            this.numRotation.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numRotation.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numRotation.Name = "numRotation";
            this.numRotation.Size = new System.Drawing.Size(62, 21);
            this.numRotation.TabIndex = 8;
            this.numRotation.ValueChanged += new System.EventHandler(this.numRotation_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Rotation";
            // 
            // txtImageName
            // 
            this.txtImageName.Location = new System.Drawing.Point(6, 114);
            this.txtImageName.Multiline = true;
            this.txtImageName.Name = "txtImageName";
            this.txtImageName.ReadOnly = true;
            this.txtImageName.Size = new System.Drawing.Size(134, 45);
            this.txtImageName.TabIndex = 5;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "左上",
            "左下",
            "左中",
            "右上",
            "右下",
            "右中",
            "中上",
            "正中",
            "中下"});
            this.comboBox1.Location = new System.Drawing.Point(40, 65);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 20);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "PivotAlignment";
            // 
            // txtLabelText
            // 
            this.txtLabelText.Location = new System.Drawing.Point(40, 14);
            this.txtLabelText.Name = "txtLabelText";
            this.txtLabelText.Size = new System.Drawing.Size(100, 21);
            this.txtLabelText.TabIndex = 1;
            this.txtLabelText.TextChanged += new System.EventHandler(this.txtLabelText_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Text";
            // 
            // btnFullScreenLabel
            // 
            this.btnFullScreenLabel.Location = new System.Drawing.Point(6, 397);
            this.btnFullScreenLabel.Name = "btnFullScreenLabel";
            this.btnFullScreenLabel.Size = new System.Drawing.Size(75, 23);
            this.btnFullScreenLabel.TabIndex = 20;
            this.btnFullScreenLabel.Text = "设置全屏";
            this.btnFullScreenLabel.UseVisualStyleBackColor = true;
            this.btnFullScreenLabel.Click += new System.EventHandler(this.btnFullScreenLabel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 548);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OverlayLabel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRotation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLabelText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtImageName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numRotation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numDepth;
        private System.Windows.Forms.LinkLabel linkLabel_ImageName;
        private System.Windows.Forms.LinkLabel linkLabel_X;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.LinkLabel linkLabel_Height;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.LinkLabel linkLabel_Width;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.LinkLabel linkLabel_Y;
        private System.Windows.Forms.Button btnFullScreenLabel;

    }
}


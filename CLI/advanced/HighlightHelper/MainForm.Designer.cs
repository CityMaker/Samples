namespace HighlightHelper
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
            this.btnSetRegion = new System.Windows.Forms.Button();
            this.numMaxZ = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.numMinZ = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.colorBox = new System.Windows.Forms.TextBox();
            this.btnChangeColor = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.centerRadius = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSetSectorRegion = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSetCircleRegion = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numHorizontalAngle = new System.Windows.Forms.NumericUpDown();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarOpacity = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinZ)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHorizontalAngle)).BeginInit();
            
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).BeginInit();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(881, 791);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trackBarOpacity);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSetRegion);
            this.panel1.Controls.Add(this.numMaxZ);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.numMinZ);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.colorBox);
            this.panel1.Controls.Add(this.btnChangeColor);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.btnSetSectorRegion);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnSetCircleRegion);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(664, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 785);
            this.panel1.TabIndex = 1;
            // 
            // btnSetRegion
            // 
            this.btnSetRegion.Location = new System.Drawing.Point(3, 475);
            this.btnSetRegion.Name = "btnSetRegion";
            this.btnSetRegion.Size = new System.Drawing.Size(111, 33);
            this.btnSetRegion.TabIndex = 16;
            this.btnSetRegion.Text = "SetRegion";
            this.btnSetRegion.UseVisualStyleBackColor = true;
            this.btnSetRegion.Click += new System.EventHandler(this.btnSetRegion_Click);
            // 
            // numMaxZ
            // 
            this.numMaxZ.Location = new System.Drawing.Point(47, 136);
            this.numMaxZ.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMaxZ.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numMaxZ.Name = "numMaxZ";
            this.numMaxZ.Size = new System.Drawing.Size(120, 21);
            this.numMaxZ.TabIndex = 14;
            this.numMaxZ.ValueChanged += new System.EventHandler(this.numMaxZ_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 138);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 12);
            this.label14.TabIndex = 13;
            this.label14.Text = "MaxZ";
            // 
            // numMinZ
            // 
            this.numMinZ.Location = new System.Drawing.Point(47, 105);
            this.numMinZ.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMinZ.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numMinZ.Name = "numMinZ";
            this.numMinZ.Size = new System.Drawing.Size(120, 21);
            this.numMinZ.TabIndex = 12;
            this.numMinZ.ValueChanged += new System.EventHandler(this.numMinZ_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 107);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 11;
            this.label13.Text = "MinZ";
            // 
            // colorBox
            // 
            this.colorBox.Location = new System.Drawing.Point(94, 33);
            this.colorBox.Name = "colorBox";
            this.colorBox.ReadOnly = true;
            this.colorBox.Size = new System.Drawing.Size(100, 21);
            this.colorBox.TabIndex = 10;
            // 
            // btnChangeColor
            // 
            this.btnChangeColor.Location = new System.Drawing.Point(3, 29);
            this.btnChangeColor.Name = "btnChangeColor";
            this.btnChangeColor.Size = new System.Drawing.Size(85, 25);
            this.btnChangeColor.TabIndex = 9;
            this.btnChangeColor.Text = "ChangeColor";
            this.btnChangeColor.UseVisualStyleBackColor = true;
            this.btnChangeColor.Click += new System.EventHandler(this.btnChangeColor_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.centerRadius);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Location = new System.Drawing.Point(3, 233);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(210, 55);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "半径（米）";
            // 
            // centerRadius
            // 
            this.centerRadius.Location = new System.Drawing.Point(51, 27);
            this.centerRadius.Name = "centerRadius";
            this.centerRadius.ReadOnly = true;
            this.centerRadius.Size = new System.Drawing.Size(126, 21);
            this.centerRadius.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 12);
            this.label12.TabIndex = 6;
            this.label12.Text = "Radius:";
            // 
            // btnSetSectorRegion
            // 
            this.btnSetSectorRegion.Location = new System.Drawing.Point(3, 335);
            this.btnSetSectorRegion.Name = "btnSetSectorRegion";
            this.btnSetSectorRegion.Size = new System.Drawing.Size(111, 33);
            this.btnSetSectorRegion.TabIndex = 6;
            this.btnSetSectorRegion.Text = "SetSectorRegion";
            this.btnSetSectorRegion.UseVisualStyleBackColor = true;
            this.btnSetSectorRegion.Click += new System.EventHandler(this.btnSetSectorRegion_Click);
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
            // btnSetCircleRegion
            // 
            this.btnSetCircleRegion.Location = new System.Drawing.Point(3, 194);
            this.btnSetCircleRegion.Name = "btnSetCircleRegion";
            this.btnSetCircleRegion.Size = new System.Drawing.Size(111, 33);
            this.btnSetCircleRegion.TabIndex = 7;
            this.btnSetCircleRegion.Text = "SetCircleRegion";
            this.btnSetCircleRegion.UseVisualStyleBackColor = true;
            this.btnSetCircleRegion.Click += new System.EventHandler(this.btnSetCircleRegion_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.numHorizontalAngle);
            this.groupBox3.Location = new System.Drawing.Point(3, 374);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(207, 55);
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
            45,
            0,
            0,
            0});
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(655, 785);
            this.axRenderControl1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "透明度";
            // 
            // trackBarOpacity
            // 
            this.trackBarOpacity.Location = new System.Drawing.Point(54, 60);
            this.trackBarOpacity.Maximum = 255;
            this.trackBarOpacity.Name = "trackBarOpacity";
            this.trackBarOpacity.Size = new System.Drawing.Size(151, 45);
            this.trackBarOpacity.TabIndex = 18;
            this.trackBarOpacity.Scroll += new System.EventHandler(this.trackBarOpacity_Scroll);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 791);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HighlightHelper";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinZ)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHorizontalAngle)).EndInit();
            
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numHorizontalAngle;
        private System.Windows.Forms.Button btnSetSectorRegion;
        private System.Windows.Forms.Button btnSetCircleRegion;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox centerRadius;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnChangeColor;
        private System.Windows.Forms.TextBox colorBox;
        private System.Windows.Forms.NumericUpDown numMinZ;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numMaxZ;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnSetRegion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarOpacity;

    }
}


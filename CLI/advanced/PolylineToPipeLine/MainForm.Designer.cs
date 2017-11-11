namespace PolylineToPipeLine
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
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.trackBarOpacity2 = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBarOpacity1 = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSelSpeCol = new System.Windows.Forms.Button();
            this.btnSelDifCol = new System.Windows.Forms.Button();
            this.SpeColorBox = new System.Windows.Forms.TextBox();
            this.DifColorBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numDeflection = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numInnerRadius = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numOuterRadius = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDeflection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInnerRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOuterRadius)).BeginInit();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.BackColor = System.Drawing.Color.Black;
            this.axRenderControl1.ClipMode = Gvitech.CityMaker.RenderControl.gviClipMode.gviClipBox;
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.EnableMultiTouch = false;
            this.axRenderControl1.FullScreen = false;
            this.axRenderControl1.InteractMode = Gvitech.CityMaker.RenderControl.gviInteractMode.gviInteractNormal;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.MeasurementMode = Gvitech.CityMaker.RenderControl.gviMeasurementMode.gviMeasureAerialDistance;
            this.axRenderControl1.MouseCursor = "";
            this.axRenderControl1.MouseSelectMode = Gvitech.CityMaker.RenderControl.gviMouseSelectMode.gviMouseSelectClick;
            this.axRenderControl1.MouseSelectObjectMask = Gvitech.CityMaker.RenderControl.gviMouseSelectObjectMask.gviSelectAll;
            this.axRenderControl1.MouseSnapMode = Gvitech.CityMaker.RenderControl.gviMouseSnapMode.gviMouseSnapDisable;
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(679, 388);
            this.axRenderControl1.TabIndex = 0;
            this.axRenderControl1.UseEarthOrbitManipulator = Gvitech.CityMaker.RenderControl.gviManipulatorMode.gviCityMakerManipulator;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 526);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 397);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(679, 126);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.trackBarOpacity2);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.trackBarOpacity1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btnSelSpeCol);
            this.tabPage1.Controls.Add(this.btnSelDifCol);
            this.tabPage1.Controls.Add(this.SpeColorBox);
            this.tabPage1.Controls.Add(this.DifColorBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.numDeflection);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.numInnerRadius);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.numOuterRadius);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(671, 100);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "PolylineToPipeLine";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // trackBarOpacity2
            // 
            this.trackBarOpacity2.Location = new System.Drawing.Point(476, 54);
            this.trackBarOpacity2.Maximum = 255;
            this.trackBarOpacity2.Name = "trackBarOpacity2";
            this.trackBarOpacity2.Size = new System.Drawing.Size(134, 45);
            this.trackBarOpacity2.TabIndex = 28;
            this.trackBarOpacity2.Value = 255;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(441, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 27;
            this.label5.Text = "透明度";
            // 
            // trackBarOpacity1
            // 
            this.trackBarOpacity1.Location = new System.Drawing.Point(476, 5);
            this.trackBarOpacity1.Maximum = 255;
            this.trackBarOpacity1.Name = "trackBarOpacity1";
            this.trackBarOpacity1.Size = new System.Drawing.Size(134, 45);
            this.trackBarOpacity1.TabIndex = 26;
            this.trackBarOpacity1.Value = 255;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(441, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "透明度";
            // 
            // btnSelSpeCol
            // 
            this.btnSelSpeCol.Location = new System.Drawing.Point(332, 54);
            this.btnSelSpeCol.Name = "btnSelSpeCol";
            this.btnSelSpeCol.Size = new System.Drawing.Size(31, 19);
            this.btnSelSpeCol.TabIndex = 24;
            this.btnSelSpeCol.Text = "...";
            this.btnSelSpeCol.UseVisualStyleBackColor = true;
            this.btnSelSpeCol.Click += new System.EventHandler(this.btnSelSpeCol_Click);
            // 
            // btnSelDifCol
            // 
            this.btnSelDifCol.Location = new System.Drawing.Point(332, 20);
            this.btnSelDifCol.Name = "btnSelDifCol";
            this.btnSelDifCol.Size = new System.Drawing.Size(31, 19);
            this.btnSelDifCol.TabIndex = 23;
            this.btnSelDifCol.Text = "...";
            this.btnSelDifCol.UseVisualStyleBackColor = true;
            this.btnSelDifCol.Click += new System.EventHandler(this.btnSelDifCol_Click);
            // 
            // SpeColorBox
            // 
            this.SpeColorBox.Location = new System.Drawing.Point(369, 54);
            this.SpeColorBox.Name = "SpeColorBox";
            this.SpeColorBox.ReadOnly = true;
            this.SpeColorBox.Size = new System.Drawing.Size(66, 21);
            this.SpeColorBox.TabIndex = 22;
            this.SpeColorBox.Text = "FFFFFFFF";
            // 
            // DifColorBox
            // 
            this.DifColorBox.Location = new System.Drawing.Point(369, 18);
            this.DifColorBox.Name = "DifColorBox";
            this.DifColorBox.ReadOnly = true;
            this.DifColorBox.Size = new System.Drawing.Size(66, 21);
            this.DifColorBox.TabIndex = 21;
            this.DifColorBox.Text = "FFFFFFFF";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "SpecularColor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(253, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "DiffuseColor";
            // 
            // numDeflection
            // 
            this.numDeflection.DecimalPlaces = 2;
            this.numDeflection.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numDeflection.Location = new System.Drawing.Point(111, 74);
            this.numDeflection.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numDeflection.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numDeflection.Name = "numDeflection";
            this.numDeflection.Size = new System.Drawing.Size(72, 21);
            this.numDeflection.TabIndex = 15;
            this.numDeflection.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "Deflection";
            // 
            // numInnerRadius
            // 
            this.numInnerRadius.DecimalPlaces = 2;
            this.numInnerRadius.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numInnerRadius.Location = new System.Drawing.Point(109, 12);
            this.numInnerRadius.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numInnerRadius.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numInnerRadius.Name = "numInnerRadius";
            this.numInnerRadius.Size = new System.Drawing.Size(72, 21);
            this.numInnerRadius.TabIndex = 13;
            this.numInnerRadius.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "InnerRadius";
            // 
            // numOuterRadius
            // 
            this.numOuterRadius.DecimalPlaces = 2;
            this.numOuterRadius.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numOuterRadius.Location = new System.Drawing.Point(111, 45);
            this.numOuterRadius.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numOuterRadius.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numOuterRadius.Name = "numOuterRadius";
            this.numOuterRadius.Size = new System.Drawing.Size(72, 21);
            this.numOuterRadius.TabIndex = 11;
            this.numOuterRadius.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "OuterRadius";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 526);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ParametricModelling";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDeflection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInnerRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOuterRadius)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.NumericUpDown numOuterRadius;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numInnerRadius;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numDeflection;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SpeColorBox;
        private System.Windows.Forms.TextBox DifColorBox;
        private System.Windows.Forms.Button btnSelDifCol;
        private System.Windows.Forms.Button btnSelSpeCol;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TrackBar trackBarOpacity2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBarOpacity1;
        private System.Windows.Forms.Label label4;

    }
}


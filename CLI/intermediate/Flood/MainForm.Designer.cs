namespace Flood
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtWaterHNow = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSimulate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numWaterHInc = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numWaterHEnd = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numWaterHStart = new System.Windows.Forms.NumericUpDown();
            this.btnOnProcess = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numSampling = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numBufferRadius = new System.Windows.Forms.NumericUpDown();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonLoadTerrain = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSelectWaterSource = new System.Windows.Forms.ToolStripButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWaterHInc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaterHEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaterHStart)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSampling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBufferRadius)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(479, 564);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 570);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtWaterHNow);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSimulate);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.btnOnProcess);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(488, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 564);
            this.panel1.TabIndex = 1;
            // 
            // txtWaterHNow
            // 
            this.txtWaterHNow.Location = new System.Drawing.Point(74, 288);
            this.txtWaterHNow.Name = "txtWaterHNow";
            this.txtWaterHNow.ReadOnly = true;
            this.txtWaterHNow.Size = new System.Drawing.Size(100, 21);
            this.txtWaterHNow.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "当前水位:";
            // 
            // btnSimulate
            // 
            this.btnSimulate.Location = new System.Drawing.Point(110, 236);
            this.btnSimulate.Name = "btnSimulate";
            this.btnSimulate.Size = new System.Drawing.Size(75, 34);
            this.btnSimulate.TabIndex = 5;
            this.btnSimulate.Text = "动态模拟";
            this.btnSimulate.UseVisualStyleBackColor = true;
            this.btnSimulate.Click += new System.EventHandler(this.btnSimulate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.numWaterHInc);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.numWaterHEnd);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.numWaterHStart);
            this.groupBox3.Location = new System.Drawing.Point(3, 115);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(188, 114);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "水位";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "增量";
            // 
            // numWaterHInc
            // 
            this.numWaterHInc.DecimalPlaces = 1;
            this.numWaterHInc.Location = new System.Drawing.Point(82, 82);
            this.numWaterHInc.Name = "numWaterHInc";
            this.numWaterHInc.Size = new System.Drawing.Size(89, 21);
            this.numWaterHInc.TabIndex = 4;
            this.numWaterHInc.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "最终";
            // 
            // numWaterHEnd
            // 
            this.numWaterHEnd.DecimalPlaces = 1;
            this.numWaterHEnd.Location = new System.Drawing.Point(82, 51);
            this.numWaterHEnd.Name = "numWaterHEnd";
            this.numWaterHEnd.Size = new System.Drawing.Size(89, 21);
            this.numWaterHEnd.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "初始";
            // 
            // numWaterHStart
            // 
            this.numWaterHStart.DecimalPlaces = 1;
            this.numWaterHStart.Location = new System.Drawing.Point(82, 20);
            this.numWaterHStart.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numWaterHStart.Name = "numWaterHStart";
            this.numWaterHStart.Size = new System.Drawing.Size(89, 21);
            this.numWaterHStart.TabIndex = 0;
            // 
            // btnOnProcess
            // 
            this.btnOnProcess.Location = new System.Drawing.Point(4, 236);
            this.btnOnProcess.Name = "btnOnProcess";
            this.btnOnProcess.Size = new System.Drawing.Size(75, 34);
            this.btnOnProcess.TabIndex = 4;
            this.btnOnProcess.Text = "开始分析";
            this.btnOnProcess.UseVisualStyleBackColor = true;
            this.btnOnProcess.Click += new System.EventHandler(this.btnOnProcess_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numSampling);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numBufferRadius);
            this.groupBox1.Location = new System.Drawing.Point(3, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 78);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "分析参数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "采样密度";
            // 
            // numSampling
            // 
            this.numSampling.Location = new System.Drawing.Point(82, 51);
            this.numSampling.Name = "numSampling";
            this.numSampling.Size = new System.Drawing.Size(89, 21);
            this.numSampling.TabIndex = 2;
            this.numSampling.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "缓冲区半径";
            // 
            // numBufferRadius
            // 
            this.numBufferRadius.Location = new System.Drawing.Point(82, 20);
            this.numBufferRadius.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numBufferRadius.Name = "numBufferRadius";
            this.numBufferRadius.Size = new System.Drawing.Size(89, 21);
            this.numBufferRadius.TabIndex = 0;
            this.numBufferRadius.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLoadTerrain,
            this.toolStripButtonSelectWaterSource});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(194, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonLoadTerrain
            // 
            this.toolStripButtonLoadTerrain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonLoadTerrain.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLoadTerrain.Image")));
            this.toolStripButtonLoadTerrain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoadTerrain.Name = "toolStripButtonLoadTerrain";
            this.toolStripButtonLoadTerrain.Size = new System.Drawing.Size(60, 22);
            this.toolStripButtonLoadTerrain.Text = "更换地形";
            this.toolStripButtonLoadTerrain.Click += new System.EventHandler(this.toolStripButtonLoadTerrain_Click);
            // 
            // toolStripButtonSelectWaterSource
            // 
            this.toolStripButtonSelectWaterSource.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSelectWaterSource.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSelectWaterSource.Image")));
            this.toolStripButtonSelectWaterSource.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelectWaterSource.Name = "toolStripButtonSelectWaterSource";
            this.toolStripButtonSelectWaterSource.Size = new System.Drawing.Size(72, 22);
            this.toolStripButtonSelectWaterSource.Text = "选择水源点";
            this.toolStripButtonSelectWaterSource.Click += new System.EventHandler(this.toolStripButtonSelectWaterSource_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 570);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Flood";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWaterHInc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaterHEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaterHStart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSampling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBufferRadius)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadTerrain;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelectWaterSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numBufferRadius;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numSampling;
        private System.Windows.Forms.Button btnOnProcess;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numWaterHEnd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numWaterHStart;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numWaterHInc;
        private System.Windows.Forms.Button btnSimulate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWaterHNow;

    }
}


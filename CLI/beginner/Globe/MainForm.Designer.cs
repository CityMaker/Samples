namespace Globe
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
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbGlobe = new System.Windows.Forms.CheckBox();
            this.cbUseEarthOrbitManipulator = new System.Windows.Forms.CheckBox();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 33);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(679, 512);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 548);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbUseEarthOrbitManipulator);
            this.panel1.Controls.Add(this.cbGlobe);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(679, 24);
            this.panel1.TabIndex = 1;
            // 
            // cbGlobe
            // 
            this.cbGlobe.AutoSize = true;
            this.cbGlobe.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbGlobe.Location = new System.Drawing.Point(0, 0);
            this.cbGlobe.Name = "cbGlobe";
            this.cbGlobe.Size = new System.Drawing.Size(72, 24);
            this.cbGlobe.TabIndex = 0;
            this.cbGlobe.Text = "切换成球";
            this.cbGlobe.UseVisualStyleBackColor = true;
            this.cbGlobe.CheckedChanged += new System.EventHandler(this.cbGlobe_CheckedChanged);
            // 
            // cbUseEarthOrbitManipulator
            // 
            this.cbUseEarthOrbitManipulator.AutoSize = true;
            this.cbUseEarthOrbitManipulator.Location = new System.Drawing.Point(78, 4);
            this.cbUseEarthOrbitManipulator.Name = "cbUseEarthOrbitManipulator";
            this.cbUseEarthOrbitManipulator.Size = new System.Drawing.Size(120, 16);
            this.cbUseEarthOrbitManipulator.TabIndex = 1;
            this.cbUseEarthOrbitManipulator.Text = "使用谷歌交互方式";
            this.cbUseEarthOrbitManipulator.UseVisualStyleBackColor = true;
            this.cbUseEarthOrbitManipulator.CheckedChanged += new System.EventHandler(this.cbUseEarthOrbitManipulator_CheckedChanged);
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
            this.Text = "Globe";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbGlobe;
        private System.Windows.Forms.CheckBox cbUseEarthOrbitManipulator;

    }
}


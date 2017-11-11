namespace CalculateTopOnTerrain
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripAreaType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonStartAnalysis = new System.Windows.Forms.ToolStripButton();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(0, 0);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(685, 518);
            this.axRenderControl1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripAreaType,
            this.toolStripButtonStartAnalysis});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(685, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripAreaType
            // 
            this.toolStripAreaType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripAreaType.DropDownWidth = 160;
            this.toolStripAreaType.Items.AddRange(new object[] {
            "缓冲区最高点(Buffer)",
            "多边形最高点(Polygon)"});
            this.toolStripAreaType.Name = "toolStripAreaType";
            this.toolStripAreaType.Size = new System.Drawing.Size(160, 25);
            // 
            // toolStripButtonStartAnalysis
            // 
            this.toolStripButtonStartAnalysis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonStartAnalysis.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStartAnalysis.Image")));
            this.toolStripButtonStartAnalysis.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStartAnalysis.Name = "toolStripButtonStartAnalysis";
            this.toolStripButtonStartAnalysis.Size = new System.Drawing.Size(60, 22);
            this.toolStripButtonStartAnalysis.Text = "开始分析";
            this.toolStripButtonStartAnalysis.Click += new System.EventHandler(this.toolStripButtonStartAnalysis_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 518);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.axRenderControl1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CalculateTopOnTerrain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox toolStripAreaType;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolStripButton toolStripButtonStartAnalysis;

    }
}


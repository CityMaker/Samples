namespace Terrain
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
            this.toolStripDEM = new System.Windows.Forms.ToolStripButton();
            this.toolStripVisible = new System.Windows.Forms.ToolStripButton();
            this.toolStripWKT = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripGetElevationType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripGetElevation = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 43);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(679, 472);
            this.axRenderControl1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDEM,
            this.toolStripVisible,
            this.toolStripWKT,
            this.toolStripSeparator1,
            this.toolStripGetElevationType,
            this.toolStripGetElevation,
            this.toolStripSeparator2,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(685, 40);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDEM
            // 
            this.toolStripDEM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDEM.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDEM.Image")));
            this.toolStripDEM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDEM.Name = "toolStripDEM";
            this.toolStripDEM.Size = new System.Drawing.Size(64, 37);
            this.toolStripDEM.Text = "关闭DEM";
            this.toolStripDEM.Click += new System.EventHandler(this.toolStripDEM_Click);
            // 
            // toolStripVisible
            // 
            this.toolStripVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripVisible.Image = ((System.Drawing.Image)(resources.GetObject("toolStripVisible.Image")));
            this.toolStripVisible.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripVisible.Name = "toolStripVisible";
            this.toolStripVisible.Size = new System.Drawing.Size(60, 22);
            this.toolStripVisible.Text = "隐藏地形";
            this.toolStripVisible.Click += new System.EventHandler(this.toolStripVisible_Click);
            // 
            // toolStripWKT
            // 
            this.toolStripWKT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripWKT.Image = ((System.Drawing.Image)(resources.GetObject("toolStripWKT.Image")));
            this.toolStripWKT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripWKT.Name = "toolStripWKT";
            this.toolStripWKT.Size = new System.Drawing.Size(63, 22);
            this.toolStripWKT.Text = "获取WKT";
            this.toolStripWKT.Click += new System.EventHandler(this.toolStripWKT_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripGetElevationType
            // 
            this.toolStripGetElevationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripGetElevationType.DropDownWidth = 160;
            this.toolStripGetElevationType.Items.AddRange(new object[] {
            "GetElevationFromMemory",
            "GetElevationFromDatabase"});
            this.toolStripGetElevationType.Name = "toolStripGetElevationType";
            this.toolStripGetElevationType.Size = new System.Drawing.Size(160, 25);
            // 
            // toolStripGetElevation
            // 
            this.toolStripGetElevation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripGetElevation.Image = ((System.Drawing.Image)(resources.GetObject("toolStripGetElevation.Image")));
            this.toolStripGetElevation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripGetElevation.Name = "toolStripGetElevation";
            this.toolStripGetElevation.Size = new System.Drawing.Size(60, 22);
            this.toolStripGetElevation.Text = "获取高程";
            this.toolStripGetElevation.Click += new System.EventHandler(this.toolStripGetElevation_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(95, 22);
            this.toolStripLabel1.Text = "地形透明度设置:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 518);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 518);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terrain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }
 

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripDEM;
        private System.Windows.Forms.ToolStripButton toolStripVisible;
        private System.Windows.Forms.ToolStripButton toolStripGetElevation;
        private System.Windows.Forms.ToolStripButton toolStripWKT;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox toolStripGetElevationType;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}


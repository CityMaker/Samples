namespace ExportManager
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripCaptureScreen = new System.Windows.Forms.ToolStripButton();
            this.toolStripExport25D = new System.Windows.Forms.ToolStripButton();
            this.toolStripExportDOM = new System.Windows.Forms.ToolStripButton();
            this.toolStripExportDEM = new System.Windows.Forms.ToolStripButton();
            this.toolStripExportOrthoImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripClearSelection = new System.Windows.Forms.ToolStripButton();
            this.toolStripStreet = new System.Windows.Forms.ToolStripButton();
            this.toolStripExportPanorama = new System.Windows.Forms.ToolStripButton();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 33);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(867, 482);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 685F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(873, 518);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripCaptureScreen,
            this.toolStripExport25D,
            this.toolStripExportDOM,
            this.toolStripExportDEM,
            this.toolStripExportOrthoImage,
            this.toolStripClearSelection,
            this.toolStripStreet,
            this.toolStripExportPanorama});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(873, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripCaptureScreen
            // 
            this.toolStripCaptureScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripCaptureScreen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripCaptureScreen.Image")));
            this.toolStripCaptureScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCaptureScreen.Name = "toolStripCaptureScreen";
            this.toolStripCaptureScreen.Size = new System.Drawing.Size(97, 22);
            this.toolStripCaptureScreen.Text = "CaptureScreen";
            this.toolStripCaptureScreen.Click += new System.EventHandler(this.toolStripCaptureScreen_Click);
            // 
            // toolStripExport25D
            // 
            this.toolStripExport25D.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripExport25D.Image = ((System.Drawing.Image)(resources.GetObject("toolStripExport25D.Image")));
            this.toolStripExport25D.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripExport25D.Name = "toolStripExport25D";
            this.toolStripExport25D.Size = new System.Drawing.Size(73, 22);
            this.toolStripExport25D.Text = "Export25D";
            this.toolStripExport25D.Click += new System.EventHandler(this.toolStripExport25D_Click);
            // 
            // toolStripExportDOM
            // 
            this.toolStripExportDOM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripExportDOM.Image = ((System.Drawing.Image)(resources.GetObject("toolStripExportDOM.Image")));
            this.toolStripExportDOM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripExportDOM.Name = "toolStripExportDOM";
            this.toolStripExportDOM.Size = new System.Drawing.Size(81, 22);
            this.toolStripExportDOM.Text = "ExportDOM";
            this.toolStripExportDOM.Click += new System.EventHandler(this.toolStripExportDOM_Click);
            // 
            // toolStripExportDEM
            // 
            this.toolStripExportDEM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripExportDEM.Image = ((System.Drawing.Image)(resources.GetObject("toolStripExportDEM.Image")));
            this.toolStripExportDEM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripExportDEM.Name = "toolStripExportDEM";
            this.toolStripExportDEM.Size = new System.Drawing.Size(78, 22);
            this.toolStripExportDEM.Text = "ExportDEM";
            this.toolStripExportDEM.Click += new System.EventHandler(this.toolStripExportDEM_Click);
            // 
            // toolStripExportOrthoImage
            // 
            this.toolStripExportOrthoImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripExportOrthoImage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripExportOrthoImage.Image")));
            this.toolStripExportOrthoImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripExportOrthoImage.Name = "toolStripExportOrthoImage";
            this.toolStripExportOrthoImage.Size = new System.Drawing.Size(121, 22);
            this.toolStripExportOrthoImage.Text = "ExportOrthoImage";
            this.toolStripExportOrthoImage.Click += new System.EventHandler(this.toolStripExportOrthoImage_Click);
            // 
            // toolStripClearSelection
            // 
            this.toolStripClearSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripClearSelection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripClearSelection.Image")));
            this.toolStripClearSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripClearSelection.Name = "toolStripClearSelection";
            this.toolStripClearSelection.Size = new System.Drawing.Size(94, 22);
            this.toolStripClearSelection.Text = "ClearSelection";
            this.toolStripClearSelection.Click += new System.EventHandler(this.toolStripClearSelection_Click);
            // 
            // toolStripStreet
            // 
            this.toolStripStreet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStreet.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStreet.Image")));
            this.toolStripStreet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripStreet.Name = "toolStripStreet";
            this.toolStripStreet.Size = new System.Drawing.Size(72, 22);
            this.toolStripStreet.Text = "沿街立面图";
            this.toolStripStreet.Click += new System.EventHandler(this.toolStripStreet_Click);
            // 
            // toolStripExportPanorama
            // 
            this.toolStripExportPanorama.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripExportPanorama.Image = ((System.Drawing.Image)(resources.GetObject("toolStripExportPanorama.Image")));
            this.toolStripExportPanorama.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripExportPanorama.Name = "toolStripExportPanorama";
            this.toolStripExportPanorama.Size = new System.Drawing.Size(109, 22);
            this.toolStripExportPanorama.Text = "ExportPanorama";
            this.toolStripExportPanorama.Click += new System.EventHandler(this.toolStripExportPanorama_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 518);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExportManager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripCaptureScreen;
        private System.Windows.Forms.ToolStripButton toolStripExport25D;
        private System.Windows.Forms.ToolStripButton toolStripExportDOM;
        private System.Windows.Forms.ToolStripButton toolStripExportDEM;
        private System.Windows.Forms.ToolStripButton toolStripExportOrthoImage;
        private System.Windows.Forms.ToolStripButton toolStripClearSelection;
        private System.Windows.Forms.ToolStripButton toolStripStreet;
        private System.Windows.Forms.ToolStripButton toolStripExportPanorama;

    }
}


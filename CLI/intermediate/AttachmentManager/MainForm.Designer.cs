namespace AttachmentManager
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
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.cMenuCatalog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tv_CatalogTree = new System.Windows.Forms.TreeView();
            this.btn_AttachmentManager = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripPan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSelect = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cMenuCatalog
            // 
            this.cMenuCatalog.Name = "cMenuCatalog";
            this.cMenuCatalog.Size = new System.Drawing.Size(61, 4);
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(479, 512);
            this.axRenderControl1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.btn_AttachmentManager);
            this.groupBox1.Controls.Add(this.tv_CatalogTree);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(488, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 512);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "资源目录";
            // 
            // tv_CatalogTree
            // 
            this.tv_CatalogTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tv_CatalogTree.Location = new System.Drawing.Point(3, 45);
            this.tv_CatalogTree.Name = "tv_CatalogTree";
            this.tv_CatalogTree.Size = new System.Drawing.Size(188, 327);
            this.tv_CatalogTree.TabIndex = 0;
            this.tv_CatalogTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tv_CatalogTree_MouseDown);
            // 
            // btn_AttachmentManager
            // 
            this.btn_AttachmentManager.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AttachmentManager.Enabled = false;
            this.btn_AttachmentManager.Location = new System.Drawing.Point(2, 378);
            this.btn_AttachmentManager.Name = "btn_AttachmentManager";
            this.btn_AttachmentManager.Size = new System.Drawing.Size(190, 132);
            this.btn_AttachmentManager.TabIndex = 1;
            this.btn_AttachmentManager.Text = "附件管理器";
            this.btn_AttachmentManager.UseVisualStyleBackColor = true;
            this.btn_AttachmentManager.Click += new System.EventHandler(this.btn_AttachmentManager_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripPan,
            this.toolStripSelect});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(188, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripPan
            // 
            this.toolStripPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripPan.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPan.Image")));
            this.toolStripPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPan.Name = "toolStripPan";
            this.toolStripPan.Size = new System.Drawing.Size(33, 22);
            this.toolStripPan.Text = "Pan";
            this.toolStripPan.Click += new System.EventHandler(this.toolStripPan_Click);
            // 
            // toolStripSelect
            // 
            this.toolStripSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSelect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSelect.Image")));
            this.toolStripSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSelect.Name = "toolStripSelect";
            this.toolStripSelect.Size = new System.Drawing.Size(46, 22);
            this.toolStripSelect.Text = "Select";
            this.toolStripSelect.Click += new System.EventHandler(this.toolStripSelect_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 518);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.TabStop = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 518);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AttachmentManager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ContextMenuStrip cMenuCatalog;
        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripPan;
        private System.Windows.Forms.ToolStripButton toolStripSelect;
        private System.Windows.Forms.Button btn_AttachmentManager;
        private System.Windows.Forms.TreeView tv_CatalogTree;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}


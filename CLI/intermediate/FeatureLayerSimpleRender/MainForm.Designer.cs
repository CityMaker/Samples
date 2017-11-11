namespace FeatureLayerSimpleRender
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
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSetTextRender = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripUnloadTextRender = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSetGeometryRender = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripUnloadGeometryRender = new System.Windows.Forms.ToolStripMenuItem();
            this.exportGeometryRenderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importGeometryRenderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.exportTextRenderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTextRenderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(558, 512);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.48175F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.51825F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listView1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 518);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(567, 3);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(115, 512);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
            this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSetTextRender,
            this.toolStripUnloadTextRender,
            this.exportTextRenderToolStripMenuItem,
            this.importTextRenderToolStripMenuItem,
            this.toolStripSetGeometryRender,
            this.toolStripUnloadGeometryRender,
            this.exportGeometryRenderToolStripMenuItem,
            this.importGeometryRenderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(193, 202);
            this.contextMenuStrip1.Text = "UnloadGeometryRender";
            // 
            // toolStripSetTextRender
            // 
            this.toolStripSetTextRender.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSetTextRender.Name = "toolStripSetTextRender";
            this.toolStripSetTextRender.Size = new System.Drawing.Size(192, 22);
            this.toolStripSetTextRender.Text = "SetTextRender";
            this.toolStripSetTextRender.Click += new System.EventHandler(this.toolStripSetTextRender_Click);
            // 
            // toolStripUnloadTextRender
            // 
            this.toolStripUnloadTextRender.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripUnloadTextRender.Name = "toolStripUnloadTextRender";
            this.toolStripUnloadTextRender.Size = new System.Drawing.Size(192, 22);
            this.toolStripUnloadTextRender.Text = "UnloadTextRender";
            this.toolStripUnloadTextRender.Click += new System.EventHandler(this.toolStripUnloadTextRender_Click);
            // 
            // toolStripSetGeometryRender
            // 
            this.toolStripSetGeometryRender.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSetGeometryRender.Name = "toolStripSetGeometryRender";
            this.toolStripSetGeometryRender.Size = new System.Drawing.Size(192, 22);
            this.toolStripSetGeometryRender.Text = "SetGeometryRender";
            this.toolStripSetGeometryRender.Click += new System.EventHandler(this.toolStripSetGeometryRender_Click);
            // 
            // toolStripUnloadGeometryRender
            // 
            this.toolStripUnloadGeometryRender.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripUnloadGeometryRender.Name = "toolStripUnloadGeometryRender";
            this.toolStripUnloadGeometryRender.Size = new System.Drawing.Size(192, 22);
            this.toolStripUnloadGeometryRender.Text = "UnloadGeometryRender";
            this.toolStripUnloadGeometryRender.Click += new System.EventHandler(this.toolStripUnloadGeometryRender_Click);
            // 
            // exportGeometryRenderToolStripMenuItem
            // 
            this.exportGeometryRenderToolStripMenuItem.Name = "exportGeometryRenderToolStripMenuItem";
            this.exportGeometryRenderToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.exportGeometryRenderToolStripMenuItem.Text = "ExportGeometryRender";
            this.exportGeometryRenderToolStripMenuItem.Click += new System.EventHandler(this.exportGeometryRenderToolStripMenuItem_Click);
            // 
            // importGeometryRenderToolStripMenuItem
            // 
            this.importGeometryRenderToolStripMenuItem.Name = "importGeometryRenderToolStripMenuItem";
            this.importGeometryRenderToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.importGeometryRenderToolStripMenuItem.Text = "ImportGeometryRender";
            this.importGeometryRenderToolStripMenuItem.Click += new System.EventHandler(this.importGeometryRenderToolStripMenuItem_Click);
            // 
            // exportTextRenderToolStripMenuItem
            // 
            this.exportTextRenderToolStripMenuItem.Name = "exportTextRenderToolStripMenuItem";
            this.exportTextRenderToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.exportTextRenderToolStripMenuItem.Text = "ExportTextRender";
            this.exportTextRenderToolStripMenuItem.Click += new System.EventHandler(this.exportTextRenderToolStripMenuItem_Click);
            // 
            // importTextRenderToolStripMenuItem
            // 
            this.importTextRenderToolStripMenuItem.Name = "importTextRenderToolStripMenuItem";
            this.importTextRenderToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.importTextRenderToolStripMenuItem.Text = "ImportTextRender";
            this.importTextRenderToolStripMenuItem.Click += new System.EventHandler(this.importTextRenderToolStripMenuItem_Click);
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
            this.Text = "FeatureLayerSimpleRender";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripSetTextRender;
        private System.Windows.Forms.ToolStripMenuItem toolStripUnloadTextRender;
        private System.Windows.Forms.ToolStripMenuItem toolStripSetGeometryRender;
        private System.Windows.Forms.ToolStripMenuItem toolStripUnloadGeometryRender;
        private System.Windows.Forms.ToolStripMenuItem exportGeometryRenderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importGeometryRenderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTextRenderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importTextRenderToolStripMenuItem;

    }
}


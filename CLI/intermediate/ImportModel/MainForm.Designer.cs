namespace ImportModel
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageLogicTree = new System.Windows.Forms.TabPage();
            this.treeViewLogicTree = new System.Windows.Forms.TreeView();
            this.tabPageCatalogTree = new System.Windows.Forms.TabPage();
            this.treeViewCatalogTree = new System.Windows.Forms.TreeView();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.importModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageLogicTree.SuspendLayout();
            this.tabPageCatalogTree.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(110, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(572, 512);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.69301F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.30698F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 518);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageLogicTree);
            this.tabControl1.Controls.Add(this.tabPageCatalogTree);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(101, 512);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageLogicTree
            // 
            this.tabPageLogicTree.Controls.Add(this.treeViewLogicTree);
            this.tabPageLogicTree.Location = new System.Drawing.Point(4, 21);
            this.tabPageLogicTree.Name = "tabPageLogicTree";
            this.tabPageLogicTree.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLogicTree.Size = new System.Drawing.Size(93, 487);
            this.tabPageLogicTree.TabIndex = 0;
            this.tabPageLogicTree.Text = "LogicGroupTree";
            this.tabPageLogicTree.UseVisualStyleBackColor = true;
            // 
            // treeViewLogicTree
            // 
            this.treeViewLogicTree.CheckBoxes = true;
            this.treeViewLogicTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewLogicTree.Location = new System.Drawing.Point(3, 3);
            this.treeViewLogicTree.Name = "treeViewLogicTree";
            this.treeViewLogicTree.Size = new System.Drawing.Size(87, 481);
            this.treeViewLogicTree.TabIndex = 0;
            this.treeViewLogicTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewLogicTree_AfterCheck);
            this.treeViewLogicTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewLogicTree_MouseDown);
            // 
            // tabPageCatalogTree
            // 
            this.tabPageCatalogTree.Controls.Add(this.treeViewCatalogTree);
            this.tabPageCatalogTree.Location = new System.Drawing.Point(4, 21);
            this.tabPageCatalogTree.Name = "tabPageCatalogTree";
            this.tabPageCatalogTree.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCatalogTree.Size = new System.Drawing.Size(93, 487);
            this.tabPageCatalogTree.TabIndex = 1;
            this.tabPageCatalogTree.Text = "CatalogTree";
            this.tabPageCatalogTree.UseVisualStyleBackColor = true;
            // 
            // treeViewCatalogTree
            // 
            this.treeViewCatalogTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewCatalogTree.Location = new System.Drawing.Point(3, 3);
            this.treeViewCatalogTree.Name = "treeViewCatalogTree";
            this.treeViewCatalogTree.Size = new System.Drawing.Size(87, 481);
            this.treeViewCatalogTree.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importModelToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 26);
            // 
            // importModelToolStripMenuItem
            // 
            this.importModelToolStripMenuItem.Name = "importModelToolStripMenuItem";
            this.importModelToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.importModelToolStripMenuItem.Text = "ImportModel";
            this.importModelToolStripMenuItem.Click += new System.EventHandler(this.importModelToolStripMenuItem_Click);
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
            this.Text = "ImportMDB";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageLogicTree.ResumeLayout(false);
            this.tabPageCatalogTree.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageLogicTree;
        private System.Windows.Forms.TabPage tabPageCatalogTree;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TreeView treeViewLogicTree;
        private System.Windows.Forms.TreeView treeViewCatalogTree;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem importModelToolStripMenuItem;

    }
}


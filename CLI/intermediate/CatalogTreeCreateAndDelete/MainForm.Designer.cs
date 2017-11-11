namespace CatalogTreeCreateAndDelete
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addDatasourceToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.createDataSourceToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createDataSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createFeatureClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDataSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFeatureClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modifyFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.contextMenuStrip4.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 31);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(199, 484);
            this.treeView1.TabIndex = 0;
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.405406F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.5946F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(205, 518);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDatasourceToolStripButton,
            this.createDataSourceToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(205, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addDatasourceToolStripButton
            // 
            this.addDatasourceToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addDatasourceToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("addDatasourceToolStripButton.Image")));
            this.addDatasourceToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addDatasourceToolStripButton.Name = "addDatasourceToolStripButton";
            this.addDatasourceToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.addDatasourceToolStripButton.Text = "AddDatasource";
            this.addDatasourceToolStripButton.Click += new System.EventHandler(this.addDatasourceToolStripButton_Click);
            // 
            // createDataSourceToolStripButton
            // 
            this.createDataSourceToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.createDataSourceToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("createDataSourceToolStripButton.Image")));
            this.createDataSourceToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createDataSourceToolStripButton.Name = "createDataSourceToolStripButton";
            this.createDataSourceToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.createDataSourceToolStripButton.Text = "CreateDataSource";
            this.createDataSourceToolStripButton.Click += new System.EventHandler(this.createDataSourceToolStripButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDataSetToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 26);
            // 
            // createDataSetToolStripMenuItem
            // 
            this.createDataSetToolStripMenuItem.Name = "createDataSetToolStripMenuItem";
            this.createDataSetToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.createDataSetToolStripMenuItem.Text = "CreateDataSet";
            this.createDataSetToolStripMenuItem.Click += new System.EventHandler(this.createDataSetToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFeatureClassToolStripMenuItem,
            this.deleteDataSetToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(188, 48);
            // 
            // createFeatureClassToolStripMenuItem
            // 
            this.createFeatureClassToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.createFeatureClassToolStripMenuItem.Name = "createFeatureClassToolStripMenuItem";
            this.createFeatureClassToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.createFeatureClassToolStripMenuItem.Text = "CreateFeatureClass";
            this.createFeatureClassToolStripMenuItem.Click += new System.EventHandler(this.createFeatureClassToolStripMenuItem_Click);
            // 
            // deleteDataSetToolStripMenuItem
            // 
            this.deleteDataSetToolStripMenuItem.Name = "deleteDataSetToolStripMenuItem";
            this.deleteDataSetToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.deleteDataSetToolStripMenuItem.Text = "DeleteDataSet";
            this.deleteDataSetToolStripMenuItem.Click += new System.EventHandler(this.deleteDataSetToolStripMenuItem_Click);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFieldToolStripMenuItem,
            this.deleteFeatureClassToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(187, 48);
            // 
            // createFieldToolStripMenuItem
            // 
            this.createFieldToolStripMenuItem.Name = "createFieldToolStripMenuItem";
            this.createFieldToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.createFieldToolStripMenuItem.Text = "CreateField";
            this.createFieldToolStripMenuItem.Click += new System.EventHandler(this.createFieldToolStripMenuItem_Click);
            // 
            // deleteFeatureClassToolStripMenuItem
            // 
            this.deleteFeatureClassToolStripMenuItem.Name = "deleteFeatureClassToolStripMenuItem";
            this.deleteFeatureClassToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.deleteFeatureClassToolStripMenuItem.Text = "DeleteFeatureClass";
            this.deleteFeatureClassToolStripMenuItem.Click += new System.EventHandler(this.deleteFeatureClassToolStripMenuItem_Click);
            // 
            // contextMenuStrip4
            // 
            this.contextMenuStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewFieldToolStripMenuItem,
            this.modifyFieldToolStripMenuItem,
            this.deleteFieldToolStripMenuItem});
            this.contextMenuStrip4.Name = "contextMenuStrip4";
            this.contextMenuStrip4.Size = new System.Drawing.Size(153, 92);
            // 
            // modifyFieldToolStripMenuItem
            // 
            this.modifyFieldToolStripMenuItem.Name = "modifyFieldToolStripMenuItem";
            this.modifyFieldToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.modifyFieldToolStripMenuItem.Text = "ModifyField";
            this.modifyFieldToolStripMenuItem.Click += new System.EventHandler(this.modifyFieldToolStripMenuItem_Click);
            // 
            // deleteFieldToolStripMenuItem
            // 
            this.deleteFieldToolStripMenuItem.Name = "deleteFieldToolStripMenuItem";
            this.deleteFieldToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteFieldToolStripMenuItem.Text = "DeleteField";
            this.deleteFieldToolStripMenuItem.Click += new System.EventHandler(this.deleteFieldToolStripMenuItem_Click);
            // 
            // viewFieldToolStripMenuItem
            // 
            this.viewFieldToolStripMenuItem.Name = "viewFieldToolStripMenuItem";
            this.viewFieldToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.viewFieldToolStripMenuItem.Text = "ViewField";
            this.viewFieldToolStripMenuItem.Click += new System.EventHandler(this.viewFieldToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 518);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CatalogTree";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.contextMenuStrip4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem createFeatureClassToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem deleteDataSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createFieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFeatureClassToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip4;
        private System.Windows.Forms.ToolStripMenuItem createDataSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyFieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton createDataSourceToolStripButton;
        private System.Windows.Forms.ToolStripButton addDatasourceToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem viewFieldToolStripMenuItem;


    }
}


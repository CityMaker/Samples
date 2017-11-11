namespace ProjectTree
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.treeView1 = new ProjectTree.TriStateTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSaveProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPackProject = new System.Windows.Forms.ToolStripButton();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemRename = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCreateComplexParticleEffect = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCreateRenderPolyline = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemEditPolyline = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonSelect = new System.Windows.Forms.ToolStripButton();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(141, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(585, 516);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.93491F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.06509F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(729, 522);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.treeView1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(132, 516);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(3, 33);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(126, 480);
            this.treeView1.TabIndex = 2;
            this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "group.png");
            this.imageList1.Images.SetKeyName(1, "node.png");
            this.imageList1.Images.SetKeyName(2, "Tree.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSaveProject,
            this.toolStripButtonPackProject,
            this.toolStripButtonSelect});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(132, 30);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonSaveProject
            // 
            this.toolStripButtonSaveProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSaveProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveProject.Image")));
            this.toolStripButtonSaveProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveProject.Name = "toolStripButtonSaveProject";
            this.toolStripButtonSaveProject.Size = new System.Drawing.Size(60, 27);
            this.toolStripButtonSaveProject.Text = "保存工程";
            this.toolStripButtonSaveProject.Click += new System.EventHandler(this.toolStripButtonSaveProject_Click);
            // 
            // toolStripButtonPackProject
            // 
            this.toolStripButtonPackProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPackProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPackProject.Image")));
            this.toolStripButtonPackProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPackProject.Name = "toolStripButtonPackProject";
            this.toolStripButtonPackProject.Size = new System.Drawing.Size(36, 27);
            this.toolStripButtonPackProject.Text = "打包";
            this.toolStripButtonPackProject.Click += new System.EventHandler(this.toolStripButtonPackProject_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemRename,
            this.ToolStripMenuItemCreateComplexParticleEffect,
            this.ToolStripMenuItemCreateRenderPolyline,
            this.ToolStripMenuItemEditPolyline});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(148, 92);
            // 
            // ToolStripMenuItemRename
            // 
            this.ToolStripMenuItemRename.Name = "ToolStripMenuItemRename";
            this.ToolStripMenuItemRename.Size = new System.Drawing.Size(147, 22);
            this.ToolStripMenuItemRename.Text = "重命名";
            this.ToolStripMenuItemRename.Click += new System.EventHandler(this.ToolStripMenuItemRename_Click);
            // 
            // ToolStripMenuItemCreateComplexParticleEffect
            // 
            this.ToolStripMenuItemCreateComplexParticleEffect.Name = "ToolStripMenuItemCreateComplexParticleEffect";
            this.ToolStripMenuItemCreateComplexParticleEffect.Size = new System.Drawing.Size(147, 22);
            this.ToolStripMenuItemCreateComplexParticleEffect.Text = "创建组合粒子特效";
            this.ToolStripMenuItemCreateComplexParticleEffect.Click += new System.EventHandler(this.ToolStripMenuItemCreateComplexParticleEffect_Click);
            // 
            // ToolStripMenuItemCreateRenderPolyline
            // 
            this.ToolStripMenuItemCreateRenderPolyline.Name = "ToolStripMenuItemCreateRenderPolyline";
            this.ToolStripMenuItemCreateRenderPolyline.Size = new System.Drawing.Size(147, 22);
            this.ToolStripMenuItemCreateRenderPolyline.Text = "创建线段";
            this.ToolStripMenuItemCreateRenderPolyline.Click += new System.EventHandler(this.ToolStripMenuItemCreateRenderPolyline_Click);
            // 
            // ToolStripMenuItemEditPolyline
            // 
            this.ToolStripMenuItemEditPolyline.Name = "ToolStripMenuItemEditPolyline";
            this.ToolStripMenuItemEditPolyline.Size = new System.Drawing.Size(147, 22);
            this.ToolStripMenuItemEditPolyline.Text = "编辑线段";
            this.ToolStripMenuItemEditPolyline.Click += new System.EventHandler(this.ToolStripMenuItemEditPolyline_Click);
            // 
            // toolStripButtonSelect
            // 
            this.toolStripButtonSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSelect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSelect.Image")));
            this.toolStripButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelect.Name = "toolStripButtonSelect";
            this.toolStripButtonSelect.Size = new System.Drawing.Size(36, 21);
            this.toolStripButtonSelect.Text = "拾取";
            this.toolStripButtonSelect.Click += new System.EventHandler(this.toolStripButtonSelect_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 522);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProjectTree";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRename;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCreateComplexParticleEffect;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private TriStateTreeView treeView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveProject;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCreateRenderPolyline;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemEditPolyline;
        private System.Windows.Forms.ToolStripButton toolStripButtonPackProject;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelect;

    }
}


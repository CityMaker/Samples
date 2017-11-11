namespace ImageryLayer
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.图层树 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonLoadTerrain = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddImageryLayer = new System.Windows.Forms.ToolStripButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.图层树.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(139, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(619, 532);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.00263F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.99738F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.图层树, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(761, 538);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // 图层树
            // 
            this.图层树.Controls.Add(this.toolStrip1);
            this.图层树.Controls.Add(this.listView1);
            this.图层树.Dock = System.Windows.Forms.DockStyle.Fill;
            this.图层树.Location = new System.Drawing.Point(3, 3);
            this.图层树.Name = "图层树";
            this.图层树.Size = new System.Drawing.Size(130, 532);
            this.图层树.TabIndex = 1;
            this.图层树.TabStop = false;
            this.图层树.Text = "图层树";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLoadTerrain,
            this.toolStripButtonAddImageryLayer});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(124, 25);
            this.toolStrip1.TabIndex = 3;
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
            // toolStripButtonAddImageryLayer
            // 
            this.toolStripButtonAddImageryLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAddImageryLayer.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddImageryLayer.Image")));
            this.toolStripButtonAddImageryLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddImageryLayer.Name = "toolStripButtonAddImageryLayer";
            this.toolStripButtonAddImageryLayer.Size = new System.Drawing.Size(60, 22);
            this.toolStripButtonAddImageryLayer.Text = "添加影像";
            this.toolStripButtonAddImageryLayer.Click += new System.EventHandler(this.toolStripButtonAddImageryLayer_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.CheckBoxes = true;
            this.listView1.Location = new System.Drawing.Point(3, 45);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(125, 484);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 538);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImageryLayer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.图层树.ResumeLayout(false);
            this.图层树.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.GroupBox 图层树;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadTerrain;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddImageryLayer;

    }
}


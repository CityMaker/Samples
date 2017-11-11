namespace TerrainVideo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonCreateVideo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHideVideo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHideProjectionLines = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.toolStripButtonCreatePoint = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(850, 382);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.00263F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 538F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(856, 538);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 391);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(850, 144);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonCreateVideo,
            this.toolStripButtonHideVideo,
            this.toolStripButtonHideProjectionLines,
            this.toolStripButtonCreatePoint});
            this.toolStrip1.Location = new System.Drawing.Point(0, 14);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(850, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonCreateVideo
            // 
            this.toolStripButtonCreateVideo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCreateVideo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCreateVideo.Image")));
            this.toolStripButtonCreateVideo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreateVideo.Name = "toolStripButtonCreateVideo";
            this.toolStripButtonCreateVideo.Size = new System.Drawing.Size(60, 22);
            this.toolStripButtonCreateVideo.Text = "创建视频";
            this.toolStripButtonCreateVideo.Click += new System.EventHandler(this.toolStripButtonCreateVideo_Click);
            // 
            // toolStripButtonHideVideo
            // 
            this.toolStripButtonHideVideo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonHideVideo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHideVideo.Image")));
            this.toolStripButtonHideVideo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHideVideo.Name = "toolStripButtonHideVideo";
            this.toolStripButtonHideVideo.Size = new System.Drawing.Size(60, 22);
            this.toolStripButtonHideVideo.Text = "隐藏视频";
            this.toolStripButtonHideVideo.Click += new System.EventHandler(this.toolStripButtonHideVideo_Click);
            // 
            // toolStripButtonHideProjectionLines
            // 
            this.toolStripButtonHideProjectionLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonHideProjectionLines.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHideProjectionLines.Image")));
            this.toolStripButtonHideProjectionLines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHideProjectionLines.Name = "toolStripButtonHideProjectionLines";
            this.toolStripButtonHideProjectionLines.Size = new System.Drawing.Size(72, 22);
            this.toolStripButtonHideProjectionLines.Text = "显示投影线";
            this.toolStripButtonHideProjectionLines.Click += new System.EventHandler(this.toolStripButtonHideProjectionLines_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeight = 25;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 14);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 32;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(850, 130);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // toolStripButtonCreatePoint
            // 
            this.toolStripButtonCreatePoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCreatePoint.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCreatePoint.Image")));
            this.toolStripButtonCreatePoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreatePoint.Name = "toolStripButtonCreatePoint";
            this.toolStripButtonCreatePoint.Size = new System.Drawing.Size(48, 22);
            this.toolStripButtonCreatePoint.Text = "创建点";
            this.toolStripButtonCreatePoint.Click += new System.EventHandler(this.toolStripButtonCreatePoint_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 538);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerrainVideo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonHideVideo;
        private System.Windows.Forms.ToolStripButton toolStripButtonHideProjectionLines;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreateVideo;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreatePoint;

    }
}


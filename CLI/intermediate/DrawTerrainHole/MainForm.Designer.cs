namespace DrawTerrainHole
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
            this.toolStripButtonLoadTerrain = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxDeep = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonCreateTerrainHole = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDeleteHole = new System.Windows.Forms.ToolStripButton();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 28);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(679, 487);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 685F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 518);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLoadTerrain,
            this.toolStripLabel1,
            this.toolStripTextBoxDeep,
            this.toolStripButtonCreateTerrainHole,
            this.toolStripButtonDeleteHole});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(685, 25);
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
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel1.Text = "深度";
            // 
            // toolStripTextBoxDeep
            // 
            this.toolStripTextBoxDeep.Name = "toolStripTextBoxDeep";
            this.toolStripTextBoxDeep.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBoxDeep.Text = "100";
            this.toolStripTextBoxDeep.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxDeep_KeyPress);
            // 
            // toolStripButtonCreateTerrainHole
            // 
            this.toolStripButtonCreateTerrainHole.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCreateTerrainHole.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCreateTerrainHole.Image")));
            this.toolStripButtonCreateTerrainHole.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreateTerrainHole.Name = "toolStripButtonCreateTerrainHole";
            this.toolStripButtonCreateTerrainHole.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonCreateTerrainHole.Text = "挖洞";
            this.toolStripButtonCreateTerrainHole.Click += new System.EventHandler(this.toolStripButtonCreateTerrainHole_Click);
            // 
            // toolStripButtonDeleteHole
            // 
            this.toolStripButtonDeleteHole.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDeleteHole.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDeleteHole.Image")));
            this.toolStripButtonDeleteHole.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeleteHole.Name = "toolStripButtonDeleteHole";
            this.toolStripButtonDeleteHole.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonDeleteHole.Text = "删洞";
            this.toolStripButtonDeleteHole.Click += new System.EventHandler(this.toolStripButtonDeleteHole_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 518);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DrawTerrainHole";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadTerrain;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxDeep;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreateTerrainHole;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeleteHole;

    }
}


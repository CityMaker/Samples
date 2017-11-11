namespace Ocean
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
            this.toolStripButtonSetOceanRegion = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDeleteOceanRegion = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxWindSpeed = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxWindDirection = new System.Windows.Forms.ToolStripTextBox();
            
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
            this.toolStripButtonSetOceanRegion,
            this.toolStripButtonDeleteOceanRegion,
            this.toolStripLabel1,
            this.toolStripTextBoxWindSpeed,
            this.toolStripLabel2,
            this.toolStripTextBoxWindDirection});
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
            // toolStripButtonSetOceanRegion
            // 
            this.toolStripButtonSetOceanRegion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSetOceanRegion.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSetOceanRegion.Image")));
            this.toolStripButtonSetOceanRegion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSetOceanRegion.Name = "toolStripButtonSetOceanRegion";
            this.toolStripButtonSetOceanRegion.Size = new System.Drawing.Size(108, 22);
            this.toolStripButtonSetOceanRegion.Text = "SetOceanRegion";
            this.toolStripButtonSetOceanRegion.Click += new System.EventHandler(this.toolStripButtonSetOceanRegion_Click);
            // 
            // toolStripButtonDeleteOceanRegion
            // 
            this.toolStripButtonDeleteOceanRegion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDeleteOceanRegion.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDeleteOceanRegion.Image")));
            this.toolStripButtonDeleteOceanRegion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeleteOceanRegion.Name = "toolStripButtonDeleteOceanRegion";
            this.toolStripButtonDeleteOceanRegion.Size = new System.Drawing.Size(127, 22);
            this.toolStripButtonDeleteOceanRegion.Text = "DeleteOceanRegion";
            this.toolStripButtonDeleteOceanRegion.Click += new System.EventHandler(this.toolStripButtonDeleteHole_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "海面风速";
            // 
            // toolStripTextBoxWindSpeed
            // 
            this.toolStripTextBoxWindSpeed.Name = "toolStripTextBoxWindSpeed";
            this.toolStripTextBoxWindSpeed.Size = new System.Drawing.Size(50, 25);
            this.toolStripTextBoxWindSpeed.Text = "0";
            this.toolStripTextBoxWindSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxDeep_KeyPress);
            this.toolStripTextBoxWindSpeed.TextChanged += new System.EventHandler(this.toolStripTextBoxWindSpeed_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel2.Text = "海面风向";
            // 
            // toolStripTextBoxWindDirection
            // 
            this.toolStripTextBoxWindDirection.Name = "toolStripTextBoxWindDirection";
            this.toolStripTextBoxWindDirection.Size = new System.Drawing.Size(50, 25);
            this.toolStripTextBoxWindDirection.Text = "0";
            this.toolStripTextBoxWindDirection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxDeep_KeyPress);
            this.toolStripTextBoxWindDirection.TextChanged += new System.EventHandler(this.toolStripTextBoxWindDirection_TextChanged);
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
            this.Text = "Ocean";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            
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
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxWindSpeed;
        private System.Windows.Forms.ToolStripButton toolStripButtonSetOceanRegion;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeleteOceanRegion;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxWindDirection;

    }
}


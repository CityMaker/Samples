namespace FeatureSelect
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSelectModeSetting = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripUnHightLight = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbCtrlEnable = new System.Windows.Forms.CheckBox();
            
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.axRenderControl1, 2);
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 33);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(679, 482);
            this.axRenderControl1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSelectModeSetting,
            this.toolStripSeparator1,
            this.toolStripUnHightLight});
            this.toolStrip1.Location = new System.Drawing.Point(100, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(585, 30);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(92, 27);
            this.toolStripLabel1.Text = "拾取模式设置：";
            // 
            // toolStripSelectModeSetting
            // 
            this.toolStripSelectModeSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripSelectModeSetting.Items.AddRange(new object[] {
            "漫游不拾取",
            "仅点选",
            "仅框选",
            "点选+框选"});
            this.toolStripSelectModeSetting.Name = "toolStripSelectModeSetting";
            this.toolStripSelectModeSetting.Size = new System.Drawing.Size(121, 30);
            this.toolStripSelectModeSetting.SelectedIndexChanged += new System.EventHandler(this.toolStripSelectModeSetting_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // toolStripUnHightLight
            // 
            this.toolStripUnHightLight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripUnHightLight.Image = ((System.Drawing.Image)(resources.GetObject("toolStripUnHightLight.Image")));
            this.toolStripUnHightLight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripUnHightLight.Name = "toolStripUnHightLight";
            this.toolStripUnHightLight.Size = new System.Drawing.Size(60, 27);
            this.toolStripUnHightLight.Text = "取消高亮";
            this.toolStripUnHightLight.Click += new System.EventHandler(this.toolStripUnHightLight_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbCtrlEnable, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 518);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // cbCtrlEnable
            // 
            this.cbCtrlEnable.AutoSize = true;
            this.cbCtrlEnable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbCtrlEnable.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCtrlEnable.Location = new System.Drawing.Point(3, 3);
            this.cbCtrlEnable.Name = "cbCtrlEnable";
            this.cbCtrlEnable.Size = new System.Drawing.Size(94, 24);
            this.cbCtrlEnable.TabIndex = 2;
            this.cbCtrlEnable.Text = "支持Ctrl键";
            this.cbCtrlEnable.UseVisualStyleBackColor = true;
            this.cbCtrlEnable.CheckedChanged += new System.EventHandler(this.cbCtrlEnable_CheckedChanged);
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
            this.Text = "FeatureSelect";
            
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox toolStripSelectModeSetting;
        private System.Windows.Forms.ToolStripButton toolStripUnHightLight;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox cbCtrlEnable;

    }
}


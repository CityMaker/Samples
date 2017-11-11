namespace ClipPlane
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
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripInteractModeSetting = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLineColorComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbClipPlaneEnable = new System.Windows.Forms.CheckBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripClipModeSetting = new System.Windows.Forms.ToolStripComboBox();
            
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
            this.toolStripInteractModeSetting,
            this.toolStripLabel3,
            this.toolStripClipModeSetting,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.toolStripLineColorComboBox});
            this.toolStrip1.Location = new System.Drawing.Point(105, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(580, 30);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(92, 27);
            this.toolStripLabel1.Text = "交互模式设置：";
            // 
            // toolStripInteractModeSetting
            // 
            this.toolStripInteractModeSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripInteractModeSetting.Items.AddRange(new object[] {
            "裁剪面交互模式",
            "鼠标拾取模式",
            "普通漫游模式"});
            this.toolStripInteractModeSetting.Name = "toolStripInteractModeSetting";
            this.toolStripInteractModeSetting.Size = new System.Drawing.Size(121, 30);
            this.toolStripInteractModeSetting.SelectedIndexChanged += new System.EventHandler(this.toolStripInteractModeSetting_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(68, 27);
            this.toolStripLabel2.Text = "交线颜色：";
            // 
            // toolStripLineColorComboBox
            // 
            this.toolStripLineColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripLineColorComboBox.Items.AddRange(new object[] {
            "白色",
            "红色",
            "黄色",
            "蓝色"});
            this.toolStripLineColorComboBox.Name = "toolStripLineColorComboBox";
            this.toolStripLineColorComboBox.Size = new System.Drawing.Size(75, 25);
            this.toolStripLineColorComboBox.SelectedIndexChanged += new System.EventHandler(this.toolStripLineColorComboBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbClipPlaneEnable, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(821, 518);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // cbClipPlaneEnable
            // 
            this.cbClipPlaneEnable.AutoSize = true;
            this.cbClipPlaneEnable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbClipPlaneEnable.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbClipPlaneEnable.Location = new System.Drawing.Point(3, 3);
            this.cbClipPlaneEnable.Name = "cbClipPlaneEnable";
            this.cbClipPlaneEnable.Size = new System.Drawing.Size(99, 24);
            this.cbClipPlaneEnable.TabIndex = 2;
            this.cbClipPlaneEnable.Text = "是否参与裁剪";
            this.cbClipPlaneEnable.UseVisualStyleBackColor = true;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(80, 27);
            this.toolStripLabel3.Text = "裁剪面模式：";
            // 
            // toolStripClipModeSetting
            // 
            this.toolStripClipModeSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripClipModeSetting.Items.AddRange(new object[] {
            "任意面",
            "长方体"});
            this.toolStripClipModeSetting.Name = "toolStripClipModeSetting";
            this.toolStripClipModeSetting.Size = new System.Drawing.Size(100, 30);
            this.toolStripClipModeSetting.SelectedIndexChanged += new System.EventHandler(this.toolStripClipModeSetting_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 518);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ClipPlane";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
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
        private System.Windows.Forms.ToolStripComboBox toolStripInteractModeSetting;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox cbClipPlaneEnable;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripLineColorComboBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox toolStripClipModeSetting;

    }
}


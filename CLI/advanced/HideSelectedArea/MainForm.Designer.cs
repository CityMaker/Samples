namespace HideSelectedArea
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnResetFeatureVisibleMask = new System.Windows.Forms.Button();
            this.btnSetFeatureVisibleMask = new System.Windows.Forms.Button();
            this.comboBoxVisibleMask = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.comboBoxViewportMode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(881, 791);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.comboBoxViewportMode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(664, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 785);
            this.panel1.TabIndex = 1;
            // 
            // btnResetFeatureVisibleMask
            // 
            this.btnResetFeatureVisibleMask.Location = new System.Drawing.Point(24, 193);
            this.btnResetFeatureVisibleMask.Name = "btnResetFeatureVisibleMask";
            this.btnResetFeatureVisibleMask.Size = new System.Drawing.Size(143, 43);
            this.btnResetFeatureVisibleMask.TabIndex = 9;
            this.btnResetFeatureVisibleMask.Text = "取消隐藏";
            this.btnResetFeatureVisibleMask.UseVisualStyleBackColor = true;
            this.btnResetFeatureVisibleMask.Click += new System.EventHandler(this.btnResetFeatureVisibleMask_Click);
            // 
            // btnSetFeatureVisibleMask
            // 
            this.btnSetFeatureVisibleMask.Location = new System.Drawing.Point(24, 118);
            this.btnSetFeatureVisibleMask.Name = "btnSetFeatureVisibleMask";
            this.btnSetFeatureVisibleMask.Size = new System.Drawing.Size(143, 43);
            this.btnSetFeatureVisibleMask.TabIndex = 8;
            this.btnSetFeatureVisibleMask.Text = "隐藏选中Feature";
            this.btnSetFeatureVisibleMask.UseVisualStyleBackColor = true;
            this.btnSetFeatureVisibleMask.Click += new System.EventHandler(this.btnSetFeatureVisibleMask_Click);
            // 
            // comboBoxVisibleMask
            // 
            this.comboBoxVisibleMask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVisibleMask.FormattingEnabled = true;
            this.comboBoxVisibleMask.Items.AddRange(new object[] {
            "gviViewNone",
            "gviView0",
            "gviView1",
            "gviView2",
            "gviView3",
            "gviViewAllNormalView",
            "gviViewPIP"});
            this.comboBoxVisibleMask.Location = new System.Drawing.Point(24, 61);
            this.comboBoxVisibleMask.Name = "comboBoxVisibleMask";
            this.comboBoxVisibleMask.Size = new System.Drawing.Size(164, 20);
            this.comboBoxVisibleMask.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "VisibleMask";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(7, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(185, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "支持点选、框选(按ctrl键可多选)";
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(655, 785);
            this.axRenderControl1.TabIndex = 2;
            // 
            // comboBoxViewportMode
            // 
            this.comboBoxViewportMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxViewportMode.FormattingEnabled = true;
            this.comboBoxViewportMode.Items.AddRange(new object[] {
            "gviViewportSinglePerspective 单视口透视投影",
            "gviViewportL1R1  左一右一 ",
            "gviViewportT1B1  上一下一 ",
            "gviViewportL1M1R1  左一中一右一 ",
            "gviViewportT1M1B1  上一中一下一 ",
            "gviViewportL2R1  左边两个视口，右边一个 ",
            "gviViewportL1R2  左一右二 ",
            "gviViewportQuad  四视口 ",
            "gviViewportPIP  大小两视图（画中画）",
            "gviViewportQuadH  水平四视口"});
            this.comboBoxViewportMode.Location = new System.Drawing.Point(26, 56);
            this.comboBoxViewportMode.Name = "comboBoxViewportMode";
            this.comboBoxViewportMode.Size = new System.Drawing.Size(164, 20);
            this.comboBoxViewportMode.TabIndex = 11;
            this.comboBoxViewportMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxViewportMode_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "ViewportMode";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSetFeatureVisibleMask);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBoxVisibleMask);
            this.groupBox1.Controls.Add(this.btnResetFeatureVisibleMask);
            this.groupBox1.Location = new System.Drawing.Point(5, 144);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 262);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "指定区域隐藏";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 791);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HideSelectedArea";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxVisibleMask;
        private System.Windows.Forms.Button btnSetFeatureVisibleMask;
        private System.Windows.Forms.Button btnResetFeatureVisibleMask;
        private System.Windows.Forms.ComboBox comboBoxViewportMode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;

    }
}


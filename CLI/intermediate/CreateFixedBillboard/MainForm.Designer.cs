namespace CreateFixedBillboard
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
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtLabelText = new System.Windows.Forms.ToolStripTextBox();
            this.chagecolorbox = new System.Windows.Forms.ToolStripButton();
            this.colorBox = new System.Windows.Forms.ToolStripTextBox();
            this.fontsizechager = new System.Windows.Forms.ToolStripButton();
            this.toolstripFontSize = new System.Windows.Forms.ToolStripTextBox();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxColor = new System.Windows.Forms.ToolStripComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axRenderControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 462F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(933, 518);
            this.tableLayoutPanel1.TabIndex = 2;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtLabelText,
            this.chagecolorbox,
            this.colorBox,
            this.fontsizechager,
            this.toolstripFontSize,
            this.toolStripLabel2,
            this.toolStripComboBoxColor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(933, 48);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(113, 45);
            this.toolStripLabel1.Text = "请输入显示的内容：";
            // 
            // txtLabelText
            // 
            this.txtLabelText.Name = "txtLabelText";
            this.txtLabelText.Size = new System.Drawing.Size(180, 48);
            this.txtLabelText.TextChanged += new System.EventHandler(this.txtLabelText_TextChanged);
            // 
            // chagecolorbox
            // 
            this.chagecolorbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.chagecolorbox.Image = ((System.Drawing.Image)(resources.GetObject("chagecolorbox.Image")));
            this.chagecolorbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.chagecolorbox.Name = "chagecolorbox";
            this.chagecolorbox.Size = new System.Drawing.Size(105, 45);
            this.chagecolorbox.Text = "请字体选择颜色：";
            this.chagecolorbox.Click += new System.EventHandler(this.btnChangeColor_Click);
            // 
            // colorBox
            // 
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(80, 48);
            // 
            // fontsizechager
            // 
            this.fontsizechager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fontsizechager.Image = ((System.Drawing.Image)(resources.GetObject("fontsizechager.Image")));
            this.fontsizechager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fontsizechager.Name = "fontsizechager";
            this.fontsizechager.Size = new System.Drawing.Size(105, 45);
            this.fontsizechager.Text = "请选择字体大小：";
            // 
            // toolstripFontSize
            // 
            this.toolstripFontSize.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolstripFontSize.Name = "toolstripFontSize";
            this.toolstripFontSize.Size = new System.Drawing.Size(30, 48);
            this.toolstripFontSize.Text = "14";
            this.toolstripFontSize.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolstripFontSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fontsize_KeyPress);
            this.toolstripFontSize.TextChanged += new System.EventHandler(this.fontsize_TextChanged);
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 51);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(927, 464);
            this.axRenderControl1.TabIndex = 0;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(119, 45);
            this.toolStripLabel2.Text = "请选择外轮廓线颜色:";
            // 
            // toolStripComboBoxColor
            // 
            this.toolStripComboBoxColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxColor.Items.AddRange(new object[] {
            "红色",
            "黄色",
            "蓝色"});
            this.toolStripComboBoxColor.Name = "toolStripComboBoxColor";
            this.toolStripComboBoxColor.Size = new System.Drawing.Size(75, 48);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 518);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreateFixedBillboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axRenderControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox txtLabelText;
        private System.Windows.Forms.ToolStripButton chagecolorbox;
        private System.Windows.Forms.ToolStripTextBox colorBox;
        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.ToolStripButton fontsizechager;
        private System.Windows.Forms.ToolStripTextBox toolstripFontSize;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxColor;

    }
}


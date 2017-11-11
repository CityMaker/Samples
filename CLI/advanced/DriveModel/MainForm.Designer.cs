namespace DriveModel
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
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBoxScale = new System.Windows.Forms.Button();
            this.cbEditSideMode = new System.Windows.Forms.ComboBox();
            this.btnEditSide = new System.Windows.Forms.Button();
            this.btnEditPolygon = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.trackBarOpacity = new System.Windows.Forms.TrackBar();
            this.btnChangeColor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.colorBox = new System.Windows.Forms.TextBox();
            this.btnModifyModel = new System.Windows.Forms.Button();
            this.btnDriveModel = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNormal = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.numFloorHeight = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numFloorNum = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cbKeepArea = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFloorHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFloorNum)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 195F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(764, 548);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(563, 542);
            this.axRenderControl1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbKeepArea);
            this.groupBox1.Controls.Add(this.btnBoxScale);
            this.groupBox1.Controls.Add(this.cbEditSideMode);
            this.groupBox1.Controls.Add(this.btnEditSide);
            this.groupBox1.Controls.Add(this.btnEditPolygon);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnModifyModel);
            this.groupBox1.Controls.Add(this.btnDriveModel);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.numFloorHeight);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numFloorNum);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(572, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 542);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // btnBoxScale
            // 
            this.btnBoxScale.Location = new System.Drawing.Point(8, 481);
            this.btnBoxScale.Name = "btnBoxScale";
            this.btnBoxScale.Size = new System.Drawing.Size(85, 34);
            this.btnBoxScale.TabIndex = 21;
            this.btnBoxScale.Text = "整体缩放";
            this.btnBoxScale.UseVisualStyleBackColor = true;
            this.btnBoxScale.Click += new System.EventHandler(this.btnBoxScale_Click);
            // 
            // cbEditSideMode
            // 
            this.cbEditSideMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEditSideMode.FormattingEnabled = true;
            this.cbEditSideMode.Items.AddRange(new object[] {
            "插入点",
            "更新点"});
            this.cbEditSideMode.Location = new System.Drawing.Point(99, 438);
            this.cbEditSideMode.Name = "cbEditSideMode";
            this.cbEditSideMode.Size = new System.Drawing.Size(81, 20);
            this.cbEditSideMode.TabIndex = 20;
            // 
            // btnEditSide
            // 
            this.btnEditSide.Location = new System.Drawing.Point(8, 430);
            this.btnEditSide.Name = "btnEditSide";
            this.btnEditSide.Size = new System.Drawing.Size(85, 34);
            this.btnEditSide.TabIndex = 19;
            this.btnEditSide.Text = "编辑侧面";
            this.btnEditSide.UseVisualStyleBackColor = true;
            this.btnEditSide.Click += new System.EventHandler(this.btnEditSide_Click);
            // 
            // btnEditPolygon
            // 
            this.btnEditPolygon.Location = new System.Drawing.Point(8, 380);
            this.btnEditPolygon.Name = "btnEditPolygon";
            this.btnEditPolygon.Size = new System.Drawing.Size(85, 34);
            this.btnEditPolygon.TabIndex = 18;
            this.btnEditPolygon.Text = "编辑面顶点";
            this.btnEditPolygon.UseVisualStyleBackColor = true;
            this.btnEditPolygon.Click += new System.EventHandler(this.btnEditPolygon_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(8, 327);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(61, 34);
            this.btnReset.TabIndex = 17;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.trackBarOpacity);
            this.groupBox2.Controls.Add(this.btnChangeColor);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.colorBox);
            this.groupBox2.Location = new System.Drawing.Point(4, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 115);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置颜色";
            // 
            // trackBarOpacity
            // 
            this.trackBarOpacity.Location = new System.Drawing.Point(45, 61);
            this.trackBarOpacity.Maximum = 255;
            this.trackBarOpacity.Name = "trackBarOpacity";
            this.trackBarOpacity.Size = new System.Drawing.Size(134, 45);
            this.trackBarOpacity.TabIndex = 22;
            this.trackBarOpacity.Value = 255;
            this.trackBarOpacity.Scroll += new System.EventHandler(this.trackBarOpacity_Scroll);
            // 
            // btnChangeColor
            // 
            this.btnChangeColor.Location = new System.Drawing.Point(6, 30);
            this.btnChangeColor.Name = "btnChangeColor";
            this.btnChangeColor.Size = new System.Drawing.Size(85, 25);
            this.btnChangeColor.TabIndex = 19;
            this.btnChangeColor.Text = "ChangeColor";
            this.btnChangeColor.UseVisualStyleBackColor = true;
            this.btnChangeColor.Click += new System.EventHandler(this.btnChangeColor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "透明度";
            // 
            // colorBox
            // 
            this.colorBox.Location = new System.Drawing.Point(97, 34);
            this.colorBox.Name = "colorBox";
            this.colorBox.ReadOnly = true;
            this.colorBox.Size = new System.Drawing.Size(66, 21);
            this.colorBox.TabIndex = 20;
            this.colorBox.Text = "FFFFFF00";
            // 
            // btnModifyModel
            // 
            this.btnModifyModel.Location = new System.Drawing.Point(81, 274);
            this.btnModifyModel.Name = "btnModifyModel";
            this.btnModifyModel.Size = new System.Drawing.Size(61, 34);
            this.btnModifyModel.TabIndex = 15;
            this.btnModifyModel.Text = "修改属性";
            this.btnModifyModel.UseVisualStyleBackColor = true;
            this.btnModifyModel.Click += new System.EventHandler(this.btnModifyModel_Click);
            // 
            // btnDriveModel
            // 
            this.btnDriveModel.Location = new System.Drawing.Point(8, 274);
            this.btnDriveModel.Name = "btnDriveModel";
            this.btnDriveModel.Size = new System.Drawing.Size(61, 34);
            this.btnDriveModel.TabIndex = 14;
            this.btnDriveModel.Text = "驱动模型";
            this.btnDriveModel.UseVisualStyleBackColor = true;
            this.btnDriveModel.Click += new System.EventHandler(this.btnDriveModel_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grip;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNormal,
            this.toolStripButtonSelect});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(183, 25);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonNormal
            // 
            this.toolStripButtonNormal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonNormal.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNormal.Image")));
            this.toolStripButtonNormal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNormal.Name = "toolStripButtonNormal";
            this.toolStripButtonNormal.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonNormal.Text = "漫游";
            this.toolStripButtonNormal.Click += new System.EventHandler(this.toolStripButtonNormal_Click);
            // 
            // toolStripButtonSelect
            // 
            this.toolStripButtonSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSelect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSelect.Image")));
            this.toolStripButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelect.Name = "toolStripButtonSelect";
            this.toolStripButtonSelect.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonSelect.Text = "拾取";
            this.toolStripButtonSelect.Click += new System.EventHandler(this.toolStripButtonSelect_Click);
            // 
            // numFloorHeight
            // 
            this.numFloorHeight.Location = new System.Drawing.Point(47, 87);
            this.numFloorHeight.Name = "numFloorHeight";
            this.numFloorHeight.Size = new System.Drawing.Size(78, 21);
            this.numFloorHeight.TabIndex = 10;
            this.numFloorHeight.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "层高";
            // 
            // numFloorNum
            // 
            this.numFloorNum.Location = new System.Drawing.Point(47, 54);
            this.numFloorNum.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numFloorNum.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numFloorNum.Name = "numFloorNum";
            this.numFloorNum.Size = new System.Drawing.Size(78, 21);
            this.numFloorNum.TabIndex = 8;
            this.numFloorNum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "层数";
            // 
            // cbKeepArea
            // 
            this.cbKeepArea.AutoSize = true;
            this.cbKeepArea.Location = new System.Drawing.Point(95, 491);
            this.cbKeepArea.Name = "cbKeepArea";
            this.cbKeepArea.Size = new System.Drawing.Size(96, 16);
            this.cbKeepArea.TabIndex = 22;
            this.cbKeepArea.Text = "保持面积不变";
            this.cbKeepArea.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 548);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DriveModel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFloorHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFloorNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numFloorNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numFloorHeight;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonNormal;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelect;
        private System.Windows.Forms.Button btnDriveModel;
        private System.Windows.Forms.Button btnModifyModel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar trackBarOpacity;
        private System.Windows.Forms.Button btnChangeColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox colorBox;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnEditPolygon;
        private System.Windows.Forms.Button btnEditSide;
        private System.Windows.Forms.ComboBox cbEditSideMode;
        private System.Windows.Forms.Button btnBoxScale;
        private System.Windows.Forms.CheckBox cbKeepArea;

    }
}


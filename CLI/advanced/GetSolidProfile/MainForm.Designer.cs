namespace GetSolidProfile
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxSelectRenderPolygon = new System.Windows.Forms.CheckBox();
            this.buttonClearProfile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxCompareProfilePolygon = new System.Windows.Forms.CheckBox();
            this.checkBoxCompareExterior = new System.Windows.Forms.CheckBox();
            this.checkBoxCompareDrawPolygon = new System.Windows.Forms.CheckBox();
            this.checkBoxCompareLayer = new System.Windows.Forms.CheckBox();
            this.checkBoxCompareProfile = new System.Windows.Forms.CheckBox();
            this.checkBoxCompareInterior = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonCreatePolygon = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.BackColor = System.Drawing.Color.Black;
            this.axRenderControl1.ClipMode = Gvitech.CityMaker.RenderControl.gviClipMode.gviClipBox;
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.EnableMultiTouch = false;
            this.axRenderControl1.FullScreen = false;
            this.axRenderControl1.InteractMode = Gvitech.CityMaker.RenderControl.gviInteractMode.gviInteractNormal;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.MeasurementMode = Gvitech.CityMaker.RenderControl.gviMeasurementMode.gviMeasureAerialDistance;
            this.axRenderControl1.MouseCursor = "";
            this.axRenderControl1.MouseSelectMode = Gvitech.CityMaker.RenderControl.gviMouseSelectMode.gviMouseSelectClick;
            this.axRenderControl1.MouseSelectObjectMask = Gvitech.CityMaker.RenderControl.gviMouseSelectObjectMask.gviSelectAll;
            this.axRenderControl1.MouseSnapMode = Gvitech.CityMaker.RenderControl.gviMouseSnapMode.gviMouseSnapDisable;
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(474, 512);
            this.axRenderControl1.TabIndex = 0;
            this.axRenderControl1.UseEarthOrbitManipulator = Gvitech.CityMaker.RenderControl.gviManipulatorMode.gviCityMakerManipulator;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(709, 518);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxSelectRenderPolygon);
            this.panel1.Controls.Add(this.buttonClearProfile);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(483, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(223, 512);
            this.panel1.TabIndex = 1;
            // 
            // checkBoxSelectRenderPolygon
            // 
            this.checkBoxSelectRenderPolygon.AutoSize = true;
            this.checkBoxSelectRenderPolygon.Location = new System.Drawing.Point(16, 422);
            this.checkBoxSelectRenderPolygon.Name = "checkBoxSelectRenderPolygon";
            this.checkBoxSelectRenderPolygon.Size = new System.Drawing.Size(72, 16);
            this.checkBoxSelectRenderPolygon.TabIndex = 8;
            this.checkBoxSelectRenderPolygon.Text = "拾取侧面";
            this.checkBoxSelectRenderPolygon.UseVisualStyleBackColor = true;
            this.checkBoxSelectRenderPolygon.CheckedChanged += new System.EventHandler(this.checkBoxSelectRenderPolygon_CheckedChanged);
            // 
            // buttonClearProfile
            // 
            this.buttonClearProfile.Location = new System.Drawing.Point(16, 365);
            this.buttonClearProfile.Name = "buttonClearProfile";
            this.buttonClearProfile.Size = new System.Drawing.Size(108, 32);
            this.buttonClearProfile.TabIndex = 7;
            this.buttonClearProfile.Text = "清空分析结果";
            this.buttonClearProfile.UseVisualStyleBackColor = true;
            this.buttonClearProfile.Click += new System.EventHandler(this.buttonClearProfile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxCompareProfilePolygon);
            this.groupBox1.Controls.Add(this.checkBoxCompareExterior);
            this.groupBox1.Controls.Add(this.checkBoxCompareDrawPolygon);
            this.groupBox1.Controls.Add(this.checkBoxCompareLayer);
            this.groupBox1.Controls.Add(this.checkBoxCompareProfile);
            this.groupBox1.Controls.Add(this.checkBoxCompareInterior);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numHeight);
            this.groupBox1.Location = new System.Drawing.Point(3, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 299);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数";
            // 
            // checkBoxCompareProfilePolygon
            // 
            this.checkBoxCompareProfilePolygon.AutoSize = true;
            this.checkBoxCompareProfilePolygon.Location = new System.Drawing.Point(13, 185);
            this.checkBoxCompareProfilePolygon.Name = "checkBoxCompareProfilePolygon";
            this.checkBoxCompareProfilePolygon.Size = new System.Drawing.Size(108, 16);
            this.checkBoxCompareProfilePolygon.TabIndex = 10;
            this.checkBoxCompareProfilePolygon.Text = "对比侧面多边形";
            this.checkBoxCompareProfilePolygon.UseVisualStyleBackColor = true;
            this.checkBoxCompareProfilePolygon.CheckedChanged += new System.EventHandler(this.checkBoxCompareProfilePolygon_CheckedChanged);
            // 
            // checkBoxCompareExterior
            // 
            this.checkBoxCompareExterior.AutoSize = true;
            this.checkBoxCompareExterior.Location = new System.Drawing.Point(13, 116);
            this.checkBoxCompareExterior.Name = "checkBoxCompareExterior";
            this.checkBoxCompareExterior.Size = new System.Drawing.Size(96, 16);
            this.checkBoxCompareExterior.TabIndex = 9;
            this.checkBoxCompareExterior.Text = "对比剩余部分";
            this.checkBoxCompareExterior.UseVisualStyleBackColor = true;
            this.checkBoxCompareExterior.CheckedChanged += new System.EventHandler(this.checkBoxCompareExterior_CheckedChanged);
            // 
            // checkBoxCompareDrawPolygon
            // 
            this.checkBoxCompareDrawPolygon.AutoSize = true;
            this.checkBoxCompareDrawPolygon.Location = new System.Drawing.Point(13, 272);
            this.checkBoxCompareDrawPolygon.Name = "checkBoxCompareDrawPolygon";
            this.checkBoxCompareDrawPolygon.Size = new System.Drawing.Size(108, 16);
            this.checkBoxCompareDrawPolygon.TabIndex = 8;
            this.checkBoxCompareDrawPolygon.Text = "对比水平多边形";
            this.checkBoxCompareDrawPolygon.UseVisualStyleBackColor = true;
            this.checkBoxCompareDrawPolygon.CheckedChanged += new System.EventHandler(this.checkBoxCompareDrawPolygon_CheckedChanged);
            // 
            // checkBoxCompareLayer
            // 
            this.checkBoxCompareLayer.AutoSize = true;
            this.checkBoxCompareLayer.Location = new System.Drawing.Point(13, 236);
            this.checkBoxCompareLayer.Name = "checkBoxCompareLayer";
            this.checkBoxCompareLayer.Size = new System.Drawing.Size(96, 16);
            this.checkBoxCompareLayer.TabIndex = 7;
            this.checkBoxCompareLayer.Text = "对比原始图层";
            this.checkBoxCompareLayer.UseVisualStyleBackColor = true;
            this.checkBoxCompareLayer.CheckedChanged += new System.EventHandler(this.checkBoxCompareLayer_CheckedChanged);
            // 
            // checkBoxCompareProfile
            // 
            this.checkBoxCompareProfile.AutoSize = true;
            this.checkBoxCompareProfile.Location = new System.Drawing.Point(13, 161);
            this.checkBoxCompareProfile.Name = "checkBoxCompareProfile";
            this.checkBoxCompareProfile.Size = new System.Drawing.Size(108, 16);
            this.checkBoxCompareProfile.TabIndex = 6;
            this.checkBoxCompareProfile.Text = "对比生成的侧面";
            this.checkBoxCompareProfile.UseVisualStyleBackColor = true;
            this.checkBoxCompareProfile.CheckedChanged += new System.EventHandler(this.checkBoxCompareProfile_CheckedChanged);
            // 
            // checkBoxCompareInterior
            // 
            this.checkBoxCompareInterior.AutoSize = true;
            this.checkBoxCompareInterior.Location = new System.Drawing.Point(13, 90);
            this.checkBoxCompareInterior.Name = "checkBoxCompareInterior";
            this.checkBoxCompareInterior.Size = new System.Drawing.Size(96, 16);
            this.checkBoxCompareInterior.TabIndex = 5;
            this.checkBoxCompareInterior.Text = "对比挖掉部分";
            this.checkBoxCompareInterior.UseVisualStyleBackColor = true;
            this.checkBoxCompareInterior.CheckedChanged += new System.EventHandler(this.checkBoxCompareInterior_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "挖洞深度";
            // 
            // numHeight
            // 
            this.numHeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numHeight.Location = new System.Drawing.Point(13, 45);
            this.numHeight.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(75, 21);
            this.numHeight.TabIndex = 0;
            this.numHeight.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonCreatePolygon});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(223, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonCreatePolygon
            // 
            this.toolStripButtonCreatePolygon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCreatePolygon.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCreatePolygon.Image")));
            this.toolStripButtonCreatePolygon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreatePolygon.Name = "toolStripButtonCreatePolygon";
            this.toolStripButtonCreatePolygon.Size = new System.Drawing.Size(120, 22);
            this.toolStripButtonCreatePolygon.Text = "创建水平切割多边形";
            this.toolStripButtonCreatePolygon.Click += new System.EventHandler(this.toolStripButtonCreatePolygon_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 518);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GetSolidProfile";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreatePolygon;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.CheckBox checkBoxCompareInterior;
        private System.Windows.Forms.Button buttonClearProfile;
        private System.Windows.Forms.CheckBox checkBoxCompareProfile;
        private System.Windows.Forms.CheckBox checkBoxCompareLayer;
        private System.Windows.Forms.CheckBox checkBoxCompareDrawPolygon;
        private System.Windows.Forms.CheckBox checkBoxCompareExterior;
        private System.Windows.Forms.CheckBox checkBoxCompareProfilePolygon;
        private System.Windows.Forms.CheckBox checkBoxSelectRenderPolygon;

    }
}


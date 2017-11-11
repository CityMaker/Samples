namespace GeometryConvert3
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbHideModelPoint = new System.Windows.Forms.CheckBox();
            this.btnDelModelPoint = new System.Windows.Forms.Button();
            this.btnClearPolygon = new System.Windows.Forms.Button();
            this.btnCutModelPoint = new System.Windows.Forms.Button();
            this.btnCreatePolygon = new System.Windows.Forms.Button();
            this.btnSplitModelPoint = new System.Windows.Forms.Button();
            this.cbHideExteriorModelPoint = new System.Windows.Forms.CheckBox();
            this.cbHideInteriorModelPoint = new System.Windows.Forms.CheckBox();
            this.btnDelExteriorModelPoint = new System.Windows.Forms.Button();
            this.btnDelInteriorModelPoint = new System.Windows.Forms.Button();
            this.cbShowSrcModelPoint = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSymmetricDifference = new System.Windows.Forms.Button();
            this.btnDifference = new System.Windows.Forms.Button();
            this.btnUnion = new System.Windows.Forms.Button();
            this.btnIntersection = new System.Windows.Forms.Button();
            this.cbCutWithZ = new System.Windows.Forms.CheckBox();
            this.numMinZ = new System.Windows.Forms.NumericUpDown();
            this.numMaxZ = new System.Windows.Forms.NumericUpDown();
            this.MinZL = new System.Windows.Forms.Label();
            this.MaxZ = new System.Windows.Forms.Label();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxZ)).BeginInit();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(512, 566);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 572);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MaxZ);
            this.panel1.Controls.Add(this.MinZL);
            this.panel1.Controls.Add(this.numMaxZ);
            this.panel1.Controls.Add(this.numMinZ);
            this.panel1.Controls.Add(this.cbCutWithZ);
            this.panel1.Controls.Add(this.cbHideModelPoint);
            this.panel1.Controls.Add(this.btnDelModelPoint);
            this.panel1.Controls.Add(this.btnClearPolygon);
            this.panel1.Controls.Add(this.btnCutModelPoint);
            this.panel1.Controls.Add(this.btnCreatePolygon);
            this.panel1.Controls.Add(this.btnSplitModelPoint);
            this.panel1.Controls.Add(this.cbHideExteriorModelPoint);
            this.panel1.Controls.Add(this.cbHideInteriorModelPoint);
            this.panel1.Controls.Add(this.btnDelExteriorModelPoint);
            this.panel1.Controls.Add(this.btnDelInteriorModelPoint);
            this.panel1.Controls.Add(this.cbShowSrcModelPoint);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(521, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(161, 566);
            this.panel1.TabIndex = 1;
            // 
            // cbHideModelPoint
            // 
            this.cbHideModelPoint.AutoSize = true;
            this.cbHideModelPoint.Location = new System.Drawing.Point(8, 350);
            this.cbHideModelPoint.Name = "cbHideModelPoint";
            this.cbHideModelPoint.Size = new System.Drawing.Size(132, 16);
            this.cbHideModelPoint.TabIndex = 18;
            this.cbHideModelPoint.Text = "隐藏切割得到的模型";
            this.cbHideModelPoint.UseVisualStyleBackColor = true;
            this.cbHideModelPoint.CheckedChanged += new System.EventHandler(this.cbHideModelPoint_CheckedChanged);
            // 
            // btnDelModelPoint
            // 
            this.btnDelModelPoint.Location = new System.Drawing.Point(5, 372);
            this.btnDelModelPoint.Name = "btnDelModelPoint";
            this.btnDelModelPoint.Size = new System.Drawing.Size(147, 23);
            this.btnDelModelPoint.TabIndex = 17;
            this.btnDelModelPoint.Text = "删除切割得到的模型";
            this.btnDelModelPoint.UseVisualStyleBackColor = true;
            this.btnDelModelPoint.Click += new System.EventHandler(this.btnDelModelPoint_Click);
            // 
            // btnClearPolygon
            // 
            this.btnClearPolygon.Location = new System.Drawing.Point(109, 9);
            this.btnClearPolygon.Name = "btnClearPolygon";
            this.btnClearPolygon.Size = new System.Drawing.Size(43, 30);
            this.btnClearPolygon.TabIndex = 16;
            this.btnClearPolygon.Text = "清空";
            this.btnClearPolygon.UseVisualStyleBackColor = true;
            this.btnClearPolygon.Click += new System.EventHandler(this.btnClearPolygon_Click);
            // 
            // btnCutModelPoint
            // 
            this.btnCutModelPoint.Location = new System.Drawing.Point(55, 310);
            this.btnCutModelPoint.Name = "btnCutModelPoint";
            this.btnCutModelPoint.Size = new System.Drawing.Size(61, 34);
            this.btnCutModelPoint.TabIndex = 15;
            this.btnCutModelPoint.Text = "Cut";
            this.btnCutModelPoint.UseVisualStyleBackColor = true;
            this.btnCutModelPoint.Click += new System.EventHandler(this.btnCutModelPoint_Click);
            // 
            // btnCreatePolygon
            // 
            this.btnCreatePolygon.Location = new System.Drawing.Point(9, 9);
            this.btnCreatePolygon.Name = "btnCreatePolygon";
            this.btnCreatePolygon.Size = new System.Drawing.Size(85, 30);
            this.btnCreatePolygon.TabIndex = 13;
            this.btnCreatePolygon.Text = "绘制Polygon";
            this.btnCreatePolygon.UseVisualStyleBackColor = true;
            this.btnCreatePolygon.Click += new System.EventHandler(this.btnCreatePolygon_Click);
            // 
            // btnSplitModelPoint
            // 
            this.btnSplitModelPoint.Location = new System.Drawing.Point(55, 421);
            this.btnSplitModelPoint.Name = "btnSplitModelPoint";
            this.btnSplitModelPoint.Size = new System.Drawing.Size(61, 34);
            this.btnSplitModelPoint.TabIndex = 14;
            this.btnSplitModelPoint.Text = "Split";
            this.btnSplitModelPoint.UseVisualStyleBackColor = true;
            this.btnSplitModelPoint.Click += new System.EventHandler(this.btnSplitModelPoint_Click);
            // 
            // cbHideExteriorModelPoint
            // 
            this.cbHideExteriorModelPoint.AutoSize = true;
            this.cbHideExteriorModelPoint.Location = new System.Drawing.Point(3, 483);
            this.cbHideExteriorModelPoint.Name = "cbHideExteriorModelPoint";
            this.cbHideExteriorModelPoint.Size = new System.Drawing.Size(156, 16);
            this.cbHideExteriorModelPoint.TabIndex = 10;
            this.cbHideExteriorModelPoint.Text = "隐藏外环切割得到的模型";
            this.cbHideExteriorModelPoint.UseVisualStyleBackColor = true;
            this.cbHideExteriorModelPoint.CheckedChanged += new System.EventHandler(this.cbHideExteriorModelPoint_CheckedChanged);
            // 
            // cbHideInteriorModelPoint
            // 
            this.cbHideInteriorModelPoint.AutoSize = true;
            this.cbHideInteriorModelPoint.Location = new System.Drawing.Point(3, 461);
            this.cbHideInteriorModelPoint.Name = "cbHideInteriorModelPoint";
            this.cbHideInteriorModelPoint.Size = new System.Drawing.Size(156, 16);
            this.cbHideInteriorModelPoint.TabIndex = 9;
            this.cbHideInteriorModelPoint.Text = "隐藏内环切割得到的模型";
            this.cbHideInteriorModelPoint.UseVisualStyleBackColor = true;
            this.cbHideInteriorModelPoint.CheckedChanged += new System.EventHandler(this.cbHideInteriorModelPoint_CheckedChanged);
            // 
            // btnDelExteriorModelPoint
            // 
            this.btnDelExteriorModelPoint.Location = new System.Drawing.Point(9, 534);
            this.btnDelExteriorModelPoint.Name = "btnDelExteriorModelPoint";
            this.btnDelExteriorModelPoint.Size = new System.Drawing.Size(147, 23);
            this.btnDelExteriorModelPoint.TabIndex = 6;
            this.btnDelExteriorModelPoint.Text = "删除外环切割得到的模型";
            this.btnDelExteriorModelPoint.UseVisualStyleBackColor = true;
            this.btnDelExteriorModelPoint.Click += new System.EventHandler(this.btnDelExteriorModelPoint_Click);
            // 
            // btnDelInteriorModelPoint
            // 
            this.btnDelInteriorModelPoint.Location = new System.Drawing.Point(9, 505);
            this.btnDelInteriorModelPoint.Name = "btnDelInteriorModelPoint";
            this.btnDelInteriorModelPoint.Size = new System.Drawing.Size(147, 23);
            this.btnDelInteriorModelPoint.TabIndex = 5;
            this.btnDelInteriorModelPoint.Text = "删除内环切割得到的模型";
            this.btnDelInteriorModelPoint.UseVisualStyleBackColor = true;
            this.btnDelInteriorModelPoint.Click += new System.EventHandler(this.btnDelInteriorModelPoint_Click);
            // 
            // cbShowSrcModelPoint
            // 
            this.cbShowSrcModelPoint.AutoSize = true;
            this.cbShowSrcModelPoint.Checked = true;
            this.cbShowSrcModelPoint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowSrcModelPoint.Location = new System.Drawing.Point(26, 221);
            this.cbShowSrcModelPoint.Name = "cbShowSrcModelPoint";
            this.cbShowSrcModelPoint.Size = new System.Drawing.Size(96, 16);
            this.cbShowSrcModelPoint.TabIndex = 4;
            this.cbShowSrcModelPoint.Text = "显示原始模型";
            this.cbShowSrcModelPoint.UseVisualStyleBackColor = true;
            this.cbShowSrcModelPoint.CheckedChanged += new System.EventHandler(this.cbShowSrcModelPoint_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSymmetricDifference);
            this.groupBox1.Controls.Add(this.btnDifference);
            this.groupBox1.Controls.Add(this.btnUnion);
            this.groupBox1.Controls.Add(this.btnIntersection);
            this.groupBox1.Location = new System.Drawing.Point(9, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(143, 161);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作类型";
            // 
            // btnSymmetricDifference
            // 
            this.btnSymmetricDifference.Location = new System.Drawing.Point(17, 123);
            this.btnSymmetricDifference.Name = "btnSymmetricDifference";
            this.btnSymmetricDifference.Size = new System.Drawing.Size(108, 26);
            this.btnSymmetricDifference.TabIndex = 9;
            this.btnSymmetricDifference.Text = "SymmetricDifference";
            this.btnSymmetricDifference.UseVisualStyleBackColor = true;
            this.btnSymmetricDifference.Click += new System.EventHandler(this.btnSymmetricDifference_Click);
            // 
            // btnDifference
            // 
            this.btnDifference.Location = new System.Drawing.Point(17, 88);
            this.btnDifference.Name = "btnDifference";
            this.btnDifference.Size = new System.Drawing.Size(90, 26);
            this.btnDifference.TabIndex = 8;
            this.btnDifference.Text = "Difference";
            this.btnDifference.UseVisualStyleBackColor = true;
            this.btnDifference.Click += new System.EventHandler(this.btnDifference_Click);
            // 
            // btnUnion
            // 
            this.btnUnion.Location = new System.Drawing.Point(17, 54);
            this.btnUnion.Name = "btnUnion";
            this.btnUnion.Size = new System.Drawing.Size(90, 26);
            this.btnUnion.TabIndex = 7;
            this.btnUnion.Text = "Union";
            this.btnUnion.UseVisualStyleBackColor = true;
            this.btnUnion.Click += new System.EventHandler(this.btnUnion_Click);
            // 
            // btnIntersection
            // 
            this.btnIntersection.Location = new System.Drawing.Point(17, 20);
            this.btnIntersection.Name = "btnIntersection";
            this.btnIntersection.Size = new System.Drawing.Size(90, 26);
            this.btnIntersection.TabIndex = 6;
            this.btnIntersection.Text = "Intersection";
            this.btnIntersection.UseVisualStyleBackColor = true;
            this.btnIntersection.Click += new System.EventHandler(this.btnIntersection_Click);
            // 
            // cbCutWithZ
            // 
            this.cbCutWithZ.AutoSize = true;
            this.cbCutWithZ.Location = new System.Drawing.Point(26, 243);
            this.cbCutWithZ.Name = "cbCutWithZ";
            this.cbCutWithZ.Size = new System.Drawing.Size(84, 16);
            this.cbCutWithZ.TabIndex = 19;
            this.cbCutWithZ.Text = "带高度限制";
            this.cbCutWithZ.UseVisualStyleBackColor = true;
            // 
            // numMinZ
            // 
            this.numMinZ.Location = new System.Drawing.Point(26, 283);
            this.numMinZ.Name = "numMinZ";
            this.numMinZ.Size = new System.Drawing.Size(41, 21);
            this.numMinZ.TabIndex = 20;
            // 
            // numMaxZ
            // 
            this.numMaxZ.Location = new System.Drawing.Point(93, 283);
            this.numMaxZ.Name = "numMaxZ";
            this.numMaxZ.Size = new System.Drawing.Size(41, 21);
            this.numMaxZ.TabIndex = 21;
            // 
            // MinZL
            // 
            this.MinZL.AutoSize = true;
            this.MinZL.Location = new System.Drawing.Point(24, 266);
            this.MinZL.Name = "MinZL";
            this.MinZL.Size = new System.Drawing.Size(29, 12);
            this.MinZL.TabIndex = 22;
            this.MinZL.Text = "MinZ";
            // 
            // MaxZ
            // 
            this.MaxZ.AutoSize = true;
            this.MaxZ.Location = new System.Drawing.Point(91, 266);
            this.MaxZ.Name = "MaxZ";
            this.MaxZ.Size = new System.Drawing.Size(29, 12);
            this.MaxZ.TabIndex = 23;
            this.MaxZ.Text = "MaxZ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 572);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GeometryConvert3";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMinZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxZ)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbShowSrcModelPoint;
        private System.Windows.Forms.Button btnDelInteriorModelPoint;
        private System.Windows.Forms.Button btnDelExteriorModelPoint;
        private System.Windows.Forms.CheckBox cbHideInteriorModelPoint;
        private System.Windows.Forms.CheckBox cbHideExteriorModelPoint;
        private System.Windows.Forms.Button btnCreatePolygon;
        private System.Windows.Forms.Button btnSplitModelPoint;
        private System.Windows.Forms.Button btnCutModelPoint;
        private System.Windows.Forms.Button btnClearPolygon;
        private System.Windows.Forms.Button btnSymmetricDifference;
        private System.Windows.Forms.Button btnDifference;
        private System.Windows.Forms.Button btnUnion;
        private System.Windows.Forms.Button btnIntersection;
        private System.Windows.Forms.CheckBox cbHideModelPoint;
        private System.Windows.Forms.Button btnDelModelPoint;
        private System.Windows.Forms.CheckBox cbCutWithZ;
        private System.Windows.Forms.Label MinZL;
        private System.Windows.Forms.NumericUpDown numMaxZ;
        private System.Windows.Forms.NumericUpDown numMinZ;
        private System.Windows.Forms.Label MaxZ;

    }
}


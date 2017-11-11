namespace GeometryConvert
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
            this.btnDelRenderMultiPoint = new System.Windows.Forms.Button();
            this.btnDelRenderPolygon2 = new System.Windows.Forms.Button();
            this.btnDelRenderPolygon = new System.Windows.Forms.Button();
            this.btnDelRenderTMesh = new System.Windows.Forms.Button();
            this.cbShowModelPointLayer = new System.Windows.Forms.CheckBox();
            this.cbSelectFeature = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbCreateRenderMulPoint = new System.Windows.Forms.CheckBox();
            this.cbCreateRenderPolygon2 = new System.Windows.Forms.CheckBox();
            this.cbCreateRenderPolygon = new System.Windows.Forms.CheckBox();
            this.cbCreateRenderTriMesh = new System.Windows.Forms.CheckBox();
            this.cbHideRenderTMesh = new System.Windows.Forms.CheckBox();
            this.cbHideRenderPolygon = new System.Windows.Forms.CheckBox();
            this.cbHideRenderPolygon2 = new System.Windows.Forms.CheckBox();
            this.cbHideRenderMultiPoint = new System.Windows.Forms.CheckBox();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(512, 512);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 518);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbHideRenderMultiPoint);
            this.panel1.Controls.Add(this.cbHideRenderPolygon2);
            this.panel1.Controls.Add(this.cbHideRenderPolygon);
            this.panel1.Controls.Add(this.cbHideRenderTMesh);
            this.panel1.Controls.Add(this.btnDelRenderMultiPoint);
            this.panel1.Controls.Add(this.btnDelRenderPolygon2);
            this.panel1.Controls.Add(this.btnDelRenderPolygon);
            this.panel1.Controls.Add(this.btnDelRenderTMesh);
            this.panel1.Controls.Add(this.cbShowModelPointLayer);
            this.panel1.Controls.Add(this.cbSelectFeature);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(521, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(161, 512);
            this.panel1.TabIndex = 1;
            // 
            // btnDelRenderMultiPoint
            // 
            this.btnDelRenderMultiPoint.Location = new System.Drawing.Point(9, 435);
            this.btnDelRenderMultiPoint.Name = "btnDelRenderMultiPoint";
            this.btnDelRenderMultiPoint.Size = new System.Drawing.Size(147, 23);
            this.btnDelRenderMultiPoint.TabIndex = 8;
            this.btnDelRenderMultiPoint.Text = "删除界面上MultiPoint";
            this.btnDelRenderMultiPoint.UseVisualStyleBackColor = true;
            this.btnDelRenderMultiPoint.Click += new System.EventHandler(this.btnDelRenderMultiPoint_Click);
            // 
            // btnDelRenderPolygon2
            // 
            this.btnDelRenderPolygon2.Location = new System.Drawing.Point(9, 406);
            this.btnDelRenderPolygon2.Name = "btnDelRenderPolygon2";
            this.btnDelRenderPolygon2.Size = new System.Drawing.Size(147, 23);
            this.btnDelRenderPolygon2.TabIndex = 7;
            this.btnDelRenderPolygon2.Text = "删除界面上切割Polygon";
            this.btnDelRenderPolygon2.UseVisualStyleBackColor = true;
            this.btnDelRenderPolygon2.Click += new System.EventHandler(this.btnDelRenderPolygon2_Click);
            // 
            // btnDelRenderPolygon
            // 
            this.btnDelRenderPolygon.Location = new System.Drawing.Point(9, 377);
            this.btnDelRenderPolygon.Name = "btnDelRenderPolygon";
            this.btnDelRenderPolygon.Size = new System.Drawing.Size(147, 23);
            this.btnDelRenderPolygon.TabIndex = 6;
            this.btnDelRenderPolygon.Text = "删除界面上投影Polygon";
            this.btnDelRenderPolygon.UseVisualStyleBackColor = true;
            this.btnDelRenderPolygon.Click += new System.EventHandler(this.btnDelRenderPolygon_Click);
            // 
            // btnDelRenderTMesh
            // 
            this.btnDelRenderTMesh.Location = new System.Drawing.Point(9, 348);
            this.btnDelRenderTMesh.Name = "btnDelRenderTMesh";
            this.btnDelRenderTMesh.Size = new System.Drawing.Size(147, 23);
            this.btnDelRenderTMesh.TabIndex = 5;
            this.btnDelRenderTMesh.Text = "删除界面上MTriMesh";
            this.btnDelRenderTMesh.UseVisualStyleBackColor = true;
            this.btnDelRenderTMesh.Click += new System.EventHandler(this.btnDelRenderTMesh_Click);
            // 
            // cbShowModelPointLayer
            // 
            this.cbShowModelPointLayer.AutoSize = true;
            this.cbShowModelPointLayer.Checked = true;
            this.cbShowModelPointLayer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowModelPointLayer.Location = new System.Drawing.Point(9, 305);
            this.cbShowModelPointLayer.Name = "cbShowModelPointLayer";
            this.cbShowModelPointLayer.Size = new System.Drawing.Size(84, 16);
            this.cbShowModelPointLayer.TabIndex = 4;
            this.cbShowModelPointLayer.Text = "显示模型层";
            this.cbShowModelPointLayer.UseVisualStyleBackColor = true;
            this.cbShowModelPointLayer.CheckedChanged += new System.EventHandler(this.cbShowModelPointLayer_CheckedChanged);
            // 
            // cbSelectFeature
            // 
            this.cbSelectFeature.Location = new System.Drawing.Point(23, 138);
            this.cbSelectFeature.Name = "cbSelectFeature";
            this.cbSelectFeature.Size = new System.Drawing.Size(112, 34);
            this.cbSelectFeature.TabIndex = 3;
            this.cbSelectFeature.Text = "进入选择模式 选择转换物体";
            this.cbSelectFeature.UseVisualStyleBackColor = true;
            this.cbSelectFeature.CheckedChanged += new System.EventHandler(this.cbSelectFeature_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbCreateRenderMulPoint);
            this.groupBox1.Controls.Add(this.cbCreateRenderPolygon2);
            this.groupBox1.Controls.Add(this.cbCreateRenderPolygon);
            this.groupBox1.Controls.Add(this.cbCreateRenderTriMesh);
            this.groupBox1.Location = new System.Drawing.Point(9, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(132, 114);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ConvertType";
            // 
            // cbCreateRenderMulPoint
            // 
            this.cbCreateRenderMulPoint.AutoSize = true;
            this.cbCreateRenderMulPoint.Location = new System.Drawing.Point(16, 85);
            this.cbCreateRenderMulPoint.Name = "cbCreateRenderMulPoint";
            this.cbCreateRenderMulPoint.Size = new System.Drawing.Size(84, 16);
            this.cbCreateRenderMulPoint.TabIndex = 3;
            this.cbCreateRenderMulPoint.Text = "MultiPoint";
            this.cbCreateRenderMulPoint.UseVisualStyleBackColor = true;
            // 
            // cbCreateRenderPolygon2
            // 
            this.cbCreateRenderPolygon2.AutoSize = true;
            this.cbCreateRenderPolygon2.Location = new System.Drawing.Point(16, 63);
            this.cbCreateRenderPolygon2.Name = "cbCreateRenderPolygon2";
            this.cbCreateRenderPolygon2.Size = new System.Drawing.Size(90, 16);
            this.cbCreateRenderPolygon2.TabIndex = 2;
            this.cbCreateRenderPolygon2.Text = "切割Polygon";
            this.cbCreateRenderPolygon2.UseVisualStyleBackColor = true;
            // 
            // cbCreateRenderPolygon
            // 
            this.cbCreateRenderPolygon.AutoSize = true;
            this.cbCreateRenderPolygon.Location = new System.Drawing.Point(16, 41);
            this.cbCreateRenderPolygon.Name = "cbCreateRenderPolygon";
            this.cbCreateRenderPolygon.Size = new System.Drawing.Size(90, 16);
            this.cbCreateRenderPolygon.TabIndex = 1;
            this.cbCreateRenderPolygon.Text = "投影Polygon";
            this.cbCreateRenderPolygon.UseVisualStyleBackColor = true;
            // 
            // cbCreateRenderTriMesh
            // 
            this.cbCreateRenderTriMesh.AutoSize = true;
            this.cbCreateRenderTriMesh.Checked = true;
            this.cbCreateRenderTriMesh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCreateRenderTriMesh.Location = new System.Drawing.Point(16, 19);
            this.cbCreateRenderTriMesh.Name = "cbCreateRenderTriMesh";
            this.cbCreateRenderTriMesh.Size = new System.Drawing.Size(96, 16);
            this.cbCreateRenderTriMesh.TabIndex = 0;
            this.cbCreateRenderTriMesh.Text = "MultiTriMesh";
            this.cbCreateRenderTriMesh.UseVisualStyleBackColor = true;
            // 
            // cbHideRenderTMesh
            // 
            this.cbHideRenderTMesh.AutoSize = true;
            this.cbHideRenderTMesh.Location = new System.Drawing.Point(9, 202);
            this.cbHideRenderTMesh.Name = "cbHideRenderTMesh";
            this.cbHideRenderTMesh.Size = new System.Drawing.Size(132, 16);
            this.cbHideRenderTMesh.TabIndex = 9;
            this.cbHideRenderTMesh.Text = "隐藏界面上MTriMesh";
            this.cbHideRenderTMesh.UseVisualStyleBackColor = true;
            this.cbHideRenderTMesh.CheckedChanged += new System.EventHandler(this.cbHideRenderTMesh_CheckedChanged);
            // 
            // cbHideRenderPolygon
            // 
            this.cbHideRenderPolygon.AutoSize = true;
            this.cbHideRenderPolygon.Location = new System.Drawing.Point(9, 224);
            this.cbHideRenderPolygon.Name = "cbHideRenderPolygon";
            this.cbHideRenderPolygon.Size = new System.Drawing.Size(150, 16);
            this.cbHideRenderPolygon.TabIndex = 10;
            this.cbHideRenderPolygon.Text = "隐藏界面上投影Polygon";
            this.cbHideRenderPolygon.UseVisualStyleBackColor = true;
            this.cbHideRenderPolygon.CheckedChanged += new System.EventHandler(this.cbHideRenderPolygon_CheckedChanged);
            // 
            // cbHideRenderPolygon2
            // 
            this.cbHideRenderPolygon2.AutoSize = true;
            this.cbHideRenderPolygon2.Location = new System.Drawing.Point(9, 246);
            this.cbHideRenderPolygon2.Name = "cbHideRenderPolygon2";
            this.cbHideRenderPolygon2.Size = new System.Drawing.Size(150, 16);
            this.cbHideRenderPolygon2.TabIndex = 11;
            this.cbHideRenderPolygon2.Text = "隐藏界面上切割Polygon";
            this.cbHideRenderPolygon2.UseVisualStyleBackColor = true;
            this.cbHideRenderPolygon2.CheckedChanged += new System.EventHandler(this.cbHideRenderPolygon2_CheckedChanged);
            // 
            // cbHideRenderMultiPoint
            // 
            this.cbHideRenderMultiPoint.AutoSize = true;
            this.cbHideRenderMultiPoint.Location = new System.Drawing.Point(9, 268);
            this.cbHideRenderMultiPoint.Name = "cbHideRenderMultiPoint";
            this.cbHideRenderMultiPoint.Size = new System.Drawing.Size(144, 16);
            this.cbHideRenderMultiPoint.TabIndex = 12;
            this.cbHideRenderMultiPoint.Text = "隐藏界面上MultiPoint";
            this.cbHideRenderMultiPoint.UseVisualStyleBackColor = true;
            this.cbHideRenderMultiPoint.CheckedChanged += new System.EventHandler(this.cbHideRenderMultiPoint_CheckedChanged);
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
            this.Text = "GeometryConvert";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbCreateRenderTriMesh;
        private System.Windows.Forms.CheckBox cbCreateRenderPolygon;
        private System.Windows.Forms.CheckBox cbCreateRenderPolygon2;
        private System.Windows.Forms.CheckBox cbCreateRenderMulPoint;
        private System.Windows.Forms.CheckBox cbSelectFeature;
        private System.Windows.Forms.CheckBox cbShowModelPointLayer;
        private System.Windows.Forms.Button btnDelRenderTMesh;
        private System.Windows.Forms.Button btnDelRenderPolygon;
        private System.Windows.Forms.Button btnDelRenderPolygon2;
        private System.Windows.Forms.Button btnDelRenderMultiPoint;
        private System.Windows.Forms.CheckBox cbHideRenderTMesh;
        private System.Windows.Forms.CheckBox cbHideRenderPolygon;
        private System.Windows.Forms.CheckBox cbHideRenderPolygon2;
        private System.Windows.Forms.CheckBox cbHideRenderMultiPoint;

    }
}


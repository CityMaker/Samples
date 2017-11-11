namespace GeometryEdit
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
            this.btnCancelEdit = new System.Windows.Forms.Button();
            this.cbBeginEdit = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbBoxScale = new System.Windows.Forms.RadioButton();
            this.rbZscale = new System.Windows.Forms.RadioButton();
            this.rbZrotate = new System.Windows.Forms.RadioButton();
            this.rbVertex = new System.Windows.Forms.RadioButton();
            this.rbScale = new System.Windows.Forms.RadioButton();
            this.rbRotate = new System.Windows.Forms.RadioButton();
            this.rbMove = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCreateFeature = new System.Windows.Forms.Button();
            this.btnCreatePolygon = new System.Windows.Forms.Button();
            this.btnCreatePolyline = new System.Windows.Forms.Button();
            this.btnCreatePoint = new System.Windows.Forms.Button();
            this.btnCreateModelPoint = new System.Windows.Forms.Button();
            this.btnFlyToFeatureLayer = new System.Windows.Forms.Button();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(529, 512);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
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
            this.panel1.Controls.Add(this.btnCancelEdit);
            this.panel1.Controls.Add(this.cbBeginEdit);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnFlyToFeatureLayer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(538, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 512);
            this.panel1.TabIndex = 1;
            // 
            // btnCancelEdit
            // 
            this.btnCancelEdit.Location = new System.Drawing.Point(15, 473);
            this.btnCancelEdit.Name = "btnCancelEdit";
            this.btnCancelEdit.Size = new System.Drawing.Size(120, 27);
            this.btnCancelEdit.TabIndex = 7;
            this.btnCancelEdit.Text = "CancelEdit";
            this.btnCancelEdit.UseVisualStyleBackColor = true;
            this.btnCancelEdit.Click += new System.EventHandler(this.btnCancelEdit_Click);
            // 
            // cbBeginEdit
            // 
            this.cbBeginEdit.AutoSize = true;
            this.cbBeginEdit.Location = new System.Drawing.Point(24, 260);
            this.cbBeginEdit.Name = "cbBeginEdit";
            this.cbBeginEdit.Size = new System.Drawing.Size(72, 16);
            this.cbBeginEdit.TabIndex = 6;
            this.cbBeginEdit.Text = "我要编辑";
            this.cbBeginEdit.UseVisualStyleBackColor = true;
            this.cbBeginEdit.CheckedChanged += new System.EventHandler(this.cbBeginEdit_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbBoxScale);
            this.groupBox2.Controls.Add(this.rbZscale);
            this.groupBox2.Controls.Add(this.rbZrotate);
            this.groupBox2.Controls.Add(this.rbVertex);
            this.groupBox2.Controls.Add(this.rbScale);
            this.groupBox2.Controls.Add(this.rbRotate);
            this.groupBox2.Controls.Add(this.rbMove);
            this.groupBox2.Location = new System.Drawing.Point(5, 282);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(132, 174);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "EditType";
            // 
            // rbBoxScale
            // 
            this.rbBoxScale.AutoSize = true;
            this.rbBoxScale.Location = new System.Drawing.Point(19, 152);
            this.rbBoxScale.Name = "rbBoxScale";
            this.rbBoxScale.Size = new System.Drawing.Size(83, 16);
            this.rbBoxScale.TabIndex = 6;
            this.rbBoxScale.TabStop = true;
            this.rbBoxScale.Text = "包围盒缩放";
            this.rbBoxScale.UseVisualStyleBackColor = true;
            this.rbBoxScale.CheckedChanged += new System.EventHandler(this.rbBoxScale_CheckedChanged);
            // 
            // rbZscale
            // 
            this.rbZscale.AutoSize = true;
            this.rbZscale.Location = new System.Drawing.Point(19, 130);
            this.rbZscale.Name = "rbZscale";
            this.rbZscale.Size = new System.Drawing.Size(77, 16);
            this.rbZscale.TabIndex = 5;
            this.rbZscale.TabStop = true;
            this.rbZscale.Text = "绕Z轴缩放";
            this.rbZscale.UseVisualStyleBackColor = true;
            this.rbZscale.CheckedChanged += new System.EventHandler(this.rbZscale_CheckedChanged);
            // 
            // rbZrotate
            // 
            this.rbZrotate.AutoSize = true;
            this.rbZrotate.Location = new System.Drawing.Point(19, 108);
            this.rbZrotate.Name = "rbZrotate";
            this.rbZrotate.Size = new System.Drawing.Size(77, 16);
            this.rbZrotate.TabIndex = 4;
            this.rbZrotate.TabStop = true;
            this.rbZrotate.Text = "绕Z轴旋转";
            this.rbZrotate.UseVisualStyleBackColor = true;
            this.rbZrotate.CheckedChanged += new System.EventHandler(this.rbZrotate_CheckedChanged);
            // 
            // rbVertex
            // 
            this.rbVertex.AutoSize = true;
            this.rbVertex.Location = new System.Drawing.Point(19, 86);
            this.rbVertex.Name = "rbVertex";
            this.rbVertex.Size = new System.Drawing.Size(71, 16);
            this.rbVertex.TabIndex = 3;
            this.rbVertex.TabStop = true;
            this.rbVertex.Text = "顶点编辑";
            this.rbVertex.UseVisualStyleBackColor = true;
            this.rbVertex.CheckedChanged += new System.EventHandler(this.rbVertex_CheckedChanged);
            // 
            // rbScale
            // 
            this.rbScale.AutoSize = true;
            this.rbScale.Location = new System.Drawing.Point(19, 64);
            this.rbScale.Name = "rbScale";
            this.rbScale.Size = new System.Drawing.Size(47, 16);
            this.rbScale.TabIndex = 2;
            this.rbScale.TabStop = true;
            this.rbScale.Text = "缩放";
            this.rbScale.UseVisualStyleBackColor = true;
            this.rbScale.CheckedChanged += new System.EventHandler(this.rbScale_CheckedChanged);
            // 
            // rbRotate
            // 
            this.rbRotate.AutoSize = true;
            this.rbRotate.Location = new System.Drawing.Point(19, 42);
            this.rbRotate.Name = "rbRotate";
            this.rbRotate.Size = new System.Drawing.Size(47, 16);
            this.rbRotate.TabIndex = 1;
            this.rbRotate.TabStop = true;
            this.rbRotate.Text = "旋转";
            this.rbRotate.UseVisualStyleBackColor = true;
            this.rbRotate.CheckedChanged += new System.EventHandler(this.rbRotate_CheckedChanged);
            // 
            // rbMove
            // 
            this.rbMove.AutoSize = true;
            this.rbMove.Checked = true;
            this.rbMove.Location = new System.Drawing.Point(19, 20);
            this.rbMove.Name = "rbMove";
            this.rbMove.Size = new System.Drawing.Size(47, 16);
            this.rbMove.TabIndex = 0;
            this.rbMove.TabStop = true;
            this.rbMove.Text = "平移";
            this.rbMove.UseVisualStyleBackColor = true;
            this.rbMove.CheckedChanged += new System.EventHandler(this.rbMove_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCreateFeature);
            this.groupBox1.Controls.Add(this.btnCreatePolygon);
            this.groupBox1.Controls.Add(this.btnCreatePolyline);
            this.groupBox1.Controls.Add(this.btnCreatePoint);
            this.groupBox1.Controls.Add(this.btnCreateModelPoint);
            this.groupBox1.Location = new System.Drawing.Point(5, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(132, 162);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CreateType";
            // 
            // btnCreateFeature
            // 
            this.btnCreateFeature.Location = new System.Drawing.Point(17, 133);
            this.btnCreateFeature.Name = "btnCreateFeature";
            this.btnCreateFeature.Size = new System.Drawing.Size(91, 23);
            this.btnCreateFeature.TabIndex = 4;
            this.btnCreateFeature.Text = "CreateFeature";
            this.btnCreateFeature.UseVisualStyleBackColor = true;
            this.btnCreateFeature.Click += new System.EventHandler(this.btnCreateFeature_Click);
            // 
            // btnCreatePolygon
            // 
            this.btnCreatePolygon.Location = new System.Drawing.Point(17, 107);
            this.btnCreatePolygon.Name = "btnCreatePolygon";
            this.btnCreatePolygon.Size = new System.Drawing.Size(91, 23);
            this.btnCreatePolygon.TabIndex = 3;
            this.btnCreatePolygon.Text = "CreatePolygon";
            this.btnCreatePolygon.UseVisualStyleBackColor = true;
            this.btnCreatePolygon.Click += new System.EventHandler(this.btnCreatePolygon_Click);
            // 
            // btnCreatePolyline
            // 
            this.btnCreatePolyline.Location = new System.Drawing.Point(17, 78);
            this.btnCreatePolyline.Name = "btnCreatePolyline";
            this.btnCreatePolyline.Size = new System.Drawing.Size(100, 23);
            this.btnCreatePolyline.TabIndex = 2;
            this.btnCreatePolyline.Text = "CreatePolyline";
            this.btnCreatePolyline.UseVisualStyleBackColor = true;
            this.btnCreatePolyline.Click += new System.EventHandler(this.btnCreatePolyline_Click);
            // 
            // btnCreatePoint
            // 
            this.btnCreatePoint.Location = new System.Drawing.Point(17, 49);
            this.btnCreatePoint.Name = "btnCreatePoint";
            this.btnCreatePoint.Size = new System.Drawing.Size(91, 23);
            this.btnCreatePoint.TabIndex = 1;
            this.btnCreatePoint.Text = "CreatePoint";
            this.btnCreatePoint.UseVisualStyleBackColor = true;
            this.btnCreatePoint.Click += new System.EventHandler(this.btnCreatePoint_Click);
            // 
            // btnCreateModelPoint
            // 
            this.btnCreateModelPoint.Location = new System.Drawing.Point(17, 20);
            this.btnCreateModelPoint.Name = "btnCreateModelPoint";
            this.btnCreateModelPoint.Size = new System.Drawing.Size(109, 23);
            this.btnCreateModelPoint.TabIndex = 0;
            this.btnCreateModelPoint.Text = "CreateModelPoint";
            this.btnCreateModelPoint.UseVisualStyleBackColor = true;
            this.btnCreateModelPoint.Click += new System.EventHandler(this.btnCreateModelPoint_Click);
            // 
            // btnFlyToFeatureLayer
            // 
            this.btnFlyToFeatureLayer.Location = new System.Drawing.Point(15, 5);
            this.btnFlyToFeatureLayer.Name = "btnFlyToFeatureLayer";
            this.btnFlyToFeatureLayer.Size = new System.Drawing.Size(120, 27);
            this.btnFlyToFeatureLayer.TabIndex = 0;
            this.btnFlyToFeatureLayer.Text = "FlyToFeatureLayer";
            this.btnFlyToFeatureLayer.UseVisualStyleBackColor = true;
            this.btnFlyToFeatureLayer.Click += new System.EventHandler(this.btnFlyToFeatureLayer_Click);
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
            this.Text = "GeometryEditor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFlyToFeatureLayer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCreateModelPoint;
        private System.Windows.Forms.Button btnCreatePoint;
        private System.Windows.Forms.Button btnCreatePolyline;
        private System.Windows.Forms.Button btnCreatePolygon;
        private System.Windows.Forms.Button btnCreateFeature;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbMove;
        private System.Windows.Forms.RadioButton rbRotate;
        private System.Windows.Forms.RadioButton rbScale;
        private System.Windows.Forms.RadioButton rbVertex;
        private System.Windows.Forms.RadioButton rbZrotate;
        private System.Windows.Forms.RadioButton rbZscale;
        private System.Windows.Forms.CheckBox cbBeginEdit;
        private System.Windows.Forms.Button btnCancelEdit;
        private System.Windows.Forms.RadioButton rbBoxScale;

    }
}


namespace CreateGeometry
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
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnConstructCircle = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnConstructCirculeArc = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnConstructClosedTriMesh = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnConstructCompoundLine = new System.Windows.Forms.Button();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.btnConstructLine = new System.Windows.Forms.Button();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.btnConstructModelPoint = new System.Windows.Forms.Button();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.btnConstructMultiPoint = new System.Windows.Forms.Button();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.btnConstructMultiPolygon = new System.Windows.Forms.Button();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.btnConstructMultiPolyline = new System.Windows.Forms.Button();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.btnConstructMultiTriMesh = new System.Windows.Forms.Button();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this.btnConstructPoint = new System.Windows.Forms.Button();
            this.tabPage17 = new System.Windows.Forms.TabPage();
            this.btnConstructPolygon = new System.Windows.Forms.Button();
            this.tabPage18 = new System.Windows.Forms.TabPage();
            this.btnConstructPolyline = new System.Windows.Forms.Button();
            this.tabPage20 = new System.Windows.Forms.TabPage();
            this.btnConstructTriMesh = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.tabPage16.SuspendLayout();
            this.tabPage17.SuspendLayout();
            this.tabPage18.SuspendLayout();
            this.tabPage20.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Controls.Add(this.tabPage11);
            this.tabControl1.Controls.Add(this.tabPage12);
            this.tabControl1.Controls.Add(this.tabPage13);
            this.tabControl1.Controls.Add(this.tabPage15);
            this.tabControl1.Controls.Add(this.tabPage16);
            this.tabControl1.Controls.Add(this.tabPage17);
            this.tabControl1.Controls.Add(this.tabPage18);
            this.tabControl1.Controls.Add(this.tabPage20);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(294, 529);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnConstructCircle);
            this.tabPage1.Location = new System.Drawing.Point(4, 94);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(286, 431);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Circle";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnConstructCircle
            // 
            this.btnConstructCircle.Location = new System.Drawing.Point(26, 28);
            this.btnConstructCircle.Name = "btnConstructCircle";
            this.btnConstructCircle.Size = new System.Drawing.Size(194, 28);
            this.btnConstructCircle.TabIndex = 0;
            this.btnConstructCircle.Text = "ConstructFromCenterAndRadius";
            this.btnConstructCircle.UseVisualStyleBackColor = true;
            this.btnConstructCircle.Click += new System.EventHandler(this.btnConstructCircle_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnConstructCirculeArc);
            this.tabPage2.Location = new System.Drawing.Point(4, 112);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(286, 413);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CirculeArc";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnConstructCirculeArc
            // 
            this.btnConstructCirculeArc.Location = new System.Drawing.Point(28, 26);
            this.btnConstructCirculeArc.Name = "btnConstructCirculeArc";
            this.btnConstructCirculeArc.Size = new System.Drawing.Size(137, 28);
            this.btnConstructCirculeArc.TabIndex = 1;
            this.btnConstructCirculeArc.Text = "ConstructThreePoints";
            this.btnConstructCirculeArc.UseVisualStyleBackColor = true;
            this.btnConstructCirculeArc.Click += new System.EventHandler(this.btnConstructCirculeArc_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnConstructClosedTriMesh);
            this.tabPage3.Location = new System.Drawing.Point(4, 112);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(286, 413);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "ClosedTriMesh";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnConstructClosedTriMesh
            // 
            this.btnConstructClosedTriMesh.Location = new System.Drawing.Point(38, 38);
            this.btnConstructClosedTriMesh.Name = "btnConstructClosedTriMesh";
            this.btnConstructClosedTriMesh.Size = new System.Drawing.Size(86, 28);
            this.btnConstructClosedTriMesh.TabIndex = 9;
            this.btnConstructClosedTriMesh.Text = "Construct";
            this.btnConstructClosedTriMesh.UseVisualStyleBackColor = true;
            this.btnConstructClosedTriMesh.Click += new System.EventHandler(this.btnConstructClosedTriMesh_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnConstructCompoundLine);
            this.tabPage4.Location = new System.Drawing.Point(4, 112);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(286, 413);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "CompoundLine";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnConstructCompoundLine
            // 
            this.btnConstructCompoundLine.Location = new System.Drawing.Point(39, 40);
            this.btnConstructCompoundLine.Name = "btnConstructCompoundLine";
            this.btnConstructCompoundLine.Size = new System.Drawing.Size(86, 28);
            this.btnConstructCompoundLine.TabIndex = 4;
            this.btnConstructCompoundLine.Text = "Construct";
            this.btnConstructCompoundLine.UseVisualStyleBackColor = true;
            this.btnConstructCompoundLine.Click += new System.EventHandler(this.btnConstructCompoundLine_Click);
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 112);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(286, 413);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "GeoCollection";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.btnConstructLine);
            this.tabPage9.Location = new System.Drawing.Point(4, 112);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(286, 413);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "Line";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // btnConstructLine
            // 
            this.btnConstructLine.Location = new System.Drawing.Point(29, 35);
            this.btnConstructLine.Name = "btnConstructLine";
            this.btnConstructLine.Size = new System.Drawing.Size(86, 28);
            this.btnConstructLine.TabIndex = 3;
            this.btnConstructLine.Text = "Construct";
            this.btnConstructLine.UseVisualStyleBackColor = true;
            this.btnConstructLine.Click += new System.EventHandler(this.btnConstructLine_Click);
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.btnConstructModelPoint);
            this.tabPage10.Location = new System.Drawing.Point(4, 112);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(286, 413);
            this.tabPage10.TabIndex = 9;
            this.tabPage10.Text = "ModelPoint";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // btnConstructModelPoint
            // 
            this.btnConstructModelPoint.Location = new System.Drawing.Point(28, 33);
            this.btnConstructModelPoint.Name = "btnConstructModelPoint";
            this.btnConstructModelPoint.Size = new System.Drawing.Size(93, 28);
            this.btnConstructModelPoint.TabIndex = 2;
            this.btnConstructModelPoint.Text = "Construct";
            this.btnConstructModelPoint.UseVisualStyleBackColor = true;
            this.btnConstructModelPoint.Click += new System.EventHandler(this.btnConstructModelPoint_Click);
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.btnConstructMultiPoint);
            this.tabPage11.Location = new System.Drawing.Point(4, 112);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(286, 413);
            this.tabPage11.TabIndex = 10;
            this.tabPage11.Text = "MultiPoint";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // btnConstructMultiPoint
            // 
            this.btnConstructMultiPoint.Location = new System.Drawing.Point(42, 46);
            this.btnConstructMultiPoint.Name = "btnConstructMultiPoint";
            this.btnConstructMultiPoint.Size = new System.Drawing.Size(86, 28);
            this.btnConstructMultiPoint.TabIndex = 10;
            this.btnConstructMultiPoint.Text = "Construct";
            this.btnConstructMultiPoint.UseVisualStyleBackColor = true;
            this.btnConstructMultiPoint.Click += new System.EventHandler(this.btnConstructMultiPoint_Click);
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.btnConstructMultiPolygon);
            this.tabPage12.Location = new System.Drawing.Point(4, 112);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(286, 413);
            this.tabPage12.TabIndex = 11;
            this.tabPage12.Text = "MultiPolygon";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // btnConstructMultiPolygon
            // 
            this.btnConstructMultiPolygon.Location = new System.Drawing.Point(46, 64);
            this.btnConstructMultiPolygon.Name = "btnConstructMultiPolygon";
            this.btnConstructMultiPolygon.Size = new System.Drawing.Size(86, 28);
            this.btnConstructMultiPolygon.TabIndex = 12;
            this.btnConstructMultiPolygon.Text = "Construct";
            this.btnConstructMultiPolygon.UseVisualStyleBackColor = true;
            this.btnConstructMultiPolygon.Click += new System.EventHandler(this.btnConstructMultiPolygon_Click);
            // 
            // tabPage13
            // 
            this.tabPage13.Controls.Add(this.btnConstructMultiPolyline);
            this.tabPage13.Location = new System.Drawing.Point(4, 112);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage13.Size = new System.Drawing.Size(286, 413);
            this.tabPage13.TabIndex = 12;
            this.tabPage13.Text = "MultiPolyline";
            this.tabPage13.UseVisualStyleBackColor = true;
            // 
            // btnConstructMultiPolyline
            // 
            this.btnConstructMultiPolyline.Location = new System.Drawing.Point(47, 57);
            this.btnConstructMultiPolyline.Name = "btnConstructMultiPolyline";
            this.btnConstructMultiPolyline.Size = new System.Drawing.Size(86, 28);
            this.btnConstructMultiPolyline.TabIndex = 11;
            this.btnConstructMultiPolyline.Text = "Construct";
            this.btnConstructMultiPolyline.UseVisualStyleBackColor = true;
            this.btnConstructMultiPolyline.Click += new System.EventHandler(this.btnConstructMultiPolyline_Click);
            // 
            // tabPage15
            // 
            this.tabPage15.Controls.Add(this.btnConstructMultiTriMesh);
            this.tabPage15.Location = new System.Drawing.Point(4, 112);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage15.Size = new System.Drawing.Size(286, 413);
            this.tabPage15.TabIndex = 14;
            this.tabPage15.Text = "MultiTriMesh";
            this.tabPage15.UseVisualStyleBackColor = true;
            // 
            // btnConstructMultiTriMesh
            // 
            this.btnConstructMultiTriMesh.Location = new System.Drawing.Point(37, 44);
            this.btnConstructMultiTriMesh.Name = "btnConstructMultiTriMesh";
            this.btnConstructMultiTriMesh.Size = new System.Drawing.Size(86, 28);
            this.btnConstructMultiTriMesh.TabIndex = 13;
            this.btnConstructMultiTriMesh.Text = "Construct";
            this.btnConstructMultiTriMesh.UseVisualStyleBackColor = true;
            this.btnConstructMultiTriMesh.Click += new System.EventHandler(this.btnConstructMultiTriMesh_Click);
            // 
            // tabPage16
            // 
            this.tabPage16.Controls.Add(this.btnConstructPoint);
            this.tabPage16.Location = new System.Drawing.Point(4, 112);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage16.Size = new System.Drawing.Size(286, 413);
            this.tabPage16.TabIndex = 15;
            this.tabPage16.Text = "Point";
            this.tabPage16.UseVisualStyleBackColor = true;
            // 
            // btnConstructPoint
            // 
            this.btnConstructPoint.Location = new System.Drawing.Point(21, 35);
            this.btnConstructPoint.Name = "btnConstructPoint";
            this.btnConstructPoint.Size = new System.Drawing.Size(93, 28);
            this.btnConstructPoint.TabIndex = 1;
            this.btnConstructPoint.Text = "Construct";
            this.btnConstructPoint.UseVisualStyleBackColor = true;
            this.btnConstructPoint.Click += new System.EventHandler(this.btnConstructPoint_Click);
            // 
            // tabPage17
            // 
            this.tabPage17.Controls.Add(this.btnConstructPolygon);
            this.tabPage17.Location = new System.Drawing.Point(4, 112);
            this.tabPage17.Name = "tabPage17";
            this.tabPage17.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage17.Size = new System.Drawing.Size(286, 413);
            this.tabPage17.TabIndex = 16;
            this.tabPage17.Text = "Polygon";
            this.tabPage17.UseVisualStyleBackColor = true;
            // 
            // btnConstructPolygon
            // 
            this.btnConstructPolygon.Location = new System.Drawing.Point(36, 38);
            this.btnConstructPolygon.Name = "btnConstructPolygon";
            this.btnConstructPolygon.Size = new System.Drawing.Size(86, 28);
            this.btnConstructPolygon.TabIndex = 6;
            this.btnConstructPolygon.Text = "Construct";
            this.btnConstructPolygon.UseVisualStyleBackColor = true;
            this.btnConstructPolygon.Click += new System.EventHandler(this.btnConstructPolygon_Click);
            // 
            // tabPage18
            // 
            this.tabPage18.Controls.Add(this.btnConstructPolyline);
            this.tabPage18.Location = new System.Drawing.Point(4, 112);
            this.tabPage18.Name = "tabPage18";
            this.tabPage18.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage18.Size = new System.Drawing.Size(286, 413);
            this.tabPage18.TabIndex = 17;
            this.tabPage18.Text = "PolyLine";
            this.tabPage18.UseVisualStyleBackColor = true;
            // 
            // btnConstructPolyline
            // 
            this.btnConstructPolyline.Location = new System.Drawing.Point(39, 41);
            this.btnConstructPolyline.Name = "btnConstructPolyline";
            this.btnConstructPolyline.Size = new System.Drawing.Size(86, 28);
            this.btnConstructPolyline.TabIndex = 5;
            this.btnConstructPolyline.Text = "Construct";
            this.btnConstructPolyline.UseVisualStyleBackColor = true;
            this.btnConstructPolyline.Click += new System.EventHandler(this.btnConstructPolyline_Click);
            // 
            // tabPage20
            // 
            this.tabPage20.Controls.Add(this.btnConstructTriMesh);
            this.tabPage20.Location = new System.Drawing.Point(4, 94);
            this.tabPage20.Name = "tabPage20";
            this.tabPage20.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage20.Size = new System.Drawing.Size(286, 431);
            this.tabPage20.TabIndex = 19;
            this.tabPage20.Text = "TriMesh";
            this.tabPage20.UseVisualStyleBackColor = true;
            // 
            // btnConstructTriMesh
            // 
            this.btnConstructTriMesh.Location = new System.Drawing.Point(31, 36);
            this.btnConstructTriMesh.Name = "btnConstructTriMesh";
            this.btnConstructTriMesh.Size = new System.Drawing.Size(86, 28);
            this.btnConstructTriMesh.TabIndex = 8;
            this.btnConstructTriMesh.Text = "Construct";
            this.btnConstructTriMesh.UseVisualStyleBackColor = true;
            this.btnConstructTriMesh.Click += new System.EventHandler(this.btnConstructTriMesh_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(303, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.Size = new System.Drawing.Size(394, 529);
            this.propertyGrid1.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.propertyGrid1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(700, 535);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 94);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(286, 431);
            this.tabPage5.TabIndex = 20;
            this.tabPage5.Text = "Ring";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 535);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreateGeometry";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.tabPage11.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage13.ResumeLayout(false);
            this.tabPage15.ResumeLayout(false);
            this.tabPage16.ResumeLayout(false);
            this.tabPage17.ResumeLayout(false);
            this.tabPage18.ResumeLayout(false);
            this.tabPage20.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.TabPage tabPage12;
        private System.Windows.Forms.TabPage tabPage13;
        private System.Windows.Forms.TabPage tabPage15;
        private System.Windows.Forms.TabPage tabPage16;
        private System.Windows.Forms.TabPage tabPage17;
        private System.Windows.Forms.TabPage tabPage18;
        private System.Windows.Forms.TabPage tabPage20;
        private System.Windows.Forms.Button btnConstructCircle;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnConstructCirculeArc;
        private System.Windows.Forms.Button btnConstructPoint;
        private System.Windows.Forms.Button btnConstructModelPoint;
        private System.Windows.Forms.Button btnConstructLine;
        private System.Windows.Forms.Button btnConstructCompoundLine;
        private System.Windows.Forms.Button btnConstructPolyline;
        private System.Windows.Forms.Button btnConstructPolygon;
        private System.Windows.Forms.Button btnConstructTriMesh;
        private System.Windows.Forms.Button btnConstructClosedTriMesh;
        private System.Windows.Forms.Button btnConstructMultiPoint;
        private System.Windows.Forms.Button btnConstructMultiPolyline;
        private System.Windows.Forms.Button btnConstructMultiPolygon;
        private System.Windows.Forms.Button btnConstructMultiTriMesh;
        private System.Windows.Forms.TabPage tabPage5;
    }
}


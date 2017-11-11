namespace SpatialQuery2
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
            this.btnFlyToSourcePoint = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.FID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFlyToTargetPoint = new System.Windows.Forms.Button();
            this.endZ = new System.Windows.Forms.TextBox();
            this.endY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.endX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.startZ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.startY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.startX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_createLine = new System.Windows.Forms.Button();
            this.btn_analyse = new System.Windows.Forms.Button();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(881, 585);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFlyToSourcePoint);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btn_createLine);
            this.panel1.Controls.Add(this.btn_analyse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(664, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 579);
            this.panel1.TabIndex = 1;
            // 
            // btnFlyToSourcePoint
            // 
            this.btnFlyToSourcePoint.Location = new System.Drawing.Point(130, 52);
            this.btnFlyToSourcePoint.Name = "btnFlyToSourcePoint";
            this.btnFlyToSourcePoint.Size = new System.Drawing.Size(75, 23);
            this.btnFlyToSourcePoint.TabIndex = 6;
            this.btnFlyToSourcePoint.Text = "飞入观察点";
            this.btnFlyToSourcePoint.UseVisualStyleBackColor = true;
            this.btnFlyToSourcePoint.Click += new System.EventHandler(this.btnFlyToSourcePoint_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(13, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "按F1显示操作指南";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(4, 309);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(207, 261);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "遮挡物";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FID});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(201, 241);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // FID
            // 
            this.FID.HeaderText = "OID";
            this.FID.Name = "FID";
            this.FID.ReadOnly = true;
            this.FID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnFlyToTargetPoint);
            this.groupBox2.Controls.Add(this.endZ);
            this.groupBox2.Controls.Add(this.endY);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.endX);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(2, 177);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(207, 125);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "目标点";
            // 
            // btnFlyToTargetPoint
            // 
            this.btnFlyToTargetPoint.Location = new System.Drawing.Point(128, 3);
            this.btnFlyToTargetPoint.Name = "btnFlyToTargetPoint";
            this.btnFlyToTargetPoint.Size = new System.Drawing.Size(75, 23);
            this.btnFlyToTargetPoint.TabIndex = 7;
            this.btnFlyToTargetPoint.Text = "飞入目标点";
            this.btnFlyToTargetPoint.UseVisualStyleBackColor = true;
            this.btnFlyToTargetPoint.Click += new System.EventHandler(this.btnFlyToTargetPoint_Click);
            // 
            // endZ
            // 
            this.endZ.Enabled = false;
            this.endZ.Location = new System.Drawing.Point(30, 94);
            this.endZ.Name = "endZ";
            this.endZ.Size = new System.Drawing.Size(147, 21);
            this.endZ.TabIndex = 11;
            this.endZ.TextChanged += new System.EventHandler(this.startX_TextChanged);
            // 
            // endY
            // 
            this.endY.Enabled = false;
            this.endY.Location = new System.Drawing.Point(30, 60);
            this.endY.Name = "endY";
            this.endY.Size = new System.Drawing.Size(147, 21);
            this.endY.TabIndex = 9;
            this.endY.TextChanged += new System.EventHandler(this.startX_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Z:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "X:";
            // 
            // endX
            // 
            this.endX.Enabled = false;
            this.endX.Location = new System.Drawing.Point(30, 32);
            this.endX.Name = "endX";
            this.endX.Size = new System.Drawing.Size(147, 21);
            this.endX.TabIndex = 7;
            this.endX.TextChanged += new System.EventHandler(this.startX_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Y:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.startZ);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.startY);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.startX);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 111);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "观察点";
            // 
            // startZ
            // 
            this.startZ.Enabled = false;
            this.startZ.Location = new System.Drawing.Point(31, 83);
            this.startZ.Name = "startZ";
            this.startZ.Size = new System.Drawing.Size(147, 21);
            this.startZ.TabIndex = 5;
            this.startZ.TextChanged += new System.EventHandler(this.startX_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Z:";
            // 
            // startY
            // 
            this.startY.Enabled = false;
            this.startY.Location = new System.Drawing.Point(31, 49);
            this.startY.Name = "startY";
            this.startY.Size = new System.Drawing.Size(147, 21);
            this.startY.TabIndex = 3;
            this.startY.TextChanged += new System.EventHandler(this.startX_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y:";
            // 
            // startX
            // 
            this.startX.Enabled = false;
            this.startX.Location = new System.Drawing.Point(31, 21);
            this.startX.Name = "startX";
            this.startX.Size = new System.Drawing.Size(147, 21);
            this.startX.TabIndex = 1;
            this.startX.TextChanged += new System.EventHandler(this.startX_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // btn_createLine
            // 
            this.btn_createLine.Location = new System.Drawing.Point(15, 9);
            this.btn_createLine.Name = "btn_createLine";
            this.btn_createLine.Size = new System.Drawing.Size(75, 23);
            this.btn_createLine.TabIndex = 1;
            this.btn_createLine.Text = "绘线";
            this.btn_createLine.UseVisualStyleBackColor = true;
            this.btn_createLine.Click += new System.EventHandler(this.btn_createLine_Click);
            // 
            // btn_analyse
            // 
            this.btn_analyse.Location = new System.Drawing.Point(120, 9);
            this.btn_analyse.Name = "btn_analyse";
            this.btn_analyse.Size = new System.Drawing.Size(75, 23);
            this.btn_analyse.TabIndex = 0;
            this.btn_analyse.Text = "通视分析";
            this.btn_analyse.UseVisualStyleBackColor = true;
            this.btn_analyse.Click += new System.EventHandler(this.btn_Analize_Click);
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
            this.axRenderControl1.Size = new System.Drawing.Size(655, 579);
            this.axRenderControl1.TabIndex = 2;
            this.axRenderControl1.UseEarthOrbitManipulator = Gvitech.CityMaker.RenderControl.gviManipulatorMode.gviCityMakerManipulator;
            // 
            // NameColumn
            // 
            this.NameColumn.Name = "NameColumn";
            // 
            // GroupID
            // 
            this.GroupID.Name = "GroupID";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 585);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpatialQuery2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.Button btn_analyse;
        private System.Windows.Forms.Button btn_createLine;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox endZ;
        private System.Windows.Forms.TextBox endY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox endX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox startZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox startY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox startX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FID;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupID;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button btnFlyToSourcePoint;
        private System.Windows.Forms.Button btnFlyToTargetPoint;

    }
}


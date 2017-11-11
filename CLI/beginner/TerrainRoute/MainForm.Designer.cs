namespace TerrainRoute
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
            this.cbAutoRepeat = new System.Windows.Forms.CheckBox();
            this.btnStopRoute = new System.Windows.Forms.Button();
            this.btnPauseRoute = new System.Windows.Forms.Button();
            this.btnPlayRoute = new System.Windows.Forms.Button();
            this.numSpeed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReplacePoint = new System.Windows.Forms.Button();
            this.btnDelPoint = new System.Windows.Forms.Button();
            this.btnAddPoint = new System.Windows.Forms.Button();
            this.btnCreateRoute = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnExportAsXML = new System.Windows.Forms.Button();
            this.btnImportFromXML = new System.Windows.Forms.Button();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(578, 362);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 518);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExportAsXML);
            this.panel1.Controls.Add(this.btnImportFromXML);
            this.panel1.Controls.Add(this.cbAutoRepeat);
            this.panel1.Controls.Add(this.btnStopRoute);
            this.panel1.Controls.Add(this.btnPauseRoute);
            this.panel1.Controls.Add(this.btnPlayRoute);
            this.panel1.Controls.Add(this.numSpeed);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnReplacePoint);
            this.panel1.Controls.Add(this.btnDelPoint);
            this.panel1.Controls.Add(this.btnAddPoint);
            this.panel1.Controls.Add(this.btnCreateRoute);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(587, 3);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(95, 512);
            this.panel1.TabIndex = 1;
            // 
            // cbAutoRepeat
            // 
            this.cbAutoRepeat.AutoSize = true;
            this.cbAutoRepeat.Location = new System.Drawing.Point(6, 238);
            this.cbAutoRepeat.Name = "cbAutoRepeat";
            this.cbAutoRepeat.Size = new System.Drawing.Size(84, 16);
            this.cbAutoRepeat.TabIndex = 9;
            this.cbAutoRepeat.Text = "AutoRepeat";
            this.cbAutoRepeat.UseVisualStyleBackColor = true;
            this.cbAutoRepeat.CheckedChanged += new System.EventHandler(this.cbAutoRepeat_CheckedChanged);
            // 
            // btnStopRoute
            // 
            this.btnStopRoute.Location = new System.Drawing.Point(6, 332);
            this.btnStopRoute.Name = "btnStopRoute";
            this.btnStopRoute.Size = new System.Drawing.Size(80, 30);
            this.btnStopRoute.TabIndex = 8;
            this.btnStopRoute.Text = "StopRoute";
            this.btnStopRoute.UseVisualStyleBackColor = true;
            this.btnStopRoute.Click += new System.EventHandler(this.btnStopRoute_Click);
            // 
            // btnPauseRoute
            // 
            this.btnPauseRoute.Location = new System.Drawing.Point(6, 296);
            this.btnPauseRoute.Name = "btnPauseRoute";
            this.btnPauseRoute.Size = new System.Drawing.Size(80, 30);
            this.btnPauseRoute.TabIndex = 7;
            this.btnPauseRoute.Text = "PauseRoute";
            this.btnPauseRoute.UseVisualStyleBackColor = true;
            this.btnPauseRoute.Click += new System.EventHandler(this.btnPauseRoute_Click);
            // 
            // btnPlayRoute
            // 
            this.btnPlayRoute.Location = new System.Drawing.Point(6, 260);
            this.btnPlayRoute.Name = "btnPlayRoute";
            this.btnPlayRoute.Size = new System.Drawing.Size(80, 30);
            this.btnPlayRoute.TabIndex = 6;
            this.btnPlayRoute.Text = "PlayRoute";
            this.btnPlayRoute.UseVisualStyleBackColor = true;
            this.btnPlayRoute.Click += new System.EventHandler(this.btnPlayRoute_Click);
            // 
            // numSpeed
            // 
            this.numSpeed.Location = new System.Drawing.Point(12, 71);
            this.numSpeed.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSpeed.Name = "numSpeed";
            this.numSpeed.Size = new System.Drawing.Size(74, 21);
            this.numSpeed.TabIndex = 5;
            this.numSpeed.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "speed";
            // 
            // btnReplacePoint
            // 
            this.btnReplacePoint.Location = new System.Drawing.Point(6, 170);
            this.btnReplacePoint.Name = "btnReplacePoint";
            this.btnReplacePoint.Size = new System.Drawing.Size(86, 30);
            this.btnReplacePoint.TabIndex = 3;
            this.btnReplacePoint.Text = "ReplacePoint";
            this.btnReplacePoint.UseVisualStyleBackColor = true;
            this.btnReplacePoint.Click += new System.EventHandler(this.btnReplacePoint_Click);
            // 
            // btnDelPoint
            // 
            this.btnDelPoint.Location = new System.Drawing.Point(6, 134);
            this.btnDelPoint.Name = "btnDelPoint";
            this.btnDelPoint.Size = new System.Drawing.Size(80, 30);
            this.btnDelPoint.TabIndex = 2;
            this.btnDelPoint.Text = "DelPoint";
            this.btnDelPoint.UseVisualStyleBackColor = true;
            this.btnDelPoint.Click += new System.EventHandler(this.btnDelPoint_Click);
            // 
            // btnAddPoint
            // 
            this.btnAddPoint.Location = new System.Drawing.Point(6, 98);
            this.btnAddPoint.Name = "btnAddPoint";
            this.btnAddPoint.Size = new System.Drawing.Size(80, 30);
            this.btnAddPoint.TabIndex = 1;
            this.btnAddPoint.Text = "AddPoint";
            this.btnAddPoint.UseVisualStyleBackColor = true;
            this.btnAddPoint.Click += new System.EventHandler(this.btnAddPoint_Click);
            // 
            // btnCreateRoute
            // 
            this.btnCreateRoute.Location = new System.Drawing.Point(6, 9);
            this.btnCreateRoute.Name = "btnCreateRoute";
            this.btnCreateRoute.Size = new System.Drawing.Size(80, 30);
            this.btnCreateRoute.TabIndex = 0;
            this.btnCreateRoute.Text = "CreateRoute";
            this.btnCreateRoute.UseVisualStyleBackColor = true;
            this.btnCreateRoute.Click += new System.EventHandler(this.btnCreateRoute_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 371);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(578, 144);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseDoubleClick);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // btnExportAsXML
            // 
            this.btnExportAsXML.Location = new System.Drawing.Point(6, 420);
            this.btnExportAsXML.Name = "btnExportAsXML";
            this.btnExportAsXML.Size = new System.Drawing.Size(81, 27);
            this.btnExportAsXML.TabIndex = 15;
            this.btnExportAsXML.Text = "ExportAsXML";
            this.btnExportAsXML.UseVisualStyleBackColor = true;
            this.btnExportAsXML.Click += new System.EventHandler(this.btnExportAsXML_Click);
            // 
            // btnImportFromXML
            // 
            this.btnImportFromXML.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImportFromXML.Location = new System.Drawing.Point(6, 387);
            this.btnImportFromXML.Name = "btnImportFromXML";
            this.btnImportFromXML.Size = new System.Drawing.Size(81, 27);
            this.btnImportFromXML.TabIndex = 14;
            this.btnImportFromXML.Text = "ImportFromXML";
            this.btnImportFromXML.UseVisualStyleBackColor = true;
            this.btnImportFromXML.Click += new System.EventHandler(this.btnImportFromXML_Click);
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
            this.Text = "TerrainRoute";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCreateRoute;
        private System.Windows.Forms.Button btnReplacePoint;
        private System.Windows.Forms.Button btnDelPoint;
        private System.Windows.Forms.Button btnAddPoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numSpeed;
        private System.Windows.Forms.Button btnStopRoute;
        private System.Windows.Forms.Button btnPauseRoute;
        private System.Windows.Forms.Button btnPlayRoute;
        private System.Windows.Forms.CheckBox cbAutoRepeat;
        private System.Windows.Forms.Button btnExportAsXML;
        private System.Windows.Forms.Button btnImportFromXML;

    }
}


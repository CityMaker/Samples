namespace CameraTour
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
            this.components = new System.ComponentModel.Container();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPlayTourFromTime = new System.Windows.Forms.Button();
            this.btnExportAsFramePictures = new System.Windows.Forms.Button();
            this.btnExportAsVideo = new System.Windows.Forms.Button();
            this.btnExportAsXML = new System.Windows.Forms.Button();
            this.btnImportFromXML = new System.Windows.Forms.Button();
            this.cbAutoRepeat = new System.Windows.Forms.CheckBox();
            this.btnStopTour = new System.Windows.Forms.Button();
            this.btnPauseTour = new System.Windows.Forms.Button();
            this.btnPlayTourFromIndex = new System.Windows.Forms.Button();
            this.btnReplacePoint = new System.Windows.Forms.Button();
            this.btnDelPoint = new System.Windows.Forms.Button();
            this.btnAddPoint = new System.Windows.Forms.Button();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateTour = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.trackBarTime = new System.Windows.Forms.TrackBar();
            this.labelTime = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnExportAsPanoramaFramePictures = new System.Windows.Forms.Button();
            
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTime)).BeginInit();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(579, 521);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 178F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 737);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 530);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(579, 172);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseDoubleClick);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExportAsPanoramaFramePictures);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnPlayTourFromTime);
            this.panel1.Controls.Add(this.btnExportAsFramePictures);
            this.panel1.Controls.Add(this.btnExportAsVideo);
            this.panel1.Controls.Add(this.btnExportAsXML);
            this.panel1.Controls.Add(this.btnImportFromXML);
            this.panel1.Controls.Add(this.cbAutoRepeat);
            this.panel1.Controls.Add(this.btnStopTour);
            this.panel1.Controls.Add(this.btnPauseTour);
            this.panel1.Controls.Add(this.btnPlayTourFromIndex);
            this.panel1.Controls.Add(this.btnReplacePoint);
            this.panel1.Controls.Add(this.btnDelPoint);
            this.panel1.Controls.Add(this.btnAddPoint);
            this.panel1.Controls.Add(this.cbMode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.numDuration);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnCreateTour);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(588, 3);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 3);
            this.panel1.Size = new System.Drawing.Size(94, 731);
            this.panel1.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(4, 681);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 41);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "关闭窗口";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPlayTourFromTime
            // 
            this.btnPlayTourFromTime.Location = new System.Drawing.Point(4, 320);
            this.btnPlayTourFromTime.Name = "btnPlayTourFromTime";
            this.btnPlayTourFromTime.Size = new System.Drawing.Size(81, 50);
            this.btnPlayTourFromTime.TabIndex = 16;
            this.btnPlayTourFromTime.Text = "PlayTour(从指定时刻开始播放)";
            this.btnPlayTourFromTime.UseVisualStyleBackColor = true;
            this.btnPlayTourFromTime.Click += new System.EventHandler(this.btnPlayTourFromTime_Click);
            // 
            // btnExportAsFramePictures
            // 
            this.btnExportAsFramePictures.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExportAsFramePictures.Location = new System.Drawing.Point(4, 559);
            this.btnExportAsFramePictures.Name = "btnExportAsFramePictures";
            this.btnExportAsFramePictures.Size = new System.Drawing.Size(81, 27);
            this.btnExportAsFramePictures.TabIndex = 15;
            this.btnExportAsFramePictures.Text = "出序列帧图";
            this.btnExportAsFramePictures.UseVisualStyleBackColor = true;
            this.btnExportAsFramePictures.Click += new System.EventHandler(this.btnExportAsFramePictures_Click);
            // 
            // btnExportAsVideo
            // 
            this.btnExportAsVideo.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExportAsVideo.Location = new System.Drawing.Point(4, 528);
            this.btnExportAsVideo.Name = "btnExportAsVideo";
            this.btnExportAsVideo.Size = new System.Drawing.Size(81, 27);
            this.btnExportAsVideo.TabIndex = 14;
            this.btnExportAsVideo.Text = "ExportAsVideo";
            this.btnExportAsVideo.UseVisualStyleBackColor = true;
            this.btnExportAsVideo.Click += new System.EventHandler(this.btnExportAsVideo_Click);
            // 
            // btnExportAsXML
            // 
            this.btnExportAsXML.Location = new System.Drawing.Point(3, 495);
            this.btnExportAsXML.Name = "btnExportAsXML";
            this.btnExportAsXML.Size = new System.Drawing.Size(81, 27);
            this.btnExportAsXML.TabIndex = 13;
            this.btnExportAsXML.Text = "ExportAsXML";
            this.btnExportAsXML.UseVisualStyleBackColor = true;
            this.btnExportAsXML.Click += new System.EventHandler(this.btnExportAsXML_Click);
            // 
            // btnImportFromXML
            // 
            this.btnImportFromXML.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImportFromXML.Location = new System.Drawing.Point(3, 462);
            this.btnImportFromXML.Name = "btnImportFromXML";
            this.btnImportFromXML.Size = new System.Drawing.Size(81, 27);
            this.btnImportFromXML.TabIndex = 12;
            this.btnImportFromXML.Text = "ImportFromXML";
            this.btnImportFromXML.UseVisualStyleBackColor = true;
            this.btnImportFromXML.Click += new System.EventHandler(this.btnImportFromXML_Click);
            // 
            // cbAutoRepeat
            // 
            this.cbAutoRepeat.AutoSize = true;
            this.cbAutoRepeat.Location = new System.Drawing.Point(4, 246);
            this.cbAutoRepeat.Name = "cbAutoRepeat";
            this.cbAutoRepeat.Size = new System.Drawing.Size(84, 16);
            this.cbAutoRepeat.TabIndex = 11;
            this.cbAutoRepeat.Text = "AutoRepeat";
            this.cbAutoRepeat.UseVisualStyleBackColor = true;
            this.cbAutoRepeat.CheckedChanged += new System.EventHandler(this.cbAutoRepeat_CheckedChanged);
            // 
            // btnStopTour
            // 
            this.btnStopTour.Location = new System.Drawing.Point(4, 404);
            this.btnStopTour.Name = "btnStopTour";
            this.btnStopTour.Size = new System.Drawing.Size(81, 27);
            this.btnStopTour.TabIndex = 10;
            this.btnStopTour.Text = "StopTour";
            this.btnStopTour.UseVisualStyleBackColor = true;
            this.btnStopTour.Click += new System.EventHandler(this.btnStopTour_Click);
            // 
            // btnPauseTour
            // 
            this.btnPauseTour.Location = new System.Drawing.Point(4, 373);
            this.btnPauseTour.Name = "btnPauseTour";
            this.btnPauseTour.Size = new System.Drawing.Size(81, 27);
            this.btnPauseTour.TabIndex = 9;
            this.btnPauseTour.Text = "PauseTour";
            this.btnPauseTour.UseVisualStyleBackColor = true;
            this.btnPauseTour.Click += new System.EventHandler(this.btnPauseTour_Click);
            // 
            // btnPlayTourFromIndex
            // 
            this.btnPlayTourFromIndex.Location = new System.Drawing.Point(4, 267);
            this.btnPlayTourFromIndex.Name = "btnPlayTourFromIndex";
            this.btnPlayTourFromIndex.Size = new System.Drawing.Size(81, 50);
            this.btnPlayTourFromIndex.TabIndex = 8;
            this.btnPlayTourFromIndex.Text = "PlayTour(从指定节点开始播放)";
            this.btnPlayTourFromIndex.UseVisualStyleBackColor = true;
            this.btnPlayTourFromIndex.Click += new System.EventHandler(this.btnPlayTourFromIndex_Click);
            // 
            // btnReplacePoint
            // 
            this.btnReplacePoint.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReplacePoint.Location = new System.Drawing.Point(4, 189);
            this.btnReplacePoint.Name = "btnReplacePoint";
            this.btnReplacePoint.Size = new System.Drawing.Size(87, 27);
            this.btnReplacePoint.TabIndex = 7;
            this.btnReplacePoint.Text = "ReplacePoint";
            this.btnReplacePoint.UseVisualStyleBackColor = true;
            this.btnReplacePoint.Click += new System.EventHandler(this.btnReplacePoint_Click);
            // 
            // btnDelPoint
            // 
            this.btnDelPoint.Location = new System.Drawing.Point(4, 156);
            this.btnDelPoint.Name = "btnDelPoint";
            this.btnDelPoint.Size = new System.Drawing.Size(81, 27);
            this.btnDelPoint.TabIndex = 6;
            this.btnDelPoint.Text = "DelPoint";
            this.btnDelPoint.UseVisualStyleBackColor = true;
            this.btnDelPoint.Click += new System.EventHandler(this.btnDelPoint_Click);
            // 
            // btnAddPoint
            // 
            this.btnAddPoint.Location = new System.Drawing.Point(4, 123);
            this.btnAddPoint.Name = "btnAddPoint";
            this.btnAddPoint.Size = new System.Drawing.Size(81, 27);
            this.btnAddPoint.TabIndex = 5;
            this.btnAddPoint.Text = "AddPoint";
            this.btnAddPoint.UseVisualStyleBackColor = true;
            this.btnAddPoint.Click += new System.EventHandler(this.btnAddPoint_Click);
            // 
            // cbMode
            // 
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Items.AddRange(new object[] {
            "Smooth",
            "Bounce",
            "Linear"});
            this.cbMode.Location = new System.Drawing.Point(10, 96);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(75, 20);
            this.cbMode.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mode";
            // 
            // numDuration
            // 
            this.numDuration.Location = new System.Drawing.Point(10, 56);
            this.numDuration.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(75, 21);
            this.numDuration.TabIndex = 2;
            this.numDuration.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Duration";
            // 
            // btnCreateTour
            // 
            this.btnCreateTour.Location = new System.Drawing.Point(4, 3);
            this.btnCreateTour.Name = "btnCreateTour";
            this.btnCreateTour.Size = new System.Drawing.Size(81, 27);
            this.btnCreateTour.TabIndex = 0;
            this.btnCreateTour.Text = "CreateTour";
            this.btnCreateTour.UseVisualStyleBackColor = true;
            this.btnCreateTour.Click += new System.EventHandler(this.btnCreateTour_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel2.Controls.Add(this.trackBarTime, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelTime, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 708);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(579, 26);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // trackBarTime
            // 
            this.trackBarTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBarTime.Location = new System.Drawing.Point(3, 3);
            this.trackBarTime.Name = "trackBarTime";
            this.trackBarTime.Size = new System.Drawing.Size(530, 20);
            this.trackBarTime.TabIndex = 0;
            this.trackBarTime.ValueChanged += new System.EventHandler(this.trackBarTime_ValueChanged);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTime.Location = new System.Drawing.Point(539, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(37, 26);
            this.labelTime.TabIndex = 1;
            this.labelTime.Text = "0";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnExportAsPanoramaFramePictures
            // 
            this.btnExportAsPanoramaFramePictures.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExportAsPanoramaFramePictures.Location = new System.Drawing.Point(4, 591);
            this.btnExportAsPanoramaFramePictures.Name = "btnExportAsPanoramaFramePictures";
            this.btnExportAsPanoramaFramePictures.Size = new System.Drawing.Size(81, 27);
            this.btnExportAsPanoramaFramePictures.TabIndex = 18;
            this.btnExportAsPanoramaFramePictures.Text = "出全景序列帧图";
            this.btnExportAsPanoramaFramePictures.UseVisualStyleBackColor = true;
            this.btnExportAsPanoramaFramePictures.Click += new System.EventHandler(this.btnExportAsPanoramaFramePictures_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 737);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CameraTour";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCreateTour;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numDuration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbMode;
        private System.Windows.Forms.Button btnAddPoint;
        private System.Windows.Forms.Button btnDelPoint;
        private System.Windows.Forms.Button btnReplacePoint;
        private System.Windows.Forms.Button btnPlayTourFromIndex;
        private System.Windows.Forms.Button btnPauseTour;
        private System.Windows.Forms.Button btnStopTour;
        private System.Windows.Forms.CheckBox cbAutoRepeat;
        private System.Windows.Forms.Button btnImportFromXML;
        private System.Windows.Forms.Button btnExportAsXML;
        private System.Windows.Forms.Button btnExportAsVideo;
        private System.Windows.Forms.Button btnExportAsFramePictures;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TrackBar trackBarTime;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnPlayTourFromTime;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnExportAsPanoramaFramePictures;

    }
}


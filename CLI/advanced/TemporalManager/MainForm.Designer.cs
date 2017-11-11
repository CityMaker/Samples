namespace TemporalManager
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.trackBarTime = new System.Windows.Forms.TrackBar();
            this.labelTime = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbLayerEnableTemporal = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnUpdateTemporal = new System.Windows.Forms.Button();
            this.listBoxTemporalInstanceTime = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDeleteTemporal = new System.Windows.Forms.Button();
            this.btnInsertTemporal = new System.Windows.Forms.Button();
            this.dateTimePickerTemporalBirthDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeleteFeature = new System.Windows.Forms.Button();
            this.btnSetFeatureDeadDate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInsertFeature = new System.Windows.Forms.Button();
            this.dateTimePickerBirthDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerDeadDate = new System.Windows.Forms.DateTimePicker();
            this.btnResetBirthDate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSetEndDate = new System.Windows.Forms.Button();
            this.dateTimePickerTrackbarEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSetBeginDate = new System.Windows.Forms.Button();
            this.dateTimePickerTrackbarBeginDate = new System.Windows.Forms.DateTimePicker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hasTemporalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableTemporalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableTemporalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getKeyDatetimesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopRenderingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startRenderingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTime)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(792, 628);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 178F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(798, 844);
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
            this.dataGridView1.Location = new System.Drawing.Point(3, 637);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(792, 172);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseDoubleClick);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel2.Controls.Add(this.trackBarTime, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelTime, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 815);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(792, 26);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // trackBarTime
            // 
            this.trackBarTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBarTime.LargeChange = 1;
            this.trackBarTime.Location = new System.Drawing.Point(3, 3);
            this.trackBarTime.Name = "trackBarTime";
            this.trackBarTime.Size = new System.Drawing.Size(708, 20);
            this.trackBarTime.TabIndex = 0;
            this.trackBarTime.ValueChanged += new System.EventHandler(this.trackBarTime_ValueChanged);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTime.Location = new System.Drawing.Point(717, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(72, 26);
            this.labelTime.TabIndex = 1;
            this.labelTime.Text = "0";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(1311, 844);
            this.splitContainer1.SplitterDistance = 509;
            this.splitContainer1.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.treeView1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(509, 844);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(503, 416);
            this.treeView1.TabIndex = 0;
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbLayerEnableTemporal);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 425);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(503, 416);
            this.panel1.TabIndex = 1;
            // 
            // cbLayerEnableTemporal
            // 
            this.cbLayerEnableTemporal.AutoSize = true;
            this.cbLayerEnableTemporal.Location = new System.Drawing.Point(11, 12);
            this.cbLayerEnableTemporal.Name = "cbLayerEnableTemporal";
            this.cbLayerEnableTemporal.Size = new System.Drawing.Size(180, 16);
            this.cbLayerEnableTemporal.TabIndex = 20;
            this.cbLayerEnableTemporal.Text = "FeatureLayerEnableTemporal";
            this.cbLayerEnableTemporal.UseVisualStyleBackColor = true;
            this.cbLayerEnableTemporal.CheckedChanged += new System.EventHandler(this.cbLayerEnableTemporal_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnUpdateTemporal);
            this.groupBox3.Controls.Add(this.listBoxTemporalInstanceTime);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnDeleteTemporal);
            this.groupBox3.Controls.Add(this.btnInsertTemporal);
            this.groupBox3.Controls.Add(this.dateTimePickerTemporalBirthDate);
            this.groupBox3.Location = new System.Drawing.Point(0, 252);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(494, 163);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "时序增删改";
            // 
            // btnUpdateTemporal
            // 
            this.btnUpdateTemporal.Location = new System.Drawing.Point(207, 64);
            this.btnUpdateTemporal.Name = "btnUpdateTemporal";
            this.btnUpdateTemporal.Size = new System.Drawing.Size(100, 32);
            this.btnUpdateTemporal.TabIndex = 18;
            this.btnUpdateTemporal.Text = "更新选中时序";
            this.btnUpdateTemporal.UseVisualStyleBackColor = true;
            this.btnUpdateTemporal.Click += new System.EventHandler(this.btnUpdateTemporal_Click);
            // 
            // listBoxTemporalInstanceTime
            // 
            this.listBoxTemporalInstanceTime.FormattingEnabled = true;
            this.listBoxTemporalInstanceTime.ItemHeight = 12;
            this.listBoxTemporalInstanceTime.Location = new System.Drawing.Point(8, 20);
            this.listBoxTemporalInstanceTime.Name = "listBoxTemporalInstanceTime";
            this.listBoxTemporalInstanceTime.Size = new System.Drawing.Size(188, 100);
            this.listBoxTemporalInstanceTime.TabIndex = 13;
            this.listBoxTemporalInstanceTime.SelectedIndexChanged += new System.EventHandler(this.listBoxTemporalInstanceTime_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "时序起始时间";
            // 
            // btnDeleteTemporal
            // 
            this.btnDeleteTemporal.Location = new System.Drawing.Point(206, 26);
            this.btnDeleteTemporal.Name = "btnDeleteTemporal";
            this.btnDeleteTemporal.Size = new System.Drawing.Size(100, 32);
            this.btnDeleteTemporal.TabIndex = 17;
            this.btnDeleteTemporal.Text = "删除选中时序";
            this.btnDeleteTemporal.UseVisualStyleBackColor = true;
            this.btnDeleteTemporal.Click += new System.EventHandler(this.btnDeleteTemporal_Click);
            // 
            // btnInsertTemporal
            // 
            this.btnInsertTemporal.Location = new System.Drawing.Point(231, 121);
            this.btnInsertTemporal.Name = "btnInsertTemporal";
            this.btnInsertTemporal.Size = new System.Drawing.Size(75, 32);
            this.btnInsertTemporal.TabIndex = 15;
            this.btnInsertTemporal.Text = "插入新时序";
            this.btnInsertTemporal.UseVisualStyleBackColor = true;
            this.btnInsertTemporal.Click += new System.EventHandler(this.btnInsertTemporal_Click);
            // 
            // dateTimePickerTemporalBirthDate
            // 
            this.dateTimePickerTemporalBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTemporalBirthDate.Location = new System.Drawing.Point(92, 126);
            this.dateTimePickerTemporalBirthDate.Name = "dateTimePickerTemporalBirthDate";
            this.dateTimePickerTemporalBirthDate.Size = new System.Drawing.Size(133, 21);
            this.dateTimePickerTemporalBirthDate.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDeleteFeature);
            this.groupBox2.Controls.Add(this.btnSetFeatureDeadDate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnInsertFeature);
            this.groupBox2.Controls.Add(this.dateTimePickerBirthDate);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dateTimePickerDeadDate);
            this.groupBox2.Controls.Add(this.btnResetBirthDate);
            this.groupBox2.Location = new System.Drawing.Point(3, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(494, 122);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "要素增删改";
            // 
            // btnDeleteFeature
            // 
            this.btnDeleteFeature.Location = new System.Drawing.Point(8, 83);
            this.btnDeleteFeature.Name = "btnDeleteFeature";
            this.btnDeleteFeature.Size = new System.Drawing.Size(100, 32);
            this.btnDeleteFeature.TabIndex = 18;
            this.btnDeleteFeature.Text = "删除选中要素";
            this.btnDeleteFeature.UseVisualStyleBackColor = true;
            this.btnDeleteFeature.Click += new System.EventHandler(this.btnDeleteFeature_Click);
            // 
            // btnSetFeatureDeadDate
            // 
            this.btnSetFeatureDeadDate.Location = new System.Drawing.Point(228, 51);
            this.btnSetFeatureDeadDate.Name = "btnSetFeatureDeadDate";
            this.btnSetFeatureDeadDate.Size = new System.Drawing.Size(114, 32);
            this.btnSetFeatureDeadDate.TabIndex = 8;
            this.btnSetFeatureDeadDate.Text = "重置要素死亡时间";
            this.btnSetFeatureDeadDate.UseVisualStyleBackColor = true;
            this.btnSetFeatureDeadDate.Click += new System.EventHandler(this.btnSetFeatureDeadDate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "要素起始时间";
            // 
            // btnInsertFeature
            // 
            this.btnInsertFeature.Location = new System.Drawing.Point(228, 16);
            this.btnInsertFeature.Name = "btnInsertFeature";
            this.btnInsertFeature.Size = new System.Drawing.Size(75, 32);
            this.btnInsertFeature.TabIndex = 1;
            this.btnInsertFeature.Text = "插入新要素";
            this.btnInsertFeature.UseVisualStyleBackColor = true;
            this.btnInsertFeature.Click += new System.EventHandler(this.btnInsertFeature_Click);
            // 
            // dateTimePickerBirthDate
            // 
            this.dateTimePickerBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerBirthDate.Location = new System.Drawing.Point(89, 21);
            this.dateTimePickerBirthDate.Name = "dateTimePickerBirthDate";
            this.dateTimePickerBirthDate.Size = new System.Drawing.Size(133, 21);
            this.dateTimePickerBirthDate.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "要素死亡时间";
            // 
            // dateTimePickerDeadDate
            // 
            this.dateTimePickerDeadDate.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerDeadDate.Location = new System.Drawing.Point(89, 56);
            this.dateTimePickerDeadDate.Name = "dateTimePickerDeadDate";
            this.dateTimePickerDeadDate.Size = new System.Drawing.Size(133, 21);
            this.dateTimePickerDeadDate.TabIndex = 9;
            // 
            // btnResetBirthDate
            // 
            this.btnResetBirthDate.Location = new System.Drawing.Point(309, 17);
            this.btnResetBirthDate.Name = "btnResetBirthDate";
            this.btnResetBirthDate.Size = new System.Drawing.Size(114, 32);
            this.btnResetBirthDate.TabIndex = 11;
            this.btnResetBirthDate.Text = "重置要素出生时间";
            this.btnResetBirthDate.UseVisualStyleBackColor = true;
            this.btnResetBirthDate.Click += new System.EventHandler(this.btnResetBirthDate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnSetEndDate);
            this.groupBox1.Controls.Add(this.dateTimePickerTrackbarEndDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSetBeginDate);
            this.groupBox1.Controls.Add(this.dateTimePickerTrackbarBeginDate);
            this.groupBox1.Location = new System.Drawing.Point(3, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 49);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "自定义时间轴";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(255, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "结束时间";
            // 
            // btnSetEndDate
            // 
            this.btnSetEndDate.Location = new System.Drawing.Point(453, 14);
            this.btnSetEndDate.Name = "btnSetEndDate";
            this.btnSetEndDate.Size = new System.Drawing.Size(41, 26);
            this.btnSetEndDate.TabIndex = 8;
            this.btnSetEndDate.Text = "确定";
            this.btnSetEndDate.UseVisualStyleBackColor = true;
            this.btnSetEndDate.Click += new System.EventHandler(this.btnSetEndDate_Click);
            // 
            // dateTimePickerTrackbarEndDate
            // 
            this.dateTimePickerTrackbarEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTrackbarEndDate.Location = new System.Drawing.Point(310, 15);
            this.dateTimePickerTrackbarEndDate.Name = "dateTimePickerTrackbarEndDate";
            this.dateTimePickerTrackbarEndDate.Size = new System.Drawing.Size(133, 21);
            this.dateTimePickerTrackbarEndDate.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "开始时间";
            // 
            // btnSetBeginDate
            // 
            this.btnSetBeginDate.Location = new System.Drawing.Point(204, 16);
            this.btnSetBeginDate.Name = "btnSetBeginDate";
            this.btnSetBeginDate.Size = new System.Drawing.Size(41, 26);
            this.btnSetBeginDate.TabIndex = 5;
            this.btnSetBeginDate.Text = "确定";
            this.btnSetBeginDate.UseVisualStyleBackColor = true;
            this.btnSetBeginDate.Click += new System.EventHandler(this.btnSetBeginDate_Click);
            // 
            // dateTimePickerTrackbarBeginDate
            // 
            this.dateTimePickerTrackbarBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTrackbarBeginDate.Location = new System.Drawing.Point(61, 17);
            this.dateTimePickerTrackbarBeginDate.Name = "dateTimePickerTrackbarBeginDate";
            this.dateTimePickerTrackbarBeginDate.Size = new System.Drawing.Size(133, 21);
            this.dateTimePickerTrackbarBeginDate.TabIndex = 4;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hasTemporalToolStripMenuItem,
            this.enableTemporalToolStripMenuItem,
            this.disableTemporalToolStripMenuItem,
            this.searchAllToolStripMenuItem,
            this.searchBaseToolStripMenuItem,
            this.getKeyDatetimesToolStripMenuItem,
            this.stopRenderingToolStripMenuItem,
            this.startRenderingToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(176, 180);
            // 
            // hasTemporalToolStripMenuItem
            // 
            this.hasTemporalToolStripMenuItem.Name = "hasTemporalToolStripMenuItem";
            this.hasTemporalToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.hasTemporalToolStripMenuItem.Text = "HasTemporal";
            this.hasTemporalToolStripMenuItem.Click += new System.EventHandler(this.hasTemporalToolStripMenuItem_Click);
            // 
            // enableTemporalToolStripMenuItem
            // 
            this.enableTemporalToolStripMenuItem.Name = "enableTemporalToolStripMenuItem";
            this.enableTemporalToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.enableTemporalToolStripMenuItem.Text = "EnableTemporal";
            this.enableTemporalToolStripMenuItem.Click += new System.EventHandler(this.enableTemporalToolStripMenuItem_Click);
            // 
            // disableTemporalToolStripMenuItem
            // 
            this.disableTemporalToolStripMenuItem.Name = "disableTemporalToolStripMenuItem";
            this.disableTemporalToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.disableTemporalToolStripMenuItem.Text = "DisableTemporal";
            this.disableTemporalToolStripMenuItem.Click += new System.EventHandler(this.disableTemporalToolStripMenuItem_Click);
            // 
            // searchAllToolStripMenuItem
            // 
            this.searchAllToolStripMenuItem.Name = "searchAllToolStripMenuItem";
            this.searchAllToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.searchAllToolStripMenuItem.Text = "SearchAll";
            this.searchAllToolStripMenuItem.Click += new System.EventHandler(this.searchAllToolStripMenuItem_Click);
            // 
            // searchBaseToolStripMenuItem
            // 
            this.searchBaseToolStripMenuItem.Name = "searchBaseToolStripMenuItem";
            this.searchBaseToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.searchBaseToolStripMenuItem.Text = "SearchBase";
            this.searchBaseToolStripMenuItem.Click += new System.EventHandler(this.searchBaseToolStripMenuItem_Click);
            // 
            // getKeyDatetimesToolStripMenuItem
            // 
            this.getKeyDatetimesToolStripMenuItem.Name = "getKeyDatetimesToolStripMenuItem";
            this.getKeyDatetimesToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.getKeyDatetimesToolStripMenuItem.Text = "GetKeyDatetimes";
            this.getKeyDatetimesToolStripMenuItem.Click += new System.EventHandler(this.getKeyDatetimesToolStripMenuItem_Click);
            // 
            // stopRenderingToolStripMenuItem
            // 
            this.stopRenderingToolStripMenuItem.Name = "stopRenderingToolStripMenuItem";
            this.stopRenderingToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.stopRenderingToolStripMenuItem.Text = "StopRendering";
            this.stopRenderingToolStripMenuItem.Click += new System.EventHandler(this.stopRenderingToolStripMenuItem_Click);
            // 
            // startRenderingToolStripMenuItem
            // 
            this.startRenderingToolStripMenuItem.Name = "startRenderingToolStripMenuItem";
            this.startRenderingToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.startRenderingToolStripMenuItem.Text = "StartRendering";
            this.startRenderingToolStripMenuItem.Click += new System.EventHandler(this.startRenderingToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 844);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TemporalManager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTime)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TrackBar trackBarTime;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem enableTemporalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableTemporalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getKeyDatetimesToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInsertFeature;
        private System.Windows.Forms.DateTimePicker dateTimePickerBirthDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerTrackbarBeginDate;
        private System.Windows.Forms.Button btnSetBeginDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSetEndDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerTrackbarEndDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerDeadDate;
        private System.Windows.Forms.Button btnSetFeatureDeadDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnResetBirthDate;
        private System.Windows.Forms.ListBox listBoxTemporalInstanceTime;
        private System.Windows.Forms.ToolStripMenuItem searchBaseToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dateTimePickerTemporalBirthDate;
        private System.Windows.Forms.Button btnInsertTemporal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDeleteTemporal;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDeleteFeature;
        private System.Windows.Forms.Button btnUpdateTemporal;
        private System.Windows.Forms.CheckBox cbLayerEnableTemporal;
        private System.Windows.Forms.ToolStripMenuItem stopRenderingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startRenderingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hasTemporalToolStripMenuItem;

    }
}


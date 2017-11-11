namespace HelloWorld
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
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem("无", 0);
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem("金色晨曦", 1);
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem("光暗之手", 2);
            System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem("天马行空", 3);
            System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem("飘絮人间", 4);
            System.Windows.Forms.ListViewItem listViewItem22 = new System.Windows.Forms.ListViewItem("七彩紫罗", 5);
            System.Windows.Forms.ListViewItem listViewItem23 = new System.Windows.Forms.ListViewItem("云中之触", 6);
            System.Windows.Forms.ListViewItem listViewItem24 = new System.Windows.Forms.ListViewItem("鲲鹏万里", 7);
            System.Windows.Forms.ListViewItem listViewItem25 = new System.Windows.Forms.ListViewItem("血色苍穹", 8);
            System.Windows.Forms.ListViewItem listViewItem26 = new System.Windows.Forms.ListViewItem("白云旋天", 9);
            System.Windows.Forms.ListViewItem listViewItem27 = new System.Windows.Forms.ListViewItem("长空破日", 10);
            System.Windows.Forms.ListViewItem listViewItem28 = new System.Windows.Forms.ListViewItem("霞光掩影", 11);
            System.Windows.Forms.ListViewItem listViewItem29 = new System.Windows.Forms.ListViewItem("混沌沧海", 12);
            System.Windows.Forms.ListViewItem listViewItem30 = new System.Windows.Forms.ListViewItem("梦境之末", 13);
            System.Windows.Forms.ListViewItem listViewItem31 = new System.Windows.Forms.ListViewItem("玄浑宇宙", 14);
            System.Windows.Forms.ListViewItem listViewItem32 = new System.Windows.Forms.ListViewItem("月神之眼", 15);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.skyboxListView = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCaptureScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.请设置天气ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxWeather = new System.Windows.Forms.ToolStripComboBox();
            this.雾ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripFog = new System.Windows.Forms.ToolStripMenuItem();
            this.上一视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下一视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 29);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.59074F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 489);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.BackColor = System.Drawing.Color.White;
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.EnableMultiTouch = false;
            this.axRenderControl1.FullScreen = false;
            this.axRenderControl1.InteractMode = Gvitech.CityMaker.RenderControl.gviInteractMode.gviInteractNormal;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.MouseSelectObjectMask = Gvitech.CityMaker.RenderControl.gviMouseSelectObjectMask.gviSelectNone;
            this.axRenderControl1.MouseSnapMode = Gvitech.CityMaker.RenderControl.gviMouseSnapMode.gviMouseSnapDisable;
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(543, 483);
            this.axRenderControl1.TabIndex = 1;
            this.axRenderControl1.UseEarthOrbitManipulator = Gvitech.CityMaker.RenderControl.gviManipulatorMode.gviCityMakerManipulator;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(552, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(130, 483);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.skyboxListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(122, 457);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SkyBox";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // skyboxListView
            // 
            this.skyboxListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skyboxListView.HideSelection = false;
            this.skyboxListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem17,
            listViewItem18,
            listViewItem19,
            listViewItem20,
            listViewItem21,
            listViewItem22,
            listViewItem23,
            listViewItem24,
            listViewItem25,
            listViewItem26,
            listViewItem27,
            listViewItem28,
            listViewItem29,
            listViewItem30,
            listViewItem31,
            listViewItem32});
            this.skyboxListView.LargeImageList = this.imageList1;
            this.skyboxListView.Location = new System.Drawing.Point(3, 3);
            this.skyboxListView.MultiSelect = false;
            this.skyboxListView.Name = "skyboxListView";
            this.skyboxListView.Size = new System.Drawing.Size(116, 451);
            this.skyboxListView.TabIndex = 0;
            this.skyboxListView.UseCompatibleStateImageBehavior = false;
            this.skyboxListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.skyboxListView_MouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "无");
            this.imageList1.Images.SetKeyName(1, "金色晨曦");
            this.imageList1.Images.SetKeyName(2, "光暗之手");
            this.imageList1.Images.SetKeyName(3, "天马行空");
            this.imageList1.Images.SetKeyName(4, "飘絮人间");
            this.imageList1.Images.SetKeyName(5, "七彩紫罗");
            this.imageList1.Images.SetKeyName(6, "云中之触");
            this.imageList1.Images.SetKeyName(7, "鲲鹏万里");
            this.imageList1.Images.SetKeyName(8, "血色苍穹");
            this.imageList1.Images.SetKeyName(9, "白云旋天");
            this.imageList1.Images.SetKeyName(10, "长空破日");
            this.imageList1.Images.SetKeyName(11, "霞光掩影");
            this.imageList1.Images.SetKeyName(12, "混沌沧海");
            this.imageList1.Images.SetKeyName(13, "梦境之末");
            this.imageList1.Images.SetKeyName(14, "玄浑宇宙");
            this.imageList1.Images.SetKeyName(15, "月神之眼");
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(122, 457);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CameraTour";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPause);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Location = new System.Drawing.Point(2, 245);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(116, 42);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // btnPause
            // 
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(66, 13);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(23, 24);
            this.btnPause.TabIndex = 1;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(30, 13);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(23, 24);
            this.btnStop.TabIndex = 0;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(116, 240);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.请设置天气ToolStripMenuItem,
            this.toolStripComboBoxWeather,
            this.雾ToolStripMenuItem,
            this.上一视图ToolStripMenuItem,
            this.下一视图ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(685, 29);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripFullScreen,
            this.toolStripCaptureScreen});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(52, 25);
            this.toolStripMenuItem1.Text = "Tools";
            // 
            // toolStripFullScreen
            // 
            this.toolStripFullScreen.Name = "toolStripFullScreen";
            this.toolStripFullScreen.Size = new System.Drawing.Size(161, 22);
            this.toolStripFullScreen.Text = "FullScreen";
            this.toolStripFullScreen.Click += new System.EventHandler(this.toolStripFullScreen_Click);
            // 
            // toolStripCaptureScreen
            // 
            this.toolStripCaptureScreen.Name = "toolStripCaptureScreen";
            this.toolStripCaptureScreen.Size = new System.Drawing.Size(161, 22);
            this.toolStripCaptureScreen.Text = "CaptureScreen";
            this.toolStripCaptureScreen.Click += new System.EventHandler(this.toolStripCaptureScreen_Click);
            // 
            // 请设置天气ToolStripMenuItem
            // 
            this.请设置天气ToolStripMenuItem.Name = "请设置天气ToolStripMenuItem";
            this.请设置天气ToolStripMenuItem.Size = new System.Drawing.Size(92, 25);
            this.请设置天气ToolStripMenuItem.Text = "请设置天气：";
            // 
            // toolStripComboBoxWeather
            // 
            this.toolStripComboBoxWeather.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxWeather.DropDownWidth = 75;
            this.toolStripComboBoxWeather.Items.AddRange(new object[] {
            "晴天",
            "小雨",
            "中雨",
            "大雨",
            "小雪",
            "中雪",
            "大雪"});
            this.toolStripComboBoxWeather.Name = "toolStripComboBoxWeather";
            this.toolStripComboBoxWeather.Size = new System.Drawing.Size(75, 25);
            this.toolStripComboBoxWeather.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxWeather_SelectedIndexChanged);
            // 
            // 雾ToolStripMenuItem
            // 
            this.雾ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripFog});
            this.雾ToolStripMenuItem.Name = "雾ToolStripMenuItem";
            this.雾ToolStripMenuItem.Size = new System.Drawing.Size(56, 25);
            this.雾ToolStripMenuItem.Text = "雾效果";
            // 
            // toolStripFog
            // 
            this.toolStripFog.Name = "toolStripFog";
            this.toolStripFog.Size = new System.Drawing.Size(124, 22);
            this.toolStripFog.Text = "开启雾效";
            this.toolStripFog.Click += new System.EventHandler(this.toolStripFog_Click);
            // 
            // 上一视图ToolStripMenuItem
            // 
            this.上一视图ToolStripMenuItem.Name = "上一视图ToolStripMenuItem";
            this.上一视图ToolStripMenuItem.Size = new System.Drawing.Size(68, 25);
            this.上一视图ToolStripMenuItem.Text = "上一视图";
            this.上一视图ToolStripMenuItem.Click += new System.EventHandler(this.上一视图ToolStripMenuItem_Click);
            // 
            // 下一视图ToolStripMenuItem
            // 
            this.下一视图ToolStripMenuItem.Name = "下一视图ToolStripMenuItem";
            this.下一视图ToolStripMenuItem.Size = new System.Drawing.Size(68, 25);
            this.下一视图ToolStripMenuItem.Text = "下一视图";
            this.下一视图ToolStripMenuItem.Click += new System.EventHandler(this.下一视图ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripAbout});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 25);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // toolStripAbout
            // 
            this.toolStripAbout.Name = "toolStripAbout";
            this.toolStripAbout.Size = new System.Drawing.Size(100, 22);
            this.toolStripAbout.Text = "关于";
            this.toolStripAbout.Click += new System.EventHandler(this.toolStripAbout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 518);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HelloWorld";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.ListView skyboxListView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripFullScreen;
        private System.Windows.Forms.ToolStripMenuItem toolStripCaptureScreen;
        private System.Windows.Forms.ToolStripMenuItem 请设置天气ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxWeather;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripAbout;
        private System.Windows.Forms.ToolStripMenuItem 雾ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripFog;
        private System.Windows.Forms.ToolStripMenuItem 上一视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下一视图ToolStripMenuItem;


    }
}


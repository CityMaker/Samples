namespace RenderParam
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.长度单位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.米ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.公里ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.英尺ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.英里ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.海里ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.面积单位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.平方米ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.平方公里ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.公顷ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.亩ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.顷ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.英亩ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.平方英里ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.语言ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.简体中文ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.繁体中文ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.英文ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMeasurement = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCoordinate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripAerialDistance = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripHorizontalDistance = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripVerticalDistance = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripGroundDistance = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripArea = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripGroundArea = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripGroupSightLine = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripWalk = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2DMap = new System.Windows.Forms.ToolStripMenuItem();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.线框模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开启ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 33);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(679, 482);
            this.axRenderControl1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(685, 30);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.长度单位ToolStripMenuItem,
            this.面积单位ToolStripMenuItem,
            this.语言ToolStripMenuItem,
            this.线框模式ToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(178, 27);
            this.toolStripDropDownButton1.Text = "RenderControl全局配置参数";
            // 
            // 长度单位ToolStripMenuItem
            // 
            this.长度单位ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.米ToolStripMenuItem,
            this.公里ToolStripMenuItem,
            this.英尺ToolStripMenuItem,
            this.英里ToolStripMenuItem,
            this.海里ToolStripMenuItem});
            this.长度单位ToolStripMenuItem.Name = "长度单位ToolStripMenuItem";
            this.长度单位ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.长度单位ToolStripMenuItem.Text = "长度单位";
            // 
            // 米ToolStripMenuItem
            // 
            this.米ToolStripMenuItem.Name = "米ToolStripMenuItem";
            this.米ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.米ToolStripMenuItem.Text = "米";
            this.米ToolStripMenuItem.Click += new System.EventHandler(this.米ToolStripMenuItem_Click);
            // 
            // 公里ToolStripMenuItem
            // 
            this.公里ToolStripMenuItem.Name = "公里ToolStripMenuItem";
            this.公里ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.公里ToolStripMenuItem.Text = "公里";
            this.公里ToolStripMenuItem.Click += new System.EventHandler(this.公里ToolStripMenuItem_Click);
            // 
            // 英尺ToolStripMenuItem
            // 
            this.英尺ToolStripMenuItem.Name = "英尺ToolStripMenuItem";
            this.英尺ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.英尺ToolStripMenuItem.Text = "英尺";
            this.英尺ToolStripMenuItem.Click += new System.EventHandler(this.英尺ToolStripMenuItem_Click);
            // 
            // 英里ToolStripMenuItem
            // 
            this.英里ToolStripMenuItem.Name = "英里ToolStripMenuItem";
            this.英里ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.英里ToolStripMenuItem.Text = "英里";
            this.英里ToolStripMenuItem.Click += new System.EventHandler(this.英里ToolStripMenuItem_Click);
            // 
            // 海里ToolStripMenuItem
            // 
            this.海里ToolStripMenuItem.Name = "海里ToolStripMenuItem";
            this.海里ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.海里ToolStripMenuItem.Text = "海里";
            this.海里ToolStripMenuItem.Click += new System.EventHandler(this.海里ToolStripMenuItem_Click);
            // 
            // 面积单位ToolStripMenuItem
            // 
            this.面积单位ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.平方米ToolStripMenuItem,
            this.平方公里ToolStripMenuItem,
            this.公顷ToolStripMenuItem,
            this.亩ToolStripMenuItem,
            this.顷ToolStripMenuItem,
            this.英亩ToolStripMenuItem,
            this.平方英里ToolStripMenuItem});
            this.面积单位ToolStripMenuItem.Name = "面积单位ToolStripMenuItem";
            this.面积单位ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.面积单位ToolStripMenuItem.Text = "面积单位";
            // 
            // 平方米ToolStripMenuItem
            // 
            this.平方米ToolStripMenuItem.Name = "平方米ToolStripMenuItem";
            this.平方米ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.平方米ToolStripMenuItem.Text = "平方米";
            this.平方米ToolStripMenuItem.Click += new System.EventHandler(this.平方米ToolStripMenuItem_Click);
            // 
            // 平方公里ToolStripMenuItem
            // 
            this.平方公里ToolStripMenuItem.Name = "平方公里ToolStripMenuItem";
            this.平方公里ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.平方公里ToolStripMenuItem.Text = "平方公里";
            this.平方公里ToolStripMenuItem.Click += new System.EventHandler(this.平方公里ToolStripMenuItem_Click);
            // 
            // 公顷ToolStripMenuItem
            // 
            this.公顷ToolStripMenuItem.Name = "公顷ToolStripMenuItem";
            this.公顷ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.公顷ToolStripMenuItem.Text = "公顷";
            this.公顷ToolStripMenuItem.Click += new System.EventHandler(this.公顷ToolStripMenuItem_Click);
            // 
            // 亩ToolStripMenuItem
            // 
            this.亩ToolStripMenuItem.Name = "亩ToolStripMenuItem";
            this.亩ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.亩ToolStripMenuItem.Text = "亩";
            this.亩ToolStripMenuItem.Click += new System.EventHandler(this.亩ToolStripMenuItem_Click);
            // 
            // 顷ToolStripMenuItem
            // 
            this.顷ToolStripMenuItem.Name = "顷ToolStripMenuItem";
            this.顷ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.顷ToolStripMenuItem.Text = "顷";
            this.顷ToolStripMenuItem.Click += new System.EventHandler(this.顷ToolStripMenuItem_Click);
            // 
            // 英亩ToolStripMenuItem
            // 
            this.英亩ToolStripMenuItem.Name = "英亩ToolStripMenuItem";
            this.英亩ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.英亩ToolStripMenuItem.Text = "英亩";
            this.英亩ToolStripMenuItem.Click += new System.EventHandler(this.英亩ToolStripMenuItem_Click);
            // 
            // 平方英里ToolStripMenuItem
            // 
            this.平方英里ToolStripMenuItem.Name = "平方英里ToolStripMenuItem";
            this.平方英里ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.平方英里ToolStripMenuItem.Text = "平方英里";
            this.平方英里ToolStripMenuItem.Click += new System.EventHandler(this.平方英里ToolStripMenuItem_Click);
            // 
            // 语言ToolStripMenuItem
            // 
            this.语言ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.简体中文ToolStripMenuItem,
            this.繁体中文ToolStripMenuItem,
            this.英文ToolStripMenuItem});
            this.语言ToolStripMenuItem.Name = "语言ToolStripMenuItem";
            this.语言ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.语言ToolStripMenuItem.Text = "语言";
            // 
            // 简体中文ToolStripMenuItem
            // 
            this.简体中文ToolStripMenuItem.Name = "简体中文ToolStripMenuItem";
            this.简体中文ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.简体中文ToolStripMenuItem.Text = "简体中文";
            this.简体中文ToolStripMenuItem.Click += new System.EventHandler(this.简体中文ToolStripMenuItem_Click);
            // 
            // 繁体中文ToolStripMenuItem
            // 
            this.繁体中文ToolStripMenuItem.Name = "繁体中文ToolStripMenuItem";
            this.繁体中文ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.繁体中文ToolStripMenuItem.Text = "繁体中文";
            this.繁体中文ToolStripMenuItem.Click += new System.EventHandler(this.繁体中文ToolStripMenuItem_Click);
            // 
            // 英文ToolStripMenuItem
            // 
            this.英文ToolStripMenuItem.Name = "英文ToolStripMenuItem";
            this.英文ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.英文ToolStripMenuItem.Text = "英文";
            this.英文ToolStripMenuItem.Click += new System.EventHandler(this.英文ToolStripMenuItem_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripNormal,
            this.toolStripSelect,
            this.toolStripMeasurement,
            this.toolStripWalk,
            this.toolStripDisable,
            this.toolStrip2DMap});
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(69, 27);
            this.toolStripLabel1.Text = "交互模式";
            // 
            // toolStripNormal
            // 
            this.toolStripNormal.Name = "toolStripNormal";
            this.toolStripNormal.Size = new System.Drawing.Size(124, 22);
            this.toolStripNormal.Text = "漫游";
            this.toolStripNormal.Click += new System.EventHandler(this.toolStripNormal_Click);
            // 
            // toolStripSelect
            // 
            this.toolStripSelect.Name = "toolStripSelect";
            this.toolStripSelect.Size = new System.Drawing.Size(124, 22);
            this.toolStripSelect.Text = "拾取";
            this.toolStripSelect.Click += new System.EventHandler(this.toolStripSelect_Click);
            // 
            // toolStripMeasurement
            // 
            this.toolStripMeasurement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripCoordinate,
            this.toolStripAerialDistance,
            this.toolStripHorizontalDistance,
            this.toolStripVerticalDistance,
            this.toolStripGroundDistance,
            this.toolStripArea,
            this.toolStripGroundArea,
            this.toolStripGroupSightLine});
            this.toolStripMeasurement.Name = "toolStripMeasurement";
            this.toolStripMeasurement.Size = new System.Drawing.Size(124, 22);
            this.toolStripMeasurement.Text = "测量";
            // 
            // toolStripCoordinate
            // 
            this.toolStripCoordinate.Name = "toolStripCoordinate";
            this.toolStripCoordinate.Size = new System.Drawing.Size(148, 22);
            this.toolStripCoordinate.Text = "拾取坐标";
            this.toolStripCoordinate.Click += new System.EventHandler(this.toolStripCoordinate_Click);
            // 
            // toolStripAerialDistance
            // 
            this.toolStripAerialDistance.Name = "toolStripAerialDistance";
            this.toolStripAerialDistance.Size = new System.Drawing.Size(148, 22);
            this.toolStripAerialDistance.Text = "直线测距";
            this.toolStripAerialDistance.Click += new System.EventHandler(this.toolStripAerialDistance_Click);
            // 
            // toolStripHorizontalDistance
            // 
            this.toolStripHorizontalDistance.Name = "toolStripHorizontalDistance";
            this.toolStripHorizontalDistance.Size = new System.Drawing.Size(148, 22);
            this.toolStripHorizontalDistance.Text = "水平测距";
            this.toolStripHorizontalDistance.Click += new System.EventHandler(this.toolStripHorizontalDistance_Click);
            // 
            // toolStripVerticalDistance
            // 
            this.toolStripVerticalDistance.Name = "toolStripVerticalDistance";
            this.toolStripVerticalDistance.Size = new System.Drawing.Size(148, 22);
            this.toolStripVerticalDistance.Text = "垂直测距";
            this.toolStripVerticalDistance.Click += new System.EventHandler(this.toolStripVerticalDistance_Click);
            // 
            // toolStripGroundDistance
            // 
            this.toolStripGroundDistance.Name = "toolStripGroundDistance";
            this.toolStripGroundDistance.Size = new System.Drawing.Size(148, 22);
            this.toolStripGroundDistance.Text = "地表距离";
            this.toolStripGroundDistance.Click += new System.EventHandler(this.toolStripGroundDistance_Click);
            // 
            // toolStripArea
            // 
            this.toolStripArea.Name = "toolStripArea";
            this.toolStripArea.Size = new System.Drawing.Size(148, 22);
            this.toolStripArea.Text = "投影面积";
            this.toolStripArea.Click += new System.EventHandler(this.toolStripArea_Click);
            // 
            // toolStripGroundArea
            // 
            this.toolStripGroundArea.Name = "toolStripGroundArea";
            this.toolStripGroundArea.Size = new System.Drawing.Size(148, 22);
            this.toolStripGroundArea.Text = "地表面积";
            this.toolStripGroundArea.Click += new System.EventHandler(this.toolStripGroundArea_Click);
            // 
            // toolStripGroupSightLine
            // 
            this.toolStripGroupSightLine.Name = "toolStripGroupSightLine";
            this.toolStripGroupSightLine.Size = new System.Drawing.Size(148, 22);
            this.toolStripGroupSightLine.Text = "地形通视分析";
            this.toolStripGroupSightLine.Click += new System.EventHandler(this.toolStripGroupSightLine_Click);
            // 
            // toolStripWalk
            // 
            this.toolStripWalk.Name = "toolStripWalk";
            this.toolStripWalk.Size = new System.Drawing.Size(124, 22);
            this.toolStripWalk.Text = "步行";
            this.toolStripWalk.Click += new System.EventHandler(this.toolStripWalk_Click);
            // 
            // toolStripDisable
            // 
            this.toolStripDisable.Name = "toolStripDisable";
            this.toolStripDisable.Size = new System.Drawing.Size(124, 22);
            this.toolStripDisable.Text = "禁止交互";
            this.toolStripDisable.Click += new System.EventHandler(this.toolStripDisable_Click);
            // 
            // toolStrip2DMap
            // 
            this.toolStrip2DMap.Name = "toolStrip2DMap";
            this.toolStrip2DMap.Size = new System.Drawing.Size(124, 22);
            this.toolStrip2DMap.Text = "二维地图";
            this.toolStrip2DMap.Click += new System.EventHandler(this.toolStrip2DMap_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 518);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // 线框模式ToolStripMenuItem
            // 
            this.线框模式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开启ToolStripMenuItem,
            this.关闭ToolStripMenuItem});
            this.线框模式ToolStripMenuItem.Name = "线框模式ToolStripMenuItem";
            this.线框模式ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.线框模式ToolStripMenuItem.Text = "线框模式";
            // 
            // 开启ToolStripMenuItem
            // 
            this.开启ToolStripMenuItem.Name = "开启ToolStripMenuItem";
            this.开启ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.开启ToolStripMenuItem.Text = "开启";
            this.开启ToolStripMenuItem.Click += new System.EventHandler(this.开启ToolStripMenuItem_Click);
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
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
            this.Text = "RenderParam";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripLabel1;
        private System.Windows.Forms.ToolStripMenuItem toolStripNormal;
        private System.Windows.Forms.ToolStripMenuItem toolStripSelect;
        private System.Windows.Forms.ToolStripMenuItem toolStripMeasurement;
        private System.Windows.Forms.ToolStripMenuItem toolStripAerialDistance;
        private System.Windows.Forms.ToolStripMenuItem toolStripHorizontalDistance;
        private System.Windows.Forms.ToolStripMenuItem toolStripVerticalDistance;
        private System.Windows.Forms.ToolStripMenuItem toolStripWalk;
        private System.Windows.Forms.ToolStripMenuItem toolStripCoordinate;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem toolStrip2DMap;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem 长度单位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 米ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 公里ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 英尺ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 英里ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 海里ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 面积单位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 语言ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 平方米ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 平方公里ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 公顷ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 亩ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 顷ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 英亩ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 平方英里ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 简体中文ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 繁体中文ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 英文ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripDisable;
        private System.Windows.Forms.ToolStripMenuItem toolStripGroundDistance;
        private System.Windows.Forms.ToolStripMenuItem toolStripArea;
        private System.Windows.Forms.ToolStripMenuItem toolStripGroundArea;
        private System.Windows.Forms.ToolStripMenuItem toolStripGroupSightLine;
        private System.Windows.Forms.ToolStripMenuItem 线框模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开启ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;

    }
}


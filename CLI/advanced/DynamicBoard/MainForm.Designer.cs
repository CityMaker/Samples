namespace DynamicBoard
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnStartRealtime = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSelectBiaopan = new System.Windows.Forms.Button();
            this.btnRover = new System.Windows.Forms.Button();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(679, 512);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 518F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 518);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // btnStartRealtime
            // 
            this.btnStartRealtime.Location = new System.Drawing.Point(112, 0);
            this.btnStartRealtime.Name = "btnStartRealtime";
            this.btnStartRealtime.Size = new System.Drawing.Size(112, 23);
            this.btnStartRealtime.TabIndex = 3;
            this.btnStartRealtime.Text = "实时读取仪表值";
            this.btnStartRealtime.UseVisualStyleBackColor = true;
            this.btnStartRealtime.Click += new System.EventHandler(this.btnStartRealtime_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnSelectBiaopan
            // 
            this.btnSelectBiaopan.Location = new System.Drawing.Point(272, 0);
            this.btnSelectBiaopan.Name = "btnSelectBiaopan";
            this.btnSelectBiaopan.Size = new System.Drawing.Size(156, 23);
            this.btnSelectBiaopan.TabIndex = 4;
            this.btnSelectBiaopan.Text = "拾取表盘实时查看压力值";
            this.btnSelectBiaopan.UseVisualStyleBackColor = true;
            this.btnSelectBiaopan.Click += new System.EventHandler(this.btnSelectBiaopan_Click);
            // 
            // btnRover
            // 
            this.btnRover.Location = new System.Drawing.Point(502, 0);
            this.btnRover.Name = "btnRover";
            this.btnRover.Size = new System.Drawing.Size(75, 23);
            this.btnRover.TabIndex = 5;
            this.btnRover.Text = "恢复漫游";
            this.btnRover.UseVisualStyleBackColor = true;
            this.btnRover.Click += new System.EventHandler(this.btnRover_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 518);
            this.Controls.Add(this.btnRover);
            this.Controls.Add(this.btnSelectBiaopan);
            this.Controls.Add(this.btnStartRealtime);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "仪表盘";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnStartRealtime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnSelectBiaopan;
        private System.Windows.Forms.Button btnRover;

    }
}


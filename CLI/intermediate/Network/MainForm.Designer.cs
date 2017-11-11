namespace Network
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
            this.txtMaxNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCutoff = new System.Windows.Forms.TextBox();
            this.btnFindWC = new System.Windows.Forms.Button();
            this.btnImHere = new System.Windows.Forms.Button();
            this.cbCameraFollow = new System.Windows.Forms.CheckBox();
            this.btnNavigate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearchTolerance = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLoaderTolerance = new System.Windows.Forms.TextBox();
            this.btnBuildRoute = new System.Windows.Forms.Button();
            this.txtLocationNames = new System.Windows.Forms.TextBox();
            this.btnSelectLocation = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbOrderPolicy = new System.Windows.Forms.ComboBox();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(571, 539);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(790, 545);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbOrderPolicy);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtMaxNum);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtCutoff);
            this.panel1.Controls.Add(this.btnFindWC);
            this.panel1.Controls.Add(this.btnImHere);
            this.panel1.Controls.Add(this.cbCameraFollow);
            this.panel1.Controls.Add(this.btnNavigate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtSearchTolerance);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtLoaderTolerance);
            this.panel1.Controls.Add(this.btnBuildRoute);
            this.panel1.Controls.Add(this.txtLocationNames);
            this.panel1.Controls.Add(this.btnSelectLocation);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(580, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 539);
            this.panel1.TabIndex = 1;
            // 
            // txtMaxNum
            // 
            this.txtMaxNum.Location = new System.Drawing.Point(110, 482);
            this.txtMaxNum.Name = "txtMaxNum";
            this.txtMaxNum.Size = new System.Drawing.Size(66, 21);
            this.txtMaxNum.TabIndex = 14;
            this.txtMaxNum.Text = "3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 485);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "最多查找多少个";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 452);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "多少米内的厕所";
            // 
            // txtCutoff
            // 
            this.txtCutoff.Location = new System.Drawing.Point(109, 449);
            this.txtCutoff.Name = "txtCutoff";
            this.txtCutoff.Size = new System.Drawing.Size(66, 21);
            this.txtCutoff.TabIndex = 11;
            this.txtCutoff.Text = "500";
            // 
            // btnFindWC
            // 
            this.btnFindWC.Location = new System.Drawing.Point(13, 414);
            this.btnFindWC.Name = "btnFindWC";
            this.btnFindWC.Size = new System.Drawing.Size(117, 29);
            this.btnFindWC.TabIndex = 10;
            this.btnFindWC.Text = "找厕所-我在这!";
            this.btnFindWC.UseVisualStyleBackColor = true;
            this.btnFindWC.Click += new System.EventHandler(this.btnFindWC_Click);
            // 
            // btnImHere
            // 
            this.btnImHere.Location = new System.Drawing.Point(94, 115);
            this.btnImHere.Name = "btnImHere";
            this.btnImHere.Size = new System.Drawing.Size(76, 29);
            this.btnImHere.TabIndex = 9;
            this.btnImHere.Text = "我在这!";
            this.btnImHere.UseVisualStyleBackColor = true;
            this.btnImHere.Click += new System.EventHandler(this.btnSelectLocation_Click);
            // 
            // cbCameraFollow
            // 
            this.cbCameraFollow.AutoSize = true;
            this.cbCameraFollow.Location = new System.Drawing.Point(104, 353);
            this.cbCameraFollow.Name = "cbCameraFollow";
            this.cbCameraFollow.Size = new System.Drawing.Size(72, 16);
            this.cbCameraFollow.TabIndex = 8;
            this.cbCameraFollow.Text = "相机跟随";
            this.cbCameraFollow.UseVisualStyleBackColor = true;
            this.cbCameraFollow.CheckedChanged += new System.EventHandler(this.cbCameraFollow_CheckedChanged);
            // 
            // btnNavigate
            // 
            this.btnNavigate.Enabled = false;
            this.btnNavigate.Location = new System.Drawing.Point(12, 344);
            this.btnNavigate.Name = "btnNavigate";
            this.btnNavigate.Size = new System.Drawing.Size(91, 32);
            this.btnNavigate.TabIndex = 7;
            this.btnNavigate.Text = "模拟导航";
            this.btnNavigate.UseVisualStyleBackColor = true;
            this.btnNavigate.Click += new System.EventHandler(this.btnNavigate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "LocationSearchTolerance";
            // 
            // txtSearchTolerance
            // 
            this.txtSearchTolerance.Location = new System.Drawing.Point(96, 78);
            this.txtSearchTolerance.Name = "txtSearchTolerance";
            this.txtSearchTolerance.Size = new System.Drawing.Size(68, 21);
            this.txtSearchTolerance.TabIndex = 5;
            this.txtSearchTolerance.Text = "60";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Loader线连通的容差";
            // 
            // txtLoaderTolerance
            // 
            this.txtLoaderTolerance.Location = new System.Drawing.Point(96, 30);
            this.txtLoaderTolerance.Name = "txtLoaderTolerance";
            this.txtLoaderTolerance.Size = new System.Drawing.Size(68, 21);
            this.txtLoaderTolerance.TabIndex = 3;
            this.txtLoaderTolerance.Text = "1";
            // 
            // btnBuildRoute
            // 
            this.btnBuildRoute.Location = new System.Drawing.Point(12, 306);
            this.btnBuildRoute.Name = "btnBuildRoute";
            this.btnBuildRoute.Size = new System.Drawing.Size(91, 32);
            this.btnBuildRoute.TabIndex = 2;
            this.btnBuildRoute.Text = "生成最优路径";
            this.btnBuildRoute.UseVisualStyleBackColor = true;
            this.btnBuildRoute.Click += new System.EventHandler(this.btnBuildRoute_Click);
            // 
            // txtLocationNames
            // 
            this.txtLocationNames.Location = new System.Drawing.Point(12, 150);
            this.txtLocationNames.Multiline = true;
            this.txtLocationNames.Name = "txtLocationNames";
            this.txtLocationNames.Size = new System.Drawing.Size(153, 94);
            this.txtLocationNames.TabIndex = 1;
            // 
            // btnSelectLocation
            // 
            this.btnSelectLocation.Location = new System.Drawing.Point(12, 115);
            this.btnSelectLocation.Name = "btnSelectLocation";
            this.btnSelectLocation.Size = new System.Drawing.Size(76, 29);
            this.btnSelectLocation.TabIndex = 0;
            this.btnSelectLocation.Text = "拾取景点";
            this.btnSelectLocation.UseVisualStyleBackColor = true;
            this.btnSelectLocation.Click += new System.EventHandler(this.btnSelectLocation_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 260);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "顺序策略";
            // 
            // cbOrderPolicy
            // 
            this.cbOrderPolicy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrderPolicy.FormattingEnabled = true;
            this.cbOrderPolicy.Items.AddRange(new object[] {
            "按顺序 ",
            "固定起点，其他可以改变顺序 ",
            "固定起点，中间可以改变顺序，最后返回起点 ",
            "固定首尾点，中间可以改变顺序 ",
            "允许随意排列"});
            this.cbOrderPolicy.Location = new System.Drawing.Point(13, 280);
            this.cbOrderPolicy.Name = "cbOrderPolicy";
            this.cbOrderPolicy.Size = new System.Drawing.Size(172, 20);
            this.cbOrderPolicy.TabIndex = 16;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 545);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Network";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSelectLocation;
        private System.Windows.Forms.TextBox txtLocationNames;
        private System.Windows.Forms.Button btnBuildRoute;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLoaderTolerance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearchTolerance;
        private System.Windows.Forms.Button btnNavigate;
        private System.Windows.Forms.CheckBox cbCameraFollow;
        private System.Windows.Forms.Button btnImHere;
        private System.Windows.Forms.Button btnFindWC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCutoff;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMaxNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbOrderPolicy;

    }
}


namespace ParticleEffect
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnLocateSkinmesh = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.btnSkinnedMeshStop = new System.Windows.Forms.Button();
            this.btnSkinnedMeshPause = new System.Windows.Forms.Button();
            this.btnSkinnedMeshPlay = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLocate = new System.Windows.Forms.Button();
            this.btnProperty = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbComplexType = new System.Windows.Forms.ComboBox();
            this.btnComplexProperty = new System.Windows.Forms.Button();
            this.btnComplexLocate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            
            this.groupBox4.SuspendLayout();
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
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(664, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 579);
            this.panel1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnLocateSkinmesh);
            this.groupBox3.Controls.Add(this.comboBox2);
            this.groupBox3.Controls.Add(this.btnSkinnedMeshStop);
            this.groupBox3.Controls.Add(this.btnSkinnedMeshPause);
            this.groupBox3.Controls.Add(this.btnSkinnedMeshPlay);
            this.groupBox3.Location = new System.Drawing.Point(9, 139);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 105);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "骨骼动画";
            // 
            // btnLocateSkinmesh
            // 
            this.btnLocateSkinmesh.Location = new System.Drawing.Point(139, 26);
            this.btnLocateSkinmesh.Name = "btnLocateSkinmesh";
            this.btnLocateSkinmesh.Size = new System.Drawing.Size(55, 25);
            this.btnLocateSkinmesh.TabIndex = 4;
            this.btnLocateSkinmesh.Text = "定位";
            this.btnLocateSkinmesh.UseVisualStyleBackColor = true;
            this.btnLocateSkinmesh.Click += new System.EventHandler(this.btnLocateSkinmesh_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "人",
            "树1",
            "树2",
            "树3",
            "无人机"});
            this.comboBox2.Location = new System.Drawing.Point(11, 29);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 6;
            // 
            // btnSkinnedMeshStop
            // 
            this.btnSkinnedMeshStop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSkinnedMeshStop.BackgroundImage")));
            this.btnSkinnedMeshStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSkinnedMeshStop.Location = new System.Drawing.Point(89, 65);
            this.btnSkinnedMeshStop.Name = "btnSkinnedMeshStop";
            this.btnSkinnedMeshStop.Size = new System.Drawing.Size(23, 23);
            this.btnSkinnedMeshStop.TabIndex = 5;
            this.btnSkinnedMeshStop.UseVisualStyleBackColor = true;
            this.btnSkinnedMeshStop.Click += new System.EventHandler(this.btnSkinnedMeshStop_Click);
            // 
            // btnSkinnedMeshPause
            // 
            this.btnSkinnedMeshPause.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSkinnedMeshPause.BackgroundImage")));
            this.btnSkinnedMeshPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSkinnedMeshPause.Location = new System.Drawing.Point(49, 65);
            this.btnSkinnedMeshPause.Name = "btnSkinnedMeshPause";
            this.btnSkinnedMeshPause.Size = new System.Drawing.Size(23, 23);
            this.btnSkinnedMeshPause.TabIndex = 4;
            this.btnSkinnedMeshPause.UseVisualStyleBackColor = true;
            this.btnSkinnedMeshPause.Click += new System.EventHandler(this.btnSkinnedMeshPause_Click);
            // 
            // btnSkinnedMeshPlay
            // 
            this.btnSkinnedMeshPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSkinnedMeshPlay.BackgroundImage")));
            this.btnSkinnedMeshPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSkinnedMeshPlay.Location = new System.Drawing.Point(9, 65);
            this.btnSkinnedMeshPlay.Name = "btnSkinnedMeshPlay";
            this.btnSkinnedMeshPlay.Size = new System.Drawing.Size(23, 23);
            this.btnSkinnedMeshPlay.TabIndex = 3;
            this.btnSkinnedMeshPlay.UseVisualStyleBackColor = true;
            this.btnSkinnedMeshPlay.Click += new System.EventHandler(this.btnSkinnedMeshPlay_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.btnPause);
            this.groupBox2.Controls.Add(this.btnPlay);
            this.groupBox2.Location = new System.Drawing.Point(9, 263);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 61);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "运动物体";
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStop.BackgroundImage")));
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStop.Location = new System.Drawing.Point(89, 21);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(23, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPause.BackgroundImage")));
            this.btnPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPause.Location = new System.Drawing.Point(49, 21);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(23, 23);
            this.btnPause.TabIndex = 1;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPlay.BackgroundImage")));
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPlay.Location = new System.Drawing.Point(9, 21);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(23, 23);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLocate);
            this.groupBox1.Controls.Add(this.btnProperty);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "粒子特效";
            // 
            // btnLocate
            // 
            this.btnLocate.Location = new System.Drawing.Point(102, 66);
            this.btnLocate.Name = "btnLocate";
            this.btnLocate.Size = new System.Drawing.Size(87, 23);
            this.btnLocate.TabIndex = 3;
            this.btnLocate.Text = "定位";
            this.btnLocate.UseVisualStyleBackColor = true;
            this.btnLocate.Click += new System.EventHandler(this.btnLocate_Click);
            // 
            // btnProperty
            // 
            this.btnProperty.Location = new System.Drawing.Point(9, 66);
            this.btnProperty.Name = "btnProperty";
            this.btnProperty.Size = new System.Drawing.Size(75, 23);
            this.btnProperty.TabIndex = 2;
            this.btnProperty.Text = "属性";
            this.btnProperty.UseVisualStyleBackColor = true;
            this.btnProperty.Click += new System.EventHandler(this.btnProperty_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "粒子火",
            "粒子烟",
            "粒子水"});
            this.comboBox1.Location = new System.Drawing.Point(74, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(115, 20);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "粒子类型：";
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(655, 579);
            this.axRenderControl1.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnComplexLocate);
            this.groupBox4.Controls.Add(this.btnComplexProperty);
            this.groupBox4.Controls.Add(this.cbComplexType);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(9, 344);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 102);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "组合粒子特效";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "粒子类型：";
            // 
            // cbComplexType
            // 
            this.cbComplexType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComplexType.FormattingEnabled = true;
            this.cbComplexType.Items.AddRange(new object[] {
            "Fire_0",
            "Fire_1",
            "Fire_2",
            "Fire_3",
            "Fire_4",
            "Smoke_0",
            "Smoke_1",
            "Smoke_2",
            "Explosion_0",
            "Explosion_1",
            "Explosion_2",
            "Explosion_3",
            "Explosion_4",
            "Explosion_5",
            "Explosion_6",
            "Explosion_7",
            "Explosion_8",
            "RocketTailFlame"});
            this.cbComplexType.Location = new System.Drawing.Point(74, 27);
            this.cbComplexType.Name = "cbComplexType";
            this.cbComplexType.Size = new System.Drawing.Size(115, 20);
            this.cbComplexType.TabIndex = 4;
            // 
            // btnComplexProperty
            // 
            this.btnComplexProperty.Location = new System.Drawing.Point(11, 62);
            this.btnComplexProperty.Name = "btnComplexProperty";
            this.btnComplexProperty.Size = new System.Drawing.Size(75, 23);
            this.btnComplexProperty.TabIndex = 4;
            this.btnComplexProperty.Text = "属性";
            this.btnComplexProperty.UseVisualStyleBackColor = true;
            this.btnComplexProperty.Click += new System.EventHandler(this.btnComplexProperty_Click);
            // 
            // btnComplexLocate
            // 
            this.btnComplexLocate.Location = new System.Drawing.Point(102, 62);
            this.btnComplexLocate.Name = "btnComplexLocate";
            this.btnComplexLocate.Size = new System.Drawing.Size(87, 23);
            this.btnComplexLocate.TabIndex = 4;
            this.btnComplexLocate.Text = "定位";
            this.btnComplexLocate.UseVisualStyleBackColor = true;
            this.btnComplexLocate.Click += new System.EventHandler(this.btnComplexLocate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 585);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ParticleEffect";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnProperty;
        private System.Windows.Forms.Button btnLocate;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSkinnedMeshStop;
        private System.Windows.Forms.Button btnSkinnedMeshPause;
        private System.Windows.Forms.Button btnSkinnedMeshPlay;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button btnLocateSkinmesh;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbComplexType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnComplexLocate;
        private System.Windows.Forms.Button btnComplexProperty;

    }
}


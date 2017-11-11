namespace MultipleScreen
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonPIP = new System.Windows.Forms.RadioButton();
            this.radioButtonT1B1SingleFrustum = new System.Windows.Forms.RadioButton();
            this.radioButtonL1R1SingleFrustum = new System.Windows.Forms.RadioButton();
            this.radioButtonL1R2 = new System.Windows.Forms.RadioButton();
            this.radioButtonL2R1 = new System.Windows.Forms.RadioButton();
            this.radioButtonT1M1B1 = new System.Windows.Forms.RadioButton();
            this.radioButtonL1M1R1 = new System.Windows.Forms.RadioButton();
            this.radioButtonT1B1 = new System.Windows.Forms.RadioButton();
            this.radioButtonL1R1 = new System.Windows.Forms.RadioButton();
            this.radioButtonQuadH = new System.Windows.Forms.RadioButton();
            this.radioButtonQuad = new System.Windows.Forms.RadioButton();
            this.cbCameraBind = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbShowBorder = new System.Windows.Forms.CheckBox();
            this.SplitRatioH = new System.Windows.Forms.Label();
            this.cbCompassVisibleMask = new System.Windows.Forms.ComboBox();
            this.CompassVisibleMask = new System.Windows.Forms.Label();
            this.cbCameraInfoVisible = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.SplitRatioV = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1205, 673);
            this.tableLayoutPanel1.TabIndex = 39;
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 83);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(1199, 527);
            this.axRenderControl1.TabIndex = 39;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.cbCameraBind);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1199, 34);
            this.panel1.TabIndex = 38;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonPIP);
            this.groupBox1.Controls.Add(this.radioButtonT1B1SingleFrustum);
            this.groupBox1.Controls.Add(this.radioButtonL1R1SingleFrustum);
            this.groupBox1.Controls.Add(this.radioButtonL1R2);
            this.groupBox1.Controls.Add(this.radioButtonL2R1);
            this.groupBox1.Controls.Add(this.radioButtonT1M1B1);
            this.groupBox1.Controls.Add(this.radioButtonL1M1R1);
            this.groupBox1.Controls.Add(this.radioButtonT1B1);
            this.groupBox1.Controls.Add(this.radioButtonL1R1);
            this.groupBox1.Controls.Add(this.radioButtonQuadH);
            this.groupBox1.Controls.Add(this.radioButtonQuad);
            this.groupBox1.Location = new System.Drawing.Point(111, -3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1012, 35);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // radioButtonPIP
            // 
            this.radioButtonPIP.AutoSize = true;
            this.radioButtonPIP.Location = new System.Drawing.Point(925, 14);
            this.radioButtonPIP.Name = "radioButtonPIP";
            this.radioButtonPIP.Size = new System.Drawing.Size(59, 16);
            this.radioButtonPIP.TabIndex = 10;
            this.radioButtonPIP.TabStop = true;
            this.radioButtonPIP.Text = "画中画";
            this.radioButtonPIP.UseVisualStyleBackColor = true;
            this.radioButtonPIP.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonT1B1SingleFrustum
            // 
            this.radioButtonT1B1SingleFrustum.AutoSize = true;
            this.radioButtonT1B1SingleFrustum.Location = new System.Drawing.Point(846, 14);
            this.radioButtonT1B1SingleFrustum.Name = "radioButtonT1B1SingleFrustum";
            this.radioButtonT1B1SingleFrustum.Size = new System.Drawing.Size(71, 16);
            this.radioButtonT1B1SingleFrustum.TabIndex = 9;
            this.radioButtonT1B1SingleFrustum.TabStop = true;
            this.radioButtonT1B1SingleFrustum.Text = "垂直分屏";
            this.radioButtonT1B1SingleFrustum.UseVisualStyleBackColor = true;
            this.radioButtonT1B1SingleFrustum.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonL1R1SingleFrustum
            // 
            this.radioButtonL1R1SingleFrustum.AutoSize = true;
            this.radioButtonL1R1SingleFrustum.Location = new System.Drawing.Point(756, 14);
            this.radioButtonL1R1SingleFrustum.Name = "radioButtonL1R1SingleFrustum";
            this.radioButtonL1R1SingleFrustum.Size = new System.Drawing.Size(71, 16);
            this.radioButtonL1R1SingleFrustum.TabIndex = 8;
            this.radioButtonL1R1SingleFrustum.TabStop = true;
            this.radioButtonL1R1SingleFrustum.Text = "水平分屏";
            this.radioButtonL1R1SingleFrustum.UseVisualStyleBackColor = true;
            this.radioButtonL1R1SingleFrustum.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonL1R2
            // 
            this.radioButtonL1R2.AutoSize = true;
            this.radioButtonL1R2.Location = new System.Drawing.Point(666, 14);
            this.radioButtonL1R2.Name = "radioButtonL1R2";
            this.radioButtonL1R2.Size = new System.Drawing.Size(71, 16);
            this.radioButtonL1R2.TabIndex = 7;
            this.radioButtonL1R2.TabStop = true;
            this.radioButtonL1R2.Text = "左一右二";
            this.radioButtonL1R2.UseVisualStyleBackColor = true;
            this.radioButtonL1R2.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonL2R1
            // 
            this.radioButtonL2R1.AutoSize = true;
            this.radioButtonL2R1.Location = new System.Drawing.Point(578, 14);
            this.radioButtonL2R1.Name = "radioButtonL2R1";
            this.radioButtonL2R1.Size = new System.Drawing.Size(71, 16);
            this.radioButtonL2R1.TabIndex = 6;
            this.radioButtonL2R1.TabStop = true;
            this.radioButtonL2R1.Text = "左二右一";
            this.radioButtonL2R1.UseVisualStyleBackColor = true;
            this.radioButtonL2R1.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonT1M1B1
            // 
            this.radioButtonT1M1B1.AutoSize = true;
            this.radioButtonT1M1B1.Location = new System.Drawing.Point(466, 14);
            this.radioButtonT1M1B1.Name = "radioButtonT1M1B1";
            this.radioButtonT1M1B1.Size = new System.Drawing.Size(95, 16);
            this.radioButtonT1M1B1.TabIndex = 5;
            this.radioButtonT1M1B1.TabStop = true;
            this.radioButtonT1M1B1.Text = "上一中一下一";
            this.radioButtonT1M1B1.UseVisualStyleBackColor = true;
            this.radioButtonT1M1B1.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonL1M1R1
            // 
            this.radioButtonL1M1R1.AutoSize = true;
            this.radioButtonL1M1R1.Location = new System.Drawing.Point(354, 14);
            this.radioButtonL1M1R1.Name = "radioButtonL1M1R1";
            this.radioButtonL1M1R1.Size = new System.Drawing.Size(95, 16);
            this.radioButtonL1M1R1.TabIndex = 4;
            this.radioButtonL1M1R1.TabStop = true;
            this.radioButtonL1M1R1.Text = "左一中一右一";
            this.radioButtonL1M1R1.UseVisualStyleBackColor = true;
            this.radioButtonL1M1R1.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonT1B1
            // 
            this.radioButtonT1B1.AutoSize = true;
            this.radioButtonT1B1.Location = new System.Drawing.Point(266, 14);
            this.radioButtonT1B1.Name = "radioButtonT1B1";
            this.radioButtonT1B1.Size = new System.Drawing.Size(71, 16);
            this.radioButtonT1B1.TabIndex = 3;
            this.radioButtonT1B1.TabStop = true;
            this.radioButtonT1B1.Text = "上一下一";
            this.radioButtonT1B1.UseVisualStyleBackColor = true;
            this.radioButtonT1B1.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonL1R1
            // 
            this.radioButtonL1R1.AutoSize = true;
            this.radioButtonL1R1.Location = new System.Drawing.Point(178, 14);
            this.radioButtonL1R1.Name = "radioButtonL1R1";
            this.radioButtonL1R1.Size = new System.Drawing.Size(71, 16);
            this.radioButtonL1R1.TabIndex = 2;
            this.radioButtonL1R1.TabStop = true;
            this.radioButtonL1R1.Text = "左一右一";
            this.radioButtonL1R1.UseVisualStyleBackColor = true;
            this.radioButtonL1R1.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonQuadH
            // 
            this.radioButtonQuadH.AutoSize = true;
            this.radioButtonQuadH.Location = new System.Drawing.Point(80, 14);
            this.radioButtonQuadH.Name = "radioButtonQuadH";
            this.radioButtonQuadH.Size = new System.Drawing.Size(83, 16);
            this.radioButtonQuadH.TabIndex = 1;
            this.radioButtonQuadH.TabStop = true;
            this.radioButtonQuadH.Text = "水平四视口";
            this.radioButtonQuadH.UseVisualStyleBackColor = true;
            this.radioButtonQuadH.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonQuad
            // 
            this.radioButtonQuad.AutoSize = true;
            this.radioButtonQuad.Checked = true;
            this.radioButtonQuad.Location = new System.Drawing.Point(6, 15);
            this.radioButtonQuad.Name = "radioButtonQuad";
            this.radioButtonQuad.Size = new System.Drawing.Size(59, 16);
            this.radioButtonQuad.TabIndex = 0;
            this.radioButtonQuad.TabStop = true;
            this.radioButtonQuad.Text = "四视口";
            this.radioButtonQuad.UseVisualStyleBackColor = true;
            this.radioButtonQuad.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // cbCameraBind
            // 
            this.cbCameraBind.AutoSize = true;
            this.cbCameraBind.Location = new System.Drawing.Point(12, 12);
            this.cbCameraBind.Name = "cbCameraBind";
            this.cbCameraBind.Size = new System.Drawing.Size(72, 16);
            this.cbCameraBind.TabIndex = 2;
            this.cbCameraBind.Text = "相机绑定";
            this.cbCameraBind.UseVisualStyleBackColor = true;
            this.cbCameraBind.CheckedChanged += new System.EventHandler(this.cbCameraBind_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbShowBorder);
            this.panel2.Controls.Add(this.cbCompassVisibleMask);
            this.panel2.Controls.Add(this.CompassVisibleMask);
            this.panel2.Controls.Add(this.cbCameraInfoVisible);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1199, 34);
            this.panel2.TabIndex = 40;
            // 
            // cbShowBorder
            // 
            this.cbShowBorder.AutoSize = true;
            this.cbShowBorder.Location = new System.Drawing.Point(437, 9);
            this.cbShowBorder.Name = "cbShowBorder";
            this.cbShowBorder.Size = new System.Drawing.Size(84, 16);
            this.cbShowBorder.TabIndex = 7;
            this.cbShowBorder.Text = "ShowBorder";
            this.cbShowBorder.UseVisualStyleBackColor = true;
            this.cbShowBorder.CheckedChanged += new System.EventHandler(this.cbShowBorder_CheckedChanged);
            // 
            // SplitRatioH
            // 
            this.SplitRatioH.AutoSize = true;
            this.SplitRatioH.Location = new System.Drawing.Point(5, 7);
            this.SplitRatioH.Name = "SplitRatioH";
            this.SplitRatioH.Size = new System.Drawing.Size(71, 12);
            this.SplitRatioH.TabIndex = 6;
            this.SplitRatioH.Text = "SplitRatioH";
            // 
            // cbCompassVisibleMask
            // 
            this.cbCompassVisibleMask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompassVisibleMask.FormattingEnabled = true;
            this.cbCompassVisibleMask.Items.AddRange(new object[] {
            "所有视口都不显示 ",
            "仅在第一个视口显示 ",
            "仅在第二个视口显示 ",
            "仅在第三个视口显示 ",
            "仅在第四个视口显示 ",
            "正常的四个视口都显示(默认值) ",
            "仅在画中画视口显示"});
            this.cbCompassVisibleMask.Location = new System.Drawing.Point(280, 6);
            this.cbCompassVisibleMask.Name = "cbCompassVisibleMask";
            this.cbCompassVisibleMask.Size = new System.Drawing.Size(140, 20);
            this.cbCompassVisibleMask.TabIndex = 5;
            this.cbCompassVisibleMask.SelectedIndexChanged += new System.EventHandler(this.cbCompassVisibleMask_SelectedIndexChanged);
            // 
            // CompassVisibleMask
            // 
            this.CompassVisibleMask.AutoSize = true;
            this.CompassVisibleMask.Location = new System.Drawing.Point(161, 10);
            this.CompassVisibleMask.Name = "CompassVisibleMask";
            this.CompassVisibleMask.Size = new System.Drawing.Size(113, 12);
            this.CompassVisibleMask.TabIndex = 4;
            this.CompassVisibleMask.Text = "CompassVisibleMask";
            // 
            // cbCameraInfoVisible
            // 
            this.cbCameraInfoVisible.AutoSize = true;
            this.cbCameraInfoVisible.Location = new System.Drawing.Point(12, 10);
            this.cbCameraInfoVisible.Name = "cbCameraInfoVisible";
            this.cbCameraInfoVisible.Size = new System.Drawing.Size(132, 16);
            this.cbCameraInfoVisible.TabIndex = 3;
            this.cbCameraInfoVisible.Text = "CameraInfo是否可见";
            this.cbCameraInfoVisible.UseVisualStyleBackColor = true;
            this.cbCameraInfoVisible.CheckedChanged += new System.EventHandler(this.cbCameraInfoVisible_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.SplitRatioH);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 616);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1199, 24);
            this.panel3.TabIndex = 41;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.SplitRatioV);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 646);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1199, 24);
            this.panel4.TabIndex = 42;
            // 
            // SplitRatioV
            // 
            this.SplitRatioV.AutoSize = true;
            this.SplitRatioV.Location = new System.Drawing.Point(3, 6);
            this.SplitRatioV.Name = "SplitRatioV";
            this.SplitRatioV.Size = new System.Drawing.Size(41, 12);
            this.SplitRatioV.TabIndex = 7;
            this.SplitRatioV.Text = "label1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 673);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "三维显示窗口";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonT1B1SingleFrustum;
        private System.Windows.Forms.RadioButton radioButtonL1R1SingleFrustum;
        private System.Windows.Forms.RadioButton radioButtonL1R2;
        private System.Windows.Forms.RadioButton radioButtonL2R1;
        private System.Windows.Forms.RadioButton radioButtonT1M1B1;
        private System.Windows.Forms.RadioButton radioButtonL1M1R1;
        private System.Windows.Forms.RadioButton radioButtonT1B1;
        private System.Windows.Forms.RadioButton radioButtonL1R1;
        private System.Windows.Forms.RadioButton radioButtonQuadH;
        private System.Windows.Forms.RadioButton radioButtonQuad;
        private System.Windows.Forms.CheckBox cbCameraBind;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cbCameraInfoVisible;
        private System.Windows.Forms.Label CompassVisibleMask;
        private System.Windows.Forms.ComboBox cbCompassVisibleMask;
        private System.Windows.Forms.Label SplitRatioH;
        private System.Windows.Forms.CheckBox cbShowBorder;
        private System.Windows.Forms.RadioButton radioButtonPIP;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label SplitRatioV;
    }
}
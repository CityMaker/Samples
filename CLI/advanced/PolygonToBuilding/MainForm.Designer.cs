namespace PolygonToBuilding
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.numHeightOffset = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numExteriorOffset = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numInteriorOffset = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxRoofTexture = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxFacadeTexture = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numFloorHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numFloorNumber = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.numBuildingYScale = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numBuildingXScale = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numBuildingHeight = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeightOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExteriorOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInteriorOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFloorHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFloorNumber)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBuildingYScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBuildingXScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBuildingHeight)).BeginInit();
            this.SuspendLayout();
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
            this.axRenderControl1.Size = new System.Drawing.Size(679, 388);
            this.axRenderControl1.TabIndex = 0;
            this.axRenderControl1.UseEarthOrbitManipulator = Gvitech.CityMaker.RenderControl.gviManipulatorMode.gviCityMakerManipulator;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 526);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 397);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(679, 126);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.numHeightOffset);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.numExteriorOffset);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.numInteriorOffset);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.comboBoxRoofTexture);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.comboBoxFacadeTexture);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.numFloorHeight);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.numFloorNumber);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(671, 100);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "PolygonToBuilding";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // numHeightOffset
            // 
            this.numHeightOffset.DecimalPlaces = 2;
            this.numHeightOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numHeightOffset.Location = new System.Drawing.Point(264, 74);
            this.numHeightOffset.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHeightOffset.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numHeightOffset.Name = "numHeightOffset";
            this.numHeightOffset.Size = new System.Drawing.Size(72, 21);
            this.numHeightOffset.TabIndex = 15;
            this.numHeightOffset.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(173, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "HeightOffset";
            // 
            // numExteriorOffset
            // 
            this.numExteriorOffset.DecimalPlaces = 2;
            this.numExteriorOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numExteriorOffset.Location = new System.Drawing.Point(262, 12);
            this.numExteriorOffset.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numExteriorOffset.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numExteriorOffset.Name = "numExteriorOffset";
            this.numExteriorOffset.Size = new System.Drawing.Size(72, 21);
            this.numExteriorOffset.TabIndex = 13;
            this.numExteriorOffset.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "ExteriorOffset";
            // 
            // numInteriorOffset
            // 
            this.numInteriorOffset.DecimalPlaces = 2;
            this.numInteriorOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numInteriorOffset.Location = new System.Drawing.Point(264, 45);
            this.numInteriorOffset.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numInteriorOffset.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numInteriorOffset.Name = "numInteriorOffset";
            this.numInteriorOffset.Size = new System.Drawing.Size(72, 21);
            this.numInteriorOffset.TabIndex = 11;
            this.numInteriorOffset.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(173, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "InteriorOffset";
            // 
            // comboBoxRoofTexture
            // 
            this.comboBoxRoofTexture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRoofTexture.FormattingEnabled = true;
            this.comboBoxRoofTexture.Items.AddRange(new object[] {
            "MGC68T15.dds",
            "TXDPI035.dds",
            "TXDPI036.dds",
            "TXDPO001A.dds",
            "TXDPO002B.dds",
            "TXDPO004.dds",
            "TXDPO005B.dds",
            "TXDPO006B.dds",
            "TXDPO008B.dds"});
            this.comboBoxRoofTexture.Location = new System.Drawing.Point(471, 45);
            this.comboBoxRoofTexture.Name = "comboBoxRoofTexture";
            this.comboBoxRoofTexture.Size = new System.Drawing.Size(126, 20);
            this.comboBoxRoofTexture.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(358, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "RoofTextureName";
            // 
            // comboBoxFacadeTexture
            // 
            this.comboBoxFacadeTexture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFacadeTexture.FormattingEnabled = true;
            this.comboBoxFacadeTexture.Items.AddRange(new object[] {
            "TXLGJ007A.dds",
            "TXLGJ007C.dds",
            "TXLGJ009A.dds",
            "TXLZZ037A.dds",
            "TXLZZ049A.dds",
            "TXLZZ049B.dds",
            "TXLZZ049E.dds",
            "TXLZZ052B.dds",
            "TXLZZ053B.dds",
            "TXLZZ053C.dds",
            "TXLZZ054B.dds",
            "TXLZZ056A.dds"});
            this.comboBoxFacadeTexture.Location = new System.Drawing.Point(471, 11);
            this.comboBoxFacadeTexture.Name = "comboBoxFacadeTexture";
            this.comboBoxFacadeTexture.Size = new System.Drawing.Size(126, 20);
            this.comboBoxFacadeTexture.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(358, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "FacadeTextureName";
            // 
            // numFloorHeight
            // 
            this.numFloorHeight.DecimalPlaces = 2;
            this.numFloorHeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numFloorHeight.Location = new System.Drawing.Point(89, 48);
            this.numFloorHeight.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numFloorHeight.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numFloorHeight.Name = "numFloorHeight";
            this.numFloorHeight.Size = new System.Drawing.Size(72, 21);
            this.numFloorHeight.TabIndex = 3;
            this.numFloorHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "FloorHeight";
            // 
            // numFloorNumber
            // 
            this.numFloorNumber.Location = new System.Drawing.Point(89, 12);
            this.numFloorNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFloorNumber.Name = "numFloorNumber";
            this.numFloorNumber.Size = new System.Drawing.Size(48, 21);
            this.numFloorNumber.TabIndex = 1;
            this.numFloorNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "FloorNumber";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.numBuildingYScale);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.numBuildingXScale);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.numBuildingHeight);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(671, 100);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "平屋顶";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // numBuildingYScale
            // 
            this.numBuildingYScale.Location = new System.Drawing.Point(267, 41);
            this.numBuildingYScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBuildingYScale.Name = "numBuildingYScale";
            this.numBuildingYScale.Size = new System.Drawing.Size(48, 21);
            this.numBuildingYScale.TabIndex = 7;
            this.numBuildingYScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(176, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "BuildingYScale";
            // 
            // numBuildingXScale
            // 
            this.numBuildingXScale.Location = new System.Drawing.Point(267, 12);
            this.numBuildingXScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBuildingXScale.Name = "numBuildingXScale";
            this.numBuildingXScale.Size = new System.Drawing.Size(48, 21);
            this.numBuildingXScale.TabIndex = 5;
            this.numBuildingXScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(176, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "BuildingXScale";
            // 
            // numBuildingHeight
            // 
            this.numBuildingHeight.Location = new System.Drawing.Point(104, 12);
            this.numBuildingHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBuildingHeight.Name = "numBuildingHeight";
            this.numBuildingHeight.Size = new System.Drawing.Size(48, 21);
            this.numBuildingHeight.TabIndex = 3;
            this.numBuildingHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "BuildingHeight";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 526);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GeometryConvert2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeightOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExteriorOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInteriorOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFloorHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFloorNumber)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBuildingYScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBuildingXScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBuildingHeight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.NumericUpDown numFloorNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numFloorHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxRoofTexture;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxFacadeTexture;
        private System.Windows.Forms.NumericUpDown numInteriorOffset;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numExteriorOffset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numHeightOffset;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.NumericUpDown numBuildingHeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numBuildingXScale;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numBuildingYScale;
        private System.Windows.Forms.Label label10;

    }
}


using Gvitech.CityMaker.Controls;
namespace RenderPipeLine
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage0Polyline = new System.Windows.Forms.TabPage();
            this.dataGridViewPolyline = new System.Windows.Forms.DataGridView();
            this.tabPage1Param = new System.Windows.Forms.TabPage();
            this.btnStop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownDuration = new System.Windows.Forms.NumericUpDown();
            this.checkBoxHideLayer = new System.Windows.Forms.CheckBox();
            this.checkBoxNeedLoop = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxPlayMode = new System.Windows.Forms.ComboBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage0Polyline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPolyline)).BeginInit();
            this.tabPage1Param.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 204F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(881, 585);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(875, 375);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage0Polyline);
            this.tabControl1.Controls.Add(this.tabPage1Param);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 384);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(875, 198);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage0Polyline
            // 
            this.tabPage0Polyline.Controls.Add(this.dataGridViewPolyline);
            this.tabPage0Polyline.Location = new System.Drawing.Point(4, 22);
            this.tabPage0Polyline.Name = "tabPage0Polyline";
            this.tabPage0Polyline.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage0Polyline.Size = new System.Drawing.Size(867, 172);
            this.tabPage0Polyline.TabIndex = 0;
            this.tabPage0Polyline.Text = "Polyline";
            this.tabPage0Polyline.UseVisualStyleBackColor = true;
            // 
            // dataGridViewPolyline
            // 
            this.dataGridViewPolyline.AllowUserToAddRows = false;
            this.dataGridViewPolyline.AllowUserToDeleteRows = false;
            this.dataGridViewPolyline.AllowUserToResizeRows = false;
            this.dataGridViewPolyline.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPolyline.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPolyline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPolyline.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewPolyline.MultiSelect = false;
            this.dataGridViewPolyline.Name = "dataGridViewPolyline";
            this.dataGridViewPolyline.ReadOnly = true;
            this.dataGridViewPolyline.RowTemplate.Height = 23;
            this.dataGridViewPolyline.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPolyline.Size = new System.Drawing.Size(861, 166);
            this.dataGridViewPolyline.TabIndex = 0;
            this.dataGridViewPolyline.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseDoubleClick);
            // 
            // tabPage1Param
            // 
            this.tabPage1Param.Controls.Add(this.btnStop);
            this.tabPage1Param.Controls.Add(this.label2);
            this.tabPage1Param.Controls.Add(this.numericUpDownDuration);
            this.tabPage1Param.Controls.Add(this.checkBoxHideLayer);
            this.tabPage1Param.Controls.Add(this.checkBoxNeedLoop);
            this.tabPage1Param.Controls.Add(this.label1);
            this.tabPage1Param.Controls.Add(this.comboBoxPlayMode);
            this.tabPage1Param.Location = new System.Drawing.Point(4, 22);
            this.tabPage1Param.Name = "tabPage1Param";
            this.tabPage1Param.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1Param.Size = new System.Drawing.Size(867, 172);
            this.tabPage1Param.TabIndex = 1;
            this.tabPage1Param.Text = "Param";
            this.tabPage1Param.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(221, 60);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(55, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Duration";
            // 
            // numericUpDownDuration
            // 
            this.numericUpDownDuration.Location = new System.Drawing.Point(75, 60);
            this.numericUpDownDuration.Name = "numericUpDownDuration";
            this.numericUpDownDuration.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownDuration.TabIndex = 4;
            this.numericUpDownDuration.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // checkBoxHideLayer
            // 
            this.checkBoxHideLayer.AutoSize = true;
            this.checkBoxHideLayer.Location = new System.Drawing.Point(320, 19);
            this.checkBoxHideLayer.Name = "checkBoxHideLayer";
            this.checkBoxHideLayer.Size = new System.Drawing.Size(120, 16);
            this.checkBoxHideLayer.TabIndex = 3;
            this.checkBoxHideLayer.Text = "HideFeatureLayer";
            this.checkBoxHideLayer.UseVisualStyleBackColor = true;
            this.checkBoxHideLayer.CheckedChanged += new System.EventHandler(this.checkBoxHideLayer_CheckedChanged);
            // 
            // checkBoxNeedLoop
            // 
            this.checkBoxNeedLoop.AutoSize = true;
            this.checkBoxNeedLoop.Location = new System.Drawing.Point(224, 20);
            this.checkBoxNeedLoop.Name = "checkBoxNeedLoop";
            this.checkBoxNeedLoop.Size = new System.Drawing.Size(72, 16);
            this.checkBoxNeedLoop.TabIndex = 2;
            this.checkBoxNeedLoop.Text = "NeedLoop";
            this.checkBoxNeedLoop.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "PlayMode";
            // 
            // comboBoxPlayMode
            // 
            this.comboBoxPlayMode.FormattingEnabled = true;
            this.comboBoxPlayMode.Items.AddRange(new object[] {
            "ShowTrack",
            "NoTrack",
            "DrawTrack"});
            this.comboBoxPlayMode.Location = new System.Drawing.Point(75, 17);
            this.comboBoxPlayMode.Name = "comboBoxPlayMode";
            this.comboBoxPlayMode.Size = new System.Drawing.Size(121, 20);
            this.comboBoxPlayMode.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 585);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RenderPipeLine";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage0Polyline.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPolyline)).EndInit();
            this.tabPage1Param.ResumeLayout(false);
            this.tabPage1Param.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDuration)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion


        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage0Polyline;
        private System.Windows.Forms.DataGridView dataGridViewPolyline;
        private System.Windows.Forms.TabPage tabPage1Param;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxPlayMode;
        private System.Windows.Forms.CheckBox checkBoxNeedLoop;
        private System.Windows.Forms.CheckBox checkBoxHideLayer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownDuration;
        private System.Windows.Forms.Button btnStop;

    }
}


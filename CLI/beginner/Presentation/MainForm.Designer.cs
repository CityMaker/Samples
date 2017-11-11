namespace Presentation
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
            this.btnShowEditor = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnStopRecord = new System.Windows.Forms.Button();
            this.btnStartRecord = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbPlayMode = new System.Windows.Forms.ComboBox();
            this.btnResetPresentation = new System.Windows.Forms.Button();
            this.btnNextStep = new System.Windows.Forms.Button();
            this.btnPreviousStep = new System.Windows.Forms.Button();
            this.btnPlayStep = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlayFromStartIndex = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.cbLoopRoute = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCreateCaptionStep = new System.Windows.Forms.Button();
            this.btnReplaceLocationStep = new System.Windows.Forms.Button();
            this.btnCreateLocationStep = new System.Windows.Forms.Button();
            this.btnDeleteStep = new System.Windows.Forms.Button();
            this.btnCreatePresentation = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnCreateFlightSpeedFactorStep = new System.Windows.Forms.Button();
            this.cbFlightSpeedFactor = new System.Windows.Forms.ComboBox();
            this.btnCreateFollowDynamicObjectStep = new System.Windows.Forms.Button();
            this.btnCreateRestartDynamicObjectStep = new System.Windows.Forms.Button();
            this.btnCreateShowObjectStep = new System.Windows.Forms.Button();
            this.btnCreateHideObjectStep = new System.Windows.Forms.Button();
            
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(3, 3);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(576, 563);
            this.axRenderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 334F));
            this.tableLayoutPanel1.Controls.Add(this.axRenderControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(916, 719);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnShowEditor);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnCreatePresentation);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(585, 3);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(328, 713);
            this.panel1.TabIndex = 1;
            // 
            // btnShowEditor
            // 
            this.btnShowEditor.Location = new System.Drawing.Point(6, 442);
            this.btnShowEditor.Name = "btnShowEditor";
            this.btnShowEditor.Size = new System.Drawing.Size(113, 49);
            this.btnShowEditor.TabIndex = 6;
            this.btnShowEditor.Text = "ShowEditor";
            this.btnShowEditor.UseVisualStyleBackColor = true;
            this.btnShowEditor.Click += new System.EventHandler(this.btnShowEditor_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnStopRecord);
            this.groupBox3.Controls.Add(this.btnStartRecord);
            this.groupBox3.Location = new System.Drawing.Point(6, 363);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(313, 61);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "录制";
            // 
            // btnStopRecord
            // 
            this.btnStopRecord.Location = new System.Drawing.Point(104, 21);
            this.btnStopRecord.Name = "btnStopRecord";
            this.btnStopRecord.Size = new System.Drawing.Size(80, 30);
            this.btnStopRecord.TabIndex = 5;
            this.btnStopRecord.Text = "StopRecord";
            this.btnStopRecord.UseVisualStyleBackColor = true;
            this.btnStopRecord.Click += new System.EventHandler(this.btnStopRecord_Click);
            // 
            // btnStartRecord
            // 
            this.btnStartRecord.Location = new System.Drawing.Point(9, 21);
            this.btnStartRecord.Name = "btnStartRecord";
            this.btnStartRecord.Size = new System.Drawing.Size(80, 30);
            this.btnStartRecord.TabIndex = 4;
            this.btnStartRecord.Text = "StartRecord";
            this.btnStartRecord.UseVisualStyleBackColor = true;
            this.btnStartRecord.Click += new System.EventHandler(this.btnStartRecord_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbPlayMode);
            this.groupBox2.Controls.Add(this.btnResetPresentation);
            this.groupBox2.Controls.Add(this.btnNextStep);
            this.groupBox2.Controls.Add(this.btnPreviousStep);
            this.groupBox2.Controls.Add(this.btnPlayStep);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.btnPlayFromStartIndex);
            this.groupBox2.Controls.Add(this.btnContinue);
            this.groupBox2.Controls.Add(this.btnPause);
            this.groupBox2.Controls.Add(this.cbLoopRoute);
            this.groupBox2.Location = new System.Drawing.Point(6, 536);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(313, 168);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "播放控制";
            // 
            // cbPlayMode
            // 
            this.cbPlayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlayMode.FormattingEnabled = true;
            this.cbPlayMode.Items.AddRange(new object[] {
            "PlayAutomatic",
            "PlayManual"});
            this.cbPlayMode.Location = new System.Drawing.Point(104, 17);
            this.cbPlayMode.Name = "cbPlayMode";
            this.cbPlayMode.Size = new System.Drawing.Size(97, 20);
            this.cbPlayMode.TabIndex = 15;
            this.cbPlayMode.SelectedIndexChanged += new System.EventHandler(this.cbPlayMode_SelectedIndexChanged);
            // 
            // btnResetPresentation
            // 
            this.btnResetPresentation.Location = new System.Drawing.Point(136, 41);
            this.btnResetPresentation.Name = "btnResetPresentation";
            this.btnResetPresentation.Size = new System.Drawing.Size(121, 30);
            this.btnResetPresentation.TabIndex = 14;
            this.btnResetPresentation.Text = "ResetPresentation";
            this.btnResetPresentation.UseVisualStyleBackColor = true;
            this.btnResetPresentation.Click += new System.EventHandler(this.btnResetPresentation_Click);
            // 
            // btnNextStep
            // 
            this.btnNextStep.Location = new System.Drawing.Point(176, 113);
            this.btnNextStep.Name = "btnNextStep";
            this.btnNextStep.Size = new System.Drawing.Size(63, 30);
            this.btnNextStep.TabIndex = 13;
            this.btnNextStep.Text = "NextStep";
            this.btnNextStep.UseVisualStyleBackColor = true;
            this.btnNextStep.Click += new System.EventHandler(this.btnNextStep_Click);
            // 
            // btnPreviousStep
            // 
            this.btnPreviousStep.Location = new System.Drawing.Point(80, 113);
            this.btnPreviousStep.Name = "btnPreviousStep";
            this.btnPreviousStep.Size = new System.Drawing.Size(90, 30);
            this.btnPreviousStep.TabIndex = 12;
            this.btnPreviousStep.Text = "PreviousStep";
            this.btnPreviousStep.UseVisualStyleBackColor = true;
            this.btnPreviousStep.Click += new System.EventHandler(this.btnPreviousStep_Click);
            // 
            // btnPlayStep
            // 
            this.btnPlayStep.Location = new System.Drawing.Point(9, 113);
            this.btnPlayStep.Name = "btnPlayStep";
            this.btnPlayStep.Size = new System.Drawing.Size(65, 30);
            this.btnPlayStep.TabIndex = 11;
            this.btnPlayStep.Text = "PlayStep";
            this.btnPlayStep.UseVisualStyleBackColor = true;
            this.btnPlayStep.Click += new System.EventHandler(this.btnPlayStep_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(136, 77);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(48, 30);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlayFromStartIndex
            // 
            this.btnPlayFromStartIndex.Location = new System.Drawing.Point(9, 41);
            this.btnPlayFromStartIndex.Name = "btnPlayFromStartIndex";
            this.btnPlayFromStartIndex.Size = new System.Drawing.Size(121, 30);
            this.btnPlayFromStartIndex.TabIndex = 6;
            this.btnPlayFromStartIndex.Text = "PlayFromStartIndex";
            this.btnPlayFromStartIndex.UseVisualStyleBackColor = true;
            this.btnPlayFromStartIndex.Click += new System.EventHandler(this.PlayFromStartIndex_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(63, 77);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(67, 30);
            this.btnContinue.TabIndex = 10;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(9, 77);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(48, 30);
            this.btnPause.TabIndex = 7;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // cbLoopRoute
            // 
            this.cbLoopRoute.AutoSize = true;
            this.cbLoopRoute.Location = new System.Drawing.Point(9, 19);
            this.cbLoopRoute.Name = "cbLoopRoute";
            this.cbLoopRoute.Size = new System.Drawing.Size(78, 16);
            this.cbLoopRoute.TabIndex = 9;
            this.cbLoopRoute.Text = "LoopRoute";
            this.cbLoopRoute.UseVisualStyleBackColor = true;
            this.cbLoopRoute.CheckedChanged += new System.EventHandler(this.cbLoopRoute_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCreateHideObjectStep);
            this.groupBox1.Controls.Add(this.btnCreateShowObjectStep);
            this.groupBox1.Controls.Add(this.btnCreateRestartDynamicObjectStep);
            this.groupBox1.Controls.Add(this.btnCreateFollowDynamicObjectStep);
            this.groupBox1.Controls.Add(this.cbFlightSpeedFactor);
            this.groupBox1.Controls.Add(this.btnCreateFlightSpeedFactorStep);
            this.groupBox1.Controls.Add(this.btnCreateCaptionStep);
            this.groupBox1.Controls.Add(this.btnReplaceLocationStep);
            this.groupBox1.Controls.Add(this.btnCreateLocationStep);
            this.groupBox1.Controls.Add(this.btnDeleteStep);
            this.groupBox1.Location = new System.Drawing.Point(6, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 312);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "创建";
            // 
            // btnCreateCaptionStep
            // 
            this.btnCreateCaptionStep.Location = new System.Drawing.Point(9, 54);
            this.btnCreateCaptionStep.Name = "btnCreateCaptionStep";
            this.btnCreateCaptionStep.Size = new System.Drawing.Size(121, 30);
            this.btnCreateCaptionStep.TabIndex = 4;
            this.btnCreateCaptionStep.Text = "CreateCaptionStep";
            this.btnCreateCaptionStep.UseVisualStyleBackColor = true;
            this.btnCreateCaptionStep.Click += new System.EventHandler(this.btnCreateCaptionStep_Click);
            // 
            // btnReplaceLocationStep
            // 
            this.btnReplaceLocationStep.Location = new System.Drawing.Point(136, 18);
            this.btnReplaceLocationStep.Name = "btnReplaceLocationStep";
            this.btnReplaceLocationStep.Size = new System.Drawing.Size(132, 30);
            this.btnReplaceLocationStep.TabIndex = 3;
            this.btnReplaceLocationStep.Text = "ReplaceLocationStep";
            this.btnReplaceLocationStep.UseVisualStyleBackColor = true;
            this.btnReplaceLocationStep.Click += new System.EventHandler(this.btnReplaceLocationStep_Click);
            // 
            // btnCreateLocationStep
            // 
            this.btnCreateLocationStep.Location = new System.Drawing.Point(9, 18);
            this.btnCreateLocationStep.Name = "btnCreateLocationStep";
            this.btnCreateLocationStep.Size = new System.Drawing.Size(121, 30);
            this.btnCreateLocationStep.TabIndex = 1;
            this.btnCreateLocationStep.Text = "CreateLocationStep";
            this.btnCreateLocationStep.UseVisualStyleBackColor = true;
            this.btnCreateLocationStep.Click += new System.EventHandler(this.btnCreateLocationStep_Click);
            // 
            // btnDeleteStep
            // 
            this.btnDeleteStep.Location = new System.Drawing.Point(9, 276);
            this.btnDeleteStep.Name = "btnDeleteStep";
            this.btnDeleteStep.Size = new System.Drawing.Size(80, 30);
            this.btnDeleteStep.TabIndex = 2;
            this.btnDeleteStep.Text = "DeleteStep";
            this.btnDeleteStep.UseVisualStyleBackColor = true;
            this.btnDeleteStep.Click += new System.EventHandler(this.btnDeleteStep_Click);
            // 
            // btnCreatePresentation
            // 
            this.btnCreatePresentation.Location = new System.Drawing.Point(6, 9);
            this.btnCreatePresentation.Name = "btnCreatePresentation";
            this.btnCreatePresentation.Size = new System.Drawing.Size(122, 30);
            this.btnCreatePresentation.TabIndex = 0;
            this.btnCreatePresentation.Text = "CreatePresentation";
            this.btnCreatePresentation.UseVisualStyleBackColor = true;
            this.btnCreatePresentation.Click += new System.EventHandler(this.btnCreatePresentation_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 572);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(576, 144);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseDoubleClick);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // btnCreateFlightSpeedFactorStep
            // 
            this.btnCreateFlightSpeedFactorStep.Location = new System.Drawing.Point(9, 90);
            this.btnCreateFlightSpeedFactorStep.Name = "btnCreateFlightSpeedFactorStep";
            this.btnCreateFlightSpeedFactorStep.Size = new System.Drawing.Size(175, 30);
            this.btnCreateFlightSpeedFactorStep.TabIndex = 5;
            this.btnCreateFlightSpeedFactorStep.Text = "CreateFlightSpeedFactorStep";
            this.btnCreateFlightSpeedFactorStep.UseVisualStyleBackColor = true;
            this.btnCreateFlightSpeedFactorStep.Click += new System.EventHandler(this.btnCreateFlightSpeedFactorStep_Click);
            // 
            // cbFlightSpeedFactor
            // 
            this.cbFlightSpeedFactor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFlightSpeedFactor.FormattingEnabled = true;
            this.cbFlightSpeedFactor.Items.AddRange(new object[] {
            "VerySlow",
            "Slow",
            "Normal",
            "Fast ",
            "VeryFast"});
            this.cbFlightSpeedFactor.Location = new System.Drawing.Point(190, 96);
            this.cbFlightSpeedFactor.Name = "cbFlightSpeedFactor";
            this.cbFlightSpeedFactor.Size = new System.Drawing.Size(121, 20);
            this.cbFlightSpeedFactor.TabIndex = 6;
            // 
            // btnCreateFollowDynamicObjectStep
            // 
            this.btnCreateFollowDynamicObjectStep.Location = new System.Drawing.Point(9, 126);
            this.btnCreateFollowDynamicObjectStep.Name = "btnCreateFollowDynamicObjectStep";
            this.btnCreateFollowDynamicObjectStep.Size = new System.Drawing.Size(192, 30);
            this.btnCreateFollowDynamicObjectStep.TabIndex = 7;
            this.btnCreateFollowDynamicObjectStep.Text = "CreateFollowDynamicObjectStep";
            this.btnCreateFollowDynamicObjectStep.UseVisualStyleBackColor = true;
            this.btnCreateFollowDynamicObjectStep.Click += new System.EventHandler(this.btnCreateFollowDynamicObjectStep_Click);
            // 
            // btnCreateRestartDynamicObjectStep
            // 
            this.btnCreateRestartDynamicObjectStep.Location = new System.Drawing.Point(9, 162);
            this.btnCreateRestartDynamicObjectStep.Name = "btnCreateRestartDynamicObjectStep";
            this.btnCreateRestartDynamicObjectStep.Size = new System.Drawing.Size(207, 30);
            this.btnCreateRestartDynamicObjectStep.TabIndex = 8;
            this.btnCreateRestartDynamicObjectStep.Text = "CreateRestartDynamicObjectStep";
            this.btnCreateRestartDynamicObjectStep.UseVisualStyleBackColor = true;
            this.btnCreateRestartDynamicObjectStep.Click += new System.EventHandler(this.btnCreateRestartDynamicObjectStep_Click);
            // 
            // btnCreateShowObjectStep
            // 
            this.btnCreateShowObjectStep.Location = new System.Drawing.Point(149, 209);
            this.btnCreateShowObjectStep.Name = "btnCreateShowObjectStep";
            this.btnCreateShowObjectStep.Size = new System.Drawing.Size(134, 30);
            this.btnCreateShowObjectStep.TabIndex = 9;
            this.btnCreateShowObjectStep.Text = "CreateShowObjectStep";
            this.btnCreateShowObjectStep.UseVisualStyleBackColor = true;
            this.btnCreateShowObjectStep.Click += new System.EventHandler(this.btnCreateShowObjectStep_Click);
            // 
            // btnCreateHideObjectStep
            // 
            this.btnCreateHideObjectStep.Location = new System.Drawing.Point(9, 209);
            this.btnCreateHideObjectStep.Name = "btnCreateHideObjectStep";
            this.btnCreateHideObjectStep.Size = new System.Drawing.Size(134, 30);
            this.btnCreateHideObjectStep.TabIndex = 10;
            this.btnCreateHideObjectStep.Text = "CreateHideObjectStep";
            this.btnCreateHideObjectStep.UseVisualStyleBackColor = true;
            this.btnCreateHideObjectStep.Click += new System.EventHandler(this.btnCreateHideObjectStep_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 719);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Presentation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCreatePresentation;
        private System.Windows.Forms.Button btnReplaceLocationStep;
        private System.Windows.Forms.Button btnDeleteStep;
        private System.Windows.Forms.Button btnCreateLocationStep;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlayFromStartIndex;
        private System.Windows.Forms.CheckBox cbLoopRoute;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPlayStep;
        private System.Windows.Forms.Button btnPreviousStep;
        private System.Windows.Forms.Button btnNextStep;
        private System.Windows.Forms.Button btnResetPresentation;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnStartRecord;
        private System.Windows.Forms.Button btnStopRecord;
        private System.Windows.Forms.Button btnShowEditor;
        private System.Windows.Forms.ComboBox cbPlayMode;
        private System.Windows.Forms.Button btnCreateCaptionStep;
        private System.Windows.Forms.Button btnCreateFlightSpeedFactorStep;
        private System.Windows.Forms.ComboBox cbFlightSpeedFactor;
        private System.Windows.Forms.Button btnCreateFollowDynamicObjectStep;
        private System.Windows.Forms.Button btnCreateRestartDynamicObjectStep;
        private System.Windows.Forms.Button btnCreateShowObjectStep;
        private System.Windows.Forms.Button btnCreateHideObjectStep;

    }
}


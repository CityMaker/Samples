namespace DataImport
{
    partial class DataImportProgressDlg
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
            this.lbl_Info = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lbl_ImportCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_Info
            // 
            this.lbl_Info.AutoSize = true;
            this.lbl_Info.Location = new System.Drawing.Point(22, 9);
            this.lbl_Info.Name = "lbl_Info";
            this.lbl_Info.Size = new System.Drawing.Size(89, 12);
            this.lbl_Info.TabIndex = 0;
            this.lbl_Info.Text = "数据导入初始化";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(22, 35);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(267, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 1;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(120, 66);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // lbl_ImportCount
            // 
            this.lbl_ImportCount.AutoSize = true;
            this.lbl_ImportCount.Location = new System.Drawing.Point(126, 41);
            this.lbl_ImportCount.Name = "lbl_ImportCount";
            this.lbl_ImportCount.Size = new System.Drawing.Size(53, 12);
            this.lbl_ImportCount.TabIndex = 3;
            this.lbl_ImportCount.Text = "正在导入";
            this.lbl_ImportCount.Visible = false;
            // 
            // DataImportProgressDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 101);
            
            this.Controls.Add(this.lbl_ImportCount);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lbl_Info);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataImportProgressDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "进度信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_Info;
        public System.Windows.Forms.ProgressBar progressBar;
        public System.Windows.Forms.Button btn_Cancel;
        public System.Windows.Forms.Label lbl_ImportCount;
    }
}
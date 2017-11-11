namespace DynamicModelPoint
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox_x = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox_y = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox_n = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox_i = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton_CreateRenderModelPoint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_DynamicModelPoint = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTextBox_x,
            this.toolStripLabel2,
            this.toolStripTextBox_y,
            this.toolStripLabel3,
            this.toolStripTextBox_n,
            this.toolStripLabel4,
            this.toolStripTextBox_i,
            this.toolStripButton_CreateRenderModelPoint,
            this.toolStripButton_DynamicModelPoint});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(864, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(19, 22);
            this.toolStripLabel1.Text = "X:";
            // 
            // toolStripTextBox_x
            // 
            this.toolStripTextBox_x.Name = "toolStripTextBox_x";
            this.toolStripTextBox_x.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBox_x.Text = "10";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(18, 22);
            this.toolStripLabel2.Text = "Y:";
            // 
            // toolStripTextBox_y
            // 
            this.toolStripTextBox_y.Name = "toolStripTextBox_y";
            this.toolStripTextBox_y.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBox_y.Text = "10";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel3.Text = "间距";
            // 
            // toolStripTextBox_n
            // 
            this.toolStripTextBox_n.Name = "toolStripTextBox_n";
            this.toolStripTextBox_n.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBox_n.Text = "10";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel4.Text = "间隔";
            // 
            // toolStripTextBox_i
            // 
            this.toolStripTextBox_i.Name = "toolStripTextBox_i";
            this.toolStripTextBox_i.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBox_i.Text = "5";
            // 
            // toolStripButton_CreateRenderModelPoint
            // 
            this.toolStripButton_CreateRenderModelPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_CreateRenderModelPoint.Name = "toolStripButton_CreateRenderModelPoint";
            this.toolStripButton_CreateRenderModelPoint.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton_CreateRenderModelPoint.Text = "创建模型";
            this.toolStripButton_CreateRenderModelPoint.Click += new System.EventHandler(this.toolStripButton_CreateRenderModelPoint_Click);
            // 
            // toolStripButton_DynamicModelPoint
            // 
            this.toolStripButton_DynamicModelPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_DynamicModelPoint.Name = "toolStripButton_DynamicModelPoint";
            this.toolStripButton_DynamicModelPoint.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton_DynamicModelPoint.Text = "让模型动起来";
            this.toolStripButton_DynamicModelPoint.Click += new System.EventHandler(this.toolStripButton_DynamicModelPoint_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 575);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_x;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_y;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_n;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_i;
        private System.Windows.Forms.ToolStripButton toolStripButton_CreateRenderModelPoint;
        private System.Windows.Forms.ToolStripButton toolStripButton_DynamicModelPoint;
    }
}
namespace UndoRedo
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_Undo = new System.Windows.Forms.ToolStripButton();
            this.tsb_Redo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Pan = new System.Windows.Forms.ToolStripButton();
            this.tsb_Select = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Delete = new System.Windows.Forms.ToolStripButton();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.axRenderControl1 = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tsb_Update = new System.Windows.Forms.ToolStripButton();
            this.tsb_Insert = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Undo,
            this.tsb_Redo,
            this.toolStripSeparator1,
            this.tsb_Pan,
            this.tsb_Select,
            this.toolStripSeparator2,
            this.tsb_Delete,
            this.tsb_Update,
            this.tsb_Insert});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(685, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_Undo
            // 
            this.tsb_Undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_Undo.Enabled = false;
            this.tsb_Undo.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Undo.Image")));
            this.tsb_Undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Undo.Name = "tsb_Undo";
            this.tsb_Undo.Size = new System.Drawing.Size(44, 22);
            this.tsb_Undo.Text = "Undo";
            this.tsb_Undo.Click += new System.EventHandler(this.tsb_Undo_Click);
            // 
            // tsb_Redo
            // 
            this.tsb_Redo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_Redo.Enabled = false;
            this.tsb_Redo.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Redo.Image")));
            this.tsb_Redo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Redo.Name = "tsb_Redo";
            this.tsb_Redo.Size = new System.Drawing.Size(43, 22);
            this.tsb_Redo.Text = "Redo";
            this.tsb_Redo.Click += new System.EventHandler(this.tsb_Redo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_Pan
            // 
            this.tsb_Pan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_Pan.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Pan.Image")));
            this.tsb_Pan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Pan.Name = "tsb_Pan";
            this.tsb_Pan.Size = new System.Drawing.Size(33, 22);
            this.tsb_Pan.Text = "Pan";
            this.tsb_Pan.Click += new System.EventHandler(this.tsb_Pan_Click);
            // 
            // tsb_Select
            // 
            this.tsb_Select.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_Select.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsb_Select.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Select.Image")));
            this.tsb_Select.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Select.Name = "tsb_Select";
            this.tsb_Select.Size = new System.Drawing.Size(46, 22);
            this.tsb_Select.Text = "Select";
            this.tsb_Select.Click += new System.EventHandler(this.tsb_Select_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_Delete
            // 
            this.tsb_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_Delete.Enabled = false;
            this.tsb_Delete.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Delete.Image")));
            this.tsb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Delete.Name = "tsb_Delete";
            this.tsb_Delete.Size = new System.Drawing.Size(49, 22);
            this.tsb_Delete.Text = "Delete";
            this.tsb_Delete.Click += new System.EventHandler(this.tsb_Delete_Click);
            // 
            // axRenderControl1
            // 
            this.axRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRenderControl1.Enabled = true;
            this.axRenderControl1.Location = new System.Drawing.Point(0, 0);
            this.axRenderControl1.Name = "axRenderControl1";
            this.axRenderControl1.Size = new System.Drawing.Size(685, 490);
            this.axRenderControl1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.axRenderControl1);
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(685, 490);
            this.panel1.TabIndex = 2;
            // 
            // tsb_Update
            // 
            this.tsb_Update.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_Update.Enabled = false;
            this.tsb_Update.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Update.Image")));
            this.tsb_Update.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Update.Name = "tsb_Update";
            this.tsb_Update.Size = new System.Drawing.Size(55, 22);
            this.tsb_Update.Text = "Update";
            this.tsb_Update.Click += new System.EventHandler(this.tsb_Update_Click);
            // 
            // tsb_Insert
            // 
            this.tsb_Insert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_Insert.Enabled = false;
            this.tsb_Insert.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Insert.Image")));
            this.tsb_Insert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Insert.Name = "tsb_Insert";
            this.tsb_Insert.Size = new System.Drawing.Size(45, 22);
            this.tsb_Insert.Text = "Insert";
            this.tsb_Insert.Click += new System.EventHandler(this.tsb_Insert_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 518);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UndoRedo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private Gvitech.CityMaker.Controls.AxRenderControl axRenderControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton tsb_Undo;
        private System.Windows.Forms.ToolStripButton tsb_Redo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsb_Pan;
        private System.Windows.Forms.ToolStripButton tsb_Select;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsb_Delete;
        private System.Windows.Forms.ToolStripButton tsb_Update;
        private System.Windows.Forms.ToolStripButton tsb_Insert;

    }
}


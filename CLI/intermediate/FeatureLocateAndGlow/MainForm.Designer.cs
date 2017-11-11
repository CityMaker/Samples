using Gvitech.CityMaker.Controls;
namespace FeatureLocateAndGlow
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
            this.tabPage0Road = new System.Windows.Forms.TabPage();
            this.dataGridView0Road = new System.Windows.Forms.DataGridView();
            this.tabPage1Building = new System.Windows.Forms.TabPage();
            this.dataGridView1Building = new System.Windows.Forms.DataGridView();
            this.tabPage2Trees = new System.Windows.Forms.TabPage();
            this.dataGridView2Trees = new System.Windows.Forms.DataGridView();
            this.tabPage3Facility = new System.Windows.Forms.TabPage();
            this.dataGridView3Facility = new System.Windows.Forms.DataGridView();
            this.tabPage4LandScape = new System.Windows.Forms.TabPage();
            this.dataGridView4LandScape = new System.Windows.Forms.DataGridView();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tableLayoutPanel1.SuspendLayout();
            
            this.tabControl1.SuspendLayout();
            this.tabPage0Road.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView0Road)).BeginInit();
            this.tabPage1Building.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1Building)).BeginInit();
            this.tabPage2Trees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2Trees)).BeginInit();
            this.tabPage3Facility.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3Facility)).BeginInit();
            this.tabPage4LandScape.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4LandScape)).BeginInit();
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
            this.tabControl1.Controls.Add(this.tabPage0Road);
            this.tabControl1.Controls.Add(this.tabPage1Building);
            this.tabControl1.Controls.Add(this.tabPage2Trees);
            this.tabControl1.Controls.Add(this.tabPage3Facility);
            this.tabControl1.Controls.Add(this.tabPage4LandScape);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 384);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(875, 198);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage0Road
            // 
            this.tabPage0Road.Controls.Add(this.dataGridView0Road);
            this.tabPage0Road.Location = new System.Drawing.Point(4, 22);
            this.tabPage0Road.Name = "tabPage0Road";
            this.tabPage0Road.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage0Road.Size = new System.Drawing.Size(867, 172);
            this.tabPage0Road.TabIndex = 0;
            this.tabPage0Road.Text = "Road";
            this.tabPage0Road.UseVisualStyleBackColor = true;
            // 
            // dataGridView0Road
            // 
            this.dataGridView0Road.AllowUserToAddRows = false;
            this.dataGridView0Road.AllowUserToDeleteRows = false;
            this.dataGridView0Road.AllowUserToResizeRows = false;
            this.dataGridView0Road.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView0Road.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView0Road.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView0Road.Location = new System.Drawing.Point(3, 3);
            this.dataGridView0Road.MultiSelect = false;
            this.dataGridView0Road.Name = "dataGridView0Road";
            this.dataGridView0Road.ReadOnly = true;
            this.dataGridView0Road.RowTemplate.Height = 23;
            this.dataGridView0Road.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView0Road.Size = new System.Drawing.Size(861, 166);
            this.dataGridView0Road.TabIndex = 0;
            this.dataGridView0Road.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseDoubleClick);
            // 
            // tabPage1Building
            // 
            this.tabPage1Building.Controls.Add(this.dataGridView1Building);
            this.tabPage1Building.Location = new System.Drawing.Point(4, 22);
            this.tabPage1Building.Name = "tabPage1Building";
            this.tabPage1Building.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1Building.Size = new System.Drawing.Size(867, 172);
            this.tabPage1Building.TabIndex = 1;
            this.tabPage1Building.Text = "Building";
            this.tabPage1Building.UseVisualStyleBackColor = true;
            // 
            // dataGridView1Building
            // 
            this.dataGridView1Building.AllowUserToAddRows = false;
            this.dataGridView1Building.AllowUserToDeleteRows = false;
            this.dataGridView1Building.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1Building.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1Building.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1Building.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1Building.MultiSelect = false;
            this.dataGridView1Building.Name = "dataGridView1Building";
            this.dataGridView1Building.ReadOnly = true;
            this.dataGridView1Building.RowTemplate.Height = 23;
            this.dataGridView1Building.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1Building.Size = new System.Drawing.Size(861, 166);
            this.dataGridView1Building.TabIndex = 0;
            this.dataGridView1Building.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseDoubleClick);
            // 
            // tabPage2Trees
            // 
            this.tabPage2Trees.Controls.Add(this.dataGridView2Trees);
            this.tabPage2Trees.Location = new System.Drawing.Point(4, 22);
            this.tabPage2Trees.Name = "tabPage2Trees";
            this.tabPage2Trees.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2Trees.Size = new System.Drawing.Size(867, 172);
            this.tabPage2Trees.TabIndex = 2;
            this.tabPage2Trees.Text = "Trees";
            this.tabPage2Trees.UseVisualStyleBackColor = true;
            // 
            // dataGridView2Trees
            // 
            this.dataGridView2Trees.AllowUserToAddRows = false;
            this.dataGridView2Trees.AllowUserToDeleteRows = false;
            this.dataGridView2Trees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2Trees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2Trees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2Trees.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2Trees.MultiSelect = false;
            this.dataGridView2Trees.Name = "dataGridView2Trees";
            this.dataGridView2Trees.ReadOnly = true;
            this.dataGridView2Trees.RowTemplate.Height = 23;
            this.dataGridView2Trees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2Trees.Size = new System.Drawing.Size(861, 166);
            this.dataGridView2Trees.TabIndex = 0;
            this.dataGridView2Trees.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseDoubleClick);
            // 
            // tabPage3Facility
            // 
            this.tabPage3Facility.Controls.Add(this.dataGridView3Facility);
            this.tabPage3Facility.Location = new System.Drawing.Point(4, 22);
            this.tabPage3Facility.Name = "tabPage3Facility";
            this.tabPage3Facility.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3Facility.Size = new System.Drawing.Size(867, 172);
            this.tabPage3Facility.TabIndex = 3;
            this.tabPage3Facility.Text = "Facility";
            this.tabPage3Facility.UseVisualStyleBackColor = true;
            // 
            // dataGridView3Facility
            // 
            this.dataGridView3Facility.AllowUserToAddRows = false;
            this.dataGridView3Facility.AllowUserToDeleteRows = false;
            this.dataGridView3Facility.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3Facility.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3Facility.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3Facility.Location = new System.Drawing.Point(3, 3);
            this.dataGridView3Facility.MultiSelect = false;
            this.dataGridView3Facility.Name = "dataGridView3Facility";
            this.dataGridView3Facility.ReadOnly = true;
            this.dataGridView3Facility.RowTemplate.Height = 23;
            this.dataGridView3Facility.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3Facility.Size = new System.Drawing.Size(861, 166);
            this.dataGridView3Facility.TabIndex = 0;
            this.dataGridView3Facility.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseDoubleClick);
            // 
            // tabPage4LandScape
            // 
            this.tabPage4LandScape.Controls.Add(this.dataGridView4LandScape);
            this.tabPage4LandScape.Location = new System.Drawing.Point(4, 22);
            this.tabPage4LandScape.Name = "tabPage4LandScape";
            this.tabPage4LandScape.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4LandScape.Size = new System.Drawing.Size(867, 172);
            this.tabPage4LandScape.TabIndex = 4;
            this.tabPage4LandScape.Text = "LandScape";
            this.tabPage4LandScape.UseVisualStyleBackColor = true;
            // 
            // dataGridView4LandScape
            // 
            this.dataGridView4LandScape.AllowUserToAddRows = false;
            this.dataGridView4LandScape.AllowUserToDeleteRows = false;
            this.dataGridView4LandScape.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView4LandScape.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4LandScape.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView4LandScape.Location = new System.Drawing.Point(3, 3);
            this.dataGridView4LandScape.MultiSelect = false;
            this.dataGridView4LandScape.Name = "dataGridView4LandScape";
            this.dataGridView4LandScape.ReadOnly = true;
            this.dataGridView4LandScape.RowTemplate.Height = 23;
            this.dataGridView4LandScape.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView4LandScape.Size = new System.Drawing.Size(861, 166);
            this.dataGridView4LandScape.TabIndex = 0;
            this.dataGridView4LandScape.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 585);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FeatureLocateAndGlow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            
            this.tabControl1.ResumeLayout(false);
            this.tabPage0Road.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView0Road)).EndInit();
            this.tabPage1Building.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1Building)).EndInit();
            this.tabPage2Trees.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2Trees)).EndInit();
            this.tabPage3Facility.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3Facility)).EndInit();
            this.tabPage4LandScape.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4LandScape)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion


        private AxRenderControl axRenderControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage0Road;
        private System.Windows.Forms.TabPage tabPage1Building;
        private System.Windows.Forms.TabPage tabPage2Trees;
        private System.Windows.Forms.TabPage tabPage3Facility;
        private System.Windows.Forms.TabPage tabPage4LandScape;
        private System.Windows.Forms.DataGridView dataGridView0Road;
        private System.Windows.Forms.DataGridView dataGridView1Building;
        private System.Windows.Forms.DataGridView dataGridView2Trees;
        private System.Windows.Forms.DataGridView dataGridView3Facility;
        private System.Windows.Forms.DataGridView dataGridView4LandScape;

    }
}


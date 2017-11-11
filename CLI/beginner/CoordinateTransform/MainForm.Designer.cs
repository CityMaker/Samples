namespace CoordinateTransform
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
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textSourceZ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textSourceY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textSourceX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelectTargetWKT = new System.Windows.Forms.Button();
            this.btnSelectSourceWKT = new System.Windows.Forms.Button();
            this.textTargetWKT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textSourceWKT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textTargetZ = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textTargetY = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textTargetX = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnTransform = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textSourceZ);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textSourceY);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textSourceX);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 132);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "源点坐标";
            // 
            // textSourceZ
            // 
            this.textSourceZ.Location = new System.Drawing.Point(47, 85);
            this.textSourceZ.Name = "textSourceZ";
            this.textSourceZ.Size = new System.Drawing.Size(143, 21);
            this.textSourceZ.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Z";
            // 
            // textSourceY
            // 
            this.textSourceY.Location = new System.Drawing.Point(47, 58);
            this.textSourceY.Name = "textSourceY";
            this.textSourceY.Size = new System.Drawing.Size(143, 21);
            this.textSourceY.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y";
            // 
            // textSourceX
            // 
            this.textSourceX.Location = new System.Drawing.Point(47, 31);
            this.textSourceX.Name = "textSourceX";
            this.textSourceX.Size = new System.Drawing.Size(143, 21);
            this.textSourceX.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 2);
            this.groupBox2.Controls.Add(this.btnSelectTargetWKT);
            this.groupBox2.Controls.Add(this.btnSelectSourceWKT);
            this.groupBox2.Controls.Add(this.textTargetWKT);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textSourceWKT);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(430, 302);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "坐标系";
            // 
            // btnSelectTargetWKT
            // 
            this.btnSelectTargetWKT.Location = new System.Drawing.Point(89, 159);
            this.btnSelectTargetWKT.Name = "btnSelectTargetWKT";
            this.btnSelectTargetWKT.Size = new System.Drawing.Size(51, 23);
            this.btnSelectTargetWKT.TabIndex = 6;
            this.btnSelectTargetWKT.Text = "选择";
            this.btnSelectTargetWKT.UseVisualStyleBackColor = true;
            this.btnSelectTargetWKT.Click += new System.EventHandler(this.btnSelectTargetWKT_Click);
            // 
            // btnSelectSourceWKT
            // 
            this.btnSelectSourceWKT.Location = new System.Drawing.Point(77, 20);
            this.btnSelectSourceWKT.Name = "btnSelectSourceWKT";
            this.btnSelectSourceWKT.Size = new System.Drawing.Size(51, 23);
            this.btnSelectSourceWKT.TabIndex = 5;
            this.btnSelectSourceWKT.Text = "选择";
            this.btnSelectSourceWKT.UseVisualStyleBackColor = true;
            this.btnSelectSourceWKT.Click += new System.EventHandler(this.btnSelectSourceWKT_Click);
            // 
            // textTargetWKT
            // 
            this.textTargetWKT.Location = new System.Drawing.Point(14, 188);
            this.textTargetWKT.Multiline = true;
            this.textTargetWKT.Name = "textTargetWKT";
            this.textTargetWKT.Size = new System.Drawing.Size(397, 108);
            this.textTargetWKT.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "目标坐标系";
            // 
            // textSourceWKT
            // 
            this.textSourceWKT.Location = new System.Drawing.Point(14, 48);
            this.textSourceWKT.Multiline = true;
            this.textSourceWKT.Name = "textSourceWKT";
            this.textSourceWKT.Size = new System.Drawing.Size(397, 105);
            this.textSourceWKT.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "源坐标系";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textTargetZ);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textTargetY);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textTargetX);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(216, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(217, 132);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "目标点坐标";
            // 
            // textTargetZ
            // 
            this.textTargetZ.Location = new System.Drawing.Point(47, 85);
            this.textTargetZ.Name = "textTargetZ";
            this.textTargetZ.Size = new System.Drawing.Size(143, 21);
            this.textTargetZ.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "Z";
            // 
            // textTargetY
            // 
            this.textTargetY.Location = new System.Drawing.Point(47, 58);
            this.textTargetY.Name = "textTargetY";
            this.textTargetY.Size = new System.Drawing.Size(143, 21);
            this.textTargetY.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "Y";
            // 
            // textTargetX
            // 
            this.textTargetX.Location = new System.Drawing.Point(47, 31);
            this.textTargetX.Name = "textTargetX";
            this.textTargetX.Size = new System.Drawing.Size(143, 21);
            this.textTargetX.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "X";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 223F));
            this.tableLayoutPanel1.Controls.Add(this.btnTransform, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 308F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(436, 497);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // btnTransform
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btnTransform, 2);
            this.btnTransform.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTransform.Location = new System.Drawing.Point(3, 449);
            this.btnTransform.Name = "btnTransform";
            this.btnTransform.Size = new System.Drawing.Size(430, 45);
            this.btnTransform.TabIndex = 3;
            this.btnTransform.Text = "转换";
            this.btnTransform.UseVisualStyleBackColor = true;
            this.btnTransform.Click += new System.EventHandler(this.btnTransform_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 497);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
this.Load += new System.EventHandler(this.MainForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CoordinateTransform";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textSourceZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textSourceY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textSourceX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textTargetWKT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textSourceWKT;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textTargetZ;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textTargetY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textTargetX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSelectSourceWKT;
        private System.Windows.Forms.Button btnSelectTargetWKT;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnTransform;


    }
}


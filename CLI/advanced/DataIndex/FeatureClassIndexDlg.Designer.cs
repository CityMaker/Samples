namespace DataIndex
{
    partial class FeatureClassIndexDlg
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lb_AttrIndexFields = new System.Windows.Forms.ListBox();
            this.btn_AttrDelete = new System.Windows.Forms.Button();
            this.btn_AttrAdd = new System.Windows.Forms.Button();
            this.lb_AttrIndex = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_SpatialCal = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lb_SpatialIndexGeo = new System.Windows.Forms.ListBox();
            this.btn_SpatialDelete = new System.Windows.Forms.Button();
            this.btn_SpatialAdd = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tb_SpatialGeoField = new System.Windows.Forms.TextBox();
            this.tb_L3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_L2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_L1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btn_RenderCal = new System.Windows.Forms.Button();
            this.btn_RenderDelete = new System.Windows.Forms.Button();
            this.btn_RenderAdd = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btn_RenderFieldDelete = new System.Windows.Forms.Button();
            this.btn_RenderFieldAdd = new System.Windows.Forms.Button();
            this.lb_RenderField = new System.Windows.Forms.ListBox();
            this.lb_RenderAllField = new System.Windows.Forms.ListBox();
            this.tb_RenderL1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_RenderGeoField = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lb_RenderIndexGeo = new System.Windows.Forms.ListBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lb_AttrIndexFields);
            this.groupBox1.Controls.Add(this.btn_AttrDelete);
            this.groupBox1.Controls.Add(this.btn_AttrAdd);
            this.groupBox1.Controls.Add(this.lb_AttrIndex);
            this.groupBox1.Location = new System.Drawing.Point(12, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 189);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性索引";
            // 
            // lb_AttrIndexFields
            // 
            this.lb_AttrIndexFields.FormattingEnabled = true;
            this.lb_AttrIndexFields.ItemHeight = 12;
            this.lb_AttrIndexFields.Location = new System.Drawing.Point(6, 104);
            this.lb_AttrIndexFields.Name = "lb_AttrIndexFields";
            this.lb_AttrIndexFields.Size = new System.Drawing.Size(293, 76);
            this.lb_AttrIndexFields.TabIndex = 4;
            // 
            // btn_AttrDelete
            // 
            this.btn_AttrDelete.Location = new System.Drawing.Point(305, 49);
            this.btn_AttrDelete.Name = "btn_AttrDelete";
            this.btn_AttrDelete.Size = new System.Drawing.Size(59, 23);
            this.btn_AttrDelete.TabIndex = 3;
            this.btn_AttrDelete.Text = "删除";
            this.btn_AttrDelete.UseVisualStyleBackColor = true;
            this.btn_AttrDelete.Click += new System.EventHandler(this.btn_AttrDelete_Click);
            // 
            // btn_AttrAdd
            // 
            this.btn_AttrAdd.Location = new System.Drawing.Point(305, 20);
            this.btn_AttrAdd.Name = "btn_AttrAdd";
            this.btn_AttrAdd.Size = new System.Drawing.Size(59, 23);
            this.btn_AttrAdd.TabIndex = 2;
            this.btn_AttrAdd.Text = "添加";
            this.btn_AttrAdd.UseVisualStyleBackColor = true;
            this.btn_AttrAdd.Click += new System.EventHandler(this.btn_AttrAdd_Click);
            // 
            // lb_AttrIndex
            // 
            this.lb_AttrIndex.FormattingEnabled = true;
            this.lb_AttrIndex.ItemHeight = 12;
            this.lb_AttrIndex.Location = new System.Drawing.Point(6, 20);
            this.lb_AttrIndex.Name = "lb_AttrIndex";
            this.lb_AttrIndex.Size = new System.Drawing.Size(293, 76);
            this.lb_AttrIndex.TabIndex = 0;
            this.lb_AttrIndex.SelectedIndexChanged += new System.EventHandler(this.lb_AttrIndex_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_SpatialCal);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.btn_SpatialDelete);
            this.groupBox2.Controls.Add(this.btn_SpatialAdd);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Location = new System.Drawing.Point(12, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(370, 145);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "空间索引";
            // 
            // btn_SpatialCal
            // 
            this.btn_SpatialCal.Location = new System.Drawing.Point(305, 85);
            this.btn_SpatialCal.Name = "btn_SpatialCal";
            this.btn_SpatialCal.Size = new System.Drawing.Size(59, 23);
            this.btn_SpatialCal.TabIndex = 10;
            this.btn_SpatialCal.Text = "计算";
            this.btn_SpatialCal.UseVisualStyleBackColor = true;
            this.btn_SpatialCal.Click += new System.EventHandler(this.btn_SpatialCal_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lb_SpatialIndexGeo);
            this.groupBox5.Location = new System.Drawing.Point(6, 14);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(123, 124);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            // 
            // lb_SpatialIndexGeo
            // 
            this.lb_SpatialIndexGeo.FormattingEnabled = true;
            this.lb_SpatialIndexGeo.ItemHeight = 12;
            this.lb_SpatialIndexGeo.Location = new System.Drawing.Point(6, 15);
            this.lb_SpatialIndexGeo.Name = "lb_SpatialIndexGeo";
            this.lb_SpatialIndexGeo.Size = new System.Drawing.Size(111, 100);
            this.lb_SpatialIndexGeo.TabIndex = 0;
            this.lb_SpatialIndexGeo.SelectedIndexChanged += new System.EventHandler(this.lb_SpatialIndexGeo_SelectedIndexChanged);
            // 
            // btn_SpatialDelete
            // 
            this.btn_SpatialDelete.Location = new System.Drawing.Point(305, 56);
            this.btn_SpatialDelete.Name = "btn_SpatialDelete";
            this.btn_SpatialDelete.Size = new System.Drawing.Size(59, 23);
            this.btn_SpatialDelete.TabIndex = 9;
            this.btn_SpatialDelete.Text = "删除";
            this.btn_SpatialDelete.UseVisualStyleBackColor = true;
            this.btn_SpatialDelete.Click += new System.EventHandler(this.btn_SpatialDelete_Click);
            // 
            // btn_SpatialAdd
            // 
            this.btn_SpatialAdd.Location = new System.Drawing.Point(305, 27);
            this.btn_SpatialAdd.Name = "btn_SpatialAdd";
            this.btn_SpatialAdd.Size = new System.Drawing.Size(59, 23);
            this.btn_SpatialAdd.TabIndex = 8;
            this.btn_SpatialAdd.Text = "添加";
            this.btn_SpatialAdd.UseVisualStyleBackColor = true;
            this.btn_SpatialAdd.Click += new System.EventHandler(this.btn_SpatialAdd_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tb_SpatialGeoField);
            this.groupBox4.Controls.Add(this.tb_L3);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.tb_L2);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.tb_L1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(130, 14);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(171, 124);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            // 
            // tb_SpatialGeoField
            // 
            this.tb_SpatialGeoField.Location = new System.Drawing.Point(52, 15);
            this.tb_SpatialGeoField.Name = "tb_SpatialGeoField";
            this.tb_SpatialGeoField.ReadOnly = true;
            this.tb_SpatialGeoField.Size = new System.Drawing.Size(113, 21);
            this.tb_SpatialGeoField.TabIndex = 3;
            // 
            // tb_L3
            // 
            this.tb_L3.Location = new System.Drawing.Point(52, 93);
            this.tb_L3.Name = "tb_L3";
            this.tb_L3.Size = new System.Drawing.Size(113, 21);
            this.tb_L3.TabIndex = 8;
            this.tb_L3.TextChanged += new System.EventHandler(this.tb_L3_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "空间列";
            // 
            // tb_L2
            // 
            this.tb_L2.Location = new System.Drawing.Point(52, 67);
            this.tb_L2.Name = "tb_L2";
            this.tb_L2.Size = new System.Drawing.Size(113, 21);
            this.tb_L2.TabIndex = 6;
            this.tb_L2.TextChanged += new System.EventHandler(this.tb_L2_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "L3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "L2";
            // 
            // tb_L1
            // 
            this.tb_L1.Location = new System.Drawing.Point(52, 41);
            this.tb_L1.Name = "tb_L1";
            this.tb_L1.Size = new System.Drawing.Size(113, 21);
            this.tb_L1.TabIndex = 5;
            this.tb_L1.TextChanged += new System.EventHandler(this.tb_L1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "L1";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btn_RenderCal);
            this.groupBox6.Controls.Add(this.btn_RenderDelete);
            this.groupBox6.Controls.Add(this.btn_RenderAdd);
            this.groupBox6.Controls.Add(this.groupBox7);
            this.groupBox6.Controls.Add(this.groupBox3);
            this.groupBox6.Location = new System.Drawing.Point(12, 341);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(370, 190);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "渲染索引";
            // 
            // btn_RenderCal
            // 
            this.btn_RenderCal.Location = new System.Drawing.Point(289, 20);
            this.btn_RenderCal.Name = "btn_RenderCal";
            this.btn_RenderCal.Size = new System.Drawing.Size(59, 23);
            this.btn_RenderCal.TabIndex = 11;
            this.btn_RenderCal.Text = "计算";
            this.btn_RenderCal.UseVisualStyleBackColor = true;
            this.btn_RenderCal.Click += new System.EventHandler(this.btn_RenderCal_Click);
            // 
            // btn_RenderDelete
            // 
            this.btn_RenderDelete.Location = new System.Drawing.Point(165, 20);
            this.btn_RenderDelete.Name = "btn_RenderDelete";
            this.btn_RenderDelete.Size = new System.Drawing.Size(59, 23);
            this.btn_RenderDelete.TabIndex = 10;
            this.btn_RenderDelete.Text = "删除";
            this.btn_RenderDelete.UseVisualStyleBackColor = true;
            this.btn_RenderDelete.Click += new System.EventHandler(this.btn_RenderDelete_Click);
            // 
            // btn_RenderAdd
            // 
            this.btn_RenderAdd.Location = new System.Drawing.Point(50, 20);
            this.btn_RenderAdd.Name = "btn_RenderAdd";
            this.btn_RenderAdd.Size = new System.Drawing.Size(59, 23);
            this.btn_RenderAdd.TabIndex = 9;
            this.btn_RenderAdd.Text = "添加";
            this.btn_RenderAdd.UseVisualStyleBackColor = true;
            this.btn_RenderAdd.Click += new System.EventHandler(this.btn_RenderAdd_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btn_RenderFieldDelete);
            this.groupBox7.Controls.Add(this.btn_RenderFieldAdd);
            this.groupBox7.Controls.Add(this.lb_RenderField);
            this.groupBox7.Controls.Add(this.lb_RenderAllField);
            this.groupBox7.Controls.Add(this.tb_RenderL1);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.tb_RenderGeoField);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Location = new System.Drawing.Point(135, 39);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(229, 145);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            // 
            // btn_RenderFieldDelete
            // 
            this.btn_RenderFieldDelete.Location = new System.Drawing.Point(102, 94);
            this.btn_RenderFieldDelete.Name = "btn_RenderFieldDelete";
            this.btn_RenderFieldDelete.Size = new System.Drawing.Size(22, 23);
            this.btn_RenderFieldDelete.TabIndex = 11;
            this.btn_RenderFieldDelete.Text = "<";
            this.btn_RenderFieldDelete.UseVisualStyleBackColor = true;
            this.btn_RenderFieldDelete.Click += new System.EventHandler(this.btn_RenderFieldDelete_Click);
            // 
            // btn_RenderFieldAdd
            // 
            this.btn_RenderFieldAdd.Location = new System.Drawing.Point(102, 48);
            this.btn_RenderFieldAdd.Name = "btn_RenderFieldAdd";
            this.btn_RenderFieldAdd.Size = new System.Drawing.Size(22, 23);
            this.btn_RenderFieldAdd.TabIndex = 10;
            this.btn_RenderFieldAdd.Text = ">";
            this.btn_RenderFieldAdd.UseVisualStyleBackColor = true;
            this.btn_RenderFieldAdd.Click += new System.EventHandler(this.btn_RenderFieldAdd_Click);
            // 
            // lb_RenderField
            // 
            this.lb_RenderField.FormattingEnabled = true;
            this.lb_RenderField.ItemHeight = 12;
            this.lb_RenderField.Location = new System.Drawing.Point(128, 36);
            this.lb_RenderField.Name = "lb_RenderField";
            this.lb_RenderField.Size = new System.Drawing.Size(95, 100);
            this.lb_RenderField.TabIndex = 9;
            // 
            // lb_RenderAllField
            // 
            this.lb_RenderAllField.FormattingEnabled = true;
            this.lb_RenderAllField.ItemHeight = 12;
            this.lb_RenderAllField.Location = new System.Drawing.Point(6, 37);
            this.lb_RenderAllField.Name = "lb_RenderAllField";
            this.lb_RenderAllField.Size = new System.Drawing.Size(95, 100);
            this.lb_RenderAllField.TabIndex = 8;
            // 
            // tb_RenderL1
            // 
            this.tb_RenderL1.Location = new System.Drawing.Point(141, 10);
            this.tb_RenderL1.Name = "tb_RenderL1";
            this.tb_RenderL1.Size = new System.Drawing.Size(82, 21);
            this.tb_RenderL1.TabIndex = 7;
            this.tb_RenderL1.TextChanged += new System.EventHandler(this.tb_RenderL1_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(125, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "L1";
            // 
            // tb_RenderGeoField
            // 
            this.tb_RenderGeoField.Location = new System.Drawing.Point(17, 10);
            this.tb_RenderGeoField.Name = "tb_RenderGeoField";
            this.tb_RenderGeoField.ReadOnly = true;
            this.tb_RenderGeoField.Size = new System.Drawing.Size(84, 21);
            this.tb_RenderGeoField.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "列";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lb_RenderIndexGeo);
            this.groupBox3.Location = new System.Drawing.Point(6, 39);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(123, 145);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // lb_RenderIndexGeo
            // 
            this.lb_RenderIndexGeo.FormattingEnabled = true;
            this.lb_RenderIndexGeo.ItemHeight = 12;
            this.lb_RenderIndexGeo.Location = new System.Drawing.Point(6, 12);
            this.lb_RenderIndexGeo.Name = "lb_RenderIndexGeo";
            this.lb_RenderIndexGeo.Size = new System.Drawing.Size(111, 124);
            this.lb_RenderIndexGeo.TabIndex = 0;
            this.lb_RenderIndexGeo.SelectedIndexChanged += new System.EventHandler(this.lb_RenderIndexGeo_SelectedIndexChanged);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(79, 537);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 3;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(236, 537);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // FeatureClassIndexDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 572);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FeatureClassIndexDlg";
            this.Text = "索引";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_AttrDelete;
        private System.Windows.Forms.Button btn_AttrAdd;
        private System.Windows.Forms.ListBox lb_AttrIndex;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lb_AttrIndexFields;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tb_L3;
        private System.Windows.Forms.TextBox tb_L2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_L1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_SpatialGeoField;
        private System.Windows.Forms.Button btn_SpatialDelete;
        private System.Windows.Forms.Button btn_SpatialAdd;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_SpatialCal;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ListBox lb_SpatialIndexGeo;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lb_RenderIndexGeo;
        private System.Windows.Forms.Button btn_RenderAdd;
        private System.Windows.Forms.Button btn_RenderCal;
        private System.Windows.Forms.Button btn_RenderDelete;
        private System.Windows.Forms.Button btn_RenderFieldDelete;
        private System.Windows.Forms.Button btn_RenderFieldAdd;
        private System.Windows.Forms.ListBox lb_RenderField;
        private System.Windows.Forms.ListBox lb_RenderAllField;
        private System.Windows.Forms.TextBox tb_RenderL1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_RenderGeoField;
        private System.Windows.Forms.Label label5;
    }
}
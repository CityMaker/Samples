namespace Domain
{
    partial class EditDomainDlg
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgv_DomainAttr = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fieldtype = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.domaintype = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.IsNew = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_DomainValue = new System.Windows.Forms.DataGridView();
            this.btn_Save = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DomainAttr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DomainValue)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgv_DomainAttr);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv_DomainValue);
            this.splitContainer1.Size = new System.Drawing.Size(442, 442);
            this.splitContainer1.SplitterDistance = 215;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgv_DomainAttr
            // 
            this.dgv_DomainAttr.AllowUserToAddRows = false;
            this.dgv_DomainAttr.AllowUserToDeleteRows = false;
            this.dgv_DomainAttr.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_DomainAttr.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_DomainAttr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DomainAttr.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.description,
            this.fieldtype,
            this.domaintype,
            this.IsNew});
            this.dgv_DomainAttr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DomainAttr.Location = new System.Drawing.Point(0, 0);
            this.dgv_DomainAttr.MultiSelect = false;
            this.dgv_DomainAttr.Name = "dgv_DomainAttr";
            this.dgv_DomainAttr.RowHeadersVisible = false;
            this.dgv_DomainAttr.RowTemplate.Height = 23;
            this.dgv_DomainAttr.Size = new System.Drawing.Size(442, 215);
            this.dgv_DomainAttr.TabIndex = 0;
            this.dgv_DomainAttr.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_DomainAttr_CellValueChanged);
            this.dgv_DomainAttr.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_DomainAttr_CellEnter);
            this.dgv_DomainAttr.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgv_DomainAttr_KeyUp);
            // 
            // name
            // 
            this.name.HeaderText = "名称";
            this.name.Name = "name";
            // 
            // description
            // 
            this.description.HeaderText = "描述";
            this.description.Name = "description";
            // 
            // fieldtype
            // 
            this.fieldtype.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.fieldtype.HeaderText = "字段类型";
            this.fieldtype.Items.AddRange(new object[] {
            "",
            "Int16",
            "Int32",
            "Float",
            "Double",
            "String"});
            this.fieldtype.Name = "fieldtype";
            // 
            // domaintype
            // 
            this.domaintype.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.domaintype.HeaderText = "域类型";
            this.domaintype.Items.AddRange(new object[] {
            "",
            "值域型",
            "枚举型"});
            this.domaintype.Name = "domaintype";
            // 
            // IsNew
            // 
            this.IsNew.HeaderText = "是否新建";
            this.IsNew.Name = "IsNew";
            this.IsNew.Visible = false;
            // 
            // dgv_DomainValue
            // 
            this.dgv_DomainValue.AllowUserToAddRows = false;
            this.dgv_DomainValue.AllowUserToDeleteRows = false;
            this.dgv_DomainValue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_DomainValue.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_DomainValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DomainValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DomainValue.Location = new System.Drawing.Point(0, 0);
            this.dgv_DomainValue.MultiSelect = false;
            this.dgv_DomainValue.Name = "dgv_DomainValue";
            this.dgv_DomainValue.RowHeadersVisible = false;
            this.dgv_DomainValue.RowTemplate.Height = 23;
            this.dgv_DomainValue.Size = new System.Drawing.Size(442, 223);
            this.dgv_DomainValue.TabIndex = 1;
            this.dgv_DomainValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgv_DomainValue_KeyUp);
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Save.Location = new System.Drawing.Point(355, 448);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 1;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // EditDomainDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 483);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.splitContainer1);
            this.Name = "EditDomainDlg";
            this.Text = "属性域";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DomainAttr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DomainValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgv_DomainAttr;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.DataGridView dgv_DomainValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewComboBoxColumn fieldtype;
        private System.Windows.Forms.DataGridViewComboBoxColumn domaintype;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsNew;

    }
}
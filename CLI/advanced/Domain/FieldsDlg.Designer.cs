namespace Domain
{
    partial class FieldsDlg
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
            this.btn_Save = new System.Windows.Forms.Button();
            this.dgv_FieldInfo = new System.Windows.Forms.DataGridView();
            this.fieldname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fieldtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FieldInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Save.Location = new System.Drawing.Point(355, 454);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 3;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // dgv_FieldInfo
            // 
            this.dgv_FieldInfo.AllowUserToAddRows = false;
            this.dgv_FieldInfo.AllowUserToDeleteRows = false;
            this.dgv_FieldInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_FieldInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_FieldInfo.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_FieldInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_FieldInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fieldname,
            this.fieldtype});
            this.dgv_FieldInfo.Location = new System.Drawing.Point(1, -1);
            this.dgv_FieldInfo.MultiSelect = false;
            this.dgv_FieldInfo.Name = "dgv_FieldInfo";
            this.dgv_FieldInfo.RowHeadersVisible = false;
            this.dgv_FieldInfo.RowTemplate.Height = 23;
            this.dgv_FieldInfo.Size = new System.Drawing.Size(442, 449);
            this.dgv_FieldInfo.TabIndex = 0;
            this.dgv_FieldInfo.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_FieldInfo_CellEnter);
            // 
            // fieldname
            // 
            this.fieldname.HeaderText = "字段名称";
            this.fieldname.Name = "fieldname";
            this.fieldname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fieldtype
            // 
            this.fieldtype.HeaderText = "字段类型";
            this.fieldtype.Name = "fieldtype";
            this.fieldtype.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FieldsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 483);
            this.Controls.Add(this.dgv_FieldInfo);
            this.Controls.Add(this.btn_Save);
            this.Name = "FieldsDlg";
            this.Text = "字段信息";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FieldInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.DataGridView dgv_FieldInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldname;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldtype;
    }
}
namespace Domain
{
    partial class FeatureAttrEditDlg
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
            this.dgv_FieldValue = new System.Windows.Forms.DataGridView();
            this.fieldname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fieldvalue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FieldValue)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_FieldValue
            // 
            this.dgv_FieldValue.AllowUserToAddRows = false;
            this.dgv_FieldValue.AllowUserToDeleteRows = false;
            this.dgv_FieldValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_FieldValue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_FieldValue.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_FieldValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_FieldValue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fieldname,
            this.fieldvalue});
            this.dgv_FieldValue.Location = new System.Drawing.Point(0, 2);
            this.dgv_FieldValue.MultiSelect = false;
            this.dgv_FieldValue.Name = "dgv_FieldValue";
            this.dgv_FieldValue.RowHeadersVisible = false;
            this.dgv_FieldValue.RowTemplate.Height = 23;
            this.dgv_FieldValue.Size = new System.Drawing.Size(442, 449);
            this.dgv_FieldValue.TabIndex = 4;
            this.dgv_FieldValue.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_FieldValue_CellValueChanged);
            this.dgv_FieldValue.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_FieldValue_CellEnter);
            // 
            // fieldname
            // 
            this.fieldname.HeaderText = "字段名称";
            this.fieldname.Name = "fieldname";
            this.fieldname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fieldvalue
            // 
            this.fieldvalue.HeaderText = "值";
            this.fieldvalue.Name = "fieldvalue";
            this.fieldvalue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Save.Location = new System.Drawing.Point(354, 457);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 5;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // FeatureAttrEditDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 483);
            this.Controls.Add(this.dgv_FieldValue);
            this.Controls.Add(this.btn_Save);
            this.Name = "FeatureAttrEditDlg";
            this.Text = "FeatureAttrEditDlg";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FieldValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_FieldValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldname;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldvalue;
        private System.Windows.Forms.Button btn_Save;
    }
}
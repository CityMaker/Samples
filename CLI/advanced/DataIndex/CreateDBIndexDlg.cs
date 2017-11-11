using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gvitech.CityMaker.FdeCore;

namespace DataIndex
{
    public partial class CreateDBIndexDlg : Form
    {
        private IFieldInfoCollection fcFields = null;

        private List<string> indexNames = null;

        private string fidField = "";

        public string sDBIndexName = "";

        public List<string> listDBIndexFields = new List<string>();


        public CreateDBIndexDlg(IFieldInfoCollection pFields,List<string> existIndexName)
        {
            InitializeComponent();
            fcFields = pFields;
            indexNames = existIndexName;

            for (int k = 0; k < fcFields.Count; k++)
            {
                IFieldInfo fcField = fcFields.Get(k);
                if (fcField.FieldType == gviFieldType.gviFieldFID)
                    fidField = fcField.Name;
                if (fcField.FieldType != gviFieldType.gviFieldGeometry && fcField.FieldType != gviFieldType.gviFieldBlob)
                    this.lb_Fields.Items.Add(new myFieldInfo(fcField));
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_Name.Text.Trim()))
            {
                MessageBox.Show("索引名称不能为空");
                return;
            }
            if (this.tb_Name.Text.Trim().Length > 32)
            {
                MessageBox.Show("索引名称不能超过32个字符");
                return;
            }
            if (indexNames.Contains(this.tb_Name.Text.Trim().ToUpper()))
            {
                MessageBox.Show("已存在的索引名称，索引名不区分大小写");
                return;
            }
            if (this.lb_SelectFields.Items.Count < 1)
            {
                MessageBox.Show("创建属性索引需要指定至少一个字段");
                return;
            }
            if (this.lb_SelectFields.Items.Count == 1 && fidField.Equals(this.lb_SelectFields.Items[0].ToString()))
            {
                MessageBox.Show("无法创建只有FID列的属性索引");
                return;
            }
            this.sDBIndexName = this.tb_Name.Text.Trim();
            for (int l = 0; l < this.lb_SelectFields.Items.Count; l++)
            {
                this.listDBIndexFields.Add(this.lb_SelectFields.Items[l].ToString());
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (this.lb_Fields.SelectedItems.Count > 0)
            {
                myFieldInfo selectField = this.lb_Fields.SelectedItems[0] as myFieldInfo;
                this.lb_Fields.Items.RemoveAt(this.lb_Fields.SelectedIndices[0]);
                this.lb_SelectFields.Items.Add(selectField);
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.lb_SelectFields.SelectedItems.Count > 0)
            {
                myFieldInfo selectField = this.lb_SelectFields.SelectedItems[0] as myFieldInfo;
                this.lb_SelectFields.Items.RemoveAt(this.lb_SelectFields.SelectedIndices[0]);
                this.lb_Fields.Items.Add(selectField);
            }
        }
    }
}

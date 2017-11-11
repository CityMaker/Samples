using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gvitech.CityMaker.FdeCore;
using System.Runtime.InteropServices;

namespace Domain
{
    public partial class EditDomainDlg : Form
    {
        private IDataSource ds = null;
        private List<string> deletedomains = new List<string>();

        private string sMinFieldName = "最小值";
        private string sMaxFieldName = "最大值";

        private string sEmunFieldName = "枚举值";
        private string sDscribFieldName = "描述";

        public EditDomainDlg(IDataSource pDs)
        {
            InitializeComponent();

            ds = pDs;
            this.LoadDsDomains();
        }

        private void LoadDsDomains()
        {
            IDomain d = null;
            IRangeDomain rd = null;
            ICodedValueDomain cv = null;
            try
            {
                string[] domainList = ds.GetDomainNames();
                if (domainList != null)
                {
                    for (int l = 0; l < domainList.Length; l++)
                    {
                        DataTable curtable = new DataTable();
                        d = ds.GetDomainByName(domainList[l].ToString());
                        if (d == null) continue;
                        string dt = d.DomainType == gviDomainType.gviDomainRange ? "值域型" : "枚举型";
                        string ft = ConvertFieldTypeByString(d.FieldType);

                        int iRowIndex = this.dgv_DomainAttr.Rows.Add(new object[] { d.Name, d.Description, ft, dt, false });
                        if (dt == "值域型")
                        {
                            rd = d as IRangeDomain;
                            curtable.Columns.Add(sMinFieldName, GetColumnTypeByString(ft));
                            curtable.Columns.Add(sMaxFieldName, GetColumnTypeByString(ft));
                            curtable.Rows.Add(new object[] { rd.MinValue, rd.MaxValue });
                        }
                        else
                        {
                            cv = d as ICodedValueDomain;
                            curtable.Columns.Add(sEmunFieldName, GetColumnTypeByString(ft));
                            curtable.Columns.Add(sDscribFieldName, typeof(string));
                            for (int k = 0; k < cv.CodeCount; k++)
                            {
                                curtable.Rows.Add(new object[] { cv.GetCodeValue(k), cv.GetCodeName(k) });
                            }
                            //添加空行
                            for (int m = 0; m < 200; m++)
                            {
                                curtable.Rows.Add(new object[] { null, "" });
                            }
                        }
                        this.dgv_DomainAttr.Rows[iRowIndex].Tag = curtable;
                    }
                }
                for (int i = 0; i < 100; i++)
                {
                    this.dgv_DomainAttr.Rows.Add(new object[] { "", "", "", "", true });
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cv != null)
                {
                    //Marshal.ReleaseComObject(cv);
                    cv = null;
                }
                if (rd != null)
                {
                    //Marshal.ReleaseComObject(rd);
                    rd = null;
                }
                if (d != null)
                {
                    //Marshal.ReleaseComObject(d);
                    d = null;
                }
            }
        }

        String ConvertFieldTypeByString(gviFieldType fieldtype)
        {
            switch (fieldtype)
            {
                case gviFieldType.gviFieldInt16: return "Int16";

                case gviFieldType.gviFieldInt32: return "Int32";

                case gviFieldType.gviFieldInt64: return "Int64";

                case gviFieldType.gviFieldFloat: return "Float";

                case gviFieldType.gviFieldDouble: return "Double";

                case gviFieldType.gviFieldDate: return "Date";

                default: return "String";
            }
        }

        Type GetColumnTypeByString(String type)
        {
            switch (type)
            {
                case "Int16": return typeof(short);

                case "Int32": return typeof(int);

                case "Int64": return typeof(long);

                case "Float": return typeof(float);

                case "Double": return typeof(double);

                case "Date": return typeof(DateTime);

                default: return typeof(String);
            }
        }

        private bool HasDomain(String domainName, IDataSource ds)
        {
            String[] list = ds.GetDomainNames();
            foreach (String s in list)
            {
                if (domainName.Equals(s))
                    return true;
            }
            return false;
        }

        gviFieldType GetFDEFieldTypeByString(String fieldtype)
        {
            switch (fieldtype)
            {
                case "Int16": return gviFieldType.gviFieldInt16;

                case "Int32": return gviFieldType.gviFieldInt32;

                case "Int64": return gviFieldType.gviFieldInt64;

                case "Float": return gviFieldType.gviFieldFloat;

                case "Double": return gviFieldType.gviFieldDouble;

                case "Date": return gviFieldType.gviFieldDate;

                default: return gviFieldType.gviFieldString;
            }
        }

        object GetColumnDefaultValue(string type)
        {
            switch (type)
            {
                case "Int16": return 0;

                case "Int32": return 0;

                case "Int64": return 0;

                case "Float": return 0;

                case "Double": return 0;

                case "Date": return DateTime.Now;

                default: return "";
            }
        }

        private void dgv_DomainAttr_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow curRow = this.dgv_DomainAttr.Rows[e.RowIndex];
                DataTable curTable = null;
                if (curRow.Tag != null)
                {
                    curTable = curRow.Tag as DataTable;
                }
                else
                {
                    curTable = new DataTable();
                    curRow.Tag = curTable;
                }
                this.dgv_DomainValue.DataSource = curTable;
                bool bIsNew = (bool)curRow.Cells["IsNew"].Value;
                if (!bIsNew && e.ColumnIndex != curRow.Cells["description"].ColumnIndex)
                    curRow.Cells[e.ColumnIndex].ReadOnly = true;
                else
                {
                    string[] strArr = null;
                    if (e.ColumnIndex == curRow.Cells["fieldtype"].ColumnIndex)
                    {
                        if (curRow.Cells["domaintype"].Value == null || curRow.Cells["domaintype"].Value.ToString() != "值域型")
                            strArr = new string[] { "","Int16", "Int32", "Float", "Double", "String" };
                        else
                            strArr = new string[] { "","Int16", "Int32", "Float", "Double" };
                        
                    }
                    else if (e.ColumnIndex == curRow.Cells["domaintype"].ColumnIndex)
                    {
                        if (curRow.Cells["fieldtype"].Value.ToString() != "String")
                            strArr = new string[] { "", "值域型", "枚举型" };
                        else
                            strArr = new string[] { "", "枚举型" };
                    }
                    if (strArr != null && strArr.Length > 0)
                    {
                        (curRow.Cells[e.ColumnIndex] as DataGridViewComboBoxCell).Items.Clear();
                        (curRow.Cells[e.ColumnIndex] as DataGridViewComboBoxCell).Items.AddRange(strArr);
                    }
                }
            }
        }

        private void dgv_DomainAttr_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == this.dgv_DomainAttr.Rows[e.RowIndex].Cells["fieldtype"].ColumnIndex || e.ColumnIndex == this.dgv_DomainAttr.Rows[e.RowIndex].Cells["domaintype"].ColumnIndex )
                {
                    DataGridViewRow curRow = this.dgv_DomainAttr.Rows[e.RowIndex];
                    DataTable dt = null;
                    if (curRow.Tag != null)
                    {
                        dt = curRow.Tag as DataTable;
                    }
                    if (dt == null)
                    {
                        dt = new DataTable();
                        curRow.Tag = dt;
                    }
                    dt.Rows.Clear();
                    dt.Columns.Clear();
                    if (curRow.Cells["domaintype"].Value == null || string.IsNullOrEmpty(curRow.Cells["domaintype"].Value.ToString()))
                    {
                        this.dgv_DomainValue.DataSource = dt;
                        return;
                    }
                    if (curRow.Cells["domaintype"].Value.ToString() == "值域型")
                    {
                        string ft = "";
                        if(curRow.Cells["fieldtype"].Value != null && !string.IsNullOrEmpty(curRow.Cells["fieldtype"].Value.ToString()))
                            ft = curRow.Cells["fieldtype"].Value.ToString();
                        dt.Columns.Add(sMinFieldName, GetColumnTypeByString(ft));
                        dt.Columns.Add(sMaxFieldName, GetColumnTypeByString(ft));
                        dt.Rows.Add(null, null);
                    }
                    if (curRow.Cells["domaintype"].Value.ToString() == "枚举型")
                    {
                        string ft = "";
                        if(curRow.Cells["fieldtype"].Value != null && !string.IsNullOrEmpty(curRow.Cells["fieldtype"].Value.ToString()))
                            ft = curRow.Cells["fieldtype"].Value.ToString();

                        dt.Columns.Add(sEmunFieldName, GetColumnTypeByString(ft));
                        dt.Columns.Add(sDscribFieldName, typeof(string));
                        for (int i = 0; i < 200; i++)
                        {
                            dt.Rows.Add(null, "");
                        }
                    }
                    this.dgv_DomainValue.DataSource = dt;
                }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            IDomain domain = null;
            IRangeDomain rd = null;
            ICodedValueDomain cv = null;
            IDomainFactory df = new DomainFactory();
            DataTable table = null;
            try
            {
                //保存时，删除操作才生效
                foreach (string s in deletedomains)
                {
                    ds.DeleteDomain(s);
                }
                for (int i = 0; i < this.dgv_DomainAttr.Rows.Count; i++)
                {
                    DataGridViewRow myRow = this.dgv_DomainAttr.Rows[i];
                    string name = myRow.Cells["name"].Value == null ? "" : myRow.Cells["name"].Value.ToString();
                    string description = myRow.Cells["description"].Value == null ? "" : myRow.Cells["description"].Value.ToString();
                    string ft = myRow.Cells["fieldtype"].Value == null ? "" : myRow.Cells["fieldtype"].Value.ToString();
                    string dt =  myRow.Cells["domaintype"].Value == null ? "" : myRow.Cells["domaintype"].Value.ToString();
                    if (name.Equals("")) continue;        //名称为空，不创建
                    if (myRow.Tag == null) continue;
                    if (ft.Equals("String") && dt.Equals("值域型")) continue;
                    table = myRow.Tag as DataTable;
                    if (!HasDomain(name, ds))
                    {
                        if (dt == "值域型")
                        {
                            rd = df.CreateRangeDomain(name, GetFDEFieldTypeByString(ft));
                            rd.Description = description;
                            rd.MinValue = table.Rows[0].IsNull(sMinFieldName) ? GetColumnDefaultValue(ft) : table.Rows[0][sMinFieldName];
                            rd.MaxValue = table.Rows[0].IsNull(sMaxFieldName) ? GetColumnDefaultValue(ft) : table.Rows[0][sMaxFieldName];

                            ds.AddDomain(rd);
                        }
                        else if (dt == "枚举型")
                        {
                            cv = df.CreateCodedValueDomain(name, GetFDEFieldTypeByString(ft));
                            cv.Description = description;
                            for (int j = 0; j < table.Rows.Count; j++)
                            {
                                if (table.Rows[j][sDscribFieldName].ToString() != "")
                                {
                                    if (table.Rows[j].IsNull(sEmunFieldName))
                                        continue;
                                    cv.AddCode(table.Rows[j][sEmunFieldName], table.Rows[j][sDscribFieldName].ToString());
                                }
                            }
                            ds.AddDomain(cv);
                        }
                    }
                    else
                    {
                        domain = ds.GetDomainByName(name);
                        if (dt == "值域型")
                        {
                            rd = domain as IRangeDomain;
                            rd.Description = description;
                            rd.MaxValue = table.Rows[0].IsNull(sMaxFieldName) ? GetColumnDefaultValue(ft) : table.Rows[0][sMaxFieldName];
                            rd.MinValue = table.Rows[0].IsNull(sMinFieldName) ? GetColumnDefaultValue(ft) : table.Rows[0][sMinFieldName];
                            ds.ModifyDomain(rd);
                        }
                        else
                        {
                            cv = domain as ICodedValueDomain;
                            cv.Description = description;
                            int codecount = cv.CodeCount;
                            for (int a = 0; a < codecount; a++)
                            {
                                cv.DeleteCode(cv.GetCodeValue(0));
                            }
                            for (int l = 0; l < table.Rows.Count; l++)
                            {
                                if (table.Rows[l][sDscribFieldName].ToString() != "")
                                {
                                    if (table.Rows[l].IsNull(sEmunFieldName))
                                        continue;
                                    cv.AddCode(table.Rows[l][sEmunFieldName], table.Rows[l][sDscribFieldName].ToString());
                                }
                            }
                            ds.ModifyDomain(cv);
                        }
                    }
                }
                MessageBox.Show("保存成功！");
            }
            catch (COMException comEx)
            {
                MessageBox.Show(comEx.Message);
                this.DialogResult = DialogResult.None;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.DialogResult = DialogResult.None;
            }
            finally
            {
                if (cv != null)
                {
                    //Marshal.ReleaseComObject(cv);
                    cv = null;
                }
                if (rd != null)
                {
                    //Marshal.ReleaseComObject(rd);
                    rd = null;
                }
                if (domain != null)
                {
                    //Marshal.ReleaseComObject(domain);
                    domain = null;
                }
            }
        }

        private void dgv_DomainAttr_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                DataGridViewRow curRow = this.dgv_DomainAttr.CurrentRow;
                if(curRow != null)
                {
                    if (curRow.Cells["name"].Value != null && !string.IsNullOrEmpty(curRow.Cells["name"].Value.ToString()))
                    {
                        bool bNew = (bool)curRow.Cells["IsNew"].Value;
                        if (!bNew)
                        {
                            deletedomains.Add(curRow.Cells["name"].Value.ToString());
                        }
                    }
                    this.dgv_DomainValue.DataSource = null;
                    this.dgv_DomainAttr.Rows.Remove(curRow);
                }
            }
        }

        private void dgv_DomainValue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataGridViewRow row = this.dgv_DomainValue.CurrentRow;
                if (row != null)
                {
                    string s = this.dgv_DomainValue.Columns[0].Name;
                    if (s.Equals(sEmunFieldName))
                    {
                        this.dgv_DomainValue.Rows.Remove(row);
                    }
                }
            }
        }
    }
}

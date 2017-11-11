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
    public partial class FieldsDlg : Form
    {
        IDataSource ds;
        private IFeatureClass fc;

        public FieldsDlg(IFeatureClass pFc)
        {
            InitializeComponent();
            fc = pFc;
            ds = fc.DataSource;

            DataGridViewComboBoxColumn comColumn = new DataGridViewComboBoxColumn();
            comColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            comColumn.HeaderText = "域";
            comColumn.Items.Add("");
            comColumn.Items.AddRange(ds.GetDomainNames());
            comColumn.Name = "fielddomain";
            comColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgv_FieldInfo.Columns.Add(comColumn);

            this.LoadFcFieldInfo();
        }

        private void LoadFcFieldInfo()
        {
            IFieldInfoCollection fieldinfos = fc.GetFields();
            for (int i = 0; i < fieldinfos.Count; i++)
            {
                IFieldInfo fieldinfo = fieldinfos.Get(i);
                if (null == fieldinfo)
                    continue;
                if (fieldinfo.FieldType == gviFieldType.gviFieldGeometry || fieldinfo.FieldType == gviFieldType.gviFieldFID)
                    continue;
                int iRowIndex = -1;
                if (fieldinfo.Domain != null)
                {
                    iRowIndex = this.dgv_FieldInfo.Rows.Add(new object[] { fieldinfo.Name, ConvertFieldTypeByString(fieldinfo.FieldType), fieldinfo.Domain.Name });
                }
                else
                    iRowIndex = this.dgv_FieldInfo.Rows.Add(new object[] { fieldinfo.Name, ConvertFieldTypeByString(fieldinfo.FieldType), null });
                this.dgv_FieldInfo.Rows[iRowIndex].Tag = fieldinfo;
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

        private void dgv_FieldInfo_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow curRow = this.dgv_FieldInfo.Rows[e.RowIndex];
                if (e.ColumnIndex == curRow.Cells["fieldname"].ColumnIndex || e.ColumnIndex == curRow.Cells["fieldtype"].ColumnIndex)
                {
                    curRow.Cells[e.ColumnIndex].ReadOnly = true;
                }
                if (e.ColumnIndex == curRow.Cells["fielddomain"].ColumnIndex)
                {
                    //if ((curRow.Tag as IFieldInfo).DomainFixed)
                    //    curRow.Cells[e.ColumnIndex].ReadOnly = true;

                    IDomain d = null;
                    IRangeDomain rd = null;
                    ICodedValueDomain cv = null;
                    List<string> itemList = new List<string>();
                    itemList.Add("");
                    gviFieldType curft = (curRow.Tag as IFieldInfo).FieldType;
                    try
                    {
                        string[] domainList = ds.GetDomainNames();
                        if (domainList != null)
                        {
                            for (int l = 0; l < domainList.Length; l++)
                            {
                                d = ds.GetDomainByName(domainList[l].ToString());
                                if (d == null) continue;
                                if (d.FieldType == curft)
                                    itemList.Add(d.Name);
                            }
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
                    (curRow.Cells[e.ColumnIndex] as DataGridViewComboBoxCell).Items.Clear();
                    (curRow.Cells[e.ColumnIndex] as DataGridViewComboBoxCell).Items.AddRange(itemList.ToArray());
                }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            IDomain d = null;
            try
            {
                fc.LockType = gviLockType.gviLockExclusiveSchema;
                for (int i = 0; i < this.dgv_FieldInfo.Rows.Count; i++)
                {
                    DataGridViewRow myRow = this.dgv_FieldInfo.Rows[i];
                    if (myRow.Tag == null)
                        continue;
                    IFieldInfo field = myRow.Tag as IFieldInfo;
                    //if (field.DomainFixed)
                    //    continue;
                    if (myRow.Cells["fielddomain"].Value != null && !string.IsNullOrEmpty(myRow.Cells["fielddomain"].Value.ToString()))
                    {
                        d = ds.GetDomainByName(myRow.Cells["fielddomain"].Value.ToString());
                        field.Domain = d;
                        fc.ModifyField(field);
                    }
                    else
                    {
                        if (field.Domain != null)
                        {
                            field.Domain = null;
                            fc.ModifyField(field);
                        }
                    }
                    
                }
                fc.LockType = gviLockType.gviLockSharedSchema;
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
                if (d != null)
                {
                    //Marshal.ReleaseComObject(d);
                    d = null;
                }
            }
        }
    }
}

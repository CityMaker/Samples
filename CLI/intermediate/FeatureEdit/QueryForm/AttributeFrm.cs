using System;
using System.Data;
using System.Windows.Forms;
using Gvitech.CityMaker.FdeCore;
using System.Runtime.InteropServices;

namespace FeatureEdit
{
    public partial class AttributeFrm : Form
    {
        DataTable AttriTable = null;
        string FilterWhereClause = "";
        FCConnectionInfo Info = null;

        IDataSourceFactory dsFactory = null;

        public AttributeFrm(DataTable dt, FCConnectionInfo info, string filterWhereClause)
        {
            InitializeComponent();

            AttriTable = dt;
            Info = info;
            FilterWhereClause = filterWhereClause;
        }

        private void AttributeFrm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = AttriTable;
            if (FilterWhereClause.Equals(""))
                this.Text = "Attributes of " + Info.featureclassName + "  [Total records: " + AttriTable.Rows.Count.ToString() + "]";
            else
                this.Text = "Attributes of " + Info.featureclassName + "  [Total records: " + AttriTable.Rows.Count.ToString() + "]" + "  Filter: " + FilterWhereClause;

            this.dataGridView1.Columns[0].ReadOnly = true;
            this.dataGridView1.Columns["GroupName"].ReadOnly = true;
        }

        private void toolStripButtonEditFeature_Click(object sender, EventArgs e)
        {
            this.dataGridView1.EndEdit();  //强制提交
            if (dsFactory == null)
                dsFactory = new DataSourceFactory();

            IDataSource ds = null;
            IFeatureDataSet dataset = null;
            IFeatureClass fc = null;
            IFdeCursor cursor = null;
            IFieldInfo field = null;
            try
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    ds = dsFactory.OpenDataSource(Info.ci);
                    dataset = ds.OpenFeatureDataset(Info.datasetName);
                    fc = dataset.OpenFeatureClass(Info.featureclassName);

                    // 比较是否修改了记录
                    int oid = int.Parse(dataGridView1.SelectedRows[0].Cells["oid"].Value.ToString());
                    IRowBuffer fdeRow = fc.GetRow(oid);

                    bool isChanged = false;
                    for (int i = 0; i < AttriTable.Columns.Count; ++i)
                    {
                        string strColName = AttriTable.Columns[i].ColumnName;
                        int nPos = fdeRow.FieldIndex(strColName);
                        if (nPos != -1)
                            field = fdeRow.Fields.Get(nPos);
                        if (nPos != -1 && strColName != "oid" && strColName != "GroupName" && field.FieldType != gviFieldType.gviFieldGeometry)
                        {
                            if (fdeRow.GetValue(nPos) == null || (!dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[i].EditedFormattedValue.Equals(fdeRow.GetValue(nPos).ToString())))
                            {
                                isChanged = true;
                                break;
                            }
                        }
                    }
                    if (!isChanged)
                    {
                        MessageBox.Show("该条记录无改动");
                        return;
                    }

                    for (int j = 0; j < AttriTable.Columns.Count; ++j)
                    {
                        string strColName = AttriTable.Columns[j].ColumnName;
                        int nPos = fdeRow.FieldIndex(strColName);
                        if (nPos != -1)
                            field = fdeRow.Fields.Get(nPos);
                        if (nPos != -1 && strColName != "oid" && strColName != "GroupName" && field.FieldType != gviFieldType.gviFieldGeometry)
                        {
                            fdeRow.SetValue(nPos, dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[j].EditedFormattedValue);   //插入字段值
                        }
                    }

                    // 修改数据库中记录
                    IRowBufferCollection col = new RowBufferCollection();
                    col.Add(fdeRow);
                    fc.UpdateRows(col, false);
                    MessageBox.Show("修改成功");
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            finally
            {
                if (ds != null)
                {
                    //Marshal.ReleaseComObject(ds);
                    ds = null;
                }
                if (dataset != null)
                {
                    //Marshal.ReleaseComObject(dataset);
                    dataset = null;
                }
                if (fc != null)
                {
                    //Marshal.ReleaseComObject(fc);
                    fc = null;
                }
                if (cursor != null)
                {
                    //Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
            }
        }

    }
}

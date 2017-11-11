using System;
using System.Data;
using System.Windows.Forms;
using Gvitech.CityMaker.FdeCore;
using System.Runtime.InteropServices;

namespace FeatureCreateAndDelete
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
            if(FilterWhereClause.Equals(""))
                this.Text = "Attributes of " + Info.featureclassName + "  [Total records: " + AttriTable.Rows.Count.ToString() + "]";
            else
                this.Text = "Attributes of " + Info.featureclassName + "  [Total records: " + AttriTable.Rows.Count.ToString() + "]" + "  Filter: " + FilterWhereClause;
        }

        /// <summary>
        /// 在要素类里删除要素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonDeleteFeature_Click(object sender, EventArgs e)
        {
            if (dsFactory == null)
                dsFactory = new DataSourceFactory();

            IDataSource ds = null;
            IFeatureDataSet dataset = null;
            IFeatureClass fc = null;
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("该操作无法恢复，请确定是否删除所选择的要素？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        ds = dsFactory.OpenDataSource(Info.ci);
                        dataset = ds.OpenFeatureDataset(Info.datasetName);
                        fc = dataset.OpenFeatureClass(Info.featureclassName);

                        int oid = int.Parse(dataGridView1.SelectedRows[0].Cells["oid"].Value.ToString());

                        // 从数据库中删除
                        IQueryFilter filter = new QueryFilter();
                        filter.WhereClause = "oid = " + oid;
                        int nRet = fc.Delete(filter);
                        MessageBox.Show("成功删除" + nRet + "条记录");

                        // 从界面DataTable中删除
                        int index = dataGridView1.SelectedRows[0].Index;
                        AttriTable.Rows.RemoveAt(index);
                        this.dataGridView1.Refresh();
                        if (index < dataGridView1.RowCount)
                        {
                            this.dataGridView1.Rows[index].Selected = true;
                            this.dataGridView1.FirstDisplayedScrollingRowIndex = index;
                        }
                        else
                        {
                            this.dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = true;
                            this.dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                        }
                        

                        // 更新表头文字
                        if (FilterWhereClause.Equals(""))
                            this.Text = "Attributes of " + Info.featureclassName + "  [Total records: " + fc.GetCount(null).ToString() + "]";
                        else
                            this.Text = "Attributes of " + Info.featureclassName + "  [Total records: " + AttriTable.Rows.Count.ToString() + "]" + "  Filter: " + FilterWhereClause;
                    }
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
            }
        }

        /// <summary>
        /// 在要素类里创建要素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonInsertFeature_Click(object sender, EventArgs e)
        {
            if (dsFactory == null)
                dsFactory = new DataSourceFactory();

            IDataSource ds = null;
            IFeatureDataSet dataset = null;
            IFeatureClass fc = null;
            IFdeCursor cursor = null;
            try
            {
                ds = dsFactory.OpenDataSource(Info.ci);
                dataset = ds.OpenFeatureDataset(Info.datasetName);
                fc = dataset.OpenFeatureClass(Info.featureclassName);
                               
                DataRow dr = AttriTable.NewRow();;
                RowBufferFactory rbf = new RowBufferFactory();
                IRowBuffer rb = rbf.CreateRowBuffer(fc.GetFields());

                // 构造待插入的数据：暂时用最后一行的数据进行插入
                int lastOid = int.Parse(dataGridView1.Rows[AttriTable.Rows.Count - 1].Cells["oid"].Value.ToString());
                IRowBuffer lastRow = fc.GetRow(lastOid);
                for (int i = 0; i < fc.GetFields().Count; ++i)
                {
                    string strColName = fc.GetFields().Get(i).Name;
                    int nPos = rb.FieldIndex(strColName);
                    if (fc.GetFields().Get(i).Name != "oid")
                    {
                        rb.SetValue(nPos, lastRow.GetValue(nPos));   //插入字段值
                    }
                }
                // 构造界面显示数据
                for (int j = 0; j < AttriTable.Columns.Count; ++j)
                {
                    string strColName = AttriTable.Columns[j].ColumnName;
                    int nPos = rb.FieldIndex(strColName);
                    if (nPos != -1 && strColName != "oid")
                    {
                        if (lastRow.GetValue(nPos) != null)
                            dr[j] = lastRow.GetValue(nPos).ToString();
                    }

                    if ("GroupName" == strColName)
                        dr[j] = this.dataGridView1.Rows[AttriTable.Rows.Count - 1].Cells["GroupName"].Value.ToString();
                }

                // 往数据库中增加
                cursor = fc.Insert();
                cursor.InsertRow(rb);

                // 往界面DataTable上增加    
                dr["oid"] = cursor.LastInsertId;  //获取插入后oid值在界面显示
                AttriTable.Rows.Add(dr);
                this.dataGridView1.Refresh();
                this.dataGridView1.Rows[AttriTable.Rows.Count - 1].Selected = true;
                this.dataGridView1.FirstDisplayedScrollingRowIndex = AttriTable.Rows.Count - 1;               
                MessageBox.Show("增加记录成功");

                // 更新表头文字
                if (FilterWhereClause.Equals(""))
                    this.Text = "Attributes of " + Info.featureclassName + "  [Total records: " + fc.GetCount(null).ToString() + "]";
                else
                    this.Text = "Attributes of " + Info.featureclassName + "  [Total records: " + AttriTable.Rows.Count.ToString() + "]" + "  Filter: " + FilterWhereClause;

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

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
using Gvitech.CityMaker.FdeGeometry;
using System.Globalization;

namespace Domain
{
    public partial class FeatureAttrEditDlg : Form
    {
        public IFeatureClass fc = null;
        public int fid = -1;

        public FeatureAttrEditDlg(IFeatureClass pFc,int iFeatureId)
        {
            InitializeComponent();
            fc = pFc;
            fid = iFeatureId;
            InitialView();
        }

        private void InitialView()
        {
            try
            {
                IRowBuffer myRow = fc.GetRow(fid);
                IFieldInfoCollection fieldinfos = fc.GetFields();
                for (int i = 0; i < fieldinfos.Count; i++)
                {
                    int iRowIndex = -1;
                    IFieldInfo fieldinfo = fieldinfos.Get(i);
                    if (null == fieldinfo)
                        continue;
                    if (fieldinfo.FieldType == gviFieldType.gviFieldGeometry)
                    {
                        if (myRow.IsNull(i))
                        {
                            iRowIndex = this.dgv_FieldValue.Rows.Add(new object[] { fieldinfo.Name, null });
                            continue;
                        }
                        IGeometry geo = myRow.GetValue(i) as IGeometry;
                        string geoValue = null;
                        if(geo != null)
                        {
                            switch (geo.GeometryType)
                            {
                                case gviGeometryType.gviGeometryModelPoint:
                                    geoValue = "ModelPoint";
                                    break;
                                case gviGeometryType.gviGeometryPoint:
                                    geoValue = "Point";
                                    break;
                                case gviGeometryType.gviGeometryMultiPoint:
                                    geoValue = "MultiPoint";
                                    break;
                                case gviGeometryType.gviGeometryPolyline:
                                    geoValue = "Polyline";
                                    break;
                                case gviGeometryType.gviGeometryMultiPolyline:
                                    geoValue = "MultiPolyline";
                                    break;
                                case gviGeometryType.gviGeometryPolygon:
                                    geoValue = "Polygon";
                                    break;
                                case gviGeometryType.gviGeometryMultiPolygon:
                                    geoValue = "MultiPolygon";
                                    break;
                            }
                        }
                        iRowIndex = this.dgv_FieldValue.Rows.Add(new object[] { fieldinfo.Name, geoValue });
                    }
                    else if (fieldinfo.FieldType == gviFieldType.gviFieldFID)
                    {
                        iRowIndex = this.dgv_FieldValue.Rows.Add(new object[] { fieldinfo.Name, myRow.GetValue(i) });
                    }
                    else
                    {
                        IDomain fieldDomain = fieldinfo.Domain;
                        if(fieldDomain != null && fieldDomain.DomainType == gviDomainType.gviDomainCodedValue)
                        {
                            DataGridViewComboBoxCell comCell = new DataGridViewComboBoxCell();
                            comCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                            comCell.Items.Clear();
                            comCell.Items.Add("");
                            Dictionary<object ,string> codeValue = this.GetDomainCodeValuePair(fieldDomain);
                            comCell.Items.AddRange(codeValue.Values.ToArray());
                            iRowIndex = this.dgv_FieldValue.Rows.Add();
                            DataGridViewRow myViewRow = this.dgv_FieldValue.Rows[iRowIndex];
                            myViewRow.Cells[1] = comCell;
                            object oCodeValue = myRow.GetValue(i);
                            if (codeValue.ContainsKey(oCodeValue))
                                myViewRow.SetValues(new object[] { fieldinfo.Name, codeValue[oCodeValue] });
                            else
                                myViewRow.SetValues(new object[] { fieldinfo.Name, oCodeValue });
                        }
                        else
                            iRowIndex = this.dgv_FieldValue.Rows.Add(new object[] { fieldinfo.Name, myRow.GetValue(i) });
                    }
                    this.dgv_FieldValue.Rows[iRowIndex].Tag = new EditField(fieldinfo,i);
                }
                this.dgv_FieldValue.Tag = myRow;
            }
            catch (COMException comEx)
            {
                MessageBox.Show(comEx.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        private Dictionary<object,string > GetDomainCodeValuePair(IDomain pDomain)
        {
            Dictionary<object, string> myDic = new Dictionary<object, string>();
            ICodedValueDomain cv = pDomain as ICodedValueDomain;
            for (int i = 0; i < cv.CodeCount; i++)
            {
                myDic.Add(cv.GetCodeValue(i), cv.GetCodeName(i));
            }
            return myDic;
        }

        private Dictionary<string, object> GetDomainNameValuePair(IDomain pDomain)
        {
            Dictionary<string, object> myDic = new Dictionary<string, object>();
            ICodedValueDomain cv = pDomain as ICodedValueDomain;
            for (int i = 0; i < cv.CodeCount; i++)
            {
                myDic.Add(cv.GetCodeName(i), cv.GetCodeValue(i));
            }
            return myDic;
        }

        private void dgv_FieldValue_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow myViewRow = this.dgv_FieldValue.CurrentRow;
                if (myViewRow == null)
                    return;
                if (e.ColumnIndex == myViewRow.Cells["fieldname"].ColumnIndex)
                    myViewRow.Cells[e.ColumnIndex].ReadOnly = true;
                else
                {
                    object oRowTag = myViewRow.Tag;
                    if (oRowTag == null)
                        return;
                    IFieldInfo myField = (oRowTag as EditField).fieldInfo;
                    if (myField.FieldType == gviFieldType.gviFieldGeometry || myField.FieldType == gviFieldType.gviFieldFID || myField.FieldType == gviFieldType.gviFieldUUID || myField.FieldType == gviFieldType.gviFieldBlob)
                        myViewRow.Cells[e.ColumnIndex].ReadOnly = true;
                }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                IRowBuffer myRowBuffer = this.dgv_FieldValue.Tag as IRowBuffer;
                for (int i = 0; i < this.dgv_FieldValue.Rows.Count; i++)
                {
                    DataGridViewRow myViewRow = this.dgv_FieldValue.Rows[i];
                    if (myViewRow.Tag == null)
                        continue;
                    EditField editfield = myViewRow.Tag as EditField;
                    if (myViewRow.Cells["fieldvalue"].Value == null || string.IsNullOrEmpty(myViewRow.Cells["fieldvalue"].Value.ToString()))
                    {
                        if (editfield.fieldInfo.Nullable)
                        {
                            myRowBuffer.SetNull(editfield.fieldIndex);
                        }
                    }
                    else
                    {
                        IDomain dm = editfield.fieldInfo.Domain;
                        if (dm == null || dm.DomainType == gviDomainType.gviDomainRange)
                            myRowBuffer.SetValue(editfield.fieldIndex, myViewRow.Cells["fieldvalue"].Value);
                        else
                        {
                            myRowBuffer.SetValue(editfield.fieldIndex, this.GetDomainNameValuePair(dm)[myViewRow.Cells["fieldvalue"].Value.ToString()]);
                        }
                    }
                }
                fc.Store(myRowBuffer);
                MessageBox.Show("保存编辑成功！");
                (this.Owner as MainForm).UnhighlightRcFeature(fc, fid);
                this.Close();
            }
            catch
            {
                MessageBox.Show("保存编辑失败！");
            }
        }

        private void dgv_FieldValue_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow myViewRow = this.dgv_FieldValue.CurrentRow;
                if (myViewRow == null)
                    return;
                if (e.ColumnIndex == myViewRow.Cells["fieldvalue"].ColumnIndex)
                {
                    object oRowTag = myViewRow.Tag;
                    if (oRowTag == null)
                        return;
                    EditField ef = oRowTag as EditField;
                    IFieldInfo myField = ef.fieldInfo;
                    IDomain fieldDomain = myField.Domain;
                    if (fieldDomain == null || fieldDomain.DomainType != gviDomainType.gviDomainRange || myViewRow.Cells[e.ColumnIndex].Value == null)
                        return;
                    IRangeDomain rdm = fieldDomain as IRangeDomain;
                    decimal minValue = decimal.Parse(rdm.MinValue.ToString(), NumberStyles.Any);
                    decimal maxValue = decimal.Parse(rdm.MaxValue.ToString(), NumberStyles.Any);

                    decimal newValue = decimal.Parse(myViewRow.Cells[e.ColumnIndex].Value.ToString(), NumberStyles.Any);
                    if (newValue < minValue || newValue > maxValue)
                    {
                        MessageBox.Show("值超出域范围！");
                        myViewRow.Cells[e.ColumnIndex].Value = (this.dgv_FieldValue.Tag as IRowBuffer).GetValue(ef.fieldIndex);
                    }
                }
            }
        }
    }

    public class EditField
    {
        public IFieldInfo fieldInfo = null;

        public int fieldIndex = -1;

        public EditField(IFieldInfo fi, int findex)
        {
            fieldInfo = fi;
            fieldIndex = findex;
        }
    }
}

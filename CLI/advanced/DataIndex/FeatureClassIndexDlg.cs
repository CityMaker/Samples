using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gvitech.CityMaker.FdeCore;
using System.Collections;

namespace DataIndex
{
    public partial class FeatureClassIndexDlg : Form
    {
        private IFeatureClass featureCls = null;

        private IFieldInfoCollection fcFields = null;

        private List<string> AttrIndexList = new List<string>();

        private List<string> DelAttrIndexList = new List<string>();

        private Hashtable GridIndexHash = null;

        private Hashtable RenderIndexHash = null;

        private List<string> RegRenderIndexFields = new List<string>();

        private Hashtable RegRenderIndexHash = new Hashtable();

        public FeatureClassIndexDlg(IFeatureClass pFc)
        {
            InitializeComponent();
            featureCls = pFc;
            fcFields = pFc.GetFields();
            featureCls.LockType = gviLockType.gviLockExclusiveSchema;

            List<string> geoFields = new List<string>();
            for (int l = 0; l < fcFields.Count; l++)
            {
                IFieldInfo myField = fcFields.Get(l);
                if (myField != null && myField.FieldType == gviFieldType.gviFieldGeometry)
                    geoFields.Add(myField.Name);
            }

            //读取FC的索引信息
            //属性索引
            IDbIndexInfoCollection DbIndexInfos = featureCls.GetDbIndexInfos();
            if (DbIndexInfos != null)
            {
                for (int k = 0; k < DbIndexInfos.Count; k++)
                {
                    IDbIndexInfo DbIndex = DbIndexInfos.Get(k);
                    if (DbIndex.FieldCount == 1)
                    {
                        string getName = DbIndex.GetFieldName(0);
                        int ind = fcFields.IndexOf(getName);
                        if (ind >= 0)
                        {
                            IFieldInfo unVaildField = fcFields.Get(ind);
                            if (unVaildField.FieldType == gviFieldType.gviFieldFID || unVaildField.FieldType == gviFieldType.gviFieldGeometry || unVaildField.FieldType == gviFieldType.gviFieldBlob)
                                continue;
                        }
                        else
                            continue;
                    }
                    AttrIndexList.Add(DbIndex.Name);
                    this.lb_AttrIndex.Items.Add(new myAttrIndex(DbIndex,false));
                }
            }

            //空间索引
            IIndexInfoCollection GridIndexInfos = featureCls.GetSpatialIndexInfos();
            if (GridIndexInfos != null)
            {
                GridIndexHash = new Hashtable(GridIndexInfos.Count);
                for (int k = 0; k < GridIndexInfos.Count; k++)
                {
                    IGridIndexInfo GridIndex = GridIndexInfos.Get(k) as IGridIndexInfo;
                    if (GridIndex != null)
                    {
                        GridIndexHash.Add(GridIndex.GeoColumnName, GridIndex);
                        CheckIndexEdit GridIndexItem = new CheckIndexEdit(GridIndex.GeoColumnName);
                        GridIndexItem.ExistIndex = new IndexAboutGeometry(GridIndex.L1, GridIndex.L2, GridIndex.L3,true);
                        GridIndexItem.NewIndex = new IndexAboutGeometry(GridIndex.L1, GridIndex.L2, GridIndex.L3,true);
                        this.lb_SpatialIndexGeo.Items.Add(GridIndexItem);
                    }
                }

                foreach (string sGeoColumn in geoFields)
                {
                    if (GridIndexHash.ContainsKey(sGeoColumn))
                        continue;
                    CheckIndexEdit GridIndexItem = new CheckIndexEdit(sGeoColumn);
                    GridIndexItem.ExistIndex = null;
                    GridIndexItem.NewIndex = new IndexAboutGeometry(0, 0, 0,false);
                    this.lb_SpatialIndexGeo.Items.Add(GridIndexItem);
                }
            }

            //渲染索引
            IIndexInfoCollection RenderIndexInfos = featureCls.GetRenderIndexInfos();
            if (RenderIndexInfos != null)
            {
                RenderIndexHash = new Hashtable(RenderIndexInfos.Count);
                for (int k = 0; k < RenderIndexInfos.Count; k++)
                {
                    IRenderIndexInfo RenderIndex = RenderIndexInfos.Get(k) as IRenderIndexInfo;
                    if (RenderIndex != null)
                    {
                        RenderIndexHash.Add(RenderIndex.GeoColumnName, RenderIndex);
                        CheckIndexEdit RenderIndexItem = new CheckIndexEdit(RenderIndex.GeoColumnName);
                        RenderIndexItem.ExistIndex = new IndexAboutGeometry(RenderIndex.L1, 0, 0,true);
                        RenderIndexItem.NewIndex = new IndexAboutGeometry(RenderIndex.L1, 0, 0,true);
                        this.lb_RenderIndexGeo.Items.Add(RenderIndexItem);
                    }
                }

                foreach (string sGeoColumn in geoFields)
                {
                    if (RenderIndexHash.ContainsKey(sGeoColumn))
                        continue;
                    CheckIndexEdit RenderIndexItem = new CheckIndexEdit(sGeoColumn);
                    RenderIndexItem.ExistIndex = null;
                    RenderIndexItem.NewIndex = new IndexAboutGeometry(0, 0, 0,false);
                    this.lb_RenderIndexGeo.Items.Add(RenderIndexItem);
                }
                
                //填充渲染索引的ListBoxControl控件，一个是lbc_FieldsAvailable，另一个是lbc_FieldsSelected
                for (int k = 0; k < fcFields.Count; k++)
                {
                    IFieldInfo fcField = fcFields.Get(k);
                    if (fcField.FieldType == gviFieldType.gviFieldFID || fcField.FieldType == gviFieldType.gviFieldGeometry || fcField.FieldType == gviFieldType.gviFieldBlob)
                        continue;
                    if (fcField.RegisteredRenderIndex)
                    {
                        this.lb_RenderField.Items.Add(new myFieldInfo(fcField));
                        RegRenderIndexFields.Add(fcField.Name);
                        RegRenderIndexHash.Add(fcField.Name, fcField);
                    }
                    else
                        this.lb_RenderAllField.Items.Add(new myFieldInfo(fcField));
                }
            }
        }

        private void lb_AttrIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lb_AttrIndexFields.Items.Clear();
            if (this.lb_AttrIndex.SelectedItems.Count > 0)
            {
                myAttrIndex myIndex = this.lb_AttrIndex.SelectedItems[0] as myAttrIndex;
                if (myIndex != null)
                {
                    for (int i = 0; i < myIndex.dbIndex.FieldCount; i++)
                    {
                        this.lb_AttrIndexFields.Items.Add(myIndex.dbIndex.GetFieldName(i));
                    }

                }
            }
        }

        private void btn_AttrAdd_Click(object sender, EventArgs e)
        {
            List<string> existIndexName = new List<string>();
            for (int i = 0; i < this.lb_AttrIndex.Items.Count; i++)
            {
                existIndexName.Add(this.lb_AttrIndex.Items[i].ToString());
            }
            CreateDBIndexDlg myDlg = new CreateDBIndexDlg(fcFields, existIndexName);
            if(myDlg.ShowDialog() == DialogResult.OK)
            {
                IDbIndexInfo myNewDBIndex = new DbIndexInfo();
                myNewDBIndex.Name = myDlg.sDBIndexName;
                foreach (string sField in myDlg.listDBIndexFields)
                {
                    myNewDBIndex.AppendFieldDefine(sField, true);
                }
                int iAddIndex = this.lb_AttrIndex.Items.Add(new myAttrIndex(myNewDBIndex, true));
                this.lb_AttrIndex.SelectedIndex = iAddIndex;
            }
        }

        private void btn_AttrDelete_Click(object sender, EventArgs e)
        {
            if (this.lb_AttrIndex.SelectedItems.Count > 0)
            {
                myAttrIndex selectIndexItem = this.lb_AttrIndex.SelectedItems[0] as myAttrIndex;
                if (AttrIndexList.Contains(selectIndexItem.ToString()))
                    DelAttrIndexList.Add(selectIndexItem.ToString());
                this.lb_AttrIndex.Items.RemoveAt(this.lb_AttrIndex.SelectedIndices[0]);
                this.lb_AttrIndexFields.Items.Clear();
            }
        }

        private void lb_SpatialIndexGeo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lb_SpatialIndexGeo.SelectedItems.Count > 0)
            {
                CheckIndexEdit mySelectIndex = this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit;
                if (mySelectIndex != null)
                {
                    if (mySelectIndex.ExistIndex != null)
                    {
                        this.btn_SpatialAdd.Enabled = false;
                        this.btn_SpatialDelete.Enabled = true;
                    }
                    else
                    {
                        this.btn_SpatialAdd.Enabled = true;
                        this.btn_SpatialDelete.Enabled = false;
                    }
                    if (mySelectIndex.NewIndex != null && mySelectIndex.NewIndex.IsInitail)
                    {
                        this.SetGridIndexControl(mySelectIndex.GeoFieldName, mySelectIndex.NewIndex);
                        this.btn_SpatialCal.Enabled = true;
                    }
                    else
                        this.btn_SpatialCal.Enabled = false;
                }
            }
            else
                this.btn_SpatialCal.Enabled = false;
        }

        private void SetGridIndexControl(string sGeoFieldName,IndexAboutGeometry newGridIndex)
        {
            this.tb_SpatialGeoField.Text = sGeoFieldName;
            if (newGridIndex.L1 != 0)
                this.tb_L1.Text = newGridIndex.L1.ToString();
            else
                this.tb_L1.Text = "";
            if (newGridIndex.L2 != 0)
                this.tb_L2.Text = newGridIndex.L2.ToString();
            else
                this.tb_L2.Text = "";
            if (newGridIndex.L3 != 0)
                this.tb_L3.Text = newGridIndex.L3.ToString();
            else
                this.tb_L3.Text = "";
        }

        private void ClearGridIndexControl()
        {
            this.tb_SpatialGeoField.Text = "";
            this.tb_L1.Text = "";
            this.tb_L2.Text = "";
            this.tb_L3.Text = "";
        }

        private void lb_RenderIndexGeo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lb_RenderIndexGeo.SelectedItems.Count > 0)
            {
                CheckIndexEdit mySelectIndex = this.lb_RenderIndexGeo.SelectedItems[0] as CheckIndexEdit;
                if (mySelectIndex != null)
                {
                    if (mySelectIndex.ExistIndex != null)
                    {
                        this.btn_RenderAdd.Enabled = false;
                        this.btn_RenderDelete.Enabled = true;
                    }
                    else
                    {
                        this.btn_RenderAdd.Enabled = true;
                        this.btn_RenderDelete.Enabled = false;
                    }
                    if (mySelectIndex.NewIndex != null && mySelectIndex.NewIndex.IsInitail)
                    {
                        this.SetRenderIndexControl(mySelectIndex.GeoFieldName, mySelectIndex.NewIndex);
                        this.btn_RenderCal.Enabled = true;
                    }
                    else
                        this.btn_RenderCal.Enabled = false;
                }
            }
            else
                this.btn_RenderCal.Enabled = false;
        }

        private void SetRenderIndexControl(string sGeoFieldName, IndexAboutGeometry newGridIndex)
        {
            this.tb_RenderGeoField.Text = sGeoFieldName;
            if (newGridIndex.L1 != 0)
                this.tb_RenderL1.Text = newGridIndex.L1.ToString();
            else
                this.tb_RenderL1.Text = "";
        }

        private void btn_SpatialAdd_Click(object sender, EventArgs e)
        {
            if (this.lb_SpatialIndexGeo.SelectedItems.Count > 0)
            {
                CheckIndexEdit SelectGridIndexItem = this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit;
                SelectGridIndexItem.NewIndex.IsInitail = true;
                this.SetGridIndexControl(SelectGridIndexItem.GeoFieldName, SelectGridIndexItem.NewIndex);
                this.btn_SpatialAdd.Enabled = false;
                this.btn_SpatialCal.Enabled = true;
                this.btn_SpatialDelete.Enabled = true;
            }
        }

        private void btn_SpatialDelete_Click(object sender, EventArgs e)
        {
            if (this.lb_SpatialIndexGeo.SelectedItems.Count > 0 && (this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.IsInitail)
            {
                CheckIndexEdit SelectGridIndexItem = this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit;
                SelectGridIndexItem.ExistIndex = null;
                SelectGridIndexItem.NewIndex.IsInitail = false;
                SelectGridIndexItem.NewIndex.L1 = 0;
                SelectGridIndexItem.NewIndex.L2 = 0;
                SelectGridIndexItem.NewIndex.L3 = 0;
                this.ClearGridIndexControl();
                this.btn_SpatialAdd.Enabled = true;
                this.btn_SpatialCal.Enabled = false;
                this.btn_SpatialDelete.Enabled = false;
            }
        }

        private void btn_SpatialCal_Click(object sender, EventArgs e)
        {
            if (this.lb_SpatialIndexGeo.SelectedItems.Count > 0 && (this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.IsInitail)
            {
                CheckIndexEdit SelectGridIndexItem = this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit;
                IGridIndexInfo tempIndexInfo = featureCls.CalculateDefaultGridIndex(SelectGridIndexItem.GeoFieldName);
                SelectGridIndexItem.NewIndex.L1 = tempIndexInfo.L1;
                SelectGridIndexItem.NewIndex.L2 = tempIndexInfo.L2;
                SelectGridIndexItem.NewIndex.L3 = tempIndexInfo.L3;
                this.SetGridIndexControl(SelectGridIndexItem.GeoFieldName, SelectGridIndexItem.NewIndex);
            }
        }

        private void btn_RenderAdd_Click(object sender, EventArgs e)
        {
            if (this.lb_RenderIndexGeo.SelectedItems.Count > 0)
            {
                CheckIndexEdit SelectIndexItem = this.lb_RenderIndexGeo.SelectedItems[0] as CheckIndexEdit;
                SelectIndexItem.NewIndex.IsInitail = true;
                this.SetRenderIndexControl(SelectIndexItem.GeoFieldName, SelectIndexItem.NewIndex);
                this.btn_RenderAdd.Enabled = false;
                this.btn_RenderDelete.Enabled = true;
                this.btn_RenderCal.Enabled = true;
            }
        }

        private void btn_RenderDelete_Click(object sender, EventArgs e)
        {
            if (this.lb_RenderIndexGeo.SelectedItems.Count > 0)
            {
                CheckIndexEdit SelectIndexItem = this.lb_RenderIndexGeo.SelectedItems[0] as CheckIndexEdit;
                SelectIndexItem.ExistIndex = null;
                SelectIndexItem.NewIndex.IsInitail = false;
                SelectIndexItem.NewIndex.L1 = 0;
                this.tb_RenderGeoField.Text = "";
                this.tb_RenderL1.Text = "";
                this.btn_RenderAdd.Enabled = true;
                this.btn_RenderDelete.Enabled = false;
                this.btn_RenderCal.Enabled = false;
            }
        }

        private void btn_RenderCal_Click(object sender, EventArgs e)
        {
            if (this.lb_RenderIndexGeo.SelectedItems.Count > 0 && (this.lb_RenderIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.IsInitail)
            {
                CheckIndexEdit SelectIndexItem = this.lb_RenderIndexGeo.SelectedItems[0] as CheckIndexEdit;
                IRenderIndexInfo tempIndexInfo = featureCls.CalculateDefaultRenderIndex(SelectIndexItem.GeoFieldName);
                SelectIndexItem.NewIndex.L1 = tempIndexInfo.L1;
                this.SetRenderIndexControl(SelectIndexItem.GeoFieldName, SelectIndexItem.NewIndex);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                //属性索引
                foreach (string sDbIndexName in DelAttrIndexList)
                {
                    featureCls.DeleteDbIndex(sDbIndexName);
                    AttrIndexList.Remove(sDbIndexName);
                }
                for (int i = 0; i < this.lb_AttrIndex.Items.Count; i++)
                {
                    myAttrIndex oneIndexItem = this.lb_AttrIndex.Items[i] as myAttrIndex;
                    if (AttrIndexList.Contains(oneIndexItem.ToString()))
                        continue;
                    featureCls.AddDbIndex(oneIndexItem.dbIndex);
                }

                //空间索引
                for (int j = 0; j < this.lb_SpatialIndexGeo.Items.Count; j++)
                {
                    CheckIndexEdit oneIndexItem = this.lb_SpatialIndexGeo.Items[j] as CheckIndexEdit;
                    string sGeoCol = oneIndexItem.GeoFieldName;
                    if (oneIndexItem.ExistIndex == null && GridIndexHash.ContainsKey(sGeoCol))
                        featureCls.DeleteSpatialIndex(sGeoCol);
                    if (oneIndexItem.ExistIndex == null && oneIndexItem.NewIndex.IsInitail)
                    {
                        IGridIndexInfo NewIndexInfo = new GridIndexInfo();
                        NewIndexInfo.GeoColumnName = sGeoCol;
                        NewIndexInfo.L1 = oneIndexItem.NewIndex.L1;
                        NewIndexInfo.L2 = oneIndexItem.NewIndex.L2;
                        NewIndexInfo.L3 = oneIndexItem.NewIndex.L3;
                        featureCls.AddSpatialIndex(NewIndexInfo as IIndexInfo);
                    }
                    if (oneIndexItem.ExistIndex != null && !oneIndexItem.IsSameIndex())
                    {
                        featureCls.DeleteSpatialIndex(sGeoCol);
                        IGridIndexInfo NewIndexInfo = new GridIndexInfo();
                        NewIndexInfo.GeoColumnName = sGeoCol;
                        NewIndexInfo.L1 = oneIndexItem.NewIndex.L1;
                        NewIndexInfo.L2 = oneIndexItem.NewIndex.L2;
                        NewIndexInfo.L3 = oneIndexItem.NewIndex.L3;
                        featureCls.AddSpatialIndex(NewIndexInfo as IIndexInfo);
                    }
                }

                //渲染索引
                for (int k = 0; k < this.lb_RenderIndexGeo.Items.Count; k++)
                {
                    CheckIndexEdit oneIndexItem = this.lb_RenderIndexGeo.Items[k] as CheckIndexEdit;
                    string sGeoCol = oneIndexItem.GeoFieldName;
                    if (oneIndexItem.ExistIndex == null && RenderIndexHash.ContainsKey(sGeoCol))
                        featureCls.DeleteRenderIndex(sGeoCol);
                    if (oneIndexItem.ExistIndex == null && oneIndexItem.NewIndex.IsInitail)
                    {
                        IRenderIndexInfo NewIndexInfo = new RenderIndexInfo();
                        NewIndexInfo.GeoColumnName = sGeoCol;
                        NewIndexInfo.L1 = oneIndexItem.NewIndex.L1;
                        featureCls.AddRenderIndex(NewIndexInfo);
                    }
                    if (oneIndexItem.ExistIndex != null && !oneIndexItem.IsSameIndex())
                    {
                        featureCls.DeleteRenderIndex(sGeoCol);
                        IRenderIndexInfo NewIndexInfo = new RenderIndexInfo();
                        NewIndexInfo.GeoColumnName = sGeoCol;
                        NewIndexInfo.L1 = oneIndexItem.NewIndex.L1;
                        featureCls.AddRenderIndex(NewIndexInfo);
                    }
                }
                for (int l = 0; l < this.lb_RenderField.Items.Count; l++) 
                {
                    myFieldInfo oneField = this.lb_RenderField.Items[l] as myFieldInfo;
                    if (RegRenderIndexFields.Contains(oneField.ToString()))
                    {
                        RegRenderIndexFields.Remove(oneField.ToString());
                        continue;
                    }
                    else
                    {
                        oneField.fieldinfo.RegisteredRenderIndex = true;
                        featureCls.ModifyField(oneField.fieldinfo);
                    }
                }
                if (RegRenderIndexFields.Count > 0)
                {
                    foreach (string sFieldName in RegRenderIndexFields)
                    {
                        IFieldInfo delField = RegRenderIndexHash[sFieldName] as IFieldInfo;
                        delField.RegisteredRenderIndex = false;
                        featureCls.ModifyField(delField);
                    }
                }
                MessageBox.Show("保存成功！");
            }
            catch(Exception ex)
            {
                MessageBox.Show("保存失败！" + ex.ToString());
            }
            finally
            {
                featureCls.LockType = gviLockType.gviLockSharedSchema;
                this.Close();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            featureCls.LockType = gviLockType.gviLockSharedSchema;
            this.Close();
        }

        private void tb_L1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.lb_SpatialIndexGeo.SelectedItems.Count > 0 && (this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.IsInitail && !string.IsNullOrEmpty((sender as TextBox).Text.Trim()))
                {
                    (this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.L1 = Convert.ToDouble((sender as TextBox).Text.Trim());
                }
                else
                    (sender as TextBox).Text = "";
            }
            catch
            {
                double l1 = (this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.L1;
                if (l1 != 0)
                    (sender as TextBox).Text = l1.ToString();
                else
                    (sender as TextBox).Text = "";
            }
        }

        private void tb_L2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.lb_SpatialIndexGeo.SelectedItems.Count > 0 && (this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.IsInitail && !string.IsNullOrEmpty((sender as TextBox).Text.Trim()))
                {
                    (this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.L2 = Convert.ToDouble((sender as TextBox).Text.Trim());
                }
                else
                    (sender as TextBox).Text = "";
            }
            catch
            {
                double l2 = (this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.L2;
                if (l2 != 0)
                    (sender as TextBox).Text = l2.ToString();
                else
                    (sender as TextBox).Text = "";
            }
        }

        private void tb_L3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.lb_SpatialIndexGeo.SelectedItems.Count > 0 && (this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.IsInitail && !string.IsNullOrEmpty((sender as TextBox).Text.Trim()))
                {
                    (this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.L3 = Convert.ToDouble((sender as TextBox).Text.Trim());
                }
                else
                    (sender as TextBox).Text = "";
            }
            catch
            {
                double l3 = (this.lb_SpatialIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.L3;
                if (l3 != 0)
                    (sender as TextBox).Text = l3.ToString();
                else
                    (sender as TextBox).Text = "";
            }
        }

        private void tb_RenderL1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.lb_RenderIndexGeo.SelectedItems.Count > 0 && (this.lb_RenderIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.IsInitail && !string.IsNullOrEmpty((sender as TextBox).Text.Trim()))
                {
                    (this.lb_RenderIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.L1 = Convert.ToDouble((sender as TextBox).Text.Trim());
                }
                else
                    (sender as TextBox).Text = "";
            }
            catch
            {
                double l1 = (this.lb_RenderIndexGeo.SelectedItems[0] as CheckIndexEdit).NewIndex.L1;
                if (l1 != 0)
                    (sender as TextBox).Text = l1.ToString();
                else
                    (sender as TextBox).Text = "";
            }
        }

        private void btn_RenderFieldAdd_Click(object sender, EventArgs e)
        {
            if (this.lb_RenderAllField.SelectedItems.Count > 0)
            {
                myFieldInfo selectField = this.lb_RenderAllField.SelectedItems[0] as myFieldInfo;
                this.lb_RenderAllField.Items.RemoveAt(this.lb_RenderAllField.SelectedIndices[0]);
                this.lb_RenderField.Items.Add(selectField);
            }
        }

        private void btn_RenderFieldDelete_Click(object sender, EventArgs e)
        {
            if (this.lb_RenderField.SelectedItems.Count > 0)
            {
                myFieldInfo selectField = this.lb_RenderField.SelectedItems[0] as myFieldInfo;
                this.lb_RenderField.Items.RemoveAt(this.lb_RenderField.SelectedIndices[0]);
                this.lb_RenderAllField.Items.Add(selectField);
            }
        }
    }



    public class myAttrIndex
    {
        public IDbIndexInfo dbIndex = null;

        public bool IsNew = false;

        public myAttrIndex(IDbIndexInfo pDbIndex,bool bIsNew)
        {
            dbIndex = pDbIndex;
            IsNew = bIsNew;
        }

        public override string  ToString()
        {
            return dbIndex.Name;
        }
    }

    public class myFieldInfo
    {
        public IFieldInfo fieldinfo = null;

        public myFieldInfo(IFieldInfo pFieldInfo)
        {
            fieldinfo = pFieldInfo;
        }

        public override string ToString()
        {
            return fieldinfo.Name;
        }
    }

    public class IndexAboutGeometry
    {
        public double L1 = 0;

        public double L2 = 0;

        public double L3 = 0;

        public bool IsInitail = false;

        public IndexAboutGeometry()
        {
 
        }

        public IndexAboutGeometry(double dbL1, double dbL2, double dbL3,bool bIsInitail)
        {
            L1 = dbL1;
            L2 = dbL2;
            L3 = dbL3;
            IsInitail = bIsInitail;
        }

        public override bool Equals(object obj)
        {
            IndexAboutGeometry myIndex = obj as IndexAboutGeometry;
            if (myIndex.L1 == L1 && myIndex.L2 == L2 && myIndex.L3 == L3)
                return true;
            return false;
        }
    }

    public class CheckIndexEdit
    {
        public IndexAboutGeometry ExistIndex;

        public IndexAboutGeometry NewIndex;

        public string GeoFieldName;

        public CheckIndexEdit(string sGeoColumnName)
        {
            GeoFieldName = sGeoColumnName;
        }

        public bool IsSameIndex()
        {
            if (ExistIndex == null || NewIndex == null)
                return false;
            return ExistIndex == NewIndex;
        }

        public override string ToString()
        {
            return GeoFieldName;
        }
    }
}

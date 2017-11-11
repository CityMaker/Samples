using System;
using System.Windows.Forms;
using Gvitech.CityMaker.FdeCore;
using Gvitech;

namespace CatalogTreeCreateAndDelete
{
    public partial class OperateFieldInfoForm : Form
    {
        private bool isCreate = false;
        public IFieldInfo newFieldInfo = null;

        public OperateFieldInfoForm(IFieldInfo fieldInfo)
        {
            InitializeComponent();

            // 新建字段
            if (fieldInfo == null)
            {
                fieldInfo = new FieldInfo();
                isCreate = true;
            }

            {
                string[] row1 = new string[] { "Name", fieldInfo.Name };
                string[] row2 = new string[] { "FieldType", fieldInfo.FieldType.ToString() };
                string[] row3 = new string[] { "IsSystemField", fieldInfo.IsSystemField.ToString() };
                string[] row4 = new string[] { "Alias", fieldInfo.Alias };
                string[] row5 = null;
                if (fieldInfo.DefaultValue != null)
                    row5 = new string[] { "DefaultValue", fieldInfo.DefaultValue.ToString() };
                else
                    row5 = new string[] { "DefaultValue", "null" };
                string[] row6 = new string[] { "Editable", fieldInfo.Editable.ToString() };
                string[] row7 = new string[] { "Length", fieldInfo.Length.ToString() };
                string[] row8 = new string[] { "Nullable", fieldInfo.Nullable.ToString() };
                string[] row9 = new string[] { "RegisteredRenderIndex", fieldInfo.RegisteredRenderIndex.ToString() };
                string[] row10 = new string[] { "Precision", fieldInfo.Precision.ToString() };
                string[] row11 = new string[] { "Scale", fieldInfo.Scale.ToString() };
                string[] row12 = new string[] { "DomainFixed", fieldInfo.DomainFixed.ToString() };
                object[] rows = new object[] { row1, row2, row3, row4, row5, row6, row7, row8, row9, row10, row11, row12 };
                foreach (string[] rowArray in rows)
                {
                    this.dataGridView1.Rows.Add(rowArray);
                }
            }

            if (fieldInfo.GeometryDef != null)
            {
                string[] row1 = new string[] { "GeometryDef.GeometryColumnType", fieldInfo.GeometryDef.GeometryColumnType.ToString() };
                string[] row2 = new string[] { "GeometryDef.HasSpatialIndex", fieldInfo.GeometryDef.HasSpatialIndex.ToString() };
                string[] row3 = new string[] { "GeometryDef.HasM", fieldInfo.GeometryDef.HasM.ToString() };
                string[] row4 = new string[] { "GeometryDef.HasZ", fieldInfo.GeometryDef.HasZ.ToString() };
                string[] row5 = new string[] { "GeometryDef.MaxM", fieldInfo.GeometryDef.MaxM.ToString() };
                string[] row6 = new string[] { "GeometryDef.MinM", fieldInfo.GeometryDef.MinM.ToString() };
                string[] row7 = new string[] { "GeometryDef.AvgNumPoints", fieldInfo.GeometryDef.AvgNumPoints.ToString() };
                string[] row8 = new string[] { "GeometryDef.Envelope.Width", fieldInfo.GeometryDef.Envelope.Width.ToString() };
                string[] row9 = new string[] { "GeometryDef.Envelope.Height", fieldInfo.GeometryDef.Envelope.Height.ToString() };
                string[] row10 = new string[] { "GeometryDef.Envelope.Depth", fieldInfo.GeometryDef.Envelope.Depth.ToString() };
                string[] row11 = new string[] { "GeometryDef.Envelope.MinX", fieldInfo.GeometryDef.Envelope.MinX.ToString() };
                string[] row12 = new string[] { "GeometryDef.Envelope.MaxX", fieldInfo.GeometryDef.Envelope.MaxX.ToString() };
                string[] row13 = new string[] { "GeometryDef.Envelope.MinY", fieldInfo.GeometryDef.Envelope.MinY.ToString() };
                string[] row14 = new string[] { "GeometryDef.Envelope.MaxY", fieldInfo.GeometryDef.Envelope.MaxY.ToString() };
                string[] row15 = new string[] { "GeometryDef.Envelope.MinZ", fieldInfo.GeometryDef.Envelope.MinZ.ToString() };
                string[] row16 = new string[] { "GeometryDef.Envelope.MaxZ", fieldInfo.GeometryDef.Envelope.MaxZ.ToString() };
                object[] rows = new object[] { row1, row2, row3, row4, row5, row6, row7, row8, row9, row10, row11, row12, row13, row14, row15, row16 };
                foreach (string[] rowArray in rows)
                {
                    this.dataGridView1.Rows.Add(rowArray);
                }
            }

            if (isCreate)
            {
                IGeometryDef geoDefine = new GeometryDef();
                string[] row1 = new string[] { "GeometryDef.GeometryColumnType", geoDefine.GeometryColumnType.ToString() };
                string[] row2 = new string[] { "GeometryDef.HasSpatialIndex", geoDefine.HasSpatialIndex.ToString() };
                string[] row3 = new string[] { "GeometryDef.HasM", geoDefine.HasM.ToString() };
                string[] row4 = new string[] { "GeometryDef.HasZ", geoDefine.HasZ.ToString() };
                string[] row5 = new string[] { "GeometryDef.MaxM", geoDefine.MaxM.ToString() };
                string[] row6 = new string[] { "GeometryDef.MinM", geoDefine.MinM.ToString() };
                string[] row7 = new string[] { "GeometryDef.AvgNumPoints", geoDefine.AvgNumPoints.ToString() };
                string[] row8 = new string[] { "GeometryDef.Envelope.Width", null };
                string[] row9 = new string[] { "GeometryDef.Envelope.Height", null };
                string[] row10 = new string[] { "GeometryDef.Envelope.Depth", null };
                string[] row11 = new string[] { "GeometryDef.Envelope.MinX", null };
                string[] row12 = new string[] { "GeometryDef.Envelope.MaxX", null };
                string[] row13 = new string[] { "GeometryDef.Envelope.MinY", null };
                string[] row14 = new string[] { "GeometryDef.Envelope.MaxY", null };
                string[] row15 = new string[] { "GeometryDef.Envelope.MinZ", null };
                string[] row16 = new string[] { "GeometryDef.Envelope.MaxZ", null };
                object[] rows = new object[] { row1, row2, row3, row4, row5, row6, row7, row8, row9, row10, row11, row12, row13, row14, row15, row16 };
                foreach (string[] rowArray in rows)
                {
                    this.dataGridView1.Rows.Add(rowArray);
                }
            }

            if (fieldInfo.Domain != null)
            {
                string[] row1 = new string[] { "Domain.Name", fieldInfo.Domain.Name };
                string[] row2 = new string[] { "Domain.Description", fieldInfo.Domain.Description };
                string[] row3 = new string[] { "Domain.DomainType", fieldInfo.Domain.DomainType.ToString() };
                string[] row4 = new string[] { "Domain.FieldType", fieldInfo.Domain.FieldType.ToString() };
                string[] row5 = new string[] { "Domain.Owner", fieldInfo.Domain.Owner };
                object[] rows = new object[] { row1, row2, row3, row4, row5 };
                foreach (string[] rowArray in rows)
                {
                    this.dataGridView1.Rows.Add(rowArray);
                }
            }

            this.dataGridView1.Rows[2].ReadOnly = true;  //IsSystemField只读
            this.dataGridView1.Rows[11].ReadOnly = true; //DomainFixed只读
                       
            if (this.dataGridView1.Rows.Count > 12)
            {
                this.dataGridView1.Rows[13].ReadOnly = true; //HasSpatialIndex属性只读
                this.dataGridView1.Rows[19].ReadOnly = true; //envelope属性都是只读
                this.dataGridView1.Rows[20].ReadOnly = true;
                this.dataGridView1.Rows[21].ReadOnly = true;
                this.dataGridView1.Rows[22].ReadOnly = true;
                this.dataGridView1.Rows[23].ReadOnly = true;
                this.dataGridView1.Rows[24].ReadOnly = true;
                this.dataGridView1.Rows[25].ReadOnly = true;
                this.dataGridView1.Rows[26].ReadOnly = true;
                this.dataGridView1.Rows[27].ReadOnly = true;
            }

            if (this.dataGridView1.Rows.Count > 28)
            {
                this.dataGridView1.Rows[30].ReadOnly = true; //Domain.DomainType只读
                this.dataGridView1.Rows[31].ReadOnly = true; //Domain.FieldType只读
            }       
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string propertyName = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string propertyValue = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                switch (e.RowIndex)
                {
                    //case 2:
                    case 5:
                    case 7:
                    case 8:
                    //case 11:
                    //case 13:
                    case 14:
                    case 15:
                        {
                            EditForm edit = new EditForm(propertyValue.ToLower() == "true" ? true : false, propertyName);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = edit.checkBox1.Checked.ToString();
                            }
                        }
                        break;
                    case 1:
                        {
                            object[] items = new object[] { "", 
                                "gviFieldInt16",
                                "gviFieldInt32",
                                "gviFieldInt64",
                                "gviFieldFloat",
                                "gviFieldDouble",
                                "gviFieldString",
                                "gviFieldDate",
                                "gviFieldBlob",
                                "gviFieldFID",
                                "gviFieldUUID",
                                "gviFieldGeometry"};
                            int index = Utils.getIndexFromItems(items, propertyValue.Trim());
                            EditForm edit = new EditForm(items, index);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = edit.comboBox1.SelectedItem.ToString();
                            }
                        }
                        break;
                    case 12:
                        {
                            object[] items = new object[] { "", 
                                "gviGeometryColumnUnknown",
                                "gviGeometryColumnPoint",
                                "gviGeometryColumnModelPoint",
                                "gviGeometryColumnImagePoint",
                                "gviGeometryColumnMultiPoint",
                                "gviGeometryColumnCurve",
                                "gviGeometryColumnPolygon",
                                "gviGeometryColumnSolid",
                                "gviGeometryColumnSurface",
                                "gviGeometryColumnPointCloud",
                                "gviGeometryColumnCollection"};
                            int index = Utils.getIndexFromItems(items, propertyValue.Trim());
                            EditForm edit = new EditForm(items, index);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = edit.comboBox1.SelectedItem.ToString();
                            }
                        }
                        break;
                    case 6:
                    case 9:
                    case 10:
                    case 18:
                        {
                            int old = int.Parse(propertyValue);
                            EditForm edit = new EditForm(0, int.MaxValue, old);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = edit.numericUpDown1.Value.ToString();
                            }
                        }
                        break;
                    case 16:
                    case 17:                    
                    //case 22:
                    //case 23:
                    //case 24:
                    //case 25:
                    //case 26:
                    //case 27:
                        {
                            double old = double.Parse(propertyValue);
                            EditForm edit = new EditForm(double.MinValue, double.MaxValue, old);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = edit.numericUpDown1.Value.ToString();
                            }
                        }
                        break;
                    //case 19:
                    //case 20:
                    //case 21:
                    //    {
                    //        double old = double.Parse(propertyValue);
                    //        double min = double.Parse("0");
                    //        EditForm edit = new EditForm(min, old);
                    //        if (edit.ShowDialog() == DialogResult.OK)
                    //        {
                    //            this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = edit.numericUpDown1.Value.ToString();
                    //        }
                    //    }
                    //    break;
                    //case 30:
                    //    {
                    //        object[] items = new object[] { "", "gviDomainRange", "gviDomainCodedValue" };
                    //        int index = Utils.getIndexFromItems(items, propertyValue.Trim());
                    //        EditForm edit = new EditForm(items, index);
                    //        if (edit.ShowDialog() == DialogResult.OK)
                    //        {
                    //            this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = edit.comboBox1.SelectedItem.ToString();
                    //        }
                    //    }
                    //    break;
                    //case 31:
                    //    {
                    //        object[] items = new object[] { "",
                    //           "gviFieldUnknown",
                    //           "gviFieldInt16",
                    //           "gviFieldInt32",
                    //           "gviFieldInt64",
                    //           "gviFieldFloat",
                    //           "gviFieldDouble",
                    //           "gviFieldString",
                    //           "gviFieldDate",
                    //           "gviFieldBlob",
                    //           "gviFieldFID",
                    //           "gviFieldUUID",
                    //           "gviFieldGeometry"};
                    //        int index = Utils.getIndexFromItems(items, propertyValue.Trim());
                    //        EditForm edit = new EditForm(items, index);
                    //        if (edit.ShowDialog() == DialogResult.OK)
                    //        {
                    //            this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = edit.comboBox1.SelectedItem.ToString();
                    //        }
                    //    }
                    //    break;
                }
                this.btnOK.Focus();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            newFieldInfo = new FieldInfo();

            if (this.dataGridView1.Rows[0].Cells[1].Value.ToString() != "")
                newFieldInfo.Name = this.dataGridView1.Rows[0].Cells[1].Value.ToString();
            if (this.dataGridView1.Rows[1].Cells[1].Value.ToString() != "")
            {
                newFieldInfo.FieldType = StringToGviFieldType(this.dataGridView1.Rows[1].Cells[1].Value.ToString());
                if (newFieldInfo.FieldType == gviFieldType.gviFieldUnknown)
                {
                    MessageBox.Show("字段类型不能设置为gviFieldUnknown");
                    this.DialogResult = DialogResult.No;
                    this.dataGridView1.Rows[1].Cells[1].Selected = true;
                    return;
                }
            }
            //if (this.dataGridView1.Rows[2].Cells[1].Value.ToString() != "")
            //    newFieldInfo.IsSystemField = this.dataGridView1.Rows[2].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            if (this.dataGridView1.Rows[3].Cells[1].Value.ToString() != "")
                newFieldInfo.Alias = this.dataGridView1.Rows[3].Cells[1].Value.ToString();
            if (this.dataGridView1.Rows[4].Cells[1].Value.ToString() != "")
                newFieldInfo.DefaultValue = this.dataGridView1.Rows[4].Cells[1].Value;
            if (this.dataGridView1.Rows[5].Cells[1].Value.ToString() != "")
                newFieldInfo.Editable = this.dataGridView1.Rows[5].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            if (this.dataGridView1.Rows[6].Cells[1].Value.ToString() != "")
                newFieldInfo.Length = int.Parse(this.dataGridView1.Rows[6].Cells[1].Value.ToString());
            if (this.dataGridView1.Rows[7].Cells[1].Value.ToString() != "")
                newFieldInfo.Nullable = this.dataGridView1.Rows[7].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            if (this.dataGridView1.Rows[8].Cells[1].Value.ToString() != "")
                newFieldInfo.RegisteredRenderIndex = this.dataGridView1.Rows[8].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            if (this.dataGridView1.Rows[9].Cells[1].Value.ToString() != "")
                newFieldInfo.Precision = int.Parse(this.dataGridView1.Rows[9].Cells[1].Value.ToString());
            if (this.dataGridView1.Rows[10].Cells[1].Value.ToString() != "")
                newFieldInfo.Scale = int.Parse(this.dataGridView1.Rows[10].Cells[1].Value.ToString());
            //if (this.dataGridView1.Rows[11].Cells[1].Value.ToString() != "")
            //    newFieldInfo.DomainFixed = this.dataGridView1.Rows[11].Cells[1].Value.ToString().ToLower() == "true" ? true : false;

            if (newFieldInfo.FieldType == gviFieldType.gviFieldGeometry)
            {
                IGeometryDef newGeometryDefine = new GeometryDef();
                if (this.dataGridView1.Rows[12].Cells[1].Value.ToString() != "")
                    newGeometryDefine.GeometryColumnType = StringToGviGeometryColumnType(this.dataGridView1.Rows[12].Cells[1].Value.ToString());
                //if (this.dataGridView1.Rows[13].Cells[1].Value.ToString() != "")
                //    newGeometryDefine.HasSpatialIndex = this.dataGridView1.Rows[13].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
                if (this.dataGridView1.Rows[14].Cells[1].Value.ToString() != "")
                    newGeometryDefine.HasM = this.dataGridView1.Rows[14].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
                if (this.dataGridView1.Rows[15].Cells[1].Value.ToString() != "")
                    newGeometryDefine.HasZ = this.dataGridView1.Rows[15].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
                if (this.dataGridView1.Rows[16].Cells[1].Value.ToString() != "")
                    newGeometryDefine.MaxM = double.Parse(this.dataGridView1.Rows[16].Cells[1].Value.ToString());
                if (this.dataGridView1.Rows[17].Cells[1].Value.ToString() != "")
                    newGeometryDefine.MinM = double.Parse(this.dataGridView1.Rows[17].Cells[1].Value.ToString());
                //if (this.dataGridView1.Rows[18].Cells[1].Value.ToString() != "")
                //    newGeometryDefine.AvgNumPoints = int.Parse(this.dataGridView1.Rows[18].Cells[1].Value.ToString());
                
                newFieldInfo.GeometryDef = newGeometryDefine;
            }

            if (this.dataGridView1.Rows.Count > 28)
            {
                if (this.dataGridView1.Rows[28].Cells[1].Value.ToString() != "")
                    newFieldInfo.Domain.Name = this.dataGridView1.Rows[28].Cells[1].Value.ToString();
                if (this.dataGridView1.Rows[29].Cells[1].Value.ToString() != "")
                    newFieldInfo.Domain.Description = this.dataGridView1.Rows[29].Cells[1].Value.ToString();
                //if (this.dataGridView1.Rows[30].Cells[1].Value.ToString() != "")
                //    newFieldInfo.Domain.DomainType = StringToGviDomainType(this.dataGridView1.Rows[30].Cells[1].Value.ToString());
                //if (this.dataGridView1.Rows[31].Cells[1].Value.ToString() != "")
                //    newFieldInfo.Domain.FieldType = StringToGviFieldType(this.dataGridView1.Rows[31].Cells[1].Value.ToString());
                if (this.dataGridView1.Rows[32].Cells[1].Value.ToString() != "")
                    newFieldInfo.Domain.Owner = this.dataGridView1.Rows[32].Cells[1].Value.ToString();
            }

            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        public static gviFieldType StringToGviFieldType(string value)
        {
            gviFieldType type;
            switch (value)
            {
                case "gviFieldInt16":
                    type = gviFieldType.gviFieldInt16;
                    break;
                case "gviFieldInt32":
                    type = gviFieldType.gviFieldInt32;
                    break;
                case "gviFieldInt64":
                    type = gviFieldType.gviFieldInt64;
                    break;
                case "gviFieldFloat":
                    type = gviFieldType.gviFieldFloat;
                    break;
                case "gviFieldDouble":
                    type = gviFieldType.gviFieldDouble;
                    break;
                case "gviFieldString":
                    type = gviFieldType.gviFieldString;
                    break;
                case "gviFieldDate":
                    type = gviFieldType.gviFieldDate;
                    break;
                case "gviFieldBlob":
                    type = gviFieldType.gviFieldBlob;
                    break;
                case "gviFieldFID":
                    type = gviFieldType.gviFieldFID;
                    break;
                case "gviFieldUUID":
                    type = gviFieldType.gviFieldUUID;
                    break;
                case "gviFieldGeometry":
                    type = gviFieldType.gviFieldGeometry;
                    break;
                default:
                    type = gviFieldType.gviFieldUnknown;
                    break;
            }
            return type;
        }

        public static gviGeometryColumnType StringToGviGeometryColumnType(string value)
        {
            gviGeometryColumnType type;
            switch (value)
            {
                case "gviGeometryColumnPoint":
                    type = gviGeometryColumnType.gviGeometryColumnPoint;
                    break;
                case "gviGeometryColumnModelPoint":
                    type = gviGeometryColumnType.gviGeometryColumnModelPoint;
                    break;
                case "gviGeometryColumnMultiPoint":
                    type = gviGeometryColumnType.gviGeometryColumnMultiPoint;
                    break;
                case "gviGeometryColumnPolyline":
                    type = gviGeometryColumnType.gviGeometryColumnPolyline;
                    break;
                case "gviGeometryColumnTriMesh":
                    type = gviGeometryColumnType.gviGeometryColumnTriMesh;
                    break;
                case "gviGeometryColumnPolygon":
                    type = gviGeometryColumnType.gviGeometryColumnPolygon;
                    break;
                case "gviGeometryColumnPointCloud":
                    type = gviGeometryColumnType.gviGeometryColumnPointCloud;
                    break;
                case "gviGeometryColumnCollection":
                    type = gviGeometryColumnType.gviGeometryColumnCollection;
                    break;
                default:
                    type = gviGeometryColumnType.gviGeometryColumnUnknown;
                    break;
            }
            return type;
        }

        public static gviDomainType StringToGviDomainType(string value)
        {
            gviDomainType type = gviDomainType.gviDomainCodedValue;
            switch (value)
            {
                case "gviDomainCodedValue":
                    type = gviDomainType.gviDomainCodedValue;
                    break;
                case "gviDomainRange":
                    type = gviDomainType.gviDomainRange;
                    break;
            }
            return type;
        }
    }
}

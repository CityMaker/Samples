using System.Windows.Forms;
using Gvitech.CityMaker.FdeCore;

namespace CatalogTreeCreateAndDelete
{
    public partial class ViewFieldInfoForm : Form
    {
        public ViewFieldInfoForm(IFieldInfo fieldInfo)
        {
            InitializeComponent();

            {
                string[] row1 = new string[] { "Name", fieldInfo.Name };
                string[] row2 = new string[] { "FieldType", fieldInfo.FieldType.ToString() };
                string[] row3 = new string[] { "IsSystemField", fieldInfo.IsSystemField.ToString() };
                string[] row4 = new string[] { "Alias", fieldInfo.Alias };
                string[] row5 = null;
                if(fieldInfo.DefaultValue != null)
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
            
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using Gvitech.CityMaker.RenderControl;
using Gvitech;

namespace FeatureLayerSimpleRender
{
    public partial class ModelPointRenderForm : Form
    {
        public ISimpleGeometryRender newRender;

        public ModelPointRenderForm(IGeometryRender geoRender, object[] fieldNamesItems)
        {
            InitializeComponent();

            {
                this.comboBox1.Items.AddRange(fieldNamesItems);
                this.comboBox1.Text = "";

                if (geoRender != null)
                {
                    int index = Utils.getIndexFromItems(fieldNamesItems, geoRender.RenderGroupField);
                    this.comboBox1.SelectedIndex = index;
                }  
            }

            ISimpleGeometryRender render = geoRender as ISimpleGeometryRender;

            if (render == null || render.Symbol == null)
            {
                IModelPointSymbol tmpSymbol = new ModelPointSymbol();
                string[] row1 = new string[] { "Color", tmpSymbol.Color.ToString() };
                string[] row2 = new string[] { "EnableColor", tmpSymbol.EnableColor.ToString() };
                string[] row3 = new string[] { "EnableTexture ", tmpSymbol.EnableTexture.ToString() };
                object[] rows = new object[] { row1, row2, row3};
                foreach (string[] rowArray in rows)
                {
                    this.dataGridView2.Rows.Add(rowArray);
                }
                return;
            }
            
            if (render.Symbol == null)
                return;

            {
                IModelPointSymbol symbol = render.Symbol as IModelPointSymbol;
                string[] row1 = new string[] { "Color", symbol.Color.ToString() };
                string[] row2 = new string[] { "EnableColor", symbol.EnableColor.ToString() };
                string[] row3 = new string[] { "EnableTexture ", symbol.EnableTexture.ToString() };
                object[] rows = new object[] { row1, row2, row3 };
                foreach (string[] rowArray in rows)
                {
                    this.dataGridView2.Rows.Add(rowArray);
                }
            }         
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("只有注册了RenderIndex的字段才有效!\n 配置LogicGroupField后模型会看不见，如要使模型可见，请参见\\beginner\\LogicGroupTree'");
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string propertyName = this.dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                string propertyValue = this.dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                switch (e.RowIndex)
                {
                    case 0:
                        {
                            this.colorDialog1.Color = Utils.HexNumberToColor(propertyValue);
                            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
                            {
                                uint olec = (uint)(this.colorDialog1.Color.A << 24 | this.colorDialog1.Color.R << 16 | this.colorDialog1.Color.G << 8 | this.colorDialog1.Color.B);
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = olec.ToString();
                            }
                        }
                        break;
                    case 1:
                    case 2:
                        {
                            EditForm edit = new EditForm(propertyValue.ToLower() == "true" ? true : false, propertyName);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = edit.checkBox1.Checked.ToString();
                            }
                        }
                        break;
                }
                this.btnOK.Focus();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            newRender = new SimpleGeometryRender();
            newRender.RenderGroupField = this.comboBox1.Text;

            IModelPointSymbol newSymbol = new ModelPointSymbol();
            if (this.dataGridView2.Rows[0].Cells[1].Value.ToString() != "")
            {
                string colstr = this.dataGridView2.Rows[0].Cells[1].Value.ToString();
                Color col = Utils.HexNumberToColor(colstr);
                newSymbol.Color = col;
            }
            if (this.dataGridView2.Rows[1].Cells[1].Value.ToString() != "")
                newSymbol.EnableColor = this.dataGridView2.Rows[1].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            if (this.dataGridView2.Rows[2].Cells[1].Value.ToString() != "")
                newSymbol.EnableTexture = this.dataGridView2.Rows[2].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            
            newRender.Symbol = newSymbol;
        }
        
    }
}

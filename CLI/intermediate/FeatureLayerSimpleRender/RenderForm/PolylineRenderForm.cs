using System;
using System.Drawing;
using System.Windows.Forms;
using Gvitech.CityMaker.RenderControl;
using Gvitech;
using System.IO;

namespace FeatureLayerSimpleRender
{
    public partial class PolylineRenderForm : Form
    {
        public ISimpleGeometryRender newRender;
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        public PolylineRenderForm(IGeometryRender geoRender, object[] fieldNamesItems)
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
                ICurveSymbol tmpSymbol = new CurveSymbol();
                string[] row1 = new string[] { "Color", tmpSymbol.Color.ToString() };
                string[] row2 = new string[] { "RepeatLength", tmpSymbol.RepeatLength.ToString() };
                string[] row3 = new string[] { "Width", tmpSymbol.Width.ToString() };
                object[] rows = new object[] { row1, row2, row3 };
                foreach (string[] rowArray in rows)
                {
                    this.dataGridView2.Rows.Add(rowArray);
                }
                this.textBoxPictureName.Text = "";
                return;
            }
            
            if (render.Symbol == null)
            {
                this.textBoxPictureName.Text = "";
                return;
            }

            {
                ICurveSymbol symbol = render.Symbol as ICurveSymbol;
                string[] row1 = new string[] { "Color", symbol.Color.ToString() };
                string[] row2 = new string[] { "RepeatLength", symbol.RepeatLength.ToString() };
                string[] row3 = new string[] { "Width", symbol.Width.ToString() };
                object[] rows = new object[] { row1, row2, row3 };
                foreach (string[] rowArray in rows)
                {
                    this.dataGridView2.Rows.Add(rowArray);
                }

                this.textBoxPicturePath.Text = symbol.ImageName;
                this.textBoxPictureName.Text = symbol.ImageName;
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
                        {
                            float old = float.Parse(propertyValue);
                            EditForm edit = new EditForm(0, old);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = edit.numericUpDown1.Value.ToString();
                            }
                        }
                        break;
                    case 2:
                        {
                            float old = float.Parse(propertyValue);
                            EditForm edit = new EditForm(-1000, old);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = edit.numericUpDown1.Value.ToString();
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

            ICurveSymbol newSymbol = new CurveSymbol();
            if (this.dataGridView2.Rows[0].Cells[1].Value.ToString() != "")
            {
                string colstr = this.dataGridView2.Rows[0].Cells[1].Value.ToString();
                Color col = Utils.HexNumberToColor(colstr);
                newSymbol.Color = col;
            }
            if (this.dataGridView2.Rows[1].Cells[1].Value.ToString() != "")
                newSymbol.RepeatLength = float.Parse(this.dataGridView2.Rows[1].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[2].Cells[1].Value.ToString() != "")
                newSymbol.Width = float.Parse(this.dataGridView2.Rows[2].Cells[1].Value.ToString());

            if (this.checkBox1.Checked)
                newSymbol.ImageName = this.textBoxPicturePath.Text;
            else
                newSymbol.ImageName = this.textBoxPictureName.Text;
            
            newRender.Symbol = newSymbol;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.textBoxPicturePath.Enabled = true;
                this.textBoxPictureName.Enabled = false;
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "png文件|*.png|所有文件|*.*";
                if(System.IO.Directory.Exists(strMediaPath))
                {
                    dlg.InitialDirectory = strMediaPath + @"\png";
                }
                dlg.RestoreDirectory = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(dlg.FileName))
                    {
                        this.textBoxPicturePath.Text = dlg.FileName;
                    }
                }
            }
            else
            {
                this.textBoxPicturePath.Enabled = false;
                this.textBoxPictureName.Enabled = true;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("只有只有使用ImageClass里有的图片才有效!");
        }
        
    }
}

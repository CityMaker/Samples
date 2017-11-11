using System;
using System.Drawing;
using System.Windows.Forms;
using Gvitech.CityMaker.RenderControl;
using Gvitech;
using System.IO;

namespace FeatureLayerSimpleRender
{
    public partial class PolygonRenderForm : Form
    {
        public ISimpleGeometryRender newRender;
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";

        public PolygonRenderForm(IGeometryRender geoRender, object[] fieldNamesItems)
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
                ISurfaceSymbol tmpSurfaceSymbol = new SurfaceSymbol();
                string[] row1 = new string[] { "Color", tmpSurfaceSymbol.Color.ToString() };
                string[] row2 = new string[] { "EnableLight", tmpSurfaceSymbol.EnableLight.ToString() };
                string[] row3 = new string[] { "RepeatLengthU", tmpSurfaceSymbol.RepeatLengthU.ToString() };
                string[] row4 = new string[] { "RepeatLengthV", tmpSurfaceSymbol.RepeatLengthV.ToString() };
                string[] row5 = new string[] { "Rotation", tmpSurfaceSymbol.Rotation.ToString() };
                ICurveSymbol tmpCurveSymbol = new CurveSymbol();
                string[] row6 = new string[] { "BoundaryColor", tmpCurveSymbol.Color.ToString() };
                string[] row7 = new string[] { "RepeatLength", tmpCurveSymbol.RepeatLength.ToString() };
                string[] row8 = new string[] { "Width", tmpCurveSymbol.Width.ToString() };
                object[] rows = new object[] { row1, row2, row3, row4, row5, row6, row7, row8 };
                foreach (string[] rowArray in rows)
                {
                    this.dataGridView2.Rows.Add(rowArray);
                }
                return;
            }

            if (render.Symbol == null)
            {
                this.textBoxSurfacePictureName.Text = "";
                return;
            }

            {
                ISurfaceSymbol surfaceSymbol = render.Symbol as ISurfaceSymbol;
                ICurveSymbol boundarySymbol = surfaceSymbol.BoundarySymbol as ICurveSymbol;
                if (boundarySymbol != null)
                {
                    string[] row1 = new string[] { "Color", surfaceSymbol.Color.ToString() };
                    string[] row2 = new string[] { "EnableLight", surfaceSymbol.EnableLight.ToString() };
                    string[] row3 = new string[] { "RepeatLengthU", surfaceSymbol.RepeatLengthU.ToString() };
                    string[] row4 = new string[] { "RepeatLengthV", surfaceSymbol.RepeatLengthV.ToString() };
                    string[] row5 = new string[] { "Rotation", surfaceSymbol.Rotation.ToString() };
                    string[] row6 = new string[] { "BoundaryColor", boundarySymbol.Color.ToString() };
                    string[] row7 = new string[] { "RepeatLength", boundarySymbol.RepeatLength.ToString() };
                    string[] row8 = new string[] { "Width", boundarySymbol.Width.ToString() };
                    object[] rows2 = new object[] { row1, row2, row3, row4, row5, row6, row7, row8 };
                    foreach (string[] rowArray in rows2)
                    {
                        this.dataGridView2.Rows.Add(rowArray);
                    }
                    this.textBoxCurvePictureName.Text = boundarySymbol.ImageName;
                    this.textBoxCurvePicturePath.Text = boundarySymbol.ImageName;
                }
                else
                {
                    ICurveSymbol boundarySymbolNew = new CurveSymbol();
                    string[] row1 = new string[] { "Color", surfaceSymbol.Color.ToString() };
                    string[] row2 = new string[] { "EnableLight", surfaceSymbol.EnableLight.ToString() };
                    string[] row3 = new string[] { "RepeatLengthU", surfaceSymbol.RepeatLengthU.ToString() };
                    string[] row4 = new string[] { "RepeatLengthV", surfaceSymbol.RepeatLengthV.ToString() };
                    string[] row5 = new string[] { "Rotation", surfaceSymbol.Rotation.ToString() };
                    string[] row6 = new string[] { "BoundaryColor", boundarySymbolNew.Color.ToString() };
                    string[] row7 = new string[] { "RepeatLength", boundarySymbolNew.RepeatLength.ToString() };
                    string[] row8 = new string[] { "Width", boundarySymbolNew.Width.ToString() };
                    object[] rows2 = new object[] { row1, row2, row3, row4, row5, row6, row7, row8 };
                    foreach (string[] rowArray in rows2)
                    {
                        this.dataGridView2.Rows.Add(rowArray);
                    }
                    this.textBoxCurvePictureName.Text = "";
                    this.textBoxCurvePicturePath.Text = "";
                }

                this.textBoxSurfacePictureName.Text = surfaceSymbol.ImageName;
                this.textBoxSurfacePicturePath.Text = surfaceSymbol.ImageName;
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
                    case 5:
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
                            EditForm edit = new EditForm(propertyValue.ToLower() == "true" ? true : false, propertyName);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = edit.checkBox1.Checked.ToString();
                            }
                        }
                        break;
                    case 2:
                    case 3:
                    case 4:
                        {
                            float old = float.Parse(propertyValue);
                            EditForm edit = new EditForm(0, old);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = edit.numericUpDown1.Value.ToString();
                            }
                        }
                        break;
                    case 6:
                        {
                            float old = float.Parse(propertyValue);
                            EditForm edit = new EditForm(-360, old);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = edit.numericUpDown1.Value.ToString();
                            }
                        }
                        break;
                    case 7:
                        {
                            float old = float.Parse(propertyValue);
                            EditForm edit = new EditForm(-2, old);
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

            ISurfaceSymbol newSurfaceSymbol = new SurfaceSymbol();
            ICurveSymbol newBoundarySymbol = new CurveSymbol();

            if (this.dataGridView2.Rows[0].Cells[1].Value.ToString() != "")
            {
                string colstr = this.dataGridView2.Rows[0].Cells[1].Value.ToString();
                Color col = Utils.HexNumberToColor(colstr);
                newSurfaceSymbol.Color = col;
            }
            if (this.dataGridView2.Rows[1].Cells[1].Value.ToString() != "")
                newSurfaceSymbol.EnableLight = this.dataGridView2.Rows[1].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            if (this.dataGridView2.Rows[2].Cells[1].Value.ToString() != "")
                newSurfaceSymbol.RepeatLengthU = float.Parse(this.dataGridView2.Rows[2].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[3].Cells[1].Value.ToString() != "")
                newSurfaceSymbol.RepeatLengthV = float.Parse(this.dataGridView2.Rows[3].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[4].Cells[1].Value.ToString() != "")
                newSurfaceSymbol.Rotation = float.Parse(this.dataGridView2.Rows[4].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[5].Cells[1].Value.ToString() != "")
            {
                string colstr = this.dataGridView2.Rows[5].Cells[1].Value.ToString();
                Color col = Utils.HexNumberToColor(colstr);
                newBoundarySymbol.Color = col;
            }
            if (this.dataGridView2.Rows[6].Cells[1].Value.ToString() != "")
                newBoundarySymbol.RepeatLength = float.Parse(this.dataGridView2.Rows[6].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[7].Cells[1].Value.ToString() != "")
                newBoundarySymbol.Width = float.Parse(this.dataGridView2.Rows[7].Cells[1].Value.ToString());

            if (this.checkBoxSurface.Checked)
                newSurfaceSymbol.ImageName = this.textBoxSurfacePicturePath.Text;
            else
                newSurfaceSymbol.ImageName = this.textBoxSurfacePictureName.Text;

            if (this.checkBoxCurve.Checked)
                newBoundarySymbol.ImageName = this.textBoxCurvePicturePath.Text;
            else
                newBoundarySymbol.ImageName = this.textBoxCurvePictureName.Text;

            newSurfaceSymbol.BoundarySymbol = newBoundarySymbol;
            newRender.Symbol = newSurfaceSymbol;
        }

        private void checkBoxSurface_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxSurface.Checked)
            {
                this.textBoxSurfacePicturePath.Enabled = true;
                this.textBoxSurfacePictureName.Enabled = false;
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
                        this.textBoxSurfacePicturePath.Text = dlg.FileName;
                    }
                }
            }
            else
            {
                this.textBoxSurfacePicturePath.Enabled = false;
                this.textBoxSurfacePictureName.Enabled = true;
            }
        }

        private void checkBoxCurve_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxCurve.Checked)
            {
                this.textBoxCurvePicturePath.Enabled = true;
                this.textBoxCurvePictureName.Enabled = false;
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
                        this.textBoxCurvePicturePath.Text = dlg.FileName;
                    }
                }
            }
            else
            {
                this.textBoxCurvePicturePath.Enabled = false;
                this.textBoxCurvePictureName.Enabled = true;
            }
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("只有只有使用ImageClass里有的图片才有效!");
        }
        
    }
}

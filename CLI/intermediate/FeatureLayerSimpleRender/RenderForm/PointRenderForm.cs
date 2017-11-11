using System;
using System.Drawing;
using System.Windows.Forms;
using Gvitech.CityMaker.RenderControl;
using Gvitech;
using System.IO;

namespace FeatureLayerSimpleRender
{
    public partial class PointRenderForm : Form
    {
        public ISimpleGeometryRender newRender;
        public bool IsSimplePoint;
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";

        public PointRenderForm(IGeometryRender geoRender, object[] fieldNamesItems, bool isSimplePoint)
        {
            InitializeComponent();
            IsSimplePoint = isSimplePoint;

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

            if (render == null)
            {
                if (isSimplePoint)
                {
                    this.dataGridView1.Enabled = true;
                    this.checkBox1.Enabled = false;
                    this.textBoxPictureName.Enabled = false;
                    this.textBoxPicturePath.Enabled = false;

                    ISimplePointSymbol tmpSymbol = new SimplePointSymbol();
                    string[] row1 = new string[] { "Alignment", tmpSymbol.Alignment.ToString() };
                    string[] row2 = new string[] { "Size", tmpSymbol.Size.ToString() };
                    object[] rows = new object[] { row1, row2 };
                    foreach (string[] rowArray in rows)
                    {
                        this.dataGridView2.Rows.Add(rowArray);
                    }

                    string[] row11 = new string[] { "FillColor", tmpSymbol.FillColor.ToString() };                    
                    string[] row12 = new string[] { "Style", tmpSymbol.Style.ToString() };
                    object[] rows2 = new object[] { row11, row12 };
                    foreach (string[] rowArray in rows2)
                    {
                        this.dataGridView1.Rows.Add(rowArray);
                    }
                    return;
                }
                else
                {
                    this.dataGridView1.Enabled = false;
                    this.checkBox1.Enabled = true;
                    this.textBoxPictureName.Enabled = true;

                    IImagePointSymbol tmpSymbol = new ImagePointSymbol();
                    string[] row1 = new string[] { "Alignment", tmpSymbol.Alignment.ToString() };
                    string[] row2 = new string[] { "Size", tmpSymbol.Size.ToString() };
                    object[] rows = new object[] { row1, row2 };
                    foreach (string[] rowArray in rows)
                    {
                        this.dataGridView2.Rows.Add(rowArray);
                    }

                    this.textBoxPictureName.Text = "";
                    return;
                }                
            }
            
            if (render.Symbol == null)
                return;

            if (isSimplePoint)
            {
                this.dataGridView1.Enabled = true;
                this.checkBox1.Enabled = false;
                this.textBoxPictureName.Enabled = false;
                this.textBoxPicturePath.Enabled = false;

                ISimplePointSymbol symbol = render.Symbol as ISimplePointSymbol;
                if (symbol != null)
                {
                    string[] row1 = new string[] { "Alignment", symbol.Alignment.ToString() };
                    string[] row2 = new string[] { "Size", symbol.Size.ToString() };
                    object[] rows = new object[] { row1, row2 };
                    foreach (string[] rowArray in rows)
                    {
                        this.dataGridView2.Rows.Add(rowArray);
                    }

                    string[] row11 = new string[] { "FillColor", symbol.FillColor.ToString() };                    
                    string[] row12 = new string[] { "Style", symbol.Style.ToString() };
                    object[] rows2 = new object[] { row11, row12 };
                    foreach (string[] rowArray in rows2)
                    {
                        this.dataGridView1.Rows.Add(rowArray);
                    }
                }
                else
                {
                    ISimplePointSymbol tmpSymbol = new SimplePointSymbol();
                    string[] row1 = new string[] { "Alignment", tmpSymbol.Alignment.ToString() };
                    string[] row2 = new string[] { "Size", tmpSymbol.Size.ToString() };
                    object[] rows = new object[] { row1, row2 };
                    foreach (string[] rowArray in rows)
                    {
                        this.dataGridView2.Rows.Add(rowArray);
                    }

                    string[] row11 = new string[] { "FillColor", tmpSymbol.FillColor.ToString() };                    
                    string[] row12 = new string[] { "Style", tmpSymbol.Style.ToString() };
                    object[] rows2 = new object[] { row11, row12 };
                    foreach (string[] rowArray in rows2)
                    {
                        this.dataGridView1.Rows.Add(rowArray);
                    }
                }                
            }
            else
            {
                this.dataGridView1.Enabled = false;
                this.checkBox1.Enabled = true;
                this.textBoxPictureName.Enabled = true;

                IImagePointSymbol symbol = render.Symbol as IImagePointSymbol;
                if (symbol != null)
                {
                    string[] row1 = new string[] { "Alignment", symbol.Alignment.ToString() };
                    string[] row2 = new string[] { "Size", symbol.Size.ToString() };
                    object[] rows = new object[] { row1, row2 };
                    foreach (string[] rowArray in rows)
                    {
                        this.dataGridView2.Rows.Add(rowArray);
                    }

                    this.textBoxPicturePath.Text = symbol.ImageName;
                    this.textBoxPictureName.Text = symbol.ImageName;
                }
                else
                {
                    IImagePointSymbol tmpSymbol = new ImagePointSymbol();
                    string[] row1 = new string[] { "Alignment", tmpSymbol.Alignment.ToString() };
                    string[] row2 = new string[] { "Size", tmpSymbol.Size.ToString() };
                    object[] rows = new object[] { row1, row2 };
                    foreach (string[] rowArray in rows)
                    {
                        this.dataGridView2.Rows.Add(rowArray);
                    }

                    this.textBoxPictureName.Text = "";
                }
            } 
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("只有注册了RenderIndex的字段才有效!");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("只有只有使用ImageClass里有的图片才有效!");
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
                            object[] items = new object[] { "",
                                "gviPivotAlignBottomLeft",
                                "gviPivotAlignBottomCenter",
                                "gviPivotAlignBottomRight",
                                "gviPivotAlignCenterLeft",
                                "gviPivotAlignCenterCenter",
                                "gviPivotAlignCenterRight",
                                "gviPivotAlignTopLeft",
                                "gviPivotAlignTopCenter",
                                "gviPivotAlignTopRight"};
                            int index = Utils.getIndexFromItems(items, propertyValue.Trim());
                            EditForm edit = new EditForm(items, index);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = edit.comboBox1.SelectedItem.ToString();
                            }
                        }
                        break;
                    case 1:
                        {
                            int old = int.Parse(propertyValue);
                            EditForm edit = new EditForm(1, old);
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

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string propertyName = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string propertyValue = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                switch (e.RowIndex)
                {
                    case 0:
                    case 1:
                        {
                            this.colorDialog1.Color = Utils.HexNumberToColor(propertyValue);
                            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
                            {
                                uint olec = (uint)(this.colorDialog1.Color.A << 24 | this.colorDialog1.Color.R << 16 | this.colorDialog1.Color.G << 8 | this.colorDialog1.Color.B);
                                this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = olec.ToString();
                            }
                        }
                        break;
                    case 2:
                        {
                            object[] items = new object[] { "",
                                        "gviSimplePointCircle",
                                        "gviSimplePointSquare",
                                        "gviSimplePointCross",
                                        "gviSimplePointX",
                                        "gviSimplePointDiamond"};
                            int index = Utils.getIndexFromItems(items, propertyValue.Trim());
                            EditForm edit = new EditForm(items, index);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = edit.comboBox1.SelectedItem.ToString();
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

            if (IsSimplePoint)
            {
                ISimplePointSymbol newSymbol = new SimplePointSymbol();
                if (this.dataGridView2.Rows[0].Cells[1].Value.ToString() != "")
                    newSymbol.Alignment = StringToGviPivotAlignment(this.dataGridView2.Rows[0].Cells[1].Value.ToString());
                if (this.dataGridView2.Rows[1].Cells[1].Value.ToString() != "")
                    newSymbol.Size = int.Parse(this.dataGridView2.Rows[1].Cells[1].Value.ToString());

                if (this.dataGridView1.Rows[0].Cells[1].Value.ToString() != "")
                {
                    string colstr = this.dataGridView1.Rows[0].Cells[1].Value.ToString();
                    Color col = Utils.HexNumberToColor(colstr);
                    newSymbol.FillColor = col;
                }
                if (this.dataGridView1.Rows[1].Cells[1].Value.ToString() != "")
                    newSymbol.Style = StringToSimplePointStyle(this.dataGridView1.Rows[1].Cells[1].Value.ToString());

                newRender.Symbol = newSymbol;
            }
            else
            {
                IImagePointSymbol newSymbol = new ImagePointSymbol();
                if (this.dataGridView2.Rows[0].Cells[1].Value.ToString() != "")
                    newSymbol.Alignment = StringToGviPivotAlignment(this.dataGridView2.Rows[0].Cells[1].Value.ToString());
                if (this.dataGridView2.Rows[1].Cells[1].Value.ToString() != "")
                    newSymbol.Size = int.Parse(this.dataGridView2.Rows[1].Cells[1].Value.ToString());

                if (this.checkBox1.Checked)
                    newSymbol.ImageName = this.textBoxPicturePath.Text;
                else
                    newSymbol.ImageName = this.textBoxPictureName.Text;

                newRender.Symbol = newSymbol;
            }                               
        }


        public static gviPivotAlignment StringToGviPivotAlignment(string value)
        {
            gviPivotAlignment mode = gviPivotAlignment.gviPivotAlignBottomLeft;
            switch (value)
            {
                case "gviMultilineCenter":
                    mode = gviPivotAlignment.gviPivotAlignBottomCenter;
                    break;
                case "gviMultilineRight":
                    mode = gviPivotAlignment.gviPivotAlignBottomRight;
                    break;
                case "gviPivotAlignCenterCenter":
                    mode = gviPivotAlignment.gviPivotAlignCenterCenter;
                    break;
                case "gviPivotAlignCenterLeft":
                    mode = gviPivotAlignment.gviPivotAlignCenterLeft;
                    break;
                case "gviPivotAlignCenterRight":
                    mode = gviPivotAlignment.gviPivotAlignCenterRight;
                    break;
                case "gviPivotAlignTopCenter":
                    mode = gviPivotAlignment.gviPivotAlignTopCenter;
                    break;
                case "gviPivotAlignTopLeft":
                    mode = gviPivotAlignment.gviPivotAlignTopLeft;
                    break;
                case "gviPivotAlignTopRight":
                    mode = gviPivotAlignment.gviPivotAlignTopRight;
                    break;
            }
            return mode;
        }

        public static gviSimplePointStyle StringToSimplePointStyle(string value)
        {
            gviSimplePointStyle mode = gviSimplePointStyle.gviSimplePointCircle;
            switch (value)
            {
                case "gviSimplePointCircle":
                    mode = gviSimplePointStyle.gviSimplePointCircle;
                    break;
                case "gviSimplePointSquare":
                    mode = gviSimplePointStyle.gviSimplePointSquare;
                    break;
                case "gviSimplePointCross":
                    mode = gviSimplePointStyle.gviSimplePointCross;
                    break;
                case "gviSimplePointX":
                    mode = gviSimplePointStyle.gviSimplePointX;
                    break;
                case "gviSimplePointDiamond":
                    mode = gviSimplePointStyle.gviSimplePointDiamond;
                    break;
            }
            return mode;
        }
    }
}

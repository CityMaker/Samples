using System;
using System.Drawing;
using System.Windows.Forms;
using Gvitech.CityMaker.RenderControl;
using Gvitech;

namespace FeatureLayerSimpleRender
{
    public partial class TextRenderForm : Form
    {
        public ISimpleTextRender newRender;

        public TextRenderForm(ITextRender textRender)
        {
            InitializeComponent();

            // ITextRender
            {
                if (textRender == null)
                    this.textBox1.Text = "";
                else
                    this.textBox1.Text = textRender.Expression.ToString();
            }

            // ISimpleTextRender
            ISimpleTextRender render = textRender as ISimpleTextRender;

            if (render == null || render.Symbol == null)
            {
                ITextSymbol tmpSymbol = new TextSymbol();
                TextAttribute tmpAttribute = new TextAttribute();
                string[] row1 = new string[] { "BackgroundColor", tmpAttribute.BackgroundColor.ToString() };
                string[] row2 = new string[] { "Bold", tmpAttribute.Bold.ToString() };
                string[] row3 = new string[] { "DrawLine", tmpSymbol.DrawLine.ToString() };
                string[] row4 = new string[] { "Font", tmpAttribute.Font };
                string[] row5 = new string[] { "Italic", tmpAttribute.Italic.ToString() };
                string[] row6 = new string[] { "LineColor", tmpSymbol.LineColor.ToString() };
                string[] row7 = new string[] { "MaxVisualDistance", tmpSymbol.MaxVisualDistance.ToString() };
                string[] row8 = new string[] { "MinVisualDistance", tmpSymbol.MinVisualDistance.ToString() };
                string[] row9 = new string[] { "MultilineJustification", tmpAttribute.MultilineJustification.ToString() };
                string[] row10 = new string[] { "PivotAlignment", tmpSymbol.PivotAlignment.ToString() };
                string[] row11 = new string[] { "Priority", tmpSymbol.Priority.ToString() };
                string[] row12 = new string[] { "TextColor", tmpAttribute.TextColor.ToString() };
                string[] row13 = new string[] { "TextSize", tmpAttribute.TextSize.ToString() };
                string[] row14 = new string[] { "Underline", tmpAttribute.Underline.ToString() };
                string[] row15 = new string[] { "VerticalOffset", tmpSymbol.VerticalOffset.ToString() };
                string[] row16 = new string[] { "MarginWidth", tmpSymbol.MarginWidth.ToString() };
                string[] row17 = new string[] { "MarginHeight", tmpSymbol.MarginHeight.ToString() };
                string[] row18 = new string[] { "OutlineColor", tmpAttribute.OutlineColor.ToString() };
                string[] row19 = new string[] { "LockMode", tmpSymbol.LockMode.ToString() };
                string[] row20 = new string[] { "DynamicPlacement", render.DynamicPlacement.ToString() };
                string[] row21 = new string[] { "MinimizeOverlap", render.MinimizeOverlap.ToString() };
                string[] row22 = new string[] { "RemoveDuplicate", render.RemoveDuplicate.ToString() };
                object[] rows = new object[] { row1, row2, row3, row4, row5, row6, row7, row8, row9, row10, row11, row12, row13, row14, row15, row16, row17, row18, row19, row20, row21, row22 };
                foreach (string[] rowArray in rows)
                {
                    this.dataGridView2.Rows.Add(rowArray);
                }
                return;
            }

            if (render.Symbol == null)
                return;

            {
                ITextSymbol symbol = render.Symbol as ITextSymbol;
                ITextAttribute attribute = symbol.TextAttribute;
                if (attribute != null)
                {
                    string[] row1 = new string[] { "BackgroundColor", attribute.BackgroundColor.ToString() };
                    string[] row2 = new string[] { "Bold", attribute.Bold.ToString() };
                    string[] row3 = new string[] { "DrawLine", symbol.DrawLine.ToString() };
                    string[] row4 = new string[] { "Font", attribute.Font };
                    string[] row5 = new string[] { "Italic", attribute.Italic.ToString() };
                    string[] row6 = new string[] { "LineColor", symbol.LineColor.ToString() };
                    string[] row7 = new string[] { "MaxVisualDistance", symbol.MaxVisualDistance.ToString() };
                    string[] row8 = new string[] { "MinVisualDistance", symbol.MinVisualDistance.ToString() };
                    string[] row9 = new string[] { "MultilineJustification", attribute.MultilineJustification.ToString() };
                    string[] row10 = new string[] { "PivotAlignment", symbol.PivotAlignment.ToString() };
                    string[] row11 = new string[] { "Priority", symbol.Priority.ToString() };
                    string[] row12 = new string[] { "TextColor", attribute.TextColor.ToString() };
                    string[] row13 = new string[] { "TextSize", attribute.TextSize.ToString() };
                    string[] row14 = new string[] { "Underline", attribute.Underline.ToString() };
                    string[] row15 = new string[] { "VerticalOffset", symbol.VerticalOffset.ToString() };
                    string[] row16 = new string[] { "MarginWidth", symbol.MarginWidth.ToString() };
                    string[] row17 = new string[] { "MarginHeight", symbol.MarginHeight.ToString() };
                    string[] row18 = new string[] { "OutlineColor", attribute.OutlineColor.ToString() };
                    string[] row19 = new string[] { "LockMode", symbol.LockMode.ToString() };
                    string[] row20 = new string[] { "DynamicPlacement", render.DynamicPlacement.ToString() };
                    string[] row21 = new string[] { "MinimizeOverlap", render.MinimizeOverlap.ToString() };
                    string[] row22 = new string[] { "RemoveDuplicate", render.RemoveDuplicate.ToString() };
                    object[] rows = new object[] { row1, row2, row3, row4, row5, row6, row7, row8, row9, row10, row11, row12, row13, row14, row15, row16, row17, row18, row19, row20, row21, row22 };
                    foreach (string[] rowArray in rows)
                    {
                        this.dataGridView2.Rows.Add(rowArray);
                    }
                }
                else
                {
                    ITextAttribute attributeNew = new TextAttribute();
                    string[] row1 = new string[] { "BackgroundColor", attributeNew.BackgroundColor.ToString() };
                    string[] row2 = new string[] { "Bold", attributeNew.Bold.ToString() };
                    string[] row3 = new string[] { "DrawLine", symbol.DrawLine.ToString() };
                    string[] row4 = new string[] { "Font", attributeNew.Font };
                    string[] row5 = new string[] { "Italic", attributeNew.Italic.ToString() };
                    string[] row6 = new string[] { "LineColor", symbol.LineColor.ToString() };
                    string[] row7 = new string[] { "MaxVisualDistance", symbol.MaxVisualDistance.ToString() };
                    string[] row8 = new string[] { "MinVisualDistance", symbol.MinVisualDistance.ToString() };
                    string[] row9 = new string[] { "MultilineJustification", attributeNew.MultilineJustification.ToString() };
                    string[] row10 = new string[] { "PivotAlignment", symbol.PivotAlignment.ToString() };
                    string[] row11 = new string[] { "Priority", symbol.Priority.ToString() };
                    string[] row12 = new string[] { "TextColor", attributeNew.TextColor.ToString() };
                    string[] row13 = new string[] { "TextSize", attributeNew.TextSize.ToString() };
                    string[] row14 = new string[] { "Underline", attributeNew.Underline.ToString() };
                    string[] row15 = new string[] { "VerticalOffset", symbol.VerticalOffset.ToString() };
                    string[] row16 = new string[] { "MarginWidth", symbol.MarginWidth.ToString() };
                    string[] row17 = new string[] { "MarginHeight", symbol.MarginHeight.ToString() };
                    string[] row18 = new string[] { "OutlineColor", attributeNew.OutlineColor.ToString() };
                    string[] row19 = new string[] { "LockMode", symbol.LockMode.ToString() };
                    string[] row20 = new string[] { "DynamicPlacement", render.DynamicPlacement.ToString() };
                    string[] row21 = new string[] { "MinimizeOverlap", render.MinimizeOverlap.ToString() };
                    string[] row22 = new string[] { "RemoveDuplicate", render.RemoveDuplicate.ToString() };
                    object[] rows = new object[] { row1, row2, row3, row4, row5, row6, row7, row8, row9, row10, row11, row12, row13, row14, row15, row16, row17, row18, row19, row20, row21, row22 };
                    foreach (string[] rowArray in rows)
                    {
                        this.dataGridView2.Rows.Add(rowArray);
                    }
                }
               
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //MessageBox.Show("遵循lua脚本语法 for example：\n 'model_gid:'..$(Groupid)");
            this.textBox1.Text = "'此处文字可随意指定oid:'..$(oid)";
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
                    case 11:
                    case 17:
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
                    case 4:
                    case 13:
                    case 19:
                    case 20:
                    case 21:
                        {
                            EditForm edit = new EditForm(propertyValue.ToLower() == "true" ? true : false, propertyName);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = edit.checkBox1.Checked.ToString();
                            }
                        }
                        break;
                    case 3:
                        {
                            object[] items = new object[] { "", "Airal", "宋体", "黑体" };
                            int index = Utils.getIndexFromItems(items, propertyValue.Trim());
                            EditForm edit = new EditForm(items, index);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = edit.comboBox1.SelectedItem.ToString();
                            }
                        }
                        break;
                    case 8:
                        {
                            object[] items = new object[] {"", 
                                "gviMultilineLeft", 
                                "gviMultilineCenter", 
                                "gviMultilineRight" };
                            int index = Utils.getIndexFromItems(items, propertyValue.Trim());
                            EditForm edit = new EditForm(items, index);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = edit.comboBox1.SelectedItem.ToString();
                            }
                        }
                        break;
                    case 9:
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
                    case 18:
                        {
                            object[] items = new object[] { "",
                                "gviLockDecal",
                                "gviLockAxis",
                                "gviLockAxisTextUp"};
                            int index = Utils.getIndexFromItems(items, propertyValue.Trim());
                            EditForm edit = new EditForm(items, index);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = edit.comboBox1.SelectedItem.ToString();
                            }
                        }
                        break;
                    case 6:
                    case 7:
                    case 14:
                        {
                            double old = double.Parse(propertyValue);
                            double min = double.Parse("0");
                            EditForm edit = new EditForm(min, old);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = edit.numericUpDown1.Value.ToString();
                            }
                        }
                        break;
                    case 10:
                        {
                            int old = int.Parse(propertyValue);
                            EditForm edit = new EditForm(-65535, old);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = edit.numericUpDown1.Value.ToString();
                            }
                        }
                        break;
                    case 12:
                        {
                            int old = int.Parse(propertyValue);
                            EditForm edit = new EditForm(1, old);
                            if (edit.ShowDialog() == DialogResult.OK)
                            {
                                this.dataGridView2.Rows[e.RowIndex].Cells[1].Value = edit.numericUpDown1.Value.ToString();
                            }
                        }
                        break;
                    case 15:
                    case 16:
                        {
                            int old = int.Parse(propertyValue);
                            EditForm edit = new EditForm(0, old);
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
            newRender = new SimpleTextRender();
            newRender.Expression = this.textBox1.Text;

            ITextSymbol newSymbol = new TextSymbol();
            TextAttribute newAttribute = new TextAttribute();
            if (this.dataGridView2.Rows[0].Cells[1].Value.ToString() != "")
            {
                string colstr = this.dataGridView2.Rows[0].Cells[1].Value.ToString();
                Color col = Utils.HexNumberToColor(colstr);
                newAttribute.BackgroundColor = col;
            }
            if (this.dataGridView2.Rows[1].Cells[1].Value.ToString() != "")
                newAttribute.Bold = this.dataGridView2.Rows[1].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            if (this.dataGridView2.Rows[2].Cells[1].Value.ToString() != "")
                newSymbol.DrawLine = this.dataGridView2.Rows[2].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            if (this.dataGridView2.Rows[3].Cells[1].Value.ToString() != "")
                newAttribute.Font = this.dataGridView2.Rows[3].Cells[1].Value.ToString();
            if (this.dataGridView2.Rows[4].Cells[1].Value.ToString() != "")
                newAttribute.Italic = this.dataGridView2.Rows[4].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            if (this.dataGridView2.Rows[5].Cells[1].Value.ToString() != "")
            {
                string colstr = this.dataGridView2.Rows[5].Cells[1].Value.ToString();
                Color col = Utils.HexNumberToColor(colstr);
                newSymbol.LineColor = col;
            }
            if (this.dataGridView2.Rows[6].Cells[1].Value.ToString() != "")
                newSymbol.MaxVisualDistance = double.Parse(this.dataGridView2.Rows[6].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[7].Cells[1].Value.ToString() != "")
                newSymbol.MinVisualDistance = double.Parse(this.dataGridView2.Rows[7].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[8].Cells[1].Value.ToString() != "")
                newAttribute.MultilineJustification = StringToGviMultilineJustification(this.dataGridView2.Rows[8].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[9].Cells[1].Value.ToString() != "")
                newSymbol.PivotAlignment = StringToGviPivotAlignment(this.dataGridView2.Rows[9].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[10].Cells[1].Value.ToString() != "")
                newSymbol.Priority = int.Parse(this.dataGridView2.Rows[10].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[11].Cells[1].Value.ToString() != "")
            {
                string colstr = this.dataGridView2.Rows[11].Cells[1].Value.ToString();
                Color col = Utils.HexNumberToColor(colstr);
                newAttribute.TextColor = col;
            }
            if (this.dataGridView2.Rows[12].Cells[1].Value.ToString() != "")
                newAttribute.TextSize = int.Parse(this.dataGridView2.Rows[12].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[13].Cells[1].Value.ToString() != "")
                newAttribute.Underline = this.dataGridView2.Rows[13].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            if (this.dataGridView2.Rows[14].Cells[1].Value.ToString() != "")
                newSymbol.VerticalOffset = double.Parse(this.dataGridView2.Rows[14].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[15].Cells[1].Value.ToString() != "")
                newSymbol.MarginWidth = int.Parse(this.dataGridView2.Rows[15].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[16].Cells[1].Value.ToString() != "")
                newSymbol.MarginHeight = int.Parse(this.dataGridView2.Rows[16].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[17].Cells[1].Value.ToString() != "")
            {
                string colstr = this.dataGridView2.Rows[17].Cells[1].Value.ToString();
                Color col = Utils.HexNumberToColor(colstr);
                newAttribute.OutlineColor = col;
            }
            if (this.dataGridView2.Rows[18].Cells[1].Value.ToString() != "")
                newSymbol.LockMode = StringToGviLockMode(this.dataGridView2.Rows[18].Cells[1].Value.ToString());
            if (this.dataGridView2.Rows[19].Cells[1].Value.ToString() != "")
                newRender.DynamicPlacement = this.dataGridView2.Rows[19].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            if (this.dataGridView2.Rows[20].Cells[1].Value.ToString() != "")
                newRender.MinimizeOverlap = this.dataGridView2.Rows[20].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            if (this.dataGridView2.Rows[21].Cells[1].Value.ToString() != "")
                newRender.RemoveDuplicate = this.dataGridView2.Rows[21].Cells[1].Value.ToString().ToLower() == "true" ? true : false;

            newSymbol.TextAttribute = newAttribute;
            newRender.Symbol = newSymbol;
        }

        public static gviMultilineJustification StringToGviMultilineJustification(string value)
        {
            gviMultilineJustification mode = gviMultilineJustification.gviMultilineLeft;
            switch (value)
            {
                case "gviMultilineCenter":
                    mode = gviMultilineJustification.gviMultilineCenter;
                    break;
                case "gviMultilineRight":
                    mode = gviMultilineJustification.gviMultilineRight;
                    break;
            }
            return mode;
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

        public static gviLockMode StringToGviLockMode(string value)
        {
            gviLockMode mode = gviLockMode.gviLockDecal;
            switch (value)
            {
                case "gviLockAxis":
                    mode = gviLockMode.gviLockAxis;
                    break;
                case "gviLockAxisTextUp":
                    mode = gviLockMode.gviLockAxisTextUp;
                    break;
            }
            return mode;
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using Gvitech.CityMaker.RenderControl;
using Gvitech;

namespace ToolTipShow
{
    public partial class TextRenderForm : Form
    {
        public IToolTipTextRender newRender;
        public object[] cols = null;

        public TextRenderForm(ITextRender textRender, object[] fields)
        {
            InitializeComponent();
            cols = fields;

            // ITextRender
            {
                if (textRender == null)
                    this.textBox1.Text = "";
                else
                    this.textBox1.Text = textRender.Expression.ToString();
            }

            // ISimpleTextRender
            IToolTipTextRender render = textRender as IToolTipTextRender;

            if (render == null)
            {
                render = new ToolTipTextRender();
            }

            string[] row1 = new string[] { "DynamicPlacement", render.DynamicPlacement.ToString() };
            string[] row2 = new string[] { "MinimizeOverlap", render.MinimizeOverlap.ToString() };
            string[] row3 = new string[] { "RemoveDuplicate", render.RemoveDuplicate.ToString() };
            object[] rows = new object[] { row1, row2, row3 };
            foreach (string[] rowArray in rows)
            {
                this.dataGridView2.Rows.Add(rowArray);
            }           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //MessageBox.Show("遵循lua脚本语法 for example：\n 'model_gid:'..$(Groupid)");
            this.textBox1.Text = "'此处文字可随意指定'..$(oid)";

            SelectFieldForm edit = new SelectFieldForm(cols);
            if (edit.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = "'此处文字可随意指定'..$(" + cols[edit.comboBox1.SelectedIndex] + ")";
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
            newRender = new ToolTipTextRender();
            newRender.Expression = this.textBox1.Text;
            
            if (this.dataGridView2.Rows[0].Cells[1].Value.ToString() != "")
                newRender.DynamicPlacement = this.dataGridView2.Rows[0].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            if (this.dataGridView2.Rows[1].Cells[1].Value.ToString() != "")
                newRender.MinimizeOverlap = this.dataGridView2.Rows[1].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
            if (this.dataGridView2.Rows[2].Cells[1].Value.ToString() != "")
                newRender.RemoveDuplicate = this.dataGridView2.Rows[2].Cells[1].Value.ToString().ToLower() == "true" ? true : false;
        }

        
    }
}

using System;
using System.Windows.Forms;
using Gvitech.CityMaker.FdeCore;
using System.Collections;
using Gvitech.CityMaker.Common;
using System.IO;

namespace AttachmentManager
{
    public partial class AttachmentMangerForm : Form
    {
        private IFeatureClass fc = null;
        private IAttachmentManager attcMgr = null;
        private int fid = 0;
        private ArrayList Ids = new ArrayList();

        public AttachmentMangerForm()
        {
            InitializeComponent();
        }

        public AttachmentMangerForm(IFeatureClass featurecls, int id)
            : this()
        {
            fc = featurecls;
            fid = id;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterParent;
            this.dataGridView1.ColumnCount = 4;
            this.dataGridView1.Columns[0].Name = "attcName";
            this.dataGridView1.Columns[0].HeaderText = "附件";
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[0].Visible = true;
            this.dataGridView1.Columns[1].Name = "attcExt";
            this.dataGridView1.Columns[1].Visible = false;
            this.dataGridView1.Columns[2].Name = "attcId";
            this.dataGridView1.Columns[2].Visible = false;
            this.dataGridView1.Columns[3].Name = "attcPath";
            this.dataGridView1.Columns[3].Visible = false;
            attcMgr = fc.GetAttachmentManager();
            IAttachmentCollection attclist = attcMgr.GetAttachmentsByFeatureId(id);
            for (int i = 0; i < attclist.Count; i++)
            {
                IAttachment attc = attclist.Get(i);
                string[] newRow = { attc.Name, "", attc.Id.ToString(), "" };
                this.dataGridView1.Rows.Add(newRow);
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Filter = "All Files|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dlg.FileNames)
                {
                    string[] newRow = { GetFileName(file),"",(-1).ToString(),file };
                    this.dataGridView1.Rows.Add(newRow);
                }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells["attcId"].Value.ToString());
                if (id >= 0)
                    Ids.Add(id);
                this.dataGridView1.Rows.Remove(this.dataGridView1.SelectedRows[0]);
                this.dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (attcMgr == null) return;
                for (int i = 0; i < this.dataGridView1.RowCount; i++)
                {
                    DataGridViewRow saveRow = this.dataGridView1.Rows[i];
                    string filename = saveRow.Cells["attcName"].Value.ToString();
                    string filepath = saveRow.Cells["attcPath"].Value.ToString();
                    int id = Convert.ToInt32(saveRow.Cells["attcId"].Value.ToString());
                    if (id == -1)
                    {
                        IAttachment attc = new Attachment();
                        attc.Name = filename;
                        attc.FeatureId = fid;
                        attc.MimeType = "application/x-tar";
                        attc.Data = GetFileData(filepath);
                        if (attcMgr.AddAttachment(attc) == -1)
                            MessageBox.Show("添加附件\"Path=" + filepath + "\"失败!");
                    }
                }
                foreach (int id in Ids)
                {
                    if (!attcMgr.DeleteAttachment(id))
                        MessageBox.Show("删除附件\"附件ID=" + id.ToString() + "\"失败!");
                }
                MessageBox.Show("编辑附件完成！");
                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 1)
            {
                this.btn_delete.Enabled = true;
            }
            else
                this.btn_delete.Enabled = false;
        }

        private string GetFileName(string filepath)
        {
            string s = "";
            try
            {
                int len1 = filepath.LastIndexOf('\\');
                s = filepath.Substring(len1 + 1, filepath.Length - len1 - 1);
            }
            catch
            { }
            return s;
        }

        private IBinaryBuffer GetFileData(string file)
        {
            IBinaryBuffer bb = new BinaryBuffer();
            FileStream fs = null;
            bool b = false;
            try
            {
                fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                byte[] buf = new byte[fs.Length];
                fs.Read(buf, 0, (int)fs.Length);
                b = bb.FromByteArray(buf);
            }
            catch (Exception ex)
            { }
            finally
            {
                fs.Close();
            }
            if (b)
                return bb;
            else
                return null;
        }

        private void btn_SaveAs_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    bool bSaveSucceed = false;
                    foreach (DataGridViewRow myRow in this.dataGridView1.SelectedRows)
                    {
                        string fileName = myRow.Cells["attcName"].Value.ToString();
                        string filePath = myRow.Cells["attcPath"].Value.ToString();
                        string OutPath = string.Format("{0}\\{1}", dlg.SelectedPath, fileName);
                        int id = Convert.ToInt32(myRow.Cells["attcId"].Value);
                        bSaveSucceed = this.SaveFile(filePath, OutPath, id, true);
                        if (!bSaveSucceed)
                            break;
                    }
                    if(bSaveSucceed)
                        MessageBox.Show("保存成功！");
                    else
                        MessageBox.Show("保存失败！");
                }
                else
                {
                    MessageBox.Show("至少选择一条记录");
                }
            }
        }

        private bool SaveFile(string srcfilepath, string tarfilepath, int id, bool log)
        {
            bool b = false;
            FileStream fs = null;
            try
            {
                if (id >= 0)
                {
                    IAttachment attc = attcMgr.GetAttachment(id);
                    IBinaryBuffer bb = attc.Data;
                    byte[] buf = bb.AsByteArray();
                    fs = File.Create(tarfilepath);
                    fs.Write(buf, 0, buf.Length);
                    fs.Flush();
                }
                else
                {
                    File.Copy(srcfilepath, tarfilepath, true);
                }
                b = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                b = false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return b;
        }
    }
}

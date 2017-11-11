using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;

namespace KmlGroup
{
    public enum TreeNodeType
    {
        NT_DATASOURCE,
        NT_DATASET,
        NT_FEATURECLASS,
        NT_SUBTYPE,
        NT_CODEVALUE,
        NT_IMAGELAYER,
        NT_TERRAINLAYER,
        NT_TiltedLAYER,
        NT_KmlGroup,
        NT_TerrainModifier,
        NT_RenderGeomtry
    }

    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {


            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(false, ps);

            // 设置天空盒
            
            if(System.IO.Directory.Exists(strMediaPath))
            {
                // 创建KMLGroup
                string tmpKmlPath = (strMediaPath + @"\kml+kmz\蒙山.kml");
                IKmlGroup kmlGroup = this.axRenderControl1.ObjectManager.CreateKmlGroup(tmpKmlPath);
                //this.axRenderControl1.Camera.FlyToObject(kmlGroup.Guid, gviActionCode.gviActionFlyTo);
                // 添加节点到界面控件上
                myListNode item = new myListNode(string.Format("{0}_{1}", "蒙山", kmlGroup.Type), TreeNodeType.NT_KmlGroup, kmlGroup);
                item.Checked = true;
                listView1.Items.Add(item);

                tmpKmlPath = (strMediaPath + @"\kml+kmz\内流区诸河.kmz");
                kmlGroup = this.axRenderControl1.ObjectManager.CreateKmlGroup(tmpKmlPath);
                this.axRenderControl1.Camera.FlyToObject(kmlGroup.Guid, gviActionCode.gviActionFlyTo);
                // 添加节点到界面控件上
                item = new myListNode(string.Format("{0}_{1}", "内流区诸河", kmlGroup.Type), TreeNodeType.NT_KmlGroup, kmlGroup);
                item.Checked = true;
                listView1.Items.Add(item);
            }
            else
            {
                MessageBox.Show("请不要随意更改SDK目录名");
                return;
            }      
            
            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "KmlGroup.html";
            }
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            myListNode item = (myListNode)e.Item;
            if (e.Item.Checked)
            {
                switch (item.type)
                {
                    case TreeNodeType.NT_KmlGroup:
                        IKmlGroup kmlGroup = item.obj as IKmlGroup;
                        kmlGroup.SetVisibleMask(gviViewportMask.gviViewAllNormalView);
                        break;
                }
            }
            else
            {
                switch (item.type)
                {
                    case TreeNodeType.NT_KmlGroup:
                        IKmlGroup kmlGroup = item.obj as IKmlGroup;
                        kmlGroup.SetVisibleMask(gviViewportMask.gviViewNone);
                        break;
                }
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0) return;
            myListNode item = (myListNode)this.listView1.SelectedItems[0];
            item.Checked = true;
            switch (item.type)
            {
                case TreeNodeType.NT_KmlGroup:
                    IKmlGroup kmlGroup = item.obj as IKmlGroup;
                    this.axRenderControl1.Camera.FlyToObject(kmlGroup.Guid, gviActionCode.gviActionFlyTo);
                    break;
            }
        }

        private void toolStripButtonAddKMLGroup_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "kml文件(*.kml)|*.kml|kmz文件(*.kmz)|*.kmz";
            if(System.IO.Directory.Exists(strMediaPath))
            {
                od.InitialDirectory = strMediaPath + @"\kml+kmz";
            }
            od.RestoreDirectory = true;
            if (DialogResult.OK == od.ShowDialog())
            {
                IKmlGroup kmlGroup = this.axRenderControl1.ObjectManager.CreateKmlGroup(od.FileName);
                this.axRenderControl1.Camera.FlyToObject(kmlGroup.Guid, gviActionCode.gviActionFlyTo);
                // 添加节点到界面控件上
                myListNode item = new myListNode(string.Format("{0}_{1}", od.FileName, kmlGroup.Type), TreeNodeType.NT_KmlGroup, kmlGroup);
                item.Checked = true;
                listView1.Items.Add(item);
            }
        }

    }

    class myListNode : ListViewItem
    {
        public string name;
        public TreeNodeType type;
        public IRObject obj;

        public myListNode(string n, TreeNodeType t, IRObject o)
        {
            name = n;
            type = t;
            obj = o;
            this.Text = n;
        }
    }
}

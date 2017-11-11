using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Common;
using System.Collections;

namespace MultiSpatialColumns
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private IFeatureClass _featureClass = null;  //“建筑”要素类
        private System.Guid rootId = new System.Guid();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {


            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(true, ps);
            this.axRenderControl1.Camera.FlyTime = 1;

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;

            // 设置天空盒
            
            if(System.IO.Directory.Exists(strMediaPath))
            {
                string tmpSkyboxPath = strMediaPath + @"\skybox";
                ISkyBox skybox = this.axRenderControl1.ObjectManager.GetSkyBox(0);
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\1_BK.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\1_DN.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\1_FR.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\1_LF.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\1_RT.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\1_UP.jpg");   
            }
            else
            {
                MessageBox.Show("请不要随意更改SDK目录名");
                return;
            }

            #region 加载FDB场景
            try
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\MultiSpatialColumns.FDB");
                ci.Database = tmpFDBPath;
                IDataSourceFactory dsFactory = new DataSourceFactory();
                IDataSource ds = dsFactory.OpenDataSource(ci);
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                IFeatureDataSet dataset = ds.OpenFeatureDataset(setnames[0]);
                string[] fcnames = (string[])dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if (fcnames.Length == 0)
                    return;

                // 仅仅加载“建筑”要素类
                _featureClass = dataset.OpenFeatureClass("建筑");

                {
                    // 用名为"Geometry"的空间列创建模型FeautureLayer  
                    IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(_featureClass, "Geometry", null, null, rootId);
                    // 添加节点到界面控件上
                    myListNode item = new myListNode(string.Format("{0}_{1}_{2}", "ModelPoint", _featureClass.Name, featureLayer.MaxVisibleDistance.ToString()), featureLayer);
                    item.Checked = true;
                    listView1.Items.Add(item);
                }
                {
                    //用名为"Surface"的空间列创建模型FeautureLayer  
                    IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(_featureClass, "Surface", null, null, rootId);
                    // 添加节点到界面控件上
                    myListNode item = new myListNode(string.Format("{0}_{1}_{2}", "Surface", _featureClass.Name, featureLayer.MaxVisibleDistance.ToString()), featureLayer);
                    item.Checked = true;
                    listView1.Items.Add(item);
                }
                {
                    //用名为"Polygon"的空间列创建模型FeautureLayer  
                    IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(_featureClass, "Polygon", null, null, rootId);
                    // 添加节点到界面控件上
                    myListNode item = new myListNode(string.Format("{0}_{1}_{2}", "Polygon", _featureClass.Name, featureLayer.MaxVisibleDistance.ToString()), featureLayer);
                    item.Checked = true;
                    listView1.Items.Add(item);
                }
                //{
                //    //用名为"Point"的空间列创建模型FeautureLayer  
                //    IFeatureLayer featureLayer = this.axRenderControl1.FeatureLayerManager.CreateFeatureLayer(_featureClass, "Point", null, null);
                //    // 添加节点到界面控件上
                //    myListNode item = new myListNode(string.Format("{0}_{1}_{2}", "Point", _featureClass.Name, featureLayer.MaxVisibleDistance.ToString()), featureLayer);
                //    item.Checked = true;
                //    listView1.Items.Add(item);
                //}          

                IFieldInfoCollection fieldinfos = _featureClass.GetFields();
                IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf("Geometry"));
                IGeometryDef geometryDef = fieldinfo.GeometryDef;
                IEnvelope env = geometryDef.Envelope;
                IEulerAngle angle = new EulerAngle();
                angle.Set(0, -20, 0);
                this.axRenderControl1.Camera.LookAt(env.Center, 400, angle);
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }
            #endregion

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "MultiSpatialColumns.html";
            }
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                if (e.Item == null)
                    return;
                myListNode item = (myListNode)e.Item;
                if (e.Item.Checked)
                    item.layer.VisibleMask = gviViewportMask.gviViewAllNormalView;
                else
                    item.layer.VisibleMask = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.listView1.SelectedItems.Count == 0) return;
                myListNode item = (myListNode)this.listView1.SelectedItems[0];
                item.Checked = true;
                //this.axRenderControl1.Camera.LookAtEnvelope(item.layer.Envelope);

                IEnvelope env = item.layer.Envelope;
                if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                    env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                    return;
                IEulerAngle angle = new EulerAngle();
                angle.Set(0, -20, 0);
                this.axRenderControl1.Camera.LookAt(env.Center, 500, angle);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
    }

    class myListNode : ListViewItem
    {
        public string name;
        public IFeatureLayer layer;

        public myListNode(string n, IFeatureLayer fl)
        {
            name = n;
            layer = fl;
            this.Text = n;
        }
    }
}

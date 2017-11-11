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

namespace FeatureLayerVisualize
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        IEulerAngle angle = new EulerAngle();
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        private Hashtable layerEnvelopeMap = null;  //IFeatureLayer, IEnvelope 存储所有加载的featurelayer及其对应的envelope

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

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "FeatureLayerVisualize.html";
            }

            layerEnvelopeMap = new Hashtable();

            // 可视化Point类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\point.FDB");
                ci.Database = tmpFDBPath;
                FeatureLayerVisualize(ci, true, "Point");
            }

            // 可视化Polyline类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\polyline.FDB");
                ci.Database = tmpFDBPath;
                FeatureLayerVisualize(ci, false, "Polyline");
            }

            // 可视化Polygon类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\polygon.FDB");
                ci.Database = tmpFDBPath;
                FeatureLayerVisualize(ci, false, "Polygon");
            }

            // 可视化ModelPoint类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\SDKDEMO.FDB");
                ci.Database = tmpFDBPath;
                FeatureLayerVisualize(ci, false, "ModelPoint");
            }
        }

        // 公共方法
        void FeatureLayerVisualize(IConnectionInfo ci, bool needfly, string sourceName)
        {
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                IDataSource ds = dsFactory.OpenDataSource(ci);
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                IFeatureDataSet dataset = ds.OpenFeatureDataset(setnames[0]);
                string[] fcnames = (string[])dataset.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if (fcnames.Length == 0)
                    return;
                fcMap = new Hashtable(fcnames.Length);
                foreach (string name in fcnames)
                {
                    IFeatureClass fc = dataset.OpenFeatureClass(name);
                    // 找到空间列字段
                    List<string> geoNames = new List<string>();
                    IFieldInfoCollection fieldinfos = fc.GetFields();
                    for (int i = 0; i < fieldinfos.Count; i++)
                    {
                        IFieldInfo fieldinfo = fieldinfos.Get(i);
                        if (null == fieldinfo)
                            continue;
                        IGeometryDef geometryDef = fieldinfo.GeometryDef;
                        if (null == geometryDef)
                            continue;
                        geoNames.Add(fieldinfo.Name);
                    }
                    fcMap.Add(fc, geoNames);
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }

            // CreateFeautureLayer
            bool hasfly = !needfly;  
            foreach (IFeatureClass fc in fcMap.Keys)
            {
                List<string> geoNames = (List<string>)fcMap[fc];
                foreach (string geoName in geoNames)
                {
                    IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                    fc, geoName, null, null, rootId);

                    // 添加节点到界面控件上
                    myListNode item = new myListNode(string.Format("{0}_{1}_{2}", sourceName, fc.Name, featureLayer.MaxVisibleDistance.ToString()), featureLayer);
                    item.Checked = true;
                    listView1.Items.Add(item);

                    IFieldInfoCollection fieldinfos = fc.GetFields();
                    IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                    IGeometryDef geometryDef = fieldinfo.GeometryDef;
                    IEnvelope env = geometryDef.Envelope;
                    layerEnvelopeMap.Add(featureLayer, env);
                    if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                        env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                        continue;

                    // 相机飞入
                    if (!hasfly)
                    {
                        angle.Set(0, -20, 0);
                        this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                    }
                    hasfly = true;
                }
            }
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            myListNode item = (myListNode)e.Item;
            if (e.Item.Checked)
                item.layer.VisibleMask = gviViewportMask.gviViewAllNormalView;
            else
                item.layer.VisibleMask = gviViewportMask.gviViewNone;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0) return;
            myListNode item = (myListNode)this.listView1.SelectedItems[0];
            item.Checked = true;
            //this.axRenderControl1.Camera.LookAtEnvelope(item.layer.Envelope);

            IEnvelope env = (IEnvelope)layerEnvelopeMap[item.layer];
            if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                return;
            this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
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

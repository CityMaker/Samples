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
using System;

namespace GeometrySymbolScript
{
    public enum LayerType
    {
        Point,
        Polyline,
        Polygon,
        ModelPoint
    };

    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        IEulerAngle angle = new EulerAngle();
        private Hashtable fcGeoMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private Hashtable layerEnvelopeMap = null;  //IFeatureLayer, IEnvelope 存储所有加载的featurelayer及其对应的envelope
        private Hashtable fcGuidMap = new Hashtable();

        private IFeatureLayer selectedLayer = null;
        private LayerType type;

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
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\7_BK.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\7_DN.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\7_FR.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\7_LF.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\7_RT.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\7_UP.jpg");   
            }
            else
            {
                MessageBox.Show("请不要随意更改SDK目录名");
                return;
            }

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "GeometrySymbolScript.html";
            }

            layerEnvelopeMap = new Hashtable();

            // 可视化Point类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\point.FDB");
                ci.Database = tmpFDBPath;

                // *****定义几何物体渲染风格*****
                ISimpleGeometryRender geoRender = new SimpleGeometryRender();
                //-- 配置为简单点 --
                ISimplePointSymbol geoSymbol = new SimplePointSymbol();
                geoSymbol.Script = "<Size>$(Groupid)*10</Size><FillColor>$(OBJECTID)</FillColor>";
                //-- 配置为图片点 --
                //IImagePointSymbol geoSymbol = new ImagePointSymbol();
                //geoSymbol.Script = "<Size>$(Groupid)*10</Size><ImageName>$(ImageName)</ImageName>";
                geoRender.Symbol = geoSymbol;

                type = LayerType.Point;
                FeatureLayerVisualize(ci, true, "Point", null, geoRender, type);
            }

            // 可视化Polyline类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\polyline.FDB");
                ci.Database = tmpFDBPath;

                // *****定义几何物体渲染风格*****
                ISimpleGeometryRender geoRender = new SimpleGeometryRender();
                ICurveSymbol geoSymbol = new CurveSymbol();
                geoSymbol.Script = "<Color>System.Drawing.Color.Yellow</Color><Width>$(Groupid)*10</Width><ImageName>$(ImageName)</ImageName><RepeatLength>100</RepeatLength>";
                geoRender.Symbol = geoSymbol;

                type = LayerType.Polyline;
                FeatureLayerVisualize(ci, false, "Polyline", null, geoRender, type);
            }

            // 可视化Polygon类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\polygon.FDB");
                ci.Database = tmpFDBPath;

                // *****定义几何物体渲染风格*****
                ISimpleGeometryRender geoRender = new SimpleGeometryRender();
                //-- 配置为简单面 --
                //ISurfaceSymbol geoSymbol = new SurfaceSymbol();
                //geoSymbol.Script = "<Color>$(CommunitID)</Color>";
                //-- 配置为立体矩形 -- 
                IPolygon3DSymbol geoSymbol = new Polygon3DSymbol();
                geoSymbol.Script = "<Color>$(CommunitID)</Color><Height>$(oid)</Height>";
                ICurveSymbol curveSymbol = new CurveSymbol();
                curveSymbol.Script = "<Color>$(CommunitID)</Color>";
                geoSymbol.BoundarySymbol = curveSymbol;
                geoRender.Symbol = geoSymbol;

                type = LayerType.Polygon;
                FeatureLayerVisualize(ci, false, "Polygon", null, geoRender, type);
            }

            // 可视化ModelPoint类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\SDKDEMO.FDB");
                ci.Database = tmpFDBPath;

                // *****定义几何物体渲染风格*****
                ISimpleGeometryRender geoRender = new SimpleGeometryRender();
                IModelPointSymbol geoSymbol = new ModelPointSymbol();
                geoSymbol.Script = "<Color>System.Drawing.Color.Yellow</Color>";
                geoSymbol.EnableColor = true;  // 记得要开启!
                geoRender.Symbol = geoSymbol;

                type = LayerType.ModelPoint;
                FeatureLayerVisualize(ci, false, "ModelPoint", null, geoRender, type);
            }
        }

        // 公共方法
        void FeatureLayerVisualize(IConnectionInfo ci, bool needfly, string sourceName,
            ITextRender textRender, IGeometryRender geoRender, LayerType type)
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
                fcGeoMap = new Hashtable(fcnames.Length);
                foreach (string name in fcnames)
                {
                    IFeatureClass fc = dataset.OpenFeatureClass(name);
                    fcGuidMap.Add(fc.Guid, fc);
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
                    fcGeoMap.Add(fc, geoNames);
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }

            // CreateFeautureLayer
            bool hasfly = !needfly;  
            foreach (IFeatureClass fc in fcGeoMap.Keys)
            {
                List<string> geoNames = (List<string>)fcGeoMap[fc];
                foreach (string geoName in geoNames)
                {
                    if (!geoName.Equals("Geometry"))
                        continue;

                    IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                    fc, geoName, textRender, geoRender, rootId);

                    // 添加节点到界面控件上
                    myListNode item = new myListNode(string.Format("{0}_{1}_{2}", sourceName, fc.Name, featureLayer.MaxVisibleDistance.ToString()), featureLayer, type);
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

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            myListNode item = (myListNode)this.listView1.GetItemAt(e.X, e.Y);
            if (item == null) return;

            cbCOMProperties.Items.Clear();
            selectedLayer = item.layer;
            IGeometryRender render = selectedLayer.GetGeometryRender();
            if (render != null)
            {
                if (render.RenderType == gviRenderType.gviRenderSimple)
                {
                    IGeometrySymbol geoSymbol = (render as ISimpleGeometryRender).Symbol;
                    txtScript.Text = geoSymbol.Script;

                    switch (geoSymbol.SymbolType)
                    {
                        case gviGeometrySymbolType.gviGeoSymbolPoint:
                            {
                                cbCOMProperties.Items.Add("Size");
                                cbCOMProperties.Items.Add("FillColor");
                            }
                            break;
                        case gviGeometrySymbolType.gviGeoSymbolModelPoint:
                            {
                                cbCOMProperties.Items.Add("Color");
                            }
                            break;
                        case gviGeometrySymbolType.gviGeoSymbolImagePoint:
                            {
                                cbCOMProperties.Items.Add("Size");
                                cbCOMProperties.Items.Add("ImageName");
                            }
                            break;
                        case gviGeometrySymbolType.gviGeoSymbolCurve:
                            {
                                cbCOMProperties.Items.Add("Color");
                                cbCOMProperties.Items.Add("ImageName");
                                cbCOMProperties.Items.Add("RepeatLength");
                                cbCOMProperties.Items.Add("Width");
                            }
                            break;
                        case gviGeometrySymbolType.gviGeoSymbolSurface:
                            {
                                cbCOMProperties.Items.Add("Color");
                            }
                            break;
                        case gviGeometrySymbolType.gviGeoSymbol3DPolygon:
                            {
                                cbCOMProperties.Items.Add("Color");
                                cbCOMProperties.Items.Add("Height");
                            }
                            break;
                    }
                }
            }
            else
            {
                switch (item.type)
                {
                    case LayerType.ModelPoint:
                        {
                            cbCOMProperties.Items.Add("Color");
                        }
                        break;
                    case LayerType.Polyline:
                        {
                            cbCOMProperties.Items.Add("Color");
                            cbCOMProperties.Items.Add("ImageName");
                            cbCOMProperties.Items.Add("RepeatLength");
                            cbCOMProperties.Items.Add("Width");
                        }
                        break;
                    case LayerType.Point:
                        {
                            cbCOMProperties.Items.Add("Size");
                            cbCOMProperties.Items.Add("FillColor");
                            cbCOMProperties.Items.Add("ImageName");
                        }
                        break;
                    case LayerType.Polygon:
                        {
                            cbCOMProperties.Items.Add("Color");
                            cbCOMProperties.Items.Add("Height");
                        }
                        break;
                }
            }

            cbFields.Items.Clear();
            IFeatureClass fc = null;
            try
            {
                foreach (System.Guid guid in fcGuidMap.Keys)
                {
                    if (guid.Equals(selectedLayer.FeatureClassId))
                        fc = fcGuidMap[guid] as IFeatureClass;
                }

                IFieldInfoCollection fields = fc.GetFields();
                for (int i = 0; i < fields.Count; i++)
                {
                    IFieldInfo field = fields.Get(i);
                    if (field.FieldType == gviFieldType.gviFieldGeometry || field.FieldType == gviFieldType.gviFieldBlob)
                        continue;
                    cbFields.Items.Add(field.Name);
                }
            }
            catch (System.Exception ex)
            {

            }

            type = item.type;
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
            IEnvelope env = (IEnvelope)layerEnvelopeMap[item.layer];
            if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                return;
            this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
        }

        private void btnClear_Click(object sender, System.EventArgs e)
        {
            this.txtScript.Clear();
        }

        private void btnApply_Click(object sender, System.EventArgs e)
        {
            string script = this.txtScript.Text.Trim();
            if (script != "")
            {
                IGeometryRender render = selectedLayer.GetGeometryRender();
                if (render != null)
                {
                    if (render.RenderType == gviRenderType.gviRenderSimple)
                    {
                        ISimpleGeometryRender simpleRender = render as ISimpleGeometryRender;
                        IGeometrySymbol geoSymbol = simpleRender.Symbol;
                        geoSymbol.Script = script;
                        simpleRender.Symbol = geoSymbol;
                        if (!selectedLayer.SetGeometryRender(simpleRender))
                            MessageBox.Show("错误码为：" + this.axRenderControl1.GetLastError());
                    }
                }
                else
                {
                    ISimpleGeometryRender simpleRender = new SimpleGeometryRender();
                    switch (type)
                    {
                        case LayerType.ModelPoint:
                            {
                                IModelPointSymbol mps = new ModelPointSymbol();
                                mps.Script = script;
                                simpleRender.Symbol = mps;
                            }
                            break;
                        case LayerType.Polyline:
                            {
                                ICurveSymbol cs = new CurveSymbol();
                                cs.Script = script;
                                simpleRender.Symbol = cs;
                            }
                            break;
                        case LayerType.Point:
                            {
                                if (script.Contains("<ImageName>"))
                                {
                                    IImagePointSymbol ips = new ImagePointSymbol();
                                    ips.Script = script;
                                    simpleRender.Symbol = ips;
                                }
                                else
                                {
                                    ISimplePointSymbol sps = new SimplePointSymbol();
                                    sps.Script = script;
                                    simpleRender.Symbol = sps;
                                }
                            }
                            break;
                        case LayerType.Polygon:
                            {
                                if (script.Contains("<Height>"))
                                {
                                    IPolygon3DSymbol p3s = new Polygon3DSymbol();
                                    p3s.Script = script;
                                    simpleRender.Symbol = p3s;
                                }
                                else
                                {
                                    ISurfaceSymbol ss = new SurfaceSymbol();
                                    ss.Script = script;
                                    simpleRender.Symbol = ss;
                                }
                            }
                            break;
                    }
                    if (!selectedLayer.SetGeometryRender(simpleRender))
                        MessageBox.Show("错误码为：" + this.axRenderControl1.GetLastError());
                }
            }
            else
                selectedLayer.SetGeometryRender(null);
        }

        private void cbCOMProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = this.cbCOMProperties.Text;
            string value = this.txtScript.Text;
            this.txtScript.Text = value + "<" + text + "></" + text + ">";
        }

        private void cbFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = this.cbFields.Text;
            string value = this.txtScript.Text;  //$(Groupid)
            this.txtScript.Text = value + "$(" + text + ")";
        }

    }

    class myListNode : ListViewItem
    {
        public string name;
        public IFeatureLayer layer;
        public LayerType type;

        public myListNode(string n, IFeatureLayer fl, LayerType t)
        {
            name = n;
            layer = fl;
            type = t;
            this.Text = n;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using System.Xml;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;


namespace ToolTipShow
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private IEulerAngle angle = new EulerAngle();
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        private Hashtable layerEnvelopeMap = null;  //IFeatureLayer, IEnvelope 存储所有加载的featurelayer及其对应的envelope

        private Hashtable layerFcMap = null;  //IFeatureLayer, IFeatureClass 存储所有加载的featurelayer及其对应的featureclass
        private ListViewItem selectNode = null;  //标记listView控件中当前被选中的节点

        private System.Guid rootId = new System.Guid();
        private ISpatialCRS crs = null;

        public MainForm()
        {
            InitializeComponent();           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 初始化RenderControl控件
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl1.Initialize(false, ps);

            rootId = this.axRenderControl1.ObjectManager.GetProjectTree().RootID;
            this.axRenderControl1.Camera.FlyTime = 0;

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
                this.helpProvider1.HelpNamespace = "ToolTipShow.html";
            }

            layerEnvelopeMap = new Hashtable();
            layerFcMap = new Hashtable();

            // 可视化Point类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\point.FDB");
                ci.Database = tmpFDBPath;

                // *******定义文字渲染风格*******
                IToolTipTextRender textRender = new ToolTipTextRender();
                textRender.Expression = "''..$(oid)";

                ISimpleGeometryRender geoRender = new SimpleGeometryRender();
                IImagePointSymbol geoSymbol = new ImagePointSymbol();   //将点以图片的形式显示出来
                geoSymbol.ImageName = "huang.png";  //使用素材库里存在的图片
                geoSymbol.Size = 25;
                geoRender.Symbol = geoSymbol;

                FeatureLayerVisualize(ci, true, "Point", textRender, geoRender);
            }
            // 可视化Polyline类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\polyline.FDB");
                ci.Database = tmpFDBPath;

                // *******定义文字渲染风格*******
                IToolTipTextRender textRender = new ToolTipTextRender();
                textRender.Expression = "''..$(oid)";

                ISimpleGeometryRender geoRender = new SimpleGeometryRender();
                ICurveSymbol geoSymbol = new CurveSymbol();
                geoSymbol.Color = System.Drawing.Color.Purple;  //线颜色为Purple
                geoSymbol.Width = 5;
                geoRender.Symbol = geoSymbol;

                FeatureLayerVisualize(ci, false, "Polyline", textRender, geoRender);
            }

            // 可视化Polygon类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\polygon.FDB");
                ci.Database = tmpFDBPath;

                // *******定义文字渲染风格*******
                IToolTipTextRender textRender = new ToolTipTextRender();
                textRender.Expression = "''..$(oid)";

                ISimpleGeometryRender geoRender = new SimpleGeometryRender();
                ISurfaceSymbol geoSymbol = new SurfaceSymbol();
                geoSymbol.Color = System.Drawing.Color.Yellow;  //面填充色为Yellow
                geoRender.Symbol = geoSymbol;

                FeatureLayerVisualize(ci, false, "Polygon", textRender, geoRender);
            }

            // 可视化ModelPoint类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\community.FDB");
                ci.Database = tmpFDBPath;

                // *******定义文字渲染风格*******
                IToolTipTextRender textRender = new ToolTipTextRender();
                textRender.Expression = "''..$(oid)";

                FeatureLayerVisualize(ci, false, "ModelPoint", textRender, null);
            }
        }

        // 公共方法
        void FeatureLayerVisualize(IConnectionInfo ci, bool needfly, string sourceName,
            ITextRender textRender, IGeometryRender geoRender)
        {
            IDataSourceFactory dsFactory = null;
            IDataSource ds = null;
            IFeatureDataSet dataset = null;
            try
            {
                dsFactory = new DataSourceFactory();
                ds = dsFactory.OpenDataSource(ci);
                string[] setnames = (string[])ds.GetFeatureDatasetNames();
                if (setnames.Length == 0)
                    return;
                dataset = ds.OpenFeatureDataset(setnames[0]);
                crs = dataset.SpatialReference;
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

                // CreateFeautureLayer
                bool hasfly = !needfly;
                foreach (IFeatureClass fcInMap in fcMap.Keys)
                {
                    List<string> geoNames = (List<string>)fcMap[fcInMap];
                    foreach (string geoName in geoNames)
                    {
                        IFeatureLayer featureLayer = this.axRenderControl1.ObjectManager.CreateFeatureLayer(
                        fcInMap, geoName, textRender, geoRender, rootId);

                        // 添加节点到界面控件上
                        object[] registeredFields = GetRegisteredRenderIndexFields(fcInMap);
                        myListNode item = new myListNode(string.Format("{0}_{1}_{2}", sourceName, fcInMap.Name, featureLayer.MaxVisibleDistance.ToString()), featureLayer, registeredFields);
                        item.Checked = true;
                        listView1.Items.Add(item);
                        layerFcMap.Add(featureLayer, fcInMap);

                        IFieldInfoCollection fieldinfos = fcInMap.GetFields();
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
                            IPoint pos = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            pos.SpatialCRS = crs;
                            pos.Position = env.Center;
                            this.axRenderControl1.Camera.LookAt2(pos, 1000, angle);
                        }
                        hasfly = true;
                    }
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return;
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
                return;
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
                if (dataset != null)
                {
                    dataset.Dispose();
                    dataset = null;
                }
                //if (fc != null)
                //{
                //    fc.Dispose();
                //    fc = null;
                //}
            }
        }

        private object[] GetRegisteredRenderIndexFields(IFeatureClass fc)
        {
            ArrayList list = new ArrayList();
            IFieldInfoCollection cols = fc.GetFields();
            for (int i = 0; i < cols.Count; i++)
            {
                IFieldInfo field = cols.Get(i);
                if (field.RegisteredRenderIndex == true)
                    list.Add(field.Name);
            }
            return list.ToArray();
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
            IPoint pos = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            pos.SpatialCRS = crs;
            pos.Position = env.Center;
            this.axRenderControl1.Camera.LookAt2(pos, 1000, angle);
        }


        #region 配置渲染风格
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                this.contextMenuStrip1.Visible = false;
                this.contextMenuStrip1.Items[0].Visible = false;
                this.contextMenuStrip1.Items[1].Visible = false;
                this.contextMenuStrip1.Items[2].Visible = false;
                this.contextMenuStrip1.Items[3].Visible = false;
                return;
            }

            selectNode = this.listView1.GetItemAt(e.X, e.Y);

            if (selectNode == null)
            {
                this.contextMenuStrip1.Visible = false;
                this.contextMenuStrip1.Items[0].Visible = false;
                this.contextMenuStrip1.Items[1].Visible = false;
                this.contextMenuStrip1.Items[2].Visible = false;
                this.contextMenuStrip1.Items[3].Visible = false;
            }
            else
            {
                this.contextMenuStrip1.Visible = true;
                this.contextMenuStrip1.Items[0].Visible = true;
                this.contextMenuStrip1.Items[1].Visible = true;
                this.contextMenuStrip1.Items[2].Visible = true;
                this.contextMenuStrip1.Items[3].Visible = true;
            }
        }

        private void toolStripSetTextRender_Click(object sender, EventArgs e)
        {
            myListNode node = selectNode as myListNode;
            TextRenderForm trform = new TextRenderForm(node.layer.GetTextRender(), node.fields);
            if (trform.ShowDialog() == DialogResult.OK)
            {
                node.layer.SetTextRender(trform.newRender);
                IFeatureClass fc = layerFcMap[node.layer] as IFeatureClass;
                this.axRenderControl1.FeatureManager.RefreshFeatureClass(fc);
            }
        }

        private void toolStripUnloadTextRender_Click(object sender, EventArgs e)
        {
            myListNode node = selectNode as myListNode;
            IFeatureLayer layer = node.layer;
            layer.SetTextRender(null);
            IFeatureClass fc = layerFcMap[layer] as IFeatureClass;
            this.axRenderControl1.FeatureManager.RefreshFeatureClass(fc);
        }

        private void exportTextRenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nodeName = selectNode.Text;
            myListNode node = selectNode as myListNode;
            IFeatureLayer layer = node.layer;
            string renderStr = layer.GetTextRender().AsXml();

            SaveFileDialog sd = new SaveFileDialog();
            sd.AddExtension = true;
            sd.DefaultExt = "xml";
            sd.Filter = "XML文件|*.xml";
            sd.RestoreDirectory = true;
            if (sd.ShowDialog() == DialogResult.OK)
            {
                String final = sd.FileName;
                if (sd.FileName.LastIndexOf(".xml") == -1)
                    final = String.Format("{0}.xml", sd.FileName);
                System.IO.File.WriteAllText(final, renderStr);
                MessageBox.Show("导出成功");
            }
        }

        private void importTextRenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "XML文件|*.xml";
            od.RestoreDirectory = true;
            if (od.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(od.FileName))
                {
                    StreamReader sr = new StreamReader(od.FileName);
                    string xmlstring = sr.ReadToEnd();
                    sr.Close();

                    ITextRender tr = this.axRenderControl1.ObjectManager.CreateTextRenderFromXML(xmlstring);
                    string nodeName = selectNode.Text;
                    myListNode node = selectNode as myListNode;
                    IFeatureLayer layer = node.layer;
                    layer.SetTextRender(tr);
                }
            }
        }
       
        #endregion


    }

    class myListNode : ListViewItem
    {
        public string name;
        public IFeatureLayer layer;
        public object[] fields;

        public myListNode(string n, IFeatureLayer fl, object[] fs)
        {
            name = n;
            layer = fl;
            fields = fs;
            this.Text = n;
        }
    }
}

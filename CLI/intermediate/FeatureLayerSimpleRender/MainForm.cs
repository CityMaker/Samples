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


namespace FeatureLayerSimpleRender
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

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "FeatureLayerSimpleRender.html";
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
                ISimpleTextRender textRender = new SimpleTextRender();
                textRender.Expression = "''..$(oid)";
                ITextSymbol textSymbol = new TextSymbol();
                TextAttribute textAttribute = new TextAttribute();
                textAttribute.TextColor = System.Drawing.Color.Red;
                textSymbol.TextAttribute = textAttribute;
                textSymbol.PivotAlignment = gviPivotAlignment.gviPivotAlignBottomCenter;
                textSymbol.VerticalOffset = 10;
                // 注意：必须设置symbol，默认文字不显示
                textRender.Symbol = textSymbol;  
            
                // *****定义几何物体渲染风格*****
                ISimpleGeometryRender geoRender = new SimpleGeometryRender();
                // 特别注意：此处设置了逻辑组字段，则需要在CreateFeatureLayer后设置组的可见性。否则，默认FeatureLayer将不可见。
                // 注意：必须用已注册RenderIndex的字段，否则CreateFeatureLayer创建不成功返回null
                geoRender.RenderGroupField = "Groupid";
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
                ISimpleTextRender textRender = new SimpleTextRender();
                textRender.Expression = "''..$(oid)";

                // *****定义几何物体渲染风格*****
                ISimpleGeometryRender geoRender = new SimpleGeometryRender();
                // 特别注意：此处设置了逻辑组字段，则需要在CreateFeatureLayer后设置组的可见性。否则，默认FeatureLayer将不可见。
                geoRender.RenderGroupField = "Groupid";
                ICurveSymbol geoSymbol = new CurveSymbol();
                geoSymbol.Color = System.Drawing.Color.Red;  //线颜色为Purple
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
                ISimpleTextRender textRender = new SimpleTextRender();
                textRender.Expression = "''..$(oid)";

                // *****定义几何物体渲染风格*****
                ISimpleGeometryRender geoRender = new SimpleGeometryRender();
                // 特别注意：此处设置了逻辑组字段，则需要在CreateFeatureLayer后设置组的可见性。否则，默认FeatureLayer将不可见。
                geoRender.RenderGroupField = "Groupid";
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
                ISimpleTextRender textRender = new SimpleTextRender();
                textRender.Expression = "''..$(oid)";

                // *****定义几何物体渲染风格*****
                ISimpleGeometryRender geoRender = new SimpleGeometryRender();
                //特别注意：此处设置了逻辑组字段，则需要在CreateFeatureLayer后设置组的可见性。否则，默认FeatureLayer将不可见。
                geoRender.RenderGroupField = "Groupid";
                IModelPointSymbol geoSymbol = new ModelPointSymbol();
                geoSymbol.Color = System.Drawing.Color.Yellow;  //模型颜色为Red
                geoSymbol.EnableColor = true;  //需开启，否则颜色设置无效
                geoRender.Symbol = geoSymbol;

                FeatureLayerVisualize(ci, false, "ModelPoint", textRender, geoRender);
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

                        // 设置featureLayer组可见
                        if (!geoRender.RenderGroupField.Equals(""))
                        {
                            SetGroupVisiable(dataset, featureLayer);
                        }

                        // 添加节点到界面控件上
                        myListNode item = new myListNode(string.Format("{0}_{1}_{2}", sourceName, fcInMap.Name, featureLayer.MaxVisibleDistance.ToString()), featureLayer);
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
                            this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
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
                if (dsFactory != null)
                {
                    //Marshal.ReleaseComObject(dsFactory);
                    dsFactory = null;
                }
                if (ds != null)
                {
                    //Marshal.ReleaseComObject(ds);
                    ds = null;
                }
                if (dataset != null)
                {
                    //Marshal.ReleaseComObject(dataset);
                    dataset = null;
                }
                //if (fc != null)
                //{
                //    //Marshal.ReleaseComObject(fc);
                //    fc = null;
                //}
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

        #region 读数据库获取逻辑树xml内容
        private byte[] GetLogicTreeContent(IFeatureDataSet dataset)
        {
            byte[] strContent = null;

            try
            {
                IQueryDef qd = dataset.DataSource.CreateQueryDef();
                qd.AddSubField("content");

                qd.Tables = new String[] { "cm_logictree", "cm_group" };
                qd.WhereClause = String.Format("cm_group.groupuid = cm_logictree.groupid "
                                + " and cm_group.DataSet = '{0}'", dataset.Name);

                IFdeCursor cursor = qd.Execute(false);
                IRowBuffer row = null;
                if ((row = cursor.NextRow()) != null)
                {
                    //content
                    int nPose = row.FieldIndex("content");
                    if (nPose != -1)
                    {
                        IBinaryBuffer bb = row.GetValue(nPose) as IBinaryBuffer;
                        strContent = (byte[])bb.AsByteArray();
                    }
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return null;
            }

            return strContent;
        }
        #endregion


        #region 解析逻辑树xml设置FeatureLayer可见
        private void SetGroupVisiable(IFeatureDataSet dataset, IFeatureLayer featureLayer)
        {
            byte[] bb = GetLogicTreeContent(dataset);
            if (bb == null)
                return;
            MemoryStream ms = new MemoryStream(bb);
            XmlDocument doc = new XmlDocument();
            doc.Load(ms);
            XmlNode rootNode = doc.DocumentElement;
            if (rootNode != null && rootNode.HasChildNodes)
            {
                TravelXML(rootNode.ChildNodes[0], featureLayer);
            }
        }

        private void TravelXML(XmlNode pNode, IFeatureLayer fLayer)
        {
            if (pNode == null)
                return;
            string nodeName = pNode.Name;
            if (nodeName.ToLower() == "pgroup" || nodeName.ToLower() == "agroup")
            {
                XmlAttribute att = pNode.Attributes["ID"];
                if (att != null)
                {
                    int grpId;
                    if (int.TryParse(att.Value, out grpId) && fLayer != null)
                    {
                        fLayer.SetGroupVisibleMask(grpId, gviViewportMask.gviViewAllNormalView);
                    }
                }
            }
            else if (nodeName.ToLower() == "pgroups" || nodeName.ToLower() == "agroups")
            {
                foreach (XmlNode node in pNode.ChildNodes)
                {
                    TravelXML(node, fLayer);
                }
            }
        }
        #endregion

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
            TextRenderForm trform = new TextRenderForm(node.layer.GetTextRender());
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

        private void toolStripSetGeometryRender_Click(object sender, EventArgs e)
        {
            string nodeName = selectNode.Text;
            myListNode node = selectNode as myListNode;
            IFeatureLayer layer = node.layer;
            // 获取注册了RenderIndex的字段名集合
            IFeatureClass fc = layerFcMap[node.layer] as IFeatureClass;
            ArrayList fieldNamesWithRegisterRenderIndex = new ArrayList();
            fieldNamesWithRegisterRenderIndex.Add("");
            IFieldInfoCollection fields = fc.GetFields();
            for (int i = 0; i < fields.Count; i++)
            {
                if (fields.Get(i).RegisteredRenderIndex)
                    fieldNamesWithRegisterRenderIndex.Add(fields.Get(i).Name);
            }
            switch (nodeName.Split('_')[0])
            {                    
                case "ModelPoint":
                    {                        
                        ModelPointRenderForm trform = new ModelPointRenderForm(node.layer.GetGeometryRender(), fieldNamesWithRegisterRenderIndex.ToArray());
                        if (trform.ShowDialog() == DialogResult.OK)
                        {
                            node.layer.SetGeometryRender(trform.newRender);                            
                            this.axRenderControl1.FeatureManager.RefreshFeatureClass(fc);
                        }
                    }
                    break;
                case "Point":
                    {
                        SelectPointStyleForm selectStyleForm = new SelectPointStyleForm();
                        if (selectStyleForm.ShowDialog() == DialogResult.OK)
                        {
                            PointRenderForm trform;
                            if (selectStyleForm.radioButtonSimplePoint.Checked)
                                trform = new PointRenderForm(node.layer.GetGeometryRender(), fieldNamesWithRegisterRenderIndex.ToArray(), true);
                            else
                                trform = new PointRenderForm(node.layer.GetGeometryRender(), fieldNamesWithRegisterRenderIndex.ToArray(), false);
                            if (trform.ShowDialog() == DialogResult.OK)
                            {
                                node.layer.SetGeometryRender(trform.newRender);
                                this.axRenderControl1.FeatureManager.RefreshFeatureClass(fc);
                            }
                        }
                    }
                    break;
                case "Polyline":
                    {
                        PolylineRenderForm trform = new PolylineRenderForm(node.layer.GetGeometryRender(), fieldNamesWithRegisterRenderIndex.ToArray());
                        if (trform.ShowDialog() == DialogResult.OK)
                        {
                            node.layer.SetGeometryRender(trform.newRender);
                            this.axRenderControl1.FeatureManager.RefreshFeatureClass(fc);
                        }
                    }
                    break;
                case "Polygon":
                    {
                        PolygonRenderForm trform = new PolygonRenderForm(node.layer.GetGeometryRender(), fieldNamesWithRegisterRenderIndex.ToArray());
                        if (trform.ShowDialog() == DialogResult.OK)
                        {
                            node.layer.SetGeometryRender(trform.newRender);
                            this.axRenderControl1.FeatureManager.RefreshFeatureClass(fc);
                        }
                    }
                    break;
            }
        }

        private void toolStripUnloadGeometryRender_Click(object sender, EventArgs e)
        {
            myListNode node = selectNode as myListNode;
            IFeatureLayer layer = node.layer;
            layer.SetGeometryRender(null);
            IFeatureClass fc = layerFcMap[layer] as IFeatureClass;
            this.axRenderControl1.FeatureManager.RefreshFeatureClass(fc);
        }
        #endregion

        private void exportGeometryRenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nodeName = selectNode.Text;
            myListNode node = selectNode as myListNode;
            IFeatureLayer layer = node.layer;
            string renderStr = layer.GetGeometryRender().AsXml();

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

        private void importGeometryRenderToolStripMenuItem_Click(object sender, EventArgs e)
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

                    IGeometryRender gr = this.axRenderControl1.ObjectManager.CreateGeometryRenderFromXML(xmlstring);
                    string nodeName = selectNode.Text;
                    myListNode node = selectNode as myListNode;
                    IFeatureLayer layer = node.layer;
                    layer.SetGeometryRender(gr);
                }
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

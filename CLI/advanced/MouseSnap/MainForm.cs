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
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Controls;

namespace MouseSnap
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        IEulerAngle angle = new EulerAngle();
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
        private Hashtable layerEnvelopeMap = null;  //IFeatureLayer, IEnvelope 存储所有加载的featurelayer及其对应的envelope
        private Hashtable fcGUIDMap = null;  //featureclass.GUID, IFeatureClass 存储featureclass及对应的GUID
        private IObjectEditor _geoEditor = null;
        private bool resultCode;

        CheckBox enableSnapBox;  //开启捕捉复选框
        ToolStripControlHost host;

        private IFeatureClass _featureClass = null;
        private IFeatureLayer _featureLayer = null;
        private IRowBuffer _buffer = null;  //标记当前正在编辑的Feature
        private IGeometry oldfdeGeometry = null;  //记录原始Feature，用于存数据库失败时恢复位置

        private System.Guid rootId = new System.Guid();

        // 线程转发
        public int MainThreadId = 0;
        

        public MainForm()
        {
            InitializeComponent();

            MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;             

            enableSnapBox = new CheckBox();
            enableSnapBox.Checked = true;
            enableSnapBox.Text = "开启捕捉";
            host = new ToolStripControlHost(enableSnapBox);
            toolStrip1.Items.Add(host);            
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

            // 开启顶点捕捉
            this.axRenderControl1.MouseSnapMode = gviMouseSnapMode.gviMouseSnapVertex;
            _geoEditor = this.axRenderControl1.ObjectEditor;

            enableSnapBox.CheckedChanged += new System.EventHandler(enableSnapBox_CheckedChanged);
            this.toolStripComboBoxMeasure.SelectedIndex = 0;
            this.toolStripComboBoxEdit.SelectedIndex = 0;

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "MouseSnap.html";
            }

            layerEnvelopeMap = new Hashtable();
            fcGUIDMap = new Hashtable();

            // 可视化Point类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\point.FDB");
                ci.Database = tmpFDBPath;
                ISimpleGeometryRender render = new SimpleGeometryRender();
                ISimplePointSymbol pointSymbol = new SimplePointSymbol();
                pointSymbol.Size = 10;
                render.Symbol = pointSymbol;
                FeatureLayerVisualize(ci, true, "Point", render);
            }

            // 可视化Polyline类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\polyline.FDB");
                ci.Database = tmpFDBPath;
                FeatureLayerVisualize(ci, false, "Polyline", null);
            }

            // 可视化Polygon类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\polygon.FDB");
                ci.Database = tmpFDBPath;
                FeatureLayerVisualize(ci, false, "Polygon", null);
            }

            // 可视化ModelPoint类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\SDKDEMO.FDB");
                ci.Database = tmpFDBPath;
                FeatureLayerVisualize(ci, false, "ModelPoint", null);
            }

            // 绑定选择事件
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
        }

        // 公共方法
        void FeatureLayerVisualize(IConnectionInfo ci, bool needfly, string sourceName, IGeometryRender geoRender)
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
                    fcGUIDMap.Add(fc.Guid, fc);
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
                    fc, geoName, null, geoRender, rootId);

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




        void enableSnapBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (enableSnapBox.Checked)
                this.axRenderControl1.MouseSnapMode = gviMouseSnapMode.gviMouseSnapVertex;
            else
                this.axRenderControl1.MouseSnapMode = gviMouseSnapMode.gviMouseSnapDisable;
        }

        private void toolStripComboBoxMeasure_Click(object sender, System.EventArgs e)
        {
            //不开启
            //拾取坐标
            //直线测距
            //水平测距
            //垂直测距
            //投影面积  
            switch (this.toolStripComboBoxMeasure.SelectedIndex)
            {
                case 0:
                    this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                    break;
                case 1:
                    this.axRenderControl1.InteractMode = gviInteractMode.gviInteractMeasurement;
                    this.axRenderControl1.MeasurementMode = gviMeasurementMode.gviMeasureCoordinate;
                    break;
                case 2:
                    this.axRenderControl1.InteractMode = gviInteractMode.gviInteractMeasurement;
                    this.axRenderControl1.MeasurementMode = gviMeasurementMode.gviMeasureAerialDistance;
                    break;
                case 3:
                    this.axRenderControl1.InteractMode = gviInteractMode.gviInteractMeasurement;
                    this.axRenderControl1.MeasurementMode = gviMeasurementMode.gviMeasureHorizontalDistance;
                    break;
                case 4:
                    this.axRenderControl1.InteractMode = gviInteractMode.gviInteractMeasurement;
                    this.axRenderControl1.MeasurementMode = gviMeasurementMode.gviMeasureVerticalDistance;
                    break;
                case 5:
                    this.axRenderControl1.InteractMode = gviInteractMode.gviInteractMeasurement;
                    this.axRenderControl1.MeasurementMode = gviMeasurementMode.gviMeasureArea;
                    break;
            }
        }

        private void toolStripComboBoxEdit_Click(object sender, System.EventArgs e)
        {
            switch (this.toolStripComboBoxEdit.SelectedIndex)
            {
                case 0:
                        this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
                        this.axRenderControl1.FeatureManager.UnhighlightAll();
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;
                    this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
                    this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                    EditGeometry();
                    break; 
            }
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            this.axRenderControl1.FeatureManager.UnhighlightAll();        
            
            // 置空
            _buffer = null;
            _featureLayer = null;

            if (PickResult != null)
            {
                switch (PickResult.Type)
                {
                    case gviObjectType.gviObjectFeatureLayer:
                        {
                            IFeatureLayerPickResult flpr = PickResult as IFeatureLayerPickResult;
                            int fid = flpr.FeatureId;
                            //加载多FeatureClass时要每次重新获取
                            _featureClass = (IFeatureClass)fcGUIDMap[flpr.FeatureLayer.FeatureClassId];
                            _featureLayer = flpr.FeatureLayer;

                            IFdeCursor cursor = null;
                            try
                            {
                                _buffer = _featureClass.CreateRowBuffer();
                                QueryFilter filter = new QueryFilter();
                                //filter.AddSubField("oid");  //注意：StartEditFeatureGeometry里必须传入一个完整的rowbuffer，所以这里不能限定字段
                                filter.WhereClause = "oid =" + fid;
                                cursor = _featureClass.Search(filter, false);
                                IRowBuffer row = null;
                                if ((row = cursor.NextRow()) != null)
                                {
                                    _buffer = row as IRowBuffer;
                                    int pos = _buffer.FieldIndex("Geometry");
                                    oldfdeGeometry = _buffer.GetValue(pos) as IGeometry;
                                }
                            }
                            catch (COMException ex)
                            {
                                System.Diagnostics.Trace.WriteLine(ex.Message);
                            }
                            finally
                            {
                             
                            }

                            this.axRenderControl1.FeatureManager.HighlightFeature(_featureClass, fid, System.Drawing.Color.Yellow);
                            EditGeometry();
                        }
                        break;
                }
            }
        }

        void EditGeometry()
        {
            _geoEditor.FinishEdit();   //用于当拾取到基准面时，或者没有正常右键结束连续调用Start时
            if (_buffer != null && _featureLayer != null)
            {
                if (this.toolStripComboBoxEdit.SelectedIndex == 1)
                    resultCode = _geoEditor.StartEditFeatureGeometry(_buffer, _featureLayer, gviGeoEditType.gviGeoEdit3DMove);
                else if (this.toolStripComboBoxEdit.SelectedIndex == 2)
                    resultCode = _geoEditor.StartEditFeatureGeometry(_buffer, _featureLayer, gviGeoEditType.gviGeoEdit3DRotate);
                else if (this.toolStripComboBoxEdit.SelectedIndex == 3)
                    resultCode = _geoEditor.StartEditFeatureGeometry(_buffer, _featureLayer, gviGeoEditType.gviGeoEdit3DScale);
                else if (this.toolStripComboBoxEdit.SelectedIndex == 4)
                    resultCode = _geoEditor.StartEditFeatureGeometry(_buffer, _featureLayer, gviGeoEditType.gviGeoEditVertex);
                else if (this.toolStripComboBoxEdit.SelectedIndex == 5)
                    resultCode = _geoEditor.StartEditFeatureGeometry(_buffer, _featureLayer, gviGeoEditType.gviGeoEditZRotate);
                else if (this.toolStripComboBoxEdit.SelectedIndex == 6)
                    resultCode = _geoEditor.StartEditFeatureGeometry(_buffer, _featureLayer, gviGeoEditType.gviGeoEditZScale);
                if (!resultCode)
                    MessageBox.Show(this.axRenderControl1.GetLastError().ToString());
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

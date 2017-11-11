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
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.Controls;

namespace TopologicalOperator
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名

        private IMultiPolygon geo1 = null;
        private IMultiPolygon geo2 = null;
        private SOURCE source;
        private IGeometryFactory gfactory = null;
        private IPolygon fde_polygon = null;
        private IRenderPolygon rpolygon = null;
        private IGeometry geoEditing = null;
        private bool resultCode;
        private List<IRenderGeometry> renderGeoToDel = new List<IRenderGeometry>();
        private ITableLabel tableLabel = null;
        private List<ITableLabel> labelToDel = new List<ITableLabel>();
        private List<SelectObject> objToHide = new List<SelectObject>();
        private List<IRenderPolygon> rpolyToDel = new List<IRenderPolygon>();

        private System.Guid rootId = new System.Guid();

        // 线程转发
        public int MainThreadId = 0;
        
        
        

        enum SOURCE
        {
            SELECT1,
            DRAW1,
            SELECT2,
            DRAW2
        }

        public MainForm()
        {
            InitializeComponent();

            MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;             
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

            // 可视化Polygon类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\polygon.FDB");
                ci.Database = tmpFDBPath;
                FeatureLayerVisualize(ci, true, "Polygon");
            }

            if (gfactory == null)
                gfactory = new GeometryFactory();
            geo1 = gfactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeNone) as IMultiPolygon;
            geo2 = gfactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeNone) as IMultiPolygon;

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "TopologicalOperator.html";
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

                    IFieldInfoCollection fieldinfos = fc.GetFields();
                    IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                    IGeometryDef geometryDef = fieldinfo.GeometryDef;
                    IEnvelope env = geometryDef.Envelope;
                    if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                        env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                        continue;

                    // 相机飞入
                    if (!hasfly)
                    {
                        IEulerAngle angle = new EulerAngle();
                        angle.Set(0, -20, 0);
                        this.axRenderControl1.Camera.LookAt(env.Center, 1000, angle);
                    }
                    hasfly = true;
                }
            }
        }

        void axRenderControl1_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            

            if (PickResult != null)
            {
                if (PickResult.Type == gviObjectType.gviObjectFeatureLayer)
                {
                    IFeatureLayerPickResult flpr = PickResult as IFeatureLayerPickResult;
                    int fid = flpr.FeatureId;
                    IFeatureLayer fl = flpr.FeatureLayer;
                    foreach (IFeatureClass fc in fcMap.Keys)
                    {
                        if (fc.Guid.Equals(fl.FeatureClassId))
                        {
                            IRowBuffer fdeRow = fc.GetRow(fid);
                            int nPos = fdeRow.FieldIndex("Geometry");
                            switch (source)
                            {
                                case SOURCE.SELECT1:
                                    this.axRenderControl1.FeatureManager.HighlightFeature(fc, fid, System.Drawing.Color.Yellow);
                                    geo1.AddGeometry(fdeRow.GetValue(nPos) as IPolygon);
                                    drawLabel(IntersectPoint, "要素一", System.Drawing.Color.Yellow);
                                    break;
                                case SOURCE.SELECT2:
                                    this.axRenderControl1.FeatureManager.HighlightFeature(fc, fid, System.Drawing.Color.Red);
                                    geo2.AddGeometry(fdeRow.GetValue(nPos) as IPolygon);
                                    drawLabel(IntersectPoint, "要素二", System.Drawing.Color.Yellow);
                                    break;
                            }
                            SelectObject obj = new SelectObject();
                            obj.FC = fc;
                            obj.ID = fid;
                            objToHide.Add(obj);
                        }
                    } // end foreach                 
                }
            }
            if (source == SOURCE.SELECT1 || source == SOURCE.SELECT2)
            {
                this.axRenderControl1.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
                
                this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
            }
        }

        private void btnSelect1_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            source = SOURCE.SELECT1;
        }

        private void btnSelect2_Click(object sender, EventArgs e)
        {
            this.axRenderControl1.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(axRenderControl1_RcMouseClickSelect);
            
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractSelect;
            this.axRenderControl1.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            this.axRenderControl1.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            source = SOURCE.SELECT2;
        }

        void axRenderControl1_RcObjectEditFinish()
        {
            

            switch (source)
            {
                case SOURCE.DRAW1:
                    geo1.AddGeometry(geoEditing as IPolygon);
                    drawLabel((geoEditing as IPolygon).Centroid, "要素一", System.Drawing.Color.Yellow);
                    break;
                case SOURCE.DRAW2:
                    geo2.AddGeometry(geoEditing as IPolygon);
                    drawLabel((geoEditing as IPolygon).Centroid, "要素二", System.Drawing.Color.Yellow);
                    break;
            }
            this.axRenderControl1.RcObjectEditing -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
            
            this.axRenderControl1.RcObjectEditFinish -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
            
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractNormal;
        }

        void axRenderControl1_RcObjectEditing(IGeometry Geometry)
        {
            

            geoEditing = Geometry;
        }

        private void btnDraw1_Click(object sender, EventArgs e)
        {
            ISurfaceSymbol sym = new SurfaceSymbol();
            sym.Color = System.Drawing.Color.YellowGreen;
            preDraw(sym);
            source = SOURCE.DRAW1;
        }

        private void btnDraw2_Click(object sender, EventArgs e)
        {
            ISurfaceSymbol sym = new SurfaceSymbol();
            sym.Color = System.Drawing.Color.Green;
            preDraw(sym);
            source = SOURCE.DRAW2;
        }

        void preDraw(ISurfaceSymbol symbol)
        {
            this.axRenderControl1.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(axRenderControl1_RcObjectEditing);
            
            this.axRenderControl1.RcObjectEditFinish += new _IRenderControlEvents_RcObjectEditFinishEventHandler(axRenderControl1_RcObjectEditFinish);
            
            this.axRenderControl1.InteractMode = gviInteractMode.gviInteractEdit;

            this.axRenderControl1.ObjectEditor.FinishEdit();   //用于当拾取到基准面时，或者没有正常右键结束连续调用Start时  

            if (gfactory == null)
                gfactory = new GeometryFactory();

            fde_polygon = (IPolygon)gfactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                gviVertexAttribute.gviVertexAttributeNone);
            rpolygon = this.axRenderControl1.ObjectManager.CreateRenderPolygon(fde_polygon, symbol, rootId);
            rpolygon.MaxVisibleDistance = 3000;
            rpolygon.MouseSelectMask = gviViewportMask.gviViewNone;
            renderGeoToDel.Add(rpolygon);

            resultCode = this.axRenderControl1.ObjectEditor.StartEditRenderGeometry(rpolygon, gviGeoEditType.gviGeoEditCreator);
            if (!resultCode)
            {
                MessageBox.Show(this.axRenderControl1.GetLastError().ToString());
                return;
            }
        }
 
 
        private void btnIntersection_Click(object sender, EventArgs e)
        {
            ITopologicalOperator2D topoOpera = geo1 as ITopologicalOperator2D;
            if (topoOpera == null) return;
            IGeometry geo = topoOpera.Intersection2D(geo2);
            draw(geo);
        }

        private void btnUnion_Click(object sender, EventArgs e)
        {
            ITopologicalOperator2D topoOpera = geo1 as ITopologicalOperator2D;
            if (topoOpera == null) return;
            IGeometry geo = topoOpera.Union2D(geo2);
            draw(geo);
        }

        private void btnDifference_Click(object sender, EventArgs e)
        {
            ITopologicalOperator2D topoOpera = geo1 as ITopologicalOperator2D;
            if (topoOpera == null) return;
            IGeometry geo = topoOpera.Difference2D(geo2);
            draw(geo);
        }

        private void btnSymmetricDifference_Click(object sender, EventArgs e)
        {
            ITopologicalOperator2D topoOpera = geo1 as ITopologicalOperator2D;
            if (topoOpera == null) return;
            IGeometry geo = topoOpera.SymmetricDifference2D(geo2);
            draw(geo);
        }

        void draw(IGeometry geo)
        {
            if (geo == null) return;
            //清空上一次计算的结果
            foreach (IRenderPolygon rpoly in rpolyToDel)
            {
                if (rpoly != null)
                    this.axRenderControl1.ObjectManager.DeleteObject(rpoly.Guid);
            }
            rpolyToDel.Clear();

            switch (geo.GeometryType)
            {
                case gviGeometryType.gviGeometryPolygon:
                    {
                        ISurfaceSymbol sym = new SurfaceSymbol();
                        sym.Color = System.Drawing.Color.Yellow;
                        IRenderPolygon rpoly = this.axRenderControl1.ObjectManager.CreateRenderPolygon(geo as IPolygon, sym, rootId);
                        rpolyToDel.Add(rpoly);
                    }
                    break;
                case gviGeometryType.gviGeometryMultiPolygon:
                    {
                        IMultiPolygon multiPolygon = geo as IMultiPolygon;
                        for (int i = 0; i < multiPolygon.GeometryCount; i++)
                        {
                            IPolygon polygon = multiPolygon.GetGeometry(i) as IPolygon;
                            ISurfaceSymbol sym = new SurfaceSymbol();
                            sym.Color = System.Drawing.Color.Yellow;
                            IRenderPolygon rpoly = this.axRenderControl1.ObjectManager.CreateRenderPolygon(polygon, sym, rootId);
                            rpolyToDel.Add(rpoly);
                        }
                    }
                    break;
            }
            setHide();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //重置前将原始polygon恢复
            for (int i = 0; i < objToHide.Count; i++)
            {
                SelectObject obj = objToHide[i];
                this.axRenderControl1.FeatureManager.ResetFeatureVisibleMask(obj.FC, obj.ID);
            }
            //开始重置
            this.axRenderControl1.FeatureManager.UnhighlightAll();
            foreach (IRenderGeometry render in renderGeoToDel)
            {
                if (render != null)
                    this.axRenderControl1.ObjectManager.DeleteObject(render.Guid);
            }
            renderGeoToDel.Clear();
            geo1.Clear();
            geo2.Clear();
            foreach (ITableLabel label in labelToDel)
            {
                if(label != null)
                    this.axRenderControl1.ObjectManager.DeleteObject(label.Guid);
            }
            labelToDel.Clear();
            objToHide.Clear();
            foreach (IRenderPolygon rpoly in rpolyToDel)
            {
                if (rpoly != null)
                    this.axRenderControl1.ObjectManager.DeleteObject(rpoly.Guid);
            }
            rpolyToDel.Clear();
        }

        private void cbHide_CheckedChanged(object sender, EventArgs e)
        {
            setHide();
        }

        private void setHide()
        {
            if (cbHide.Checked)
            {
                for (int i = 0; i < objToHide.Count; i++)
                {
                    SelectObject obj = objToHide[i];
                    this.axRenderControl1.FeatureManager.SetFeatureVisibleMask(obj.FC, obj.ID, gviViewportMask.gviViewNone);
                }
                for (int j = 0; j < renderGeoToDel.Count; j++)
                {
                    renderGeoToDel[j].VisibleMask = gviViewportMask.gviViewNone;
                }
                for (int k = 0; k < labelToDel.Count; k++)
                {
                    labelToDel[k].VisibleMask = gviViewportMask.gviViewNone;
                }
            }
            else
            {
                for (int i = 0; i < objToHide.Count; i++)
                {
                    SelectObject obj = objToHide[i];
                    this.axRenderControl1.FeatureManager.ResetFeatureVisibleMask(obj.FC, obj.ID);
                }
                for (int j = 0; j < renderGeoToDel.Count; j++)
                {
                    renderGeoToDel[j].VisibleMask = gviViewportMask.gviView0;
                }
                for (int k = 0; k < labelToDel.Count; k++)
                {
                    labelToDel[k].VisibleMask = gviViewportMask.gviView0;
                }
            }
        }

        private void drawLabel(IPoint p, string text, System.Drawing.Color color)
        {
            tableLabel = this.axRenderControl1.ObjectManager.CreateTableLabel(1, 1, rootId);
            tableLabel.TitleText = "";
            tableLabel.SetRecord(0, 0, text);
            tableLabel.Position = p;
            
            TextAttribute textAttribute = new TextAttribute();
            textAttribute.TextSize = 16;
            textAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
            textAttribute.TextColor = color;
            textAttribute.Font = "幼圆";
            textAttribute.Bold = false;
            tableLabel.SetColumnTextAttribute(0, textAttribute);
            tableLabel.SetColumnWidth(0, 60);

            TextAttribute capitalTextAttribute = new TextAttribute();            
            capitalTextAttribute.TextSize = 1;            
            tableLabel.TitleTextAttribute = capitalTextAttribute;

            tableLabel.TableBackgroundColor = System.Drawing.Color.Yellow;
            tableLabel.MaxVisibleDistance = 3000;
            labelToDel.Add(tableLabel);
        }

    }

    struct SelectObject
    {
        public IFeatureClass FC;
        public int ID;
    }
}

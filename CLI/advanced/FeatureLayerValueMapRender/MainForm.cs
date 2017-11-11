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
using System.Drawing;


namespace FeatureLayerValueMapRender
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        private Hashtable fcMap = null;  //IFeatureClass, List<string> 存储dataset里featureclass及对应的空间列名
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

            // 可视化ModelPoint类型FeatureLayer
            {
                IConnectionInfo ci = new ConnectionInfo();
                ci.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                string tmpFDBPath = (strMediaPath + @"\community.FDB");
                ci.Database = tmpFDBPath;

                // *******定义文字渲染风格*******
                IValueMapTextRender textRender = new ValueMapTextRender();
                textRender.Expression = "''..$(Name)";

                {
                    TextAttribute textAttribute = new TextAttribute();
                    textAttribute.TextColor = System.Drawing.Color.Yellow;
                    textAttribute.Font = "黑体";
                    textAttribute.TextSize = 15;

                    ITextSymbol textSymbol = new TextSymbol();
                    textSymbol.TextAttribute = textAttribute;
                    textSymbol.PivotAlignment = gviPivotAlignment.gviPivotAlignBottomCenter;

                    IUniqueValuesRenderRule uniqValRule = new UniqueValuesRenderRule();
                    uniqValRule.LookUpField = "type";
                    uniqValRule.AddValue("写字楼");

                    ITextRenderScheme textScheme = new TextRenderScheme();
                    textScheme.Symbol = textSymbol;
                    textScheme.AddRule(uniqValRule);
                    textRender.AddScheme(textScheme);
                }
                                
                {
                    TextAttribute textAttribute = new TextAttribute();
                    textAttribute.TextColor = Color.Black;
                    textAttribute.TextSize = 11;
                    textAttribute.Font = "华文新魏";

                    ITextSymbol textSymbol = new TextSymbol();
                    textSymbol.TextAttribute = textAttribute;
                    textSymbol.PivotAlignment = gviPivotAlignment.gviPivotAlignBottomCenter;

                    IUniqueValuesRenderRule uniqValRule = new UniqueValuesRenderRule();
                    uniqValRule.LookUpField = "type";
                    uniqValRule.AddValue("别墅");

                    ITextRenderScheme textScheme = new TextRenderScheme();
                    textScheme.Symbol = textSymbol;
                    textScheme.AddRule(uniqValRule);
                    textRender.AddScheme(textScheme);
                }

                {
                    TextAttribute textAttribute = new TextAttribute();
                    textAttribute.TextColor = Color.Red;
                    textAttribute.TextSize = 20;
                    textAttribute.Font = "幼圆";

                    ITextSymbol textSymbol = new TextSymbol();
                    textSymbol.TextAttribute = textAttribute;
                    textSymbol.PivotAlignment = gviPivotAlignment.gviPivotAlignBottomCenter;

                    IUniqueValuesRenderRule uniqValRule = new UniqueValuesRenderRule();
                    uniqValRule.LookUpField = "type";
                    uniqValRule.AddValue("商场");

                    ITextRenderScheme textScheme = new TextRenderScheme();
                    textScheme.Symbol = textSymbol;
                    textScheme.AddRule(uniqValRule);
                    textRender.AddScheme(textScheme);              
                }

                // *****定义几何物体渲染风格*****
                IValueMapGeometryRender geoRender = new ValueMapGeometryRender();
                {
                    IRangeRenderRule rangeRule = new RangeRenderRule();
                    rangeRule.LookUpField = "storey";
                    rangeRule.MaxValue = 3;
                    rangeRule.MinValue = 0;
                    rangeRule.IncludeMin = false;

                    IModelPointSymbol geoSymbol = new ModelPointSymbol();
                    geoSymbol.Color = Color.Yellow;  //模型颜色为浅黄
                    geoSymbol.EnableColor = true;  //需开启，否则颜色设置无效

                    IGeometryRenderScheme grs = new GeometryRenderScheme();
                    grs.AddRule(rangeRule);
                    grs.Symbol = geoSymbol;
                    geoRender.AddScheme(grs);
                }
                {
                    IRangeRenderRule rangeRule = new RangeRenderRule();
                    rangeRule.LookUpField = "storey";
                    rangeRule.MaxValue = 9;
                    rangeRule.MinValue = 3;
                    rangeRule.IncludeMin = false;

                    IModelPointSymbol geoSymbol = new ModelPointSymbol();
                    geoSymbol.Color = Color.YellowGreen;  //模型颜色为中黄
                    geoSymbol.EnableColor = true;  //需开启，否则颜色设置无效

                    IGeometryRenderScheme grs = new GeometryRenderScheme();
                    grs.AddRule(rangeRule);
                    grs.Symbol = geoSymbol;
                    geoRender.AddScheme(grs);
                }
                {
                    IRangeRenderRule rangeRule = new RangeRenderRule();
                    rangeRule.LookUpField = "storey";
                    rangeRule.MaxValue = 12;
                    rangeRule.MinValue = 9;
                    rangeRule.IncludeMin = false;

                    IModelPointSymbol geoSymbol = new ModelPointSymbol();
                    geoSymbol.Color = Color.Red;  //模型颜色为深黄
                    geoSymbol.EnableColor = true;  //需开启，否则颜色设置无效

                    IGeometryRenderScheme grs = new GeometryRenderScheme();
                    grs.AddRule(rangeRule);
                    grs.Symbol = geoSymbol;
                    geoRender.AddScheme(grs);
                }
                {
                    IRangeRenderRule rangeRule = new RangeRenderRule();
                    rangeRule.LookUpField = "storey";
                    rangeRule.MaxValue = 15;
                    rangeRule.MinValue = 12;
                    rangeRule.IncludeMin = false;

                    IModelPointSymbol geoSymbol = new ModelPointSymbol();
                    geoSymbol.Color = Color.PowderBlue;  //模型颜色为深黄
                    geoSymbol.EnableColor = true;  //需开启，否则颜色设置无效

                    IGeometryRenderScheme grs = new GeometryRenderScheme();
                    grs.AddRule(rangeRule);
                    grs.Symbol = geoSymbol;
                    geoRender.AddScheme(grs);
                }
                {
                    IGeometryRenderScheme geoSchemeOther = new GeometryRenderScheme();
                    geoRender.AddScheme(geoSchemeOther);
                }

                FeatureLayerVisualize(ci, true, "建筑", textRender, geoRender);
            }

            {
                this.helpProvider1.SetShowHelp(this.axRenderControl1, true);
                this.helpProvider1.SetHelpString(this.axRenderControl1, "");
                this.helpProvider1.HelpNamespace = "FeatureLayerValueMapRender.html";
            }
        }

        // 公共方法
        void FeatureLayerVisualize(IConnectionInfo ci, bool needfly, string fcNameNeedMap,
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
                        if (fcInMap.Name.Equals(fcNameNeedMap))
                            this.axRenderControl1.ObjectManager.CreateFeatureLayer(fcInMap, geoName, textRender, geoRender, rootId);
                        else
                            this.axRenderControl1.ObjectManager.CreateFeatureLayer(fcInMap, geoName, null, null, rootId);
                        
                        IFieldInfoCollection fieldinfos = fcInMap.GetFields();
                        IFieldInfo fieldinfo = fieldinfos.Get(fieldinfos.IndexOf(geoName));
                        IGeometryDef geometryDef = fieldinfo.GeometryDef;
                        IEnvelope env = geometryDef.Envelope;
                        if (env == null || (env.MaxX == 0.0 && env.MaxY == 0.0 && env.MaxZ == 0.0 &&
                            env.MinX == 0.0 && env.MinY == 0.0 && env.MinZ == 0.0))
                            continue;

                        // 相机飞入
                        if (!hasfly)
                        {
                            IVector3 pos = new Vector3();
                            pos.Set(env.Center.X, env.Center.Y, env.Center.Z);
                            IEulerAngle ang = new EulerAngle();
                            ang.Set(0, -20, 0);
                            this.axRenderControl1.Camera.LookAt(pos, 600, ang);
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
            }
        }

    }
}

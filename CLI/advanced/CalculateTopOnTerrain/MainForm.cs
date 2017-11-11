using System;
using System.Windows.Forms;
using System.IO;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Common;
using System.Collections;
using Gvitech.CityMaker.Controls;

namespace CalculateTopOnTerrain
{
    public partial class MainForm : Form
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        
        private ICalculateTop calculateTop = null;

        // 线程转发
        public int MainThreadId = 0;
        

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
                this.helpProvider1.HelpNamespace = "CalculateTopOnTerrain.html";
            } 

            // 注册地形
            string tmpTedPath = (strMediaPath + @"\terrain\terrain.ted");
            this.axRenderControl1.Terrain.RegisterTerrain(tmpTedPath, "");
            this.axRenderControl1.Terrain.FlyTo(gviTerrainActionCode.gviFlyToTerrain);

            // 默认选buffer
            this.toolStripAreaType.SelectedIndex = 0;
            this.axRenderControl1.RcKeyUp += new _IRenderControlEvents_RcKeyUpEventHandler(axRenderControl1_RcKeyUp);

        }


        private bool axRenderControl1_RcKeyUp(uint Flags, uint Ch)
        {
            

            if (Ch == 27)  //ESC键
            {
                calculateTop.Reset();

                this.toolStripAreaType.Enabled = true;
                this.toolStripButtonStartAnalysis.Enabled = true;
            }
            return default(bool);
        }

        private void toolStripButtonStartAnalysis_Click(object sender, EventArgs e)
        {
            this.toolStripAreaType.Enabled = false;
            this.toolStripButtonStartAnalysis.Enabled = false;
            this.axRenderControl1.Focus();
            if (this.toolStripAreaType.SelectedIndex == 0)
            {
                calculateTop = new BufferCalculateTop(axRenderControl1, MainThreadId, this);
                calculateTop.CalculateTop();
            }
            else if (this.toolStripAreaType.SelectedIndex == 1)
            {
                calculateTop = new PolygonCalculateTop(axRenderControl1, MainThreadId, this);
                calculateTop.CalculateTop();
            }
        }      
    }
}

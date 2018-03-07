using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Common;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string strMediaPath = System.IO.Path.GetTempPath() + "Gvitech\\SampleMedia";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IPropertySet ps = new PropertySet();
            ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
            this.axRenderControl.Initialize(true, ps);

            // 设置天空盒

            if (System.IO.Directory.Exists(strMediaPath))
            {
                string tmpSkyboxPath = strMediaPath + @"\skybox";
                ISkyBox skybox = this.axRenderControl.ObjectManager.GetSkyBox(0);
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
        }
    }
}

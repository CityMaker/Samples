using System;
using System.Windows.Forms;

namespace HelloWorld
{
    public partial class AboutDialog : Form
    {
        public AboutDialog(string sInstallData,string sVersion)
        {
            InitializeComponent();
            this.Text = "CityMaker" + sVersion;
            if (string.IsNullOrEmpty(sInstallData))
                sInstallData = "获取安装日期需安装CityMakerRuntime";
            if (string.IsNullOrEmpty(sVersion))
                sVersion = "获取版本信息需安装CityMakerRuntime";
            this.label_InstallData.Text = sInstallData;
            this.label_SoftVersion.Text = sVersion;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}

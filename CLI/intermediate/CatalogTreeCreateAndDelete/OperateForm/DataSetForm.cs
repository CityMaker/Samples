using System;
using System.Windows.Forms;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;

namespace CatalogTreeCreateAndDelete
{
    public partial class DataSetForm : Form
    {
        private ICoordSysDialog csDiag = null;
        private ICRSFactory coorFactory = new CRSFactory();
        private ICoordinateReferenceSystem coorSys = null;

        public string DatasetName
        {
            get { return this.txtName.Text.Trim(); }
        }

        public string CoordString
        {
            get { return this.txtCoorSys.Text.Trim(); }
        }

        public DataSetForm()
        {
            InitializeComponent();
        }

        private void btnSetCoorSys_Click(object sender, EventArgs e)
        {
            try
            {
                csDiag = new CoordSysDialog();
                string strCrs = csDiag.ShowDialog(gviLanguage.gviLanguageChineseSimple);
                if (strCrs != "")
                {                    
                    coorSys = coorFactory.CreateFromWKT(strCrs);
                    if (coorSys == null)
                    {
                        MessageBox.Show("无效的空间坐标系");
                        this.txtCoorSys.Text = @"UNKNOWNCS[\""unnamed\""]"; ;
                        this.txtCoorSys.Focus();
                    }
                    else
                    {
                        this.txtCoorSys.Text = strCrs;
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
    }
}

using System;
using System.Windows.Forms;
//****Gvitech.CityMaker****
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Common;


namespace CoordinateTransform
{
    public partial class MainForm : Form
    {
        private IPoint point = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {


            {
                this.helpProvider1.SetShowHelp(this.tableLayoutPanel1, true);
                this.helpProvider1.SetHelpString(this.tableLayoutPanel1, "");
                this.helpProvider1.HelpNamespace = "CoordinateTransform.html";
            }    
        }

        private void btnTransform_Click(object sender, EventArgs e)
        {
            try
            {
                point = new GeometryFactory().CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                string sourceWKT = this.textSourceWKT.Text;
                point.SpatialCRS = new CRSFactory().CreateFromWKT(sourceWKT) as ISpatialCRS;
                point.SetCoords(double.Parse(this.textSourceX.Text),
                    double.Parse(this.textSourceY.Text),
                    double.Parse(this.textSourceZ.Text),
                    1, 1);
                string targetWKT = this.textTargetWKT.Text;
                bool bPrj = point.Project(new CRSFactory().CreateFromWKT(targetWKT) as ISpatialCRS);
                if (bPrj)
                {
                    this.textTargetX.Text = point.X.ToString();
                    this.textTargetY.Text = point.Y.ToString();
                    this.textTargetZ.Text = point.Z.ToString();
                }
                else
                {
                    MessageBox.Show("Project Failed!");
                    this.textTargetX.Text = "";
                    this.textTargetY.Text = "";
                    this.textTargetZ.Text = "";
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btnSelectSourceWKT_Click(object sender, EventArgs e)
        {
             ICoordSysDialog dialog = new CoordSysDialog();
             this.textSourceWKT.Text = dialog.ShowDialog(gviLanguage.gviLanguageChineseSimple);
        }

        private void btnSelectTargetWKT_Click(object sender, EventArgs e)
        {
            ICoordSysDialog dialog = new CoordSysDialog();
            this.textTargetWKT.Text = dialog.ShowDialog(gviLanguage.gviLanguageChineseSimple);
        }

    }
}

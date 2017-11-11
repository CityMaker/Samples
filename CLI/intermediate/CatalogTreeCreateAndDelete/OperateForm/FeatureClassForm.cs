using System.Windows.Forms;

namespace CatalogTreeCreateAndDelete
{
    public partial class FeatureClassForm : Form
    {
        public string FeatureClassName
        {
            get { return this.txtName.Text.Trim(); }
        }

        public FeatureClassForm()
        {
            InitializeComponent();
        }
    }
}

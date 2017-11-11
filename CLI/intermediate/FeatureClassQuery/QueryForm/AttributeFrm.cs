using System;
using System.Data;
using System.Windows.Forms;

namespace FeatureClassQuery
{
    public partial class AttributeFrm : Form
    {
        DataTable AttriTable = null;
        string FCName = "";
        string FilterWhereClause = "";
      
        public AttributeFrm(DataTable dt, string fcName, string filterWhereClause)
        {
            InitializeComponent();

            AttriTable = dt;
            FCName = fcName;
            FilterWhereClause = filterWhereClause;
        }

        private void AttributeFrm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = AttriTable;
            if(FilterWhereClause.Equals(""))
            this.Text = "Attributes of " + FCName + "  [Total records: " + AttriTable.Rows.Count.ToString()+"]";
            else
                this.Text = "Attributes of " + FCName + "  [Total records: " + AttriTable.Rows.Count.ToString() + "]" + "  Filter: " + FilterWhereClause;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1_AdonetCustomer
{
    public partial class frmmap : Form
    {
        public frmmap()
        {
            InitializeComponent();
        }

        private void btnOpenCityForm_Click(object sender, EventArgs e)
        {
            frmcity frmcity= new frmcity();
            frmcity.Show();
        }

        private void btnOpenCustomerForm_Click(object sender, EventArgs e)
        {
            frmcustomer frmcustomer= new frmcustomer();
            frmcustomer.Show();
        }

        private void frmmap_Load(object sender, EventArgs e)
        {

        }
    }
}

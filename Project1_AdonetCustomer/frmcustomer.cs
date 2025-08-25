using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1_AdonetCustomer
{
    public partial class frmcustomer : Form
    {
        SqlConnection sqlConnection = new SqlConnection("Server=Mert;initial catalog=DbCustomer;integrated security=true");
        public frmcustomer()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnList_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("select CustomerId,CustomerName,CustomerSurname,CustomerBalance,CustomerStatus,CityName from TblCustomer Inner Join TblCity on TblCity.CityId=TblCustomer.CustomerCity",sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection.Close();

        }

        private void btnProcedure_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("execute CustomerListWith", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection.Close();
        }

        private void frmcustomer_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from TblCity", sqlConnection);
            SqlDataAdapter adapter=new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cmbCustomerCity.ValueMember= "CityId";
            cmbCustomerCity.DisplayMember = "CityName";
            cmbCustomerCity.DataSource = dt;
           
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Insert  into tblcustomer (CustomerName,CustomerSurname,CustomerCity,CustomerBalance,CustomerStatus) values (@customerName,@customerSurname,@customerCity,@customerBalance,@customerStatus)", sqlConnection);

            cmd.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            cmd.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            cmd.Parameters.AddWithValue("@customerCity", cmbCustomerCity.SelectedValue);
            cmd.Parameters.AddWithValue("@customerBalance", decimal.Parse(txtCustomerBalance.Text));
            if (rdbActive.Checked)
            {
                cmd.Parameters.AddWithValue("@customerStatus", true);
            }
            if (rdbPassive.Checked)
            {
                cmd.Parameters.AddWithValue("@customerStatus", false);
            }
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Eklendi");



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Delete from tblcustomer where CustomerId=@customerId", sqlConnection);
            cmd.Parameters.AddWithValue("@customerId", txtCustomerId.Text);
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Bilgileri Silindi");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Update tblcustomer set CustomerName=@customerName,CustomerSurname=@customerSurname,CustomerCity=@customerCity,CustomerBalance=@customerBalance,CustomerStatus=@customerStatus where CustomerId=@customerId", sqlConnection);
            cmd.Parameters.AddWithValue("@customerId", txtCustomerId.Text);
            cmd.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            cmd.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            cmd.Parameters.AddWithValue("@customerCity", cmbCustomerCity.SelectedValue);
            cmd.Parameters.AddWithValue("@customerBalance", decimal.Parse(txtCustomerBalance.Text));
            if (rdbActive.Checked)
            {
                cmd.Parameters.AddWithValue("@customerStatus", true);
            }
            if (rdbPassive.Checked)
            {
                cmd.Parameters.AddWithValue("@customerStatus", false);
            }
            cmd.ExecuteNonQuery();
            sqlConnection.Close();  
            MessageBox.Show("Müşteri Bilgileri Güncellendi");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("select CustomerId,CustomerName,CustomerSurname,CustomerBalance,CustomerStatus,CityName from TblCustomer Inner Join TblCity on TblCity.CityId=TblCustomer.CustomerCity where CustomerName=@customerName", sqlConnection);
            cmd.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection.Close();

        }
    }
}

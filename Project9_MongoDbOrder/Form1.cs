using Project9_MongoDbOrder.Entities;
using Project9_MongoDbOrder.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project9_MongoDbOrder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OrderOperation orderOperation = new OrderOperation();
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnlist_Click(object sender, EventArgs e)
        {
            List<Order> orders = orderOperation.GetAllOrders();
            dataGridView1.DataSource = orders;
        }

        private void btncreate_Click(object sender, EventArgs e)
        {
            var order=new Order
            {
                CustomerName = txtcustomer.Text,
                District = txtDistrict.Text,
                City = txtCity.Text,
                TotalPrice = decimal.Parse(txtTotalPrice.Text)
            };
            orderOperation.AddOrder(order);
            MessageBox.Show("Order Created");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string orderId = txtId.Text;
            orderOperation.DeleteOrder(orderId);
            MessageBox.Show("Order Deleted");
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            var updateOrder = new Order
            {
                CustomerName = txtcustomer.Text,
                District = txtDistrict.Text,
                City = txtCity.Text,
                TotalPrice = decimal.Parse(txtTotalPrice.Text),
                OrderId = id
            };
            orderOperation.UpdateOrder(id, updateOrder);
            MessageBox.Show("Order Updated");
        }

        private void btngetbyid_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            Order orders = orderOperation.GetOrderById(id);
            dataGridView1.DataSource = new List<Order> { orders};
        }
    }
}

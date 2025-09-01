using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project2_EntityFrameworkDbFirstProduct
{
    public partial class frmProduct : Form
    {
        public frmProduct()
        {
            InitializeComponent();
        }
        Db2Project20Entities db = new Db2Project20Entities();
        void ProductList()
        {
            dataGridView1.DataSource = db.TblProduct.ToList();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            ProductList();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var value = db.TblProduct.Find(int.Parse(txtProductId.Text));
            db.TblProduct.Remove(value);
            db.SaveChanges();
            ProductList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var value = db.TblProduct.Find(int.Parse(txtProductId.Text));
            value.ProductPrice = Decimal.Parse(txtProductPrice.Text);
            value.ProductStock = Convert.ToInt32(txtProductStock.Text);
            value.ProductName = txtPrdouctName.Text;
            value.CategoryId = int.Parse(cmbProductCategory.SelectedValue.ToString());
            db.SaveChanges();
            ProductList();

        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            var value = db.TblCategory.ToList();
            cmbProductCategory.DisplayMember = "CategoryName";
            cmbProductCategory.ValueMember = "CategoryId";
            cmbProductCategory.DataSource = value;

        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            TblProduct tblProduct = new TblProduct();
            tblProduct.ProductPrice = Decimal.Parse(txtProductPrice.Text);
            tblProduct.ProductStock = Convert.ToInt32(txtProductStock.Text);
            tblProduct.ProductName = txtPrdouctName.Text;
            tblProduct.CategoryId = int.Parse(cmbProductCategory.SelectedValue.ToString());
            db.TblProduct.Add(tblProduct);
            db.SaveChanges();
            ProductList();
        }

        private void btnProductListWithCategory_Click(object sender, EventArgs e)
        {
          var values=db.TblProduct
                .Join(db.TblCategory,
                product=> product.CategoryId,
                category=> category.CategoryId,
                (product,category)=> new
                {
                    ProductId= product.ProductId,
                    ProductName= product.ProductName,
                    ProductStock= product.ProductStock,
                    ProductPrice= product.ProductPrice,
                    CategoryId= category.CategoryId,
                    CategoryName= category.CategoryName

                }).ToList();
            dataGridView1.DataSource = values;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var value=db.TblProduct.Where(x=>x.ProductName==txtPrdouctName.Text).ToList();
            dataGridView1.DataSource = value;
        }
    }
}

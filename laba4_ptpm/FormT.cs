using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba4_ptpm
{
    public partial class FormT : Form
    {
        private readonly laba4Entities _context;
        private readonly Product _product;
        public FormT(laba4Entities context, Product product)
        {
            InitializeComponent();
            _context = context;
            _product = product;
            lblName.Text = "Name: " + product.Name;
            lblPrice.Text = "Price: " + product.Price.ToString();
            lblQuantity.Text = "Quantity: " + product.Quantity.ToString();

            pictureBox1.Image = ByteArrayToImage(product.Image);
        }
        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }
        private void button3_Click(object sender, EventArgs e) // назад
        {
            Form3 f = new Form3(new laba4Entities());
            Hide();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) // редактирование
        {
            Edit formB = new Edit(new laba4Entities(), _product);
            Hide();
            formB.Show();
        }
        public bool DeleteProd()
        {
            var existingProduct = _context.Product.Find(_product.Id);
            if (existingProduct != null)
            {
                _context.Product.Remove(existingProduct);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        private void button2_Click(object sender, EventArgs e) //удалить
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this product?", "Delete Product", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (Delete(_product.Id))
                {

                    MessageBox.Show("Product deleted successfully");
                }
                    else
                {

                    MessageBox.Show("Product not found in the database");
                }
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var existingProduct = _context.Product.Find(id); // Поиск объекта по его Id
                if (existingProduct != null)
                {
                    _context.Product.Remove(existingProduct);
                    _context.SaveChanges();
                    this.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; };
        }
    }
}

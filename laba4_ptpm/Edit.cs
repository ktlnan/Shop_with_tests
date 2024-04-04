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
    public partial class Edit : Form

    {
        private readonly laba4Entities _context;
        private readonly Product _product;
        public Edit(laba4Entities context, Product product)
        {
            InitializeComponent();
            _context = context;
            _product = product;
            txtName.Text = product.Name.ToString();
            txtPrice.Text = product.Price.ToString();
            txtQuantity.Text = product.Quantity.ToString();

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

        private void button1_Click(object sender, EventArgs e) // выбрать
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
            }
        }
        public bool EditProd()
        {
            using (laba4Entities db = new laba4Entities())
            {
                var productInContext = _context.Product.Find(_product.Id);
                string txtName = "test";
                int txtPrice = 111;
                int txtQuantity = 111;
                pictureBox1.Image = new Bitmap("D:\\Downloads\\cat.png");
                productInContext.Name = txtName;
                productInContext.Price = txtPrice;
                productInContext.Quantity = txtQuantity;
                productInContext.Image = ImageToByteArray(pictureBox1.Image);
                db.SaveChanges();
                return true;
            }
        }

        private void button2_Click(object sender, EventArgs e) // редактировать
        {
           
           
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(txtQuantity.Text) || pictureBox1.Image == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }

            if (txtQuantity.Text.Contains(" ") || txtName.Text.Contains(" ") || txtPrice.Text.Contains(" "))
            {
                MessageBox.Show("Поля не должны содержать пробелы");
                return;
            }
            if (!int.TryParse(txtPrice.Text, out _))
            {
                MessageBox.Show("Поле цены должно содержать только цифры");
                return;
            }
            if (!int.TryParse(txtQuantity.Text, out _))
            {
                MessageBox.Show("Поле кол-ва товаров должно содержать только цифры");
                return;
            }
           var product = new Product();
            if (pictureBox1.Image != null)
            {
                
                product = Update(_product.Id, txtName.Text, Convert.ToInt32(txtPrice.Text), Convert.ToInt32(txtQuantity.Text), ImageToByteArray(pictureBox1.Image));
                if (product is null)
                {
                    MessageBox.Show("error");
                    return;
                }
                MessageBox.Show("Изменения сохранены успешно!");

             
            }
            else
            {
             product = Update(_product.Id, txtName.Text,Convert.ToInt32(txtPrice.Text), Convert.ToInt32(txtQuantity.Text), null);
            if (product is null)
            {
                MessageBox.Show("error");
                return;
            }
            MessageBox.Show("Изменения сохранены успешно!");

            }

            FormT formB = new FormT(new laba4Entities(), product);
            Hide();
            formB.Show();
        }
        public byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }
        private void button3_Click(object sender, EventArgs e) //назад
        {
            Form3 f = new Form3(new laba4Entities());
            Hide();
            f.ShowDialog();
        }
        public Product Update(int id, string name, int price, int quantity, byte[] image)
        {
            try
            {
                var product = _context.Product.Find(id);
                product.Name = txtName.Text;
                product.Price = int.Parse(txtPrice.Text);
                product.Quantity = int.Parse(txtQuantity.Text);
                if (image != null)
                {
                    product.Image = image;
                }
                _context.SaveChanges();
                return product;
            }
            catch { return null; }
        }
    }
}

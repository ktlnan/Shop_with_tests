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
            var productInContext = _context.Product.Find(_product.Id);
            var pr = _context.Product.Find(_product.Id);
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
            if (pictureBox1.Image == null)
            {
                productInContext.Image = ImageToByteArray(pictureBox1.Image);
            }
            else
            {

            }
            if (productInContext != null)
            {
                productInContext.Name = txtName.Text;
                productInContext.Price = int.Parse(txtPrice.Text);
                productInContext.Quantity = int.Parse(txtQuantity.Text);

                _context.SaveChanges();
                MessageBox.Show("Изменения сохранены успешно!");

                FormT formB = new FormT(new laba4Entities(), productInContext);
                Hide();
                formB.Show();
            }
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
    }
}

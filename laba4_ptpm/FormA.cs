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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace laba4_ptpm
{
    public partial class FormA : Form
    {
        private readonly laba4Entities _context;
        public FormA(laba4Entities context)
        {
            InitializeComponent();
            _context = context;
        }
        private void button1_Click(object sender, EventArgs e) // добавить фото
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg;*.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
            }
        }
        public Product AddProd(string name, int price, int quantity, byte[] image)
        {
            try
            {
                Product newProduct = new Product
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    Image = image
                };

                _context.Product.Add(newProduct);
                _context.SaveChanges();
                return newProduct;
            }
            catch { return null; }
        }

       

        private void button2_Click(object sender, EventArgs e) // добавиь
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
            string name = txtName.Text;
            int price = int.Parse(txtPrice.Text);
            int quantity = int.Parse(txtQuantity.Text);

            if (_context.Product.Any(p => p.Name == name && p.Price == price && p.Quantity == quantity))
            {
                MessageBox.Show("Товар с такими же данными уже существует");
                return;
            }
       
           var newProduct = AddProd(name, price, quantity, ImageToByteArray(pictureBox1.Image));
            if(newProduct is null)
            {
                MessageBox.Show("error");
            }
            MessageBox.Show("Product saved successfully!");


            FormT formB = new FormT(new laba4Entities(), newProduct);
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

        private void button3_Click(object sender, EventArgs e) // назад
        {
            Form3 a = new Form3(new laba4Entities());
            Hide();
            a.ShowDialog();
        }
    }
}

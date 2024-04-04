using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba4_ptpm
{
    public partial class FormU : Form
    {
        private readonly laba4Entities  _context;
        public FormU(laba4Entities context, bool Hide)
        {
            InitializeComponent();
            _context = context;
            List<Product> products = GetProducts();
            dataGridView1.DataSource = products.Select(p => new
            {
                p.Name,
                p.Price,
                p.Quantity
            }).ToList();
            button1.Visible = Hide;
        }

        private void button1_Click(object sender, EventArgs e) //назад
        {
                Form3 a = new Form3(new laba4Entities());
                Hide();
                a.ShowDialog();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                string productName = row.Cells["Name"].Value.ToString();
                Product selectedProduct = _context.Product.FirstOrDefault(p => p.Name == productName);

                if (selectedProduct != null)
                {
                    FormT formT = new FormT(_context, selectedProduct);
                    Hide();
                    formT.Show();
                }
            }
        }
        public List<Product> GetProducts()
        {
            return _context.Product.ToList();
        }
    }
}

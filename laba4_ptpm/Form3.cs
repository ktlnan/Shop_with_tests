using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba4_ptpm
{
    public partial class Form3 : Form
    {
        private readonly laba4Entities _context;
        public Form3(laba4Entities contex)
        {
            InitializeComponent();
            _context = new laba4Entities();
        }

        private void button2_Click(object sender, EventArgs e) //список
        {
           
            FormU t = new FormU(new laba4Entities(), true);
            Hide();
            t.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) // добавить
        { 
            FormA a = new FormA(new laba4Entities());
            Hide();
            a.ShowDialog();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
    
}

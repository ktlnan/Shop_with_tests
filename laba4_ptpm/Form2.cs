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
    public partial class Form2 : Form
    {
        private laba4Entities context;
        public Form2(laba4Entities context)
        {
            InitializeComponent();
            this.context = context;
        }

        public bool RegisterUser(string login, string password, string name, string phone, string role)
        {
            if (context.User.Any(u => u.Login == login))
            {
                return false;
            }
            else
            {
                var id = 0;
                try{
                    id = context.User.Max(u => u.id) + 1;
                }
                catch { };
                User us = new User(id, login, password, name, int.Parse(phone), role);
                context.User.Add(us);
                context.SaveChanges();
                return true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
       string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }

            if (textBox1.Text.Contains(" ") || textBox2.Text.Contains(" ") || textBox3.Text.Contains(" ") || textBox4.Text.Contains(" "))
            {
                MessageBox.Show("Поля не должны содержать пробелы");
                return;
            }
            if (!int.TryParse(textBox4.Text, out _))
            {
                MessageBox.Show("Поле телефона должно содержать только цифры");
                return;
            }
            if (!radioButton_user.Checked && !radioButton_admin.Checked)
            {
                MessageBox.Show("Выберите роль");
                return;
            }
            using (laba4Entities db = new laba4Entities())
            {
                string role = "";
                if (radioButton_admin.Checked)
                {
                    role = "admin";
                }
                else if (radioButton_user.Checked)
                {
                    role = "user";
                }

                bool result = RegisterUser(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, role);
                if (result)
                {
                    if (role == "user")
                    {
                        FormU u = new FormU(new laba4Entities(), false);
                        Hide();
                        u.ShowDialog();
                    }
                    Form3 a = new Form3(new laba4Entities());
                    Hide();
                    a.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже существует");
                }


            }
        }
    }
}

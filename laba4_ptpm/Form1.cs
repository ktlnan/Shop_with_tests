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
    public partial class Form1 : Form
    {
        private laba4Entities context;
        public Form1(laba4Entities context)
        {
            InitializeComponent();
            this.context = context;
        }
        public bool LogIn(string login, string password)
        {
            foreach (User user in context.User)
            {
                if (login == user.Login && password == user.Password)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckAuthorization(string login, string password)
        {
            foreach (User user in context.User)
            {
                if (login == user.Login && password == user.Password)
                {
                    return true;
                }
            }
            return false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(context);
            Hide();
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox_log.Text;
            string password = textBox_pass.Text;
            if (CheckAuthorization(login, password))
            {
                MessageBox.Show("Вы успешно авторизовались!", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                User user = context.User.FirstOrDefault(u => u.Login == login && u.Password == password);
                if (user != null && user.Role == "user")
                {
                    FormU u = new FormU(new laba4Entities(), false);
                    Hide();
                    u.ShowDialog();
                    return;
                }
                Form3 a = new Form3(new laba4Entities());
                Hide();
                a.ShowDialog();
            }
            else
            {
                MessageBox.Show("Неправильный email или пароль", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

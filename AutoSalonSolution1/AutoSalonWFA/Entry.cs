using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoSalonWFA.Model;
using AutoSalonWFA.Extension;

namespace AutoSalonWFA
{
    public partial class Entry : Form
    {
        private readonly AutoSalonEntities db;
        public Entry()
        {
            db = new AutoSalonEntities();
            InitializeComponent();
        }
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtName.Text) || String.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Fill Inputs!");
                return;
            }
            string name = txtName.Text.Trim();
            string password = txtPassword.Text.Trim();
            password = Extension.Extension.HashPassword(password);
            bool asAdmin = rbtnAdmin.Checked;
            bool asUser = rbtnUser.Checked;
            if (asAdmin)
            {
                try
                {
                    Model.Admin admin = db.Admins.FirstOrDefault(a => a.Name == name && a.Password == password);
                    if (admin == null)
                    {
                        MessageBox.Show("Admin is not found! Check Inputs");
                        return;
                    }
                    AdminView adminView = new AdminView();
                    adminView.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else if(asUser)
            {
                Model.User user = db.Users.FirstOrDefault(a => a.Name == name && a.Password == password);
                if (user == null)
                {
                    MessageBox.Show("User is not found! Check Inputs");
                    return;
                }
                User userView = new User();
                userView.Show();
            }
            else
            {
                MessageBox.Show("Please Select any Role!");
            }
        }
    }
}

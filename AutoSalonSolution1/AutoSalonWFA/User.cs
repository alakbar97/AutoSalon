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
    public partial class User : Form
    {
        private readonly AutoSalonEntities db;
        Car selectedCar;
        public User()
        {
            db = new AutoSalonEntities();

            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {
            updateInfo();
        }
        private void updateInfo()
        {
            dgvUser.DataSource = db.Cars.Where(w => w.Available == true).Select(s => new
            {
                s.ID,
                s.Marka.Name,
                s.ModelName,
                s.EnginePower,
                s.FuelType,
                s.ReleasedYear
            }).ToList();
        }
        private void cleartxt()
        {
            tbxMarka.Text = "";
            tbxModel.Text = "";
        }
        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dgvUser.Rows[e.RowIndex].Cells[0].Value;
            selectedCar = db.Cars.Where(w => w.ID == id).FirstOrDefault();
            tbxModel.Text = selectedCar.ModelName;
            tbxMarka.Text = selectedCar.Marka.Name;
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            string marka = tbxMarka.Text.Trim();
            string model = tbxModel.Text.Trim();
            if (String.IsNullOrEmpty(marka) || String.IsNullOrEmpty(model))
            {
                MessageBox.Show("Choose Car from the Table");
            }
            else
            {
                selectedCar.Available = false;
                db.SaveChanges();
                updateInfo();
                cleartxt();
            }
        }

    }
}

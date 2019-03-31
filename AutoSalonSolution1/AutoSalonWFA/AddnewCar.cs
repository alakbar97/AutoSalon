using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoSalonWFA.Extension;
using AutoSalonWFA.Model;

namespace AutoSalonWFA
{
    public partial class AddnewCar : Form
    {
        private readonly AutoSalonEntities db;
        Car selectedCar;
        public AddnewCar()
        {
            db = new AutoSalonEntities();
            InitializeComponent();
        }

        private void AddnewCar_Load(object sender, EventArgs e)
        {
            updateInfo();
            cbxMarkaName.DataSource = db.Markas.Select(s => new ComboItem
            {
                Text = s.Name,
                Value = s.ID
            }).ToList();

        }
        public void updateInfo()
        {
            dgvCars.DataSource = db.Cars.Where(w => w.Available == true).Select(s => new
            {
                s.ID,
                s.Marka.Name,
                s.ModelName,
                s.EnginePower,
                s.FuelType,
                s.ReleasedYear,
                s.Available
            }).ToList();
        }
        public void clearText()
        {
            txtModelName.Text = "";
            tbxFuel.Text = "";
            nudEngine.Value = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string newModelName = txtModelName.Text.Trim();
            string newFuel = tbxFuel.Text;
            ComboItem newMarkaName = cbxMarkaName.SelectedItem as ComboItem;
            int newEnginePower = (int)nudEngine.Value;
            DateTime dateTime = dtpYear.Value;
            if (String.IsNullOrEmpty(newModelName) || String.IsNullOrEmpty(newFuel) || newEnginePower == 0)
            {
                MessageBox.Show("Please Fill inputs Correctly");
                return;
            }
            else
            {
                Car newCar = new Car
                {
                    MarkaID = newMarkaName.Value,
                    ModelName = newModelName,
                    FuelType = newFuel,
                    EnginePower = newEnginePower,
                    ReleasedYear = dateTime,
                    Available = true
                };
                db.Cars.Add(newCar);
                db.SaveChanges();
                updateInfo();
                clearText();
            }
        }

        private void dgvCars_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dgvCars.Rows[e.RowIndex].Cells[0].Value;
            selectedCar = db.Cars.Where(w => w.ID == id).FirstOrDefault();
            tbxFuel.Text = selectedCar.FuelType;
            dtpYear.Value = selectedCar.ReleasedYear;
            nudEngine.Value = selectedCar.EnginePower;
            txtModelName.Text = selectedCar.ModelName;
            btnAdd.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string newModelName = txtModelName.Text.Trim();
            string newFuel = tbxFuel.Text;
            ComboItem newMarkaName = cbxMarkaName.SelectedItem as ComboItem;
            int newEnginePower = (int)nudEngine.Value;
            DateTime dateTime = dtpYear.Value;

            if (String.IsNullOrEmpty(newModelName) || String.IsNullOrEmpty(newFuel) || newEnginePower == 0)
            {
                MessageBox.Show("Please Select Car From Table");
                return;
            }
            else
            {
                selectedCar.ModelName = newModelName;
                selectedCar.FuelType = newFuel;
                selectedCar.Marka.Name = newMarkaName.Text;
                selectedCar.EnginePower = newEnginePower;
                selectedCar.ReleasedYear = dateTime;
                db.SaveChanges();
                updateInfo();
                btnAdd.Enabled = true;
                clearText();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string newModelName = txtModelName.Text.Trim();
            string newFuel = tbxFuel.Text;
            int newEnginePower = (int)nudEngine.Value;

            if (String.IsNullOrEmpty(newModelName) || String.IsNullOrEmpty(newFuel) || newEnginePower == 0)
            {
                MessageBox.Show("Please Select Car From Table");
                return;
            }
            else
            {
                selectedCar.Available = false;
                db.SaveChanges();
                updateInfo();
                clearText();
            }
        }
    }
}

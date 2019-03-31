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
    public partial class AddNewMarka : Form
    {
        private readonly AutoSalonEntities db;
        Marka selectedMarka;
        public AddNewMarka()
        {
            db = new AutoSalonEntities();
            InitializeComponent();
        }

        private void AddNewMarka_Load(object sender, EventArgs e)
        {
            updateData();
        }
        private void updateData()
        {
            dgvMarka.DataSource = db.Markas.Select(s => new
            {
                s.ID,
                s.Name
            }).ToList();
        }
        private void cleartxt()
        {
            txtName.Text = "";
        }
        private void dgvMarka_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dgvMarka.Rows[e.RowIndex].Cells[0].Value;
            selectedMarka = db.Markas.Where(w => w.ID == id).FirstOrDefault();
            txtName.Text = selectedMarka.Name;
            btnAddNew.Enabled = false;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (String.IsNullOrEmpty(name))
            {
                MessageBox.Show("Write Name!");
                return;
            }
            else
            {
                Marka newMarka = new Marka
                {
                    Name = name
                };
                db.Markas.Add(newMarka);
                db.SaveChanges();
                updateData();
                cleartxt();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            string name = txtName.Text;
            if (String.IsNullOrEmpty(name))
            {
                MessageBox.Show("Select Marka!");
                return;
            }
            else
            {
                selectedMarka.Name = name;
                db.SaveChanges();
                updateData();
                cleartxt();
                btnAddNew.Enabled = true;
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            if (String.IsNullOrEmpty(name))
            {
                MessageBox.Show("Select Marka!");
                return;
            }
            db.Markas.Remove(selectedMarka);
            db.SaveChanges();
            updateData();
            cleartxt();
            btnAddNew.Enabled = true;
        }
    }
}

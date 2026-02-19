using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Business;
using DVLD.Core.DTOs.Enums;

namespace DVLD.WinForms
{
    public partial class frmManagePeople : Form
    {
        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void LoadDataGridView()
        {
            try
            {
                dgvPeople.DataSource = PersonBusiness.GetAll();
            }
            catch (Exception)
            {
                MessageBox.Show("An expected error occurred while trying to loading people.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateLabelCount()
        {
            lblCount.Text = dgvPeople.RowCount.ToString();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            dgvPeople.AutoGenerateColumns = false;
            LoadDataGridView();
            UpdateLabelCount();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {

        }
    }
}

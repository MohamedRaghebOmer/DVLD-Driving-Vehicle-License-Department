using System;
using System.Data;
using System.Windows.Forms;
using DVLD.Business;

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
            DataTable people = PersonBusiness.GetAllWithDateParts();
            people.CaseSensitive = cbMatchCase.Checked;
            dgvPeople.DataSource = people;
        }

        private void UpdateLabelCount()
        {
            lblCount.Text = dgvPeople.RowCount.ToString();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            dgvPeople.AutoGenerateColumns = false;
            cbFilterBy.SelectedIndex = 0;
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

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0 || cbFilterBy.SelectedIndex == 8)
            {
                txtFilterValue.Visible = false;
                dtpDateOfBirth.Visible = false;

                if (dgvPeople.DataSource != null)
                {
                    if (string.IsNullOrEmpty(txtSortValue.Text.Trim()))
                        ((DataTable)dgvPeople.DataSource).DefaultView.RowFilter = string.Empty;
                    UpdateLabelCount();
                }
            }
            else
            {
                dtpDateOfBirth.Visible = false;
                txtFilterValue.Visible = true;
                txtFilterValue.Clear();
                txtFilterValue.Focus();
                if (string.IsNullOrEmpty(txtSortValue.Text.Trim()))
                    ((DataTable)dgvPeople.DataSource).DefaultView.RowFilter = string.Empty;
            }

            if (cbFilterBy.SelectedIndex == 8)
            {
                dtpDateOfBirth.Visible = true;
                dtpDateOfBirth.Focus();
                dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
                dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
                dtpDateOfBirth_ValueChanged(null, null);
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedIndex != 1)
                return;

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFilterBy.SelectedIndex)
            {
                case 1: filterColumn = "PersonID"; break;
                case 2: filterColumn = "NationalNo"; break;
                case 3: filterColumn = "FirstName"; break;
                case 4: filterColumn = "SecondName"; break;
                case 5: filterColumn = "ThirdName"; break;
                case 6: filterColumn = "LastName"; break;
                case 7: filterColumn = "Gender"; break;
                case 8: filterColumn = "DateOfBirth"; break;
                case 9: filterColumn = "Nationality"; break;
                case 10: filterColumn = "Phone"; break;
                case 11: filterColumn = "Email"; break;
                default: filterColumn = "None"; break;
            }

            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()) || filterColumn == "None")
            {
                if (string.IsNullOrEmpty(txtSortValue.Text.Trim()))
                    ((DataTable)dgvPeople.DataSource).DefaultView.RowFilter = string.Empty;
                UpdateLabelCount();
                return;
            }

            if (filterColumn == "PersonID")
            {
                if (int.TryParse(txtFilterValue.Text.Trim(), out int id))
                {
                    ((DataTable)dgvPeople.DataSource).DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, id);
                }
            }
            else
            {
                ((DataTable)dgvPeople.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, txtFilterValue.Text.Trim());
            }

            UpdateLabelCount();
        }

        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {
            string filter = string.Format("[DateOfBirth] = '{0:yyyy-MM-dd}'", dtpDateOfBirth.Value);
            ((DataTable)dgvPeople.DataSource).DefaultView.RowFilter = filter;
            UpdateLabelCount();
        }

        private void cbMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvPeople.DataSource != null && cbFilterBy.SelectedIndex != 8)
            {
                DataTable dt = (DataTable)dgvPeople.DataSource;
                dt.CaseSensitive = cbMatchCase.Checked;
                txtFilterValue_TextChanged(null, null);
            }
        }

        private void txtSortValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtSortValue_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSortValue.Text.Trim()))
            {
                ((DataTable)dgvPeople.DataSource).DefaultView.RowFilter = string.Empty;
                UpdateLabelCount();
                return;
            }

            if (rbDay.Checked)
            {
                string filter = string.Format("[BirthDay] = {0}", txtSortValue.Text.Trim());
                ((DataTable)dgvPeople.DataSource).DefaultView.RowFilter = filter;
            }
            else if (rbMonth.Checked)
            {
                string filter = string.Format("[BirthMonth] = {0}", txtSortValue.Text.Trim());
                ((DataTable)dgvPeople.DataSource).DefaultView.RowFilter = filter;
            }
            else if (rbYear.Checked)
            {
                string filter = string.Format("[BirthYear] = {0}", txtSortValue.Text.Trim());
                ((DataTable)dgvPeople.DataSource).DefaultView.RowFilter = filter;
            }
           
            UpdateLabelCount();
        }

        private void RadioButtonSort_CheckedChanged(object sender, EventArgs e)
        {
            txtSortValue.Clear();
            txtSortValue.Focus();

            if (rbDay.Checked)
                txtSortValue.MaxLength = 2;
            else if (rbMonth.Checked)
                txtSortValue.MaxLength = 2;
            else if (rbYear.Checked)
                txtSortValue.MaxLength = 4;
        }
    }
}

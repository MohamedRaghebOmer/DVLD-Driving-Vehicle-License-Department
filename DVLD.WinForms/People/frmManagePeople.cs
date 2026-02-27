using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DVLD.Business;
using DVLD.WinForms.People;

namespace DVLD.WinForms
{
    public partial class frmManagePeople : Form
    {
        int selectedRowIndex = -1;

        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            dgvPeople.AutoGenerateColumns = false;
            cbFilterBy.SelectedIndex = 0;
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            DataTable people = PersonService.GetAllWithDateParts();
            people.CaseSensitive = cbMatchCase.Checked;
            dgvPeople.DataSource = people;
        }

        public void UpdateLabelCount(object sender, EventArgs e)
        {
            lblCount.Text = dgvPeople.RowCount.ToString();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
            LoadDataGridView();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0 || cbFilterBy.SelectedIndex == 8)
            {
                txtFilterValue.Visible = false;
                dtpDateOfBirth.Visible = false;
                cbMatchCase.Visible = false;

                if (dgvPeople.DataSource != null)
                {
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
            else
            {
                dtpDateOfBirth.Visible = false;
                txtFilterValue.Visible = true;
                txtFilterValue.Clear();
                txtFilterValue.Focus();
                
                if (string.IsNullOrEmpty(txtSortValue.Text.Trim()))
                    ((DataTable)dgvPeople.DataSource).DefaultView.RowFilter = string.Empty;
                
                if (cbFilterBy.SelectedIndex == 1 || cbFilterBy.SelectedIndex == 10)
                    cbMatchCase.Visible = false;
                else
                    cbMatchCase.Visible = true;
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
        }

        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {
            string filter = string.Format("[DateOfBirth] = '{0:yyyy-MM-dd}'", dtpDateOfBirth.Value);
            ((DataTable)dgvPeople.DataSource).DefaultView.RowFilter = filter;
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

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not available yet.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not available yet.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0)
                return;

            int personID = GetSelectedPersonID();

            if (MessageBox.Show(
                $"Are you sure you want to delete the person with ID [{personID}]?",
                "Warning",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            try
            {
                if (!PersonService.Delete(personID))
                {
                    MessageBox.Show("Person does not exist.",
                                    "Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

                LoadDataGridView();
                selectedRowIndex = -1;

                MessageBox.Show("Person has been deleted successfully.",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Person was not deleted because it has data linked to it.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
        
        private void dgvPeople_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dgvPeople.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    selectedRowIndex = hit.RowIndex;
                    dgvPeople.ClearSelection();
                    dgvPeople.Rows[selectedRowIndex].Selected = true;
                }
            }
        }

        private int GetSelectedPersonID()
        {
            return Convert.ToInt32(dgvPeople.Rows[selectedRowIndex].Cells[0].Value);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPersonInfo(GetSelectedPersonID());
            frm.ShowDialog();
            LoadDataGridView();
        }

        private void txtSortValue_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmPersonDetails(GetSelectedPersonID());
            frm.ShowDialog();
            LoadDataGridView();
        }
    }
}

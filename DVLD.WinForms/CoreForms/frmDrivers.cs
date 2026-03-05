using DVLD.Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.WinForms.CoreForms
{
    public partial class frmDrivers : Form
    {
        public frmDrivers()
        {
            InitializeComponent();
        }

        private void frmDrivers_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            dgvDrivers.DataSource = DriverService.GetDriversView();
        }

        private void UpdateLabelCount(object sender, EventArgs e)
        {
            lblCount.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Clear();

            if (dgvDrivers.DataSource is DataTable dt)
                dt.DefaultView.RowFilter = string.Empty;

            string selectedFilter = cbFilterBy.Text;

            // Hide all first (Reset UI)
            ResetFilterControls();

            switch (selectedFilter)
            {
                case "None":
                    break;

                case "National Number":
                case "Full Name":
                    txtFilterValue.Visible = true;
                    chkMatchCase.Visible = true;
                    txtFilterValue.Focus();
                    break;

                case "Date of Consideration":
                    dtpDateOfConsideration.Visible = true;
                    dtpDateOfConsideration.Focus();
                    break;

                default: // Driver Id, Person Id
                    txtFilterValue.Visible = true;
                    txtFilterValue.Focus();
                    break;
            }
        }
        private void ResetFilterControls()
        {
            txtFilterValue.Visible = false;
            dtpDateOfConsideration.Visible = false;
            chkMatchCase.Visible = false;
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (dgvDrivers.DataSource is DataTable dt)
                dt.DefaultView.RowFilter = string.Empty;
            else
                return;

            if (string.IsNullOrEmpty(txtFilterValue.Text))
                return;

            string selectedFilter = cbFilterBy.Text;

            switch (selectedFilter)
            {
                case "None":
                    dt.DefaultView.RowFilter = string.Empty;
                    break;

                case "National Number":
                    dt.DefaultView.RowFilter =
                        string.Format("NationalNo LIKE '{0}%'",
                        txtFilterValue.Text);
                    break;

                case "Full Name":
                    dt.DefaultView.RowFilter =
                       string.Format("FullName LIKE '{0}%'",
                       txtFilterValue.Text);
                    break;

                case "Date of Consideration":
                    dtpDateOfConsideration_ValueChanged(null, null);
                    break;

                case "Driver Id":
                    dt.DefaultView.RowFilter =
                        string.Format("DriverId = '{0}'",
                        txtFilterValue.Text);
                    break;

                case "Person Id":
                    dt.DefaultView.RowFilter =
                        string.Format("PersonId = '{0}'",
                        txtFilterValue.Text);
                    break;

                case "Active Licenses":
                    dt.DefaultView.RowFilter =
                        string.Format("NumberOfActiveLicenses = '{0}'",
                        txtFilterValue.Text);
                    break;
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if the selected is Person Id or Driver Id or Active Licenses,
            // and the key pressed is not digit or control, then ignore key press
            if ((cbFilterBy.SelectedIndex == 1 || cbFilterBy.SelectedIndex == 2
                || cbFilterBy.SelectedIndex == 6) && (!char.IsDigit(e.KeyChar)
                && !char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
            else if (cbFilterBy.SelectedIndex == 0 ||
                cbFilterBy.SelectedIndex == 5) // None, date of consideration 
            {
                e.Handled = true;
            }
        }

        private void dtpDateOfConsideration_ValueChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvDrivers.DataSource;

            DateTime selectedDate = dtpDateOfConsideration.Value.Date;
            DateTime nextDate = selectedDate.AddDays(1);

            dt.DefaultView.RowFilter =
                string.Format("CreatedDate >= #{0:MM/dd/yyyy}# AND CreatedDate < #{1:MM/dd/yyyy}#",
                selectedDate,
                nextDate);
        }

        private void chkMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            ((DataTable)dgvDrivers.DataSource).CaseSensitive =
                chkMatchCase.Checked;
        }
    }
}

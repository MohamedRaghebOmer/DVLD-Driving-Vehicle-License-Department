using DVLD.Business;
using DVLD.WinForms.Applications.ManageApplications.AddNewApplications;
using DVLD.WinForms.Licenses;
using DVLD.WinForms.People;
using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace DVLD.WinForms.Applications.ManageApplications.ManageApplications
{
    public partial class frmManageInternationalLicenseApplications : Form
    {
        private int _selectedRowIndex = -1;

        public frmManageInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void frmManageInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            DataTable dt = InternationalLicenseService.GetAll();
            dt.Columns.Remove("CreatedByUserID");
            dgvApplications.DataSource = dt;
            dgvApplications.Columns["colIssueDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvApplications.Columns["colExpirationDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void UpdateLabelCount(object sender, EventArgs e)
        {
            var dt = dgvApplications.DataSource as DataTable;
            lblCount.Text = (dt != null) ? dt.DefaultView.Count.ToString() : "0";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewIntApp_Click(object sender, EventArgs e)
        {
            var frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (_selectedRowIndex < 0)
                e.Cancel = true;
        }

        private void dgvApplications_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _selectedRowIndex = dgvApplications.HitTest(e.X, e.Y).RowIndex;

                if (_selectedRowIndex >= 0)
                {
                    dgvApplications.ClearSelection();
                    dgvApplications.Rows[_selectedRowIndex].Selected = true;
                }
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnVisibleFilterControls();

            switch (cbFilterBy.Text)
            {
                case "None":
                    DataTable dt = (DataTable)dgvApplications.DataSource;

                    if (dt != null)
                        dt.DefaultView.RowFilter = string.Empty;
                    break;

                case "Issue Date":
                case "Expiration Date":
                    dtpApplicationDate.Visible = true;
                    dtpApplicationDate.Focus();
                    dtpApplicationDate_ValueChanged(null, null);
                    break;

                case "Is Active":
                    cbIsActive.Visible = true;
                    cbIsActive.SelectedIndex = 0;
                    cbIsActive.Focus();
                    break;

                default: // Int.License Id, Application Id, Driver Id, Local License Id
                    txtFilterValue.Visible = true;
                    txtFilterValue.Clear();
                    txtFilterValue.Focus();
                    break;
            }
        }

        private void UnVisibleFilterControls()
        {
            txtFilterValue.Visible = false;
            dtpApplicationDate.Visible = false;
            cbIsActive.Visible = false;
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text != "Int.License Id"
                && cbFilterBy.Text != "Application Id"
                && cbFilterBy.Text != "Driver Id"
                && cbFilterBy.Text != "Local License Id")
            {
                return;
            }

            string columnName = GetFilterColumnName();
            string value = txtFilterValue.Text.Replace("'", "''");

            if (string.IsNullOrEmpty(columnName) || string.IsNullOrEmpty(value))
            {
                DataTable dt = (DataTable)dgvApplications.DataSource;

                if (dt != null)
                    dt.DefaultView.RowFilter = string.Empty;

                return;
            }

            ((DataTable)dgvApplications.DataSource).DefaultView.RowFilter =
                $"{columnName} = '{value}'";
        }

        private void dtpApplicationDate_ValueChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text != "Issue Date"
                && cbFilterBy.Text != "Expiration Date")
            {
                return;
            }

            string columnName = GetFilterColumnName();

            if (string.IsNullOrEmpty(columnName))
            {
                var dt = dgvApplications.DataSource as DataTable;
                if (dt != null)
                    dt.DefaultView.RowFilter = string.Empty;
                return;
            }

            // use a date range to ignore stored time component
            DateTime start = dtpApplicationDate.Value.Date;
            DateTime end = start.AddDays(1);

            string sStart = start.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string sEnd = end.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

            ((DataTable)dgvApplications.DataSource).DefaultView.RowFilter =
                $"{columnName} >= #{sStart}# AND {columnName} < #{sEnd}#";
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text != "Is Active")
            {
                return;
            }

            string columnName = GetFilterColumnName();

            if (string.IsNullOrEmpty(columnName))
            {
                DataTable dt = (DataTable)dgvApplications.DataSource;

                if (dt != null)
                    dt.DefaultView.RowFilter = string.Empty;

                return;
            }

            bool isActive = cbIsActive.SelectedIndex == 0;

            ((DataTable)dgvApplications.DataSource).DefaultView.RowFilter =
                $"{columnName} = {isActive}";
        }

        private string GetFilterColumnName()
        {
            switch (cbFilterBy.Text)
            {
                case "Int.License Id":
                    return "InternationalLicenseId";

                case "Application Id":
                    return "ApplicationId";

                case "Driver Id":
                    return "DriverId";

                case "Local License Id":
                    return "IssuedUsingLocalLicenseID";

                case "Issue Date":
                    return "IssueDate";

                case "Expiration Date":
                    return "ExpirationDate";

                case "Is Active":
                    return "IsActive";
            }

            return string.Empty;
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmPersonDetails(PersonService.GetIdByDriverId(GetSelectedDriverId()));
            frm.ShowDialog();
        }

        private int GetSelectedDriverId()
        {
            if (_selectedRowIndex >= 0)
                return (int)dgvApplications.Rows[_selectedRowIndex].Cells[2].Value;
            return -1;
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmInternationalLicenseInfo(GetSelectedInternationalLicenseId());
            frm.ShowDialog();
        }

        private int GetSelectedInternationalLicenseId()
        {
            if (_selectedRowIndex >= 0)
                return (int)dgvApplications.Rows[_selectedRowIndex].Cells[0].Value;
            return -1;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmLicensesHistory(GetSelectedDriverId(), frmLicensesHistory.LoadType.UsingDriverId);
            frm.ShowDialog();
        }
    }
}

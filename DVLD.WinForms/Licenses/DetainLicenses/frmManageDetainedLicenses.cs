using DVLD.Business;
using DVLD.WinForms.Applications.ManageApplications;
using DVLD.WinForms.People;
using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace DVLD.WinForms.Licenses.Detain
{
    public partial class frmManageDetainedLicenses : Form
    {
        private int _selectedRowIndex = -1;

        public frmManageDetainedLicenses()
        {
            InitializeComponent();
            LoadDataGrid();
            cbFilterBy.SelectedIndex = 0;
        }

        private void LoadDataGrid()
        {
            dgvDetainedLicenses.DataSource = DetainedLicenseService.GetAllWithDetails();
        }

        private void btnDetainNewLicense_Click(object sender, EventArgs e)
        {
            var frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            var frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideFilterControls();

            txtFilterValue.Clear();
            dtpDetainReleaseDate.Value = DateTime.Now;

            string columnName = GetFilterColumnName();
            ManageFilterControlsVisibility();

        }

        private void ManageFilterControlsVisibility()
        {
            HideFilterControls();

            switch (cbFilterBy.Text)
            {
                case "None":
                    break;

                case "Detain Date":
                case "Release Date":
                    dtpDetainReleaseDate.Visible = true;
                    dtpDetainReleaseDate.Focus();
                    break;

                case "Is Released":
                    cbIsReleased.Visible = true;
                    cbIsReleased.SelectedIndex = 0;
                    cbIsReleased.Focus();
                    break;

                default:
                    txtFilterValue.Visible = true;
                    txtFilterValue.Clear();
                    txtFilterValue.Focus();
                    break;
            }
        }

        private bool IsFilterByNumber()
        {
            return cbFilterBy.Text == "Detain Id"
                || cbFilterBy.Text == "Release Id"
                || cbFilterBy.Text == "License Id"
                || cbFilterBy.Text == "Fine Fees"
                || cbFilterBy.Text == "Release Application Id";
        }

        private bool IsFilterByDate()
        {
            return cbFilterBy.Text == "Detain Date"
                || cbFilterBy.Text == "Release Date";
        }

        private void HideFilterControls()
        {
            dtpDetainReleaseDate.Visible = false;
            txtFilterValue.Visible = false;
            cbIsReleased.Visible = false;
        }

        private string GetFilterColumnName()
        {
            switch (cbFilterBy.Text)
            {
                case "Detain Id":
                case "DetainID":
                    return "DetainID";

                case "License Id":
                case "LicenseID":
                    return "LicenseID";

                case "Detain Date":
                case "DetainDate":
                    return "DetainDate";

                case "Release Date":
                case "ReleaseDate":
                    return "ReleaseDate";

                case "Is Released":
                case "IsReleased":
                    return "IsReleased";

                case "Fine Fees":
                case "FineFees":
                    return "FineFees";

                case "National Number":
                case "NationalNo":
                    return "NationalNo";

                case "Full Name":
                case "FullName":
                    return "FullName";

                case "Release App Id":
                case "Release Application Id":
                case "ReleaseApplicationID":
                case "Release Id":
                    return "ReleaseApplicationID";

                default:
                    return string.Empty;
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsFilterByNumber()
                && !char.IsDigit(e.KeyChar)
                && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void UpdateLabelCount(object sender, EventArgs e)
        {
            lblCount.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void dgvDetainedLicenses_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _selectedRowIndex = dgvDetainedLicenses.HitTest(e.X, e.Y).RowIndex;

                if (_selectedRowIndex >= 0)
                {
                    dgvDetainedLicenses.ClearSelection();
                    dgvDetainedLicenses.Rows[_selectedRowIndex].Selected = true;
                }
            }

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (_selectedRowIndex < 0)
                e.Cancel = true;
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedNationalNo = GetSelectedRowNationalNo();

            if (!string.IsNullOrEmpty(selectedNationalNo))
            {
                var frm = new frmPersonDetails(selectedNationalNo);
                frm.ShowDialog();
            }
        }

        private string GetSelectedRowNationalNo()
        {
            if (_selectedRowIndex >= 0)
                return dgvDetainedLicenses.Rows[_selectedRowIndex].Cells[6].Value.ToString();
            else
                return string.Empty;
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int licenseId = GetSelectedRowLicenseId();

            if (licenseId > 0)
            {
                var frm = new frmLicenseInfo(licenseId, frmLicenseInfo.LoadType.UsingLicenseId);
                frm.ShowDialog();
            }
        }

        private int GetSelectedRowLicenseId()
        {
            if (_selectedRowIndex >= 0
                && int.TryParse(dgvDetainedLicenses.Rows[_selectedRowIndex]
                .Cells[1].Value.ToString(), out int licenseId))
            {
                return licenseId;
            }
            else
            {
                return -1;
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nationalNo = GetSelectedRowNationalNo();

            if (!string.IsNullOrEmpty(nationalNo))
            {
                var frm = new frmLicensesHistory(nationalNo);
                frm.ShowDialog();
            }
        }

        private void ApplyRowFilter(object sender, EventArgs e)
        {
            if (!(dgvDetainedLicenses.DataSource is DataTable dt))
                return;

            var dv = dt.DefaultView;

            if (cbFilterBy.Text == "None")
            {
                dv.RowFilter = string.Empty;
                return;
            }

            string columnName = GetFilterColumnName();

            if (string.IsNullOrEmpty(columnName))
            {
                dv.RowFilter = string.Empty;
                return;
            }

            string filter = string.Empty;

            // numeric columns
            var numericCols = new[] { "DetainID", "ReleaseApplicationID", "LicenseID", "FineFees" };
            // date columns
            var dateCols = new[] { "DetainDate", "ReleaseDate" };

            if (numericCols.Contains(columnName))
            {
                // try parse as decimal (works for int/decimal)
                if (!string.IsNullOrWhiteSpace(txtFilterValue.Text)
                    && decimal.TryParse(txtFilterValue.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var num))
                {
                    // ensure invariant formatting (use dot for decimals)
                    filter = $"{columnName} = {num.ToString(CultureInfo.InvariantCulture)}";
                }
                else
                {
                    // if user input empty or not a number, clear filter (or set impossible filter)
                    filter = string.Empty;
                }
            }
            else if (dateCols.Contains(columnName))
            {
                var date = dtpDetainReleaseDate.Value.Date;
                var nextDate = date.AddDays(1);

                filter = $"{columnName} >= #{date:MM/dd/yyyy}# AND {columnName} < #{nextDate:MM/dd/yyyy}#";
            }
            else if (columnName == "IsReleased")
            {
                // Interpret combo box text (supports "Yes"/"No", "True"/"False") or fall back to SelectedIndex
                bool isReleased = false;
                if (!string.IsNullOrWhiteSpace(cbIsReleased.Text))
                {
                    var t = cbIsReleased.Text.Trim().ToLowerInvariant();
                    if (t == "yes" || t == "true" || t == "released" || t == "1") isReleased = true;
                    else if (t == "no" || t == "false" || t == "not released" || t == "0") isReleased = false;
                    else isReleased = cbIsReleased.SelectedIndex == 0; // fallback
                }
                else
                {
                    isReleased = cbIsReleased.SelectedIndex == 0;
                }

                // In RowFilter booleans are written as True/False (no quotes)
                filter = $"{columnName} = {isReleased}";
            }
            else
            {
                // text columns: escape single quotes and use LIKE
                if (!string.IsNullOrWhiteSpace(txtFilterValue.Text))
                {
                    var escaped = txtFilterValue.Text.Replace("'", "''");
                    filter = $"{columnName} LIKE '{escaped}%'";
                }
                else
                {
                    filter = string.Empty;
                }
            }

            dv.RowFilter = filter ?? string.Empty;
        }
    }
}

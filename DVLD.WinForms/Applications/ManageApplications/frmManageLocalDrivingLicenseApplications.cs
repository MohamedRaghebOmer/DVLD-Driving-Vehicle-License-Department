using DVLD.Business;
using DVLD.Core.DTOs.Enums;
using DVLD.WinForms.Licenses;
using DVLD.WinForms.NavigateForms;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace DVLD.WinForms.Applications.ManageApplications
{
    public partial class frmManageLocalDrivingLicenseApplications : Form
    {
        private int _selectedRowIndex = -1;

        public frmManageLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void frmManageLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            LoadDataGridApplications();
            cbFilterBy.SelectedIndex = 0;
        }

        private void LoadDataGridApplications()
        {
            dgvApplications.DataSource = LocalDrivingLicenseApplicationService.GetAllWithDetails();
        }

        private void UpdateLabelCount(object sender, EventArgs e)
        {
            lblCount.Text = dgvApplications.Rows.Count.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControlsVisibility();
            ApplyRowFiltering();
        }

        private void UpdateControlsVisibility()
        {
            if (cbFilterBy.SelectedIndex == 0) // No filter
            {
                txtFilterValue.Visible = false;
                cbStatus.Visible = false;
                chkMatchCase.Visible = false;
            }
            // All other filters unless status
            else if (cbFilterBy.SelectedIndex != 4)
            {
                txtFilterValue.Visible = true;
                txtFilterValue.Clear();
                txtFilterValue.Focus();
                cbStatus.Visible = false;

                chkMatchCase.Visible = cbFilterBy.SelectedIndex == 2 ||
                    cbFilterBy.SelectedIndex == 3;
            }
            else
            {
                txtFilterValue.Visible = false;
                cbStatus.Visible = true;
                chkMatchCase.Visible = false;
                cbStatus.SelectedIndex = 0;
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0)
            {
                e.Handled = true;
                return;
            }

            if (cbFilterBy.SelectedIndex == 1 && !char.IsDigit(e.KeyChar) &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }


        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyRowFiltering();
        }

        private void ApplyRowFiltering()
        {
            if (cbFilterBy.SelectedIndex == 0 ||
                (cbFilterBy.SelectedIndex == 4 && cbStatus.SelectedIndex == 0)
                || (txtFilterValue.Visible && string.IsNullOrEmpty(txtFilterValue.Text)))
            {
                ((DataTable)dgvApplications.DataSource).
                    DefaultView.RowFilter = string.Empty;
                return;
            }

            string filterColumn = string.Empty;

            switch (cbFilterBy.SelectedIndex)
            {
                case 1:
                    filterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case 2:
                    filterColumn = "NationalNo";
                    break;
                case 3:
                    filterColumn = "FullName";
                    break;
                case 4:
                    filterColumn = "ClassName";
                    break;
                case 5:
                    filterColumn = "Status";
                    break;
            }

            DataTable dt = (DataTable)dgvApplications.DataSource;

            if (cbFilterBy.SelectedIndex == 1) // L.D.L ApplicationId
            {
                dt.DefaultView.RowFilter = string.Format("{0} = {1}",
                    filterColumn, txtFilterValue.Text);
            }
            else if (cbFilterBy.SelectedIndex != 4) // National No || Full Name || Class Name
            {
                dt.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'",
                    filterColumn, txtFilterValue.Text);
            }
            else // Status
            {
                if (cbFilterBy.SelectedIndex == 0)
                {
                    dt.DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    dt.DefaultView.RowFilter =
                        string.Format("[Status] = '{0}'", cbStatus.Text);
                }
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            ApplyRowFiltering();
        }

        private void chkMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            ((DataTable)dgvApplications.DataSource).CaseSensitive =
                chkMatchCase.Checked;
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedId = GetSelectedLocalApplicationId();

            if (MessageBox.Show($@"Are you sure do you want to cancel
        application with id = {selectedId}?", "Warning",
        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (ApplicationService.Cancel(selectedId))
                {
                    MessageBox.Show("Application canceled successfully.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridApplications();
                }
                else
                {
                    MessageBox.Show("Failed to cancel application.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        private int GetSelectedLocalApplicationId()
        {
            if (_selectedRowIndex < 0)
                return -1;
            return (int)dgvApplications.Rows[_selectedRowIndex].Cells[0].Value;
        }

        private string GetSelectedNationalNo()
        {
            if (_selectedRowIndex < 0)
                return string.Empty;
            return (string)dgvApplications.Rows[_selectedRowIndex].Cells[2].Value;
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($@"Are you sure do you want to delete
        application with id = {GetSelectedLocalApplicationId()}?", "Warning",
MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (ApplicationService.Delete(GetSelectedLocalApplicationId()))
                    {
                        MessageBox.Show("Application deleted successfully.",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGridApplications();
                    }
                    else
                    {
                        MessageBox.Show("Error deleting application.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (_selectedRowIndex < 0 || _selectedRowIndex > dgvApplications.Rows.Count)
            {
                e.Cancel = true;
                return;
            }

            ApplicationStatus selectedStatus = GetSelectedApplicationStatus();

            switch (selectedStatus)
            {
                case ApplicationStatus.Completed:
                    contextMenuStrip1.Items["showLicenseToolStripMenuItem"].Enabled = true;
                    contextMenuStrip1.Items["editAppToolStripMenuItem"].Enabled = false;
                    contextMenuStrip1.Items["deleteApplicationToolStripMenuItem"].Enabled = false;
                    contextMenuStrip1.Items["cancelApplicationToolStripMenuItem"].Enabled = false;
                    contextMenuStrip1.Items["scheduleTestsToolStripMenuItem"].Enabled = false;
                    contextMenuStrip1.Items["issueDrivingLicenseFirstTimeToolStripMenuItem"].Enabled = false;

                    break;

                case ApplicationStatus.Canceled:
                    editAppToolStripMenuItem.Enabled = false;
                    deleteApplicationToolStripMenuItem.Enabled = true;
                    cancelApplicationToolStripMenuItem.Enabled = false;
                    scheduleTestsToolStripMenuItem.Enabled = false;
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                    showLicenseToolStripMenuItem.Enabled = false;
                    break;

                case ApplicationStatus.New:
                    editAppToolStripMenuItem.Enabled = true;
                    deleteApplicationToolStripMenuItem.Enabled = true;
                    cancelApplicationToolStripMenuItem.Enabled = true;

                    int numberOfCompetedTests = GetSelectedNumOfCompletedTests();

                    switch (numberOfCompetedTests)
                    {
                        case 3:
                            scheduleTestsToolStripMenuItem.Enabled = false;
                            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
                            break;

                        case 2:
                        case 1:
                            scheduleTestsToolStripMenuItem.Enabled = true;
                            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;

                            int selectedNationalNo = GetSelectedLocalApplicationId();
                            bool hasPassedVisionTest = TestService.HasPassed(selectedNationalNo, TestType.VisionTest);
                            bool hasPassedWrittenTest = TestService.HasPassed(selectedNationalNo, TestType.WrittenTheoryTest);
                            bool hasPassedStreetTest = TestService.HasPassed(selectedNationalNo, TestType.PracticalStreetTest);

                            scheduleVisionTestToolStripMenuItem.Enabled = !hasPassedVisionTest;
                            scheduleWrittenTestToolStripMenuItem.Enabled = !hasPassedWrittenTest;
                            scheduleStreetTestToolStripMenuItem.Enabled = !hasPassedStreetTest;
                            break;

                        case 0:
                            scheduleTestsToolStripMenuItem.Enabled = true;
                            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                            scheduleVisionTestToolStripMenuItem.Enabled = true;
                            scheduleWrittenTestToolStripMenuItem.Enabled = true;
                            scheduleStreetTestToolStripMenuItem.Enabled = true;
                            break;
                    }

                    break;
            }
        }

        private int GetSelectedNumOfCompletedTests()
        {
            return Convert.ToInt32(dgvApplications.Rows[_selectedRowIndex].Cells[5].Value);
        }

        private ApplicationStatus GetSelectedApplicationStatus()
        {
            switch (dgvApplications.Rows[_selectedRowIndex].Cells[6].Value.
                ToString().Trim())
            {
                case "New":
                    return ApplicationStatus.New;

                case "Canceled":
                    return ApplicationStatus.Canceled;

                case "Completed":
                    return ApplicationStatus.Completed;

                default:
                    return ApplicationStatus.New;
            }
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmLicenseInfo(GetSelectedLocalApplicationId(), frmLicenseInfo.LoadType.UsingLocalApplicationId);
            frm.ShowDialog();
        }

        private void btnAddNewLocalApp_Click(object sender, EventArgs e)
        {
            Form frm = new frmNewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmLicensesHistory(GetSelectedNationalNo());
            frm.ShowDialog();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmApplicationInfo
                (GetSelectedLocalApplicationId(),
                frmApplicationInfo.LoadType.UsingLocalApplicationId);
            frm.ShowDialog();
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmIssueDrivingLicense(GetSelectedLocalApplicationId());
            frm.ShowDialog();
            LoadDataGridApplications();
        }
    }
}

using DVLD.Business;
using DVLD.Core.DTOs.Enums;
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
                ((DataTable)dgvApplications.DataSource).DefaultView.RowFilter = string.Empty;
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
                dt.DefaultView.RowFilter = string.Format("{0} LIKE '%{1}%'",
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
            DataTable dt = (DataTable)dgvApplications.DataSource;

            if (dt == null)
                return;

            dt.CaseSensitive = chkMatchCase.Checked;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ApplyRowFiltering();
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedId = GetSelectedApplicationId();

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

        private int GetSelectedApplicationId()
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
        application with id = {GetSelectedApplicationId()}?", "Warning",
MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (ApplicationService.Delete(GetSelectedApplicationId()))
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
            string selectedNationalNo = GetSelectedNationalNo();

            //if (TestService.HasPassed(selectedNationalNo, TestType.VisionTest))
            //{

            //}
        }
    }
}

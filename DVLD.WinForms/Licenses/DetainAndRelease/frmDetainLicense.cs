using DVLD.Business;
using DVLD.Core.Logging;
using DVLD.WinForms.Applications.ManageApplications;
using System;
using System.Windows.Forms;
using License = DVLD.Core.DTOs.Entities.License;

namespace DVLD.WinForms.Licenses.Detain
{
    public partial class frmDetainLicense : Form
    {
        public frmDetainLicense()
        {
            InitializeComponent();
            SetLabels();
        }

        private void SetLabels()
        {
            lblDetainDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblCreatedBy.Text = LoggedInUserInfo.Username;
        }

        private void ctrlFindLicense1_OnLicenseSelected(License obj)
        {
            lblLicenseId.Text = obj.LicenseID.ToString();
            btnDetain.Enabled = false;

            if (!obj.IsActive)
            {
                MessageBox.Show("Selected License is not active.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (obj.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("Selected License is expired.", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (DetainedLicenseService.IsDetained(obj.LicenseID))
            {
                MessageBox.Show("Selected License is already detained.", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                btnDetain.Enabled = true;
            }
        }

        private void ctrlFindLicense1_OnLicenseNullSelected()
        {
            MessageBox.Show("Selected License does not exist.", "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
            lblLicenseId.Text = "???";
            lblDetainId.Text = "???";
            btnDetain.Enabled = false;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (ctrlFindLicense1.License == null)
            {
                MessageBox.Show("Selected a License first.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Are you want to detain license with id {ctrlFindLicense1.License.LicenseID}?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int detainId = DetainedLicenseService.Detain(ctrlFindLicense1.License.LicenseID);

                    if (detainId > 0)
                    {
                        MessageBox.Show($"License detained successfully. Detain Id is {detainId}.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ctrlFindLicense1.Refresh();
                        btnDetain.Enabled = false;
                        lblDetainId.Text = detainId.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Error detaining license.",
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlFindLicense1.License != null)
            {
                var frm = new frmLicensesHistory(
                    ctrlFindLicense1.License.DriverId, frmLicensesHistory.LoadType.UsingDriverId);
                frm.ShowDialog();
            }
        }
    }
}

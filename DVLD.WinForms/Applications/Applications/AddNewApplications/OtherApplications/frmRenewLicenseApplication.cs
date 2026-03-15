using DVLD.Business;
using DVLD.WinForms.Licenses;
using System;
using System.Windows.Forms;
using License = DVLD.Core.DTOs.Entities.License;

namespace DVLD.WinForms.Applications.Applications.AddNewApplications.OtherApplications
{
    public partial class frmRenewLicenseApplication : Form
    {
        private int _newLicenseId = -1;

        public frmRenewLicenseApplication()
        {
            InitializeComponent();
        }

        private void ctrlLicenseAndApplicationInfo1_OnLicenseSelected(License obj)
        {
            if (obj == null)
            {
                MessageBox.Show("License does not exist.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (obj.ExpirationDate > DateTime.Now)
            {
                MessageBox.Show("License is not expired.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!LicenseService.IsActive(ctrlLicenseAndApplicationInfo1.License.LicenseID))
            {
                MessageBox.Show("License is not active.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DetainedLicenseService.IsDetained(ctrlLicenseAndApplicationInfo1.License.LicenseID))
            {
                MessageBox.Show("License is detained.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnRenew.Enabled = true;
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to renew the license?", "Confirm", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    _newLicenseId = LicenseService.Renew(ctrlLicenseAndApplicationInfo1.License.LicenseID);

                    if (_newLicenseId > 0)
                    {
                        MessageBox.Show($"License has been renewed successfully. New License Id = {_newLicenseId}.",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblShowNewLicenseInfo.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Error renewing the license", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_newLicenseId < 0)
                return;

            var frm = new frmLicenseInfo(_newLicenseId, frmLicenseInfo.LoadType.UsingLicenseId);
            frm.ShowDialog();
        }
    }
}

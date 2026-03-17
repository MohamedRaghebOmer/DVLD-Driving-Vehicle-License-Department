using DVLD.Business;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using System;
using System.Windows.Forms;
using License = DVLD.Core.DTOs.Entities.License;

namespace DVLD.WinForms.Licenses.Detain
{
    public partial class frmReleaseDetainedLicense : Form
    {
        DetainedLicense _detainedLicense = null;

        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
            SetLabels();
        }

        public frmReleaseDetainedLicense(int licenseId)
        {
            InitializeComponent();

            SetLabels();
            ctrlFindLicense1.Initialize(licenseId);
        }

        public frmReleaseDetainedLicense(License license)
        {
            InitializeComponent();
            SetLabels();

            ctrlFindLicense1.Initialize(license);
        }

        private void SetLabels()
        {
            decimal applicationFees = ApplicationTypeService.GetFees(ApplicationType.ReleaseDetainedDrivingLicense);

            lblApplicationFees.Text = applicationFees.ToString("C");
            lblTotalFees.Text = (applicationFees + 150).ToString("C");
            lblCreatedBy.Text = LoggedInUserInfo.Username;
            lblReleaseDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void ctrlFindLicense1_OnLicenseSelected(License obj)
        {
            btnRelease.Enabled = false;
            lblLicenseId.Text = obj.LicenseID.ToString();

            if (!obj.IsActive)
            {
                MessageBox.Show("License is not active", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (obj.ExpirationDate <= DateTime.Now)
            {
                MessageBox.Show("License is expired", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!DetainedLicenseService.IsDetained(obj.LicenseID))
            {
                MessageBox.Show("License is not detained", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _detainedLicense = DetainedLicenseService.GetById(obj.LicenseID);

                if (_detainedLicense == null)
                    return;

                lblDetainId.Text = _detainedLicense.DetainID.ToString();
                lblDetainDate.Text = _detainedLicense.DetainDate.ToString("dd/MM/yyyy");
                btnRelease.Enabled = true;
            }
        }

        private void ctrlFindLicense1_OnLicenseNullSelected()
        {
            MessageBox.Show("License does not exist.", "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);

            lblDetainId.Text = "???";
            lblLicenseId.Text = "???";
            lblDetainDate.Text = "???";
            btnRelease.Enabled = false;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (ctrlFindLicense1.License == null)
            {
                MessageBox.Show("Select a License first.", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Do you want to release license with id {ctrlFindLicense1.License.LicenseID}?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int releaseAppId = LicenseService.Release(ctrlFindLicense1.License.LicenseID);

                    if (releaseAppId > 0)
                    {
                        MessageBox.Show($"License released successfully.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        label13.Text = releaseAppId.ToString();
                        btnRelease.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show($"Error releasing the license.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlFindLicense1.License != null)
            {
                var frm = new frmLicenseInfo(
                    ctrlFindLicense1.License.LicenseID, frmLicenseInfo.LoadType.UsingLicenseId);
                frm.ShowDialog();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

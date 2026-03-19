using DVLD.Business;
using DVLD.WinForms.Licenses;
using System;
using System.Windows.Forms;
using License = DVLD.Core.DTOs.Entities.License;

namespace DVLD.WinForms.UserControls
{
    public partial class ctrlInternationalLicenseAndApplicationInfo : UserControl
    {
        public event Action<License> OnLicenseSelected;

        private License _license = null;

        public License LocalLicense { get => _license; }

        public ctrlInternationalLicenseAndApplicationInfo()
        {
            InitializeComponent();
        }

        public override void Refresh()
        {
            base.Refresh();
            btnFind_Click(null, null);
        }

        private void txtLicenseId_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            GetLicense();

            if (_license == null)
            {
                ctrlLicenseInfo1.LicenseId = -1; // Reset license info
                ctrlInternationalLicenseApplicationInfo1.Initialize(-1); // Reset license info
            }
            else
            {
                ctrlLicenseInfo1.LicenseId = _license.LicenseID;
                ctrlInternationalLicenseApplicationInfo1.Initialize(_license.LicenseID);
            }

            RefreshLinkedLabels();
            OnLicenseSelected?.Invoke(_license);
        }

        private void RefreshLinkedLabels()
        {
            lblShowInterLicenseInfo.Enabled =
                ctrlInternationalLicenseApplicationInfo1.InternationalLicense != null;
        }

        // Search for the license and assign it to private field: (_license)
        private void GetLicense()
        {
            int id = GetLicenseId();

            if (id == -1)
                return;

            _license = LicenseService.GetById(id);
        }

        // Gets license id from text box, validates it and returns -1 if invalid
        private int GetLicenseId()
        {
            if (string.IsNullOrEmpty(txtLicenseId.Text.Trim()))
            {
                MessageBox.Show("Please enter a license id.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            if (!int.TryParse(txtLicenseId.Text.Trim(), out int licenseId))
            {
                MessageBox.Show("Please enter a valid license id.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            if (licenseId <= 0)
            {
                MessageBox.Show("License does not exist.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            return licenseId;
        }

        private void lblShowDriverInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_license != null)
            {
                frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_license.LicenseID, frmLicenseInfo.LoadType.UsingLicenseId);
                frmLicenseInfo.ShowDialog();
            }
        }

        private void lblShowInterLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmInternationalLicenseInfo
                (ctrlInternationalLicenseApplicationInfo1.InternationalLicense.InternationalLicenseID);
            frm.ShowDialog();
        }
    }
}


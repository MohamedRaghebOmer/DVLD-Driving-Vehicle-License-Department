using DVLD.Business;
using System;
using System.Windows.Forms;
using License = DVLD.Core.DTOs.Entities.License;

namespace DVLD.WinForms.UserControls
{
    public partial class ctrlLicenseAndApplicationInfo : UserControl
    {
        public event Action<License> OnLicenseSelected;
        private License _license = null;

        public License License { get => _license; }

        public ctrlLicenseAndApplicationInfo()
        {
            InitializeComponent();
        }

        private void txtLicenseId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            GetLicense();

            if (_license == null)
            {
                ctrlLicenseInfo1.LicenseId = -1;
                ctrlApplicationInfo1.ApplicationId = -1;
            }
            else
            {
                ctrlLicenseInfo1.LicenseId = _license.LicenseID;
                ctrlApplicationInfo1.ApplicationId = _license.ApplicationId;
                OnLicenseSelected?.Invoke(_license);
            }
        }

        private void GetLicense()
        {
            int licenseId = GetLicenseId();

            if (licenseId <= 0 || (_license = LicenseService.GetById(licenseId)) == null)
            {
                MessageBox.Show("License does not exist.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetLicenseId()
        {
            if (string.IsNullOrEmpty(txtLicenseId.Text))
                return -1;

            if (int.TryParse(txtLicenseId.Text, out int licenseId))
                return licenseId;
            else
                return -1;
        }
    }
}

using DVLD.Business;
using System;
using System.Windows.Forms;
using License = DVLD.Core.DTOs.Entities.License;


namespace DVLD.WinForms.UserControls
{
    public partial class ctrlFindLicense : UserControl
    {
        public event Action<License> OnLicenseSelected;
        public event Action OnLicenseNullSelected;
        private License _license = null;

        public License License { get => _license; }

        public ctrlFindLicense()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            GetLicense();

            if (_license == null)
            {
                ctrlLicenseInfo1.LicenseId = -1;
                OnLicenseNullSelected?.Invoke();
            }
            else
            {
                ctrlLicenseInfo1.LicenseId = _license.LicenseID;
                OnLicenseSelected?.Invoke(_license);
            }
        }

        private void GetLicense()
        {
            int licenseId = GetLicenseId();

            if (licenseId <= 0 || (_license = LicenseService.GetById(licenseId)) == null)
            {
                _license = null;
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

        private void txtLicenseId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}

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
        private bool _alreadyHaveLicense = false;

        public License License { get => _license; }

        public ctrlFindLicense()
        {
            InitializeComponent();
        }

        public void Initialize(License license)
        {
            _alreadyHaveLicense = true;
            txtLicenseId.Text = license.LicenseID.ToString();

            if (license.LicenseID > 0)
                this._license = license;

            btnFind_Click(null, null);
        }

        public void Initialize(int licenseId)
        {
            _alreadyHaveLicense = true;

            txtLicenseId.Text = licenseId.ToString();
            this._license = LicenseService.GetById(licenseId);

            btnFind_Click(null, null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!_alreadyHaveLicense)
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

            if (licenseId > 0)
            {
                _license = LicenseService.GetById(licenseId);
            }
            else
            {
                _license = null;
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

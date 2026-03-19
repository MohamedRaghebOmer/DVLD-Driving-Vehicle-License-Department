using DVLD.Business;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.WinForms.Licenses;
using System;
using System.Windows.Forms;
using License = DVLD.Core.DTOs.Entities.License;

namespace DVLD.WinForms.UserControls
{
    public partial class ctrlInternationalLicenseApplicationInfo : UserControl
    {
        private License _license = new License();
        private InternationalLicense _internationalLicense = null;

        public ctrlInternationalLicenseApplicationInfo()
        {
            InitializeComponent();
            SetDefaults();
        }

        public License LocalLicense
        {
            get => _license;
        }

        public InternationalLicense InternationalLicense
        {
            get => _internationalLicense;
        }

        public void Initialize(int localLicenseId)
        {
            if (localLicenseId <= 0)
            {
                SetDefaults();
                return;
            }

            this._license = LicenseService.GetById(localLicenseId);

            if (_license == null) return;

            this._internationalLicense = InternationalLicenseService.GetByLocalLicenseId(_license.LicenseID);
            LoadControl();
        }

        public override void Refresh() => Initialize(_license.LicenseID);

        private void SetDefaults()
        {
            ResetUI();
            string todayDate = DateTime.Today.Date.ToShortDateString();

            _license = null;
            lblApplicationDate.Text = todayDate;
            lblissueDate.Text = todayDate;
            lblFees.Text = ApplicationTypeService.GetFees(ApplicationType.NewInternationalLicense).ToString("C");
            lblExpirationDate.Text = DateTime.Today.AddYears(1).ToShortDateString();
            lblCreatedBy.Text = LoggedInUserInfo.Username;
        }

        private void ResetUI()
        {
            lblShowLocalLicenseInfo.Enabled = false;
            lblLocalLicenseId.Text = "???";
            lblInternationalLicenseAppId.Text = "???";
            lblInternationalLicenseId.Text = "???";
            lblApplicationDate.Text = "???";
            lblissueDate.Text = "???";
            lblFees.Text = "???";
            lblExpirationDate.Text = "???";
            lblCreatedBy.Text = "???";
        }

        private void LoadControl()
        {
            if (_license == null)
                return;

            lblShowLocalLicenseInfo.Enabled = true;

            bool hasInternationalLicense = _internationalLicense != null;

            if (hasInternationalLicense)
            {
                lblInternationalLicenseAppId.Text = _internationalLicense.ApplicationID.ToString();
                lblInternationalLicenseId.Text = _internationalLicense.InternationalLicenseID.ToString();
            }

            lblLocalLicenseId.Text = _license.LicenseID.ToString();
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_license == null) return;

            frmLicenseInfo frm = new frmLicenseInfo(_license.LicenseID, frmLicenseInfo.LoadType.UsingLicenseId);
            frm.ShowDialog();
        }
    }
}

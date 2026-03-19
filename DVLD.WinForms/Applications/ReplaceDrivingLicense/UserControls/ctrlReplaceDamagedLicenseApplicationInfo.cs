using DVLD.Business;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using System;
using System.Windows.Forms;
using Application = DVLD.Core.DTOs.Entities.Application;

namespace DVLD.WinForms.UserControls
{
    public partial class ctrlReplaceDamagedLicenseApplicationInfo : UserControl
    {
        public string LabelOldLicenseId
        {
            get => lblOldLicenseId.Text;
            set => lblOldLicenseId.Text = value;
        }

        public ctrlReplaceDamagedLicenseApplicationInfo()
        {
            InitializeComponent();
            SetDefaults();
        }

        public void Initialize(ApplicationType applicationType)
        {
            SetDefaults();
            SetLabels(applicationType);
        }

        public void Initialize(int _newLicenseIdAfterReplacement)
        {
            SetDefaults();
            Application application = null;

            if (_newLicenseIdAfterReplacement <= 0
                || (application = ApplicationService.GetByLicenseId(_newLicenseIdAfterReplacement)) == null)
                return;

            if (ValidateApplication(application))
                SetControlUI(application);
        }

        private bool ValidateApplication(Application application)
        {
            if ((application.ApplicationTypeID != ApplicationType.ReplacementForDamagedDrivingLicense
                || application.ApplicationTypeID != ApplicationType.ReplacementForLostDrivingLicense)
                && application.ApplicationStatus != ApplicationStatus.Completed)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SetControlUI(Application application)
        {
            SetLabels(application);

            lblRetakeAppId.Text = application.ApplicationID.ToString();
            lblNewLicenseIdAfterReplacement.Text = LicenseService.GetNewLicenseIdAfterReplacement(application.ApplicationID).ToString();
            lblOldLicenseId.Text = LicenseService.GetOldLicenseIdAfterReplacement(application.ApplicationID).ToString();
        }

        private void SetDefaults()
        {
            ResetLabels();
            lblApplicationDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            lblCreatedBy.Text = LoggedInUserInfo.Username;
        }

        private void ResetLabels()
        {
            lblRetakeAppId.Text = "???";
            lblNewLicenseIdAfterReplacement.Text = "???";
            lblNewLicenseIdAfterReplacement.Text = "???";
            lblApplicationDate.Text = "???";
            lblApplicationFees.Text = "???";
            lblNewLicenseIdAfterReplacement.Text = "???";
            lblOldLicenseId.Text = "???";
            lblCreatedBy.Text = "???";
        }

        private void SetLabels(ApplicationType applicationType)
        {
            lblApplicationFees.Text = ApplicationTypeService.GetFees(applicationType).ToString("C");
        }

        private void SetLabels(Application application)
        {
            lblApplicationFees.Text = application.PaidFees.ToString("C");
        }
    }
}

using DVLD.Business;
using DVLD.Core.DTOs.Enums;
using DVLD.WinForms.Licenses;
using System.Windows.Forms;

namespace DVLD.WinForms.UserControls
{
    public partial class ctrlApplicationInfo : UserControl
    {
        Core.DTOs.Entities.Application application = null;
        private int _Id = -1;

        private enum LoadType { UsingApplicationId, UsingLocalApplicationId };
        private LoadType _loadType = LoadType.UsingApplicationId;

        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }

        public int ApplicationId
        {
            get
            {
                if (_loadType == LoadType.UsingApplicationId)
                    return _Id;
                else
                    return -1;
            }

            set
            {
                if (value <= 0 || value == _Id)
                    return;

                _Id = value;
                _loadType = LoadType.UsingApplicationId;
                LoadControlInfo();
            }
        }

        public int LocalApplicationId
        {
            get
            {
                if (_loadType == LoadType.UsingLocalApplicationId)
                    return _Id;
                else
                    return -1;
            }

            set
            {
                if (value <= 0 || value == _Id)
                    return;

                _Id = value;
                _loadType = LoadType.UsingLocalApplicationId;
                LoadControlInfo();
            }
        }

        private void LoadControlInfo()
        {
            if (_loadType == LoadType.UsingApplicationId)
            {
                application = ApplicationService.GetById(ApplicationId);
            }
            else // UsingLocalApplicationId
            {
                application = ApplicationService.GetByLocalAppId(LocalApplicationId);
            }

            if (application == null)
            {
                lblShowLicenseInfo.Enabled = false;
                return;
            }

            #region Set control labels

            var localApp = LocalDrivingLicenseApplicationService.
                GetByApplicationId(application.ApplicationID);

            if (application.ApplicationStatus != ApplicationStatus.Completed)
                lblShowLicenseInfo.Enabled = false;

            lblPassedTests.Text = TestService.GetNumOfPassedTests(localApp.LocalDrivingLicenseApplicationID).ToString() + "/3";
            lblLocalAppId.Text = localApp.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = LicenseClassService.GetLicenseClassName(localApp.LicenseClassID);
            lblApplicationId.Text = application.ApplicationID.ToString();
            lblApplicantPersonId.Text = application.ApplicantPersonID.ToString();
            lblApplicationDate.Text = application.LastStatusDate.ToString("dd/MM/yyy");
            lblPaidFees.Text = application.PaidFees.ToString();

            // Set lblApplicationType
            switch (application.ApplicationTypeID)
            {
                case ApplicationType.NewLocalDrivingLicenseService:
                    lblApplicationType.Text = "New Local Driving License Service";
                    break;

                case ApplicationType.RenewDrivingLicenseService:
                    lblApplicationType.Text = "Renew Driving License Service";
                    break;

                case ApplicationType.ReplacementForLostDrivingLicense:
                    lblApplicationType.Text = "Replacement For Lost Driving License";
                    break;

                case ApplicationType.ReplacementForDamagedDrivingLicense:
                    lblApplicationType.Text = "Replacement For Damaged Driving License";
                    break;

                case ApplicationType.ReleaseDetainedDrivingLicense:
                    lblApplicationType.Text = "Release Detained Driving License";
                    break;

                case ApplicationType.NewInternationalLicense:
                    lblApplicationType.Text = "New International License";
                    break;

                case ApplicationType.RetakeTest:
                    lblApplicationType.Text = "Retake Test";
                    break;
            }

            lblApplicationStatus.Text = application.ApplicationStatus.ToString();
            lblLastStatusDate.Text = application.LastStatusDate.ToString("dd/MM/yyyy");
            lblCreatedByUserId.Text = application.CreatedByUserID.ToString();

            #endregion
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicenseInfo(application.ApplicationID, frmLicenseInfo.LoadType.UsingApplicationId);
            frm.ShowDialog();
        }

        public override void Refresh() => LoadControlInfo(); //Refresh
    }
}

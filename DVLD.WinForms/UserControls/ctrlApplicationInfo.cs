using DVLD.Business;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.WinForms.Applications.ManageApplications;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
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

            if (application == null) return;

            #region Set control labels

            var localApp = LocalDrivingLicenseApplicationService.
                GetByApplicationId(application.ApplicationID);

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

            if (application.ApplicationStatus == ApplicationStatus.Completed
                && application.ApplicationTypeID != ApplicationType.RetakeTest)
            {
                ctrlLicenseInfo1.LocalApplicationId = LocalApplicationId;
            }
            else
            {
                ctrlLicenseInfo1.Enabled = false;
            }
        }

        private void lblShowHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicensesHistory(application.ApplicantPersonID, frmLicensesHistory.LoadType.UsingPersonId);
            frm.ShowDialog();
        }

        private void lblShowPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmAddEditPersonInfo(application.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void lblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicensesHistory(application.ApplicantPersonID, frmLicensesHistory.LoadType.UsingPersonId);
            frm.ShowDialog();
        }
    }
}

using DVLD.Core.DTOs.Enums;
using System;
using System.Windows.Forms;
using License = DVLD.Core.DTOs.Entities.License;

namespace DVLD.WinForms.UserControls
{
    public partial class ctrlLicenseReplacementInfo : UserControl
    {
        public event Action<License> OnLicenseSelected;
        private ApplicationType _applicationType;
        public License License { get => ctrlFindLicense1.License; }

        public ctrlLicenseReplacementInfo()
        {
            InitializeComponent();
        }

        public void Initialize(ApplicationType applicationType)
        {
            this._applicationType = applicationType;
            ctrlReplaceDamagedLicenseApplicationInfo1.Initialize(applicationType);
        }

        public void Initialize(int _newLicenseIdAfterReplacement)
        {
            ctrlReplaceDamagedLicenseApplicationInfo1.Initialize(_newLicenseIdAfterReplacement);
        }

        private void ctrlFindLicense1_OnLicenseNullSelected()
        {
            // Reset the control
            ctrlReplaceDamagedLicenseApplicationInfo1.LabelOldLicenseId = "???";
            ctrlReplaceDamagedLicenseApplicationInfo1.Initialize(-1);
            ctrlReplaceDamagedLicenseApplicationInfo1.Initialize(_applicationType);
        }

        private void ctrlFindLicense1_OnLicenseSelected(License obj)
        {
            ctrlReplaceDamagedLicenseApplicationInfo1.LabelOldLicenseId = obj.LicenseID.ToString();
            OnLicenseSelected?.Invoke(obj);
        }
    }
}

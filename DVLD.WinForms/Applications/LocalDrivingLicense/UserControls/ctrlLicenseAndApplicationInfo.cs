using System;
using System.Windows.Forms;
using License = DVLD.Core.DTOs.Entities.License;

namespace DVLD.WinForms.UserControls
{
    public partial class ctrlLicenseAndApplicationInfo : UserControl
    {
        public event Action<License> OnLicenseSelected;

        public License License { get => ctrlFindLicense1.License; }

        public ctrlLicenseAndApplicationInfo()
        {
            InitializeComponent();
        }

        private void ctrlFindLicense1_OnLicenseSelected(License obj)
        {
            if (this.License == null)
            {
                ctrlApplicationInfo1.ApplicationId = -1;
            }
            else
            {

                ctrlApplicationInfo1.ApplicationId = this.License.ApplicationId;
                OnLicenseSelected?.Invoke(this.License);
            }
        }

        private void ctrlFindLicense1_OnLicenseNullSelected()
        {
            ctrlApplicationInfo1.ApplicationId = -1;
        }
    }
}

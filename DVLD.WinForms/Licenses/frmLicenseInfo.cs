using System;
using System.Windows.Forms;

namespace DVLD.WinForms.Licenses
{
    public partial class frmLicenseInfo : Form
    {
        private int _licenseId = -1, _applicationId = -1, _localApplicationId = -1;
        public enum LoadType
        {
            UsingLicenseId,
            UsingApplicationId,
            UsingLocalApplicationId
        };
        private LoadType _loadType = LoadType.UsingLicenseId;

        public frmLicenseInfo(int _Id, LoadType loadType)
        {
            InitializeComponent();

            this._licenseId = _Id;
            _loadType = loadType;

            switch (_loadType)
            {
                case LoadType.UsingLicenseId:
                    ctrlLicenseInfo1.LicenseId = _Id;
                    break;

                case LoadType.UsingApplicationId:
                    ctrlLicenseInfo1.ApplicationId = _Id;
                    break;

                case LoadType.UsingLocalApplicationId:
                    ctrlLicenseInfo1.LocalApplicationId = _Id;
                    break;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

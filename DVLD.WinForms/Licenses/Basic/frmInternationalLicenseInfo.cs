using System;
using System.Windows.Forms;

namespace DVLD.WinForms.Licenses
{
    public partial class frmInternationalLicenseInfo : Form
    {
        public frmInternationalLicenseInfo(int internationalLicenseID)
        {
            InitializeComponent();

            if (internationalLicenseID > 0)
            {
                ctrlInternationalLicenseInfo1.Initialize(internationalLicenseID);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.WinForms.Licenses
{
    public partial class frmLicenseInfo : Form
    {
        private int _licenseId = -1;
        private string _nationalNo = string.Empty;
        private bool _usingNationalNo = false;

        public frmLicenseInfo(int licenseId)
        {
            InitializeComponent();
            this._licenseId = licenseId;
            ctrlLicenseInfo1.LicenseId = licenseId;
        }

        public frmLicenseInfo(string nationalNo)
        {
            InitializeComponent();
            this._nationalNo = nationalNo;
            this._usingNationalNo = true;
            ctrlLicenseInfo1.NationalNo = nationalNo;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

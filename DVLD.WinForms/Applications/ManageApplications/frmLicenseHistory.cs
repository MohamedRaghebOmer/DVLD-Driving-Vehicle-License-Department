using DVLD.Business;
using System;
using System.Windows.Forms;

namespace DVLD.WinForms.Applications.ManageApplications
{
    public partial class frmLicenseHistory : Form
    {
        private int _driverId = -1;
        private string _nationalNo = string.Empty;
        private bool _usingNationalNo = false;

        public frmLicenseHistory(int driverId)
        {
            InitializeComponent();
            this._driverId = driverId;
            ctrlPersonDetails1.PersonID = driverId;
        }

        public frmLicenseHistory(string nationalNo)
        {
            InitializeComponent();
            this._nationalNo = nationalNo;
            this._usingNationalNo = true;
            ctrlPersonDetails1.NationalNo = nationalNo;
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            if (!this._usingNationalNo) // Using driver id
            {
                // Load local license history
                dgvLocal.DataSource =
                    LicenseService.GetLicenseHistory(this._driverId);

                // Load international license history
                dgvInternational.DataSource =
                InternationalLicenseService.GetLicenseHistory(this._driverId);
            }
            else // Using national no
            {
                // Load local license history
                dgvLocal.DataSource =
                    LicenseService.GetLicenseHistory(this._nationalNo);

                // Load international license history
                dgvInternational.DataSource =
                InternationalLicenseService.GetLicenseHistory(this._nationalNo);
            }
        }

        private void UpdateLabelCount(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                lblCount.Text = dgvLocal.Rows.Count.ToString();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                lblCount.Text = dgvInternational.Rows.Count.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

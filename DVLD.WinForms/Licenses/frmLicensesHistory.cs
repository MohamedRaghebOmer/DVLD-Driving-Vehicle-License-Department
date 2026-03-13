using DVLD.Business;
using System;
using System.Windows.Forms;

namespace DVLD.WinForms.Applications.ManageApplications
{
    public partial class frmLicensesHistory : Form
    {
        private int _id = -1;
        private string _nationalNo = string.Empty;

        public enum LoadType { UsingDriverId, UsingNationalNo, UsingPersonId }
        private LoadType _frmLoadType = LoadType.UsingDriverId;


        public frmLicensesHistory(int id, LoadType loadType)
        {
            InitializeComponent();

            this._id = id;

            if (loadType == LoadType.UsingDriverId)
            {
                _frmLoadType = LoadType.UsingDriverId;
                ctrlPersonDetails1.DriverID = id;
            }
            else if (loadType == LoadType.UsingPersonId)
            {
                _frmLoadType = LoadType.UsingPersonId;
                ctrlPersonDetails1.PersonID = id;
            }
        }

        public frmLicensesHistory(string nationalNo)
        {
            InitializeComponent();
            this._nationalNo = nationalNo;
            _frmLoadType = LoadType.UsingNationalNo;
            ctrlPersonDetails1.NationalNo = nationalNo;
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_frmLoadType == LoadType.UsingDriverId) // Using driver id
            {
                // Load local license history
                dgvLocal.DataSource =
                    LicenseService.GetLicenseHistoryByDriverId(this._id);

                // Load international license history
                dgvInternational.DataSource =
                InternationalLicenseService.GetLicenseHistoryByDriverId(this._id);
            }
            else if (_frmLoadType == LoadType.UsingPersonId)
            {
                // Load local license history
                dgvLocal.DataSource =
                    LicenseService.GetLicenseHistoryByPersonId(this._id);

                // Load international license history
                dgvInternational.DataSource =
                InternationalLicenseService.GetLicenseHistoryByPersonId(this._id);

            }
            else // Using national no
            {
                // Load local license history
                dgvLocal.DataSource =
                    LicenseService.GetLicenseHistoryByNationalNo(this._nationalNo);

                // Load international license history
                dgvInternational.DataSource =
                InternationalLicenseService.GetLicenseHistoryByNationalNo(this._nationalNo);
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

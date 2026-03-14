using DVLD.Business;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Helpers;
using DVLD.WinForms.Applications.ManageApplications;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DVLD.WinForms.UserControls
{
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        InternationalLicense _internationalLicense = null;
        License _license = null;
        Person _person = null;

        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
            SetDefaults();
        }

        public void Initialize(int internationalLicenseID)
        {
            if (internationalLicenseID <= 0
                || (_internationalLicense = InternationalLicenseService.GetById(internationalLicenseID)) == null)
            {
                return;
            }

            _license = LicenseService.GetById(_internationalLicense.IssuedUsingLocalLicenseID);
            _person = PersonService.GetByDriverId(_internationalLicense.DriverID);
            LoadControl();
        }

        private void SetDefaults()
        {
            this._internationalLicense = null;

            lblShowLicensesHistory.Enabled = false;
            lblDriverId.Text = "???";
            lblIntLicenseId.Text = "???";
            lblDriverName.Text = "???";
            lblLicenseId.Text = "???";
            lblNationalNo.Text = "???";
            lblGender.Text = "???";
            lblIssueDate.Text = "???";
            lblApplicationId.Text = "???";
            lblIsActive.Text = "???";
            lblDateOfBirth.Text = "???";
            lblExpirationDate.Text = "???";

            pbDriverImage.Image?.Dispose();
            pbDriverImage.Image = Properties.Resources.ManWithQuestionMark72;
        }

        private void LoadControl()
        {
            if (_internationalLicense == null || _person == null || _license == null)
            {
                SetDefaults();
                return;
            }

            lblShowLicensesHistory.Enabled = true;

            lblDriverName.Text = $"{_person.FirstName} {_person.SecondName} {_person.ThirdName} {_person.LastName}";
            lblIntLicenseId.Text = _internationalLicense.InternationalLicenseID.ToString();
            lblLicenseId.Text = _license.LicenseID.ToString();
            lblNationalNo.Text = _person.NationalNumber;
            lblGender.Text = _person.Gender.ToString();
            lblIssueDate.Text = _internationalLicense.IssueDate.ToShortDateString();
            lblApplicationId.Text = _internationalLicense.ApplicationID.ToString();
            lblIsActive.Text = _internationalLicense.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _person.DateOfBirth.ToShortDateString();
            lblDriverId.Text = _internationalLicense.DriverID.ToString();
            lblExpirationDate.Text = _internationalLicense.ExpirationDate.ToShortDateString();

            pbDriverImage.Image?.Dispose();
            string imagePath = Path.Combine(PathHelper.ImagesFolderPath, _person.ImagePath);

            if (!string.IsNullOrEmpty(_person.ImagePath) && File.Exists(imagePath))
            {
                pbDriverImage.Image = Image.FromFile(imagePath);
            }
            else
            {
                pbDriverImage.Image = Properties.Resources.ManWithQuestionMark72;
            }
        }

        private void lblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_license != null)
            {
                var frm = new frmLicensesHistory(_license.DriverId, frmLicensesHistory.LoadType.UsingDriverId);
                frm.ShowDialog();
            }
        }
    }
}

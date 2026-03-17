using DVLD.Business;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.WinForms.Applications.ManageApplications;
using DVLD.WinForms.People;
using DVLD.WinForms.Properties;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DVLD.WinForms.UserControls
{
    public partial class ctrlLicenseInfo : UserControl
    {
        private int _licenseId = -1, _applicationId = -1,
            _localApplicationId = -1, _personId = -1;
        private string _nationalNo = string.Empty;

        private enum LoadType
        {
            UsingLicenseId,
            UsingNationalNo,
            UsingApplicationId,
            UsingLocalApplicationId
        }

        private LoadType _loadType = LoadType.UsingLicenseId;

        public ctrlLicenseInfo()
        {
            InitializeComponent();
        }

        public int LicenseId
        {
            get
            {
                if (_loadType == LoadType.UsingLicenseId)
                    return _licenseId;
                else
                    return -1;
            }

            set
            {
                if (value <= 0)
                {
                    SetDefaults();
                    return;
                }

                this._licenseId = value;
                _loadType = LoadType.UsingLicenseId;
                LoadLicenseInfo();
            }
        }

        public string NationalNo
        {
            get
            {
                if (_loadType == LoadType.UsingNationalNo)
                    return _nationalNo;
                else
                    return string.Empty;
            }

            set
            {
                if (string.IsNullOrEmpty(_nationalNo) || value == _nationalNo)
                    return;

                _nationalNo = value;
                _loadType = LoadType.UsingNationalNo;
                LoadLicenseInfo();
            }
        }

        public int ApplicationId
        {
            get
            {
                if (_loadType == LoadType.UsingApplicationId)
                    return _applicationId;
                else
                    return -1;
            }

            set
            {
                if (value <= 0 || value == _applicationId) return;

                _applicationId = value;
                _loadType = LoadType.UsingApplicationId;
                LoadLicenseInfo();
            }
        }

        private void SetDefaults()
        {
            _licenseId = -1;
            _applicationId = -1;
            _localApplicationId = -1;
            _personId = -1;
            _nationalNo = string.Empty;

            lblClass.Text = "???";
            lblDriverName.Text = "???";
            lblLicenseId.Text = "???";
            lblNationalNo.Text = "???";
            lblGender.Text = "???";
            lblIssueDate.Text = "???";
            lblNotes.Text = "???";
            lblIsActive.Text = "???";
            lblDateOfBirth.Text = "???";
            lblDriverId.Text = "???";
            lblExpirationDate.Text = "???";
            lblIsDetained.Text = "???";
            lblIssueReason.Text = "???";
            lblShowLicensesHistory.Enabled = false;

            pbDriverImage.Image?.Dispose();
            pbDriverImage.Image = Resources.ManWithQuestionMark72;
        }

        private void lblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicensesHistory(_personId, frmLicensesHistory.LoadType.UsingPersonId);
            frm.ShowDialog();
        }

        private void lblShowPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_personId > 0)
            {
                Form frm = new frmPersonDetails(_personId);
                frm.ShowDialog();
            }

        }

        public int LocalApplicationId
        {
            get
            {
                if (_loadType == LoadType.UsingLocalApplicationId)
                    return _localApplicationId;
                else
                    return -1;
            }

            set
            {
                if (value <= 0 || value == _localApplicationId)
                    return;

                _localApplicationId = value;
                _loadType = LoadType.UsingLocalApplicationId;
                LoadLicenseInfo();
            }
        }

        private void LoadLicenseInfo()
        {
            License license = null;
            Person person = null;

            if (LicenseId > 0)
            {
                license = LicenseService.GetById(LicenseId);
                person = PersonService.GetByDriverId(license.DriverId);
            }
            else if (!string.IsNullOrEmpty(NationalNo))
            {
                license = LicenseService.GetByNationalNo(NationalNo);
                person = PersonService.GetByNationalNo(NationalNo);
            }
            else if (ApplicationId > 0)
            {
                license = LicenseService.GetByApplicationId(ApplicationId);
                person = PersonService.GetByApplicationId(ApplicationId);
            }
            else if (LocalApplicationId > 0)
            {
                license = LicenseService.GetByLocalApplicationId(LocalApplicationId);
                person = PersonService.GetByLocalApplicationId(LocalApplicationId);
            }

            if (license == null || person == null)
            {
                SetDefaults();
                this.Enabled = false;
                return;
            }

            lblShowLicensesHistory.Enabled = true;
            _personId = person.PersonID;
            lblClass.Text = LicenseClassService.GetLicenseClassName(license.LicenseClass);
            lblDriverName.Text = $"{person.FirstName} {person.SecondName} " +
                $"{person.ThirdName} {person.LastName}";
            lblLicenseId.Text = license.LicenseID.ToString();
            lblNationalNo.Text = person.NationalNumber;
            lblGender.Text = (person.Gender == Gender.Male) ? "Male" : "Female";
            lblIssueDate.Text = license.IssueDate.ToString("dd/MM/yyyy");
            lblIssueReason.Text = license.IssueReason.ToString();
            lblNotes.Text = license.Notes;
            lblIsActive.Text = (license.IsActive) ? "Yes" : "No";
            lblDateOfBirth.Text = person.DateOfBirth.ToString("dd/MM/yyyy");
            lblDriverId.Text = license.DriverId.ToString();
            lblExpirationDate.Text = license.ExpirationDate.ToString("dd/MM/yyyy");
            lblIsDetained.Text =
                DetainedLicenseService.IsDetained(license.LicenseID) ?
                "Yes" : "No";

            string path = PersonService.GetImagePathByFileName(person.ImagePath);

            if (File.Exists(path))
            {
                using (var img = Image.FromFile(path))
                {
                    pbDriverImage.Image = new Bitmap(img);
                }
            }
        }
    }
}

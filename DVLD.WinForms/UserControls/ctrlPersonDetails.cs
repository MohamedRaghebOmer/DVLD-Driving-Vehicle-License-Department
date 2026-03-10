using DVLD.Business;
using DVLD.Core.DTOs.Entities;
using DVLD.WinForms.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Security.Permissions;
using System.Windows.Forms;
using DVLD.Core.Helpers;

namespace DVLD.WinForms.UserControls
{
    public partial class ctrlPersonDetails : UserControl
    {
        private int _id = -1;
        private string _nationalNo = string.Empty;

        private enum LoadType { UsingPersonId, UsingNationalNo, UsingDriverId};
        private LoadType _loadType = LoadType.UsingPersonId;

        public ctrlPersonDetails()
        {
            InitializeComponent();
        }

        private void ctrlPersonDetails_Load(object sender, EventArgs e)
        {
            SetDefaults();
            SetValues();
        }


        public int PersonID
        {
            get
            {
                if (_loadType == LoadType.UsingPersonId)
                    return _id;
                else
                    return -1;
            }
            set
            {
                if (value == _id && _loadType == LoadType.UsingPersonId)
                    return;

                if (value <= 0) SetDefaults();

                _id = value;
                _loadType = LoadType.UsingPersonId;
                ctrlPersonDetails_Load(null, null);
            }
        }

        public int DriverID
        {
            get
            {
                if (_loadType == LoadType.UsingDriverId)
                    return _id;
                else
                    return -1;
            }
            set
            {
                if (value == _id && _loadType == LoadType.UsingDriverId)
                    return;

                if (value <= 0) SetDefaults();

                _id = value;
                _loadType = LoadType.UsingDriverId;
                ctrlPersonDetails_Load(null, null);
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
                if (string.IsNullOrEmpty(value) ||
                    (value == _nationalNo && _loadType == LoadType.UsingNationalNo))
                {
                    return;
                }

                _nationalNo = value;
                _loadType = LoadType.UsingNationalNo;
                ctrlPersonDetails_Load(null, null);
            }
        }

        private void SetDefaults()
        {
            lblPersonID.Text = "???";
            lblName.Text = "???";
            lblNationalNo.Text = "???";
            lblGender.Text = "???";
            lblEmail.Text = "???";
            lblAddress.Text = "???";
            lblDateOfBirth.Text = "???";
            lblPhone.Text = "???";
            lblCountry.Text = "???";
            pbPersonImage.Image = Resources.ManWithQuestionMark72;
            pbGender.Image = Resources.MaleSign512;
        }

        private void SetValues()
        {
            Person person = null;

            if (!string.IsNullOrEmpty(NationalNo))
            {
                person = PersonService.GetByNationalNo(_nationalNo);
            }
            else if (PersonID > 0) // get property auto validate on load type
            {
                person = PersonService.GetById(_id);
            }
            else if (DriverID > 0)
            {
                person = PersonService.GetByDriverId(_id);
            }

            if (person == null) return;

            // Gender handling
            bool isMale = person.Gender == 0;
            if (isMale)
            {
                lblGender.Text = "Male";
                pbGender.Image = Resources.MaleSign512;
                pbPersonImage.Image = Resources.ManWithQuestionMark72;
            }
            else
            {
                lblGender.Text = "Female";
                pbGender.Image = Resources.FemaleSign512;
                pbPersonImage.Image = Resources.WomanWithQuestionMark72;
            }

            lblPersonID.Text = person.PersonID.ToString();

            string third = string.IsNullOrWhiteSpace(person.ThirdName) ? string.Empty : " " + person.ThirdName.Trim();
            string name = $"{person.FirstName} {person.SecondName}{third} {person.LastName}";
            lblName.Text = name.Replace("  ", " ").Trim();

            lblNationalNo.Text = person.NationalNumber;
            lblEmail.Text = person.Email ?? string.Empty;
            lblAddress.Text = person.Address;
            lblDateOfBirth.Text = person.DateOfBirth.ToString("dd/MM/yyyy");
            lblPhone.Text = person.Phone;
            lblCountry.Text = CountryService.GetName(person.NationalityCountryID) ?? string.Empty;

            if (!string.IsNullOrEmpty(person.ImagePath))
                LoadPersonImage(Path.Combine(PathHelper.ImagesFolderPath, person.ImagePath));
        }

        private void LoadPersonImage(string imagePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
                    return;

                // Dispose old image to prevent memory leak
                pbPersonImage.Image?.Dispose();
                pbPersonImage.Image = null;

                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Image img = Image.FromStream(fs))
                {
                    pbPersonImage.Image = new Bitmap(img); // clone so stream can close safely
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load image: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void lblEditPersonInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (PersonID > 0)
            {
                using (Form frm = new frmAddEditPersonInfo(PersonID))
                {
                    frm.ShowDialog();
                    ctrlPersonDetails_Load(null, null);
                }
            }
            else if (!string.IsNullOrEmpty(NationalNo))
            {
                int id = PersonService.GetIdByNationalNo(NationalNo);
                if (id < 1) return;
                using (Form frm = new frmAddEditPersonInfo(id))
                {
                    frm.ShowDialog();
                    ctrlPersonDetails_Load(null, null);
                }
            }
        }
    }
}
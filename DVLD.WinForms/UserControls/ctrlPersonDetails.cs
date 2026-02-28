using DVLD.Business;
using DVLD.Core.DTOs.Entities;
using DVLD.WinForms.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DVLD.WinForms.UserControls
{
    public partial class ctrlPersonDetails : UserControl
    {
        private int _personID = -1;
        private string _nationalNo = string.Empty;
        private bool _usingNationalNo = false;

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
            get => _personID;
            set
            {
                if (value == _personID && !_usingNationalNo) return;

                if (value <= 0) SetDefaults();

                _personID = value;
                _usingNationalNo = false;
                ctrlPersonDetails_Load(null, null);
            }
        }

        public string NationalNo
        {
            get => _nationalNo;
            set
            {
                if (string.IsNullOrEmpty(value) || value == _nationalNo && _usingNationalNo) return;

                _nationalNo = value;
                _usingNationalNo = true;
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

            if (_usingNationalNo && !string.IsNullOrEmpty(_nationalNo))
            {
                person = PersonService.GetByNationalNo(_nationalNo);
            }
            else if (!_usingNationalNo && _personID > 0)
            {
                person = PersonService.GetById(_personID);
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

            LoadPersonImage();
        }

        private void LoadPersonImage()
        {
            try
            {
                string imagePath = string.Empty;

                if (_usingNationalNo)
                    imagePath = PersonService.GetImagePathByNationalNo(_nationalNo);
                else
                    imagePath = PersonService.GetImagePathByPersonId(_personID);

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
            if (!_usingNationalNo && _personID > 0)
            {
                using (Form frm = new frmAddEditPersonInfo(_personID))
                {
                    frm.ShowDialog();
                    ctrlPersonDetails_Load(null, null);
                }
            }
            else if (_usingNationalNo && !string.IsNullOrEmpty(_nationalNo))
            {
                int id = PersonService.GetIdByNationalNo(_nationalNo);
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
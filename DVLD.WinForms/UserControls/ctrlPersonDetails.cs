using DVLD.Business;
using DVLD.Core.DTOs.Entities;
using DVLD.WinForms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.WinForms.UserControls
{
    public partial class ctrlPersonDetails : UserControl
    {
        public event Action OnEditPersonInfo;

        public int PersonID { get; set; }
        
        public ctrlPersonDetails()
        {
            InitializeComponent();
        }

        private void ctrlPersonDetails_Load(object sender, EventArgs e)
        {
            SetValues();
        }

        private void SetValues()
        {
            Person person = PersonBusiness.GetById(PersonID);

            if (person == null) return;

            if (person.ThirdName == null)
                person.ThirdName = string.Empty;

            if (person.Email == null)
                person.Email = string.Empty;

            if ((int)person.Gender == 0)
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
            lblName.Text = person.FirstName + " " + person.SecondName + " " + person.ThirdName + " " + person.LastName;
            lblNationalNo.Text = person.NationalNumber;
            lblEmail.Text = person.Email;
            lblAddress.Text = person.Address;
            lblDateOfBirth.Text = person.DateOfBirth.ToString("dd/mm/yyyy");
            lblPhone.Text = person.Phone;
            lblCountry.Text = CountryBusiness.GetName(person.NationalityCountryID);
            LoadPersonImage();
        }

        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnEditPersonInfo?.Invoke();
        }

        private void LoadPersonImage()
        {
            try
            {
                string imagePath = PersonBusiness.GetImagePath(PersonID);

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        pbPersonImage.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void Refresh() => SetValues();
    }
}

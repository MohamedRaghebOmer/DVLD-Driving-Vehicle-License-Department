using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using DVLD.Business;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.WinForms.Properties;
using System.Data;
using DVLD.Core.Helpers;

namespace DVLD.WinForms
{
    public partial class ctrlAddEditPersonInfo : UserControl
    {
        public event Action<int> OnAddNew;
        public event Action<bool> OnUpdate;
        public event Action OnCancel;

        private int _personId = -1; // Default value indicate to add new person mode 
        private Person person = null;

        private enum SaveMode { AddNew, Update };
        private SaveMode _saveMode = SaveMode.AddNew;

        private string _selectedImageTempPath = null;

        // new flag to explicitly mark that user removed the image
        private bool _imageRemoved = false;

        public ctrlAddEditPersonInfo()
        {
            InitializeComponent();

            // Dispose image if ctrlAddEditPersonInfo is disposed
            this.Disposed += (s, e) =>
            {
                try { pbImage.Image?.Dispose(); }
                catch { /* ignore */ }
            };
        }

        private void ctrlAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            SetDefaults();
        }

        public int PersonID
        {
            get => _personId;
            set
            {
                if (_personId == value)
                    return;

                _personId = value;

                if (_personId > 0)
                {
                    LoadPersonInfo();
                    _saveMode = SaveMode.Update;
                }
                else
                {
                    _saveMode = SaveMode.AddNew;
                }
            }
        }

        private void SetDefaults()
        {
            SetDateOfBirth_dtp();
            LoadCountries_comboBox();

            if (person == null)
                person = new Person();

            // default image state
            _selectedImageTempPath = null;
            _imageRemoved = false;
        }

        private void SetDateOfBirth_dtp()
        {
            dtpDateOfBirth.Value = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
        }

        private void LoadCountries_comboBox()
        {
            DataTable dtCountries = CountryService.GetAllNames();
            if (dtCountries != null && dtCountries.Rows.Count > 0)
            {
                cbCountry.DataSource = dtCountries;
                cbCountry.DisplayMember = "CountryName";
                cbCountry.ValueMember = "CountryID";

                // find Egypt row, set SelectedValue if exists
                DataRow[] found = dtCountries.Select("CountryName = 'Egypt'");
                if (found.Length > 0)
                    cbCountry.SelectedValue = Convert.ToInt32(found[0]["CountryID"]);
            }
        }

        private void LoadPersonInfo()
        {
            person = PersonService.GetById(PersonID);

            if (person == null)
            {
                MessageBox.Show("Person does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtFirstName.Text = person.FirstName;
            txtSecondName.Text = person.SecondName;
            txtThirdName.Text = person.ThirdName;
            txtLastName.Text = person.LastName;
            txtNationalNumber.Text = person.NationalNumber;
            dtpDateOfBirth.Value = person.DateOfBirth;
            rbMale.Checked = person.Gender == Gender.Male;
            rbFemale.Checked = person.Gender == Gender.Female;
            txtPhone.Text = person.Phone;
            txtEmail.Text = person.Email;

            if (cbCountry.DataSource != null)
                cbCountry.SelectedValue = person.NationalityCountryID;

            txtAddress.Text = person.Address;

            // reset flags
            _selectedImageTempPath = null;
            _imageRemoved = false;

            string fullImagePath = PersonService.GetImagePath(person.ImagePath);

            if (!string.IsNullOrEmpty(fullImagePath) && File.Exists(fullImagePath))
            {
                try
                {
                    using (Image img = Image.FromFile(fullImagePath))
                    {
                        Image newImg = new Bitmap(img);
                        Image old = pbImage.Image;
                        pbImage.Image = newImg;
                        old?.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot load image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ShowRemoveLabel();
            }
            else
            {
                // show default image according to gender when no file exists
                pbImage.Image?.Dispose();
                pbImage.Image = rbMale.Checked ? Resources.ManWithQuestionMark72 : Resources.WomanWithQuestionMark72;
                HideLabelRemove();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancel?.Invoke();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateRequiredFields())
                return;

            SaveFieldsInPersonObject();

            string oldImageFileName = person?.ImagePath;
            string newImageFileName = null;

            bool isUpdate = IsUpdateMode();

            // imageRemoved already tracked by user actions (_imageRemoved)
            bool imageRemoved = _imageRemoved;

            #region Handle New Image

            if (!string.IsNullOrEmpty(_selectedImageTempPath) &&
                File.Exists(_selectedImageTempPath))
            {
                newImageFileName = SaveImage();

                if (string.IsNullOrEmpty(newImageFileName))
                {
                    MessageBox.Show("Failed to save selected image.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

                person.ImagePath = newImageFileName;

                // if user chose a new image, clear removed flag
                imageRemoved = false;
            }
            else if (imageRemoved)
            {
                // user removed the image and didn't choose a new one
                person.ImagePath = null;
            }

            #endregion

            try
            {
                bool success;
                int newId = -1;

                #region Save To Database

                if (isUpdate)
                {
                    success = PersonService.Update(person);
                }
                else
                {
                    newId = PersonService.Add(person);
                    success = newId > 0;

                    if (success)
                        _personId = newId;
                }

                #endregion

                if (!success)
                {
                    MessageBox.Show("Save operation failed.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    CleanupNewImageIfFailed(newImageFileName);
                    return;
                }

                DeleteOldImageIfNeeded(oldImageFileName, newImageFileName, imageRemoved);

                // Fire Events
                if (isUpdate)
                    OnUpdate?.Invoke(true);
                else
                    OnAddNew?.Invoke(_personId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                // reset temp state
                _selectedImageTempPath = null;
                _imageRemoved = false;
            }
        }

        private void CleanupNewImageIfFailed(string newImageFileName)
        {
            if (string.IsNullOrEmpty(newImageFileName))
                return;

            try
            {
                string fullPath = PersonService.GetImagePath(newImageFileName);
                if (File.Exists(fullPath))
                    File.Delete(fullPath);
            }
            catch { }
        }

        private void DeleteOldImageIfNeeded(string oldImageFileName,
                                    string newImageFileName,
                                    bool imageRemoved)
        {
            if (string.IsNullOrEmpty(oldImageFileName))
                return;

            bool shouldDeleteOld =
                (!string.IsNullOrEmpty(newImageFileName) &&
                 oldImageFileName != newImageFileName)
                ||
                (imageRemoved && string.IsNullOrEmpty(newImageFileName));

            if (!shouldDeleteOld)
                return;

            try
            {
                string fullOldPath = PersonService.GetImagePath(oldImageFileName);

                if (!string.IsNullOrEmpty(fullOldPath) &&
                    File.Exists(fullOldPath))
                {
                    File.Delete(fullOldPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot delete old image. " + ex.Message,
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void txtNationalNumber_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNumber.Text))
                return;

            if (person != null && txtNationalNumber.Text == person.NationalNumber)
                return;

            if (PersonService.IsNationalNoUsed(txtNationalNumber.Text, PersonID))
            {
                errorProvider1.SetError(txtNationalNumber, "National number already exists.");
                txtNationalNumber.Focus();

            }
            else
            {
                errorProvider1.SetError(txtNationalNumber, "");
            }
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhone.Text))
                return;

            if (person != null && txtPhone.Text == person.Phone)
                return;

            if (PersonService.IsPhoneUsed(txtPhone.Text, PersonID))
            {
                errorProvider1.SetError(txtPhone, "Phone number already exists.");
                txtPhone.Focus();
            }
            else
            {
                errorProvider1.SetError(txtPhone, "");
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
                return;

            if (person != null && txtEmail.Text == person.Email)
                return;

            try
            {
                var addr = new System.Net.Mail.MailAddress(txtEmail.Text);
                if (addr.Address != txtEmail.Text)
                {
                    errorProvider1.SetError(txtEmail, "Invalid email format.");
                    txtEmail.Focus();
                    return;
                }
            }
            catch
            {
                errorProvider1.SetError(txtEmail, "Invalid email format.");
                txtEmail.Focus();
                return;
            }

            if (PersonService.IsEmailUsed(txtEmail.Text, PersonID))
            {
                errorProvider1.SetError(txtEmail, "Email already exists.");
                txtEmail.Focus();
                return;
            }

            errorProvider1.SetError(txtEmail, "");
        }

        private bool ValidateRequiredFields()
        {
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                MessageBox.Show("First name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(txtSecondName.Text))
            {
                MessageBox.Show("Second name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Last name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(txtNationalNumber.Text))
            {
                MessageBox.Show("National number is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Phone number is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Address is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Select Image",
            })
            {
                if (fileDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (Image img = Image.FromFile(fileDialog.FileName))
                    {
                        Image newImg = new Bitmap(img);

                        // replace image in picturebox then dispose old one only if it's not a shared resource
                        Image old = pbImage.Image;
                        pbImage.Image = newImg;

                        // avoid disposing shared resource images from Resources
                        if (old != null && old != Resources.ManWithQuestionMark72 && old != Resources.WomanWithQuestionMark72)
                            old.Dispose();
                    }

                    // user selected a new image -> clear removed flag and set temp path
                    _selectedImageTempPath = fileDialog.FileName;
                    _imageRemoved = false;
                    ShowRemoveLabel();
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is OutOfMemoryException)
                {
                    MessageBox.Show("Cannot load image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowRemoveLabel()
        {
            // Move lblSetImage to left to free up a space to lblRemove
            Point point = new Point(897, 293);
            lblSetImage.Location = point;
            lblSetImage.Text = "Change";

            Point lblRemoveLocation = new Point(980, 293);
            lblRemove.Location = lblRemoveLocation;

            lblRemove.Visible = true;
        }

        private void lblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var old = pbImage.Image;
            if (old != Resources.ManWithQuestionMark72 && old != Resources.WomanWithQuestionMark72)
                old?.Dispose();

            // set default image based on current gender
            pbImage.Image = rbMale.Checked ? Resources.ManWithQuestionMark72 : Resources.WomanWithQuestionMark72;

            // mark image as removed and clear temp path
            _selectedImageTempPath = null;
            _imageRemoved = true;
            HideLabelRemove();
        }

        private void HideLabelRemove()
        {
            lblRemove.Visible = false;

            Point point = new Point(931, 293);
            lblSetImage.Location = point;
            lblSetImage.Text = "Set Image";
        }

        private string SaveImage()
        {
            if (string.IsNullOrEmpty(_selectedImageTempPath) || !File.Exists(_selectedImageTempPath))
                return null;

            string imagesFolderPath = PathHelper.ImagesFolderPath;

            if (!Directory.Exists(imagesFolderPath))
                Directory.CreateDirectory(imagesFolderPath);

            string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(_selectedImageTempPath);
            string destinationPath = Path.Combine(imagesFolderPath, newFileName);

            try
            {
                File.Copy(_selectedImageTempPath, destinationPath, true);
                _selectedImageTempPath = null;
                return newFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot save image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked)
                pbImage.Image = Resources.ManWithQuestionMark72;
            else
                pbImage.Image = Resources.WomanWithQuestionMark72;
        }

        // SaveImage returns the file name (guid + ext) stored under %LocalAppData%\DVLD
        private void SaveFieldsInPersonObject()
        {
            if (person == null) person = new Person();

            person.FirstName = txtFirstName.Text;
            person.SecondName = txtSecondName.Text;
            person.ThirdName = txtThirdName.Text;
            person.LastName = txtLastName.Text;
            person.NationalNumber = txtNationalNumber.Text;
            person.DateOfBirth = dtpDateOfBirth.Value;
            person.Gender = rbMale.Checked ? Gender.Male : Gender.Female;
            person.Phone = txtPhone.Text;
            person.Email = txtEmail.Text;

            int countryId = 0;
            if (cbCountry.SelectedValue != null && cbCountry.SelectedValue != DBNull.Value)
                countryId = Convert.ToInt32(cbCountry.SelectedValue);
            person.NationalityCountryID = countryId;
            person.Address = txtAddress.Text;
        }

        private bool IsUpdateMode() => _saveMode == SaveMode.Update;

        public void PerformSave()
        {
            btnSave.PerformClick();
        }
    }
}
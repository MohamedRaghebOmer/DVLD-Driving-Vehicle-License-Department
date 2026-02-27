using DVLD.Business;
using DVLD.Core.DTOs.Entities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.WinForms.Users
{
    public partial class frmAddEditUserInfo : Form
    {
        enum FormMode { AddNew, Update };
        FormMode _mode = FormMode.AddNew;
        int _userId = -1;
        User user = null;

        public frmAddEditUserInfo(int userId = -1)
        {
            InitializeComponent();
            if (userId > 0)
            {
                _mode = FormMode.Update;
                _userId = userId;
                SetUpdateMode();
            }
            else
            {
                _mode = FormMode.AddNew;
                _userId = -1;
                SetAddNewMode();
            }
        }

        private void SetUpdateMode()
        {
            user = UserService.GetById(_userId);

            // Update Form UI
            formLabel.Text = "Update User";
            Point point = new Point(385, 9);
            formLabel.Location = point;
            ctrlPersonDetailsWithFilter1.PersonID = user.PersonId;
            ctrlPersonDetailsWithFilter1.GroupBoxFilterVisible = false;
            lblUserId.Text = _userId.ToString();
            txtUsername.Text = user.Username;
            txtPassword.Text = user.Password;
            txtConfirm.Text = user.Password;
            chkIsActive.Checked = user.IsActive;
        }

        private void SetAddNewMode()
        {
            formLabel.Text = "Add New User";
            ctrlPersonDetailsWithFilter1.GroupBoxFilterVisible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool IsUpdateMode() => _mode == FormMode.Update;

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (IsValidPerson())
                tabControl1.SelectedIndex++;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!IsValidPerson())
                e.Cancel = true;
        }

        private bool IsValidPerson()
        {
            if (IsUpdateMode()) return true;

            if (ctrlPersonDetailsWithFilter1.PersonID <= 0)
            {
                MessageBox.Show("Please select an existing person first.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (UserService.ExistsByPersonId(ctrlPersonDetailsWithFilter1.PersonID))
            {
                MessageBox.Show("This person is already a user.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void EnableButtonSave(object sender, EventArgs e)
        {
            btnSave.Enabled = !string.IsNullOrEmpty(txtUsername.Text) &&
            !IsAssociatedUsername() && !string.IsNullOrEmpty(txtPassword.Text)
            && txtPassword.Text == txtConfirm.Text;
        }

        private bool IsAssociatedUsername()
        {
            try
            {
                return UserService.ExistsByUsername(txtUsername.Text, _userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            return false;
        }

        private void TextBoxes_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string errorProviderMessage = string.Empty;
            bool validationSuccess = true;

            if (txt == txtUsername && !string.IsNullOrWhiteSpace(txt.Text))
            {
                if (IsAssociatedUsername())
                {
                    errorProviderMessage = "This username is already associated with an existing user.";
                    validationSuccess = false;
                }
                else if (txt.Text.Length > 20)
                {
                    errorProviderMessage = "Username length cannot exceeds 20 characters.";
                    validationSuccess = false;
                }
            }
            else if (txt == txtPassword && txt.Text.Length < 4 || txt.Text.Length > 20)
            {
                errorProviderMessage = "Password length must be between 4 and 20 characters.";
                validationSuccess = false;
            }
            else if (txt == txtConfirm && txt.Text != txtPassword.Text)
            {
                errorProviderMessage = "Password confirmation must match password.";
                validationSuccess = false;
            }

            if (!validationSuccess)
            {
                txt.Focus();
                btnSave.Enabled = false;
            }
            errorProvider1.SetError(txt, errorProviderMessage);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsUpdateMode())
                {
                    int newUserId = UserService.Add(LoadNewUserInfoFromUI());
                    if (newUserId > 0)
                    {
                        MessageBox.Show($"User added successflly with Id =" +
                            $" {newUserId}.", "Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        lblUserId.Text = newUserId.ToString();
                        this.Close();
                    }
                }
                else
                {
                    user = LoadNewUserInfoFromUI(user);
                    if (UserService.Update(user))
                    {
                        MessageBox.Show("User updated successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("User update failed.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private User LoadNewUserInfoFromUI(User user = null)
        {
            if (user == null)
                user = new User();

            user.PersonId = ctrlPersonDetailsWithFilter1.PersonID;
            user.Username = txtUsername.Text;
            user.Password = txtPassword.Text;
            user.IsActive = chkIsActive.Checked;

            return user;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConfirm.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
                txtConfirm.PasswordChar = '*';
            }
        }
    }
}

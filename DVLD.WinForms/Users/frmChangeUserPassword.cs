using DVLD.Business;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD.WinForms.Users
{
    public partial class frmChangeUserPassword : Form
    {
        public frmChangeUserPassword(int userId)
        {
            InitializeComponent();
            ctrlUserInfo1.UserId = userId;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCurrentPassword.Text == UserService.GetPassword(
                    ctrlUserInfo1.UserId))
            {
                if (UserService.ChangePassword(ctrlUserInfo1.UserId, txtNewPassword.Text))
                {
                    MessageBox.Show("Password changed successflly.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("User does not exist.", "Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Current password is wrong.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtConfirmPassword.Text) &&
                txtConfirmPassword.Text != txtNewPassword.Text)
            {
                errorProvider1.SetError(txtConfirmPassword, "Confirmation password does not match password");
                txtConfirmPassword.Focus();
                btnSave.Enabled = false;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, string.Empty);
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCurrentPassword.Text) &&
                (txtNewPassword.Text.Length < 4 || txtNewPassword.Text.Length > 20))
            {
                errorProvider1.SetError(txtNewPassword, "Password Length must be between 4 and 20 characters.");
                txtNewPassword.Focus();
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, string.Empty);
            }
        }

        private bool CanEnableButtonSave()
        {
            return !string.IsNullOrWhiteSpace(txtCurrentPassword.Text) &&
                !string.IsNullOrWhiteSpace(txtNewPassword.Text) &&
                !string.IsNullOrWhiteSpace(txtConfirmPassword.Text) &&
                (txtNewPassword.Text == txtConfirmPassword.Text);
        }

        private void Textboxes_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = CanEnableButtonSave();
        }
    }
}

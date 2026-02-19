using System;
using System.Windows.Forms;
using System.ComponentModel;
using DVLD.Business;
using DVLD.Core.Exceptions;

namespace DVLD.WinForms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Username is required.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Password is required.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try
            {
                if (UserBusiness.Login(txtUsername.Text, txtPassword.Text))
                {
                    this.Hide();
                    frmMain frm = new frmMain();
                    frm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password.", 
                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message, "Deactivated Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem while trying to login you in. Please try again later.", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Escape key
        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.AutoValidate = AutoValidate.Disable;
                this.Close();
            }
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUsername, "Username is required.");
            }
            else
                errorProvider1.SetError(txtUsername, "");
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password is required.");
            }
            else
                errorProvider1.SetError(txtPassword, "");
        }

        // Make the form able to close even the textboxes are empty
        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.AutoValidate = AutoValidate.Disable;
            e.Cancel = false;
        }
    }

}

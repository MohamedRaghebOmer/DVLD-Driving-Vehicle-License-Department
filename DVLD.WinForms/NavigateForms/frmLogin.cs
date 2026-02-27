using System;
using System.Windows.Forms;
using DVLD.Business;
using DVLD.Core.Exceptions;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DVLD.Core.Helpers;

namespace DVLD.WinForms
{
    public partial class frmLogin : Form
    {
        string filePath = Path.Combine(PathHelper.LoggingFolderPath, "remember.txt");

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            RememberUser();
        }

        private void RememberUser()
        {
            if (AreThereRememberedCardinalities())
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length == 2)
                {
                    txtUsername.Text = lines[0];

                    byte[] encryptedPassword = Convert.FromBase64String(lines[1]);
                    byte[] decryptedPassword = ProtectedData.Unprotect(encryptedPassword, null, DataProtectionScope.CurrentUser);

                    txtPassword.Text = Encoding.UTF8.GetString(decryptedPassword);

                    chkRememberMe.Checked = true;
                }
            }
            else
                chkRememberMe.Checked = false;
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

            bool canLogin = false;

            try
            {
                canLogin = UserService.Login(txtUsername.Text, txtPassword.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (canLogin)
            {
                SaveRememberCardinalities();

                this.Hide();
                frmMain frm = new frmMain();
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect username or password.",
                            "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.AutoValidate = AutoValidate.Disable;
                this.Close();
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.AutoValidate = AutoValidate.Disable;
            e.Cancel = false;
        }

        private void SaveRememberCardinalities()
        {
            // If chkRemember is checked, and there no remembered cardinalities => then save cardinalities.
            if (chkRememberMe.Checked && !AreThereRememberedCardinalities(true))
            {
                if (!Directory.Exists(PathHelper.LoggingFolderPath))
                    Directory.CreateDirectory(PathHelper.LoggingFolderPath);

                string username = txtUsername.Text;
                string password = txtPassword.Text;

                // Encrypt Password
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] encryptedPassword = ProtectedData.Protect(passwordBytes, null, DataProtectionScope.CurrentUser);

                // Save to file
                File.WriteAllLines(filePath, new string[] {username, Convert.ToBase64String(encryptedPassword)});
            }
            else if (!chkRememberMe.Checked && AreThereRememberedCardinalities())
            {
                File.Delete(filePath);
            }
        }

        private bool AreThereRememberedCardinalities(bool checkExistingPassword = false)
        {
            bool fileExistsAndContainText = File.Exists(filePath) && !string.IsNullOrEmpty(File.ReadAllText(filePath));

            if (!fileExistsAndContainText)
                return false;

            if (!checkExistingPassword)
                return true;

            if (GetDecryptedPassword() == txtPassword.Text)
                return true;
            return false;
        }

        private string GetDecryptedPassword()
        {
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length == 2)
            {
                byte[] encryptedPassword = Convert.FromBase64String(lines[1]);
                byte[] decryptedPassword = ProtectedData.Unprotect(encryptedPassword, null, DataProtectionScope.CurrentUser);

                return Encoding.UTF8.GetString(decryptedPassword);
            }
            return null;
        }
    }
}

using System;
using System.Windows.Forms;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Win32;
using DVLD.Business;

namespace DVLD.WinForms
{
    public partial class frmLogin : Form
    {
        private readonly string rememberMeKeyPath = @"SOFTWARE\DVLD\RememberMeLoginCardinalities";

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
            string username;
            string password;

            if (TryGetRememberedCardinalities(out username, out password))
            {
                txtUsername.Text = username;
                txtPassword.Text = password;
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password is required.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (UserService.Login(txtUsername.Text, txtPassword.Text))
                {
                    SaveRememberCardinalities();

                    this.Hide();

                    frmMain frm = new frmMain();
                    frm.ShowDialog();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveRememberCardinalities()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(rememberMeKeyPath))
                {
                    if (key == null)
                        return;

                    if (chkRememberMe.Checked)
                    {
                        key.SetValue("Username", txtUsername.Text, RegistryValueKind.String);
                        key.SetValue("Password", EncryptPassword(txtPassword.Text), RegistryValueKind.String);
                    }
                    else
                    {
                        key.DeleteValue("Username", false);
                        key.DeleteValue("Password", false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registry Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string EncryptPassword(string decryptedPassword)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(decryptedPassword);
            byte[] encryptedPassword = ProtectedData.Protect(passwordBytes, null, DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encryptedPassword);
        }

        private bool TryDecryptPassword(string encryptedPassword, out string decryptedPassword)
        {
            decryptedPassword = string.Empty;

            if (string.IsNullOrWhiteSpace(encryptedPassword))
                return false;

            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);
                byte[] decryptedBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);

                decryptedPassword = Encoding.UTF8.GetString(decryptedBytes);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TryGetRememberedCardinalities(out string username, out string password)
        {
            username = string.Empty;
            password = string.Empty;

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(rememberMeKeyPath, false))
                {
                    if (key == null)
                        return false;

                    string savedUsername = key.GetValue("Username") as string;
                    string savedPassword = key.GetValue("Password") as string;

                    if (string.IsNullOrWhiteSpace(savedUsername) || string.IsNullOrWhiteSpace(savedPassword))
                        return false;

                    string decryptedPassword;
                    if (!TryDecryptPassword(savedPassword, out decryptedPassword))
                        return false;

                    username = savedUsername;
                    password = decryptedPassword;

                    return true;
                }
            }
            catch
            {
                return false;
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
        }
    }
}
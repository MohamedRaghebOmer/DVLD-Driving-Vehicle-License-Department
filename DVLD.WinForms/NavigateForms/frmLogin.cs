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
                canLogin = UserBusiness.Login(txtUsername.Text, txtPassword.Text);
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message, "Your Account was deactivated, please contact the system admin.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem while trying to login you in. Please try again later.", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (canLogin)
            {
                this.Hide();
                frmMain frm = new frmMain();
                frm.ShowDialog();
                this.Close();
                return;
            }
            else
                MessageBox.Show("Incorrect username or password.",
                "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        // Make the form able to close even the textboxes are empty
        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.AutoValidate = AutoValidate.Disable;
            e.Cancel = false;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }

}

using DVLD.Business;
using System;
using System.Windows.Forms;

namespace DVLD.WinForms.Licenses
{
    public partial class frmIssueDrivingLicense : Form
    {
        private int _localAppId = -1;

        public frmIssueDrivingLicense(int localDrivingLicenseApplicationId)
        {
            InitializeComponent();
            this._localAppId = localDrivingLicenseApplicationId;
            ctrlApplicationInfo1.LocalApplicationId = localDrivingLicenseApplicationId;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure you want to issue a license by" +
                            $" LocalDrivingLicenseApplicationId = {_localAppId}?",
                    "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                try
                {
                    int newLicenseId = LicenseService.Issue(_localAppId, txtNotes.Text);

                    if (newLicenseId > 0)
                    {
                        MessageBox.Show($"License has been issued successfully with id = {newLicenseId}!",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnIssue.Enabled = false;
                        ctrlApplicationInfo1.Refresh();
                    }
                    else
                    {
                        MessageBox.Show($"An error occurred while trying to issue the license; Please try again later.",
                            "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

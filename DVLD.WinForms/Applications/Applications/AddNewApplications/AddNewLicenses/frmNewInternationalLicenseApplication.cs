using DVLD.Business;
using DVLD.Core.DTOs.Enums;
using System;
using System.Windows.Forms;
using License = DVLD.Core.DTOs.Entities.License;

namespace DVLD.WinForms.Applications.ManageApplications.AddNewApplications
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (!CanIssue())
                return;

            try
            {
                int newId = InternationalLicenseService.IssueByLocalClass3License
                    (ctrlInternationalLicenseApplication1.LocalLicense.LicenseID);

                if (newId > 0)
                {
                    ctrlInternationalLicenseApplication1.Refresh();
                    MessageBox.Show($"International License issued successfully with Id = {newId}.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to issue International License.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CanIssue()
        {
            if (ctrlInternationalLicenseApplication1.LocalLicense == null)
                return false;

            if (MessageBox.Show("Do you want to issue an International License?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return false;

            if (ctrlInternationalLicenseApplication1.LocalLicense.LicenseClass
                != LicenseClass.Class3_OrdinaryDrivingLicense)
            {
                MessageBox.Show("Local License must be a class 3 to issue an International License.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlInternationalLicenseApplication1_OnLicenseSelected(License obj)
        {
            btnIssue.Enabled = (obj != null);
        }
    }
}

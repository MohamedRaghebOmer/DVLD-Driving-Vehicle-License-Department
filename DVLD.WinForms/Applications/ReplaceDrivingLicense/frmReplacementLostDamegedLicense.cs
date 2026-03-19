using DVLD.Business;
using DVLD.Core.DTOs.Enums;
using DVLD.WinForms.Applications.ManageApplications;
using DVLD.WinForms.Licenses;
using System;
using System.Windows.Forms;
using Application = DVLD.Core.DTOs.Entities.Application;
using License = DVLD.Core.DTOs.Entities.License;

namespace DVLD.WinForms.Applications.Applications.AddNewApplications.OtherApplications
{
    public partial class frmReplacementLostDamagedLicense : Form
    {
        private int _newLicenseId = -1;

        public frmReplacementLostDamagedLicense()
        {
            InitializeComponent();
            ApplicationTypeChanged(null, null);
        }

        private void ApplicationTypeChanged(object sender, EventArgs e)
        {
            if (sender == null)
            {
                if (rbLostLicense.Checked)
                {
                    ctrlLicenseReplacementInfo1.Initialize((ApplicationType)Convert.ToInt32(rbLostLicense.Tag));
                }
                else
                {
                    ctrlLicenseReplacementInfo1.Initialize((ApplicationType)Convert.ToInt32(rbDamagedLicense.Tag));
                }
            }
            else
            {
                RadioButton radioButton = sender as RadioButton;
                ctrlLicenseReplacementInfo1.Initialize((ApplicationType)Convert.ToInt32(radioButton.Tag));
            }
        }

        private void ctrlLicenseReplacementInfo1_OnLicenseSelected(License obj)
        {
            lblShowLicenseHistory.Enabled = true;
            btnIssueReplacement.Enabled = false;

            Application application = ApplicationService.GetById(obj.ApplicationId);

            if (obj.IsActive == false)
            {
                MessageBox.Show("Selected License is not active.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (obj.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("Selected License is expired.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (DetainedLicenseService.IsDetained(obj.LicenseID))
            {
                MessageBox.Show("Selected License is detained.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                btnIssueReplacement.Enabled = true;
            }
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (ctrlLicenseReplacementInfo1.License != null)
            {
                if (MessageBox.Show($"Do you want to do a replacement for license with Id = " +
                $"{ctrlLicenseReplacementInfo1.License.LicenseID.ToString()}?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _newLicenseId = LicenseService.Replace(ctrlLicenseReplacementInfo1.License.LicenseID,
                        rbLostLicense.Checked);

                    if (_newLicenseId > 0)
                    {
                        MessageBox.Show($"Renewal done successfully. New License Id = {_newLicenseId}.",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ctrlLicenseReplacementInfo1.Initialize(_newLicenseId);
                        lblShowNewLicenseInfo.Enabled = true;
                        btnIssueReplacement.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show($"Error replacing the license.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show($"Select a License first.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void lblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_newLicenseId > 0)
            {
                var frm = new frmLicenseInfo(_newLicenseId, frmLicenseInfo.LoadType.UsingLicenseId);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Renewal operation is not done yet", "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlLicenseReplacementInfo1.License != null)
            {
                var frm = new frmLicensesHistory(ctrlLicenseReplacementInfo1.License.DriverId,
                    frmLicensesHistory.LoadType.UsingDriverId);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show($"Select a License first.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
    }
}

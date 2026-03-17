using DVLD.Core.Logging;
using DVLD.WinForms.Applications.Applications.AddNewApplications.OtherApplications;
using DVLD.WinForms.Applications.ManageApplications;
using DVLD.WinForms.Applications.ManageApplications.AddNewApplications;
using DVLD.WinForms.Applications.ManageApplications.ManageApplications;
using DVLD.WinForms.Applications.TestTypes;
using DVLD.WinForms.CoreForms;
using DVLD.WinForms.Licenses.Detain;
using DVLD.WinForms.NavigateForms;
using DVLD.WinForms.Users;
using System;
using System.Windows.Forms;

namespace DVLD.WinForms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnPeople_Click(object sender, EventArgs e)
        {
            frmManagePeople frm = new frmManagePeople();
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageUsers();
            frm.ShowDialog();
        }

        private void currToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUserInfo(LoggedInUserInfo.UserId);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangeUserPassword(LoggedInUserInfo.UserId);
            frm.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageApplicationTypes();
            frm.ShowDialog();
        }

        private void manageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageTestTypes();
            frm.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form frm = new frmManageApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageTestTypes();
            frm.ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmNewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageLocalLicenseApplications();
            frm.ShowDialog();
        }

        private void btnDrivers_Click(object sender, EventArgs e)
        {
            Form frm = new frmDrivers();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void internaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmManageInternationalLicenseApplications();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmRenewLicenseApplication();
            frm.ShowDialog();
        }

        private void replacemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmReplacementLostDamagedLicense();
            frm.ShowDialog();
        }

        private void manageDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmManageDetainedLicenses();
            frm.ShowDialog();
        }

        private void detainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void realToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmManageLocalLicenseApplications();
            frm.ShowDialog();
        }
    }
}

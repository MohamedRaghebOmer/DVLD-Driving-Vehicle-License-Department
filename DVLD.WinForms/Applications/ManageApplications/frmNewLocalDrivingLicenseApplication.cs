using DVLD.Business;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using System;
using System.Windows.Forms;

namespace DVLD.WinForms.NavigateForms
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        public frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            LoadFormInfo();
        }

        private void LoadFormInfo()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            LoadLicenseClasses();
            lblApplicationFees.Text = ApplicationTypeService.GetFees(ApplicationType.NewLocalDrivingLicenseService).ToString();
            lblCreatedBy.Text = LoggedInUserInfo.Username;
        }

        private void LoadLicenseClasses()
        {
            cbLicenseClass.DataSource = LicenseClassService.GetAll();
            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.ValueMember = "LicenseClassID";
            cbLicenseClass.SelectedIndex = 2;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlPersonDetailsWithFilter1.PersonID <= 0)
            {
                MessageBox.Show("Please select a person", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                tabControl1.SelectedIndex = 1;
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (ctrlPersonDetailsWithFilter1.PersonID <= 0)
            {
                MessageBox.Show("Please select a person", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ctrlPersonDetailsWithFilter1.PersonID <= 0)
            {
                MessageBox.Show("Please select a person", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    Core.DTOs.Entities.Application application =
                        new Core.DTOs.Entities.Application
                        {
                            ApplicantPersonID = ctrlPersonDetailsWithFilter1.PersonID,
                            ApplicationTypeID =
                            ApplicationType.NewLocalDrivingLicenseService
                        };


                    int newApplicationID =
                        ApplicationService.Add(application, (LicenseClass)cbLicenseClass.SelectedValue);

                    if (newApplicationID > 0)
                    {
                        lblApplicationId.Text = newApplicationID.ToString();
                        MessageBox.Show("Application added successfully " +
                            "with Application ID: " + newApplicationID + ".",
                            "Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        btnSave.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}
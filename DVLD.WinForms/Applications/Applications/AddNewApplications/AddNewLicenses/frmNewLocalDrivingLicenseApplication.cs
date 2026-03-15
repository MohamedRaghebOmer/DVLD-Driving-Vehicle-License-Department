using DVLD.Business;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.WinForms.UserControls;
using System;
using System.Windows.Forms;

namespace DVLD.WinForms.NavigateForms
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        public event Action<int> OnApplicationAdded;

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
            if (ctrlPersonDetailsWithFilter1.PersonID <= 0
                && string.IsNullOrEmpty(ctrlPersonDetailsWithFilter1.NationalNo))
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
            if (ctrlPersonDetailsWithFilter1.PersonID <= 0
                && string.IsNullOrEmpty(ctrlPersonDetailsWithFilter1.NationalNo))
            {
                MessageBox.Show("Please select a person", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((IsUsingPersonId() && ctrlPersonDetailsWithFilter1.PersonID <= 0) ||
                (IsUsingNationalNo() && string.IsNullOrEmpty(ctrlPersonDetailsWithFilter1.NationalNo)))
            {
                MessageBox.Show("Please select a person", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int personId = ctrlPersonDetailsWithFilter1.PersonID;

                if (IsUsingNationalNo())
                    personId = PersonService.GetIdByNationalNo(ctrlPersonDetailsWithFilter1.NationalNo);

                if (MessageBox.Show($"Are you want to create a new local driving license " +
                    $"application for person with id = {personId}?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                try
                {
                    Core.DTOs.Entities.Application application =
                        new Core.DTOs.Entities.Application
                        {
                            ApplicantPersonID = personId,
                            ApplicationTypeID =
                            ApplicationType.NewLocalDrivingLicenseService
                        };


                    int newApplicationID =
                        ApplicationService.Add(application, (LicenseClass)cbLicenseClass.SelectedValue);

                    if (newApplicationID > 0)
                    {
                        lblApplicationId.Text = newApplicationID.ToString();
                        btnSave.Enabled = false;
                        OnApplicationAdded?.Invoke(newApplicationID);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private bool IsUsingNationalNo()
        {
            return ctrlPersonDetailsWithFilter1.LoadType ==
                ctrlPersonDetails.LoadType.UsingNationalNo;
        }

        private bool IsUsingPersonId()
        {
            return ctrlPersonDetailsWithFilter1.LoadType ==
                ctrlPersonDetails.LoadType.UsingPersonId;
        }
    }
}
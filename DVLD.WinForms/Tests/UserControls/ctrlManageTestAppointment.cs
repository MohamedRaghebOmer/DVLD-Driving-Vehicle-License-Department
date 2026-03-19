using DVLD.Business;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using System;
using System.Drawing;
using System.Windows.Forms;
using Application = DVLD.Core.DTOs.Entities.Application;

namespace DVLD.WinForms.UserControls
{
    public partial class ctrlManageTestAppointment : UserControl
    {
        public event Action OnUpdate;
        public event Action<int> OnAppointmentAdded;

        public enum Mode { Add, Edit, View };

        private TestAppointment _testAppointment;
        private LocalDrivingLicenseApplication _localApplication;
        TestTypeInfo _testTypeInfo;
        private Person _person;
        private int _failedTests;
        private Mode _mode;

        public ctrlManageTestAppointment()
        {
            InitializeComponent();
        }

        // id = TestAppointmentID when mode is Edit or View
        // id = LocalDrivingLicenseApplicationID when mode is Add
        public void Initialize(int id, Mode mode)
        {
            if (mode == Mode.Add) // id = LocalDrivingLicenseApplicationID
            {
                _testAppointment = null; // Will be initialized when saving
                _localApplication = LocalDrivingLicenseApplicationService.GetById(id);
                _testTypeInfo = GetNextTestTypeInfo();
                _person = PersonService.GetByApplicationId(_localApplication.ApplicationID);
                _failedTests = TestService.GetNumOfFailedTestsByLocalAppId(_localApplication.LocalDrivingLicenseApplicationID, _testTypeInfo.TestTypeId);
            }
            else // id = TestAppointmentID
            {
                _testAppointment = TestAppointmentService.GetById(id);
                _localApplication = LocalDrivingLicenseApplicationService.GetById(_testAppointment.LocalDrivingLicenseApplicationId);
                _person = PersonService.GetByApplicationId(_localApplication.ApplicationID);
                _failedTests = TestService.GetNumOfFailedTestsByLocalAppId(_localApplication.LocalDrivingLicenseApplicationID, _testAppointment.TestTypeId);
            }

            _mode = mode;
            LoadControlInfo();
        }

        private TestTypeInfo GetNextTestTypeInfo()
        {
            TestTypeInfo info = new TestTypeInfo();

            int passedTests = TestService.GetNumOfPassedTests(_localApplication.LocalDrivingLicenseApplicationID);

            switch (passedTests)
            {
                case 0:
                    info.TestTypeId = TestType.VisionTest;
                    info.Fees = TestTypeService.GetFees(TestType.VisionTest);
                    return info;

                case 1:
                    info.TestTypeId = TestType.WrittenTheoryTest;
                    info.Fees = TestTypeService.GetFees(TestType.WrittenTheoryTest);
                    return info;

                case 2:
                    info.TestTypeId = TestType.PracticalStreetTest;
                    info.Fees = TestTypeService.GetFees(TestType.PracticalStreetTest);
                    return info;

                default: // Unreachable by default
                    MessageBox.Show("Applicant has already passed all 3 tests. Cannot add new test.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            return null;
        }

        public void LoadControlInfo()
        {
            SetMainLabels();
            SetFeesLabels();
            InitializeFormByTestType();
        }

        private void SetMainLabels()
        {
            lblLocalAppId.Text = _localApplication.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = LicenseClassService.GetLicenseClassName(_localApplication.LicenseClassID);
            lblApplicantName.Text = _person.FirstName + " " + _person.SecondName + " " + _person.ThirdName + " " + _person.LastName;
            lblTrial.Text = _failedTests.ToString();

            switch (_mode)
            {
                case Mode.Add:
                    dtpAppointmentDate.Value = DateTime.Now;
                    break;

                case Mode.Edit:
                    dtpAppointmentDate.Value = _testAppointment.AppointmentDate;
                    break;

                case Mode.View: // Set UI for View mode
                    dtpAppointmentDate.Visible = false;
                    lblDate.Visible = true;
                    lblDate.Text = _testAppointment.AppointmentDate.ToString("dd/MM/yyyy");
                    btnSave.Visible = false;
                    gbFormGroupbox.Size = new Size(865, 479);
                    this.Size = new Size(871, 492);
                    break;
            }

            if (IsRetakeApp())
            {
                lblFormLabel.Text = "Schedule Retake Test";
            }
        }

        private void SetFeesLabels()
        {
            switch (_mode)
            {
                case Mode.Add:
                    lblFees.Text = _testTypeInfo.Fees.ToString("C");
                    lblTotalFees.Text = _testTypeInfo.Fees.ToString("C");

                    if (IsRetakeApp())
                    {
                        decimal retakeAppFees = ApplicationTypeService.GetFees(ApplicationType.RetakeTest);
                        lblRetakeAppFees.Text = retakeAppFees.ToString("C");
                        lblTotalFees.Text = (_testTypeInfo.Fees + retakeAppFees).ToString("C");
                    }
                    break;

                case Mode.Edit:
                case Mode.View:
                    decimal fees = TestTypeService.GetFees(_testAppointment.TestTypeId);
                    lblFees.Text = fees.ToString("C");
                    lblTotalFees.Text = fees.ToString("C");

                    if (IsRetakeApp())
                    {
                        decimal retakeAppFees = ApplicationTypeService.GetFees(ApplicationType.RetakeTest);
                        lblRetakeAppFees.Text = retakeAppFees.ToString("C");
                        lblTotalFees.Text = (fees + retakeAppFees).ToString("C");
                    }

                    break;
            }
        }

        private void InitializeFormByTestType()
        {
            TestType testType;

            if (_mode == Mode.Add)
                testType = _testTypeInfo.TestTypeId;
            else
                testType = _testAppointment.TestTypeId;

            switch (testType)
            {
                case TestType.VisionTest:
                    gbFormGroupbox.Text = "Vision Test";
                    pictureBox1.Image = Properties.Resources.Eye512;
                    lblFormLabel.Text = "Schedule Vision Test";
                    break;

                case TestType.WrittenTheoryTest:
                    gbFormGroupbox.Text = "Written Theory Test";
                    pictureBox1.Image = Properties.Resources.WrittenTest512;
                    lblFormLabel.Text = "Schedule Written Theory Test";
                    lblFormLabel.Location = new Point(140, 203);
                    break;

                case TestType.PracticalStreetTest:
                    gbFormGroupbox.Text = "Practical Street Test";
                    pictureBox1.Image = Properties.Resources.Road512;
                    lblFormLabel.Text = "Schedule Practical Street Test";
                    lblFormLabel.Location = new Point(160, 203);
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (_mode)
            {
                case Mode.Add:
                    AddNewAppointment();
                    break;

                case Mode.Edit:
                    UpdateTestAppointment();
                    break;
            }
        }

        private void AddNewAppointment()
        {
            if (_mode != Mode.Add) return;

            FillTestAppointment();

            try
            {
                int newTestAppointmentId = TestAppointmentService.Add(_testAppointment);

                if (newTestAppointmentId > 0)
                {
                    btnSave.Enabled = false;
                    OnAppointmentAdded?.Invoke(newTestAppointmentId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillTestAppointment()
        {
            if (_mode != Mode.Add) return; // Fill only in Add mode

            _testAppointment = new TestAppointment();

            _testAppointment.TestTypeId = _testTypeInfo.TestTypeId;
            _testAppointment.LocalDrivingLicenseApplicationId = _localApplication.LocalDrivingLicenseApplicationID;
            _testAppointment.AppointmentDate = dtpAppointmentDate.Value;

            if (IsRetakeApp())
            {
                _testAppointment.RetakeTestApplicationID = CreateRetakeApplication();
                lblRetakeTestAppId.Text = _testAppointment.RetakeTestApplicationID.ToString();
            }
            else
            {
                _testAppointment.RetakeTestApplicationID = null;
            }
        }

        private int CreateRetakeApplication()
        {
            Application retakeApplication = new Application();

            retakeApplication.ApplicationTypeID = ApplicationType.RetakeTest;
            retakeApplication.ApplicantPersonID = _person.PersonID;

            return ApplicationService.NewRetakeTestApplication(retakeApplication);
        }

        private void UpdateTestAppointment()
        {
            if (_testAppointment.AppointmentDate.Date == dtpAppointmentDate.Value.Date)
            {
                MessageBox.Show("There is no changes to be updated.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Apply changes (Update local object before saving)
            _testAppointment.AppointmentDate = dtpAppointmentDate.Value;

            try
            {
                if (TestAppointmentService.Update(_testAppointment))
                {
                    btnSave.Enabled = false;
                    OnUpdate?.Invoke();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsRetakeApp()
        {
            return _failedTests > 0;
        }
    }
}

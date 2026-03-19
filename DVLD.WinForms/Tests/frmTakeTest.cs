using DVLD.Business;
using DVLD.Core.DTOs.Entities;
using System;
using System.Windows.Forms;

namespace DVLD.WinForms.Tests
{
    public partial class frmTakeTest : Form
    {
        public event Action<int> OnTestPassed;
        public event Action<int> OnTestFailed;
        private TestAppointment _testAppointment = new TestAppointment();

        public frmTakeTest(int appointmentId)
        {
            InitializeComponent();
            ctrlManageTestAppointment1.Initialize(appointmentId, UserControls.ctrlManageTestAppointment.Mode.View);
            this._testAppointment = TestAppointmentService.GetById(appointmentId);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AddNewTest();
        }

        private bool CanAddTest()
        {
            string result = rbPass.Checked ? "Pass" : "Fail";

            return MessageBox.Show(
                    $"Do you want to save the test result as {result}?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.No;
        }

        public Test GetFilledTestObject()
        {
            Test test = new Test();

            test.TestAppointmentID = _testAppointment.TestAppointmentId;
            test.TestResult = rbPass.Checked;
            test.Notes = txtNotes.Text;

            return test;
        }

        private void AddNewTest()
        {
            if (!CanAddTest())
                return;

            try
            {
                Test test = GetFilledTestObject();
                int testId = TestService.Add(test);

                if (testId > 0)
                {
                    MessageBox.Show("Test Saved Successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (test.TestResult == true)
                        OnTestPassed?.Invoke(testId);
                    else
                        OnTestFailed?.Invoke(testId);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

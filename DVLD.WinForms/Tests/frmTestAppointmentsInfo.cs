using DVLD.Business;
using DVLD.Core.DTOs.Enums;
using DVLD.WinForms.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Mode = DVLD.WinForms.UserControls.ctrlManageTestAppointment.Mode;

namespace DVLD.WinForms.Tests
{
    public partial class frmTestAppointmentsInfo : Form
    {
        public event Action<int> OnTestPassed;
        public event Action<int> OnAppointmentAdded;

        private int _selectedRowIndex = -1;
        private TestType _testType = TestType.VisionTest;

        public frmTestAppointmentsInfo(int localAppId, TestType testType)
        {
            InitializeComponent();
            SetPrivateFields(localAppId, testType);
            LoadTestTypeInfoInUI();
        }

        private void SetPrivateFields(int localAppId, TestType testType)
        {
            ctrlApplicationInfo1.LocalApplicationId = localAppId;
            _testType = testType;
        }

        private void LoadDataGrid()
        {
            this.dgvAppointments.DataSource =
                        TestAppointmentService.GetShortAppointmentInfo(
                            ctrlApplicationInfo1.LocalApplicationId,
                            _testType);
        }

        private void LoadTestTypeInfoInUI()
        {
            LoadDataGrid();

            switch (_testType)
            {
                case TestType.VisionTest:
                    this.Text = "Vision Test Appointments";
                    this.pictureBox1.Image = Properties.Resources.Eye512;
                    this.lblFormLabel.Text = "Vision Test Appointments";
                    break;

                case TestType.WrittenTheoryTest:
                    this.Text = "Written Theory Test Appointments";
                    this.pictureBox1.Image = Properties.Resources.WrittenTest512;
                    this.lblFormLabel.Text = "Written Theory Test Appointments";
                    lblFormLabel.Location = new Point(290, 186);
                    break;

                case TestType.PracticalStreetTest:
                    this.Text = "Practical Street Test Appointments";
                    this.pictureBox1.Image = Properties.Resources.Road512;
                    this.lblFormLabel.Text = "Practical Street Test Appointments";
                    lblFormLabel.Location = new Point(290, 186);
                    break;
            }
        }

        private void UpdateLabelCount(object sender, EventArgs e)
        {
            lblCount.Text = dgvAppointments.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            // check if there is already an opened appointment
            if (TestAppointmentService.IsThereOpenedAppointment(
                ctrlApplicationInfo1.LocalApplicationId))
            {
                MessageBox.Show("There is already an opened appointment related to this applicant.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // check if the applicant has already passed the test
            if (TestService.IsPassedByLocalAppId(ctrlApplicationInfo1.LocalApplicationId, _testType))
            {
                MessageBox.Show("The applicant has already passed the test.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var frm = new frmManageTestAppointments(ctrlApplicationInfo1.LocalApplicationId, Mode.Add);
            frm.OnAppointmentAdded += AppointmentAdded;
            frm.ShowDialog();
        }

        private void AppointmentAdded(int obj)
        {
            LoadDataGrid();
            OnAppointmentAdded?.Invoke(obj);
        }

        private void dgvAppointments_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _selectedRowIndex = dgvAppointments.HitTest(e.X, e.Y).RowIndex;
                if (_selectedRowIndex >= 0)
                {
                    dgvAppointments.ClearSelection();
                    dgvAppointments.Rows[_selectedRowIndex].Selected = true;
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (_selectedRowIndex < 0)
            {
                e.Cancel = true;
                return;
            }

            bool isLocked = IsSelectedAppointmentLocked();

            takeTestToolStripMenuItem.Enabled = !isLocked;
            deleteToolStripMenuItem.Enabled = !isLocked;

            if (isLocked)
            {
                editToolStripMenuItem.Text = "Appointment Info";
                editToolStripMenuItem.Image = Resources.Paper32;
            }
            else
            {
                editToolStripMenuItem.Text = "Edit";
                editToolStripMenuItem.Image = Resources.Pen32;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode mode = IsSelectedAppointmentLocked() ? Mode.View : Mode.Edit;

            frmManageTestAppointments frm =
                new frmManageTestAppointments(GetSelectedAppointmentId(), mode);
            frm.OnAppointmentUpdated += LoadDataGrid;
            frm.ShowDialog();
        }

        private bool IsSelectedAppointmentLocked()
        {
            return Convert.ToBoolean(dgvAppointments.Rows[_selectedRowIndex].Cells["IsLocked"].Value);
        }

        private DateTime GetSelectedAppointmentDate()
        {
            return Convert.ToDateTime(dgvAppointments.Rows[_selectedRowIndex].
                Cells["AppointmentDate"].Value);
        }

        private int GetSelectedAppointmentId()
        {
            if (_selectedRowIndex < 0 || _selectedRowIndex > dgvAppointments.Rows.Count)
                return -1;

            return Convert.ToInt32(dgvAppointments.Rows[_selectedRowIndex].Cells["TestAppointmentID"].Value);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetSelectedAppointmentDate().Date != DateTime.Today.Date)
            {
                MessageBox.Show("Today is not the test date", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmTakeTest frm = new frmTakeTest(GetSelectedAppointmentId());
            frm.OnTestPassed += frmTakeTest_OnTestPassed;
            frm.OnTestFailed += frmTakeTest_OnTestFailed;
            frm.ShowDialog();
        }

        private void frmTakeTest_OnTestPassed(int obj)
        {
            LoadDataGrid();
            OnTestPassed.Invoke(obj);
            this.Close();
        }

        private void frmTakeTest_OnTestFailed(int obj)
        {
            LoadDataGrid();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectedAppointmentLocked())
                return;

            if (MessageBox.Show("Are you sure you want to delete this appointment?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (TestAppointmentService.Delete(GetSelectedAppointmentId()))
                    {
                        LoadDataGrid();
                        MessageBox.Show("Test Appointment Deleted Successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Test Appointment does not exist.", "Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

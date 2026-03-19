using DVLD.WinForms.UserControls;
using System;
using System.Windows.Forms;

namespace DVLD.WinForms.Tests
{
    public partial class frmManageTestAppointments : Form
    {
        public event Action<int> OnAppointmentAdded;
        public event Action OnAppointmentUpdated;

        public frmManageTestAppointments(int id, ctrlManageTestAppointment.Mode mode)
        {
            InitializeComponent();
            ctrlManageTestAppointment1.Initialize(id, mode);

            ctrlManageTestAppointment1.OnAppointmentAdded += AppointmentAdded;
            ctrlManageTestAppointment1.OnUpdate += AppointmentUpdated;

            if (mode == ctrlManageTestAppointment.Mode.View)
                btnCancel.Location = new System.Drawing.Point(732, 502);
        }

        private void AppointmentAdded(int obj)
        {
            OnAppointmentAdded?.Invoke(obj);

            if (obj > 0)
            {
                MessageBox.Show($"Appointment Saved Successfully With Id = {obj}.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error adding appointment.", "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }

        private void AppointmentUpdated()
        {
            OnAppointmentUpdated?.Invoke();

            MessageBox.Show("Test Appointment Updated Successfully.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

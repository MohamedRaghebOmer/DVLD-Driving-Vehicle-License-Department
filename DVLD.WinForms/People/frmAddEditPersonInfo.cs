using System;
using System.Windows.Forms;

namespace DVLD.WinForms
{
    public partial class frmAddEditPersonInfo : Form
    {
        public event Action<int> OnAddNew;

        private enum FormMode { Add, Update };
        private FormMode _mode = FormMode.Add;
        private int _personId;

        public frmAddEditPersonInfo(int personId = -1)
        {
            InitializeComponent();

            ctrlAddEditPersonInfo1.OnCancel += ctrlAddEditPersonInfo1_OnCancel;

            if (personId > 0)
            {
                this._mode = FormMode.Update;
                this._personId = personId;
                ctrlAddEditPersonInfo1.OnUpdate += ctrlAddEditPersonInfo1_OnUpdate;
            }
            else
            {
                _mode = FormMode.Add;
                ctrlAddEditPersonInfo1.OnAddNew += ctrlAddEditPersonInfo1_OnAddNew;
            }
        }

        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            SetLabels();

            if (IsUpdateMode)
                ctrlAddEditPersonInfo1.PersonID = this._personId;
        }

        private void SetLabels()
        {
            if (IsUpdateMode)
            {
                lblFormlabel.Text = "Update Person";
                lblPersonID.Text = this._personId.ToString();
            }
            else
            {
                lblFormlabel.Text = "Add New Person";
                lblPersonID.Text = "N/A";
            }
        }

        private bool IsUpdateMode => _mode == FormMode.Update;

        private void ctrlAddEditPersonInfo1_OnAddNew(int newPersonId)
        {
            if (newPersonId > 0)
            {
                MessageBox.Show("Person has been added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnAddNew?.Invoke(newPersonId);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to add the person.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ctrlAddEditPersonInfo1_OnUpdate(bool isUpdated)
        {
            if (isUpdated)
            {
                MessageBox.Show("Person has been updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update person info.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ctrlAddEditPersonInfo1_OnCancel()
        {
            this.Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ctrlAddEditPersonInfo1.OnCancel -= ctrlAddEditPersonInfo1_OnCancel;
            ctrlAddEditPersonInfo1.OnAddNew -= ctrlAddEditPersonInfo1_OnAddNew;
            ctrlAddEditPersonInfo1.OnUpdate -= ctrlAddEditPersonInfo1_OnUpdate;

            base.OnFormClosed(e);
        }

        private void frmAddEditPersonInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ctrlAddEditPersonInfo1.PerformSave();
                e.SuppressKeyPress = true; // To prevent the form from being closed
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}

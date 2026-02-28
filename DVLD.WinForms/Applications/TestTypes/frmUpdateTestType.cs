using DVLD.Business;
using DVLD.Core.DTOs.Enums;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.WinForms.Applications.TestTypes
{
    public partial class frmUpdateTestType : Form
    {
        private TestType _testType;

        public frmUpdateTestType(TestType testType)
        {
            InitializeComponent();
            this._testType = testType;
        }

        private void frmUpdateTestType_Load(object sender, System.EventArgs e)
        {
            LoadTestTypeInfoInUI();
        }

        private void LoadTestTypeInfoInUI()
        {
            DataTable dtTestTypeInfo = TestTypeService.Get(_testType);
            if (dtTestTypeInfo == null) return;

            this.lblId.Text = ((int)_testType).ToString();
            txtTitle.Text = dtTestTypeInfo.Rows[0]["TestTypeTitle"].ToString();
            txtDescription.Text = dtTestTypeInfo.Rows[0]["TestTypeDescription"].ToString();
            txtFees.Text = dtTestTypeInfo.Rows[0]["TestTypeFees"].ToString();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Test Type Title must be at least 1 character.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtFees.Text, out decimal fees) && fees < 0)
            {
                MessageBox.Show("Fees must be a positive integer.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (TestTypeService.Update(_testType,
                txtTitle.Text, txtDescription.Text,
                Convert.ToDecimal(txtFees.Text)))
            {
                MessageBox.Show("Test Type Updated Successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Test Type does not exist.", "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) &&
                e.KeyChar != '.')
                e.Handled = true;
        }
    }
}

using System;
using System.Data;
using System.Windows.Forms;
using DVLD.Business;
using DVLD.Core.DTOs.Enums;

namespace DVLD.WinForms.Applications
{
    public partial class frmUpdateApplicationType : Form
    {
        private ApplicationType _applicationType;
        
        public frmUpdateApplicationType(ApplicationType applicationType)
        {
            InitializeComponent();
            this._applicationType = applicationType;
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            DataTable dt = ApplicationTypeService.GetTitleAndFees(_applicationType);

            if (dt == null)
                return;

            lblId.Text = ((int)_applicationType).ToString();
            txtTitle.Text = dt.Rows[0]["ApplicationTypeTitle"].ToString();
            txtFees.Text = dt.Rows[0]["ApplicationFees"].ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show(
                    "Application Type Title must be at least 1 character.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtFees.Text, out decimal fees) && fees >= 0)
            {
                MessageBox.Show(
                "Fees must be a positive integer.",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                if (ApplicationTypeService.Update(_applicationType,
                txtTitle.Text.ToString(), Convert.ToDecimal(txtFees.Text)))
                {
                    MessageBox.Show("Application Type updated successfully.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else // Inaccessible by default
                {
                    MessageBox.Show("Failed to update.",
                    "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

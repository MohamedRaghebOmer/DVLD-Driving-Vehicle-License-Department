using DVLD.Business;
using DVLD.Core.DTOs.Enums;
using DVLD.WinForms.Applications;
using System;
using System.Windows.Forms;

namespace DVLD.WinForms.NavigateForms
{
    public partial class frmManageApplicationTypes : Form
    {
        private int _selectedRowIndex = -1;

        public frmManageApplicationTypes()
        {
            InitializeComponent();
            dgvApplicationTypes.DataSource = ApplicationTypeService.GetAll();
        }

        private void UpdateLabelCount(object sender, EventArgs e)
        {
            lblCount.Text = dgvApplicationTypes.Rows.Count.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUpdateApplicationType(GetSelectedApplicationType());
            frm.ShowDialog();
            // Reload data grid view
            dgvApplicationTypes.DataSource = ApplicationTypeService.GetAll();
        }

        private void dgvApplicationTypes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dgvApplicationTypes.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    _selectedRowIndex = hit.RowIndex;
                    dgvApplicationTypes.ClearSelection();
                    dgvApplicationTypes.Rows[_selectedRowIndex].Selected = true;
                }
            }
        }

        private ApplicationType GetSelectedApplicationType()
        {
            return (ApplicationType)(Convert.ToInt32(dgvApplicationTypes.Rows[_selectedRowIndex].Cells[0].Value));
        }
    }
}

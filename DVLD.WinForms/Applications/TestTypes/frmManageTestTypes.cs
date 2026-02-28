using DVLD.Business;
using DVLD.Core.DTOs.Enums;
using System;
using System.Windows.Forms;

namespace DVLD.WinForms.Applications.TestTypes
{
    public partial class frmManageTestTypes : Form
    {
        private int _selectedRowIndex = -1;

        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            dgvTestTypes.DataSource = TestTypeService.GetAll();
        }

        private void UpdateLabelCount(object sender, EventArgs e)
        {
            lblCount.Text = dgvTestTypes.Rows.Count.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUpdateTestType(GetSelectedTestType());
            frm.ShowDialog();
            dgvTestTypes.DataSource = TestTypeService.GetAll();
        }

        private TestType GetSelectedTestType()
        {
            return (TestType)(Convert.ToInt32(dgvTestTypes.Rows[_selectedRowIndex].Cells[0].Value));
        }

        private void dgvTestTypes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dgvTestTypes.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    _selectedRowIndex = hit.RowIndex;
                    dgvTestTypes.ClearSelection();
                    dgvTestTypes.Rows[_selectedRowIndex].Selected = true;
                }
            }
        }
    }
}

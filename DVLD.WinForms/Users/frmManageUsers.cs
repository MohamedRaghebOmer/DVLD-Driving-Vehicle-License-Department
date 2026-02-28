using DVLD.Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.WinForms.Users
{
    public partial class frmManageUsers : Form
    {
        int selectedRowIndex = -1;

        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            LoadDataGridUsers();
            SetDefaults();
        }

        private void LoadDataGridUsers()
        {
            dgvUsers.DataSource = UserService.GetAllWithDetails();
            AdjustColumnsWidth();
        }

        private void AdjustColumnsWidth()
        {
            dgvUsers.Columns[0].Width = 100;
            dgvUsers.Columns[1].Width = 100;
            dgvUsers.Columns[2].Width = 350;
            dgvUsers.Columns[3].Width = 170;
        }

        private void SetDefaults()
        {
            cbFilter.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateLableCount(object sender, EventArgs e)
        {
            lblCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((cbFilter.SelectedIndex == 1 || cbFilter.SelectedIndex == 3) && (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Clear();

            if (cbFilter.SelectedIndex == 5) // Is Active
            {
                txtFilterValue.Visible = false;
                cbIsActiveValue.Visible = true;
                chkMatchCase.Visible = false;
                cbIsActiveValue.SelectedIndex = 0;
            }
            else if (cbFilter.SelectedIndex != 0) // Except None
            {
                cbIsActiveValue.Visible = false;
                txtFilterValue.Visible = true;
                txtFilterValue.Focus();

                chkMatchCase.Visible = cbFilter.SelectedIndex == 2 || cbFilter.SelectedIndex == 4;
            }
            else // None
            {
                cbIsActiveValue.Visible = false;
                txtFilterValue.Visible = false;
                chkMatchCase.Visible = false;
            }
        }

        private void cbIsActiveValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsActiveValue.SelectedIndex)
            {
                case 0: // All => (No Filter)
                    ((DataTable)dgvUsers.DataSource).DefaultView.RowFilter = "";
                    break;
                case 1: // IsActive = 1
                    ((DataTable)dgvUsers.DataSource).DefaultView.RowFilter = "[Is Active] = 1";
                    break;
                case 2: // IsActive = 0
                    ((DataTable)dgvUsers.DataSource).DefaultView.RowFilter = "[Is Active] = 0";
                    break;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFilterValue.Text))
            {
                switch (cbFilter.SelectedIndex)
                {
                    case 1: // UserId
                        ((DataTable)dgvUsers.DataSource).DefaultView.RowFilter =
                            string.Format("[User Id] = {0}", txtFilterValue.Text);
                        break;
                    case 2: // Username
                        ((DataTable)dgvUsers.DataSource).DefaultView.RowFilter =
                            string.Format("[Username] LIKE '{0}%'", txtFilterValue.Text);
                        break;
                    case 3: // Person Id
                        ((DataTable)dgvUsers.DataSource).DefaultView.RowFilter =
                            string.Format("[Person Id] = {0}", txtFilterValue.Text);
                        break;
                    case 4:
                        ((DataTable)dgvUsers.DataSource).DefaultView.RowFilter =
                            string.Format("[Full Name] LIKE '{0}%'", txtFilterValue.Text);
                        break;
                    default:
                        ((DataTable)dgvUsers.DataSource).DefaultView.RowFilter = string.Empty;
                        break;
                }
            }
            else
            {
                ((DataTable)dgvUsers.DataSource).DefaultView.RowFilter = string.Empty;
            }
        }

        private void chkMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvUsers.DataSource;

            if (dt != null)
                dt.CaseSensitive = chkMatchCase.Checked;
        }

        private void dgvUsers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dgvUsers.HitTest(e.X, e.Y);

                if (hit.RowIndex >= 0)
                {
                    selectedRowIndex = hit.RowIndex;
                    dgvUsers.ClearSelection();
                    dgvUsers.Rows[selectedRowIndex].Selected = true;
                }
            }
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUserInfo();
            frm.ShowDialog();
            LoadDataGridUsers();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(GetSelectedUserId());
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmAddEditUserInfo(GetSelectedUserId());
            frm.ShowDialog();
            LoadDataGridUsers();
        }

        private int GetSelectedUserId()
        {
            return int.Parse(dgvUsers.Rows[selectedRowIndex].Cells[0].Value.ToString());
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedUserId = GetSelectedUserId();
            if (MessageBox.Show(
                $"Are you sure you want to delete user with id = {selectedUserId}?",
                "Warring", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                == DialogResult.OK)
            {
                try
                {
                    if (UserService.DeleteById(selectedUserId))
                    {
                        MessageBox.Show("User has been deleted successflly.",
                            "Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        LoadDataGridUsers();
                    }
                    else // Unreachable by default
                    {
                        MessageBox.Show("User doesn not exists.",
                            "Failed", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                            "Failed", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                }
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangeUserPassword(GetSelectedUserId());
            frm.ShowDialog();
        }

        private void UnImplementedFeatures_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature will be available soon.", "Sorry",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

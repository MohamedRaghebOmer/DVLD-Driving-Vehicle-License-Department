using DVLD.Business;
using System;
using System.Windows.Forms;

namespace DVLD.WinForms.UserControls
{
    public partial class ctrlPersonDetailsWithFilter : UserControl
    {
        public ctrlPersonDetailsWithFilter()
        {
            InitializeComponent();
            SetDefaults();
        }

        public int PersonID
        {
            get { return ctrlPersonDetails1.PersonID; }
            set { ctrlPersonDetails1.PersonID = value; }
        }

        public bool GroupBoxFilterVisible
        {
            get { return gbFilter.Visible; }
            set { gbFilter.Visible = value; }
        }

        private void SetDefaults()
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0 && (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0 && // Person Id
                !string.IsNullOrEmpty(txtFilterValue.Text) &&
                int.TryParse(txtFilterValue.Text, out int id))
            {
                if (!PersonService.Exists(id))
                    id = -1;

                ctrlPersonDetails1.PersonID = id;
            }
            else if (!string.IsNullOrEmpty(txtFilterValue.Text)) // National No
            {
                ctrlPersonDetails1.NationalNo = txtFilterValue.Text;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Clear();
            txtFilterValue.Focus();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.OnAddNew += (id) =>
            {
                ctrlPersonDetails1.PersonID = id;
                txtFilterValue.Text = id.ToString();
            }; frm.ShowDialog();
        }
    }
}

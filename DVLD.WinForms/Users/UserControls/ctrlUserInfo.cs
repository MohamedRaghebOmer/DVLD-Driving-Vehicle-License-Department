using DVLD.Business;
using DVLD.Core.DTOs.Entities;
using System.Windows.Forms;

namespace DVLD.WinForms.Users
{
    public partial class ctrlUserInfo : UserControl
    {
        private int _userId = -1;
        public ctrlUserInfo()
        {
            InitializeComponent();
        }

        public int UserId
        {
            get => _userId;

            set
            {
                if (_userId == value)
                    return;

                _userId = value;
                SetDefaults();

                if (_userId >= 1)
                    LoadControl();
            }
        }

        private void SetDefaults()
        {
            ctrlPersonDetails1.PersonID = -1;
            lblUserId.Text = "???";
            lblUsername.Text = "???";
            lblIsActive.Text = "???";
        }

        private void LoadControl()
        {
            if (UserId < 1) return;

            User user = UserService.GetById(UserId);
            if (user == null) return;

            // Set user info
            lblUserId.Text = user.UserId.ToString();
            lblUsername.Text = user.Username;
            lblIsActive.Text = (user.IsActive) ? "Yes" : "No";

            // Set person info
            ctrlPersonDetails1.PersonID = user.PersonId;
        }

        private void lblEditLoginInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (UserId <= 0)
                return;

            Form frm = new frmAddEditUserInfo(UserId);
            frm.ShowDialog();
            LoadControl();
        }
    }
}

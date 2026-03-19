using System.Windows.Forms;

namespace DVLD.WinForms.Users
{
    public partial class frmUserInfo : Form
    {
        public frmUserInfo(int userId)
        {
            InitializeComponent();
            ctrlUserInfo1.UserId = userId;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.WinForms.People
{
    public partial class frmPersonDetails : Form
    {
        
        public frmPersonDetails(int personID)
        {
            InitializeComponent();
            ctrlPersonDetails1.OnEditPersonInfo += EditPersonInfo;
            ctrlPersonDetails1.PersonID = (personID);
        }

        private void EditPersonInfo()
        {
            Form frm = new frmAddEditPersonInfo(ctrlPersonDetails1.PersonID);
            frm.ShowDialog();
            ctrlPersonDetails1.Refresh();
        }
    }
}

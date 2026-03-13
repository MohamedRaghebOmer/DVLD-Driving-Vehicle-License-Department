using System;
using System.Windows.Forms;

namespace DVLD.WinForms.Applications.ManageApplications
{
    public partial class frmApplicationInfo : Form
    {
        public enum LoadType { UsingApplicationId, UsingLocalApplicationId };

        public frmApplicationInfo(int Id, LoadType loadType)
        {
            InitializeComponent();

            if (Id > 0)
            {
                if (loadType == LoadType.UsingApplicationId)
                {
                    ctrlApplicationInfo1.ApplicationId = Id;
                }
                else
                {
                    ctrlApplicationInfo1.LocalApplicationId = Id;
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Windows.Forms;

namespace DVLD.WinForms.Applications.ManageApplications
{
    public partial class frmApplicationInfo : Form
    {
        private int _applicationId = -1, _localApplicationId = -1;

        public enum LoadType { UsingApplicationId, UsingLocalApplicationId };
        private LoadType _loadType = LoadType.UsingApplicationId;

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

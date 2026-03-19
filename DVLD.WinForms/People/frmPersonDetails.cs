using System.Windows.Forms;

namespace DVLD.WinForms.People
{
    public partial class frmPersonDetails : Form
    {

        public frmPersonDetails(int personID)
        {
            InitializeComponent();
            if (personID > 0)
                ctrlPersonDetails1.PersonID = (personID);
        }

        public frmPersonDetails(string nationalNo)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(nationalNo))
                ctrlPersonDetails1.NationalNo = nationalNo;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

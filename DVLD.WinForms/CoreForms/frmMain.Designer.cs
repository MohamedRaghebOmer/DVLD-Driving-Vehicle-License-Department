namespace DVLD.WinForms
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnApplications = new System.Windows.Forms.ToolStripDropDownButton();
            this.driToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internationalLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renewDrivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.realToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.replacemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retakeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localDrivingLicenseApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.detainedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageApplicationTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPeople = new System.Windows.Forms.ToolStripButton();
            this.btnDrivers = new System.Windows.Forms.ToolStripButton();
            this.btnUsers = new System.Windows.Forms.ToolStripButton();
            this.btnAccountSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.currToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.signOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnApplications,
            this.btnPeople,
            this.btnDrivers,
            this.btnUsers,
            this.btnAccountSettings});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1378, 80);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnApplications
            // 
            this.btnApplications.AutoSize = false;
            this.btnApplications.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.driToolStripMenuItem,
            this.manageApplicationsToolStripMenuItem,
            this.toolStripSeparator2,
            this.detainedToolStripMenuItem,
            this.manageApplicationTypesToolStripMenuItem,
            this.manageTestToolStripMenuItem});
            this.btnApplications.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplications.ForeColor = System.Drawing.Color.Black;
            this.btnApplications.Image = global::DVLD.WinForms.Properties.Resources.Application64;
            this.btnApplications.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnApplications.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnApplications.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApplications.Name = "btnApplications";
            this.btnApplications.Size = new System.Drawing.Size(180, 70);
            this.btnApplications.Text = "Applications";
            // 
            // driToolStripMenuItem
            // 
            this.driToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jToolStripMenuItem,
            this.renewDrivingLicenseToolStripMenuItem,
            this.toolStripSeparator4,
            this.realToolStripMenuItem,
            this.toolStripSeparator3,
            this.replacemToolStripMenuItem,
            this.retakeTestToolStripMenuItem});
            this.driToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.Card32;
            this.driToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.driToolStripMenuItem.Name = "driToolStripMenuItem";
            this.driToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.driToolStripMenuItem.Text = "Driving Licenses Services";
            // 
            // jToolStripMenuItem
            // 
            this.jToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.internationalLicenseToolStripMenuItem});
            this.jToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.New32;
            this.jToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.jToolStripMenuItem.Name = "jToolStripMenuItem";
            this.jToolStripMenuItem.Size = new System.Drawing.Size(444, 38);
            this.jToolStripMenuItem.Text = "New Driving License";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.Store32;
            this.newToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(271, 38);
            this.newToolStripMenuItem.Text = "Local License";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // internationalLicenseToolStripMenuItem
            // 
            this.internationalLicenseToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.International32;
            this.internationalLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.internationalLicenseToolStripMenuItem.Name = "internationalLicenseToolStripMenuItem";
            this.internationalLicenseToolStripMenuItem.Size = new System.Drawing.Size(271, 38);
            this.internationalLicenseToolStripMenuItem.Text = "International License";
            this.internationalLicenseToolStripMenuItem.Click += new System.EventHandler(this.internationalLicenseToolStripMenuItem_Click);
            // 
            // renewDrivingLicenseToolStripMenuItem
            // 
            this.renewDrivingLicenseToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.Renew32;
            this.renewDrivingLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.renewDrivingLicenseToolStripMenuItem.Name = "renewDrivingLicenseToolStripMenuItem";
            this.renewDrivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(444, 38);
            this.renewDrivingLicenseToolStripMenuItem.Text = "Renew Driving License";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(441, 6);
            // 
            // realToolStripMenuItem
            // 
            this.realToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.Rocket32;
            this.realToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.realToolStripMenuItem.Name = "realToolStripMenuItem";
            this.realToolStripMenuItem.Size = new System.Drawing.Size(444, 38);
            this.realToolStripMenuItem.Text = "Release Detained Driving License";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(441, 6);
            // 
            // replacemToolStripMenuItem
            // 
            this.replacemToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.Replace32;
            this.replacemToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.replacemToolStripMenuItem.Name = "replacemToolStripMenuItem";
            this.replacemToolStripMenuItem.Size = new System.Drawing.Size(444, 38);
            this.replacemToolStripMenuItem.Text = "Replacement for Lost or Damaged License";
            // 
            // retakeTestToolStripMenuItem
            // 
            this.retakeTestToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.Retake32;
            this.retakeTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.retakeTestToolStripMenuItem.Name = "retakeTestToolStripMenuItem";
            this.retakeTestToolStripMenuItem.Size = new System.Drawing.Size(444, 38);
            this.retakeTestToolStripMenuItem.Text = "Retake Test";
            // 
            // manageApplicationsToolStripMenuItem
            // 
            this.manageApplicationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localDrivingLicenseApplicationToolStripMenuItem,
            this.internaToolStripMenuItem});
            this.manageApplicationsToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.Planning32;
            this.manageApplicationsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.manageApplicationsToolStripMenuItem.Name = "manageApplicationsToolStripMenuItem";
            this.manageApplicationsToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.manageApplicationsToolStripMenuItem.Text = "Manage Applications";
            // 
            // localDrivingLicenseApplicationToolStripMenuItem
            // 
            this.localDrivingLicenseApplicationToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.Store32;
            this.localDrivingLicenseApplicationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.localDrivingLicenseApplicationToolStripMenuItem.Name = "localDrivingLicenseApplicationToolStripMenuItem";
            this.localDrivingLicenseApplicationToolStripMenuItem.Size = new System.Drawing.Size(379, 38);
            this.localDrivingLicenseApplicationToolStripMenuItem.Text = "Local Driving License Applications";
            this.localDrivingLicenseApplicationToolStripMenuItem.Click += new System.EventHandler(this.localDrivingLicenseApplicationToolStripMenuItem_Click);
            // 
            // internaToolStripMenuItem
            // 
            this.internaToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.International32;
            this.internaToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.internaToolStripMenuItem.Name = "internaToolStripMenuItem";
            this.internaToolStripMenuItem.Size = new System.Drawing.Size(379, 38);
            this.internaToolStripMenuItem.Text = "International License Applications";
            this.internaToolStripMenuItem.Click += new System.EventHandler(this.internaToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(315, 6);
            // 
            // detainedToolStripMenuItem
            // 
            this.detainedToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.Preson32;
            this.detainedToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.detainedToolStripMenuItem.Name = "detainedToolStripMenuItem";
            this.detainedToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.detainedToolStripMenuItem.Text = "Detain Licenses";
            // 
            // manageApplicationTypesToolStripMenuItem
            // 
            this.manageApplicationTypesToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.Shapes32;
            this.manageApplicationTypesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.manageApplicationTypesToolStripMenuItem.Name = "manageApplicationTypesToolStripMenuItem";
            this.manageApplicationTypesToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.manageApplicationTypesToolStripMenuItem.Text = "Manage Application Types";
            this.manageApplicationTypesToolStripMenuItem.Click += new System.EventHandler(this.manageApplicationTypesToolStripMenuItem_Click_1);
            // 
            // manageTestToolStripMenuItem
            // 
            this.manageTestToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.Paper32;
            this.manageTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.manageTestToolStripMenuItem.Name = "manageTestToolStripMenuItem";
            this.manageTestToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.manageTestToolStripMenuItem.Text = "Manage Test Types";
            this.manageTestToolStripMenuItem.Click += new System.EventHandler(this.manageTestToolStripMenuItem_Click);
            // 
            // btnPeople
            // 
            this.btnPeople.AutoSize = false;
            this.btnPeople.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPeople.ForeColor = System.Drawing.Color.Black;
            this.btnPeople.Image = global::DVLD.WinForms.Properties.Resources.People64;
            this.btnPeople.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPeople.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPeople.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPeople.Name = "btnPeople";
            this.btnPeople.Size = new System.Drawing.Size(136, 70);
            this.btnPeople.Text = "People";
            this.btnPeople.Click += new System.EventHandler(this.btnPeople_Click);
            // 
            // btnDrivers
            // 
            this.btnDrivers.AutoSize = false;
            this.btnDrivers.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDrivers.ForeColor = System.Drawing.Color.Black;
            this.btnDrivers.Image = global::DVLD.WinForms.Properties.Resources.Driver64;
            this.btnDrivers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDrivers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDrivers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDrivers.Name = "btnDrivers";
            this.btnDrivers.Size = new System.Drawing.Size(140, 70);
            this.btnDrivers.Text = "Drivers";
            this.btnDrivers.Click += new System.EventHandler(this.btnDrivers_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.AutoSize = false;
            this.btnUsers.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsers.ForeColor = System.Drawing.Color.Black;
            this.btnUsers.Image = global::DVLD.WinForms.Properties.Resources.Users64;
            this.btnUsers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(130, 70);
            this.btnUsers.Text = "Users";
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnAccountSettings
            // 
            this.btnAccountSettings.AutoSize = false;
            this.btnAccountSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.toolStripSeparator1,
            this.signOutToolStripMenuItem});
            this.btnAccountSettings.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccountSettings.ForeColor = System.Drawing.Color.Black;
            this.btnAccountSettings.Image = global::DVLD.WinForms.Properties.Resources.Settings64;
            this.btnAccountSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAccountSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAccountSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAccountSettings.Name = "btnAccountSettings";
            this.btnAccountSettings.Size = new System.Drawing.Size(227, 75);
            this.btnAccountSettings.Text = "Account Settings";
            // 
            // currToolStripMenuItem
            // 
            this.currToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.information32;
            this.currToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.currToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.currToolStripMenuItem.Name = "currToolStripMenuItem";
            this.currToolStripMenuItem.Size = new System.Drawing.Size(246, 38);
            this.currToolStripMenuItem.Text = "Current User Info";
            this.currToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.currToolStripMenuItem.Click += new System.EventHandler(this.currToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.ResetPassword32;
            this.changePasswordToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(246, 38);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(243, 6);
            // 
            // signOutToolStripMenuItem
            // 
            this.signOutToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.SingOut32;
            this.signOutToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.signOutToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.signOutToolStripMenuItem.Name = "signOutToolStripMenuItem";
            this.signOutToolStripMenuItem.Size = new System.Drawing.Size(246, 38);
            this.signOutToolStripMenuItem.Text = "Sign Out";
            this.signOutToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.signOutToolStripMenuItem.Click += new System.EventHandler(this.signOutToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1378, 764);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmMain";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnUsers;
        private System.Windows.Forms.ToolStripButton btnPeople;
        private System.Windows.Forms.ToolStripButton btnDrivers;
        private System.Windows.Forms.ToolStripDropDownButton btnAccountSettings;
        private System.Windows.Forms.ToolStripMenuItem currToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem signOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton btnApplications;
        private System.Windows.Forms.ToolStripMenuItem driToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem detainedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageApplicationTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internationalLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renewDrivingLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem replacemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem realToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem retakeTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localDrivingLicenseApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internaToolStripMenuItem;
    }
}
namespace DVLD.WinForms.UserControls
{
    partial class ctrlLicenseReplacementInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlFindLicense1 = new DVLD.WinForms.UserControls.ctrlFindLicense();
            this.ctrlReplaceDamagedLicenseApplicationInfo1 = new DVLD.WinForms.UserControls.ctrlReplaceDamagedLicenseApplicationInfo();
            this.SuspendLayout();
            // 
            // ctrlFindLicense1
            // 
            this.ctrlFindLicense1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlFindLicense1.Location = new System.Drawing.Point(4, 5);
            this.ctrlFindLicense1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlFindLicense1.Name = "ctrlFindLicense1";
            this.ctrlFindLicense1.Size = new System.Drawing.Size(1087, 450);
            this.ctrlFindLicense1.TabIndex = 0;
            this.ctrlFindLicense1.OnLicenseSelected += new System.Action<DVLD.Core.DTOs.Entities.License>(this.ctrlFindLicense1_OnLicenseSelected);
            this.ctrlFindLicense1.OnLicenseNullSelected += new System.Action(this.ctrlFindLicense1_OnLicenseNullSelected);
            // 
            // ctrlReplaceDamagedLicenseApplicationInfo1
            // 
            this.ctrlReplaceDamagedLicenseApplicationInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlReplaceDamagedLicenseApplicationInfo1.Location = new System.Drawing.Point(4, 451);
            this.ctrlReplaceDamagedLicenseApplicationInfo1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlReplaceDamagedLicenseApplicationInfo1.Name = "ctrlReplaceDamagedLicenseApplicationInfo1";
            this.ctrlReplaceDamagedLicenseApplicationInfo1.Size = new System.Drawing.Size(1093, 173);
            this.ctrlReplaceDamagedLicenseApplicationInfo1.TabIndex = 1;
            // 
            // ctrlLicenseReplacementInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlFindLicense1);
            this.Controls.Add(this.ctrlReplaceDamagedLicenseApplicationInfo1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ctrlLicenseReplacementInfo";
            this.Size = new System.Drawing.Size(1097, 632);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlFindLicense ctrlFindLicense1;
        private ctrlReplaceDamagedLicenseApplicationInfo ctrlReplaceDamagedLicenseApplicationInfo1;
    }
}

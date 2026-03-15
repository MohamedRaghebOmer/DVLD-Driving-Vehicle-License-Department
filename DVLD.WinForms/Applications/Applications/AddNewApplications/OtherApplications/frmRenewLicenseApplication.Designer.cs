namespace DVLD.WinForms.Applications.Applications.AddNewApplications.OtherApplications
{
    partial class frmRenewLicenseApplication
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
            this.lblShowNewLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.btnRenew = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlLicenseAndApplicationInfo1 = new DVLD.WinForms.UserControls.ctrlLicenseAndApplicationInfo();
            this.SuspendLayout();
            // 
            // lblShowNewLicenseInfo
            // 
            this.lblShowNewLicenseInfo.AutoSize = true;
            this.lblShowNewLicenseInfo.Enabled = false;
            this.lblShowNewLicenseInfo.Location = new System.Drawing.Point(12, 764);
            this.lblShowNewLicenseInfo.Name = "lblShowNewLicenseInfo";
            this.lblShowNewLicenseInfo.Size = new System.Drawing.Size(216, 25);
            this.lblShowNewLicenseInfo.TabIndex = 1;
            this.lblShowNewLicenseInfo.TabStop = true;
            this.lblShowNewLicenseInfo.Text = "Show New License Info";
            this.lblShowNewLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowNewLicenseInfo_LinkClicked);
            // 
            // btnRenew
            // 
            this.btnRenew.Enabled = false;
            this.btnRenew.Image = global::DVLD.WinForms.Properties.Resources.RenewEnergy32;
            this.btnRenew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRenew.Location = new System.Drawing.Point(549, 751);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.Size = new System.Drawing.Size(138, 51);
            this.btnRenew.TabIndex = 16;
            this.btnRenew.Text = "Renew";
            this.btnRenew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRenew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRenew.UseVisualStyleBackColor = true;
            this.btnRenew.Click += new System.EventHandler(this.btnRenew_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::DVLD.WinForms.Properties.Resources.Close32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(405, 751);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(138, 51);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlLicenseAndApplicationInfo1
            // 
            this.ctrlLicenseAndApplicationInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlLicenseAndApplicationInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctrlLicenseAndApplicationInfo1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlLicenseAndApplicationInfo1.Name = "ctrlLicenseAndApplicationInfo1";
            this.ctrlLicenseAndApplicationInfo1.Size = new System.Drawing.Size(1089, 752);
            this.ctrlLicenseAndApplicationInfo1.TabIndex = 0;
            this.ctrlLicenseAndApplicationInfo1.OnLicenseSelected += new System.Action<DVLD.Core.DTOs.Entities.License>(this.ctrlLicenseAndApplicationInfo1_OnLicenseSelected);
            // 
            // frmRenewLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 804);
            this.Controls.Add(this.ctrlLicenseAndApplicationInfo1);
            this.Controls.Add(this.lblShowNewLicenseInfo);
            this.Controls.Add(this.btnRenew);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRenewLicenseApplication";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Renew License Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.LinkLabel lblShowNewLicenseInfo;
        private System.Windows.Forms.Button btnRenew;
        private System.Windows.Forms.Button btnClose;
        private UserControls.ctrlLicenseAndApplicationInfo ctrlLicenseAndApplicationInfo1;
    }
}
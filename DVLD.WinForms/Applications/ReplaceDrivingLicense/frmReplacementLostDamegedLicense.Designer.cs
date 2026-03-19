namespace DVLD.WinForms.Applications.Applications.AddNewApplications.OtherApplications
{
    partial class frmReplacementLostDamagedLicense
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbLostLicense = new System.Windows.Forms.RadioButton();
            this.rbDamagedLicense = new System.Windows.Forms.RadioButton();
            this.lblShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.lblShowNewLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.btnIssueReplacement = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlLicenseReplacementInfo1 = new DVLD.WinForms.UserControls.ctrlLicenseReplacementInfo();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(283, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(539, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Replacement For Damaged License";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbLostLicense);
            this.groupBox1.Controls.Add(this.rbDamagedLicense);
            this.groupBox1.Location = new System.Drawing.Point(13, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 98);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Replacement For:";
            // 
            // rbLostLicense
            // 
            this.rbLostLicense.AutoSize = true;
            this.rbLostLicense.Checked = true;
            this.rbLostLicense.Location = new System.Drawing.Point(6, 28);
            this.rbLostLicense.Name = "rbLostLicense";
            this.rbLostLicense.Size = new System.Drawing.Size(143, 29);
            this.rbLostLicense.TabIndex = 1;
            this.rbLostLicense.TabStop = true;
            this.rbLostLicense.Tag = "3";
            this.rbLostLicense.Text = "Lost License";
            this.rbLostLicense.UseVisualStyleBackColor = true;
            this.rbLostLicense.CheckedChanged += new System.EventHandler(this.ApplicationTypeChanged);
            // 
            // rbDamagedLicense
            // 
            this.rbDamagedLicense.AutoSize = true;
            this.rbDamagedLicense.Location = new System.Drawing.Point(6, 63);
            this.rbDamagedLicense.Name = "rbDamagedLicense";
            this.rbDamagedLicense.Size = new System.Drawing.Size(191, 29);
            this.rbDamagedLicense.TabIndex = 2;
            this.rbDamagedLicense.Tag = "4";
            this.rbDamagedLicense.Text = "Damaged License";
            this.rbDamagedLicense.UseVisualStyleBackColor = true;
            this.rbDamagedLicense.CheckedChanged += new System.EventHandler(this.ApplicationTypeChanged);
            // 
            // lblShowLicenseHistory
            // 
            this.lblShowLicenseHistory.AutoSize = true;
            this.lblShowLicenseHistory.Enabled = false;
            this.lblShowLicenseHistory.Location = new System.Drawing.Point(10, 738);
            this.lblShowLicenseHistory.Name = "lblShowLicenseHistory";
            this.lblShowLicenseHistory.Size = new System.Drawing.Size(200, 25);
            this.lblShowLicenseHistory.TabIndex = 4;
            this.lblShowLicenseHistory.TabStop = true;
            this.lblShowLicenseHistory.Text = "Show License History";
            this.lblShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowLicenseHistory_LinkClicked);
            // 
            // lblShowNewLicenseInfo
            // 
            this.lblShowNewLicenseInfo.AutoSize = true;
            this.lblShowNewLicenseInfo.Enabled = false;
            this.lblShowNewLicenseInfo.Location = new System.Drawing.Point(9, 771);
            this.lblShowNewLicenseInfo.Name = "lblShowNewLicenseInfo";
            this.lblShowNewLicenseInfo.Size = new System.Drawing.Size(216, 25);
            this.lblShowNewLicenseInfo.TabIndex = 5;
            this.lblShowNewLicenseInfo.TabStop = true;
            this.lblShowNewLicenseInfo.Text = "Show New License Info";
            this.lblShowNewLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowNewLicenseInfo_LinkClicked);
            // 
            // btnIssueReplacement
            // 
            this.btnIssueReplacement.Enabled = false;
            this.btnIssueReplacement.Image = global::DVLD.WinForms.Properties.Resources.Replace32;
            this.btnIssueReplacement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssueReplacement.Location = new System.Drawing.Point(514, 742);
            this.btnIssueReplacement.Name = "btnIssueReplacement";
            this.btnIssueReplacement.Size = new System.Drawing.Size(220, 51);
            this.btnIssueReplacement.TabIndex = 2;
            this.btnIssueReplacement.Text = "Issue Replacement";
            this.btnIssueReplacement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIssueReplacement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIssueReplacement.UseVisualStyleBackColor = true;
            this.btnIssueReplacement.Click += new System.EventHandler(this.btnIssueReplacement_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::DVLD.WinForms.Properties.Resources.Close32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(370, 742);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(138, 51);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlLicenseReplacementInfo1
            // 
            this.ctrlLicenseReplacementInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlLicenseReplacementInfo1.Location = new System.Drawing.Point(6, 110);
            this.ctrlLicenseReplacementInfo1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlLicenseReplacementInfo1.Name = "ctrlLicenseReplacementInfo1";
            this.ctrlLicenseReplacementInfo1.Size = new System.Drawing.Size(1097, 624);
            this.ctrlLicenseReplacementInfo1.TabIndex = 0;
            this.ctrlLicenseReplacementInfo1.OnLicenseSelected += new System.Action<DVLD.Core.DTOs.Entities.License>(this.ctrlLicenseReplacementInfo1_OnLicenseSelected);
            // 
            // frmReplacementLostDamagedLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 805);
            this.Controls.Add(this.ctrlLicenseReplacementInfo1);
            this.Controls.Add(this.lblShowLicenseHistory);
            this.Controls.Add(this.btnIssueReplacement);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblShowNewLicenseInfo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReplacementLostDamagedLicense";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Replacement Lost Or Dameged License";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbDamagedLicense;
        private System.Windows.Forms.RadioButton rbLostLicense;
        private System.Windows.Forms.LinkLabel lblShowLicenseHistory;
        private System.Windows.Forms.LinkLabel lblShowNewLicenseInfo;
        private System.Windows.Forms.Button btnIssueReplacement;
        private System.Windows.Forms.Button btnClose;
        private UserControls.ctrlLicenseReplacementInfo ctrlLicenseReplacementInfo1;
    }
}
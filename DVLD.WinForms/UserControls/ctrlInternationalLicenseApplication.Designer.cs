namespace DVLD.WinForms.UserControls
{
    partial class ctrlInternationalLicenseApplication
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtLicenseId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblShowInterLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.ctrlInternationalLicenseApplicationInfo1 = new DVLD.WinForms.UserControls.ctrlInternationalLicenseApplicationInfo();
            this.ctrlLicenseInfo1 = new DVLD.WinForms.UserControls.ctrlLicenseInfo();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.txtLicenseId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(1079, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Find Local License";
            // 
            // btnFind
            // 
            this.btnFind.Image = global::DVLD.WinForms.Properties.Resources.Find24;
            this.btnFind.Location = new System.Drawing.Point(334, 30);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(47, 38);
            this.btnFind.TabIndex = 1;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtLicenseId
            // 
            this.txtLicenseId.Location = new System.Drawing.Point(131, 34);
            this.txtLicenseId.Name = "txtLicenseId";
            this.txtLicenseId.Size = new System.Drawing.Size(197, 30);
            this.txtLicenseId.TabIndex = 0;
            this.txtLicenseId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLicenseId_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "License Id:";
            // 
            // lblShowInterLicenseInfo
            // 
            this.lblShowInterLicenseInfo.AutoSize = true;
            this.lblShowInterLicenseInfo.Enabled = false;
            this.lblShowInterLicenseInfo.Location = new System.Drawing.Point(5, 731);
            this.lblShowInterLicenseInfo.Name = "lblShowInterLicenseInfo";
            this.lblShowInterLicenseInfo.Size = new System.Drawing.Size(283, 25);
            this.lblShowInterLicenseInfo.TabIndex = 4;
            this.lblShowInterLicenseInfo.TabStop = true;
            this.lblShowInterLicenseInfo.Text = "Show International License Info";
            // 
            // ctrlInternationalLicenseApplicationInfo1
            // 
            this.ctrlInternationalLicenseApplicationInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlInternationalLicenseApplicationInfo1.Location = new System.Drawing.Point(8, 475);
            this.ctrlInternationalLicenseApplicationInfo1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlInternationalLicenseApplicationInfo1.Name = "ctrlInternationalLicenseApplicationInfo1";
            this.ctrlInternationalLicenseApplicationInfo1.Size = new System.Drawing.Size(1078, 242);
            this.ctrlInternationalLicenseApplicationInfo1.TabIndex = 3;
            // 
            // ctrlLicenseInfo1
            // 
            this.ctrlLicenseInfo1.ApplicationId = -1;
            this.ctrlLicenseInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlLicenseInfo1.LicenseId = -1;
            this.ctrlLicenseInfo1.LocalApplicationId = -1;
            this.ctrlLicenseInfo1.Location = new System.Drawing.Point(5, 103);
            this.ctrlLicenseInfo1.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlLicenseInfo1.Name = "ctrlLicenseInfo1";
            this.ctrlLicenseInfo1.NationalNo = "";
            this.ctrlLicenseInfo1.Size = new System.Drawing.Size(1079, 362);
            this.ctrlLicenseInfo1.TabIndex = 2;
            // 
            // ctrlInternationalLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblShowInterLicenseInfo);
            this.Controls.Add(this.ctrlInternationalLicenseApplicationInfo1);
            this.Controls.Add(this.ctrlLicenseInfo1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ctrlInternationalLicenseApplication";
            this.Size = new System.Drawing.Size(1092, 764);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLicenseId;
        private System.Windows.Forms.Button btnFind;
        private ctrlLicenseInfo ctrlLicenseInfo1;
        private ctrlInternationalLicenseApplicationInfo ctrlInternationalLicenseApplicationInfo1;
        private System.Windows.Forms.LinkLabel lblShowInterLicenseInfo;
    }
}

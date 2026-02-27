namespace DVLD.WinForms.Users
{
    partial class ctrlUserInfo
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
            this.gbLoginInfo = new System.Windows.Forms.GroupBox();
            this.lblIsActive = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUserId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEditloginInfo = new System.Windows.Forms.LinkLabel();
            this.ctrlPersonDetails1 = new DVLD.WinForms.UserControls.ctrlPersonDetails();
            this.gbLoginInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLoginInfo
            // 
            this.gbLoginInfo.Controls.Add(this.lblEditloginInfo);
            this.gbLoginInfo.Controls.Add(this.lblIsActive);
            this.gbLoginInfo.Controls.Add(this.label4);
            this.gbLoginInfo.Controls.Add(this.lblUsername);
            this.gbLoginInfo.Controls.Add(this.label3);
            this.gbLoginInfo.Controls.Add(this.lblUserId);
            this.gbLoginInfo.Controls.Add(this.label1);
            this.gbLoginInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLoginInfo.Location = new System.Drawing.Point(4, 5);
            this.gbLoginInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLoginInfo.Name = "gbLoginInfo";
            this.gbLoginInfo.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLoginInfo.Size = new System.Drawing.Size(914, 96);
            this.gbLoginInfo.TabIndex = 0;
            this.gbLoginInfo.TabStop = false;
            this.gbLoginInfo.Text = "Login Information";
            // 
            // lblIsActive
            // 
            this.lblIsActive.AutoSize = true;
            this.lblIsActive.Location = new System.Drawing.Point(773, 55);
            this.lblIsActive.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIsActive.Name = "lblIsActive";
            this.lblIsActive.Size = new System.Drawing.Size(45, 25);
            this.lblIsActive.TabIndex = 6;
            this.lblIsActive.Text = "???";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(708, 55);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "IsActive:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(452, 55);
            this.lblUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(45, 25);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "???";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(367, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Username:";
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(148, 55);
            this.lblUserId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(45, 25);
            this.lblUserId.TabIndex = 2;
            this.lblUserId.Text = "???";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "User Id:";
            // 
            // lblEditloginInfo
            // 
            this.lblEditloginInfo.AutoSize = true;
            this.lblEditloginInfo.Location = new System.Drawing.Point(390, 17);
            this.lblEditloginInfo.Name = "lblEditloginInfo";
            this.lblEditloginInfo.Size = new System.Drawing.Size(135, 25);
            this.lblEditloginInfo.TabIndex = 2;
            this.lblEditloginInfo.TabStop = true;
            this.lblEditloginInfo.Text = "Edit Login Info";
            this.lblEditloginInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblEditLoginInfo_LinkClicked);
            // 
            // ctrlPersonDetails1
            // 
            this.ctrlPersonDetails1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlPersonDetails1.Location = new System.Drawing.Point(0, 110);
            this.ctrlPersonDetails1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlPersonDetails1.Name = "ctrlPersonDetails1";
            this.ctrlPersonDetails1.NationalNo = "";
            this.ctrlPersonDetails1.PersonID = -1;
            this.ctrlPersonDetails1.Size = new System.Drawing.Size(914, 323);
            this.ctrlPersonDetails1.TabIndex = 1;
            // 
            // ctrlUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlPersonDetails1);
            this.Controls.Add(this.gbLoginInfo);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ctrlUserInfo";
            this.Size = new System.Drawing.Size(921, 438);
            this.gbLoginInfo.ResumeLayout(false);
            this.gbLoginInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLoginInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblIsActive;
        private System.Windows.Forms.Label label4;
        private UserControls.ctrlPersonDetails ctrlPersonDetails1;
        private System.Windows.Forms.LinkLabel lblEditloginInfo;
    }
}

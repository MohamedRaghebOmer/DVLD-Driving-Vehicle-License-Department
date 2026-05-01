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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbName = new System.Windows.Forms.PictureBox();
            this.pbNationalNo = new System.Windows.Forms.PictureBox();
            this.lblEditloginInfo = new System.Windows.Forms.LinkLabel();
            this.lblIsActive = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUserId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlPersonDetails1 = new DVLD.WinForms.UserControls.ctrlPersonDetails();
            this.gbLoginInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNationalNo)).BeginInit();
            this.SuspendLayout();
            // 
            // gbLoginInfo
            // 
            this.gbLoginInfo.Controls.Add(this.pictureBox1);
            this.gbLoginInfo.Controls.Add(this.pbName);
            this.gbLoginInfo.Controls.Add(this.pbNationalNo);
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
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.WinForms.Properties.Resources.information32;
            this.pictureBox1.Location = new System.Drawing.Point(784, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 99;
            this.pictureBox1.TabStop = false;
            // 
            // pbName
            // 
            this.pbName.Image = global::DVLD.WinForms.Properties.Resources.NameCard512;
            this.pbName.Location = new System.Drawing.Point(460, 52);
            this.pbName.Name = "pbName";
            this.pbName.Size = new System.Drawing.Size(28, 28);
            this.pbName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbName.TabIndex = 98;
            this.pbName.TabStop = false;
            // 
            // pbNationalNo
            // 
            this.pbNationalNo.Image = global::DVLD.WinForms.Properties.Resources.GrayCard512;
            this.pbNationalNo.Location = new System.Drawing.Point(172, 52);
            this.pbNationalNo.Name = "pbNationalNo";
            this.pbNationalNo.Size = new System.Drawing.Size(28, 28);
            this.pbNationalNo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbNationalNo.TabIndex = 77;
            this.pbNationalNo.TabStop = false;
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
            // lblIsActive
            // 
            this.lblIsActive.AutoSize = true;
            this.lblIsActive.Location = new System.Drawing.Point(819, 55);
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
            this.lblUsername.Location = new System.Drawing.Point(495, 55);
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
            this.lblUserId.Location = new System.Drawing.Point(200, 55);
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
            // ctrlPersonDetails1
            // 
            this.ctrlPersonDetails1.DriverID = -1;
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNationalNo)).EndInit();
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
        private System.Windows.Forms.PictureBox pbNationalNo;
        private System.Windows.Forms.PictureBox pbName;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

namespace DVLD.WinForms.Licenses.Detain
{
    partial class frmManageDetainedLicenses
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
            this.components = new System.ComponentModel.Container();
            this.dtpDetainReleaseDate = new System.Windows.Forms.DateTimePicker();
            this.lblCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.cbIsReleased = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDetainNewLicense = new System.Windows.Forms.Button();
            this.dgvDetainedLicenses = new System.Windows.Forms.DataGridView();
            this.colDetainID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLicenseID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetainDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsReleased = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFineFees = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReleaseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNationalNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReleaseAppId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnReleaseLicense = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicenses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDetainReleaseDate
            // 
            this.dtpDetainReleaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDetainReleaseDate.Location = new System.Drawing.Point(324, 330);
            this.dtpDetainReleaseDate.Name = "dtpDetainReleaseDate";
            this.dtpDetainReleaseDate.Size = new System.Drawing.Size(221, 30);
            this.dtpDetainReleaseDate.TabIndex = 52;
            this.dtpDetainReleaseDate.Visible = false;
            this.dtpDetainReleaseDate.ValueChanged += new System.EventHandler(this.ApplyRowFilter);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(95, 9);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(23, 25);
            this.lblCount.TabIndex = 50;
            this.lblCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 25);
            this.label3.TabIndex = 49;
            this.label3.Text = "# Records:";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.Clock32;
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
            this.showToolStripMenuItem.Text = "Show Person License History";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // showLicenseDetailsToolStripMenuItem
            // 
            this.showLicenseDetailsToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.InternationalLicense512;
            this.showLicenseDetailsToolStripMenuItem.Name = "showLicenseDetailsToolStripMenuItem";
            this.showLicenseDetailsToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
            this.showLicenseDetailsToolStripMenuItem.Text = "Show License Details";
            this.showLicenseDetailsToolStripMenuItem.Click += new System.EventHandler(this.showLicenseDetailsToolStripMenuItem_Click);
            // 
            // showPersonDetailsToolStripMenuItem
            // 
            this.showPersonDetailsToolStripMenuItem.Image = global::DVLD.WinForms.Properties.Resources.UsernameBox72;
            this.showPersonDetailsToolStripMenuItem.Name = "showPersonDetailsToolStripMenuItem";
            this.showPersonDetailsToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
            this.showPersonDetailsToolStripMenuItem.Text = "Show Person Details ";
            this.showPersonDetailsToolStripMenuItem.Click += new System.EventHandler(this.showPersonDetailsToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPersonDetailsToolStripMenuItem,
            this.showLicenseDetailsToolStripMenuItem,
            this.showToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(269, 82);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::DVLD.WinForms.Properties.Resources.Close32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1157, 703);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(138, 51);
            this.btnClose.TabIndex = 51;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbIsReleased
            // 
            this.cbIsReleased.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsReleased.FormattingEnabled = true;
            this.cbIsReleased.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cbIsReleased.Location = new System.Drawing.Point(324, 327);
            this.cbIsReleased.Name = "cbIsReleased";
            this.cbIsReleased.Size = new System.Drawing.Size(221, 33);
            this.cbIsReleased.TabIndex = 48;
            this.cbIsReleased.Visible = false;
            this.cbIsReleased.SelectedIndexChanged += new System.EventHandler(this.ApplyRowFilter);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterValue.Location = new System.Drawing.Point(324, 328);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(221, 34);
            this.txtFilterValue.TabIndex = 47;
            this.txtFilterValue.Visible = false;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.ApplyRowFilter);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Detain Id",
            "License Id",
            "Detain Date",
            "Is Released",
            "Fine Fees",
            "Release Date",
            "National Number",
            "Full Name",
            "Release Application Id"});
            this.cbFilterBy.Location = new System.Drawing.Point(97, 329);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(221, 33);
            this.cbFilterBy.TabIndex = 46;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 332);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 25);
            this.label2.TabIndex = 45;
            this.label2.Text = "Filter By:";
            // 
            // btnDetainNewLicense
            // 
            this.btnDetainNewLicense.Image = global::DVLD.WinForms.Properties.Resources.Detain32;
            this.btnDetainNewLicense.Location = new System.Drawing.Point(1232, 312);
            this.btnDetainNewLicense.Name = "btnDetainNewLicense";
            this.btnDetainNewLicense.Size = new System.Drawing.Size(63, 61);
            this.btnDetainNewLicense.TabIndex = 44;
            this.btnDetainNewLicense.UseVisualStyleBackColor = true;
            this.btnDetainNewLicense.Click += new System.EventHandler(this.btnDetainNewLicense_Click);
            // 
            // dgvDetainedLicenses
            // 
            this.dgvDetainedLicenses.AllowUserToAddRows = false;
            this.dgvDetainedLicenses.AllowUserToDeleteRows = false;
            this.dgvDetainedLicenses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetainedLicenses.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDetainedLicenses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDetainedLicenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetainedLicenses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDetainID,
            this.colLicenseID,
            this.colDetainDate,
            this.colIsReleased,
            this.colFineFees,
            this.colReleaseDate,
            this.colNationalNo,
            this.colFullName,
            this.colReleaseAppId});
            this.dgvDetainedLicenses.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvDetainedLicenses.Location = new System.Drawing.Point(0, 379);
            this.dgvDetainedLicenses.Name = "dgvDetainedLicenses";
            this.dgvDetainedLicenses.ReadOnly = true;
            this.dgvDetainedLicenses.RowHeadersWidth = 51;
            this.dgvDetainedLicenses.RowTemplate.Height = 24;
            this.dgvDetainedLicenses.Size = new System.Drawing.Size(1295, 313);
            this.dgvDetainedLicenses.TabIndex = 43;
            this.dgvDetainedLicenses.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.UpdateLabelCount);
            this.dgvDetainedLicenses.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.UpdateLabelCount);
            this.dgvDetainedLicenses.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvDetainedLicenses_MouseDown);
            // 
            // colDetainID
            // 
            this.colDetainID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colDetainID.DataPropertyName = "DetainID";
            this.colDetainID.HeaderText = "Detain Id";
            this.colDetainID.MinimumWidth = 80;
            this.colDetainID.Name = "colDetainID";
            this.colDetainID.ReadOnly = true;
            this.colDetainID.Width = 109;
            // 
            // colLicenseID
            // 
            this.colLicenseID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colLicenseID.DataPropertyName = "LicenseID";
            this.colLicenseID.HeaderText = "License Id";
            this.colLicenseID.MinimumWidth = 80;
            this.colLicenseID.Name = "colLicenseID";
            this.colLicenseID.ReadOnly = true;
            this.colLicenseID.Width = 119;
            // 
            // colDetainDate
            // 
            this.colDetainDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colDetainDate.DataPropertyName = "DetainDate";
            this.colDetainDate.HeaderText = "Detain Date";
            this.colDetainDate.MinimumWidth = 85;
            this.colDetainDate.Name = "colDetainDate";
            this.colDetainDate.ReadOnly = true;
            this.colDetainDate.Width = 131;
            // 
            // colIsReleased
            // 
            this.colIsReleased.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colIsReleased.DataPropertyName = "IsReleased";
            this.colIsReleased.FillWeight = 225F;
            this.colIsReleased.HeaderText = "Is Released";
            this.colIsReleased.MinimumWidth = 180;
            this.colIsReleased.Name = "colIsReleased";
            this.colIsReleased.ReadOnly = true;
            this.colIsReleased.Width = 180;
            // 
            // colFineFees
            // 
            this.colFineFees.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colFineFees.DataPropertyName = "FineFees";
            this.colFineFees.FillWeight = 120F;
            this.colFineFees.HeaderText = "Fine Fees";
            this.colFineFees.MinimumWidth = 6;
            this.colFineFees.Name = "colFineFees";
            this.colFineFees.ReadOnly = true;
            this.colFineFees.Width = 118;
            // 
            // colReleaseDate
            // 
            this.colReleaseDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colReleaseDate.DataPropertyName = "ReleaseDate";
            this.colReleaseDate.FillWeight = 120F;
            this.colReleaseDate.HeaderText = "Release Date";
            this.colReleaseDate.MinimumWidth = 6;
            this.colReleaseDate.Name = "colReleaseDate";
            this.colReleaseDate.ReadOnly = true;
            this.colReleaseDate.Width = 145;
            // 
            // colNationalNo
            // 
            this.colNationalNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colNationalNo.DataPropertyName = "NationalNo";
            this.colNationalNo.HeaderText = "National Number";
            this.colNationalNo.MinimumWidth = 100;
            this.colNationalNo.Name = "colNationalNo";
            this.colNationalNo.ReadOnly = true;
            this.colNationalNo.Width = 170;
            // 
            // colFullName
            // 
            this.colFullName.DataPropertyName = "FullName";
            this.colFullName.HeaderText = "Full Name";
            this.colFullName.MinimumWidth = 6;
            this.colFullName.Name = "colFullName";
            this.colFullName.ReadOnly = true;
            // 
            // colReleaseAppId
            // 
            this.colReleaseAppId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colReleaseAppId.DataPropertyName = "ReleaseApplicationID";
            this.colReleaseAppId.HeaderText = "Release App Id";
            this.colReleaseAppId.MinimumWidth = 100;
            this.colReleaseAppId.Name = "colReleaseAppId";
            this.colReleaseAppId.ReadOnly = true;
            this.colReleaseAppId.Width = 145;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(469, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(412, 38);
            this.label1.TabIndex = 42;
            this.label1.Text = "Manage Detained Licenses";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.WinForms.Properties.Resources.InternationalLicense512;
            this.pictureBox1.Location = new System.Drawing.Point(562, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(221, 199);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            // 
            // btnReleaseLicense
            // 
            this.btnReleaseLicense.Image = global::DVLD.WinForms.Properties.Resources.Rocket32;
            this.btnReleaseLicense.Location = new System.Drawing.Point(1163, 311);
            this.btnReleaseLicense.Name = "btnReleaseLicense";
            this.btnReleaseLicense.Size = new System.Drawing.Size(63, 61);
            this.btnReleaseLicense.TabIndex = 53;
            this.btnReleaseLicense.UseVisualStyleBackColor = true;
            this.btnReleaseLicense.Click += new System.EventHandler(this.btnReleaseLicense_Click);
            // 
            // frmManageDetainedLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1307, 766);
            this.Controls.Add(this.btnReleaseLicense);
            this.Controls.Add(this.dtpDetainReleaseDate);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cbIsReleased);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDetainNewLicense);
            this.Controls.Add(this.dgvDetainedLicenses);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManageDetainedLicenses";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Detained Licenses";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicenses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDetainReleaseDate;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPersonDetailsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbIsReleased;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDetainNewLicense;
        private System.Windows.Forms.DataGridView dgvDetainedLicenses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnReleaseLicense;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetainID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLicenseID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetainDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsReleased;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFineFees;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReleaseDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNationalNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReleaseAppId;
    }
}
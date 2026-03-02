namespace DVLD.WinForms.Applications.TestTypes
{
    partial class frmManageTestTypes
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
            this.dgvTestTypes = new System.Windows.Forms.DataGridView();
            this.colApplicationTypeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colApplicationTypeTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colApplicationFees = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestTypes)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTestTypes
            // 
            this.dgvTestTypes.AllowUserToAddRows = false;
            this.dgvTestTypes.AllowUserToDeleteRows = false;
            this.dgvTestTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTestTypes.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvTestTypes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTestTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestTypes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colApplicationTypeId,
            this.colApplicationTypeTitle,
            this.colDescription,
            this.colApplicationFees});
            this.dgvTestTypes.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvTestTypes.Location = new System.Drawing.Point(12, 290);
            this.dgvTestTypes.Name = "dgvTestTypes";
            this.dgvTestTypes.ReadOnly = true;
            this.dgvTestTypes.RowHeadersWidth = 51;
            this.dgvTestTypes.RowTemplate.Height = 24;
            this.dgvTestTypes.Size = new System.Drawing.Size(1004, 257);
            this.dgvTestTypes.TabIndex = 20;
            this.dgvTestTypes.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.UpdateLabelCount);
            this.dgvTestTypes.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.UpdateLabelCount);
            this.dgvTestTypes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvTestTypes_MouseDown);
            // 
            // colApplicationTypeId
            // 
            this.colApplicationTypeId.DataPropertyName = "TestTypeID";
            this.colApplicationTypeId.FillWeight = 50F;
            this.colApplicationTypeId.HeaderText = "Id";
            this.colApplicationTypeId.MinimumWidth = 50;
            this.colApplicationTypeId.Name = "colApplicationTypeId";
            this.colApplicationTypeId.ReadOnly = true;
            // 
            // colApplicationTypeTitle
            // 
            this.colApplicationTypeTitle.DataPropertyName = "TestTypeTitle";
            this.colApplicationTypeTitle.HeaderText = "Title";
            this.colApplicationTypeTitle.MinimumWidth = 100;
            this.colApplicationTypeTitle.Name = "colApplicationTypeTitle";
            this.colApplicationTypeTitle.ReadOnly = true;
            // 
            // colDescription
            // 
            this.colDescription.DataPropertyName = "TestTypeDescription";
            this.colDescription.FillWeight = 170F;
            this.colDescription.HeaderText = "Description";
            this.colDescription.MinimumWidth = 6;
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // colApplicationFees
            // 
            this.colApplicationFees.DataPropertyName = "TestTypeFees";
            this.colApplicationFees.FillWeight = 90F;
            this.colApplicationFees.HeaderText = "Fees";
            this.colApplicationFees.MinimumWidth = 61;
            this.colApplicationFees.Name = "colApplicationFees";
            this.colApplicationFees.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 28);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(104, 24);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(383, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 38);
            this.label1.TabIndex = 19;
            this.label1.Text = "Manage Test Types";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.WinForms.Properties.Resources.Paper512;
            this.pictureBox1.Location = new System.Drawing.Point(392, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(222, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(90, 9);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(23, 25);
            this.lblCount.TabIndex = 23;
            this.lblCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 25);
            this.label3.TabIndex = 22;
            this.label3.Text = "# Records:";
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::DVLD.WinForms.Properties.Resources.Close32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(858, 553);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(138, 51);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmManageTestTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 614);
            this.Controls.Add(this.dgvTestTypes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmManageTestTypes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Test Types";
            this.Load += new System.EventHandler(this.frmManageTestTypes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestTypes)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTestTypes;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApplicationTypeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApplicationTypeTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApplicationFees;
    }
}
namespace DVLD.WinForms.NavigateForms
{
    partial class frmManageApplicationTypes
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvApplicationTypes = new System.Windows.Forms.DataGridView();
            this.colApplicationTypeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colApplicationTypeTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colApplicationFees = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApplicationTypes)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.WinForms.Properties.Resources.Shapes512;
            this.pictureBox1.Location = new System.Drawing.Point(304, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(222, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(257, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(401, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manage Application Types";
            // 
            // dgvApplicationTypes
            // 
            this.dgvApplicationTypes.AllowUserToAddRows = false;
            this.dgvApplicationTypes.AllowUserToDeleteRows = false;
            this.dgvApplicationTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvApplicationTypes.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvApplicationTypes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvApplicationTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvApplicationTypes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colApplicationTypeId,
            this.colApplicationTypeTitle,
            this.colApplicationFees});
            this.dgvApplicationTypes.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvApplicationTypes.Location = new System.Drawing.Point(-5, 256);
            this.dgvApplicationTypes.Name = "dgvApplicationTypes";
            this.dgvApplicationTypes.ReadOnly = true;
            this.dgvApplicationTypes.RowHeadersWidth = 51;
            this.dgvApplicationTypes.RowTemplate.Height = 24;
            this.dgvApplicationTypes.Size = new System.Drawing.Size(833, 257);
            this.dgvApplicationTypes.TabIndex = 2;
            this.dgvApplicationTypes.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.UpdateLabelCount);
            this.dgvApplicationTypes.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.UpdateLabelCount);
            this.dgvApplicationTypes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvApplicationTypes_MouseDown);
            // 
            // colApplicationTypeId
            // 
            this.colApplicationTypeId.DataPropertyName = "ApplicationTypeID";
            this.colApplicationTypeId.HeaderText = "Id";
            this.colApplicationTypeId.MinimumWidth = 150;
            this.colApplicationTypeId.Name = "colApplicationTypeId";
            this.colApplicationTypeId.ReadOnly = true;
            // 
            // colApplicationTypeTitle
            // 
            this.colApplicationTypeTitle.DataPropertyName = "ApplicationTypeTitle";
            this.colApplicationTypeTitle.FillWeight = 200F;
            this.colApplicationTypeTitle.HeaderText = "Title";
            this.colApplicationTypeTitle.MinimumWidth = 250;
            this.colApplicationTypeTitle.Name = "colApplicationTypeTitle";
            this.colApplicationTypeTitle.ReadOnly = true;
            // 
            // colApplicationFees
            // 
            this.colApplicationFees.DataPropertyName = "ApplicationFees";
            this.colApplicationFees.FillWeight = 115.6984F;
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
            // btnCancel
            // 
            this.btnCancel.Image = global::DVLD.WinForms.Properties.Resources.Close32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(677, 519);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(138, 51);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 25);
            this.label3.TabIndex = 15;
            this.label3.Text = "# Records:";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(91, 12);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(23, 25);
            this.lblCount.TabIndex = 17;
            this.lblCount.Text = "0";
            // 
            // frmManageApplicationTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 577);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvApplicationTypes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmManageApplicationTypes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Application Types";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApplicationTypes)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvApplicationTypes;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApplicationTypeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApplicationTypeTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApplicationFees;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    }
}
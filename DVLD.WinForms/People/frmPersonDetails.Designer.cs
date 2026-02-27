namespace DVLD.WinForms.People
{
    partial class frmPersonDetails
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
            this.lblFormlabel = new System.Windows.Forms.Label();
            this.ctrlPersonDetails1 = new DVLD.WinForms.UserControls.ctrlPersonDetails();
            this.SuspendLayout();
            // 
            // lblFormlabel
            // 
            this.lblFormlabel.AutoSize = true;
            this.lblFormlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormlabel.ForeColor = System.Drawing.Color.Coral;
            this.lblFormlabel.Location = new System.Drawing.Point(352, 9);
            this.lblFormlabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblFormlabel.Name = "lblFormlabel";
            this.lblFormlabel.Size = new System.Drawing.Size(261, 42);
            this.lblFormlabel.TabIndex = 2;
            this.lblFormlabel.Text = "Person Details";
            // 
            // ctrlPersonDetails1
            // 
            this.ctrlPersonDetails1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlPersonDetails1.Location = new System.Drawing.Point(2, 55);
            this.ctrlPersonDetails1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlPersonDetails1.Name = "ctrlPersonDetails1";
            this.ctrlPersonDetails1.NationalNo = "";
            this.ctrlPersonDetails1.PersonID = -1;
            this.ctrlPersonDetails1.Size = new System.Drawing.Size(918, 325);
            this.ctrlPersonDetails1.TabIndex = 3;
            // 
            // frmPersonDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 387);
            this.Controls.Add(this.ctrlPersonDetails1);
            this.Controls.Add(this.lblFormlabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPersonDetails";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Person Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFormlabel;
        private UserControls.ctrlPersonDetails ctrlPersonDetails1;
    }
}
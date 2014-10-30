namespace FBLADeskProject
{
    partial class frmHome
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
            this.lblRegister = new System.Windows.Forms.Label();
            this.btnRegWkshp = new System.Windows.Forms.Button();
            this.btnRegConf = new System.Windows.Forms.Button();
            this.lblCreate = new System.Windows.Forms.Label();
            this.btnCreateWkshp = new System.Windows.Forms.Button();
            this.btnCreateUser = new System.Windows.Forms.Button();
            this.lblView = new System.Windows.Forms.Label();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblRegister
            // 
            this.lblRegister.AutoSize = true;
            this.lblRegister.Location = new System.Drawing.Point(15, 9);
            this.lblRegister.Name = "lblRegister";
            this.lblRegister.Size = new System.Drawing.Size(94, 21);
            this.lblRegister.TabIndex = 0;
            this.lblRegister.Text = "Register for:";
            // 
            // btnRegWkshp
            // 
            this.btnRegWkshp.Location = new System.Drawing.Point(13, 71);
            this.btnRegWkshp.Name = "btnRegWkshp";
            this.btnRegWkshp.Size = new System.Drawing.Size(98, 32);
            this.btnRegWkshp.TabIndex = 2;
            this.btnRegWkshp.Text = "&Workshop";
            this.btnRegWkshp.UseVisualStyleBackColor = true;
            this.btnRegWkshp.Click += new System.EventHandler(this.btnRegWkshp_Click);
            // 
            // btnRegConf
            // 
            this.btnRegConf.Location = new System.Drawing.Point(13, 33);
            this.btnRegConf.Name = "btnRegConf";
            this.btnRegConf.Size = new System.Drawing.Size(98, 32);
            this.btnRegConf.TabIndex = 1;
            this.btnRegConf.Text = "&Conference";
            this.btnRegConf.UseVisualStyleBackColor = true;
            this.btnRegConf.Click += new System.EventHandler(this.btnRegConf_Click);
            // 
            // lblCreate
            // 
            this.lblCreate.AutoSize = true;
            this.lblCreate.Location = new System.Drawing.Point(27, 216);
            this.lblCreate.Name = "lblCreate";
            this.lblCreate.Size = new System.Drawing.Size(70, 21);
            this.lblCreate.TabIndex = 6;
            this.lblCreate.Text = "Create a:";
            // 
            // btnCreateWkshp
            // 
            this.btnCreateWkshp.Location = new System.Drawing.Point(13, 240);
            this.btnCreateWkshp.Name = "btnCreateWkshp";
            this.btnCreateWkshp.Size = new System.Drawing.Size(98, 32);
            this.btnCreateWkshp.TabIndex = 7;
            this.btnCreateWkshp.Text = "W&orkshop";
            this.btnCreateWkshp.UseVisualStyleBackColor = true;
            this.btnCreateWkshp.Click += new System.EventHandler(this.btnCreateWkshp_Click);
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.Location = new System.Drawing.Point(13, 278);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new System.Drawing.Size(98, 32);
            this.btnCreateUser.TabIndex = 8;
            this.btnCreateUser.Text = "&User";
            this.btnCreateUser.UseVisualStyleBackColor = true;
            this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);
            // 
            // lblView
            // 
            this.lblView.AutoSize = true;
            this.lblView.Location = new System.Drawing.Point(39, 110);
            this.lblView.Name = "lblView";
            this.lblView.Size = new System.Drawing.Size(47, 21);
            this.lblView.TabIndex = 3;
            this.lblView.Text = "View:";
            // 
            // btnProfile
            // 
            this.btnProfile.Location = new System.Drawing.Point(14, 134);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(96, 32);
            this.btnProfile.TabIndex = 4;
            this.btnProfile.Text = "&Profile";
            this.btnProfile.UseVisualStyleBackColor = true;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(14, 172);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(96, 32);
            this.btnReport.TabIndex = 5;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(124, 322);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.lblView);
            this.Controls.Add(this.btnCreateUser);
            this.Controls.Add(this.btnCreateWkshp);
            this.Controls.Add(this.lblCreate);
            this.Controls.Add(this.btnRegConf);
            this.Controls.Add(this.btnRegWkshp);
            this.Controls.Add(this.lblRegister);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmHome";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmHome_FormClosing);
            this.Load += new System.EventHandler(this.frmHome_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRegister;
        private System.Windows.Forms.Button btnRegWkshp;
        private System.Windows.Forms.Button btnRegConf;
        private System.Windows.Forms.Label lblCreate;
        private System.Windows.Forms.Button btnCreateWkshp;
        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.Label lblView;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnReport;

    }
}
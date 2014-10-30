namespace FBLADeskProject
{
    partial class frmReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReport));
            this.btnConfReport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbConf = new System.Windows.Forms.ComboBox();
            this.btnWkshpReport = new System.Windows.Forms.Button();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.ReportViewer = new AxAcroPDFLib.AxAcroPDF();
            ((System.ComponentModel.ISupportInitialize)(this.ReportViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConfReport
            // 
            this.btnConfReport.Enabled = false;
            this.btnConfReport.Location = new System.Drawing.Point(94, 16);
            this.btnConfReport.Name = "btnConfReport";
            this.btnConfReport.Size = new System.Drawing.Size(149, 29);
            this.btnConfReport.TabIndex = 1;
            this.btnConfReport.Text = "C&onference Report";
            this.btnConfReport.UseVisualStyleBackColor = true;
            this.btnConfReport.Click += new System.EventHandler(this.btnConfReport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 488);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbConf
            // 
            this.cmbConf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConf.Enabled = false;
            this.cmbConf.FormattingEnabled = true;
            this.cmbConf.Items.AddRange(new object[] {
            "NLA",
            "WDC",
            "MMN"});
            this.cmbConf.Location = new System.Drawing.Point(12, 16);
            this.cmbConf.Name = "cmbConf";
            this.cmbConf.Size = new System.Drawing.Size(76, 29);
            this.cmbConf.TabIndex = 0;
            this.cmbConf.SelectedIndexChanged += new System.EventHandler(this.cmbConf_SelectedIndexChanged);
            // 
            // btnWkshpReport
            // 
            this.btnWkshpReport.Enabled = false;
            this.btnWkshpReport.Location = new System.Drawing.Point(249, 16);
            this.btnWkshpReport.Name = "btnWkshpReport";
            this.btnWkshpReport.Size = new System.Drawing.Size(149, 29);
            this.btnWkshpReport.TabIndex = 3;
            this.btnWkshpReport.Text = "&Workshop Report";
            this.btnWkshpReport.UseVisualStyleBackColor = true;
            this.btnWkshpReport.Click += new System.EventHandler(this.btnWkshpReport_Click);
            // 
            // btnSchedule
            // 
            this.btnSchedule.Enabled = false;
            this.btnSchedule.Location = new System.Drawing.Point(404, 16);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(84, 29);
            this.btnSchedule.TabIndex = 4;
            this.btnSchedule.Text = "&Schedule";
            this.btnSchedule.UseVisualStyleBackColor = true;
            this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
            // 
            // ReportViewer
            // 
            this.ReportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ReportViewer.Enabled = true;
            this.ReportViewer.Location = new System.Drawing.Point(12, 51);
            this.ReportViewer.Name = "ReportViewer";
            this.ReportViewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("ReportViewer.OcxState")));
            this.ReportViewer.Size = new System.Drawing.Size(797, 429);
            this.ReportViewer.TabIndex = 6;
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(822, 530);
            this.Controls.Add(this.ReportViewer);
            this.Controls.Add(this.btnSchedule);
            this.Controls.Add(this.btnWkshpReport);
            this.Controls.Add(this.cmbConf);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfReport);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmReport";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReport_FormClosing);
            this.Load += new System.EventHandler(this.frmReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReportViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConfReport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbConf;
        private System.Windows.Forms.Button btnWkshpReport;
        private System.Windows.Forms.Button btnSchedule;
        private AxAcroPDFLib.AxAcroPDF ReportViewer;
    }
}
/* frmRegWkshp.cs
 * This form handles the events for when the user registers for workshops
 * Programmer: Logan Lopez
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FBLADeskProject
{
    public partial class frmRegWkshp : Form
    {
        private List<Workshop> workshops;
        private Participant part;
        // storing the type and userID throughout the program
        internal Participant Part
        {
            get
            {
                return part;
            }
            set
            {
                part = value;
            }
        }
        public frmRegWkshp()
        {
            InitializeComponent();
        }

        private void frmRegWkshp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void frmRegWkshp_Load(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            string confcode = db.GetConf(part.UUID);
            workshops = db.GetWorkshops(confcode);
            for (int i = 0; i < workshops.Count; i++)
            {
                cmbWorkshops.Items.Add(workshops[i].Name);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmHome frmHome = new frmHome();
            frmHome.Part = part;
            frmHome.Show();
            this.Hide();
        }

        private void cmbWorkshops_SelectedIndexChanged(object sender, EventArgs e)
        {
            int workshopSelection = cmbWorkshops.SelectedIndex;
            lblTitle.Text = workshops[workshopSelection].Name;
            lblDescription.Text = workshops[workshopSelection].Description;
            DateTime startDate = workshops[workshopSelection].Date;
            lblDate.Text = startDate.ToShortDateString();
            lblTime.Text = startDate.ToShortTimeString();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cmbWorkshops.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a workshop first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DBConnect db = new DBConnect();
                string wkshp = workshops[cmbWorkshops.SelectedIndex].UUID;
                bool success = db.RegWkshp(part.UUID, wkshp);
                if (success)
                {
                    MessageBox.Show("Success");
                    frmHome frmHome = new frmHome();
                    frmHome.Part = part;
                    frmHome.Show();
                    this.Hide();
                }
            }
        }
    }
}

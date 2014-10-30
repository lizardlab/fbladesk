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
        private string userID;
        private int type;
        private List<string>[] workshops;
        public int Type
        {
            set
            {
                type = value;
            }
        }
        public string UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
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
            string confcode = db.GetConf(userID);
            workshops = db.GetWorkshops(confcode);
            for (int i = 0; i < workshops.GetLength(0); i++)
            {
                cmbWorkshops.Items.Add(workshops[0][i]);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmHome frmHome = new frmHome();
            frmHome.UserID = userID;
            frmHome.Type = type;
            frmHome.Show();
            this.Hide();
        }

        private void cmbWorkshops_SelectedIndexChanged(object sender, EventArgs e)
        {
            int workshopSelection = cmbWorkshops.SelectedIndex;
            lblTitle.Text = workshops[0][workshopSelection];
            lblDescription.Text = workshops[1][workshopSelection];
            DateTime startDate = new DateTime();
            startDate = DateTime.Parse(workshops[2][workshopSelection]);
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
                string wkshp = workshops[3][cmbWorkshops.SelectedIndex];
                bool success = db.RegWkshp(userID, wkshp);
                if (success)
                {
                    MessageBox.Show("Success");
                    frmHome frmHome = new frmHome();
                    frmHome.UserID = userID;
                    frmHome.Type = type;
                    frmHome.Show();
                    this.Hide();
                }
            }
        }
    }
}

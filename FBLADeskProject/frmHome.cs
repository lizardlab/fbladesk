/* frmHome.cs
 * This handles all the events and necessary restrictions for the 
 * Home form and makes sure the user cannot access things that are 
 * unauthorized or that they need other information to complete.
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
    public partial class frmHome : Form
    {
        // store the type and userID for persistence around the program
        private string userID;
        private int type;
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
        public frmHome()
        {
            InitializeComponent();
        }
        // when user clicks create workshop
        private void btnCreateWkshp_Click(object sender, EventArgs e)
        {
            frmCreateWkshp frmCreateWkshp = new frmCreateWkshp();
            frmCreateWkshp.Type = type;
            frmCreateWkshp.UserID = userID;
            frmCreateWkshp.Show();
            this.Hide();
        }
        // when user clicks create user
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            frmCreateUser frmCreateUser = new frmCreateUser();
            frmCreateUser.Type = type;
            frmCreateUser.UserID = userID;
            frmCreateUser.Show();
            this.Hide();
        }
        // when user hits the corner red X
        private void frmHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        // allows user to register for conference
        private void btnRegConf_Click(object sender, EventArgs e)
        {
            frmRegConf frmRegConf = new frmRegConf();
            frmRegConf.UserID = userID;
            frmRegConf.Type = type;
            frmRegConf.Show();
            this.Hide();
        }
        // handles when the form is loading
        private void frmHome_Load(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            if (db.CheckConf(UserID))
            {
                // if they already registered for a conference, prevent them from changing it
                btnRegConf.Enabled = false;
            }
            else
            {
                // on the other hand, if they aren't registered for a conference don't let them register for workshops
                btnRegWkshp.Enabled = false;
            }
            // if the user does not have chapter permissions disable and remove adminstrative functions
            if (type > 2 || type == 0)
            {
                ParticipantUI();
            }
            // this is not of my own decision, the "user" (which isn't in the specs) made me do this.
            // industry best practices give hierarchal permissions and this is what was done in many of
            // the other programs that were also for this quarter.
            else
            {
                lblRegister.Enabled = false;
                btnRegConf.Enabled = false;
                btnRegWkshp.Enabled = false;
            }
        }
        // handles when the register for workshop button is clicked 
        private void btnRegWkshp_Click(object sender, EventArgs e)
        {
            frmRegWkshp frmRegWkshp = new frmRegWkshp();
            frmRegWkshp.UserID = userID;
            frmRegWkshp.Type = type;
            frmRegWkshp.Show();
            this.Hide();
        }
        // method that handles restricting UI to only participant functions
        private void ParticipantUI()
        {
            this.Height = 250;
            lblCreate.Enabled = false;
            lblCreate.Visible = false;
            btnCreateUser.Enabled = false;
            btnCreateUser.Visible = false;
            btnCreateWkshp.Enabled = false;
            btnCreateWkshp.Visible = false;
        }
        // when user clicks Report button
        private void btnReport_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            frmReport.UserID = userID;
            frmReport.Type = type;
            frmReport.Show();
            this.Hide();
        }
        // when user clicks Profile button
        private void btnProfile_Click(object sender, EventArgs e)
        {
            frmProfile frmProfile = new frmProfile();
            frmProfile.UserID = userID;
            frmProfile.Type = type;
            frmProfile.Show();
            this.Hide();
        }
    }
}
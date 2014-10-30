/* frmProfile.cs
 * This manages the events and data fetching for displaying
 * the records associated with a user profile
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
    public partial class frmProfile : Form
    {
        private int type;
        private string userID;
        private string userName;
        // storing the type and userID throughout the program
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
        public frmProfile()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmHome frmHome = new frmHome();
            frmHome.Type = type;
            frmHome.UserID = userID;
            frmHome.Show();
            this.Hide();
        }

        private void frmProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        // when the user wants to delete their account
        private void btnDeleteAcct_Click(object sender, EventArgs e)
        {
            // make really really sure that they want to actually do this
            var firstPrompt = MessageBox.Show("Are you really sure you want to delete your account?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (firstPrompt == DialogResult.Yes)
            {
                var secondPrompt = MessageBox.Show("This is permanent! Last chance to turn back.", "REALLY SURE?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (secondPrompt == DialogResult.Yes)
                {
                    DBConnect db = new DBConnect();
                    db.DeleteUser(userID);
                    MessageBox.Show("All data tied to this account had been deleted", "Deletion successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void frmProfile_Load(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            string[] userInfo = db.FetchUserData(userID);
            lblFirstName.Text = userInfo[0];
            lblLastName.Text = userInfo[1];
            string[] types = {"Über Admin", "Chapter head", "Adviser", "Member", "Guest"};
            lblType.Text = types[type - 1];
            lblConf.Text = userInfo[2];
            lblChapNum.Text = userInfo[3];
            userName = db.FetchUserName(userID);
            lblUser.Text = userName;
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            frmChangePasswd frmChangePasswd = new frmChangePasswd();
            frmChangePasswd.UserName = userName;
            frmChangePasswd.Show();
        }
    }
}

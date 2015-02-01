/* frmLogin.cs
 * This handles the input of the username and password and serves 
 * as the authentication gateway for the FBLA Desktop app.
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
    public partial class frmLogin : Form
    {
        private string userID;
        // public accessor for ID
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
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // get form values
            string userName = txtUser.Text;
            string password = txtPassword.Text;
            // setup database connector
            DBConnect db = new DBConnect();
            // get uuid of user
            userID = db.LoginUser(userName, password);
            // make sure they actually filled in the fields
            if(userName == "" || password == ""){
                MessageBox.Show("You need to input both a username and password", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // see if their password and username match up
            else if (userID != "")
            {
                // find out what permissions the user has
                Participant part = db.GetParticipant(userID);
                frmHome frmHome = new frmHome();
                // push Participant to next form, so users data moves with them
                frmHome.Part = part;
                frmHome.Show();
                // can't close this otherwise whole app closes
                this.Hide();
            }
            // if failed tell them that they got the user/password wrong
            else
            {
                MessageBox.Show("Wrong username or password", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

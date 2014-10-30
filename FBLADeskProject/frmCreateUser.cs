/* frmCreateUser.cs
 * Handles all the necessary data validation and forwarding to create
 * a user in the database as well as a participant
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
    public partial class frmCreateUser : Form
    {
        public int userType = 0;
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
        public frmCreateUser()
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

        private void frmCreateUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // get username and check if it exists
            string username = txtUser.Text;
            DBConnect db = new DBConnect();
            bool userExists = db.CheckUser(username);
            // make sure all fields are filled in
            if(txtChapNum.Text == "" || txtFirstName.Text == "" || txtLastName.Text == "" || txtPasswd.Text == "" 
                || txtUser.Text == "" || txtRepeat.Text == "" || userType == 0){
                MessageBox.Show("Sorry all fields must be filled in", "Error");
            }
            // make sure passwords match
            else if (txtPasswd.Text != txtRepeat.Text)
            {
                MessageBox.Show("Password fields must match", "Error");
            }
            // if user does exist do not allow the creation of a duplicate
            else if (userExists)
            {
                MessageBox.Show("Sorry username taken, please try a different one", "Error");
            }
            // if everything looks good allow the user to be created
            else
            {
                // assign all the rest of the inputs strings
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                int chapNum = int.Parse(txtChapNum.Text);
                string password = txtPasswd.Text;

                // create the participant and then the user in the database
                string partid = db.CreatePart(userType, firstName, lastName, chapNum);
                bool success = db.CreateUser(partid, username, password);
                // tell the admin it was successful
                if (success)
                {
                    var prompt = MessageBox.Show("User created successfully, create another?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    // see if the admin wants to make another user
                    if (prompt == DialogResult.Yes)
                    {
                        // if so reset the form
                        ResetForm();
                    }
                    else
                    {
                        // if not then close the form and bring them back to the home screen
                        frmHome frmHome = new frmHome();
                        frmHome.Type = type;
                        frmHome.UserID = userID;
                        frmHome.Show();
                        this.Hide();
                    }
                }
                // tells the admin something went wrong creating the user/participant
                else
                {
                    MessageBox.Show("Sorry something went wrong", "Error");
                }
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* gets index selected and then add 2 to match up with database
             * 1 - über admin
             * 2 - Chapter head
             * 3 - Adviser
             * 4 - Member
             * 5 - Guest
             */
            if (type >= 2)
            {
                userType = cmbType.SelectedIndex + 3;
            }
            else
            {
                userType = cmbType.SelectedIndex + 2;
            }
        }
        // resets all the input boxes on the form
        private void ResetForm()
        {
            txtFirstName.ResetText();
            txtLastName.ResetText();
            txtChapNum.ResetText();
            cmbType.SelectedIndex = -1;
            txtUser.ResetText();
            txtPasswd.ResetText();
            txtRepeat.ResetText();
            txtFirstName.Focus();
        }

        private void frmCreateUser_Load(object sender, EventArgs e)
        {
            // make sure chapter users cannot create other chapter users
            if (type >= 2)
            {
                cmbType.Items.RemoveAt(0);
            }
        }
    }
}

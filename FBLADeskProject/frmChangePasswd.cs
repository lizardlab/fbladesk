/* frmChangePasswd.cs
 * This small dialog makes sure that only the authorized user is changing their password
 * by forcing them to reauthenticate and then will change their password and also the 
 * respective salt and hash as well for maximum security.
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
    public partial class frmChangePasswd : Form
    {
        private string userName;
        public string UserName
        {
            set
            {
                userName = value;
            }
        }
        public frmChangePasswd()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            string test = db.LoginUser(userName, txtOldPasswd.Text);
            if (txtNewPasswd.Text == "" || txtOldPasswd.Text == "" || txtRepeatPasswd.Text == "")
            {
                MessageBox.Show("All fields must be filled in", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (test == "")
            {
                MessageBox.Show("Wrong password", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtNewPasswd.Text != txtRepeatPasswd.Text)
            {
                MessageBox.Show("Passwords do not match", "No match", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool success = db.ChangePass(test, txtNewPasswd.Text);
                if (success)
                {
                    MessageBox.Show("Password successfully changed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("An error occurred and your password was not updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

/* frmCreateWkshp.cs
 * This handles all the events within the create workshop form
 * and also send to the database connector all the validated data
 * to create a workshop.
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
    public partial class frmCreateWkshp : Form
    {
        private int type;
        private string userID;
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
        public frmCreateWkshp()
        {
            InitializeComponent();
        }
        // when a conference is selected make sure they can only specify respective date
        private void cmbConfCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // gets the index of the item selected
            int conf = cmbConfCode.SelectedIndex;
            // makes sure that they can only specify a date during the conference
            // New Orleans conference (NLA)
            if (conf == 0)
            {
                try
                {
                    datePicker.MaxDate = new DateTime(2014, 11, 22);
                    datePicker.MinDate = new DateTime(2014, 11, 21);
                }
                // sometimes the date gets out of range so just set the other value first
                catch (ArgumentOutOfRangeException)
                {
                    datePicker.MinDate = new DateTime(2014, 11, 21);
                    datePicker.MaxDate = new DateTime(2014, 11, 22);
                }
            }
            // Washington DC conference (WDC)
            else if (conf == 1)
            {
                try
                {
                    datePicker.MaxDate = new DateTime(2014, 11, 8);
                    datePicker.MinDate = new DateTime(2014, 11, 7);
                }
                // sometimes the date gets out of range so just set the other value first
                catch (ArgumentOutOfRangeException)
                {
                    datePicker.MinDate = new DateTime(2014, 11, 7);
                    datePicker.MaxDate = new DateTime(2014, 11, 8);
                }
            }
            // Minneapolis conference (MMN)
            else
            {
                try
                {
                    datePicker.MaxDate = new DateTime(2014, 11, 15);
                    datePicker.MinDate = new DateTime(2014, 11, 14);
                }
                // sometimes the date gets out of range so just set the other value first
                catch (ArgumentOutOfRangeException)
                {
                    datePicker.MinDate = new DateTime(2014, 11, 14);
                    datePicker.MaxDate = new DateTime(2014, 11, 15);
                }
            }
            // Once the correct date range is set, let user choose
            datePicker.Enabled = true;
            timePicker.Enabled = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string confcode = "";
            string wname = txtName.Text;
            string wdesc = txtDescription.Text;
            switch (cmbConfCode.SelectedIndex){
                case 0:
                    confcode = "NLA";
                    break;
                case 1:
                    confcode = "WDC";
                    break;
                case 2:
                    confcode = "MMN";
                    break;
                default:
                    MessageBox.Show("Error", "Something went wrong with selecting the conference");
                    break;
            }
            if (txtName.Text == null || txtDescription.Text == null || confcode == "")
            {
                MessageBox.Show("Sorry all fields must be filled in", "Error");
            }
            string startDate = datePicker.Value.ToString("yyyy-MM-dd");
            string startTime = timePicker.Value.ToString("HH:mm:ss");
            startDate = startDate + " "+ startTime;
            DBConnect db = new DBConnect();
            bool success = db.CreateWkshp(wname, wdesc, startDate, confcode);
            if (success)
            {
                var prompt = MessageBox.Show("Workshop created successfully, create another?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                // see if the admin wants to make another workshop
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
            else
            {
                MessageBox.Show("Sorry something went wrong", "Error");
            }
        }
        // handles when the user clicks the cancel button sends them back to home screen
        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmHome frmHome = new frmHome();
            frmHome.Type = type;
            frmHome.UserID = userID;
            frmHome.Show();
            this.Hide();
        }
        // handles if the user clicks the red X and closes the program
        private void frmCreateWkshp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        // resets the input boxes on form
        private void ResetForm()
        {
            txtName.ResetText();
            txtDescription.ResetText();
            cmbConfCode.SelectedIndex = -1;
            datePicker.ResetText();
            timePicker.ResetText();
            timePicker.Enabled = false;
            datePicker.Enabled = false;
            txtName.Focus();
        }
    }
}

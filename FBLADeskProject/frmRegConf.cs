﻿/* frmRegConference
 * This form allows the user to register for a conference only once
 * to prevent any data ivalidation from changing conferences but keeping
 * workshops.
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
    public partial class frmRegConf : Form
    {
        public string confCode = "";
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
        public frmRegConf()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmHome frmHome = new frmHome();
            frmHome.Part = part;
            frmHome.Show();
            this.Hide();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (confCode != "")
            {
                DBConnect db = new DBConnect();
                db.RegConf(part.UUID, confCode);
                frmHome frmHome = new frmHome();
                frmHome.Part = part;
                frmHome.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Select a conference");
            }
        }

        private void cmbConf_SelectedIndexChanged(object sender, EventArgs e)
        {
            int conf = cmbConf.SelectedIndex;
            switch (conf)
            {
                case -1:
                    lblDescription.Text = "Select a conference";
                    break;
                case 0:
                    lblDescription.Text = "New Orleans, Louisiana (NLA)\nNovember 21-22";
                    confCode = "NLA";
                    break;
                case 1:
                    lblDescription.Text = "Washington, District of Columbia (WDC)\nNovember 7-8";
                    confCode = "WDC";
                    break;
                default:
                    lblDescription.Text = "Minneapolis, Minnesota (MMN)\nNovember 14-15";
                    confCode = "MMN";
                    break;
            }
        }

        private void frmRegConf_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

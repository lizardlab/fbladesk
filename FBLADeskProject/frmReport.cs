/* frmReport.cs
 * Handles the event for the Report form and also 
 * creates the PDF reports within it.
 * Programmer: Logan Lopez
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace FBLADeskProject
{
    public partial class frmReport : Form
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
        public frmReport()
        {
            InitializeComponent();
        }
        // event handler for Conference button
        private void btnConfReport_Click(object sender, EventArgs e)
        {
            // open up a dialog to ask the user where to save the file
            SaveFileDialog fd = new SaveFileDialog();
            // only allow them to save it as a PDF
            fd.Filter = "Portable Document Format (*.pdf)|*.pdf";
            string fileName = "";
            // initialize the conference values for the combobox
            string[] confs = { "NLA", "WDC", "MMN" };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                // set the file name equal to whatever they specified in the dialog
                fileName = fd.FileName;
                // start the file stream for writing to disk
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                // set all the metadata and basic document information
                Document confReport = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(confReport, fs);
                confReport.Open();
                confReport.AddTitle("Conference Report - " + confs[cmbConf.SelectedIndex]);
                confReport.AddSubject("FBLA Desktop App Report");
                confReport.AddKeywords("Report, Conference, FBLA, PBL");
                confReport.AddCreator("FBLA Desktop App");
                confReport.AddAuthor("FBLA");
                // add a title for the report
                Font headerFont = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                confReport.Add(new Paragraph("Conference Report – " + confs[cmbConf.SelectedIndex], headerFont));
                // add the table in for the report
                PdfPTable report = new PdfPTable(4);
                // make sure it starts a bit after so it doesn't overlap with the title
                report.SpacingBefore = 20f;
                // create the header row
                string[] headerTitles = { "Last name", "First name", "Type", "Chapter number" };
                Font tableTitleFont = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.WHITE);
                foreach (string title in headerTitles)
                {
                    // changing color and font helps make it distinguishable as header
                    PdfPCell cell = new PdfPCell(new Phrase(title, tableTitleFont));
                    cell.BackgroundColor = BaseColor.BLUE;
                    cell.Border = 0;
                    report.AddCell(cell);
                }
                DBConnect db = new DBConnect();
                // get the conference attendees for the selected conference
                List<string>[] dataTable = db.GetConferenceAttendees(confs[cmbConf.SelectedIndex]);
                for (int i = 0; i < dataTable[0].Count; i++)
                {
                    for (int c = 0; c < dataTable.GetLength(0); c++)
                    {
                        // go through the List and then add each value as a new cell
                        report.AddCell(dataTable[c][i]);
                    }
                }
                // add the generated table to the report
                confReport.Add(report);
                // close the document and then write it to disk
                confReport.Close();
                // display document to report viewer
                ReportViewer.LoadFile(fileName);
            }
        }
        // close when red X is clicked
        private void frmReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        // go back to home screen if cancel or ESC is clicked
        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmHome frmHome = new frmHome();
            frmHome.UserID = userID;
            frmHome.Type = type;
            frmHome.Show();
            this.Hide();
        }
        // restrict the user to only allow reports for their permission level
        private void frmReport_Load(object sender, EventArgs e)
        {
            // if th admin allow them to do the master reports
            if (type == 1)
            {
                cmbConf.Enabled = true;
            }
            // if a chapter head allow them to do workshop reports only
            else if (type == 2)
            {
                cmbConf.Enabled = true;
                btnConfReport.Visible = false;
                btnConfReport.Enabled = false;
            }
            // if a participant only allow them to print out their schedule
            else
            {
                btnSchedule.Enabled = true;
            }
        }
        // enable the button for conference report once a conference is selected
        private void cmbConf_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnConfReport.Enabled = true;
            btnWkshpReport.Enabled = true;
        }
        // when workshop report button is clicked
        private void btnWkshpReport_Click(object sender, EventArgs e)
        {
            // open up a dialog to ask the user where to save the file
            SaveFileDialog fd = new SaveFileDialog();
            // only allow them to save it as a PDF
            fd.Filter = "Portable Document Format (*.pdf)|*.pdf";
            string fileName = "";
            // initialize the conference values for the combobox
            string[] confs = { "NLA", "WDC", "MMN" };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                // set the file name equal to whatever they specified in the dialog
                fileName = fd.FileName;
                // start the file stream for writing to disk
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                // set all the metadata and basic document information
                Document wkshpReport = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(wkshpReport, fs);
                wkshpReport.Open();
                wkshpReport.AddTitle("Workshop Report - " + confs[cmbConf.SelectedIndex]);
                wkshpReport.AddSubject("FBLA Desktop App Report");
                wkshpReport.AddKeywords("Report, Workshop, FBLA, PBL");
                wkshpReport.AddCreator("FBLA Desktop App");
                wkshpReport.AddAuthor("FBLA");
                // add a title for the report
                Font headerFont = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                wkshpReport.Add(new Paragraph("Workshop Report – " + confs[cmbConf.SelectedIndex], headerFont));
                wkshpReport.Add(new Paragraph("This page is blank as it is the cover page"));
                // create the header row
                string[] headerTitles = { "Last name", "First name", "Chapter number" };
                Font tableTitleFont = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.WHITE);
                DBConnect db = new DBConnect();
                // get list of workshops
                List<string>[] workshops = db.GetWorkshops(confs[cmbConf.SelectedIndex]);
                int wkshpsCount = workshops[0].Count;
                string wname, wdesc, startime;
                // create a page for each workshop
                for (int i = 0; i < wkshpsCount; i++)
                {
                    wkshpReport.NewPage();
                    // add the table in for the report
                    PdfPTable report = new PdfPTable(3);
                    // make sure it starts a bit after so it doesn't overlap with the title
                    report.SpacingBefore = 20f;
                    foreach (string title in headerTitles)
                    {
                        // changing color and font helps make it distinguishable as header
                        PdfPCell cell = new PdfPCell(new Phrase(title, tableTitleFont));
                        cell.BackgroundColor = BaseColor.GREEN;
                        cell.Border = 0;
                        report.AddCell(cell);
                    }
                    wname = workshops[0][i];
                    wdesc = workshops[1][i];
                    startime = workshops[2][i];
                    wkshpReport.Add(new Paragraph("Workshop name: " + wname));
                    wkshpReport.Add(new Paragraph("Workshop description: " + wdesc));
                    wkshpReport.Add(new Paragraph("Workshop start time: " + startime));
                    // get attendees for workshop
                    List<string>[] attendees = db.GetRegistrations(workshops[3][i]);
                    for (int c = 0; c < attendees[0].Count; c++)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            // then add their details to the table
                            report.AddCell(attendees[a][c]);
                        }
                    }
                    // add the generated table to the report
                    wkshpReport.Add(report);
                }
                // close the document and then write it to disk
                wkshpReport.Close();
                // display document to report viewer
                ReportViewer.LoadFile(fileName);
            }
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            // open up a dialog to ask the user where to save the file
            SaveFileDialog fd = new SaveFileDialog();
            // only allow them to save it as a PDF
            fd.Filter = "Portable Document Format (*.pdf)|*.pdf";
            string fileName = "";
            // initialize the conference values for the combobox
            string[] confs = { "NLA", "WDC", "MMN" };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                // set the file name equal to whatever they specified in the dialog
                fileName = fd.FileName;
                // start the file stream for writing to disk
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                // set all the metadata and basic document information
                Document scheduleReport = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(scheduleReport, fs);
                scheduleReport.Open();
                scheduleReport.AddTitle("Schedule");
                scheduleReport.AddSubject("FBLA Desktop App Report");
                scheduleReport.AddKeywords("Report, schedule, FBLA, PBL");
                scheduleReport.AddCreator("FBLA Desktop App");
                scheduleReport.AddAuthor("FBLA");
                // add a title for the report
                Font headerFont = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                scheduleReport.Add(new Paragraph("Schedule", headerFont));
                // add the table in for the report
                PdfPTable report = new PdfPTable(3);
                // make sure it starts a bit after so it doesn't overlap with the title
                report.SpacingBefore = 20f;
                // create the header row
                string[] headerTitles = { "Start time", "Name", "Description" };
                Font tableTitleFont = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.WHITE);
                DBConnect db = new DBConnect();
                List<string>[] schedule = db.GetSchedule(userID);
                foreach (string title in headerTitles)
                {
                    // changing color and font helps make it distinguishable as header
                    PdfPCell cell = new PdfPCell(new Phrase(title, tableTitleFont));
                    cell.BackgroundColor = BaseColor.ORANGE;
                    cell.Border = 0;
                    report.AddCell(cell);
                }
                for (int i = 0; i < schedule[0].Count; i++)
                {
                    for (int c = 0; c < schedule.GetLength(0); c++)
                    {
                        // go through the List and then add each value as a new cell
                        report.AddCell(schedule[c][i]);
                    }
                }
                scheduleReport.Add(report);
                // close the document and then write it to disk
                scheduleReport.Close();
                // display document to report viewer
                ReportViewer.LoadFile(fileName);
            }
        }
    }
}

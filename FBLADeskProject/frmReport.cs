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
                List<Participant> participants = db.GetConferenceAttendees(confs[cmbConf.SelectedIndex]);
                for (int i = 0; i < participants.Count; i++)
                {
                    // go through the List and then add each value as a new cell
                    report.AddCell(participants[i].LastName);
                    report.AddCell(participants[i].FirstName);
                    report.AddCell(participants[i].TypeString);
                    report.AddCell(participants[i].Chapter.ToString());
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
            frmHome.Part = part;
            frmHome.Show();
            this.Hide();
        }
        // restrict the user to only allow reports for their permission level
        private void frmReport_Load(object sender, EventArgs e)
        {
            // if th admin allow them to do the master reports
            if (part.Type == 1)
            {
                cmbConf.Enabled = true;
            }
            // if a chapter head allow them to do workshop reports only
            else if (part.Type == 2)
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
                List<Workshop> workshops = db.GetWorkshops(confs[cmbConf.SelectedIndex]);
                string wname, wdesc;
                DateTime startDate;
                // create a page for each workshop
                foreach (Workshop wkshp in workshops)
                {
                    wkshpReport.NewPage();
                    // add the table in for the report
                    PdfPTable report = new PdfPTable(3);
                    // make sure it starts a bit after so it doesn't overlap with the title
                    report.SpacingBefore = 20f;
                    report.HeaderRows = 1;
                    foreach (string title in headerTitles)
                    {
                        // changing color and font helps make it distinguishable as header
                        PdfPCell cell = new PdfPCell(new Phrase(title, tableTitleFont));
                        cell.BackgroundColor = BaseColor.GREEN;
                        cell.Border = 0;
                        report.AddCell(cell);
                    }
                    wname = wkshp.Name;
                    wdesc = wkshp.Description;
                    startDate = wkshp.Date;
                    wkshpReport.Add(new Paragraph("Workshop name: " + wname));
                    wkshpReport.Add(new Paragraph("Workshop description: " + wdesc));
                    wkshpReport.Add(new Paragraph("Workshop start time: " + startDate.ToString("yyyy-MM-dd HH:mm:ss")));
                    // get attendees for workshop
                    List<Participant> attendees = db.GetRegistrations(wkshp.UUID);
                    foreach (Participant part in attendees)
                    {
                        // then add their details to the table
                        report.AddCell(part.LastName);
                        report.AddCell(part.FirstName);
                        report.AddCell(part.Chapter.ToString());
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
                List<Workshop> schedule = db.GetSchedule(part.UUID);
                report.HeaderRows = 1;
                foreach (string title in headerTitles)
                {
                    // changing color and font helps make it distinguishable as header
                    PdfPCell cell = new PdfPCell(new Phrase(title, tableTitleFont));
                    cell.BackgroundColor = BaseColor.ORANGE;
                    cell.Border = 0;
                    report.AddCell(cell);
                }
                foreach (Workshop wkshp in schedule)
                {
                    report.AddCell(wkshp.Date.ToShortDateString());
                    report.AddCell(wkshp.Name);
                    report.AddCell(wkshp.Description);
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

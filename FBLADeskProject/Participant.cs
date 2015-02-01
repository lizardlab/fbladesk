using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBLADeskProject
{
    class Participant
    {
        private string uuid, fname, lname, conf;
        private int type, chapter;
        public Participant(string uuid, string fname, string lname, int type, int chapter, string conf)
        {
            this.uuid = uuid;
            this.fname = fname;
            this.lname = lname;
            this.type = type;
            this.chapter = chapter;
            this.conf = conf;
        }
        public Participant(string fname, string lname, int type, int chapter)
        {
            this.fname = fname;
            this.lname = lname;
            this.type = type;
            this.chapter = chapter;
        }
        public Participant(string fname, string lname, int chapter)
        {
            this.fname = fname;
            this.lname = lname;
            this.chapter = chapter;
        }
        public string TypeString
        {
            get{
                string[] types = { "Über Admin", "Chapter Head", "Adviser", "Member", "Guest" };
                return types[type];
            }
        }
        public string UUID
        {
            get
            {
                return uuid;
            }
        }
        public string FirstName
        {
            get
            {
                return fname;
            }
        }
        public string LastName
        {
            get
            {
                return lname;
            }
        }
        public int Type
        {
            get
            {
                return type;
            }
        }
        public int Chapter
        {
            get
            {
                return chapter;
            }
        }
    }
}

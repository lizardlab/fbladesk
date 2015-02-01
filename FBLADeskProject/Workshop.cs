using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBLADeskProject
{
    class Workshop
    {
        private string uuid, conf, name, description;
        private DateTime startDate;
        public Workshop(string uuid, string conf, string name, string description, DateTime startDate)
        {
            this.uuid = uuid;
            this.conf = conf;
            this.name = name;
            this.description = description;
            this.startDate = startDate;
        }
        public Workshop(string conf, string name, string description, DateTime startDate)
        {
            this.conf = conf;
            this.name = name;
            this.description = description;
            this.startDate = startDate;
        }
        public Workshop(string name, string description, DateTime startDate)
        {
            this.name = name;
            this.description = description;
            this.startDate = startDate;
        }
        public string UUID
        {
            get
            {
                return uuid;
            }
        }
        public string Conf
        {
            get
            {
                return conf;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
        }
        public DateTime Date
        {
            get
            {
                return startDate;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    /// <summary>
    ///  Halip Vasile Emanuel
    ///  Task model.
    /// </summary>
    public class Task
    {
        private int _id;
        private DateTime _date;   //only the time is needed here
        private string _title;
        private string _subtitle;
        private string _description;
        private string _status;
        private int _priority;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }
    }
}

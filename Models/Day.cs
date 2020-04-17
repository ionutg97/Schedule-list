using System;

namespace Models
{
    public class Day
    {
        private int _id;
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        //sau
        //public DateTime Date { get; set; }
    }
}

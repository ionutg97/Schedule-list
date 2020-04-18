using System;

namespace Models
{
    /// <summary>
    ///  Halip Vasile Emanuel
    ///  Day model.
    /// </summary>
    public class Day
    {
        private int _id;
        private DateTime _date; // only the date is needed here
        private int _task_id;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public int Task_id
        {
            get { return _task_id; }
            set { _task_id = value; }
        }



        //sau
        //public DateTime Date { get; set; }
    }
}

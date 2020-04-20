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
        private  string _date; // format dd.mm.yyyy

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }
    }
}

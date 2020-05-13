/**************************************************************************
 *                                                                        *
 *  File:        Task.cs                                                  *
 *  Copyright:   (c) 2019-2020                                            *
 *                Halip Vasile Emanuel                                    *
 *  Description: Task Shedule - Windows Form Program                      *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

namespace Models
{
    /// <summary>
    ///  Halip Vasile Emanuel
    ///  Task model.
    /// </summary>
    public class Task
    {
        private int _id;
        private string _time;   //format -> hh.mm.ss
        private string _title;
        private string _subtitle;
        private string _description;
        private string _status;
        private int _priority;
        private int _day_id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Time
        {
            get { return _time; }
            set { _time = value; }
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

        public int DayId
        {
            get { return _day_id; }
            set { _day_id = value; }
        }
    }
}

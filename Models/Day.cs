/**************************************************************************
 *                                                                        *
 *  File:        Day.cs                                                   *
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

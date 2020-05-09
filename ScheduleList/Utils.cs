/**************************************************************************
 *                                                                        *
 *  File:        Utils.cs                                                 *
 *  Copyright:   (c) 2019-2020                                            *
 *                Galan Ionut Andrei                                      *
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

namespace ScheduleList
{
    /// <summary>
    /// Utils contains functions used to change input variables from interface.  
    /// </summary>
    class Utils
    {
        /// <summary>
        /// Galan Ionut Andrei
        /// Convert DateTime to Date "dd.MM.yyyy".
        /// </summary>
        /// <param name="dateTime"> DateTime input variable</param>
        /// <returns>string which contain specific data format</returns>
       public static string ConvertDateTimeToString(DateTime dateTime)
        {
            string dateFormat = "";
            dateFormat += dateTime.ToString("dd");
            dateFormat += ".";
            dateFormat += dateTime.ToString("MM");
            dateFormat += ".";
            dateFormat += dateTime.ToString("yyyy");

            return dateFormat;
        }

        /// <summary>
        /// Galan Ionut Andrei
        /// Check if end date is not older than start date.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>true or false</returns>
        public static Boolean validateDate(DateTime start, DateTime end)
        {
            if (start <= end)
                return true;
            else
                return false;
        }

    }
}

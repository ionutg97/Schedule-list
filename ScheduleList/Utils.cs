using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleList
{
    /// <summary>
    /// Utils contains functions used to change input variables from interface.  
    /// </summary>
    class Utils
    {
        /// <summary>
        /// Galan Ionut Andrei
        /// Convert DateTime to Date "dd.MM.yyyy"
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

        public static Boolean validateDate(DateTime start, DateTime end)
        {
            if (start <= end)
                return true;
            else
                return false;
        }

    }
}

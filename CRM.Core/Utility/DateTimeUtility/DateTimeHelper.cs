using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Core.Utility.DateTimeUtility
{
    public class DateTimeHelper
    {
        /// <summary>
        /// Convert DD/MM/YYYY format date into MM/DD/YYYY format
        /// </summary>
        /// <param name="date">String type DD/MM/YYYY format date</param>
        /// <returns>Returns nullable datetime in MM/DD/YYYY format </returns>
        public static DateTime? ConvertToMmDdYyyy(string date)
        {
            try
            {
                if (!(String.IsNullOrEmpty(date) && date.Length > 0))
                {
                    string[] dt = date.Split('/');
                    date = dt[1];           //MM
                    date += "/" + dt[0];    //DD
                    date += "/" + dt[2];    //YYYY
                    return Convert.ToDateTime(date);
                }
                else
                    throw new Exception();
            }
            catch (IndexOutOfRangeException)
            { return null; }
            catch (Exception ex)
            { return null; }
        }
    }
}

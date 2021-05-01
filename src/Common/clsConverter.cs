/*
 * OLKI.Toolbox.Common
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to convert some value formats
 * 
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the LGPL General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * LGPL General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not check the GitHub-Repository.
 * 
 * */

using System;

namespace OLKI.Toolbox.Common
{
    /// <summary>
    /// Class that provides tool to convert
    /// </summary>
    public class Converter
    {
        #region Constants
        /// <summary>
        /// Default return format for DateTimeToNumDateTime
        /// </summary>
        private const string DEFAULT_DATE_TIME_FORMAT = "yyyyMMdd";
        #endregion

        #region Methods
        #region DateTimeToNumDateTime
        /// <summary>
        /// Converts the actual date and time to a numer in fomat yyyyMMddHHmmss
        /// </summary>
        public static long DateTimeToNumDateTime()
        {
            return Converter.DateTimeToNumDateTime(DateTime.Now);
        }
        /// <summary>
        /// Converts the specified DateTime to a numer in fomat yyyyMMddHHmmss
        /// </summary>
        /// <param name="timeValue">DateTime to convert</param>
        /// <returns>DateTime in format Converts the given DateTime to a numer in fomat yyyyMMddHHmmss or 0 if timeValue was not set</returns>
        public static long DateTimeToNumDateTime(Nullable<DateTime> timeValue)
        {
            return Converter.DateTimeToNumDateTime(timeValue, DEFAULT_DATE_TIME_FORMAT);
        }
        /// <summary>
        /// Converts the actual date and time to an specified fomat
        /// </summary>
        /// <param name="format">Return format</param>
        public static long DateTimeToNumDateTime(string format)
        {
            return Converter.DateTimeToNumDateTime(DateTime.Now, format);
        }
        /// <summary>
        /// Converts the specified DateTime to an user specified fomat
        /// </summary>
        /// <param name="timeValue">DateTime to convert</param>
        /// <param name="format">Return format</param>
        public static long DateTimeToNumDateTime(Nullable<DateTime> timeValue, string format)
        {
            if (timeValue.HasValue)
            {
                return Convert.ToInt64(timeValue.Value.ToString(format));
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region NumDateTimeToDate
        /// <summary>
        /// Converts an specified numeric time, fomrated as yyyyMMdd, to an only Date in DateTime format or null if timevalue is 0
        /// </summary>
        /// <param name="timevalue">Value to convert</param>
        /// <returns>Converted Date or NULL if timevalue is 0</returns>
        public static Nullable<DateTime> NumDateTimeToDate(long timevalue)
        {
            return Converter.NumDateTimeToDate(timevalue.ToString());
        }
        /// <summary>
        /// Converts an specified numeric time, fomrated as yyyyMMdd, to an only Date in DateTime format or null if timevalue is 0
        /// </summary>
        /// <param name="timevalue">Value to convert</param>
        /// <returns>Converted DateTime or NULL if timevalue is null or empth</returns>
        public static Nullable<DateTime> NumDateTimeToDate(string timevalue)
        {
            if (string.IsNullOrEmpty(timevalue) || timevalue == "0")
            {
                return null;
            }

            int Y = Convert.ToInt32(timevalue.Substring(0, 4));
            int m = Convert.ToInt32(timevalue.Substring(4, 2));
            int d = Convert.ToInt32(timevalue.Substring(6, 2));
            int h = 0;
            int i = 0;
            int s = 0;

            return new DateTime(Y, m, d, h, i, s);
        }
        #endregion

        #region NumDateTimeToDateTime
        /// <summary>
        /// Converts an specified numeric time, fomrated as yyyyMMddHHmmss, to an DateTime or null if timevalue is 0
        /// </summary>
        /// <param name="timevalue">Value to convert</param>
        /// <returns>Converted DateTime or NULL if timevalue is 0</returns>
        public static Nullable<DateTime> NumDateTimeToDateTime(long timevalue)
        {
            return Converter.NumDateTimeToDateTime(timevalue.ToString());
        }
        /// <summary>
        /// Converts an specified numeric time, fomrated as yyyyMMddHHmmss, to an DateTime or null if timevalue is 0
        /// </summary>
        /// <param name="timevalue">Value to convert</param>
        /// <returns>Converted DateTime or NULL if timevalue is null or empth</returns>
        public static Nullable<DateTime> NumDateTimeToDateTime(string timevalue)
        {
            if (string.IsNullOrEmpty(timevalue) || timevalue == "0")
            {
                return null;
            }

            int Y = Convert.ToInt32(timevalue.Substring(0, 4));
            int m = Convert.ToInt32(timevalue.Substring(4, 2));
            int d = Convert.ToInt32(timevalue.Substring(6, 2));
            int h = Convert.ToInt32(timevalue.Substring(8, 2));
            int i = Convert.ToInt32(timevalue.Substring(10, 2));
            int s = Convert.ToInt32(timevalue.Substring(12, 2));

            return new DateTime(Y, m, d, h, i, s);
        }
        #endregion
        #endregion
    }
}
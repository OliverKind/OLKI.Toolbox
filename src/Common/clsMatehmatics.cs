/*
 * OLKI.Toolbox.Common
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides mathematical tools
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
    /// Class that provides mathematical tools
    /// </summary>
    public static class Matehmatics
    {
        #region Constants
        /// <summary>
        /// The number of digits returnd by percentages calculation by default
        /// </summary>
        private const int DEFAULT_PERCENTAGES_DIGITS = 0;
        #endregion

        #region Methodes
        #region Percentage
        /// <summary>
        /// Calculate the percentage value from a diven Quotation and a basic value
        /// </summary>
        /// <param name="percentageQuotation">Specifies the quotation for percent calculation</param>
        /// <param name="basicValue">Specifies the basic value for percent calculation</param>
        /// <returns>The percentage value of a given qoutation and a basic value with none digits</returns>
        public static double Percentages(int percentageQuotation, int basicValue)
        {
            return Percentages(percentageQuotation, basicValue, DEFAULT_PERCENTAGES_DIGITS);
        }
        /// <summary>
        /// Calculate the percentage value from a diven Quotation and a basic value
        /// </summary>
        /// <param name="percentageQuotation">Specifies the quotation for percent calculation</param>
        /// <param name="basicValue">Specifies the basic value for percent calculation</param>
        /// <param name="digits">The number of digits given in result</param>
        /// <returns>The percentage value of a given qoutation and a basic value with an specified number of digits</returns>
        public static double Percentages(int percentageQuotation, int basicValue, int digits)
        {
            return Percentages((double)percentageQuotation, (double)basicValue, digits);
        }
        /// <summary>
        /// Calculate the percentage value from a diven Quotation and a basic value
        /// </summary>
        /// <param name="percentageQuotation">Specifies the quotation for percent calculation</param>
        /// <param name="basicValue">Specifies the basic value for percent calculation</param>
        /// <returns>The percentage value of a given qoutation and a basic value with none digits</returns>
        public static double Percentages(long percentageQuotation, long basicValue)
        {
            return Percentages(percentageQuotation, basicValue, DEFAULT_PERCENTAGES_DIGITS);
        }
        /// <summary>
        /// Calculate the percentage value from a diven Quotation and a basic value
        /// </summary>
        /// <param name="percentageQuotation">Specifies the quotation for percent calculation</param>
        /// <param name="basicValue">Specifies the basic value for percent calculation</param>
        /// <param name="digits">The number of digits given in result</param>
        /// <returns>The percentage value of a given qoutation and a basic value with an specified number of digits</returns>
        public static double Percentages(long percentageQuotation, long basicValue, int digits)
        {
            return Percentages((double)percentageQuotation, (double)basicValue, digits);
        }
        /// <summary>
        /// Calculate the percentage value from a diven Quotation and a basic value
        /// </summary>
        /// <param name="percentageQuotation">Specifies the quotation for percent calculation</param>
        /// <param name="basicValue">Specifies the basic value for percent calculation</param>
        /// <returns>The percentage value of a given qoutation and a basic value with none digits</returns>
        public static double Percentages(decimal percentageQuotation, decimal basicValue)
        {
            return Percentages(percentageQuotation, basicValue, DEFAULT_PERCENTAGES_DIGITS);
        }
        /// <summary>
        /// Calculate the percentage value from a diven Quotation and a basic value
        /// </summary>
        /// <param name="percentageQuotation">Specifies the quotation for percent calculation</param>
        /// <param name="basicValue">Specifies the basic value for percent calculation</param>
        /// <param name="digits">The number of digits given in result</param>
        /// <returns>The percentage value of a given qoutation and a basic value with an specified number of digits</returns>
        public static double Percentages(decimal percentageQuotation, decimal basicValue, int digits)
        {
            return Percentages((double)percentageQuotation, (double)basicValue, digits);
        }
        /// <summary>
        /// Calculate the percentage value from a diven Quotation and a basic value
        /// </summary>
        /// <param name="percentageQuotation">Specifies the quotation for percent calculation</param>
        /// <param name="basicValue">Specifies the basic value for percent calculation</param>
        /// <returns>The percentage value of a given qoutation and a basic value with none digits</returns>
        public static double Percentages(float percentageQuotation, float basicValue)
        {
            return Percentages(percentageQuotation, basicValue, DEFAULT_PERCENTAGES_DIGITS);
        }
        /// <summary>
        /// Calculate the percentage value from a diven Quotation and a basic value
        /// </summary>
        /// <param name="percentageQuotation">Specifies the quotation for percent calculation</param>
        /// <param name="basicValue">Specifies the basic value for percent calculation</param>
        /// <param name="digits">The number of digits given in result</param>
        /// <returns>The percentage value of a given qoutation and a basic value with an specified number of digits</returns>
        public static double Percentages(float percentageQuotation, float basicValue, int digits)
        {
            return Percentages((double)percentageQuotation, (double)basicValue, digits);
        }
        /// <summary>
        /// Calculate the percentage value from a diven Quotation and a basic value
        /// </summary>
        /// <param name="percentageQuotation">Specifies the quotation for percent calculation</param>
        /// <param name="basicValue">Specifies the basic value for percent calculation</param>
        /// <returns>The percentage value of a given qoutation and a basic value with none digits</returns>
        public static double Percentages(double percentageQuotation, double basicValue)
        {
            return Percentages(percentageQuotation, basicValue, DEFAULT_PERCENTAGES_DIGITS);
        }
        /// <summary>
        /// Calculate the percentage value from a diven Quotation and a basic value
        /// </summary>
        /// <param name="percentageQuotation">Specifies the quotation for percent calculation</param>
        /// <param name="basicValue">Specifies the basic value for percent calculation</param>
        /// <param name="digits">The number of digits given in result</param>
        /// <returns>The percentage value of a given qoutation and a basic value with an specified number of digits</returns>
        public static double Percentages(double percentageQuotation, double basicValue, int digits)
        {
            try
            {
                if (percentageQuotation > 0 && basicValue > 0)
                {
                    return System.Math.Round((100 * percentageQuotation / basicValue), digits);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return 0;
            }
        }
        #endregion

        #region Limiter
        /// <summary>
        /// Limitades the diven value to the set upper Limit
        /// </summary>
        /// <param name="value">The value to limit to the set upper Limit</param>
        /// <param name="upperLimit">The upper Limit to limit the value to</param>
        /// <returns>The given value or the upper Limit</returns>
        public static double UpLimit(double value, double upperLimit)
        {
            return value > upperLimit ? upperLimit : value;
        }
        #endregion

        #region Remaining Time
        /// <summary>
        /// Calculates the remaining time for an process, depenting on the elapsed time
        /// </summary>
        /// <param name="elapsedTime">Time elapsed for the actual progress</param>
        /// <param name="progressValue">Actual progress value</param>
        /// <param name="totalValueToFinish">Progress value to finish</param>
        /// <returns>The remining time to process all, or zero if the process values are NULL</returns>
        public static TimeSpan RemainingTime(TimeSpan elapsedTime, long? progressValue, long? totalValueToFinish)
        {
            if (progressValue is null || totalValueToFinish is null) return new TimeSpan();
            return RemainingTime(elapsedTime, (double)progressValue, (double)totalValueToFinish);
        }
        /// <summary>
        /// Calculates the remaining time for an process, depenting on the elapsed time
        /// </summary>
        /// <param name="elapsedTime">Time elapsed for the actual progress</param>
        /// <param name="progressValue">Actual progress value</param>
        /// <param name="totalValueToFinish">Progress value to finish</param>
        /// <returns>The remining time to process all</returns>
        public static TimeSpan RemainingTime(TimeSpan elapsedTime, long progressValue, long totalValueToFinish)
        {
            return RemainingTime(elapsedTime, progressValue, totalValueToFinish, out _);
        }
        /// <summary>
        /// Calculates the remaining time for an process, depenting on the elapsed time
        /// </summary>
        /// <param name="elapsedTime">Time elapsed for the actual progress</param>
        /// <param name="progressValue">Actual progress value</param>
        /// <param name="totalValueToFinish">Progress value to finish</param>
        /// <param name="exception">Exception during calculation</param>
        /// <returns>The remining time to process all</returns>
        public static TimeSpan RemainingTime(TimeSpan elapsedTime, long progressValue, long totalValueToFinish, out Exception exception)
        {
            return RemainingTime(elapsedTime, (double)progressValue, (double)totalValueToFinish, out exception);
        }
        /// <summary>
        /// Calculates the remaining time for an process, depenting on the elapsed time
        /// </summary>
        /// <param name="elapsedTime">Time elapsed for the actual progress</param>
        /// <param name="progressValue">Actual progress value</param>
        /// <param name="totalValueToFinish">Progress value to finish</param>
        /// <returns>The remining time to process all</returns>
        public static TimeSpan RemainingTime(TimeSpan elapsedTime, double progressValue, double totalValueToFinish)
        {
            return RemainingTime(elapsedTime, progressValue, totalValueToFinish, out _);
        }
        /// <summary>
        /// Calculates the remaining time for an process, depenting on the elapsed time
        /// </summary>
        /// <param name="elapsedTime">Time elapsed for the actual progress</param>
        /// <param name="progressValue">Actual progress value</param>
        /// <param name="totalValueToFinish">Progress value to finish</param>
        /// <param name="exception">Exception during calculation</param>
        /// <returns>The remining time to process all</returns>
        public static TimeSpan RemainingTime(TimeSpan elapsedTime, double progressValue, double totalValueToFinish, out Exception exception)
        {
            exception = null;
            try
            {
                return new TimeSpan(0, 0, 0, 0, Convert.ToInt32(elapsedTime.TotalMilliseconds / progressValue * (totalValueToFinish - progressValue)));
            }
            catch (Exception ex)
            {
                exception = ex;
                return new TimeSpan();
            }
        }
        #endregion
        #endregion
    }
}
/*
 * OLKI.Toolbox.Common
 * 
 * Initial Author: Oliver Kind - 2024
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides a writer for csv-Files
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
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OLKI.Toolbox.Common
{
    /// <summary>
    /// Class that provides a writer for csv-Files
    /// </summary>
    public class CSVwriter
    {
        #region Methodes
        /// <summary>
        /// Initial a new CSV-Writer class, with default seperators
        /// </summary>
        public CSVwriter()
        {
        }

        /// <summary>
        /// Write CSV-Data to a file, defined by file path
        /// </summary>
        /// <param name="targetFile">A string that specifies the file path to write CSV-Data to</param>
        /// <param name="fileHead">First line or lines of CSV-File, as header</param>
        /// <param name="dataRows">Data to write to CSV-File</param>
        /// <param name="newline">Newline replacement</param>
        /// <param name="seperator">Column seperator</param>
        /// <returns>True if writing of CSV-Data was successful, otherwise false</returns>
        public bool WriteCSVtoFile(string targetFile, string fileHead, List<List<string>> dataRows, string newline, string seperator)
        {
            return this.WriteCSVtoFile(targetFile, fileHead, dataRows, newline, seperator, out _);
        }

        /// <summary>
        /// Write CSV-Data to a file, defined by file path
        /// </summary>
        /// <param name="targetFile">A string that specifies the file path to write CSV-Data to</param>
        /// <param name="fileHead">First line or lines of CSV-File, as header</param>
        /// <param name="dataRows">Data to write to CSV-File</param>
        /// <param name="newline">Newline replacement</param>
        /// <param name="seperator">Column seperator</param>
        /// <param name="exception">Exception while writing CSV-Data</param>
        /// <returns>True if the reading of CSV-Data was successful, otherwise false</returns>
        public bool WriteCSVtoFile(string targetFile, string fileHead, List<List<string>> dataRows, string newline, string seperator, out Exception exception)
        {
            exception = null;

            StringBuilder DataLineBuilder = new StringBuilder();
            StringBuilder DataSring = new StringBuilder();
            if (!string.IsNullOrEmpty(fileHead)) DataSring.AppendLine(fileHead);

            try
            {
                foreach (List<string> Row in dataRows)
                {
                    DataSring.AppendLine(this.GetDataLine(DataLineBuilder, Row, newline, seperator));
                }
                using (StreamWriter outputFile = new StreamWriter(targetFile, false))
                {
                    outputFile.WriteLine(DataSring.ToString());
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get the data Line
        /// </summary>
        /// <param name="dataLineBuilder">StringBuilder to create a data line</param>
        /// <param name="rowData">Data, to create a data line from</param>
        /// <param name="newline">Newline replacement</param>
        /// <param name="seperator">Column seperator</param>
        /// <returns>Data line for CSV-File</returns>
        private string GetDataLine(StringBuilder dataLineBuilder, List<string> rowData, string newline, string seperator)
        {
            dataLineBuilder.Clear();
            for (int i = 0; i < rowData.Count; i++)
            {
                if (i > 0) dataLineBuilder.Append(seperator);

                string DataItem = rowData[i];
                if (!string.IsNullOrEmpty(newline))
                {
                    DataItem = DataItem.Replace("\r\n", newline);
                }
                DataItem = DataItem.Replace("\"", "\"\"");
                if (DataItem.Contains(seperator))
                {
                    dataLineBuilder.Append("\"");
                    dataLineBuilder.Append(DataItem);
                    dataLineBuilder.Append("\"");
                }
                else
                {
                    dataLineBuilder.Append(DataItem);
                }
            }

            return dataLineBuilder.ToString();
        }
        #endregion
    }
}
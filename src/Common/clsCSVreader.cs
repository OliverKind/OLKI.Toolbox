/*
 * OLKI.Toolbox.Common
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides a reader for csv-Files
 *
 * This code base on code written by CroweMan, 22 June 2010:
 * Original Autor:      CroweMan, 22 June 2010
 * Original Source:     https://www.codeproject.com/Articles/86973/C-CSV-Reader-and-Writer
 * Original Titel:      C# CSV Reader and Writer
 * Original Licence:    The MIT License
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
using System.Linq;
using System.Text;

namespace OLKI.Toolbox.Common
{
    /// <summary>
    /// Class that provides a reader for csv-Files
    /// </summary>
    public class CSVreader
    {
        #region Properties
        /// <summary>
        /// Get or set a list with Seperator Chars. All Chars will used as seperator.
        /// </summary>
        public List<char> Seperators { get; set; } = new List<char> { ';', '\t' };

        /// <summary>
        /// The RAW CSV-File data
        /// </summary>
        private string _rawCSVdata;
        /// <summary>
        /// Get the RAW CSV-File data
        /// </summary>
        public string RawCSVdata => this._rawCSVdata;

        /// <summary>
        /// The Number of rows in the CSV-Data
        /// </summary>
        public int RowCount => this._rows.Count;

        /// <summary>
        /// CSV-Data rows
        /// </summary>
        private readonly List<CSVrow> _rows = new List<CSVrow> { };

        /// <summary>
        /// Get the CSV-Data Rows
        /// </summary>
        public List<CSVrow> CSVrows => this._rows;
        #endregion

        #region Methodes
        /// <summary>
        /// Initial a new CSV-Reader class, with default seperators
        /// </summary>
        public CSVreader() : this(null)
        {

        }

        /// <summary>
        /// Initial a new CSV-Reader class, with user defined seperators
        /// </summary>
        /// <param name="seperators">List with Seperator Chars. All Chars will used as seperator.</param>
        public CSVreader(List<char> seperators)
        {
            if (seperators != null) this.Seperators = seperators;
        }

        /// <summary>
        /// Read CSV-Data from a file, defined by file path
        /// </summary>
        /// <param name="path">A string that specifies the file path to read CSV-Data from</param>
        /// <returns>True if the reading of CSV-Data was successful, otherwise false</returns>
        public bool ReadCSVfromFile(string path)
        {
            return this.ReadCSVfromFile(path, out _);
        }

        /// <summary>
        /// Read CSV-Data from a file, defined by file path
        /// </summary>
        /// <param name="path">A string that specifies the file path to read CSV-Data from</param>
        /// <param name="exception">Exception while reading CSV-Data</param>
        /// <returns>True if the reading of CSV-Data was successful, otherwise false</returns>
        public bool ReadCSVfromFile(string path, out Exception exception)
        {
            return this.ReadCSVfromFile(path, 0, out exception);
        }

        /// <summary>
        /// Read CSV-Data from a file, defined by file path
        /// </summary>
        /// <param name="path">A string that specifies the file path to read CSV-Data from</param>
        /// <param name="firstRow">First row in string with CSV-Data</param>
        /// <returns>True if the reading of CSV-Data was successful, otherwise false</returns>
        public bool ReadCSVfromFile(string path, int firstRow)
        {
            return this.ReadCSVfromFile(path, 0, out _);
        }

        /// <summary>
        /// Read CSV-Data from a file, defined by file path
        /// </summary>
        /// <param name="path">A string that specifies the file path to read CSV-Data from</param>
        /// <param name="firstRow">First row in string with CSV-Data</param>
        /// <param name="exception">Exception while reading CSV-Data</param>
        /// <returns>True if the reading of CSV-Data was successful, otherwise false</returns>
        public bool ReadCSVfromFile(string path, int firstRow,out Exception exception)
        {
            string CsvString;
            try
            {
                CsvString = System.IO.File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
            return this.ReadCSVdata(CsvString, firstRow, out exception);
        }

        /// <summary>
        /// Read CSV-Data from a string
        /// </summary>
        /// <param name="csvString">A string that specifies the CSV-Data</param>
        /// <returns>True if the reading of CSV-Data was successful, otherwise false</returns>
        public bool ReadCSVdata(string csvString)
        {
            return this.ReadCSVdata(csvString, out _);
        }

        /// <summary>
        /// Read CSV-Data from a string
        /// </summary>
        /// <param name="csvString">A string that specifies the CSV-Data</param>
        /// <param name="exception">Exception while reading CSV-Data</param>
        /// <returns>True if the reading of CSV-Data was successful, otherwise false</returns>
        public bool ReadCSVdata(string csvString, out Exception exception)
        {
            return this.ReadCSVdata(csvString, 0, out exception);
        }

        /// <summary>
        /// Read CSV-Data from a string
        /// </summary>
        /// <param name="csvString">A string that specifies the CSV-Data</param>
        /// <param name="firstRow">First row in string with CSV-Data</param>
        /// <returns>True if the reading of CSV-Data was successful, otherwise false</returns>
        public bool ReadCSVdata(string csvString, int firstRow)
        {
            return this.ReadCSVdata(csvString, firstRow, out _);
        }

        /// <summary>
        /// Read CSV-Data from a string
        /// </summary>
        /// <param name="csvString">A string that specifies the CSV-Data</param>
        /// <param name="firstRow">First row in string with CSV-Data</param>
        /// <param name="exception">Exception while reading CSV-Data</param>
        /// <returns>True if the reading of CSV-Data was successful, otherwise false</returns>
        public bool ReadCSVdata(string csvString, int firstRow, out Exception exception)
        {
            exception = null;
            this._rawCSVdata = csvString;

            this._rawCSVdata = this._rawCSVdata.Replace("\r\n", "\n");
            this._rawCSVdata = this._rawCSVdata.Replace("\r", "\n");

            this._rows.Clear();
            List<string> CSVrawRows = this._rawCSVdata.Split('\n').ToList();

            for (int i = firstRow; i < CSVrawRows.Count; i++)
            {
                if (!string.IsNullOrEmpty(CSVrawRows[i]))
                {
                    this._rows.Add(new CSVrow(CSVrawRows[i], this.Seperators));
                }
            }
            return true;
        }
        #endregion

        #region Subclasses
        /// <summary>
        /// A class that specifies a row with CSV-Data
        /// </summary>
        public class CSVrow
        {
            /// <summary>
            /// Get the RAW CSV-Row data
            /// </summary>
            public string RawDate { get; }

            /// <summary>
            /// CSV-Data Columns
            /// </summary>
            public readonly List<string> Columns = new List<string> { };

            /// <summary>
            /// Read the CSV-Data from an RAW-String to an CSV-Row List
            /// </summary>
            /// <param name="rawData">RAW-CSV Data</param>
            /// <param name="seperator">A list with Seperator Chars. All Chars will used as seperator.</param>
            internal CSVrow(string rawData, List<char> seperator)
            {
                this.RawDate = rawData;

                char Char;
                bool InColumn = false;
                bool InQoutes = false;
                StringBuilder ColData = new StringBuilder();

                for (int Pos = 0; Pos < this.RawDate.Length; Pos++)
                {
                    Char = this.RawDate[Pos];

                    if (!InColumn)
                    {
                        if (seperator.Contains(Char))
                        {
                            this.Columns.Add("");
                            continue;
                        }

                        // If the current character is a double quote then the column value is contained within
                        // double quotes, otherwise append the next character
                        if (Char == '"')
                        {
                            InQoutes = true;
                        }
                        else
                        {
                            ColData.Append(Char);
                        }
                        InColumn = true;
                        continue;
                    }

                    if (InQoutes)
                    {
                        // If the current character is a double quote and the next character is a Seperator or we are at the end of the line
                        // we are now no longer within the column.
                        // Otherwise increment the loop counter as we are looking at an escaped double quote within a column
                        if (Char == '"' && (this.RawDate.Length > (Pos + 1) && seperator.Contains(this.RawDate[Pos + 1]) || ((Pos + 1) == this.RawDate.Length)))
                        {
                            InQoutes = false;
                            InColumn = false;
                            Pos++;
                        }
                        else if (Char == '"' && this.RawDate.Length > (Pos + 1) && this.RawDate[Pos + 1] == '"')
                        {
                            Pos++;  //escaped double Qoutes
                        }
                    }
                    else if (seperator.Contains(Char))
                    {
                        InColumn = false;
                    }

                    // If we are no longer in the column clear the builder and add the column to the list
                    if (!InColumn)
                    {
                        this.Columns.Add(ColData.ToString());
                        ColData.Remove(0, ColData.Length);
                    }
                    else
                    {
                        ColData.Append(Char);
                    }
                }

                // If we are still inside a column add a new one
                if (InColumn)
                {
                    this.Columns.Add(ColData.ToString());
                }
                else if (this.RawDate.Length > 0 && seperator.Contains(this.RawDate[this.RawDate.Length - 1]))
                {
                    this.Columns.Add("");
                }
            }
        }
        #endregion
    }
}
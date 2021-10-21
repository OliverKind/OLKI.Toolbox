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
using System.Threading.Tasks;

namespace OLKI.Toolbox.Common
{
    /// <summary>
    /// Class that provides a reader for csv-Files
    /// </summary>
    public class CSVreader
    {
        #region Properties
        public List<char> Seperator = new List<char> { ';', '\t' };

        private string _rawCSVdata;
        public string RawCSVdata
        {
            get
            {
                return this._rawCSVdata;
            }
        }

        public int RowCount
        {
            get
            {
                return this._rows.Count;
            }
        }

        private readonly List<CSVrow> _rows = new List<CSVrow> { };

        public List<CSVrow> CSVrows
        {
            get
            {
                return this._rows;
            }
        }
        #endregion

        #region Methodes
        public CSVreader() : this(null)
        {

        }

        public CSVreader(List<char> seperator)
        {
            if (seperator != null) this.Seperator = seperator;
        }


        public bool ReadCSVfromFile(string path)
        {
            return this.ReadCSVfromFile(path, out _);
        }

        public bool ReadCSVfromFile(string path, out Exception exception)
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
            return this.ReadCSVdata(CsvString, 0, out exception);
        }

        public bool ReadCSVdata(string csvString)
        {
            return this.ReadCSVdata(csvString, out _);
        }

        public bool ReadCSVdata(string csvString, out Exception exception)
        {
            return this.ReadCSVdata(csvString, 0, out exception);
        }

        public bool ReadCSVdata(string csvString, int firstRow)
        {
            return this.ReadCSVdata(csvString, firstRow, out _);
        }

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
                    this._rows.Add(new CSVrow(CSVrawRows[i], this.Seperator));
                }
            }
            return true;
        }
        #endregion


        public class CSVrow
        {
            public string RawDate { get; }
            public readonly List<string> Columns = new List<string> { };

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
    }
}
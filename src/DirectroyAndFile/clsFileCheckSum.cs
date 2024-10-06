/*
 * OLKI.Toolbox.DirectoryAndFile
 * 
 * Initial Author: Oliver Kind - 2022
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to create File checksums
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

using OLKI.Toolbox.src.DirectroyAndFile;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace OLKI.Toolbox.DirectoryAndFile
{
    /// <summary>
    /// Class that provides tool to handle files
    /// </summary>
    public static class FileCheckSum
    {
        #region Methods
        #region SHA256
        /// <summary>
        /// Get the SHA256-Checksum of a given file
        /// </summary>
        /// <param name="path">A string that specifies the path to a File to create the Cheksum from</param>
        /// <returns>The Checksum of the specified File or a empty string if the Cheksum can't be created</returns>
        public static string SHA256(string path)
        {
            return SHA256(path, System.Security.Cryptography.SHA256.Create());
        }
        /// <summary>
        /// Get the SHA256-Checksum of a given file
        /// </summary>
        /// <param name="path">A string that specifies the path to a File to create the Cheksum from</param>
        /// <param name="sha256">SHA256-Creator</param>
        /// <returns>The Checksum of the specified File or a empty string if the Cheksum can't be created</returns>
        public static string SHA256(string path, SHA256 sha256)
        {
            return SHA256(path, sha256, out _);
        }
        /// <summary>
        /// Get the SHA256-Checksum of a given file
        /// </summary>
        /// <param name="path">A string that specifies the path to a File to create the Cheksum from</param>
        /// <param name="exception">Exception while creating the Cheksum</param>
        /// <returns>The Checksum of the specified File or a empty string if the Cheksum can't be created</returns>
        public static string SHA256(string path, out Exception exception)
        {
            return SHA256(path, System.Security.Cryptography.SHA256.Create(), false, out exception);
        }
        /// <summary>
        /// Get the SHA256-Checksum of a given file
        /// </summary>
        /// <param name="path">A string that specifies the path to a File to create the Cheksum from</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if getting checksum failed</param>
        /// <param name="exception">Exception while creating the Cheksum</param>
        /// <returns>The Checksum of the specified File or a empty string if the Cheksum can't be created</returns>
        public static string SHA256(string path, bool showExceptionMessage, out Exception exception)
        {
            return SHA256(path, System.Security.Cryptography.SHA256.Create(), showExceptionMessage, out exception);
        }
        /// <summary>
        /// Get the SHA256-Checksum of a given file
        /// </summary>
        /// <param name="path">A string that specifies the path to a File to create the Cheksum from</param>
        /// <param name="sha256">SHA256-Creator</param>
        /// <param name="exception">Exception while creating the Cheksum</param>
        /// <returns>The Checksum of the specified File or a empty string if the Cheksum can't be created</returns>
        public static string SHA256(string path, SHA256 sha256, out Exception exception)
        {
            return SHA256(path, sha256, false, out exception);
        }
        /// <summary>
        /// Get the SHA256-Checksum of a given file
        /// </summary>
        /// <param name="path">A string that specifies the path to a File to create the Cheksum from</param>
        /// <param name="sha256">SHA256-Creator</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if getting checksum failed</param>
        /// <param name="exception">Exception while creating the Cheksum</param>
        /// <returns>The Checksum of the specified File or a empty string if the Cheksum can't be created</returns>
        public static string SHA256(string path, SHA256 sha256, bool showExceptionMessage, out Exception exception)
        {
            exception = null;
            try
            {
                using (FileStream FileStream = System.IO.File.OpenRead(path))
                {
                    return Encoding.Default.GetString(sha256.ComputeHash(FileStream));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                if (showExceptionMessage) MessageBox.Show(string.Format(clsFileCheckSumStringtable._0x0001m, new object[] { path, ex.Message }), clsFileCheckSumStringtable._0x0001c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        #endregion
        #endregion
    }
}
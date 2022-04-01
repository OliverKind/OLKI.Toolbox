/*
 * OLKI.Toolbox.DirectoryAndFile
 * 
 * Initial Author: Oliver Kind - 2022
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to create file checksums
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
using System.IO;
using System.Security.Cryptography;
using System.Text;

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
        /// <param name="path">A string that specifies the path to a file to create the Cheksum from</param>
        /// <returns>The Checksum of the specified file or a empty string if the Cheksum can't be created</returns>
        public static string SHA256(string path)
        {
            return SHA256(path, System.Security.Cryptography.SHA256.Create());
        }

        /// <summary>
        /// Get the SHA256-Checksum of a given file
        /// </summary>
        /// <param name="path">A string that specifies the path to a file to create the Cheksum from</param>
        /// <param name="sha256">SHA256-Creator</param>
        /// <returns>The Checksum of the specified file or a empty string if the Cheksum can't be created</returns>
        public static string SHA256(string path, SHA256 sha256)
        {
            return SHA256(path, sha256, out _);
        }

        /// <summary>
        /// Get the SHA256-Checksum of a given file
        /// </summary>
        /// <param name="path">A string that specifies the path to a file to create the Cheksum from</param>
        /// <param name="exception">Exception while creating the Cheksum</param>
        /// <returns>The Checksum of the specified file or a empty string if the Cheksum can't be created</returns>
        public static string SHA256(string path, out Exception exception)
        {
            return SHA256(path, System.Security.Cryptography.SHA256.Create(), out exception);
        }

        /// <summary>
        /// Get the SHA256-Checksum of a given file
        /// </summary>
        /// <param name="path">A string that specifies the path to a file to create the Cheksum from</param>
        /// <param name="sha256">SHA256-Creator</param>
        /// <param name="exception">Exception while creating the Cheksum</param>
        /// <returns>The Checksum of the specified file or a empty string if the Cheksum can't be created</returns>
        public static string SHA256(string path, SHA256 sha256, out Exception exception)
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
                return "";
            }
        }
        #endregion
        #endregion
    }
}
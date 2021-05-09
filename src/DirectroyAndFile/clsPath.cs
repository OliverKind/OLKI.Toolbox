/*
 * OLKI.Toolbox.DirectoryAndFile
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to handle pathes
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

using System.IO;

namespace OLKI.Toolbox.DirectoryAndFile
{
    /// <summary>
    /// Class that provides tool to handle pathes
    /// </summary>
    public static class Path
    {
        #region Methods
        #region Repair
        /// <summary>
        /// Repairs the specified path by removing wrong escape sequences and emend wrong path seperators. Convertes \\ to \ and / to \
        /// </summary>
        /// <param name="path">A string that specifies the path to repair</param>
        /// <returns>The corrected path</returns>
        public static string Repair(string path)
        {
            path = path.Replace(@"/", @"\");
            path = path.Replace(@"\\", @"\");
            return path;
        }
        #endregion

        #region IsDrive
        /// <summary>
        /// Check if the specified path is an drive
        /// </summary>
        /// <param name="path">A string that specifies the path to check if it is an drive</param>
        /// <returns>True if the direcotry is an drive</returns>
        public static bool IsDrive(string path)
        {
            try
            {
                return IsDrive(new DirectoryInfo(path));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if the specified path is an drive
        /// </summary>
        /// <param name="path">An DirectoryInfo that specifies the direcotry  to check if it is an drive</param>
        /// <returns>True if the direcotry is an drive</returns>
        public static bool IsDrive(DirectoryInfo directory)
        {
            try
            {
                return new System.IO.DriveInfo(directory.Root.Name).Name == directory.FullName;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #endregion
    }
}
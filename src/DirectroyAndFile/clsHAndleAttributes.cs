/*
 * OLKI.Toolbox.DirectoryAndFile
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to handle directroy and File attributes
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

namespace OLKI.Toolbox.DirectoryAndFile
{
    /// <summary>
    /// Provides tools to hande Directory and File attributes
    /// </summary>
    public static class HandleAttributes
    {
        /// <summary>
        /// Handle directroy attributes
        /// </summary>
        public static class Direcotry
        {
            #region Methodes
            #region Remove attributes
            /// <summary>
            /// Remove attributes from specified directroy
            /// </summary>
            /// <param name="directory">Directroy to remove the attributes from</param>
            /// <returns>True if attributes where removed sucessfully</returns>
            public static bool Remove(DirectoryInfo directory)
            {
                return HandleAttributes.Direcotry.Remove(directory, out _);
            }

            /// <summary>
            /// Remove attributes from specified directroy and provide exception informations, if attributes can not be removed
            /// </summary>
            /// <param name="directory">Directroy to remove the attributes from</param>
            /// <param name="exception">Exception informations if attribute can not be removed, otherwile NULL</param>
            /// <returns>True if attributes where removed sucessfully</returns>
            public static bool Remove(DirectoryInfo directory, out Exception exception)
            {
                return HandleAttributes.Direcotry.Remove(directory.FullName, out exception);
            }

            /// <summary>
            /// Remove attributes from specified directroy
            /// </summary>
            /// <param name="directory">Directroy to remove the attributes from</param>
            /// <returns>True if attributes where removed sucessfully</returns>
            public static bool Remove(string directory)
            {
                return HandleAttributes.Direcotry.Remove(directory, out _);
            }

            /// <summary>
            /// Remove attributes from specified directroy and provide exception informations, if attributes can not be removed
            /// </summary>
            /// <param name="directory">Directroy to remove the attributes from</param>
            /// <param name="exception">Exception informations if attribute can not be removed, otherwile NULL</param>
            /// <returns>True if attributes where removed sucessfully</returns>
            public static bool Remove(string directory, out Exception exception)
            {
                exception = null;
                try
                {
                    System.IO.File.SetAttributes(directory, FileAttributes.Directory);
                    return true;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    return false;
                }
            }
            #endregion

            #region Set attributes
            /// <summary>
            /// Set attributes to specified directroy
            /// </summary>
            /// <param name="directory">Directroy to set the attributes</param>
            /// <param name="attributes">Attributes to set to the Directory</param>
            /// <returns>True if attributes where set sucessfully</returns>
            public static bool Set(DirectoryInfo directory, FileAttributes attributes)
            {
                return HandleAttributes.Direcotry.Set(directory, attributes, out _);
            }

            /// <summary>
            /// Set attributes to specified directroy and provide exception informations, if attributes can not be removed
            /// </summary>
            /// <param name="directory">Directroy to set the attributes</param>
            /// <param name="attributes">Attributes to set to the Directory</param>
            /// <param name="exception">Exception informations if attribute can not be set, otherwile NULL</param>
            /// <returns>True if attributes where set sucessfully</returns>
            public static bool Set(DirectoryInfo directory, FileAttributes attributes, out Exception exception)
            {
                return HandleAttributes.Direcotry.Set(directory.FullName, attributes, out exception);
            }

            /// <summary>
            /// Set attributes to specified directroy
            /// </summary>
            /// <param name="directory">Directroy to set the attributes</param>
            /// <param name="attributes">Attributes to set to the Directory</param>
            /// <returns>True if attributes where set sucessfully</returns>
            public static bool Set(string directory, FileAttributes attributes)
            {
                return HandleAttributes.Direcotry.Set(directory, attributes, out _);
            }

            /// <summary>
            /// Set attributes to specified directroy and provide exception informations, if attributes can not be removed
            /// </summary>
            /// <param name="directory">Directroy to set the attributes</param>
            /// <param name="attributes">Attributes to set to the Directory</param>
            /// <param name="exception">Exception informations if attribute can not be set, otherwile NULL</param>
            /// <returns>True if attributes where set sucessfully</returns>
            public static bool Set(string directory, FileAttributes attributes, out Exception exception)
            {
                exception = null;
                try
                {
                    System.IO.File.SetAttributes(directory, attributes);
                    return true;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    return false;
                }
            }
            #endregion
            #endregion
        }

        /// <summary>
        /// Handle File attributes
        /// </summary>
        public static class File
        {
            #region Methodes
            #region Remove attributes
            /// <summary>
            /// Remove attributes from specified file
            /// </summary>
            /// <param name="file">File to remove the attributes from</param>
            /// <returns>True if attributes where removed sucessfully</returns>
            public static bool Remove(FileInfo file)
            {
                return HandleAttributes.File.Remove(file, out _);
            }

            /// <summary>
            /// Remove attributes from specified File and provide exception informations, if attributes can not be removed
            /// </summary>
            /// <param name="file">File to remove the attributes from</param>
            /// <param name="exception">Exception informations if attribute can not be removed, otherwile NULL</param>
            /// <returns>True if attributes where removed sucessfully</returns>
            public static bool Remove(FileInfo file, out Exception exception)
            {
                return HandleAttributes.File.Remove(file.FullName, out exception);
            }

            /// <summary>
            /// Remove attributes from specified file
            /// </summary>
            /// <param name="file">File to remove the attributes from</param>
            /// <returns>True if attributes where removed sucessfully</returns>
            public static bool Remove(string file)
            {
                return HandleAttributes.File.Remove(file, out _);
            }

            /// <summary>
            /// Remove attributes from specified File and provide exception informations, if attributes can not be removed
            /// </summary>
            /// <param name="file">File to remove the attributes from</param>
            /// <param name="exception">Exception informations if attribute can not be removed, otherwile NULL</param>
            /// <returns>True if attributes where removed sucessfully</returns>
            public static bool Remove(string file, out Exception exception)
            {
                exception = null;
                try
                {
                    System.IO.File.SetAttributes(file, FileAttributes.Normal);
                    return true;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    return false;
                }
            }
            #endregion

            #region Set attributes
            /// <summary>
            /// Set attributes to specified file
            /// </summary>
            /// <param name="file">File to set the attributes</param>
            /// <param name="attributes">Attributes to set to the Directory</param>
            /// <returns>True if attributes where set sucessfully</returns>
            public static bool Set(FileInfo file, FileAttributes attributes)
            {
                return HandleAttributes.File.Set(file, attributes, out _);
            }

            /// <summary>
            /// Set attributes to specified File and provide exception informations, if attributes can not be removed
            /// </summary>
            /// <param name="file">File to set the attributes</param>
            /// <param name="attributes">Attributes to set to the File</param>
            /// <param name="exception">Exception informations if attribute can not be set, otherwile NULL</param>
            /// <returns>True if attributes where set sucessfully</returns>
            public static bool Set(FileInfo file, FileAttributes attributes, out Exception exception)
            {
                return HandleAttributes.File.Set(file.FullName, attributes, out exception);
            }

            /// <summary>
            /// Set attributes to specified file
            /// </summary>
            /// <param name="file">File to set the attributes</param>
            /// <param name="attributes">Attributes to set to the Directory</param>
            /// <returns>True if attributes where set sucessfully</returns>
            public static bool Set(string file, FileAttributes attributes)
            {
                return HandleAttributes.File.Set(file, attributes, out _);
            }

            /// <summary>
            /// Set attributes to specified File and provide exception informations, if attributes can not be removed
            /// </summary>
            /// <param name="file">File to set the attributes</param>
            /// <param name="attributes">Attributes to set to the File</param>
            /// <param name="exception">Exception informations if attribute can not be set, otherwile NULL</param>
            /// <returns>True if attributes where set sucessfully</returns>
            public static bool Set(string file, FileAttributes attributes, out Exception exception)
            {
                exception = null;
                try
                {
                    System.IO.File.SetAttributes(file, attributes);
                    return true;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    return false;
                }
            }
            #endregion
            #endregion
        }
    }
}
/*
 * OLKI.Toolbox.DirectoryAndFile
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to handle files
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
using System.Windows.Forms;

namespace OLKI.Toolbox.DirectoryAndFile
{
    /// <summary>
    /// Class that provides tool to handle files
    /// </summary>
    public static class File
    {
        #region Constants
        /// <summary>
        /// Specifies by default if an existing wile should been overwrite withouzt question by File_Copy
        /// </summary>
        private const bool DEFUALT_COPY_OVERWRITE = false;
        /// <summary>
        /// Specifies the defaukt value if a security question should been shown before deleting a File by File_Delete
        /// </summary>
        private const bool DEFUALT_DELETE_SHOW_SECURITY_QUESTION = true;
        /// <summary>
        /// Specifies the defaukt value to return if the specified File does not exists by File_OpenToString
        /// </summary>
        private const string DEFUALT_OPEN_FILE_TO_STRING_VALUE_IF_FILE_NOT_EXISTS = "";
        #endregion

        #region Methods
        #region Copy
        /// <summary>
        /// Copies the specified File to the specified destination and ask to overwrite if the destination File already exists
        /// </summary>
        /// <param name="sourcePath">A string that specifies the source File path</param>
        /// <param name="destPath">A string that specifies the destination File path</param>
        /// <returns>True if copy was successful and false if not</returns>
        public static bool Copy(string sourcePath, string destPath)
        {
            return Copy(sourcePath, destPath, true, out _);
        }
        /// <summary>
        /// Copies the specified File to the specified destination and ask to overwrite if the destination File already exists
        /// </summary>
        /// <param name="sourcePath">A string that specifies the source File path</param>
        /// <param name="destPath">A string that specifies the destination File path</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if copy failed</param>
        /// <param name="exception">Exception while copy the File</param>
        /// <returns>True if copy was successful and false if not</returns>
        public static bool Copy(string sourcePath, string destPath, bool showExceptionMessage, out Exception exception)
        {
            return Copy(sourcePath, destPath, DEFUALT_COPY_OVERWRITE, showExceptionMessage, out exception);
        }
        /// <summary>
        /// Copies the specified File to the specified destination
        /// </summary>
        /// <param name="sourcePath">A string that specifies the source File path</param>
        /// <param name="destPath">A string that specifies the destination File path</param>
        /// <param name="overwrite">Set true to overwrite an existing File without question</param>
        /// <returns>True if copy was successful and false if not</returns>
        public static bool Copy(string sourcePath, string destPath, bool overwrite)
        {
            return Copy(sourcePath, destPath, overwrite, true, out _);
        }
        /// <summary>
        /// Copies the specified File to the specified destination
        /// </summary>
        /// <param name="sourcePath">A string that specifies the source File path</param>
        /// <param name="destPath">A string that specifies the destination File path</param>
        /// <param name="overwrite">Set true to overwrite an existing File without question</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if copy failed</param>
        /// <param name="exception">Exception while copy the File</param>
        /// <returns>True if copy was successful and false if not</returns>
        public static bool Copy(string sourcePath, string destPath, bool overwrite, bool showExceptionMessage, out Exception exception)
        {
            try
            {
                exception = null;
                if (!overwrite && System.IO.File.Exists(destPath))
                {
                    if (MessageBox.Show(string.Format(clsFile_Stringtable._0x0002m, new object[] { destPath }), clsFile_Stringtable._0x0002c, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        overwrite = true;
                    }
                    else
                    {
                        return false;
                    }
                }
                System.IO.File.Copy(sourcePath, destPath, overwrite);
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                if (showExceptionMessage) MessageBox.Show(string.Format(clsFile_Stringtable._0x0001m, new object[] { sourcePath, destPath, ex.Message }), clsFile_Stringtable._0x0001c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Creates an empty File with the specified name in the specified directory
        /// </summary>
        /// <param name="directoryPath">A string that specifies the Directory where the File shoukd be created</param>
        /// <param name="fileName">A string that specifies the File name of the File to create</param>
        /// <returns>True if creation of the specified File was successful and false if not</returns>
        public static bool Create(string directoryPath, string fileName)
        {
            return Create(directoryPath, fileName, true, out _);
        }
        /// <summary>
        /// Creates an empty File with the specified name in the specified directory
        /// </summary>
        /// <param name="directoryPath">A string that specifies the Directory where the File shoukd be created</param>
        /// <param name="fileName">A string that specifies the File name of the File to create</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if create failed</param>
        /// <param name="exception">Exception while create the File</param>
        /// <returns>True if creation of the specified File was successful and false if not</returns>
        public static bool Create(string directoryPath, string fileName, bool showExceptionMessage, out Exception exception)
        {
            try
            {
                exception = null;
                directoryPath = Path.Repair(directoryPath);
                System.IO.Directory.CreateDirectory(directoryPath);
                using (StreamWriter sw = System.IO.File.CreateText(directoryPath + fileName))
                {
                    //sw.WriteLine(string.Empty); -- Create empty file
                }
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                if (showExceptionMessage) MessageBox.Show(string.Format(clsFile_Stringtable._0x0003m, new object[] { directoryPath + fileName, ex.Message }), clsFile_Stringtable._0x0003c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete the specified File an shows a security question
        /// </summary>
        /// <param name="path">A string that specifies the File to delete</param>
        /// <returns>True if deleting of the specified File was successful and false if not</returns>
        public static bool Delete(string path)
        {
            return Delete(path, true, out _);
        }
        /// <summary>
        /// Delete the specified File an shows a security question
        /// </summary>
        /// <param name="path">A string that specifies the File to delete</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if delte failed</param>
        /// <param name="exception">Exception while delete the File</param>
        /// <returns>True if deleting of the specified File was successful and false if not</returns>
        public static bool Delete(string path, bool showExceptionMessage, out Exception exception)
        {
            return Delete(path, DEFUALT_DELETE_SHOW_SECURITY_QUESTION, showExceptionMessage, out exception);
        }
        /// <summary>
        /// Delete the specified File an shows a security question if specified
        /// </summary>
        /// <param name="path">A string that specifies the File to delete</param>
        /// <param name="showSecurityQuestion">Specifies if a security question should been shown before the File will be deleted</param>
        /// <returns>True if deleting of the specified File was successful and false if not</returns>
        public static bool Delete(string path, bool showSecurityQuestion)
        {
            return Delete(path, showSecurityQuestion, true, out _);
        }
        /// <summary>
        /// Delete the specified File an shows a security question if specified
        /// </summary>
        /// <param name="path">A string that specifies the File to delete</param>
        /// <param name="showSecurityQuestion">Specifies if a security question should been shown before the File will be deleted</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if delte failed</param>
        /// <param name="exception">Exception while delete the File</param>
        /// <returns>True if deleting of the specified File was successful and false if not</returns>
        public static bool Delete(string path, bool showSecurityQuestion, bool showExceptionMessage, out Exception exception)
        {
            try
            {
                exception = null;
                if ((!showSecurityQuestion || MessageBox.Show(string.Format(clsFile_Stringtable._0x0004m, new object[] { path }), clsFile_Stringtable._0x0004c, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3) == DialogResult.Yes) && System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                if (showExceptionMessage) MessageBox.Show(string.Format(clsFile_Stringtable._0x0005m, new object[] { path, ex.Message }), clsFile_Stringtable._0x0005c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region OpenToString
        /// <summary>
        /// Opens the specified File and returns the content as string. If the File can not open an empty string will returned
        /// </summary>
        /// <param name="path">A string that specifies the File top open</param>
        /// <returns>The content of the specified File as string or an empty string if File can not be opened</returns>
        public static string OpenToString(string path)
        {
            return OpenToString(path, true, out _);
        }
        /// <summary>
        /// Opens the specified File and returns the content as string
        /// </summary>
        /// <param name="path">A string that specifies the File top open</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if open failed</param>
        /// <param name="exception">Exception while open the File</param>
        /// <returns>The content of the specified File as string or the specified string if File can not be opened</returns>
        public static string OpenToString(string path, bool showExceptionMessage, out Exception exception)
        {
            return OpenToString(path, DEFUALT_OPEN_FILE_TO_STRING_VALUE_IF_FILE_NOT_EXISTS, showExceptionMessage, out exception);
        }
        /// <summary>
        /// Opens the specified File and returns the content as string
        /// </summary>
        /// <param name="path">A string that specifies the File top open</param>
        /// <param name="valueIfFileNotExists">A string that specifies the string to return if the File can not be opened</param>
        /// <returns>The content of the specified File as string or the specified string if File can not be opened</returns>
        public static string OpenToString(string path, string valueIfFileNotExists)
        {
            return OpenToString(path, valueIfFileNotExists, true, out _);
        }
        /// <summary>
        /// Opens the specified File and returns the content as string
        /// </summary>
        /// <param name="path">A string that specifies the File top open</param>
        /// <param name="valueIfFileNotExists">A string that specifies the string to return if the File can not be opened</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if open failed</param>
        /// <param name="exception">Exception while open the File</param>
        /// <returns>The content of the specified File as string or the specified string if File can not be opened</returns>
        public static string OpenToString(string path, string valueIfFileNotExists, bool showExceptionMessage, out Exception exception)
        {
            try
            {
                exception = null;
                string FileString = string.Empty;
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        while (streamReader.Peek() != -1)
                        {
                            FileString += streamReader.ReadLine();
                        }
                    }
                }
                return FileString;
            }
            catch (Exception ex)
            {
                exception = ex;
                if (showExceptionMessage) MessageBox.Show(string.Format(clsFile_Stringtable._0x0006m, new object[] { path, ex.Message }), clsFile_Stringtable._0x0006c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return valueIfFileNotExists;
            }
        }
        #endregion

        #region Other
        /// <summary>
        /// Shorten the filename if the resulting File path is longer than 260 chars (including the terminator)
        /// </summary>
        /// <param name="fullName">Path to the file, to check the length</param>
        /// <param name="exception">Exception while creating new File name</param>
        /// <returns>The full path with the shorten filename or an empty sting if an exception occurs</returns>
        public static string ShortenFilenameToMaxPathLength(string fullName, out Exception exception)
        {
            return ShortenFilenameToMaxPathLength(fullName, 260, out exception);
        }
        /// <summary>
        /// Shorten the filename if the resulting File path is longer than the given limit.
        /// </summary>
        /// <param name="fullName">Path to the file, to check the length</param>
        /// <param name="maxLength">Target File path length</param>
        /// <param name="exception">Exception while creating new File name</param>
        /// <returns>The full path with the shorten filename or an empty sting if an exception occurs</returns>
        public static string ShortenFilenameToMaxPathLength(string fullName, int maxLength, out Exception exception)
        {
            return ShortenFilenameToMaxPathLength(new FileInfo(fullName), maxLength, out exception);
        }
        /// <summary>
        /// Shorten the filename if the resulting File path is longer than 260 chars (including the terminator)
        /// </summary>
        /// <param name="fileInfo">FileInfo of the File to shorten fie filename, if necessary</param>
        /// <param name="exception">Exception while creating new File name</param>
        /// <returns>The full path with the shorten filename or an empty sting if an exception occurs</returns>
        public static string ShortenFilenameToMaxPathLength(FileInfo fileInfo, out Exception exception)
        {
            return ShortenFilenameToMaxPathLength(fileInfo, 260, out exception);
        }
        /// <summary>
        /// Shorten the filename if the resulting File path is longer than the given limit.
        /// </summary>
        /// <param name="fileInfo">FileInfo of the File to shorten fie filename, if necessary</param>
        /// <param name="maxLength">Target File path length</param>
        /// <param name="exception">Exception while creating new File name</param>
        /// <returns>The full path with the shorten filename or an empty sting if an exception occurs</returns>
        public static string ShortenFilenameToMaxPathLength(FileInfo fileInfo, int maxLength, out Exception exception)
        {
            exception = null;
            try
            {
                int MaxL = maxLength - 1;  //Remove 1 for the invisible termination cahracter <NUL>
                int DirL = fileInfo.Directory.FullName.Length + 1; //Add one for the missing Backslash
                int FilL = fileInfo.Name.Length - fileInfo.Extension.Length;
                int ExtL = fileInfo.Extension.Length;
                int SubL = DirL + FilL + ExtL - MaxL;

                if (fileInfo.FullName.Length <= MaxL) return fileInfo.FullName;
                if (FilL - SubL <= 0)
                {
                    exception = new Exception(clsFile_Stringtable._0x0007);
                    return string.Empty;
                }

                string ShortName = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length - SubL);
                return string.Format(@"{0}\{1}{2}", fileInfo.Directory.FullName, ShortName, fileInfo.Extension);
            }
            catch (Exception ex)
            {
                exception = ex;
                return string.Empty;
            }
        }
        #endregion
        #endregion
    }
}
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
        /// Specifies the defaukt value if a security question should been shown before deleting a file by File_Delete
        /// </summary>
        private const bool DEFUALT_DELETE_SHOW_SECURITY_QUESTION = true;
        /// <summary>
        /// Specifies the defaukt value to return if the specified file does not exists by File_OpenToString
        /// </summary>
        private const string DEFUALT_OPEN_FILE_TO_STRING_VALUE_IF_FILE_NOT_EXISTS = "";
        #endregion

        #region Methods
        #region Copy
        /// <summary>
        /// Copies the specified file to the specified destination and ask to overwrite if the destination file already exists
        /// </summary>
        /// <param name="sourcePath">A string that specifies the source file path</param>
        /// <param name="destPath">A string that specifies the destination file path</param>
        /// <returns>True if copy was successful and false if not</returns>
        public static bool Copy(string sourcePath, string destPath)
        {
            return Copy(sourcePath, destPath, DEFUALT_COPY_OVERWRITE);
        }
        /// <summary>
        /// Copies the specified file to the specified destination
        /// </summary>
        /// <param name="sourcePath">A string that specifies the source file path</param>
        /// <param name="destPath">A string that specifies the destination file path</param>
        /// <param name="overwrite">Set true to overwrite an existing file without question</param>
        /// <returns>True if copy was successful and false if not</returns>
        public static bool Copy(string sourcePath, string destPath, bool overwrite)
        {
            try
            {
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
                MessageBox.Show(string.Format(clsFile_Stringtable._0x0001m, new object[] { sourcePath, destPath, ex.Message }), clsFile_Stringtable._0x0001c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Creates an empty file with the specified name in the specified directory
        /// </summary>
        /// <param name="directoryPath">A string that specifies the directory where the file shoukd be created</param>
        /// <param name="fileName">A string that specifies the file name of the file to create</param>
        /// <returns>True if creation of the specified file was successful and false if not</returns>
        public static bool Create(string directoryPath, string fileName)
        {
            try
            {
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
                MessageBox.Show(string.Format(clsFile_Stringtable._0x0003m, new object[] { directoryPath + fileName, ex.Message }), clsFile_Stringtable._0x0003c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete the specified file an shows a security question
        /// </summary>
        /// <param name="path">A string that specifies the file to delete</param>
        /// <returns>True if deleting of the specified file was successful and false if not</returns>
        public static bool Delete(string path)
        {
            return Delete(path, DEFUALT_DELETE_SHOW_SECURITY_QUESTION);
        }
        /// <summary>
        /// Delete the specified file an shows a security question if specified
        /// </summary>
        /// <param name="path">A string that specifies the file to delete</param>
        /// <param name="showSecurityQuestion">Specifies if a security question should been shown before the file will be deleted</param>
        /// <returns>True if deleting of the specified file was successful and false if not</returns>
        public static bool Delete(string path, bool showSecurityQuestion)
        {
            try
            {
                if ((!showSecurityQuestion || MessageBox.Show(string.Format(clsFile_Stringtable._0x0004m, new object[] { path }), clsFile_Stringtable._0x0004c, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3) == DialogResult.Yes) && System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(clsFile_Stringtable._0x0005m, new object[] { path, ex.Message }), clsFile_Stringtable._0x0005c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region OpenToString
        /// <summary>
        /// Opens the specified file and returns the content as string. If the file can not open an empty string will returned
        /// </summary>
        /// <param name="path">A string that specifies the file top open</param>
        /// <returns>The content of the specified file as string or an empty string if file can not be opened</returns>
        public static string OpenToString(string path)
        {
            return OpenToString(path, DEFUALT_OPEN_FILE_TO_STRING_VALUE_IF_FILE_NOT_EXISTS);
        }
        /// <summary>
        /// Opens the specified file and returns the content as string
        /// </summary>
        /// <param name="path">A string that specifies the file top open</param>
        /// <param name="valueIfFileNotExists">A string that specifies the string to return if the file can not be opened</param>
        /// <returns>The content of the specified file as string or the specified string if file can not be opened</returns>
        public static string OpenToString(string path, string valueIfFileNotExists)
        {
            try
            {
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
                MessageBox.Show(string.Format(clsFile_Stringtable._0x0006m, new object[] { path, ex.Message }), clsFile_Stringtable._0x0006c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return valueIfFileNotExists;
            }
        }
        #endregion
        #endregion
    }
}
/*
 * OLKI.Toolbox.DirectoryAndFile
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to handle diorectorys
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
    /// Class that provides tool to handle diorectorys
    /// </summary>
    public static class Directory
    {
        #region Constants
        /// <summary>
        /// Specifies the defaukt value if a messagebox should be shown, if the access to an directory is not admitted
        /// </summary>
        private const bool DEFUALT_CHECK_ACCESS_SHOW_MESSAGE_IF_NOT = false;
        /// <summary>
        /// Specifies the defaukt value if all sub directorys of an directory shold been copied by Copy
        /// </summary>
        private const bool DEFUALT_COPY_SUB_DIRECTORYS = true;
        /// <summary>
        /// Specifies the defaukt value if a security question should been shown before deleting a directory by Delete
        /// </summary>
        private const bool DEFUALT_DELETE_SHOW_SECURITY_QUESTION = true;
        /// <summary>
        /// Specifies the defaukt value if a question should been shown to create a new folder if specified folder to open does not exists by Open
        /// </summary>
        private const bool DEFUALT_OPEN_ASK_FOR_CREATE_FOLDER = true;
        #endregion

        #region Methods
        #region CheckAccess
        /// <summary>
        /// Check if the access to a specified directory is allowed
        /// </summary>
        /// <param name="directory">Specifies an directory where the access should been checked</param>
        /// <returns>True if the access is allofed and false if the access is denied</returns>
        public static bool CheckAccess(DirectoryInfo directory)
        {
            return CheckAccess(directory.FullName);
        }
        /// <summary>
        /// Check if the access to a specified directory is allowed
        /// </summary>
        /// <param name="path">A string that specifies the path to an directory where the access should been checked</param>
        /// <returns>True if the access is allofed and false if the access is denied</returns>
        public static bool CheckAccess(string path)
        {
            return CheckAccess(path, DEFUALT_CHECK_ACCESS_SHOW_MESSAGE_IF_NOT);
        }
        /// <summary>
        /// Check if the access to a specified directory is allowed and can show a message if the access is denied
        /// </summary>
        /// <param name="directory">Specifies an directory where the access should been checked</param>
        /// <param name="showMessageIfNoAccess">Specifies if an message should been shown if the access to the specified directory is denied</param>
        /// <returns>True if the access is allofed and false if the access is denied</returns>
        public static bool CheckAccess(DirectoryInfo directory, bool showMessageIfNoAccess)
        {
            return CheckAccess(directory.FullName, showMessageIfNoAccess);
        }
        /// <summary>
        /// Check if the access to a specified directory is allowed and can show a message if the access is denied
        /// </summary>
        /// <param name="path">A string that specifies the path to an directory where the access should been checked</param>
        /// <param name="showMessageIfNoAccess">Specifies if an message should been shown if the access to the specified directory is denied</param>
        /// <returns>True if the access is allofed and false if the access is denied</returns>
        public static bool CheckAccess(string path, bool showMessageIfNoAccess)
        {
            try
            {
                System.IO.Directory.GetDirectories(path);
                return true;
            }
            catch (Exception ex)
            {
                if (showMessageIfNoAccess)
                {
                    MessageBox.Show(string.Format(clsDirectory_Stringtable._0x0007m, new object[] { path, ex.Message }), src.DirectroyAndFile.clsDirectory_Stringtable._0x0007c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    System.Diagnostics.Debug.Print(ex.Message);
                }
                return false;
            }
        }
        #endregion

        #region Copy
        /// <summary>
        /// Copys a specified directory to a specified destination and the sub directories
        /// </summary>
        /// <param name="sourcePath">A string that specifies the source directory path</param>
        /// <param name="destPath">A string that specifies the destination directory path</param>
        /// <returns>True if copy was successful and false if not</returns>
        public static bool Copy(string sourcePath, string destPath)
        {
            return Copy(sourcePath, destPath, DEFUALT_COPY_SUB_DIRECTORYS);
        }
        /// <summary>
        /// Copys a specified directory to a specified destination and the sub directoys if specified
        /// </summary>
        /// <param name="sourcePath">A string that specifies the destination directory path</param>
        /// <param name="destPath">A string that specifies the destination directory path</param>
        /// <param name="copySubDirs">Specifies if sub directory should ben copied</param>
        /// <returns>True if copy was successful and false if not</returns>
        public static bool Copy(string sourcePath, string destPath, bool copySubDirs)
        {
            try
            {
                // Get the subdirectories for the specified directory.
                DirectoryInfo SourceDirectory = new DirectoryInfo(sourcePath);
                DirectoryInfo[] SubDirectorys = SourceDirectory.GetDirectories();

                // If the destination directory doesn't exist, create it. 
                if (!System.IO.Directory.Exists(destPath))
                {
                    Create(destPath);
                }

                // Get the files in the directory and copy them to the new location.
                FileInfo[] Files = SourceDirectory.GetFiles();
                foreach (FileInfo File in Files)
                {
                    string Temppath = System.IO.Path.Combine(destPath, File.Name);
                    File.CopyTo(Temppath, false);
                }

                // If copying subdirectories, copy them and their contents to new location. 
                if (copySubDirs)
                {
                    foreach (DirectoryInfo Subdir in SubDirectorys)
                    {
                        string Temppath = System.IO.Path.Combine(destPath, Subdir.Name);
                        Copy(Subdir.FullName, Temppath, copySubDirs);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(clsDirectory_Stringtable._0x0006m, new object[] { sourcePath, destPath, ex.Message }), src.DirectroyAndFile.clsDirectory_Stringtable._0x0006c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Creates a directory at the specified path
        /// </summary>
        /// <param name="path">A string that specifies the path where the directory should be created</param>
        /// <returns>True if creation of the specified directory was successful and false if not</returns>
        public static bool Create(string path)
        {
            return Create(path, null);
        }
        /// <summary>
        /// Creates a directory at the specified path and copies the content from an template directory
        /// </summary>
        /// <param name="path">A string that specifies the path where the directory should be created</param>
        /// <param name="templatePath">An null-terminated string that specifies a template directory that sould be copied</param>
        /// <returns>True if creation of the specified directory was successful and false if not</returns>
        public static bool Create(string path, string templatePath)
        {
            try
            {
                // Create Folder
                System.IO.Directory.CreateDirectory(path);

                // Copy Template Files
                if (!string.IsNullOrEmpty(templatePath))
                {
                    Copy(templatePath, path);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(src.DirectroyAndFile.clsDirectory_Stringtable._0x0005m, new object[] { path, ex.Message }), src.DirectroyAndFile.clsDirectory_Stringtable._0x0005c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete the specified direcotry and shows a secority question
        /// </summary>
        /// <param name="path">A string that specifies the path to the directory to delete</param>
        /// <returns>True if the specified directory was deleted successful and false if not</returns>
        public static bool Delete(string path)
        {
            return Delete(path, DEFUALT_DELETE_SHOW_SECURITY_QUESTION);
        }
        /// <summary>
        /// Delete the specified direcotry and shows a secority question if specified
        /// </summary>
        /// <param name="path">A string that specifies the path to the directory to delete</param>
        /// <param name="showSecurityQuestion">Specifies if a security question should been shown before the direcotry will be deleted</param>
        /// <returns>True if the specified directory was deleted successful and false if not</returns>
        public static bool Delete(string path, bool showSecurityQuestion)
        {
            try
            {
                if ((!showSecurityQuestion || MessageBox.Show(string.Format(src.DirectroyAndFile.clsDirectory_Stringtable._0x0004m, new object[] { path }), src.DirectroyAndFile.clsDirectory_Stringtable._0x0004c, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3) == DialogResult.Yes) && System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.Delete(path);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(src.DirectroyAndFile.clsDirectory_Stringtable._0x0003m, new object[] { path, ex.Message }), src.DirectroyAndFile.clsDirectory_Stringtable._0x0003c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region Open
        /// <summary>
        /// Open the specified directory in explorerr. Ask for creating the directory if it does not exists.
        /// </summary>
        /// <param name="path">A string that specifies the path to the directory to open</param>
        /// <returns>True if the specified directory was opend successful and false if not</returns>
        public static bool Open(string path)
        {
            return Open(path, DEFUALT_OPEN_ASK_FOR_CREATE_FOLDER);
        }
        /// <summary>
        /// Open the specified directory in explorer
        /// </summary>
        /// <param name="path">A string that specifies the path to the directory to open</param>
        /// <param name="askForCreateFolder">Ask if the directory should be created if it dose not exists</param>
        /// <returns>True if the specified directory was opend successful and false if not</returns>
        public static bool Open(string path, bool askForCreateFolder)
        {
            return Open(path, askForCreateFolder, null);
        }
        /// <summary>
        /// Open the specified directory in explorer.  Ask for creating the directory if it does not exists and uses the specified template directory to create the new directory
        /// </summary>
        /// <param name="path">A string that specifies the path to the directory to open</param>
        /// <param name="templatePath">An null-terminated string that specifies a template directory that sould be copied</param>
        /// <returns>True if the specified directory was opend successful and false if not</returns>
        public static bool Open(string path, string templatePath)
        {
            return Open(path, DEFUALT_OPEN_ASK_FOR_CREATE_FOLDER, templatePath);
        }
        /// <summary>
        /// Open the specified directory in explorer and uses the specified template directory to create the new directory
        /// </summary>
        /// <param name="path">A string that specifies the path to the directory to open</param>
        /// <param name="askForCreateFolder">Ask if the directory should be created if it dose not exists</param>
        /// <param name="templatePath">An null-terminated string that specifies a template directory that sould be copied</param>
        /// <returns>True if the specified directory was opend successful and false if not</returns>
        public static bool Open(string path, bool askForCreateFolder, string templatePath)
        {
            if (string.IsNullOrEmpty(path)) return false;
            path = Path.Repair(path);

            // Check if the directroy exists, otherwhise check if the directroy should been created
            if (!System.IO.Directory.Exists(path))
            {
                // If the directroy didn't exists, ask if the driectroy should been created
                if (askForCreateFolder && MessageBox.Show(string.Format(src.DirectroyAndFile.clsDirectory_Stringtable._0x0002m, new object[] { path }), src.DirectroyAndFile.clsDirectory_Stringtable._0x0002c, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    // Create the directroy, using template specified directroy
                    if (Create(path, templatePath))
                    {
                        try
                        {
                            // Try to open directroy
                            System.Diagnostics.Process.Start("explorer.exe", path);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(string.Format(src.DirectroyAndFile.clsDirectory_Stringtable._0x0001m, new object[] { path, ex.Message }), src.DirectroyAndFile.clsDirectory_Stringtable._0x0001c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    System.Diagnostics.Process.Start("explorer.exe", path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(src.DirectroyAndFile.clsDirectory_Stringtable._0x0001m, new object[] { path, ex.Message }), src.DirectroyAndFile.clsDirectory_Stringtable._0x0001c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        #endregion
        #endregion
    }
}
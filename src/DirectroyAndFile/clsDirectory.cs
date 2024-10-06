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
        /// Specifies the defaukt value if a messagebox should be shown, if the access to an Directory is not admitted
        /// </summary>
        private const bool DEFUALT_CHECK_ACCESS_SHOW_MESSAGE_IF_NOT = false;
        /// <summary>
        /// Specifies the defaukt value if all sub directorys of an Directory shold been copied by Copy
        /// </summary>
        private const bool DEFUALT_COPY_SUB_DIRECTORYS = true;
        /// <summary>
        /// Specifies the defaukt value if a security question should been shown before deleting a Directory by Delete
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
        /// Check if the access to a specified Directory is allowed
        /// </summary>
        /// <param name="directory">Specifies an Directory where the access should been checked</param>
        /// <returns>True if the access is allofed and false if the access is denied</returns>
        public static bool CheckAccess(DirectoryInfo directory)
        {
            return CheckAccess(directory, out _);
        }
        /// <summary>
        /// Check if the access to a specified Directory is allowed
        /// </summary>
        /// <param name="directory">Specifies an Directory where the access should been checked</param>
        /// <param name="exception">Exception while checking Directroy access</param>
        /// <returns>True if the access is allofed and false if the access is denied</returns>
        public static bool CheckAccess(DirectoryInfo directory, out Exception exception)
        {
            return CheckAccess(directory.FullName, out exception);
        }
        /// <summary>
        /// Check if the access to a specified Directory is allowed
        /// </summary>
        /// <param name="path">A string that specifies the path to an Directory where the access should been checked</param>
        /// <returns>True if the access is allofed and false if the access is denied</returns>
        public static bool CheckAccess(string path)
        {
            return CheckAccess(path, out _);
        }
        /// <summary>
        /// Check if the access to a specified Directory is allowed
        /// </summary>
        /// <param name="path">A string that specifies the path to an Directory where the access should been checked</param>
        /// <param name="exception">Exception while checking Directroy access</param>
        /// <returns>True if the access is allofed and false if the access is denied</returns>
        public static bool CheckAccess(string path, out Exception exception)
        {
            return CheckAccess(path, DEFUALT_CHECK_ACCESS_SHOW_MESSAGE_IF_NOT, out exception);
        }
        /// <summary>
        /// Check if the access to a specified Directory is allowed and can show a message if the access is denied
        /// </summary>
        /// <param name="directory">Specifies an Directory where the access should been checked</param>
        /// <param name="showMessageIfNoAccess">Specifies if an message should been shown if the access to the specified Directory is denied</param>
        /// <returns>True if the access is allofed and false if the access is denied</returns>
        public static bool CheckAccess(DirectoryInfo directory, bool showMessageIfNoAccess)
        {
            return CheckAccess(directory, showMessageIfNoAccess, out _);
        }
        /// <summary>
        /// Check if the access to a specified Directory is allowed and can show a message if the access is denied
        /// </summary>
        /// <param name="directory">Specifies an Directory where the access should been checked</param>
        /// <param name="showMessageIfNoAccess">Specifies if an message should been shown if the access to the specified Directory is denied</param>
        /// <param name="exception">Exception while checking Directroy access</param>
        /// <returns>True if the access is allofed and false if the access is denied</returns>
        public static bool CheckAccess(DirectoryInfo directory, bool showMessageIfNoAccess, out Exception exception)
        {
            return CheckAccess(directory.FullName, showMessageIfNoAccess, out exception);
        }
        /// <summary>
        /// Check if the access to a specified Directory is allowed and can show a message if the access is denied
        /// </summary>
        /// <param name="path">A string that specifies the path to an Directory where the access should been checked</param>
        /// <param name="showMessageIfNoAccess">Specifies if an message should been shown if the access to the specified Directory is denied</param>
        /// <returns>True if the access is allofed and false if the access is denied</returns>
        public static bool CheckAccess(string path, bool showMessageIfNoAccess)
        {
            return CheckAccess(path, showMessageIfNoAccess, out _);
        }
        /// <summary>
        /// Check if the access to a specified Directory is allowed and can show a message if the access is denied
        /// </summary>
        /// <param name="path">A string that specifies the path to an Directory where the access should been checked</param>
        /// <param name="showMessageIfNoAccess">Specifies if an message should been shown if the access to the specified Directory is denied</param>
        /// <param name="exception">Exception while checking Directroy access</param>
        /// <returns>True if the access is allofed and false if the access is denied</returns>
        public static bool CheckAccess(string path, bool showMessageIfNoAccess, out Exception exception)
        {
            try
            {
                exception = null;
                System.IO.Directory.GetDirectories(path);
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                if (showMessageIfNoAccess) MessageBox.Show(string.Format(clsDirectory_Stringtable._0x0007m, new object[] { path, ex.Message }), clsDirectory_Stringtable._0x0007c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region Copy
        /// <summary>
        /// Copys a specified Directory to a specified destination and the sub directories
        /// </summary>
        /// <param name="sourcePath">A string that specifies the source Directory path</param>
        /// <param name="destPath">A string that specifies the destination Directory path</param>
        /// <returns>True if copy was successful and false if not</returns>
        public static bool Copy(string sourcePath, string destPath)
        {
            return Copy(sourcePath, destPath, true, out _);
        }
        /// <summary>
        /// Copys a specified Directory to a specified destination and the sub directories
        /// </summary>
        /// <param name="sourcePath">A string that specifies the source Directory path</param>
        /// <param name="destPath">A string that specifies the destination Directory path</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if copy Directory failed</param>
        /// <param name="exception">Exception while copy the Directory</param>
        /// <returns>True if copy was successful and false if not</returns>
        public static bool Copy(string sourcePath, string destPath, bool showExceptionMessage, out Exception exception)
        {
            return Copy(sourcePath, destPath, DEFUALT_COPY_SUB_DIRECTORYS, showExceptionMessage, out exception);
        }
        /// <summary>
        /// Copys a specified Directory to a specified destination and the sub directoys if specified
        /// </summary>
        /// <param name="sourcePath">A string that specifies the destination Directory path</param>
        /// <param name="destPath">A string that specifies the destination Directory path</param>
        /// <param name="copySubDirs">Specifies if sub Directory should ben copied</param>
        /// <returns>True if copy was successful and false if not</returns>
        public static bool Copy(string sourcePath, string destPath, bool copySubDirs)
        {
            return Copy(sourcePath, destPath, copySubDirs, true, out _);
        }
        /// <summary>
        /// Copys a specified Directory to a specified destination and the sub directoys if specified
        /// </summary>
        /// <param name="sourcePath">A string that specifies the destination Directory path</param>
        /// <param name="destPath">A string that specifies the destination Directory path</param>
        /// <param name="copySubDirs">Specifies if sub Directory should ben copied</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if copy Directory failed</param>
        /// <param name="exception">Exception while copy the Directory</param>
        /// <returns>True if copy was successful and false if not</returns>
        public static bool Copy(string sourcePath, string destPath, bool copySubDirs, bool showExceptionMessage, out Exception exception)
        {
            try
            {
                exception = null;
                // Get the subdirectories for the specified directory.
                DirectoryInfo SourceDirectory = new DirectoryInfo(sourcePath);
                DirectoryInfo[] SubDirectorys = SourceDirectory.GetDirectories();

                // If the destination Directory doesn't exist, create it. 
                if (!System.IO.Directory.Exists(destPath))
                {
                    Create(destPath);
                }

                // Get the files in the Directory and copy them to the new location.
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
                exception = ex;
                if (showExceptionMessage) MessageBox.Show(string.Format(clsDirectory_Stringtable._0x0006m, new object[] { sourcePath, destPath, ex.Message }), src.DirectroyAndFile.clsDirectory_Stringtable._0x0006c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Creates a Directory at the specified path
        /// </summary>
        /// <param name="path">A string that specifies the path where the Directory should be created</param>
        /// <returns>True if creation of the specified Directory was successful and false if not</returns>
        public static bool Create(string path)
        {
            return Create(path, true, out _);
        }
        /// <summary>
        /// Creates a Directory at the specified path
        /// </summary>
        /// <param name="path">A string that specifies the path where the Directory should be created</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if create Directory failed</param>
        /// <param name="exception">Exception while create the Directory</param>
        /// <returns>True if creation of the specified Directory was successful and false if not</returns>
        public static bool Create(string path, bool showExceptionMessage, out Exception exception)
        {
            return Create(path, null, showExceptionMessage, out exception);
        }
        /// <summary>
        /// Creates a Directory at the specified path and copies the content from an template directory
        /// </summary>
        /// <param name="path">A string that specifies the path where the Directory should be created</param>
        /// <param name="templatePath">An null-terminated string that specifies a template Directory that sould be copied</param>
        /// <returns>True if creation of the specified Directory was successful and false if not</returns>
        public static bool Create(string path, string templatePath)
        {
            return Create(path, templatePath, true, out _);
        }
        /// <summary>
        /// Creates a Directory at the specified path and copies the content from an template directory
        /// </summary>
        /// <param name="path">A string that specifies the path where the Directory should be created</param>
        /// <param name="templatePath">An null-terminated string that specifies a template Directory that sould be copied</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if create Directory failed</param>
        /// <param name="exception">Exception while create the Directory</param>
        /// <returns>True if creation of the specified Directory was successful and false if not</returns>
        public static bool Create(string path, string templatePath, bool showExceptionMessage, out Exception exception)
        {
            try
            {
                exception = null;
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
                exception = ex;
                if (showExceptionMessage) MessageBox.Show(string.Format(clsDirectory_Stringtable._0x0005m, new object[] { path, ex.Message }), src.DirectroyAndFile.clsDirectory_Stringtable._0x0005c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete the specified direcotry and shows a secority question
        /// </summary>
        /// <param name="path">A string that specifies the path to the Directory to delete</param>
        /// <returns>True if the specified Directory was deleted successful and false if not</returns>
        public static bool Delete(string path)
        {
            return Delete(path, true, out _);
        }
        /// <summary>
        /// Delete the specified direcotry and shows a secority question
        /// </summary>
        /// <param name="path">A string that specifies the path to the Directory to delete</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if delete Directory failed</param>
        /// <param name="exception">Exception while delete the Directory</param>
        /// <returns>True if the specified Directory was deleted successful and false if not</returns>
        public static bool Delete(string path, bool showExceptionMessage, out Exception exception)
        {
            return Delete(path, DEFUALT_DELETE_SHOW_SECURITY_QUESTION, showExceptionMessage, out exception);
        }
        /// <summary>
        /// Delete the specified direcotry and shows a secority question if specified
        /// </summary>
        /// <param name="path">A string that specifies the path to the Directory to delete</param>
        /// <param name="showSecurityQuestion">Specifies if a security question should been shown before the direcotry will be deleted</param>
        /// <returns>True if the specified Directory was deleted successful and false if not</returns>
        public static bool Delete(string path, bool showSecurityQuestion)
        {
            return Delete(path, showSecurityQuestion, true, out _);
        }
        /// <summary>
        /// Delete the specified direcotry and shows a secority question if specified
        /// </summary>
        /// <param name="path">A string that specifies the path to the Directory to delete</param>
        /// <param name="showSecurityQuestion">Specifies if a security question should been shown before the direcotry will be deleted</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if delete Directory failed</param>
        /// <param name="exception">Exception while delete the Directory</param>
        /// <returns>True if the specified Directory was deleted successful and false if not</returns>
        public static bool Delete(string path, bool showSecurityQuestion, bool showExceptionMessage, out Exception exception)
        {
            try
            {
                exception = null;
                if ((!showSecurityQuestion || MessageBox.Show(string.Format(clsDirectory_Stringtable._0x0004m, new object[] { path }), clsDirectory_Stringtable._0x0004c, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3) == DialogResult.Yes) && System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.Delete(path);
                }
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                if (showExceptionMessage) MessageBox.Show(string.Format(clsDirectory_Stringtable._0x0003m, new object[] { path, ex.Message }), clsDirectory_Stringtable._0x0003c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region Open
        /// <summary>
        /// Open the specified Directory in explorerr. Ask for creating the Directory if it does not exists.
        /// </summary>
        /// <param name="path">A string that specifies the path to the Directory to open</param>
        /// <returns>True if the specified Directory was opend successful and false if not</returns>
        public static bool Open(string path)
        {
            return Open(path, true, out _);
        }
        /// <summary>
        /// Open the specified Directory in explorerr. Ask for creating the Directory if it does not exists.
        /// </summary>
        /// <param name="path">A string that specifies the path to the Directory to open</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if open Directory failed</param>
        /// <param name="exception">Exception while open directory</param>
        /// <returns>True if the specified Directory was opend successful and false if not</returns>
        public static bool Open(string path, bool showExceptionMessage, out Exception exception)
        {
            return Open(path, DEFUALT_OPEN_ASK_FOR_CREATE_FOLDER, showExceptionMessage, out exception);
        }
        /// <summary>
        /// Open the specified Directory in explorer
        /// </summary>
        /// <param name="path">A string that specifies the path to the Directory to open</param>
        /// <param name="askForCreateFolder">Ask if the Directory should be created if it dose not exists</param>
        /// <returns>True if the specified Directory was opend successful and false if not</returns>
        public static bool Open(string path, bool askForCreateFolder)
        {
            return Open(path, askForCreateFolder, true, out _);
        }
        /// <summary>
        /// Open the specified Directory in explorer
        /// </summary>
        /// <param name="path">A string that specifies the path to the Directory to open</param>
        /// <param name="askForCreateFolder">Ask if the Directory should be created if it dose not exists</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if open Directory failed</param>
        /// <param name="exception">Exception while open directory</param>
        /// <returns>True if the specified Directory was opend successful and false if not</returns>
        public static bool Open(string path, bool askForCreateFolder, bool showExceptionMessage, out Exception exception)
        {
            return Open(path, askForCreateFolder, null, showExceptionMessage, out exception);
        }
        /// <summary>
        /// Open the specified Directory in explorer.  Ask for creating the Directory if it does not exists and uses the specified template Directory to create the new directory
        /// </summary>
        /// <param name="path">A string that specifies the path to the Directory to open</param>
        /// <param name="templatePath">An null-terminated string that specifies a template Directory that sould be copied</param>
        /// <returns>True if the specified Directory was opend successful and false if not</returns>
        public static bool Open(string path, string templatePath)
        {
            return Open(path, DEFUALT_OPEN_ASK_FOR_CREATE_FOLDER, templatePath);
        }
        /// <summary>
        /// Open the specified Directory in explorer and uses the specified template Directory to create the new directory
        /// </summary>
        /// <param name="path">A string that specifies the path to the Directory to open</param>
        /// <param name="askForCreateFolder">Ask if the Directory should be created if it dose not exists</param>
        /// <param name="templatePath">An null-terminated string that specifies a template Directory that sould be copied</param>
        /// <returns>True if the specified Directory was opend successful and false if not</returns>
        public static bool Open(string path, bool askForCreateFolder, string templatePath)
        {
            return Open(path, askForCreateFolder, templatePath, true, out _);
        }
        /// <summary>
        /// Open the specified Directory in explorer and uses the specified template Directory to create the new directory
        /// </summary>
        /// <param name="path">A string that specifies the path to the Directory to open</param>
        /// <param name="askForCreateFolder">Ask if the Directory should be created if it dose not exists</param>
        /// <param name="templatePath">An null-terminated string that specifies a template Directory that sould be copied</param>
        /// <param name="showExceptionMessage">Should an Exception Message shown, if open Directory failed</param>
        /// <param name="exception">Exception while open directory</param>
        /// <returns>True if the specified Directory was opend successful and false if not</returns>
        public static bool Open(string path, bool askForCreateFolder, string templatePath, bool showExceptionMessage, out Exception exception)
        {
            exception = null;
            if (string.IsNullOrEmpty(path)) return false;
            path = Path.Repair(path);

            // Check if the directroy exists, otherwhise check if the directroy should been created
            if (!System.IO.Directory.Exists(path))
            {
                // If the directroy didn't exists, ask if the driectroy should been created
                if (askForCreateFolder && MessageBox.Show(string.Format(clsDirectory_Stringtable._0x0002m, new object[] { path }), clsDirectory_Stringtable._0x0002c, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
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
                            exception = ex;
                            if (showExceptionMessage) MessageBox.Show(string.Format(clsDirectory_Stringtable._0x0001m, new object[] { path, ex.Message }), clsDirectory_Stringtable._0x0001c, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    exception = ex;
                    if (showExceptionMessage) MessageBox.Show(string.Format(clsDirectory_Stringtable._0x0001m, new object[] { path, ex.Message }), clsDirectory_Stringtable._0x0001c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        #endregion
        #endregion
    }
}
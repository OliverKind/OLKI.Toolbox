/*
 * OLKI.Toolbox.DirectoryAndFile
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to handle File assiciations
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
using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace OLKI.Toolbox.DirectoryAndFile
{
    /// <summary>
    /// Class that provides tool to handle File assiciations
    /// </summary>
    public static class FileAssociation
    {
        #region Constants
        /// <summary>
        /// Specifies the default usertype whrere fileassociation should be defined
        /// </summary>
        private const UserType DEFAULT_FILE_ASSOCIATION_USER_TYPE = UserType.CurrentUser;
        /// <summary>
        /// Specifies the defaukt value if a messagebox should be shown, if a File is associated to the specified applicaiton
        /// </summary>
        private const bool DEFAULT_FILE_ASSOCIATION_SHOW_MESSAGE_IF_FILE_IS_ASSOCIATED = true;
        #endregion

        #region Members
        /// <summary>
        /// An Enumeration ths specifies user types
        /// </summary>
        public enum UserType
        {
            /// <summary>
            /// Current logged in user
            /// </summary>
            CurrentUser,
            /// <summary>
            /// All User
            /// </summary>
            AllUser,
        }
        #endregion

        #region Methods
        #region FindApplication
        /// <summary>
        /// Find the applications it is associated by windows default to a file. Don't throw an exception if ther is no File association.
        /// </summary>
        /// <param name="extension">The extension or A string that specifies the File to find the associated application</param>
        /// <returns>Execution path to the application, they is associated to the File or an empty string if no application is association</returns>
        public static string FindApplication(string extension)
        {
            return FindApplication(extension, false);
        }

        /// <summary>
        /// Find the applications it is associated by windows default to a file
        /// </summary>
        /// <param name="extension">The extension or A string that specifies the File to find the associated application</param>
        /// <param name="throwExceptionIfNoAssociation">Specifies if an exception should been thrown if ther is no filce assiciation</param>
        /// <returns>Execution path to the application, they is associated to the File or an empty string if no application is association</returns>
        public static string FindApplication(string extension, bool throwExceptionIfNoAssociation)
        {
            try
            {

                string AppFiletype;
                string AssociatedApp;
                string[] extSplit = extension.Split('.');

                extension = "." + extSplit[extSplit.Length - 1];

                AppFiletype = (string)Registry.GetValue(@"HKEY_CLASSES_ROOT\" + extension, "", "");
                if (string.IsNullOrEmpty(AppFiletype))
                {
                    if (throwExceptionIfNoAssociation) throw new Exception(clsFileAssociation_Stringtable.FindApplication_SE_ERR_NOASSOC);
                    return string.Empty;
                }

                AssociatedApp = (string)Registry.GetValue(@"HKEY_CLASSES_ROOT\" + AppFiletype + @"\Shell\Open\Command", "", "");
                AssociatedApp = AssociatedApp.Replace("%1", "");
                AssociatedApp = AssociatedApp.Replace("\"", "");
                AssociatedApp = AssociatedApp.Trim();

                if (string.IsNullOrEmpty(AssociatedApp))
                {
                    if (throwExceptionIfNoAssociation) throw new Exception(clsFileAssociation_Stringtable.FindApplication_SE_ERR_NOASSOC);
                    return string.Empty;
                }

                return AssociatedApp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(clsFileAssociation_Stringtable._0x0001m, new object[] { extension, ex.Message }), clsFileAssociation_Stringtable._0x0001c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region CheckMatchWithApplication
        /// <summary>
        /// Matches if the specified application executable is associated with the specified file
        /// </summary>
        /// <param name="appPath">A string that specifies a application which where the specified File shold be associated with</param>
        /// <param name="filePathOrExtension">A string that specifies a File name or only the extension</param>
        /// <returns>True if the defined application is associated wit  h the defined file</returns>
        public static bool CheckMatchWithApplication(string appPath, string filePathOrExtension)
        {
            appPath = Path.Repair(appPath);
            filePathOrExtension = Path.Repair(filePathOrExtension);

            string FileAssociation = FindApplication(filePathOrExtension);
            if (string.IsNullOrEmpty(FileAssociation) || string.IsNullOrEmpty(appPath) || string.Compare(appPath, FileAssociation, true) != 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region CheckMatchWithApplicationAndSet
        /// <summary>
        /// Matches if the specified File is associated to the specified application executable is and set the File association of the spicified File to the specified application executable, if requested, if the specified is not associated to the specified application
        /// </summary>
        /// <param name="appPath">A string that specifies a application which where the specified File shold be associated with</param>
        /// <param name="filePath">A string that specifies a File name. This File should be a document.</param>
        /// <param name="applicationFiletype">The File type of the application, as example "txt.text"</param>
        /// <param name="description">The description of the File type, as example "Textwriter Textfile"</param>
        /// <param name="iconPath">A string that specifies the icon which should be associated to the specified File type. This value can be NULL.</param>
        public static void CheckMatchWithApplicationAndSet(string appPath, string filePath, string applicationFiletype, string description, string iconPath)
        {
            CheckMatchWithApplicationAndSet(appPath, filePath, applicationFiletype, description, iconPath, DEFAULT_FILE_ASSOCIATION_SHOW_MESSAGE_IF_FILE_IS_ASSOCIATED);
        }
        /// <summary>
        /// Matches if the specified File is associated to the specified application executable is and set the File association of the spicified File to the specified application executable, if requested, if the specified is not associated to the specified application
        /// </summary>
        /// <param name="appPath">A string that specifies a application which where the specified File shold be associated with</param>
        /// <param name="filePath">A string that specifies a File name. This File should be a document.</param>
        /// <param name="applicationFiletype">The File type of the application, as example "txt.text"</param>
        /// <param name="description">The description of the File type, as example "Textwriter Textfile"</param>
        /// <param name="iconPath">A string that specifies the icon which should be associated to the specified File type. This value can be NULL.</param>
        /// <param name="showMessageIfConnected">A boolean value it specifies if an message shold been shown if the specified File is associated to specified application</param>
        public static void CheckMatchWithApplicationAndSet(string appPath, string filePath, string applicationFiletype, string description, string iconPath, bool showMessageIfConnected)
        {
            CheckMatchWithApplicationAndSet(appPath, filePath, System.IO.Path.GetExtension(filePath), applicationFiletype, description, iconPath, showMessageIfConnected);
        }
        /// <summary>
        /// Matches if the specified File is associated to the specified application executable is and set the File association of the spicified File to the specified application executable, if requested, if the specified is not associated to the specified application
        /// </summary>
        /// <param name="appPath">A string that specifies a application which where the specified File shold be associated with</param>
        /// <param name="filePath">A string that specifies a File name. This File should be a document.</param>
        /// <param name="extension">The extension of the File to associate, as example ".txt"</param>
        /// <param name="applicationFiletype">The File type of the application, as example "txt.text"</param>
        /// <param name="description">The description of the File type, as example "Textwriter Textfile"</param>
        /// <param name="iconPath">A string that specifies the icon which should be associated to the specified File type. This value can be NULL.</param>
        public static void CheckMatchWithApplicationAndSet(string appPath, string filePath, string extension, string applicationFiletype, string description, string iconPath)
        {
            CheckMatchWithApplicationAndSet(appPath, filePath, extension, applicationFiletype, description, iconPath, DEFAULT_FILE_ASSOCIATION_SHOW_MESSAGE_IF_FILE_IS_ASSOCIATED);
        }
        /// <summary>
        /// Matches if the specified File is associated to the specified application executable is and set the File association of the spicified File to the specified application executable, if requested, if the specified is not associated to the specified application
        /// </summary>
        /// <param name="appPath">A string that specifies a application which where the specified File shold be associated with</param>
        /// <param name="filePath">A string that specifies a File name. This File should be a document.</param>
        /// <param name="extension">The extension of the File to associate, as example ".txt"</param>
        /// <param name="applicationFiletype">The File type of the application, as example "txt.text"</param>
        /// <param name="description">The description of the File type, as example "Textwriter Textfile"</param>
        /// <param name="iconPath">A string that specifies the icon which should be associated to the specified File type. This value can be NULL.</param>
        /// <param name="showMessageIfFileisAccociated">A boolean value it specifies if an message shold been shown if the specified File is associated to specified application</param>
        public static void CheckMatchWithApplicationAndSet(string appPath, string filePath, string extension, string applicationFiletype, string description, string iconPath, bool showMessageIfFileisAccociated)
        {
            if (string.IsNullOrEmpty(filePath)) filePath = "." + extension;

            if (!CheckMatchWithApplication(appPath, filePath))
            {
                if (MessageBox.Show(string.Format(clsFileAssociation_Stringtable._0x0002m, new object[] { extension }), clsFileAssociation_Stringtable._0x0002c, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    Set(appPath, extension, applicationFiletype, description, iconPath);
                }
            }
            else
            {
                if (showMessageIfFileisAccociated)
                {
                    MessageBox.Show(string.Format(clsFileAssociation_Stringtable._0x0003m, new object[] { extension }), clsFileAssociation_Stringtable._0x0003c, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion

        #region Set
        /// <summary>
        /// Associates the specified File type the the specified  application
        /// </summary>
        /// <param name="appPath">A string that specifies a application which where the specified File shold be associated with</param>
        /// <param name="extension">The extension of the File to associate, as example ".txt"</param>
        /// <param name="applicationFiletype">The File type of the application, as example "txt.text"</param>
        /// <param name="description">The description of the File type, as example "Textwriter Textfile"</param>
        /// <param name="iconPath">A string that specifies the icon which should be associated to the specified File type. This value can be NULL.</param>
        /// <returns>True if the it was possible to associate the specified File type to the specified application or fale if not</returns>
        public static bool Set(string appPath, string extension, string applicationFiletype, string description, string iconPath)
        {
            return Set(appPath, extension, applicationFiletype, description, iconPath, DEFAULT_FILE_ASSOCIATION_USER_TYPE);
        }
        /// <summary>
        /// Associates the specified File type the the specified  application. It can be specified if it is for all users or only for the current user
        /// </summary>
        /// <param name="appPath">A string that specifies a application which where the specified File shold be associated with</param>
        /// <param name="extension">The extension of the File to associate, as example ".txt"</param>
        /// <param name="applicationFiletype">The File type of the application, as example "txt.text"</param>
        /// <param name="description">The description of the File type, as example "Textwriter Textfile"</param>
        /// <param name="iconPath">A string that specifies the icon which should be associated to the specified File type. This value can be NULL.</param>
        /// <param name="userType">An Enumeration it specifies if the fil type is associated for the specified application for all user or only for the current user</param>
        /// <returns>True if the it was possible to associate the specified File type to the specified application or false if not</returns>
        public static bool Set(string appPath, string extension, string applicationFiletype, string description, string iconPath, UserType userType)
        {
            appPath = Path.Repair(appPath);
            iconPath = Path.Repair(iconPath);

            // Set fileassociation for all users
            try
            {
                if (userType == UserType.AllUser || (userType & UserType.AllUser) == UserType.AllUser)
                {
                    RegistryKey FileKey = Registry.LocalMachine.OpenSubKey(@"Software\Classes\", RegistryKeyPermissionCheck.ReadWriteSubTree);
                    FileKey.CreateSubKey("." + extension, RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue("", applicationFiletype);

                    RegistryKey AppKey = Registry.LocalMachine.OpenSubKey(@"Software\Classes\", RegistryKeyPermissionCheck.ReadWriteSubTree);
                    AppKey.CreateSubKey(applicationFiletype).SetValue("", description);
                    AppKey = Registry.LocalMachine.OpenSubKey(@"Software\Classes\" + applicationFiletype, RegistryKeyPermissionCheck.ReadWriteSubTree);

                    if (!string.IsNullOrEmpty(iconPath)) AppKey.CreateSubKey("DefaultIcon").SetValue("", iconPath);
                    AppKey.CreateSubKey(@"Shell\Open\Command").SetValue("", "\"" + appPath + "\" \"%1\"");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(clsFileAssociation_Stringtable._0x0004m, new object[] { extension, ex.Message }), clsFileAssociation_Stringtable._0x0004c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Set fileassociation for current user
            try
            {
                if (userType == UserType.CurrentUser || (userType & UserType.CurrentUser) == UserType.CurrentUser)
                {
                    RegistryKey FileKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\", RegistryKeyPermissionCheck.ReadWriteSubTree);
                    FileKey.CreateSubKey("." + extension, RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue("", applicationFiletype);

                    RegistryKey AppKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\", RegistryKeyPermissionCheck.ReadWriteSubTree);
                    AppKey.CreateSubKey(applicationFiletype).SetValue("", description);
                    AppKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\" + applicationFiletype, RegistryKeyPermissionCheck.ReadWriteSubTree);
                    if (!string.IsNullOrEmpty(iconPath)) AppKey.CreateSubKey("DefaultIcon").SetValue("", iconPath);
                    AppKey.CreateSubKey(@"Shell\Open\Command").SetValue("", "\"" + appPath + "\" \"%1\"");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(clsFileAssociation_Stringtable._0x0005m, new object[] { extension, ex.Message }), clsFileAssociation_Stringtable._0x0005c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            MessageBox.Show(string.Format(clsFileAssociation_Stringtable._0x0006m, new object[] { extension }), clsFileAssociation_Stringtable._0x0006c, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }
        #endregion
        #endregion
    }
}
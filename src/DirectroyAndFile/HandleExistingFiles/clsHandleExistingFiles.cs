/*
 * OLKI.Toolbox.DirectoryAndFile
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to handle exiting files
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
using System.Windows.Forms;

namespace OLKI.Toolbox.DirectoryAndFile
{
    /// <summary>
    /// Provides tools to create a directroy and file backup
    /// </summary>
    public static class HandleExistingFiles
    {
        #region Enums
        /// <summary>
        /// How to handle exisiting file
        /// </summary>
        public enum ExistingFile
        {
            /// <summary>
            /// Exception while get handle action or rename exisiting file
            /// </summary>
            Exception,
            /// <summary>
            /// Overwrite existing file
            /// </summary>
            Overwrite,
            /// <summary>
            /// Rename existing file
            /// </summary>
            Rename,
            /// <summary>
            /// Skip exisiting file
            /// </summary>
            Skip
        }

        /// <summary>
        /// An Enumeration ths specifies how to handle existing items
        /// </summary>
        public enum HowToHandleExistingItem
        {
            /// <summary>
            /// Overwrite all existing files withoud asking
            /// </summary>
            OverwriteAll,
            /// <summary>
            /// Overwrite the exising file if the source file is newer, creation- or last change date
            /// </summary>
            OverwriteIfNewer,
            /// <summary>
            /// Aks any time if the existing file should been overwritten
            /// </summary>
            AskAnyTime,
            /// <summary>
            /// Overwrite the exising file if the length of the source and target file is different
            /// </summary>
            OverwriteIfDifferentLength,
            /// <summary>
            /// Overwrite the exising file if the source file is newer, creation- or last change date, or if the length of the source and target file is different
            /// </summary>
            OverwriteIfDifferentLengthOrNewer,
            /// <summary>
            /// Add the specified text to the existing target file name, behind extension
            /// </summary>
            AddText,
            /// <summary>
            /// Skip the file and proceed to next file
            /// </summary>
            Skipp
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Gets how to handle existing files. Show form to ask how to handle, if necessary
        /// </summary>
        /// <param name="sourceFile">Source file to copy</param>
        /// <param name="targetFile">Target file to copy the data to</param>
        /// <param name="dateFormat">Date format for adding date text to exisiting file</param>
        /// <param name="defaultAction">Pre defined action, how to handle exisitng files</param>
        /// <param name="defaultAddText">Default text to add to existing files</param>
        /// <param name="defaultRemember">Default state of remember action CheckBox</param>
        /// <param name="exception">Exception while handle existing file</param>
        /// <returns>Should the exisiting file been overwritten, was renamend or occourded an exception</returns>
        public static CheckResult GetOverwriteByAction(FileInfo sourceFile, FileInfo targetFile, string dateFormat, HowToHandleExistingItem defaultAction, string defaultAddText, bool defaultRemember, out Exception exception)
        {
            return GetOverwriteByAction(sourceFile, targetFile, dateFormat, defaultAction, defaultAddText, defaultRemember, out exception, null);
        }
        /// <summary>
        /// Gets how to handle existing files. Show form to ask how to handle, if necessary
        /// </summary>
        /// <param name="sourceFile">Source file to copy</param>
        /// <param name="targetFile">Target file to copy the data to</param>
        /// <param name="dateFormat">Date format for adding date text to exisiting file</param>
        /// <param name="defaultAction">Pre defined action, how to handle exisitng files</param>
        /// <param name="defaultAddText">Default text to add to existing files</param>
        /// <param name="defaultRemember">Default state of remember action CheckBox</param>
        /// <param name="exception">Exception while handle existing file</param>
        /// <param name="parentForm">Parent form to show dialog in modal mode</param>
        /// <returns>Should the exisiting file been overwritten, was renamend or occourded an exception</returns>

        private delegate void InvokeDelegate(Form parent);

        public static CheckResult GetOverwriteByAction(FileInfo sourceFile, FileInfo targetFile, string dateFormat, HowToHandleExistingItem defaultAction, string defaultAddText, bool defaultRemember, out Exception exception, Form parent)
        {
            exception = null;
            CheckResult CheckResult = new CheckResult
            {
                FormResult = DialogResult.OK,
                OverwriteFile = ExistingFile.Exception,
                AddText = defaultAddText,
                RememberAction = defaultRemember,
                SelectedAction = defaultAction
            };

            // Open Form, if action is ask what to do
            if (defaultAction == HowToHandleExistingItem.AskAnyTime)
            {
                HandleExistingFilesForm HandleExistingFilesForm = new HandleExistingFilesForm(HandleExistingFilesForm.FormMode.FileExists, sourceFile, targetFile, defaultAction, defaultAddText, defaultRemember);
                DialogResult FormResult; // = HandleExistingFilesForm.ShowDialog(parentForm);

                if (parent != null)
                {
                    // Show form modal to parent form, invoke
                    FormResult = (DialogResult)parent.Invoke((Func<DialogResult>)(() => HandleExistingFilesForm.ShowDialog()));

                }
                else
                {
                    // Show form with no parent form
                    FormResult = HandleExistingFilesForm.ShowDialog();
                }

                CheckResult.FormResult = FormResult;
                CheckResult.AddText = HandleExistingFilesForm.ActionAddTextText;
                CheckResult.RememberAction = HandleExistingFilesForm.RememberActionHandleExistingFiles;
                CheckResult.SelectedAction = HandleExistingFilesForm.ActionHandleExistingFiles;

                if (FormResult == DialogResult.Cancel) return CheckResult;
            }

            CheckResult.OverwriteFile = GetOverwriteByAction(sourceFile, targetFile, dateFormat, CheckResult.SelectedAction, CheckResult.AddText, out exception);
            return CheckResult;
        }

        /// <summary>
        /// Gets how if the existing file should been overwritten, by selected action
        /// </summary>
        /// <param name="sourceFile">Source file to copy</param>
        /// <param name="targetFile">Target file to copy the data to</param>
        /// <param name="dateFormat">Date format for adding date text to exisiting file</param>
        /// <param name="action">Action, how to handle exisitng files</param>
        /// <param name="addText">Text to add to existing files</param>
        /// <param name="exception">Exception while handle existing file</param>
        /// <returns>Should the exisiting file been overwritten, was renamend or occourded an exception</returns>
        private static ExistingFile GetOverwriteByAction(FileInfo sourceFile, FileInfo targetFile, string dateFormat, HowToHandleExistingItem action, string addText, out Exception exception)
        {
            exception = null;

            switch (action)
            {
                case HowToHandleExistingItem.AddText:
                    return TargetAddText(targetFile, dateFormat, addText, out exception);
                case HowToHandleExistingItem.AskAnyTime:
                    throw new ArgumentException("HandleExistingFiles->GetOverwriteByAction->Invalid value", nameof(action));
                case HowToHandleExistingItem.OverwriteAll:
                    return TargetOverwrite();
                case HowToHandleExistingItem.OverwriteIfDifferentLength:
                    return SourceDifferentLength(sourceFile, targetFile);
                case HowToHandleExistingItem.OverwriteIfDifferentLengthOrNewer:
                    return SourceDifferentLengthOrNewer(sourceFile, targetFile);
                case HowToHandleExistingItem.OverwriteIfNewer:
                    return SourceNewer(sourceFile, targetFile);
                case HowToHandleExistingItem.Skipp:
                    return SourceSkip();
                default:
                    throw new ArgumentException("HandleExistingFiles->GetOverwriteByAction->Invalid value", nameof(action));
            }
        }

        /// <summary>
        /// Rename existing file
        /// </summary>
        /// <param name="targetFile">Target file to copy</param>
        /// <param name="dateFormat">Format of an added date</param>
        /// <param name="addText">Text to add to exisitng file</param>
        /// <param name="exception">Exception while renaming existing file</param>
        /// <returns>Should been file written after renaming, or exception if an exception occours</returns>
        public static ExistingFile TargetAddText(FileInfo targetFile, string dateFormat, string addText, out Exception exception)
        {
            exception = null;
            try
            {
                addText.Replace("{{$CreationTime}}", targetFile.CreationTime.ToString(dateFormat));
                addText.Replace("{{$LastAccessTime}}", targetFile.LastAccessTime.ToString(dateFormat));
                addText.Replace("{{$LastWriteTime}}", targetFile.LastWriteTime.ToString(dateFormat));
                addText.Replace("{{$Now}}", DateTime.Now.ToString(dateFormat));

                string NewFileName = targetFile.FullName + addText;
                targetFile.MoveTo(NewFileName);
                return ExistingFile.Rename;
            }
            catch (Exception ex)
            {
                exception = ex;
                return ExistingFile.Exception;
            }
        }

        /// <summary>
        /// Overwrite existing file
        /// </summary>
        /// <returns>Overwrite existing file in any case</returns>
        public static ExistingFile TargetOverwrite()
        {
            return ExistingFile.Overwrite;
        }

        /// <summary>
        /// Returns ouverwrite if source file has a different length
        /// </summary>
        /// <param name="sourceFile">Source file to copy</param>
        /// <param name="targetFile">Target file to copy the data to</param>
        /// <returns>Overwrite if source file has a different length</returns>
        public static ExistingFile SourceDifferentLength(FileInfo sourceFile, FileInfo targetFile)
        {
            return sourceFile.Length != targetFile.Length ? ExistingFile.Overwrite : ExistingFile.Skip;
        }

        /// <summary>
        /// Returns ouverwrite if source file is newer or has a different length
        /// </summary>
        /// <param name="sourceFile">Source file to copy</param>
        /// <param name="targetFile">Target file to copy the data to</param>
        /// <returns>Overwrite if source file is newer or has a different length</returns>
        public static ExistingFile SourceDifferentLengthOrNewer(FileInfo sourceFile, FileInfo targetFile)
        {
            return sourceFile.Length != targetFile.Length || DateTime.Compare(sourceFile.CreationTime, targetFile.CreationTime) > 0 || DateTime.Compare(sourceFile.LastWriteTime, targetFile.LastWriteTime) > 0 ? ExistingFile.Overwrite : ExistingFile.Skip;
        }

        /// <summary>
        /// Returns ouverwrite if source file is newer
        /// </summary>
        /// <param name="sourceFile">Source file to copy</param>
        /// <param name="targetFile">Target file to copy the data to</param>
        /// <returns>Overwrite if source file is newer</returns>
        public static ExistingFile SourceNewer(FileInfo sourceFile, FileInfo targetFile)
        {
            return DateTime.Compare(sourceFile.CreationTime, targetFile.CreationTime) > 0 || DateTime.Compare(sourceFile.LastWriteTime, targetFile.LastWriteTime) > 0 ? ExistingFile.Overwrite : ExistingFile.Skip;
        }

        /// <summary>
        /// Skip existing file
        /// </summary>
        /// <returns>Skip existing file in any case</returns>
        public static ExistingFile SourceSkip()
        {
            return ExistingFile.Skip;
        }
        #endregion

        #region Sub classes
        /// <summary>
        /// Class with result of show handle exisitng item file form
        /// </summary>
        public class CheckResult
        {
            /// <summary>
            /// Form result
            /// </summary>
            public DialogResult? FormResult { get; internal set; } = null;

            /// <summary>
            /// Selected action for handle existing files
            /// </summary>
            public HowToHandleExistingItem SelectedAction { get; internal set; } = HowToHandleExistingItem.AskAnyTime;

            /// <summary>
            /// Text to add to existing file
            /// </summary>
            public string AddText { get; internal set; } = string.Empty;

            /// <summary>
            /// True if the action sould been remembered
            /// </summary>
            public bool RememberAction { get; internal set; } = false;

            /// <summary>
            /// Should the exisiting file been overwritten, was renamend or occourded an exception
            /// </summary>
            public ExistingFile OverwriteFile { get; internal set; } = ExistingFile.Exception;
        }
        #endregion
    }
}
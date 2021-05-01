/*
 * OLKI.Toolbox.DirectoryAndFile
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Form to handle existing files
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

using OLKI.Toolbox.src.DirectroyAndFile.HandleExistingFiles;
using OLKI.Toolbox.Widgets;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OLKI.Toolbox.DirectoryAndFile
{
    /// <summary>
    /// A Form to set the default action for handling existing directories and files or how to handle an specific existing directory or file
    /// </summary>
    public partial class HandleExistingFilesForm : Form
    {
        #region Constants
        /// <summary>
        /// Praefix to identify action radiobuttons to sett action for existing files
        /// </summary>
        private const string ACTION_RADIOBUTTON_PRAEFIX = "rabAction_";
        /// <summary>
        /// Default selected index of cboAction_AddText_Template
        /// </summary>
        private const int ADD_TEXT_DEFAULT_COMBOBOX_INDEX = 1;
        #endregion

        #region Enums
        /// <summary>
        /// An Enumeration ths specifies the mode of the form
        /// </summary>
        public enum FormMode
        {
            /// <summary>
            /// The form mode is to set the default handling of existing directories and files
            /// </summary>
            DefaultSettings,
            /// <summary>
            /// Thte form mode is to handle an specifig existing directory or file
            /// </summary>
            FileExists
        }
        #endregion

        #region Properties
        /// <summary>
        /// Action to do with exisiting files
        /// </summary>
        private HandleExistingFiles.HowToHandleExistingItem _actionHandleExistingFiles = HandleExistingFiles.HowToHandleExistingItem.AskAnyTime;
        /// <summary>
        /// Get the action how to handle existing files, choose in this Form
        /// </summary>
        public HandleExistingFiles.HowToHandleExistingItem ActionHandleExistingFiles
        {
            get
            {
                return this._actionHandleExistingFiles;
            }
        }

        /// <summary>
        /// Get if the coosen action for handle existing files should been taken for the folowing files
        /// </summary>
        public bool RememberActionHandleExistingFiles
        {
            get
            {
                return this.chkForAllFollowing.Checked;
            }
        }

        /// <summary>
        /// The text to add to an existing file, if the option is selected
        /// </summary>
        private string _actionAddTextText = string.Empty;
        /// <summary>
        /// Get the text to add to an existing file, if the option is selected
        /// </summary>
        public string ActionAddTextText
        {
            get
            {
                return this._actionAddTextText;
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Initial a new overwrite options Form
        /// </summary>
        /// <param name="formMode">Specifies the mode of the form</param>
        /// <param name="sourceFile">File info of source file or null</param>
        /// <param name="targetFile">File info of destination file or null</param>
        /// <param name="defaultAction">Initial value for action to handle existing files</param>
        public HandleExistingFilesForm(FormMode formMode, FileInfo sourceFile, FileInfo targetFile, HandleExistingFiles.HowToHandleExistingItem defaultAction, string defaultAddText, bool defaultRememberAction)
        {
            this._actionHandleExistingFiles = defaultAction;

            InitializeComponent();

            this.cboAction_AddText_Template.SelectedIndex = ADD_TEXT_DEFAULT_COMBOBOX_INDEX;
            Tools.ComboBox.AutoDropDownWidth(this.cboAction_AddText_Template);
            // Set form mode specific options
            switch (formMode)
            {
                case FormMode.DefaultSettings:
                    this.grbDestinationProperty.Visible = false;
                    this.grbSourceProperty.Visible = false;
                    this.lblActionDefaultSettings.Visible = true;
                    this.lblActionDoubleFile.Visible = true;
                    this.rabAction_AskAnyTime.Enabled = true;

                    // Move Controles, because the conrtoles on the left site are not visible
                    int PositionDisplacement = this.grbAction.Location.X - this.grbSourceProperty.Location.X;
                    this.Width -= PositionDisplacement;
                    this.btnCancel.Location = new Point(this.btnCancel.Location.X - PositionDisplacement, this.btnCancel.Location.Y);
                    this.btnOk.Location = new Point(this.btnOk.Location.X - PositionDisplacement, this.btnOk.Location.Y);
                    this.chkForAllFollowing.Location = new Point(this.chkForAllFollowing.Location.X - PositionDisplacement, this.chkForAllFollowing.Location.Y);
                    this.grbAction.Location = new Point(this.grbAction.Location.X - PositionDisplacement, this.grbAction.Location.Y);
                    this.lblActionDefaultSettings.Location = new Point(this.lblActionDefaultSettings.Location.X - PositionDisplacement, this.lblActionDefaultSettings.Location.Y);
                    break;
                case FormMode.FileExists:
                    this.lblActionDefaultSettings.Visible = false;
                    this.lblActionDoubleFile.Visible = true;
                    this.prgSourceProperty.SelectedObject = sourceFile;
                    this.prgDestinationProperty.SelectedObject = targetFile;
                    this.rabAction_AskAnyTime.Enabled = false;
                    break;
                default:
                    break;
            }
            // Set action from settings
            // Use the name of the radiobutton and the name of the enum to find the selected item
            foreach (Control Control in this.grbAction.Controls)
            {
                if (Control is RadioButton RadioButton && Control.Name == ACTION_RADIOBUTTON_PRAEFIX + defaultAction.ToString())
                {
                    RadioButton.Checked = true;
                    break;
                }
            }

            this.chkForAllFollowing.Checked = defaultRememberAction;
            this.chkForAllFollowing.Enabled = !this.rabAction_AskAnyTime.Enabled;
            this.chkForAllFollowing.Visible = this.chkForAllFollowing.Enabled;
            this.txtAction_AddText_Text.Text = defaultAddText;

            this.rabActionXXX_CheckedChanged(this, new EventArgs());
        }

        /// <summary>
        /// Return the text of the radio button controle, associated with an specified HandleExistingItem Enumeration item
        /// </summary>
        /// <param name="enumToText">Specifies the Enumeration to associated with the radiobutton to return the text from it</param>
        /// <returns>The text of the radio button controle, associated with the specified HandleExistingItem Enumeration item</returns>
        public string GetActionAsText(HandleExistingFiles.HowToHandleExistingItem enumToText)
        {
            foreach (Control Control in this.grbAction.Controls)
            {
                if (Control is RadioButton RadioButton && Control.Name == ACTION_RADIOBUTTON_PRAEFIX + enumToText.ToString())
                {
                    return RadioButton.Text;
                }
            }
            return string.Empty;
        }

        #region Form User Events
        private void btnAction_AddText_Transfair_Click(object sender, EventArgs e)
        {
            String[] AttTextSubstrings = this.cboAction_AddText_Template.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None);
            this.txtAction_AddText_Text.Text += AttTextSubstrings[1];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.rabAction_AddText.Checked && string.IsNullOrEmpty(this.txtAction_AddText_Text.Text))
            {

                MessageBox.Show(StringTable._0x0001m, StringTable._0x0001c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void rabActionXXX_CheckedChanged(object sender, EventArgs e)
        {
            // Check for checked Items
            bool OptionIsChecked = false;
            foreach (Control Control in this.grbAction.Controls)
            {
                if (Control is RadioButton RadioButton && RadioButton.Enabled && RadioButton.Checked)
                {
                    this._actionHandleExistingFiles = (HandleExistingFiles.HowToHandleExistingItem)Enum.Parse(typeof(HandleExistingFiles.HowToHandleExistingItem), Control.Name.Replace(ACTION_RADIOBUTTON_PRAEFIX, string.Empty));

                    // Set an checked option
                    OptionIsChecked = true;
                    break;
                }
            }
            this.btnOk.Enabled = OptionIsChecked;

            // Set AddText controles
            if (this.rabAction_AddText.Checked)
            {
                this.txtAction_AddText_Text.Enabled = true;
                this.btnAction_AddText_Transfair.Enabled = true;
                this.cboAction_AddText_Template.Enabled = true;
            }
            else
            {
                this.txtAction_AddText_Text.Enabled = false;
                this.btnAction_AddText_Transfair.Enabled = false;
                this.cboAction_AddText_Template.Enabled = false;
            }

        }

        private void txtAction_AddText_Text_TextChanged(object sender, EventArgs e)
        {
            this._actionAddTextText = this.txtAction_AddText_Text.Text;
        }
        #endregion
        #endregion
    }
}
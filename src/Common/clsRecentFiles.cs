/*
 * OLKI.Toolbox.Common
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provieds a recent file list and tools for it
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
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OLKI.Toolbox.Common
{
    /// <summary>
    /// Class that provieds a recent file list and tools for it
    /// </summary>
    public class RecentFiles
    {
        #region Constants
        /// <summary>
        /// Specifies the default seperator for Converting the list to a string
        /// </summary>
        private const string DEFAULT_SEPERATUR = "|";
        /// <summary>
        /// Specifies the defaukt maximum length of the file list, elements older thand the maximum length will be delited if a new element will be addes
        /// </summary>
        private const int DEFAULT_MAX_LENGHT = 4;
        #endregion

        #region Events
        /// <summary>
        /// Occours if the file list is changed by adding or removing elements
        /// </summary>
        public event EventHandler Changed_Event;
        #endregion

        #region Members
        /// <summary>
        /// Seperator for Converting the list to a string
        /// </summary>
        private string _seperator = DEFAULT_SEPERATUR;
        /// <summary>
        /// Get or set the Seperator for Converting the list to a string
        /// </summary>
        public string Seperator
        {
            get
            {
                return this._seperator;
            }
            set
            {
                this._seperator = value;
            }
        }

        /// <summary>
        /// List will recent files
        /// </summary>
        private List<string> _fileList = null;
        /// <summary>
        /// Get or set the list will recent files
        /// </summary>
        public List<string> FileList
        {
            get
            {
                return this._fileList;
            }
            set
            {
                this._fileList = value;
            }
        }

        /// <summary>
        /// Maximum length of the file list, elements older thand the maximum length will be delited if a new element will be addes
        /// </summary>
        private int _maxLength = DEFAULT_MAX_LENGHT;
        /// <summary>
        /// Get or set the maximum length of the file list, elements older thand the maximum length will be delited if a new element will be addes
        /// </summary>
        public int MaxLength
        {
            get
            {
                return this._maxLength;
            }
            set
            {
                this._maxLength = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initalise a new instans of recent files with an empty list an maximum length of 4 elements
        /// </summary>
        public RecentFiles()
            : this(null, DEFAULT_MAX_LENGHT)
        {
        }
        /// <summary>
        /// Initalise a new instans of recent files with an specified list specified maximum length
        /// </summary>
        /// <param name="fielsList">Specifies an initial recent file list</param>
        /// <param name="maxLength">Specifies the maximum length of the recent file list</param>
        public RecentFiles(List<string> fielsList, int maxLength)
        {
            if (fielsList == null)
            {
                this._fileList = new List<string> { };
            }
            else
            {
                this._fileList = fielsList;
            }
            this._maxLength = maxLength;
        }

        #region AddToList
        /// <summary>
        /// Adds a new recent file element to the list
        /// </summary>
        /// <param name="path">An null-terminated string that specifies the file path to add to the file list</param>
        public void AddToList(string path)
        {
            // If list is "null" then create new list
            if (this._fileList == null)
            {
                this._fileList = new List<string>();
            }

            int LooopStart;
            if (!this._fileList.Contains(path))
            // Element IS NOT in list
            {
                // If List is shorter then max lenght, Add item
                if (this._fileList.Count < this._maxLength)
                {
                    // Add Element
                    this._fileList.Add(string.Empty);
                }
                LooopStart = this._fileList.Count - 1;
            }
            else
            // Element IS IN list
            {
                //Find position and move elements above
                LooopStart = this._fileList.IndexOf(path);
            }
            // Move old entrys
            for (int i = LooopStart; i > 0; i--)
            {
                this._fileList[i] = this._fileList[i - 1];
            }
            // Add new Element as first item
            this._fileList[0] = path;
            if (this.Changed_Event != null)
            {
                Changed_Event(this, new EventArgs());
            }
        }
        #endregion

        #region GetAsString
        /// <summary>
        /// Get the file list as string with an "|" as seperator
        /// </summary>
        /// <returns>The file list as string with an "|" as seperator</returns>
        public string GetAsString()
        {
            return this.GetAsString(this._seperator);
        }
        /// <summary>
        /// Get the file list as string with an specified seperator
        /// </summary>
        /// <param name="seperator">Specifies an seperator to export the filelist in a string</param>
        /// <returns>The file list as string with an specified sperator</returns>
        public string GetAsString(string seperator)
        {
            return String.Join(seperator, this._fileList.ToArray());
        }
        #endregion

        #region SetFromString
        /// <summary>
        /// Converts an specified string to a recent file list, using "|" as seperator
        /// </summary>
        /// <param name="list">An null-terminated string that specifies the file list with "|" as seperator</param>
        public void SetFromString(string list)
        {
            this.SetFromString(list, this._seperator);

        }
        /// <summary>
        /// Converts an specified string to a recent file list
        /// </summary>
        /// <param name="list">An null-terminated string that specifies the file list with an specified seperator</param>
        /// <param name="setepator">An null-terminated string that specifies the seperator for the specified file list</param>
        public void SetFromString(string list, string setepator)
        {
            if (string.IsNullOrEmpty(list))
            {
                this._fileList = new List<string> { };
            }
            else
            {
                this._fileList = list.Split(new string[] { setepator }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            if (this.Changed_Event != null)
            {
                Changed_Event(this, new EventArgs());
            }
        }
        #endregion

        #region SetMenueItem
        /// <summary>
        /// Set text, enabled and visible state of an specified menue item by an specified recent file list entry
        /// </summary>
        /// <param name="menuItems">Specifies a list with menue item to set for recent files</param>
        public void SetMenueItem(List<ToolStripMenuItem> menuItems)
        {
            this.SetMenueItem(menuItems, null);
        }
        /// <summary>
        /// Set text, enabled and visible state of an specified menue item and his root item by an specified recent file list entry
        /// </summary>
        /// <param name="menuItems">Specifies a list with menue item to set for recent files</param>
        /// <param name="rootMenuItem">Specifies the root menue of the menue item to set</param>
        public void SetMenueItem(List<ToolStripMenuItem> menuItems, ToolStripMenuItem rootMenuItem)
        {
            this.SetMenueItem(menuItems, rootMenuItem, null);
        }
        /// <summary>
        /// Set text, enabled and visible state of an specified menue item and his root item and his seperator by an specified recent file list entry
        /// </summary>
        /// <param name="index">Specifies the index of the recent file list item to set the menue item</param>
        /// <param name="menuItems">Specifies a list with menue item to set for recent files</param>
        /// <param name="rootMenuItem">Specifies the root menue of the menue item to set</param>
        /// <param name="seperatorItem">Specifies the seperator of the root menue of the menue item to set</param>
        public void SetMenueItem(List<ToolStripMenuItem> menuItems, ToolStripMenuItem rootMenuItem, ToolStripSeparator seperatorItem)
        {
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == 0)
                {
                    this.SetMenueItem(i, menuItems[i], rootMenuItem, seperatorItem);
                }
                else
                {
                    this.SetMenueItem(i, menuItems[i]);
                }
            }
        }

        /// <summary>
        /// Set text, enabled and visible state of an specified menue item by an specified recent file list entry
        /// </summary>
        /// <param name="index">Specifies the index of the recent file list item to set the menue item</param>
        /// <param name="menuItem">Specifies the menue item to set</param>
        public void SetMenueItem(int index, ToolStripMenuItem menuItem)
        {
            this.SetMenueItem(index, menuItem, null);
        }
        /// <summary>
        /// Set text, enabled and visible state of an specified menue item and his root item by an specified recent file list entry
        /// </summary>
        /// <param name="index">Specifies the index of the recent file list item to set the menue item</param>
        /// <param name="menuItem">Specifies the menue item to set</param>
        /// <param name="rootMenuItem">Specifies the root menue of the menue item to set</param>
        public void SetMenueItem(int index, ToolStripMenuItem menuItem, ToolStripMenuItem rootMenuItem)
        {
            this.SetMenueItem(index, menuItem, rootMenuItem, null);
        }
        /// <summary>
        /// Set text, enabled and visible state of an specified menue item and his root item and his seperator by an specified recent file list entry
        /// </summary>
        /// <param name="index">Specifies the index of the recent file list item to set the menue item</param>
        /// <param name="menuItem">Specifies the menue item to set</param>
        /// <param name="rootMenuItem">Specifies the root menue of the menue item to set</param>
        /// <param name="seperatorItem">Specifies the seperator of the root menue of the menue item to set</param>
        public void SetMenueItem(int index, ToolStripMenuItem menuItem, ToolStripMenuItem rootMenuItem, ToolStripSeparator seperatorItem)
        {
            if (this._maxLength > 0 && this._maxLength > index && this._fileList.Count > index && !string.IsNullOrEmpty(this._fileList[index]))
            {
                menuItem.Text = this._fileList[index];
                menuItem.Visible = true;
            }
            else
            {
                menuItem.Visible = false;
            }
            if (rootMenuItem != null)
            {
                bool RootAndSeperatorMenuItem_Visible = false;
                foreach (string Item in this._fileList)
                {
                    if (!string.IsNullOrEmpty(Item))
                    {
                        RootAndSeperatorMenuItem_Visible = true;
                        break; //Nothing more to do
                    }
                }
                rootMenuItem.Visible = RootAndSeperatorMenuItem_Visible;
                if (seperatorItem != null) seperatorItem.Visible = RootAndSeperatorMenuItem_Visible;
            }
        }
        #endregion
        #endregion
    }
}
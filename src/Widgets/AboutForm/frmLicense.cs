/*
 * OLKI.Toolbox.Widgets
 * 
 * Copyright:   Oliver Kind - 2021
 * License:     LGPL
 * 
 * Desctiption:
 * Form to show license information
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
using System.Windows.Forms;

namespace OLKI.Toolbox.Widgets.AboutForm
{
    /// <summary>
    /// A form to show license information
    /// </summary>
    public partial class License : Form
    {
        #region Methods
        /// <summary>
        /// Initial a new Form to show a single license
        /// </summary>
        /// <param name="licenseText">License to show in form</param>
        public License(string licenseText)
        {
            InitializeComponent();
            this.txtLicense.Text = licenseText;
        }

        private void License_Shown(object sender, EventArgs e)
        {
            this.txtLicense.SelectionLength = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
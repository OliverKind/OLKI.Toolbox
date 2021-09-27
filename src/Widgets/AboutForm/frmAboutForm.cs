/*
 * OLKI.Toolbox.Widgets
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * A standard About form for an application
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
using System.Reflection;
using System.Windows.Forms;

namespace OLKI.Toolbox.Widgets.AboutForm
{
    /// <summary>
    /// A form to show informations about the application
    /// </summary>
    public partial class AboutForm : Form
    {
        #region Constants
        /// <summary>
        /// Specifies the name of the application version
        /// </summary>
        private const string VERSION_NAME = "Peter";
        #endregion

        #region Properties
        /// <summary>
        /// Get the title information from assembly settiongs
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = this._hostAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "") return titleAttribute.Title;
                }
                return System.IO.Path.GetFileNameWithoutExtension(this._hostAssembly.CodeBase);
            }
        }

        /// <summary>
        /// Get the version information from assembly settiongs
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return this._hostAssembly.GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Get the description from assembly settiongs
        /// </summary>
        public string AssemblyDescription
        {
            get
            {
                object[] attributes = this._hostAssembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0) return "";
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// Get the product information from assembly settiongs
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                object[] attributes = this._hostAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0) return "";
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Get the copyright information from assembly settiongs
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = this._hostAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0) return "";
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// Get the company information from assembly settiongs
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                object[] attributes = this._hostAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0) return "";
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        /// <summary>
        /// Set the Credits string
        /// </summary>
        public string Credits { private get; set; } = "";

        /// <summary>
        /// Set the Path to the Licence directroy to open if the LicenceButton is Clicked and LicenseText is not set
        /// </summary>
        public string LicenseDirectory { private get; set; } = "";

        /// <summary>
        /// Set the License Text to show if the LicenceButton is Clicked and LicenseDirectory is not set
        /// </summary>
        public string LicenseText { private get; set; } = "";
        #endregion

        #region Members
        /// <summary>
        /// Host Application Assembly for the AboutForm
        /// </summary>
        private Assembly _hostAssembly;
        #endregion

        #region Methods
        /// <summary>
        /// Initial a new AboutForm
        /// </summary>
        /// <param name="hostAssembly">Host Application for the AboutForm</param>
        /// <param name="imgApp">Application Icon to show or NULL</param>
        /// <param name="imgFile">Associated File Icon to show or NULL</param>
        public AboutForm(Assembly hostAssembly, System.Drawing.Image imgApp, System.Drawing.Image imgFile)
        {


            InitializeComponent();
            this._hostAssembly = hostAssembly;

            this.Text = string.Format(this.Text, this.AssemblyTitle);
            this.lblProductName.Text = string.Format(this.lblProductName.Text, this.AssemblyProduct);
            this.lblVersion.Text = string.Format(this.lblVersion.Text, this.AssemblyVersion);
            this.lblCopyright.Text = string.Format(this.lblCopyright.Text, this.AssemblyCopyright);
            this.lblCompanyName.Text = string.Format(this.lblCompanyName.Text, this.AssemblyCompany);
            this.txtDescription.Text = this.AssemblyDescription;
            this.txtCredits.Text = this.Credits;

            this.pbxLogoApplication.Image = imgApp;
            this.pbxLogoFile.Image = imgFile;
        }

        /// <summary>
        /// Show the about form
        /// </summary>
        /// <param name="owner">Any object that implements IWin32Window and represents the top-level window that will own this form.</param>
        public new void Show(IWin32Window owner)
        {
            this.txtCredits.Text = Credits;
            if (string.IsNullOrEmpty(this.LicenseDirectory) && string.IsNullOrEmpty(this.LicenseText))
            {
                this.btnGoToLicenses.Enabled = false;
                this.btnGoToLicenses.Visible = false;
            }
            base.Show(owner);
        }

        /// <summary>
        /// Show the about form
        /// </summary>
        /// <returns>One of the DialogResult values.</returns>
        public new DialogResult ShowDialog()
        {
            return this.ShowDialog(null);
        }

        /// <summary>
        /// Show the about form
        /// </summary>
        /// <param name="owner">Any object that implements IWin32Window and represents the top-level window that will own this form.</param>
        /// <returns>One of the DialogResult values.</returns>
        public new DialogResult ShowDialog(IWin32Window owner)
        {
            this.txtCredits.Text = Credits;
            if (string.IsNullOrEmpty(this.LicenseDirectory) && string.IsNullOrEmpty(this.LicenseText))
            {
                this.btnGoToLicenses.Enabled = false;
                this.btnGoToLicenses.Visible = false;
            }
            return base.ShowDialog(owner);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGoToLicenses_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.LicenseDirectory) && string.IsNullOrEmpty(this.LicenseText))
            {
                Toolbox.DirectoryAndFile.Directory.Open(this.LicenseDirectory, false);
                return;
            }

            if (string.IsNullOrEmpty(this.LicenseDirectory) && !string.IsNullOrEmpty(this.LicenseText))
            {
                License LicenseForm = new License(this.LicenseText);
                LicenseForm.Show(this);
                return;
            }

            throw new ArgumentException("No license directory and no license text or both is defined");
        }
        #endregion
    }
}
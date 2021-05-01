/*
 * OLKI.Toolbox.UpdateApp
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Form to show the latest Version of an Application and Download it
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

using OLKI.Toolbox.src.UpdateApp;
using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;

namespace OLKI.Toolbox.UpdateApp
{
    /// <summary>
    /// Form to show the latest Version of an Application and Download it
    /// </summary>
    public partial class UpdateForm : Form
    {
        #region Fields
        /// <summary>
        /// WebClient Instance to download new Version SetupFile
        /// </summary>
        private WebClient _downloader;

        /// <summary>
        /// ReleaseData of the latest Version
        /// </summary>
        private readonly ReleaseData _releaseData;

        /// <summary>
        /// Path to download the new Version SetupFile
        /// </summary>
        private readonly string _setupPath;
        #endregion

        #region Methodes
        /// <summary>
        /// Initial a new UpdateForm
        /// </summary>
        /// <param name="releaseData">Data for the last Relase</param>
        /// <param name="currentVersion">Current Version of the Application</param>
        public UpdateForm(ReleaseData releaseData, string currentVersion)
        {
            InitializeComponent();

            this._releaseData = releaseData;

            this.lblCurrentVersion.Text = string.Format(this.lblCurrentVersion.Text, new object[] { currentVersion });
            this.lblNewVersion.Text = string.Format(this.lblNewVersion.Text, new object[] { this._releaseData.Version.TagName });
            this.txtChangeLog.Text = this._releaseData.ChangeLog.Replace("\n", "\r\n");
            this.expDownload.MaxValue = this._releaseData.SetupAsset.Size;
            this.expDownload.Value = 0;

            this._setupPath = System.IO.Path.GetTempPath() + this._releaseData.SetupAsset.Name;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnInstallUpdate_Click(object sender, EventArgs e)
        {
            if (this._downloader != null && this._downloader.IsBusy) return;

            this.btnCancel.Enabled = false;
            this.btnInstallUpdate.Enabled = false;

            this._downloader = new WebClient();
            this._downloader.DownloadProgressChanged += this.Downloader_DownloadProgressChanged;
            this._downloader.DownloadFileCompleted += Downloader_DownloadProgressChanged_DownloadFileCompleted;
            this._downloader.DownloadFileAsync(new Uri(this._releaseData.SetupAsset.BrowserDownloadUrl), this._setupPath);
        }

        private void Downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.expDownload.Value = e.BytesReceived;
        }

        private void Downloader_DownloadProgressChanged_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show(this, Stringtable._0x0001m, Stringtable._0x0001c, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            if (e.Error != null)
            {
                MessageBox.Show(this, string.Format(Stringtable._0x0002m, new object[] { e.Error.Message }), Stringtable._0x0002c, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.DialogResult = DialogResult.Abort;
                return;
            }

            MessageBox.Show(this, string.Format(Stringtable._0x0003m, new object[] { this._setupPath }), Stringtable._0x0003c, MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            System.Diagnostics.Process.Start(this._setupPath);

            this.btnCancel.Enabled = true;
            this.btnInstallUpdate.Enabled = true;
            this.Close();
        }
        #endregion;
    }
}
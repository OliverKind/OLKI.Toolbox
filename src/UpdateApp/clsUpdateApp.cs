/*
 * OLKI.Toolbox.UpdateApp
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to update an application
 * - Get latest version
 * - Compar Version
 * - Download installer for latest verion
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

using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;

namespace OLKI.Toolbox.UpdateApp
{
    /// <summary>
    /// Class that provides tool to update an application
    /// </summary>
    public class UpdateApp
    {
        #region Constants
        private const string PRODUCT_HEADER_VALUE = "{0}-Update-Check";
        #endregion

        #region Methodes
        /// <summary>
        /// Get all Date (to Update an Application) of the last Relase of an Repository
        /// </summary>
        /// <param name="owner">Repository Owner</param>
        /// <param name="name">Repository Name</param>
        /// <param name="changeLogPattern">Pattern to identify the ChangeLog File</param>
        /// <param name="setupFilePattern">Pattern to identify the Setup Asset</param>
        /// <returns>Date of the last Release</returns>
        public ReleaseData GetLastReleaseData(string owner, string name, string changeLogPattern, string setupFilePattern)
        {
            Task<ReleaseData> ReleaseDataTask = Task.Run(async () => await this.GetLastReleaseDataAsync(owner, name, changeLogPattern, setupFilePattern));
            return ReleaseDataTask.Result;
        }

        /// <summary>
        /// Get all Date (to Update an Application) of the last Relase of an Repository, asynchronously
        /// </summary>
        /// <param name="owner">Repository Owner</param>
        /// <param name="name">Repository Name</param>
        /// <param name="changeLogPattern">Pattern to identify the ChangeLog File</param>
        /// <param name="setupFilePattern">Pattern to identify the Setup Asset</param>
        /// <returns>Date of the last Release</returns>
        public async Task<ReleaseData> GetLastReleaseDataAsync(string owner, string name, string changeLogPattern, string setupFilePattern)
        {
            ReleaseData ReleaseData = new ReleaseData();
            Release Release;

            GitHubClient GitHubClient = new GitHubClient(new Octokit.ProductHeaderValue(string.Format(PRODUCT_HEADER_VALUE, name)));

            //Get last Release
            Release = await this.GetLastReleaseAsync(owner, name, GitHubClient);
            ReleaseData.Version = new ReleaseVersion(Release.TagName);

            //Get Changelog, Async
            Task<IReadOnlyList<RepositoryContent>> ChangeLogTask = null;
            if (!string.IsNullOrEmpty(changeLogPattern))
            {
                ChangeLogTask = GitHubClient.Repository.Content.GetAllContents(owner, name, changeLogPattern);
            }


            //Get Asset Data, Async
            Task<ReleaseAsset> SetupFileTask = null;
            if (!string.IsNullOrEmpty(setupFilePattern))
            {
                setupFilePattern = string.Format(setupFilePattern, new object[] { ReleaseData.Version.TagName });
                SetupFileTask = this.GetSetupAssetAsync(Release, setupFilePattern);
            }
            //Get Asyn Data
            if (!string.IsNullOrEmpty(changeLogPattern)) ReleaseData.ChangeLog = (await ChangeLogTask)[0].Content;
            if (!string.IsNullOrEmpty(setupFilePattern)) ReleaseData.SetupAsset = await SetupFileTask;

            return ReleaseData;
        }

        /// <summary>
        /// Get the last Relase of an Repository
        /// </summary>
        /// <param name="owner">Repository Owner</param>
        /// <param name="name">Repository Name</param>
        /// <param name="client">GitHubClient instance to get the last Relase</param>
        /// <returns>The Data of the last Relase</returns>
        public Release GetLastRelease(string owner, string name, GitHubClient client)
        {
            Task<Release> ReleaseTask = Task.Run(async () => await this.GetLastReleaseAsync(owner, name, client));
            return ReleaseTask.Result;
        }

        /// <summary>
        /// Get the last Relase of an Repository, asynchronously
        /// </summary>
        /// <param name="owner">Repository Owner</param>
        /// <param name="name">Repository Name</param>
        /// <param name="client">GitHubClient instance to get the last Relase</param>
        /// <returns>The Data of the last Relase</returns>
        public async Task<Release> GetLastReleaseAsync(string owner, string name, GitHubClient client)
        {
            IReadOnlyList<Release> Releases = await client.Repository.Release.GetAll(owner, name);
            return Releases[0];
        }

        /// <summary>
        /// Get the SetupAsses from all Assets of an defined release
        /// </summary>
        /// <param name="release">Release to search of the Setup Asset</param>
        /// <param name="pattern">Pattern to identify the Setup Asset</param>
        /// <returns>The Setup Asset or NULL if there is not an Setup Asset</returns>
        public ReleaseAsset GetSetupAsset(Release release, string pattern)
        {
            Task<ReleaseAsset> AssetTask = Task.Run(async () => await this.GetSetupAssetAsync(release, pattern));
            return AssetTask.Result;
        }

        /// <summary>
        /// Get the SetupAsses from all Assets of an defined release, asynchronously
        /// </summary>
        /// <param name="release">Release to search of the Setup Asset</param>
        /// <param name="pattern">Pattern to identify the Setup Asset</param>
        /// <returns>The Setup Asset or NULL if there is not an Setup Asset</returns>
        public Task<ReleaseAsset> GetSetupAssetAsync(Release release, string pattern)
        {
            foreach (ReleaseAsset AssetItem in release.Assets)
            {
                if (AssetItem.Name == pattern) return Task.FromResult(AssetItem);
            }
            return null;
        }
        #endregion
    }
}
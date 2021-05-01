/*
 * OLKI.Toolbox.UpdateApp
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Date for the latest Release
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

using Octokit;

namespace OLKI.Toolbox.UpdateApp
{
    /// <summary>
    /// Date for the latest Release
    /// </summary>
    public class ReleaseData
    {
        /// <summary>
        /// Get or set the ChangeLog for ab application
        /// </summary>
        public string ChangeLog { get; set; }

        /// <summary>
        /// Get or set the AssetDate for the latest Application version
        /// </summary>
        public ReleaseAsset SetupAsset { get; set; }
         
        /// <summary>
        /// Get or set Version Information
        /// </summary>
        public ReleaseVersion Version { get; set; }
    }
}
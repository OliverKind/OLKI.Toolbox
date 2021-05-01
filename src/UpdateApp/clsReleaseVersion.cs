/*
 * OLKI.Toolbox.UpdateApp
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Version Data for a release
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
using System.Text.RegularExpressions;

namespace OLKI.Toolbox.UpdateApp
{
    /// <summary>
    /// Version Data for a release
    /// </summary>
    public class ReleaseVersion
    {
        #region Enums
        /// <summary>
        /// Result of comparing Application Versions
        /// </summary>
        public enum VersionCompare
        {
            Lower = -1,
            Even = 0,
            Higher = 1
        };
        #endregion

        #region Properties
        /// <summary>
        /// Get the full name of the Version Tag
        /// </summary>
        public string TagName { get; }

        /// <summary>
        /// Get the Major-Number of the Version
        /// </summary>
        public uint Major { get; }

        /// <summary>
        /// Get the Minor-Number of the Version
        /// </summary>
        public uint Minor { get; }

        /// <summary>
        /// Get the Revision-Number of the Version
        /// </summary>
        public uint Revision { get; }

        /// <summary>
        /// Get the Build-Number of the Version
        /// </summary>
        public uint Build { get; }
        #endregion

        #region Methodes
        /// <summary>
        /// Initial a new ReleaseVersion
        /// </summary>
        /// <param name="tag"></param>
        public ReleaseVersion(string tag)
        {
            if (string.IsNullOrEmpty(tag)) return;
            string[] TagSplit = tag.Split('.');

            this.TagName = tag;
            if (TagSplit.Length > 0 && Regex.Replace(TagSplit[0], "[^0-9]", "").Length > 0) this.Major = Convert.ToUInt16(Regex.Replace(TagSplit[0], "[^0-9]", ""));
            if (TagSplit.Length > 1 && Regex.Replace(TagSplit[1], "[^0-9]", "").Length > 0) this.Minor = Convert.ToUInt16(Regex.Replace(TagSplit[1], "[^0-9]", ""));
            if (TagSplit.Length > 2 && Regex.Replace(TagSplit[2], "[^0-9]", "").Length > 0) this.Revision = Convert.ToUInt16(Regex.Replace(TagSplit[2], "[^0-9]", ""));
            if (TagSplit.Length > 3 && Regex.Replace(TagSplit[3], "[^0-9]", "").Length > 0) this.Build = Convert.ToUInt16(Regex.Replace(TagSplit[3], "[^0-9]", ""));
        }

        /// <summary>
        /// Compare the Version with an defindet Version, if it is newer
        /// </summary>
        /// <param name="compareWith">Version to compare</param>
        /// <returns>Return if Version is Higher, Even or Lower than the compareWith-Version</returns>
        public VersionCompare Compare(ReleaseVersion compareWith)
        {
            if (this.Major > compareWith.Major) return VersionCompare.Higher;
            if (this.Major < compareWith.Major) return VersionCompare.Lower;

            if (this.Minor > compareWith.Minor) return VersionCompare.Higher;
            if (this.Minor < compareWith.Minor) return VersionCompare.Lower;

            if (this.Revision > compareWith.Revision) return VersionCompare.Higher;
            if (this.Revision < compareWith.Revision) return VersionCompare.Lower;

            if (this.Build > compareWith.Build) return VersionCompare.Higher;
            if (this.Build < compareWith.Build) return VersionCompare.Lower;

            return VersionCompare.Even;
        }
        #endregion
    }
}
/*
 * OLKI.Toolbox.Common
 * 
 * Initial Author: Oliver Kind - 2024
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to valid something
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
using System.Globalization;
using System.Text.RegularExpressions;

namespace OLKI.Toolbox.Common
{
    /// <summary>
    /// Class that provides tool to valid something
    /// </summary>
    public class Validation
    {
        #region Methods
        /// <summary>
        /// Get if the given stirng is a valid E-Mail Adress
        /// </summary>
        /// <param name="eMail">Uri to check if it is a valid E-Mail Adress</param>
        /// <returns>True if the given string is a valid E-Mail Adress, otherwise false</returns>
        public static bool IsValidEMail(string eMail)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(eMail)) return false;
                // Normalize the domain
                eMail = Regex.Replace(eMail, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    IdnMapping idn = new IdnMapping();
                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
                return Regex.IsMatch(eMail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (Exception ex)
            {
                _ = ex;
                return false;
            }
        }

        /// <summary>
        /// Get if the given stirng is a valid Uri
        /// </summary>
        /// <param name="uri">Uri to check if it is a valid Uri</param>
        /// <returns>True if the given string is a valid Uri, otherwise false</returns>
        public static bool IsValidUri(string uri)
        {
            try
            {
                Uri Uri = null;
                return uri.Length > 0 && Uri.TryCreate(uri, UriKind.Absolute, out Uri);
            }
            catch (Exception ex)
            {
                _ = ex;
                return false;
            }
        }
        #endregion
    }
}
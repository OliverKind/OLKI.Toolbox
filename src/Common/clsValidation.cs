﻿/*
 * OLKI.Toolbox.Common
 * 
 * Initial Author: Oliver Kind - 2024
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tools to valid something
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
using System.IO;
using System.Text.RegularExpressions;

namespace OLKI.Toolbox.Common
{
    /// <summary>
    /// Class that provides tool to valid something
    /// </summary>
    public static class Validation
    {
        #region Methods
        /// <summary>
        /// Get if the given stirng is a valid Date
        /// </summary>
        /// <param name="date">String to check if it is a valid Date</param>
        /// <returns>True if the given Date is a valid date</returns>
        public static bool IsValidDate(string date)
        {
            return IsValidDate(date, false);
        }
        /// <summary>
        /// Get if the given stirng is a valid Date
        /// </summary>
        /// <param name="date">String to check if it is a valid Date</param>
        /// <param name="allowEmpty">Allow empty strings a valid, after removing all non numerical characters</param>
        /// <returns>True if the given Date is a valid date</returns>
        public static bool IsValidDate(string date, bool allowEmpty)
        {
            return IsValidDate(date, allowEmpty, out _);
        }
        /// <summary>
        /// Get if the given stirng is a valid Date
        /// </summary>
        /// <param name="date">String to check if it is a valid Date</param>
        /// <param name="allowEmpty">Allow empty strings a valid, after removing all non numerical characters</param>
        /// <param name="parsedDateOut">The parsed Date or Null if the Date is not valid</param>
        /// <returns>True if the given Date is a valid date</returns>
        public static bool IsValidDate(string date, out DateTime? parsedDateOut)
        {
            return IsValidDate(date, false, out parsedDateOut);
        }
        /// <summary>
        /// Get if the given stirng is a valid Date
        /// </summary>
        /// <param name="date">String to check if it is a valid Date</param>
        /// <param name="allowEmpty">Allow empty strings a valid, after removing all non numerical characters</param>
        /// <param name="parsedDateOut">The parsed Date or Null if the Date is not valid</param>
        /// <returns>True if the given Date is a valid date</returns>
        public static bool IsValidDate(string date, bool allowEmpty, out DateTime? parsedDateOut)
        {
            parsedDateOut = null;
            string NumString = Regex.Replace(date, @"[^0-9]", "");               //Remove all nun numerical chars
            if (allowEmpty && string.IsNullOrEmpty(NumString)) return true;      //Return true if the given string, after removing all non numerical chars, is empty
            if (string.IsNullOrEmpty(NumString)) return false;                   //Return False if the given string, after removing all non numerical chars, is empty
            if (!DateTime.TryParse(date, out DateTime ParsedDate)) return false; //Return False if the date can not validated

            parsedDateOut = ParsedDate;
            return true;
        }

        /// <summary>
        /// Get if the path is valid
        /// </summary>
        /// <param name="path">Path to validate</param>
        /// <returns>True if the path is valid and not empty</returns>
        public static bool IsValidDirectory(string path)
        {
            return IsValidDirectory(path, false);
        }
        /// <summary>
        /// Get if the path is valid
        /// </summary>
        /// <param name="path">Path to validate</param>
        /// <param name="emptyIsValid">Should an empty Path be valid</param>
        /// <returns>True if the path is valid or empty, if wanted</returns>
        public static bool IsValidDirectory(string path, bool emptyIsValid)
        {
            if (string.IsNullOrEmpty(path) && emptyIsValid) return true;
            if (string.IsNullOrEmpty(path) && !emptyIsValid) return false;
            try
            {
                return new DirectoryInfo(path).Exists;
            }
            catch (Exception ex)
            {
                _ = ex;
            }
            return false;
        }

        /// <summary>
        /// Get if the given stirng is a valid E-Mail Adress
        /// </summary>
        /// <param name="eMailString">String to check if it is a valid E-Mail Adress</param>
        /// <returns>True if the given string is a valid E-Mail Adress, otherwise false</returns>
        /// <see cref="https://learn.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format"/>
        public static bool IsValidEMail(string eMailString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(eMailString)) return false;
                // Normalize the domain
                eMailString = Regex.Replace(eMailString, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match Match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    IdnMapping IdnMapping = new IdnMapping();
                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string DomainName = IdnMapping.GetAscii(Match.Groups[2].Value);
                    return Match.Groups[1].Value + DomainName;
                }
                return Regex.IsMatch(eMailString, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
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
        /// <param name="uriString">String to check if it is a valid Uri</param>
        /// <returns>True if the given string is a valid Uri, otherwise false</returns>
        public static bool IsValidUri(string uriString)
        {
            return IsValidUri(uriString, out _);
        }

        /// <summary>
        /// Get if the given stirng is a valid Uri
        /// </summary>
        /// <param name="uriString">String to check if it is a valid Uri</param>
        /// <param name="uri">Uri formated</param>
        /// <returns>True if the given string is a valid Uri, otherwise false</returns>
        public static bool IsValidUri(string uriString, out Uri uri)
        {
            uri = null;
            try
            {
                return uriString.Length > 0 && Uri.TryCreate(uriString, UriKind.Absolute, out uri);
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
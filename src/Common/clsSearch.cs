/*
 * OLKI.Toolbox.Common
 * 
 * Initial Author: Oliver Kind - 2024
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tools to search in text and lsts
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

namespace OLKI.Toolbox.Common
{
    /// <summary>
    /// Class that provides tools to search in text and lsts
    /// </summary>
    public static class Search
    {
        #region Methods
        /// <summary>
        /// Search if the needle Text is inside the haystack Text
        /// </summary>
        /// <param name="haystack">Text to search in</param>
        /// <param name="needle">Text to search for</param>
        /// <returns>True if needle Text is inside the haystack Text or if the search Text is empty, otherweise False, or False if the Haystack is emptý</returns>
        public static bool FullText(string haystack, string needle)
        {
            if (string.IsNullOrEmpty(needle)) return true;
            needle = needle.Trim();
            if (string.IsNullOrEmpty(needle)) return true;
            if (string.IsNullOrEmpty(haystack)) return false;
            if (haystack.IndexOf(needle, StringComparison.InvariantCultureIgnoreCase) < 0) return false;
            return true;
        }

        /// <summary>
        /// Search if the one of the needle Elements is inside the list of haystack Numbers
        /// </summary>
        /// <param name="haystack">A list wiht Numbers to search in</param>
        /// <param name="needle">Number to search for</param>
        /// <returns>True if needle Number is inside the haystack Numbers or if the search Numbers is empty, otherweise False, or False if the Haystack is emptý</returns>
        public static bool List(List<int> haystack, int needle)
        {
            if (needle == 0) return true;
            if (haystack.Contains(needle)) return true;
            return false;
        }

        /// <summary>
        /// Search if the one of the needle Elements is inside the list of haystack Numbers
        /// </summary>
        /// <param name="haystack">A list wiht Numbers to search in</param>
        /// <param name="needle">Numbers to search for</param>
        /// <returns>True if needle Numbers is inside the haystack Numbers or if the search Numbers is empty, otherweise False, or False if the Haystack is emptý</returns>
        public static bool List(List<int> haystack, List<int> needle)
        {
            if (needle == null || needle.Count == 0) return true;
            foreach (int NeedleItem in needle)
            {
                if (haystack.Contains(NeedleItem)) return true;
            }
            return false;
        }

        /// <summary>
        /// Search if the needle Text is inside the list of haystack Texts
        /// </summary>
        /// <param name="haystack">A list wiht text to search in</param>
        /// <param name="needle">Text to search for</param>
        /// <returns>True if needle Text is inside the haystack Text or if the search Text is empty, otherweise False, or False if the Haystack is emptý</returns>
        public static bool List(List<string> haystack, string needle)
        {
            if (string.IsNullOrEmpty(needle)) return true;
            needle = needle.Trim();
            if (string.IsNullOrEmpty(needle)) return true;
            foreach (string HaystackItem in haystack)
            {
                if (Search.FullText(System.Text.RegularExpressions.Regex.Replace(HaystackItem, @"\W+", ""), needle)) return true;
            }
            return false;
        }
        #endregion
    }
}
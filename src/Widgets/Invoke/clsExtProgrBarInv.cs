﻿/*
 * OLKI.Widgets
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Set ProgressBar properties by invoking, if required
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

using OLKI.Toolbox.Widgets;
using System;
using System.Windows.Forms;

namespace OLKI.Toolbox.Widgets.Invoke
{
    /// <summary>
    /// Set ProgressBar properties by invoking, if required
    /// </summary>
    public static class ExtProgrBarInv
    {
        /// <summary>
        /// Set Progressbar DescriptionText, if required invoke
        /// </summary>
        /// <param name="progressBar">Progressbar to set the value</param>
        /// <param name="descriptionText">DescriptionText to set to ProgressBar</param>
        public static void DescriptionText(ExtProgressBar progressBar, string descriptionText)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action<ExtProgressBar, string>(DescriptionText), new object[] { progressBar, descriptionText });
            }
            else
            {
                progressBar.DescriptionText = descriptionText;
            }
        }

        /// <summary>
        /// Set Progressbar Maximum value, if required invoke
        /// </summary>
        /// <param name="progressBar">Progressbar to set the value</param>
        /// <param name="maxValue">Maximum Value to set to ProgressBar.Value</param>
        public static void MaxValue(ExtProgressBar progressBar, long? maxValue)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action<ExtProgressBar, long?>(MaxValue), new object[] { progressBar, maxValue });
            }
            else
            {
                progressBar.MaxValue = maxValue;
            }
        }

        /// <summary>
        /// Set Progressbar style, if required invoke
        /// </summary>
        /// <param name="progressBar">Progressbar to set the style</param>
        /// <param name="style">Style to set to ProgressBar.Style</param>
        public static void Style(ExtProgressBar progressBar, ProgressBarStyle style)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action<ExtProgressBar, ProgressBarStyle>(Style), new object[] { progressBar, style });
            }
            else
            {
                progressBar.Style = style;
            }
        }

        /// <summary>
        /// Set Progressbar value, if required invoke
        /// </summary>
        /// <param name="progressBar">Progressbar to set the value</param>
        /// <param name="value">Value to set to ProgressBar.Value</param>
        public static void Value(ExtProgressBar progressBar, long? value)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action<ExtProgressBar, long?>(Value), new object[] { progressBar, value });
            }
            else
            {
                progressBar.Value = value;
            }
        }
    }
}

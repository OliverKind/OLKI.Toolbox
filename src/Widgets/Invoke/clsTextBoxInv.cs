﻿/*
 * OLKI.Widgets
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Set TextBox properties by invoking, if required
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

namespace OLKI.Toolbox.Widgets.Invoke
{
    /// <summary>
    /// Set TextBox properties by invoking, if required
    /// </summary>
    public static class TextBoxInv
    {
        /// <summary>
        /// Set TextBox text, if required invoke
        /// </summary>
        /// <param name="textBox">TextBox to set the text</param>
        /// <param name="text">Text to set to TextBox.Text</param>
        public static void Text(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action<TextBox, string>(Text), new object[] { textBox, text });
            }
            else
            {
                textBox.Text = text;
            }
        }
    }
}

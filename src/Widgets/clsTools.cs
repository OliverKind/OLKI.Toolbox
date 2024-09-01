/*
 * OLKI.Toolbox.Widgets
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Provides tools for widgets
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

using System.Windows.Forms;

namespace OLKI.Toolbox.Widgets
{
    /// <summary>
    /// Provides tools for widgets
    /// </summary>
    public static class Tools
    {
        public static class ComboBox
        {
            #region Methods
            /// <summary>
            /// Calculates the optimal DropDownWidth of an ComboBox to show the full drop down text an avoid cotting it of, because it is loonger than the DropDownWidth
            /// </summary>
            /// <param name="comboBox">Combobox to set the DropDownWidth automatically</param>
            public static void AutoDropDownWidth(System.Windows.Forms.ComboBox comboBox)
            {
                // Copy the Combobox to avoid access exceptions
                System.Windows.Forms.ComboBox TempComboBox = new System.Windows.Forms.ComboBox
                {
                    DropDownWidth = comboBox.DropDownWidth,
                    Font = comboBox.Font,
                    Width = comboBox.Width
                };
                object[] a = new object[comboBox.Items.Count];
                comboBox.Items.CopyTo(a, 0);
                TempComboBox.Items.AddRange(a);

                // Get the original DropDownWidth
                int DropDownWidth = TempComboBox.DropDownWidth;

                //The Graphics for the control, of the ComboBox
                System.Drawing.Graphics Graphics = TempComboBox.CreateGraphics();

                // Get the with of the scroolbar if the scroolbar is shown
                int VerticalScrollBarWidth = (TempComboBox.Items.Count > TempComboBox.MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0;

                int itemWidth;
                // Move to every item, calculate the necessary with an set new with if it is greater
                foreach (string item in TempComboBox.Items)
                {
                    itemWidth = (int)Graphics.MeasureString(item, TempComboBox.Font).Width + VerticalScrollBarWidth;
                    if (itemWidth > DropDownWidth)
                    {
                        DropDownWidth = itemWidth;
                    }
                }
                // Set new ComboBox width
                Invoke.ComboBoxInv.DropDownWidth(comboBox, DropDownWidth);
            }

            /// <summary>
            /// Set the selected intex of the ComboBox to the first Index, if the ComboBox has Items
            /// </summary>
            /// <param name="comboBox">ComboBox to set the Index to the first Index</param>
            public static void SetToFirstIndex(System.Windows.Forms.ComboBox comboBox)
            {
                if (comboBox.Items.Count > 0) comboBox.SelectedIndex = 0;
            }

            /// <summary>
            /// Set the selected intex of the ComboBox to the defined Index, if the ComboBox has the Index
            /// </summary>
            /// <param name="comboBox">ComboBox to set the Index to the defined Index</param>
            /// <param name="index">Index to set</param>
            public static void SetToIndex(System.Windows.Forms.ComboBox comboBox, int index)
            {
                if (comboBox.Items.Count > 0 && comboBox.Items.Count > index) comboBox.SelectedIndex = index;
            }

            /// <summary>
            /// Set the selected intex of the ComboBox to the lst Item, if the ComboBox has Items
            /// </summary>
            /// <param name="comboBox">ComboBox to set the Index to the last Index</param>
            public static void SetToLastIndex(System.Windows.Forms.ComboBox comboBox)
            {
                if (comboBox.Items.Count > 0) comboBox.SelectedIndex = comboBox.Items.Count - 1;
            }
            #endregion
        }
    }
}
﻿/*
 * OLKI.Toolbox.Widgets
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * An ListView with the ability to sort by click a column and add a sort arrow to the clicked column
 * 
 * This code base on code written by sagar_253, 21 Mar 2014:
 * Original Autor:      sagar_253, 21 Mar 2014
 * Original Source:     http://www.codeproject.com/Tips/734463/Sort-listview-Columns-and-Set-Sort-Arrow-Icon-on-C
 * Original Titel:      Sort listview Columns and Set Sort Arrow Icon on Column Header
 * Original Licence:    The Code Project Open License (CPOL)
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
using System.ComponentModel;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace OLKI.Toolbox.Widgets
{
    /// <summary>
    /// An ListView with the ability to sort by click a column and add a sort arrow to the clicked column
    /// </summary>
    public class SortListView : ListView
    {
        #region Fields
        /// <summary>
        /// Specifies the column sorter
        /// </summary>
        private readonly ColumnSorter _columnSorter = null;
        #endregion

        #region Properties
        /// <summary>
        /// Get or set a Key Combination to select all items
        /// </summary>
        [Category("Extendet")]
        [DefaultValue(Keys.None)]
        [DisplayName("Select all, Key combination")]
        [Description("A Key combination to select all items.")]
        public Keys SelectAllKeys { get; set; } = Keys.None;

        /// <summary>
        /// Get or set if Itmes can be manually sorted, using Drag and Drop
        /// </summary>
        [Category("Extendet")]
        [DefaultValue(false)]
        [DisplayName("AllowDragAndDropSort")]
        [Description("If option is activ, Items can be sorted manually using Drag&Drop")]
        public bool AllowDragAndDropSort { get; set; } = false;

        /// <summary>
        /// Get or set if the changing the column Width is allowed
        /// </summary>
        [Category("Extendet")]
        [DefaultValue(true)]
        [DisplayName("AllowColumnWidthChange")]
        [Description("Is changing the Column Width is allowed")]
        public bool AllowColumnWidthChange { get; set; } = true;

        /// <summary>
        /// Get or set a list with the widths of all Columns
        /// </summary>
        [Browsable(false)]
        public List<int> ColumnWidths
        {
            get
            {
                try
                {
                    List<int> Widths = new List<int>();
                    if (this.Columns == null || this.Columns.Count == 0) new List<int>();
                    for (int i = 0; i < this.Columns.Count; i++)
                    {
                        Widths.Add(this.Columns[i].Width);
                    }
                    return Widths;
                }
                catch (Exception ex)
                {
                    _ = ex;
                    return new List<int>();
                }
            }
            set
            {
                try
                {
                    if (value == null || value.Count == 0) return;
                    for (int i = 0; i < this.Columns.Count; i++)
                    {
                        if (value.Count > i && value[i] > -1) this.Columns[i].Width = value[i];
                    }
                }
                catch (Exception ex)
                {
                    _ = ex;
                }
            }
        }

        /// <summary>
        /// Get the column sorter
        /// </summary>
        [Browsable(false)]
        public ColumnSorter Sorter { get => this._columnSorter; }
        #endregion

        #region Methodes
        /// <summary>
        /// Initialise a new sortable ListView
        /// </summary>
        public SortListView()
        {
            this._columnSorter = new ColumnSorter();
            base.ListViewItemSorter = this._columnSorter;
        }

        /// <summary>
        /// Add empty ListViewSubItems, depending on the Count of Columns 
        /// </summary>
        /// <param name="listViewItem">The ListViewItem to add sub items</param>
        public void FillUpSubItems(ListViewItem listViewItem)
        {
            for (int i = 1; i < this.Columns.Count; i++)
            {
                listViewItem.SubItems.Add("");
            }
        }

        /// <summary>
        /// Ensure the last ListViewItem is visible on screen
        /// </summary>
        public void LastItemVisible()
        {
            try
            {
                if (this.Items.Count > 0) this.Items[this.Items.Count - 1].EnsureVisible();
            }
            catch (Exception ex)
            {
                _ = ex;
            }
        }

        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == this._columnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (this._columnSorter.Order == SortOrder.Ascending)
                {
                    this._columnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    this._columnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                this._columnSorter.SortColumn = e.Column;
                this._columnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.Sort();
            ListViewExtensions.SetSortIcon(this, this._columnSorter.SortColumn, this._columnSorter.Order);
            base.OnColumnClick(e);
        }

        protected override void OnColumnWidthChanging(ColumnWidthChangingEventArgs e)
        {
            if (!this.AllowColumnWidthChange)
            {
                e.Cancel = true;
                e.NewWidth = this.Columns[e.ColumnIndex].Width;
            }
            base.OnColumnWidthChanging(e);
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            if (this.AllowDragAndDropSort)
            {
                // Retrieve the index of the insertion mark;
                int TargetIndex = this.InsertionMark.Index;

                // If the insertion mark is not visible, exit the method.
                if (TargetIndex == -1) return;

                // If the insertion mark is to the right of the item with
                // the corresponding index, increment the target index.
                if (this.InsertionMark.AppearsAfterItem) TargetIndex++;

                // Retrieve the dragged item.
                ListViewItem DraggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

                // Insert a copy of the dragged item at the target index.
                // A copy must be inserted before the original item is removed
                // to preserve item index values.
                this.Items.Insert(TargetIndex, (ListViewItem)DraggedItem.Clone());

                // Remove the original copy of the dragged item.
                this.Items.Remove(DraggedItem);
            }
            base.OnDragDrop(e);
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            if (this.AllowDragAndDropSort)
            {
                e.Effect = e.AllowedEffect;
            }
            base.OnDragEnter(e);
        }

        protected override void OnDragLeave(EventArgs e)
        {
            if (this.AllowDragAndDropSort)
            {
                this.InsertionMark.Index = -1;
            }
            base.OnDragLeave(e);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (this.AllowDragAndDropSort)
            {
                // Retrieve the client coordinates of the mouse pointer.
                Point TargetPoint = this.PointToClient(new Point(e.X, e.Y));

                // Retrieve the index of the item closest to the mouse pointer.
                int TargetIndex = this.InsertionMark.NearestIndex(TargetPoint);

                // Confirm that the mouse pointer is not over the dragged item.
                if (TargetIndex > -1)
                {
                    // Determine whether the mouse pointer is to the left or
                    // the right of the midpoint of the closest item and set
                    // the InsertionMark.AppearsAfterItem property accordingly.
                    Rectangle ItemBounds = this.GetItemRect(TargetIndex);
                    if (TargetPoint.X > ItemBounds.Left + (ItemBounds.Width / 2))
                    {
                        this.InsertionMark.AppearsAfterItem = true;
                    }
                    else
                    {
                        this.InsertionMark.AppearsAfterItem = false;
                    }
                }

                // Set the location of the insertion mark. If the mouse is
                // over the dragged item, the targetIndex value is -1 and
                // the insertion mark disappears.
                this.InsertionMark.Index = TargetIndex;
            }
            base.OnDragOver(e);
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            if (this.AllowDragAndDropSort)
            {
                this.DoDragDrop(e.Item, DragDropEffects.Move);
                base.OnItemDrag(e);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //Select all items
            if (this.SelectAllKeys != Keys.None && e.KeyData == this.SelectAllKeys && e.KeyData != Keys.None && this.Items != null && this.Items.Count > 0)
            {
                foreach (ListViewItem ListViewItem in this.Items)
                {
                    ListViewItem.Selected = true;
                }
            }
            base.OnKeyDown(e);
        }

        /// <summary>
        /// Select the ListViewItem with the defined Id, the Id has to be set als Tag.
        /// All other ListViewItems will be deselected
        /// </summary>
        /// <param name="id"></param>
        public void SelectItemByIdTag(int id)
        {
            this.SelectItemByIdTag(new int[] { id });
        }
        /// <summary>
        /// Select the ListViewItem with the defined Id's, the Id's has to be set als Tag.
        /// All other ListViewItems will be deselected
        /// </summary>
        /// <param name="id"></param>
        public void SelectItemByIdTag(int[] id)
        {
            try
            {
                foreach (ListViewItem ListViewItem in this.Items)
                {
                    ListViewItem.Selected = id.Contains((int)ListViewItem.Tag);
                }
            }
            catch (Exception ex)
            {
                _ = ex;
            }
        }

        /// <summary>
        /// Set SelectState to all ListViewItems
        /// </summary>
        /// <param name="SelectState">Select state to set</param>
        public void SetAllSelections(bool SelectState)
        {
            if (this.Items == null || this.Items.Count == 0) return;
            foreach (ListViewItem ListViewItem in this.Items)
            {
                ListViewItem.Selected = SelectState;
            }
        }

        /// <summary>
        /// Manual sort
        /// </summary>
        /// <param name="sorting">String how to sort. First Column, second Order, seperated by ;. As example 5;2</param>
        public void Sort(string sorting)
        {
            try
            {
                if (string.IsNullOrEmpty(sorting)) return;
                List<int> Sorting = sorting.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToList();
                this.Sort(Sorting);
            }
            catch (Exception ex)
            {
                _ = ex;
            }
        }
        /// <summary>
        /// Manual sort
        /// </summary>
        /// <param name="sorting">List how to sort. First Column, second Order.</param>
        public void Sort(List<int> sorting)
        {
            try
            {
                if (sorting == null || sorting.Count != 2) return;
                this.Sort((int)sorting[0], (int)sorting[1]);
            }
            catch (Exception ex)
            {
                _ = ex;
            }
        }
        /// <summary>
        /// Manual sort
        /// </summary>
        /// <param name="column">Column to sort</param>
        /// <param name="order">Sortorder</param>
        public void Sort(int column, int order)
        {
            try
            {
                if (column == -1 || order == -1) return;
                this.Sort(column, (SortOrder)order);
            }
            catch (Exception ex)
            {
                _ = ex;
            }
        }
        /// <summary>
        /// Manual sort
        /// </summary>
        /// <param name="column">Column to sort</param>
        /// <param name="order">Sortorder</param>
        public void Sort(int column, SortOrder order)
        {
            try
            {
                if (column == -1) return;
                this._columnSorter.SortColumn = column;
                this._columnSorter.Order = order;
                this.Sort();
                ListViewExtensions.SetSortIcon(this, this._columnSorter.SortColumn, this._columnSorter.Order);
            }
            catch (Exception ex)
            {
                _ = ex;
            }
        }
        #endregion

        #region SubClasses
        /// <summary>
        /// Provides sorting of the columns
        /// </summary>
        public class ColumnSorter : IComparer
        {
            #region Properties
            /// <summary>
            /// Specifies the columns to sort
            /// </summary>
            private int _sortColumn = 0;
            /// <summary>
            /// Get or set the columns to sort
            /// </summary>
            public int SortColumn
            {
                internal set
                {
                    this._sortColumn = value;
                }
                get
                {
                    return this._sortColumn;
                }
            }

            /// <summary>
            /// Specifies the sort order of the column
            /// </summary>
            private SortOrder _sortOrder = SortOrder.None;
            /// <summary>
            /// Get or set the sort order of the column
            /// </summary>
            public SortOrder Order
            {
                internal set
                {
                    this._sortOrder = value;
                }
                get
                {
                    return this._sortOrder;
                }
            }
            #endregion

            #region Fields
            /// <summary>
            /// Specifies the comparer for comparing columns
            /// </summary>
            private readonly Comparer _listViewItemComparer = new Comparer(System.Globalization.CultureInfo.CurrentUICulture);
            #endregion

            /// <summary>
            /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
            /// </summary>
            /// <param name="x">First object to be compared</param>
            /// <param name="y">Second object to be compared</param>
            /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
            public int Compare(object x, object y)
            {
                try
                {
                    ListViewItem lviX = (ListViewItem)x;
                    ListViewItem lviY = (ListViewItem)y;

                    int compareResult = 0;

                    if (lviX.SubItems[this._sortColumn].Tag != null && lviY.SubItems[this._sortColumn].Tag != null)
                    {
                        compareResult = this._listViewItemComparer.Compare(lviX.SubItems[this._sortColumn].Tag, lviY.SubItems[this._sortColumn].Tag);
                    }
                    else
                    {
                        compareResult = this._listViewItemComparer.Compare(lviX.SubItems[this._sortColumn].Text, lviY.SubItems[this._sortColumn].Text);
                    }

                    if (this._sortOrder == SortOrder.Ascending)
                    {
                        return compareResult;
                    }
                    else if (this._sortOrder == SortOrder.Descending)
                    {
                        return (-compareResult);
                    }
                    else
                    {
                        return 0;
                    }

                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Extends the ListView with an sort arrow at the sorte
        /// </summary>
        private static class ListViewExtensions
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct LVCOLUMN
            {
                public int mask;
                public int cx;
                [MarshalAs(UnmanagedType.LPTStr)]
                public string pszText;
                public IntPtr hbm;
                public int cchTextMax;
                public int fmt;
                public int iSubItem;
                public int iImage;
                public int iOrder;
            }

            const int HDI_FORMAT = 0x0004;
            const int HDF_LEFT = 0x0000;
            const int HDF_BITMAP_ON_RIGHT = 0x1000;
            const int HDF_SORTUP = 0x0400;
            const int HDF_SORTDOWN = 0x0200;

            const int LVM_FIRST = 0x1000;         // List messages
            const int LVM_GETHEADER = LVM_FIRST + 31;
            const int HDM_FIRST = 0x1200;         // Header messages
            const int HDM_GETITEM = HDM_FIRST + 11;
            const int HDM_SETITEM = HDM_FIRST + 12;

            [DllImport("user32.dll")]
            private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", EntryPoint = "SendMessage")]
            private static extern IntPtr SendMessageLVCOLUMN(IntPtr hWnd, Int32 Msg, IntPtr wParam, ref LVCOLUMN lPLVCOLUMN);

            /// <summary>
            /// Set the arrow to the sorted column and removes the arrows from the all ohter columns
            /// </summary>
            /// <param name="listView">Specifies the ListView where the columns is defined</param>
            /// <param name="columnIndex">Specifies the index of the columns to set the arrow</param>
            /// <param name="order">Specifies the sort order of the columns</param>
            public static void SetSortIcon(ListView listView, int columnIndex, SortOrder order)
            {
                IntPtr columnHeader = SendMessage(listView.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);

                for (int columnNumber = 0; columnNumber <= listView.Columns.Count - 1; columnNumber++)
                {
                    IntPtr columnPtr = new IntPtr(columnNumber);
                    LVCOLUMN lvColumn = new LVCOLUMN
                    {
                        mask = HDI_FORMAT
                    };

                    SendMessageLVCOLUMN(columnHeader, HDM_GETITEM, columnPtr, ref lvColumn);

                    if (!(order == SortOrder.None) && columnNumber == columnIndex)
                    {
                        switch (order)
                        {
                            case SortOrder.Ascending:
                                lvColumn.fmt &= ~HDF_SORTDOWN;
                                lvColumn.fmt |= HDF_SORTUP;
                                break;
                            case SortOrder.Descending:
                                lvColumn.fmt &= ~HDF_SORTUP;
                                lvColumn.fmt |= HDF_SORTDOWN;
                                break;
                        }
                        lvColumn.fmt |= (HDF_LEFT | HDF_BITMAP_ON_RIGHT);
                    }
                    else
                    {
                        lvColumn.fmt &= ~HDF_SORTDOWN & ~HDF_SORTUP & ~HDF_BITMAP_ON_RIGHT;
                    }

                    SendMessageLVCOLUMN(columnHeader, HDM_SETITEM, columnPtr, ref lvColumn);
                }
            }
        }
        #endregion
    }
}
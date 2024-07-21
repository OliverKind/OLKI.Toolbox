/*
 * OLKI.Toolbox.Widgets
 * 
 * Initial Author: Oliver Kind - 2024
 * License:        LGPL
 * 
 * Desctiption:
 * A MaskedTextBox that check if an given Date is valid
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

using OLKI.Toolbox.Common;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace OLKI.Toolbox.Widgets
{
    /// <summary>
    /// A MaskedTextBox that check if an given Date is valid
    /// </summary>
    public partial class DateInputAndValidator : MaskedTextBox
    {
        #region Fields
        /// <summary>
        /// Errorprovider to show errors if input wasn't a valid Date
        /// </summary>
        private readonly ErrorProvider _errorProvider = new ErrorProvider();
        #endregion

        #region Properties
        /// <summary>
        /// The date of the DateInputAndValidator
        /// </summary>
        private DateTime? _date;
        /// <summary>
        /// Get or set the date of the DateInputAndValidator
        /// </summary>
        [Browsable(false)]
        public DateTime? Date
        {
            get
            {
                return this._date;
            }
            set
            {
                this._date = value;
                this.Text = Date.ToString();
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// initial a new MaskedTextBox that check if an given Date is valid
        /// </summary>
        public DateInputAndValidator()
        {
            this._errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (Validation.IsValidDate(this.Text, true, out this._date))
            {
                this._errorProvider.SetError(this, "");
            }
            else
            {
                this._errorProvider.SetError(this,  src.Widgets.widDateInputAndValidator_Striingtable._0x0001);
            }
            base.OnTextChanged(e);
        }
        #endregion
    }
}
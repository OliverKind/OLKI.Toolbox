/*
 * OLKI.Toolbox.Widgets
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * An Controle to show the progress, especially in connection with Byte operation 
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
using OLKI.Toolbox.DirectoryAndFile;
using OLKI.Toolbox.Widgets.Invoke;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OLKI.Toolbox.Widgets
{
    /// <summary>
    /// An Controle to show the progress, especially in connection with Byte operation 
    /// </summary>
    public partial class ExtProgressBar : UserControl
    {
        #region Constants
        /// <summary>
        /// Defines the format for a actual and maximum value
        /// </summary>
        private const string FORMAT_ACTUAL_MAX_VALUE = "{0} / {1}";
        /// <summary>
        /// Defines the format for a string with a percentage number value
        /// </summary>
        private const string FORMAT_PERCENTAGE = @"{0}%";
        /// <summary>
        /// Defines the format for a string with a byte number value
        /// </summary>
        private const string FORMAT_VALUE = "{0:n{_DIGITS_}}";
        #endregion

        #region Enums
        /// <summary>
        /// Defines wich element should been grown, if the dimension ComboBox is hidden
        /// </summary>
        public enum HideDimensionMode
        {
            GrowProgressBar,
            GrowValueTextBox
        }
        #endregion

        #region Members
        /// <summary>
        /// The progress in percentage
        /// </summary>
        private int _percentageProgress = 0;

        /// <summary>
        /// Set True if changes on widgeds was done by system and change events should been ignored
        /// </summary>
        private bool _suppressControleEvents = false;

        /// <summary>
        /// Format to Show Value Numers
        /// </summary>
        private string ValueNumberFormat
        {
            get
            {
                return FORMAT_VALUE.Replace("{_DIGITS_}", this.DecimalDigits.ToString());
            }
        }

        #region Size and Location of Controles
        /// <summary>
        /// Offset for showing or hinding the description TextBox
        /// </summary>
        private readonly int _desOffsetY;

        /// <summary>
        /// Offset for showing or hinding the dimen sion ComboBox
        /// </summary>
        private readonly int _dimOffsetX;

        /// <summary>
        /// Inital location of cboByteDime
        /// </summary>
        private readonly Point _cboByteDimeInitLoc;

        /// <summary>
        /// Inital location of pbaProgress
        /// </summary>
        private readonly Point _pbaProgressInitLoc;

        /// <summary>
        /// Inital location of txtBytesNum
        /// </summary>
        private readonly Point _txtBytesNumInitLoc;

        /// <summary>
        /// Inital location of txtBytesPer
        /// </summary>
        private readonly Point _txtBytesPerInitLoc;

        /// <summary>
        /// Initial size of this Controle
        /// </summary>
        private readonly Size _thisControlInitSiz;

        /// <summary>
        /// Initial size of txtBytesNum
        /// </summary>
        private readonly Size _txtBytesNumInitSiz;
        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Get or set thhe ByteBase for audomatic dimension calculation
        /// </summary>
        [Category("ProgressBar Extension")]
        [DisplayName("Automatical Byte Base")]
        [DefaultValue(FileSize.ByteBase.IEC)]
        [Description("ByteBase for audomatic dimension calculation")]
        public FileSize.ByteBase AutoByteBase { get; set; } = FileSize.ByteBase.IEC;

        /// <summary>
        /// Should the Byte Dimension been set automatically
        /// </summary>
        private bool _autoByteDimension = true;
        /// <summary>
        /// Get or set if the Byte Dimension should been set automatically
        /// </summary>
        [Category("ProgressBar Extension")]
        [DisplayName("Automatical Byte Dimension")]
        [DefaultValue(true)]
        [Description("Should the Byte Dimension been set automatically")]
        public bool AutoByteDimension
        {
            get
            {
                return this._autoByteDimension;
            }
            set
            {
                this._autoByteDimension = value;
                this.SetProcessValues();
            }
        }

        /// <summary>
        /// Byte Dimension, override Automatical Byte Dimension
        /// </summary>
        private FileSize.Dimension _byteDimension = FileSize.Dimension._Automatic_;
        /// <summary>
        /// Get or set the Byte Dimension, override Automatical Byte Dimension
        /// </summary>
        [Category("ProgressBar Extension")]
        [DisplayName("Byte Dimension")]
        [DefaultValue(true)]
        [Description("Byte Dimension, override Automatical Byte Dimension")]
        public FileSize.Dimension ByteDimension
        {
            get
            {
                return this._byteDimension;
            }
            set
            {
                this._byteDimension = value;
            }
        }

        /// <summary>
        /// Get or set decimal Digits to show for values
        /// </summary>
        [Category("ProgressBar Extension")]
        [DisplayName("Decimal Digits")]
        [DefaultValue(2)]
        [Description("Decimal Digits to show for values")]
        public uint DecimalDigits { get; set; } = 2;

        /// <summary>
        /// Get or set the description text
        /// </summary>
        [Category("ProgressBar Extension")]
        [DisplayName("Description Text")]
        [DefaultValue("")]
        [Description("Text to show in the Description text area")]
        public string DescriptionText
        {
            get
            {
                return this.txtDescript.Text;
            }
            set
            {
                this.txtDescript.Text = value;
            }
        }

        /// <summary>
        /// Defines wich element should been grown, if the dimension ComboBox is hidden
        /// </summary>
        private HideDimensionMode _hideDimensionGrowMode = ExtProgressBar.HideDimensionMode.GrowValueTextBox;
        /// <summary>
        /// Get or set wich element should been grown, if the dimension ComboBox is hidden
        /// </summary>
        [Category("ProgressBar Extension")]
        [DisplayName("Hide Dimension grow Mode")]
        [DefaultValue(HideDimensionMode.GrowValueTextBox)]
        [Description("Wich element should been grown, if the dimension ComboBox is hidden")]
        public HideDimensionMode HideDimensionGrowMode
        {
            get
            {
                return this._hideDimensionGrowMode;
            }
            set
            {
                this._hideDimensionGrowMode = value;
                this.SetLocAndSize();
            }
        }

        /// <summary>
        /// The maximum Value of the progress
        /// </summary>
        private long _maxValue = 0;
        /// <summary>
        /// Get or set the maximum Value of the progress. NULL will be converted to -1
        /// </summary>
        [Category("ProgressBar Extension")]
        [DisplayName("Maximum Value")]
        [DefaultValue(0)]
        [Description("Maximum Value of the progress. NULL will be converted to -1")]
        public long? MaxValue
        {
            get
            {
                return this._maxValue;
            }
            set
            {
                this._maxValue = (long)(value == null ? -1 : value);
                this.SetProcessValues();
            }
        }

        /// <summary>
        /// Get or set the style that the ProgressBar uses to indicate the progress.
        /// </summary>
        [Category("ProgressBar Extension")]
        [DisplayName("ProgressBar Style")]
        [DefaultValue(ProgressBarStyle.Blocks)]
        [Description("Specifies the style that the ProgressBar uses to indicate the progress.")]
        public ProgressBarStyle Style
        {
            get
            {
                return this.pbaProgress.Style;
            }
            set
            {
                this.pbaProgress.Style = value;
            }
        }

        /// <summary>
        ///  Defines if the ComboBox for FileSizes should been shown
        /// </summary>
        private bool _showDescriptionText = true;
        /// <summary>
        /// Get or set if the ComboBox for FileSizes should been shown
        /// </summary>
        [Category("ProgressBar Extension")]
        [DisplayName("Show Description Text")]
        [DefaultValue(true)]
        [Description("Defines if the Description Text should been shown")]
        public bool ShowDescriptionText
        {
            get
            {
                return this._showDescriptionText;
            }
            set
            {
                this._showDescriptionText = value;
                this.txtDescript.Visible = value;

                if (value)
                {
                    this.cboByteDime.Location = new Point(this.cboByteDime.Location.X, this._cboByteDimeInitLoc.Y);
                    this.pbaProgress.Location = new Point(this.pbaProgress.Location.X, this._pbaProgressInitLoc.Y);
                    this.txtBytesNum.Location = new Point(this.txtBytesNum.Location.X, this._txtBytesNumInitLoc.Y);
                    this.txtBytesPer.Location = new Point(this.txtBytesPer.Location.X, this._txtBytesPerInitLoc.Y);
                }
                else
                {
                    this.cboByteDime.Location = new Point(this.cboByteDime.Location.X, this._cboByteDimeInitLoc.Y - this._desOffsetY);
                    this.pbaProgress.Location = new Point(this.pbaProgress.Location.X, this._pbaProgressInitLoc.Y - this._desOffsetY);
                    this.txtBytesNum.Location = new Point(this.txtBytesNum.Location.X, this._txtBytesNumInitLoc.Y - this._desOffsetY);
                    this.txtBytesPer.Location = new Point(this.txtBytesPer.Location.X, this._txtBytesPerInitLoc.Y - this._desOffsetY);
                }

                this.Height = this.pbaProgress.Location.Y + this.pbaProgress.Height;
            }
        }

        /// <summary>
        ///  Defines if the ComboBox for FileSizes should been shown
        /// </summary>
        private bool _showDimensionComboBox = true;
        /// <summary>
        /// Get or set if the ComboBox for FileSizes should been shown
        /// </summary>
        [Category("ProgressBar Extension")]
        [DisplayName("Show Dimension ComboBox")]
        [DefaultValue(true)]
        [Description("Defines if the ComboBox for FileSizes should been shown")]
        public bool ShowDimensionComboBox
        {
            get
            {
                return this._showDimensionComboBox;
            }
            set
            {
                this._showDimensionComboBox = value;
                this.cboByteDime.Visible = value;

                this.SetLocAndSize();
            }
        }

        /// <summary>
        /// The actual Value of the progress. Set to -1 to blank percentage and numeric TextBox.
        /// </summary>
        private long _value = -1;
        /// <summary>
        /// Get or set the actual Value of the progress. Set to -1 to blank percentage and numeric TextBox. NULL will be converted to -1.
        /// </summary>
        [Category("ProgressBar Extension")]
        [DisplayName("Value")]
        [DefaultValue(-1)]
        [Description("Actual Value of the progress. Set to -1 to blank percentage and numeric TextBox. NULL will be converted to -1")]
        public long? Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = (long)(value == null ? -1 : value);
                this.SetProcessValues();
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Initial a new ExtProgressBar
        /// </summary>
        public ExtProgressBar()
        {
            InitializeComponent();

            //Initial Combobox
            this._suppressControleEvents = true;
            FileSize.SetDimensionlistToComboBox(this.cboByteDime, FileSize.ByteBase.IEC, FileSize.ByteBase.SI, true, true);
            Tools.ComboBox.AutoDropDownWidth(this.cboByteDime);
            Tools.ComboBox.SetToFirstIndex(this.cboByteDime);
            this._suppressControleEvents = false;

            //Initial with and position calculations
            this._desOffsetY = this.txtDescript.Height + this.txtDescript.Margin.Bottom + this.txtDescript.Margin.Top;
            this._dimOffsetX = this.cboByteDime.Width + this.cboByteDime.Margin.Left + this.txtBytesNum.Margin.Right;

            //Save intial sizes
            this._thisControlInitSiz = this.Size;
            this._txtBytesNumInitSiz = this.txtBytesNum.Size;

            //Save intial locations
            this._cboByteDimeInitLoc = this.cboByteDime.Location;
            this._pbaProgressInitLoc = this.pbaProgress.Location;
            this._txtBytesNumInitLoc = this.txtBytesNum.Location;
            this._txtBytesPerInitLoc = this.txtBytesPer.Location;
        }

        /// <summary>
        /// Set the Location and Size of the affected controles, if the DimensionComboBox is visible or not visible
        /// </summary>
        private void SetLocAndSize()
        {
            int DWidth = this.Width - this._thisControlInitSiz.Width;

            if (this._showDimensionComboBox)
            {
                //Calculate TextBoxPositions if DimensionComboBox is visible
                this.txtBytesNum.Location = new Point(this._txtBytesNumInitLoc.X + DWidth, this.txtBytesNum.Location.Y);
                this.txtBytesNum.Size = new Size(this._txtBytesNumInitSiz.Width, this.txtBytesNum.Size.Height);
            }
            else
            {
                //Calculate TextBoxPositions if DimensionComboBox is not visible
                switch (this._hideDimensionGrowMode)
                {
                    case HideDimensionMode.GrowProgressBar:
                        this.txtBytesNum.Location = new Point(this._txtBytesNumInitLoc.X + DWidth + this._dimOffsetX, this.txtBytesNum.Location.Y);
                        this.txtBytesNum.Size = new Size(this._txtBytesNumInitSiz.Width, this.txtBytesNum.Size.Height);
                        break;
                    case HideDimensionMode.GrowValueTextBox:
                        this.txtBytesNum.Location = new Point(this._txtBytesNumInitLoc.X + DWidth, this.txtBytesNum.Location.Y);
                        this.txtBytesNum.Size = new Size(this._txtBytesNumInitSiz.Width + this._dimOffsetX, this.txtBytesNum.Size.Height);
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            this.txtBytesPer.Location = new Point(this.txtBytesNum.Location.X - this.txtBytesNum.Margin.Left - this.txtBytesPer.Margin.Left - this.txtBytesPer.Size.Width, this.txtBytesPer.Location.Y);
            this.pbaProgress.Size = new Size(this.txtBytesPer.Location.X - this.txtBytesPer.Margin.Left - this.pbaProgress.Margin.Left, this.pbaProgress.Size.Height);
        }

        /// <summary>
        /// Clear the ProgressBar (to 0 and BlockStyle) and all TextBoxes
        /// </summary>
        public void Clear()
        {
            this.Clear(true);
        }
        /// <summary>
        /// Clear the ProgressBar (to 0 and if wanted to BlockStyle) and all TextBoxes
        /// </summary>
        /// <param name="setProgressBarStyleToBlocks">Shold the ProgressBarStyle set to Blocks</param>
        public void Clear(bool setProgressBarStyleToBlocks)
        {
            if (setProgressBarStyleToBlocks) ProgressBarInv.Style(this.pbaProgress, ProgressBarStyle.Blocks);
            ProgressBarInv.Value(this.pbaProgress, 0);
            TextBoxInv.Text(this.txtBytesNum, "");
            TextBoxInv.Text(this.txtBytesPer, "");
            TextBoxInv.Text(this.txtDescript, "");
        }

        /// <summary>
        /// Set the value for the ProgressBar and the TextBoxes
        /// </summary>
        private void SetProcessValues()
        {
            decimal ActValue;
            decimal MaxValue = this._maxValue;
            uint Dimension;

            if (this._autoByteDimension)
            {
                if (this._maxValue > 0)
                {
                    Dimension = FileSize.GetHighestDimension(this._maxValue, this.AutoByteBase, FileSize.Dimension._Automatic_);
                    ActValue = FileSize.ConvertNum(this._value, this.DecimalDigits, this.AutoByteBase, (FileSize.Dimension)Dimension);
                    MaxValue = FileSize.ConvertNum(this._maxValue, this.DecimalDigits, this.AutoByteBase, (FileSize.Dimension)Dimension);
                }
                else
                {
                    Dimension = FileSize.GetHighestDimension(this._value, this.AutoByteBase, FileSize.Dimension._Automatic_);
                    ActValue = FileSize.ConvertNum(this._value, this.DecimalDigits, this.AutoByteBase, (FileSize.Dimension)Dimension);
                }
                this.ByteDimension = (FileSize.Dimension)Dimension;

                if (!this.cboByteDime.DroppedDown)
                {
                    this._suppressControleEvents = true;
                    ComboBoxInv.SelectedIndex(this.cboByteDime, (int)Dimension + (this.AutoByteBase == FileSize.ByteBase.IEC ? 0 : FileSize.UnitPrefix_IEC.Length));
                    this._suppressControleEvents = false;
                }
            }
            else
            {
                Dimension = (uint)this.cboByteDime.SelectedIndex;
                Dimension -= (uint)(Dimension >= FileSize.UnitPrefix_IEC.Length ? FileSize.UnitPrefix_IEC.Length : 0);
                this.ByteDimension = (FileSize.Dimension)Dimension;

                FileSize.ByteBase ByteBase = this.cboByteDime.SelectedIndex >= FileSize.UnitPrefix_IEC.Length ? FileSize.ByteBase.SI : FileSize.ByteBase.IEC;

                ActValue = FileSize.ConvertNum(this._value, this.DecimalDigits, ByteBase, (FileSize.Dimension)Dimension);
                if (this._maxValue > 0) MaxValue = FileSize.ConvertNum(this._maxValue, this.DecimalDigits, ByteBase, (FileSize.Dimension)Dimension);
            }

            if (this._maxValue > 0)
            {
                this._percentageProgress = (int)Matehmatics.UpLimit(Matehmatics.Percentages(this._value, this._maxValue), 100);
                ProgressBarInv.Value(this.pbaProgress, this._percentageProgress);
                TextBoxInv.Text(this.txtBytesPer, this._value == -1 ? "" : string.Format(FORMAT_PERCENTAGE, this._percentageProgress));
                TextBoxInv.Text(this.txtBytesNum, this._value == -1 ? "" : string.Format(FORMAT_ACTUAL_MAX_VALUE, new object[] { string.Format(this.ValueNumberFormat, ActValue), string.Format(this.ValueNumberFormat, MaxValue) }));
            }
            else
            {
                this._percentageProgress = 0;
                ProgressBarInv.Value(this.pbaProgress, 0);
                TextBoxInv.Text(this.txtBytesPer, "");
                TextBoxInv.Text(this.txtBytesNum, this._value == -1 ? "" : string.Format(this.ValueNumberFormat, ActValue));
            }
        }

        #region Controle events
        private void cboByteDime_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this._autoByteDimension = true;
                this.SetProcessValues();
            }
        }

        private void cboByteDimension_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._suppressControleEvents) return;
            this._autoByteDimension = false;
            this.SetProcessValues();
        }

        private void ProgressbarEx_Resize(object sender, EventArgs e)
        {
            this.Height = this.pbaProgress.Location.Y + this.pbaProgress.Height;
        }
        #endregion
        #endregion
    }
}
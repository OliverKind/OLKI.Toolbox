/*
 * OLKI.Toolbox.DirectoryAndFile
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to handle file sizes
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

namespace OLKI.Toolbox.DirectoryAndFile
{
    /// <summary>
    /// Class that convert file sizes from bytes to an defined format
    /// </summary>
    public static class FileSize
    {
        #region Constants
        /// <summary>
        /// Specifies the defaukt byte base for sice conversion
        /// </summary>
        private const ByteBase DEFAULT_BYTE_BASE = ByteBase.IEC;
        /// <summary>
        /// Specifies the defaukt number of decimal digits
        /// </summary>
        private const int DEFAULT_DECIMALS = 2;
        /// <summary>
        /// Specifies the defaukt if the unit should been hidet
        /// </summary>
        private const bool DEFAULT_HIDE_UNIT = false;
        /// <summary>
        /// Specifies the defaukt result format with dimension
        /// </summary>
        private const string DEFAULT_RESULT_FORMAT = "{0} {1}";
        /// <summary>
        /// Unit prefixes for IEC Byte base 1024 in accordance with IEC 60027-2 (International Electrotechnical Commission's standard). Prefix like GiB - Gibi Byte. Used by Microsoft for SI deklaration like GB - Giga Byte
        /// </summary>
        public static readonly string[] UnitPrefix_IEC = new string[7] { "Byte", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB" };
        /// <summary>
        /// Unit prefixes for SI Byte base 1000 in accordance with SI (International System of Units) declaration. Prefix like GB - Giga Byte. Microsoft uses this declaration, but byte base 1024 like IEC
        /// </summary>
        public static readonly string[] UnitPrefix_SI = new string[7] { "Byte", "KB", "MB", "GB", "TB", "PB", "EB" };
        #endregion

        #region Methods
        /// <summary>
        /// The Byte base for sice conversion 
        /// </summary>
        public enum ByteBase
        {
            /// <summary>
            /// IEC Byte base 1024 in accordance with IEC 60027-2 (International Electrotechnical Commission's standard). Prefix like GiB - Gibi Byte. Used by Microsoft for SI deklaration like GB - Giga Byte
            /// </summary>
            IEC = 1024,
            /// <summary>
            /// SI Byte base 1000 in accordance with SI (International System of Units) declaration. Prefix like GB - Giga Byte. Microsoft uses this declaration, but byte base 1024 like IEC
            /// </summary>
            SI = 1000,
        }

        /// <summary>
        /// The dimension of a byte value
        /// </summary>
        public enum Dimension
        {
            _NotSet_ = -2,
            _Automatic_ = -1,
            NoDimension = 0,
            Kilo = 1,
            Mega = 2,
            Giga = 3,
            Tera = 4,
            Peta = 5,
            Exa = 6
        }

        #region Convert
        /// <summary>
        /// Converts the length of a specified file to the highest or maximum posible dimension, with 2 decimals and the unit
        /// </summary>
        /// <param name="file">Specifies an file to convert it length</param>
        /// <returns>The converted length</returns>
        public static string Convert(System.IO.FileInfo file)
        {
            return Convert(file, DEFAULT_HIDE_UNIT);
        }
        /// <summary>
        /// Converts the length of a specified file to the highest or maximum posible dimension, with 2 decimals and can hide the unit
        /// </summary>
        /// <param name="file">Specifies an file to convert it length</param>
        /// <param name="hideUnit">Specifies if the unit should been shown or hidet in the result</param>
        /// <returns>The converted length</returns>
        public static string Convert(System.IO.FileInfo file, bool hideUnit)
        {
            return Convert(file, DEFAULT_DECIMALS, hideUnit);
        }
        /// <summary>
        /// Converts the length of a specified file to the highest or maximum posible dimension, with an specified number of decimal digits and the unit
        /// </summary>
        /// <param name="file">Specifies an file to convert it length</param>
        /// <param name="decimals">Specifies the number of decimal digits to shown in the result</param>
        /// <returns>The converted length</returns>
        public static string Convert(System.IO.FileInfo file, uint decimals)
        {
            return Convert(file, decimals, DEFAULT_HIDE_UNIT);
        }
        /// <summary>
        /// Converts the length of a specified file to the highest or maximum posible dimension, with an specified number of decimal digits and can hide the unit
        /// </summary>
        /// <param name="file">Specifies an file to convert it length</param>
        /// <param name="decimals">Specifies the number of decimal digits to shown in the result</param>
        /// <param name="hideUnit">Specifies if the unit should been shown or hidet in the result</param>
        /// <returns>The converted length</returns>
        public static string Convert(System.IO.FileInfo file, uint decimals, bool hideUnit)
        {
            return Convert(file, decimals, DEFAULT_BYTE_BASE, hideUnit);
        }
        /// <summary>
        /// Converts the length of a specified file to the highest or maximum posible dimension, with an specified byte base, an specified number of decimal digits and the unit
        /// </summary>
        /// <param name="file">Specifies an file to convert it length</param>
        /// <param name="decimals">Specifies the number of decimal digits to shown in the result</param>
        /// <param name="byteBase">Specifies the byte base for conversion</param>
        /// <returns>The converted length</returns>
        public static string Convert(System.IO.FileInfo file, uint decimals, ByteBase byteBase)
        {
            return Convert(file, decimals, byteBase, DEFAULT_HIDE_UNIT);
        }
        /// <summary>
        /// Converts the length of a specified file to the highest or maximum posible dimension, with an specified byte base, an specified number of decimal digits and can hide the unit
        /// </summary>
        /// <param name="file">Specifies an file to convert it length</param>
        /// <param name="decimals">Specifies the number of decimal digits to shown in the result</param>
        /// <param name="byteBase">Specifies the byte base for conversion</param>
        /// <param name="hideUnit">Specifies if the unit should been shown or hidet in the result</param>
        /// <returns>The converted length</returns>
        public static string Convert(System.IO.FileInfo file, uint decimals, ByteBase byteBase, bool hideUnit)
        {
            return Convert(file, decimals, byteBase, Dimension._Automatic_, hideUnit);
        }
        /// <summary>
        /// Converts the length of a specified file to an forced or maximum posible dimension, with an specified byte base, an specified number of decimal digits and the unit
        /// </summary>
        /// <param name="file">Specifies an file to convert it length</param>
        /// <param name="decimals">Specifies the number of decimal digits to shown in the result</param>
        /// <param name="byteBase">Specifies the byte base for conversion</param>
        /// <param name="dimension">Specifies a forced dimension to convert to</param>
        /// <returns>The converted length</returns>
        public static string Convert(System.IO.FileInfo file, uint decimals, ByteBase byteBase, Dimension dimension)
        {
            return Convert(file.Length, decimals, byteBase, dimension, DEFAULT_HIDE_UNIT);
        }
        /// <summary>
        /// Converts the length of a specified file to an forced or maximum posible dimension, with an specified byte base, an specified number of decimal digits and can hide the unit
        /// </summary>
        /// <param name="file">Specifies an file to convert it length</param>
        /// <param name="decimals">Specifies the number of decimal digits to shown in the result</param>
        /// <param name="byteBase">Specifies the byte base for conversion</param>
        /// <param name="dimension">Specifies a forced dimension to convert to</param>
        /// <param name="hideUnit">Specifies if the unit should been shown or hidet in the result</param>
        /// <returns>The converted length</returns>
        public static string Convert(System.IO.FileInfo file, uint decimals, ByteBase byteBase, Dimension dimension, bool hideUnit)
        {
            return Convert(file.Length, decimals, byteBase, dimension, hideUnit);
        }
        /// <summary>
        /// Converts a specified length to the highest or maximum posible dimension, with 2 decimals and the unit
        /// </summary>
        /// <param name="length">Specifies the length of an file in byte to convert</param>
        /// <returns>The converted length</returns>
        public static string Convert(long length)
        {
            return Convert(length, DEFAULT_HIDE_UNIT);
        }
        /// <summary>
        /// Converts a specified length to the highest or maximum posible dimension, with 2 decimals and can hide the units
        /// </summary>
        /// <param name="length">Specifies the length of an file in byte to convert</param>
        /// <param name="hideUnit">Specifies if the unit should been shown or hidet in the result</param>
        /// <returns>The converted length</returns>
        public static string Convert(long length, bool hideUnit)
        {
            return Convert(length, DEFAULT_DECIMALS, hideUnit);
        }
        /// <summary>
        /// Converts a specified length to the highest or maximum posible dimension, with an specified number of decimal digits and the unit
        /// </summary>
        /// <param name="length">Specifies the length of an file in byte to convert</param>
        /// <param name="decimals">Specifies the number of decimal digits to shown in the result</param>
        /// <returns>The converted length</returns>
        public static string Convert(long length, uint decimals)
        {
            return Convert(length, decimals, DEFAULT_HIDE_UNIT);
        }
        /// <summary>
        /// Converts a specified length to the highest or maximum posible dimension, with an specified number of decimal digits and can hide the unit
        /// </summary>
        /// <param name="length">Specifies the length of an file in byte to convert</param>
        /// <param name="decimals">Specifies the number of decimal digits to shown in the result</param>
        /// <param name="hideUnit">Specifies if the unit should been shown or hidet in the result</param>
        /// <returns>The converted length</returns>
        public static string Convert(long length, uint decimals, bool hideUnit)
        {
            return Convert(length, decimals, DEFAULT_BYTE_BASE, hideUnit);
        }
        /// <summary>
        /// Converts a specified length to the highest or maximum posible dimension, with an specified byte base, an specified number of decimal digits and the unit
        /// </summary>
        /// <param name="length">Specifies the length of an file in byte to convert</param>
        /// <param name="decimals">Specifies the number of decimal digits to shown in the result</param>
        /// <param name="byteBase">Specifies the byte base for conversion</param>
        /// <returns>The converted length</returns>
        public static string Convert(long length, uint decimals, ByteBase byteBase)
        {
            return Convert(length, decimals, byteBase, DEFAULT_HIDE_UNIT);
        }
        /// <summary>
        /// Converts a specified length to the highest or maximum posible dimension, with an specified byte base, an specified number of decimal digits and can hide the unit
        /// </summary>
        /// <param name="length">Specifies the length of an file in byte to convert</param>
        /// <param name="decimals">Specifies the number of decimal digits to shown in the result</param>
        /// <param name="byteBase">Specifies the byte base for conversion</param>
        /// <param name="hideUnit">Specifies if the unit should been shown or hidet in the result</param>
        /// <returns>The converted length</returns>
        public static string Convert(long length, uint decimals, ByteBase byteBase, bool hideUnit)
        {
            return Convert(length, decimals, byteBase, Dimension._Automatic_, hideUnit);
        }
        /// <summary>
        /// Converts a specified length to an forced or maximum posible dimension, with an specified byte base, an specified number of decimal digits and the unit
        /// </summary>
        /// <param name="length">Specifies the length of an file in byte to convert</param>
        /// <param name="decimals">Specifies the number of decimal digits to shown in the result</param>
        /// <param name="byteBase">Specifies the byte base for conversion</param>
        /// <param name="dimension">Specifies a forced dimension to convert to</param>
        /// <returns>The converted length</returns>
        public static string Convert(long length, uint decimals, ByteBase byteBase, Dimension dimension)
        {
            return Convert(length, decimals, byteBase, dimension, DEFAULT_HIDE_UNIT);
        }
        /// <summary>
        /// Converts a specified length to an forced or maximum posible dimension, with an specified byte base, an specified number of decimal digits and can hide the unit
        /// </summary>
        /// <param name="length">Specifies the length of an file in byte to convert</param>
        /// <param name="decimals">Specifies the number of decimal digits to shown in the result</param>
        /// <param name="byteBase">Specifies the byte base for conversion</param>
        /// <param name="dimension">Specifies a forced dimension to convert to</param>
        /// <param name="hideUnit">Specifies if the unit should been shown or hidet in the result</param>
        /// <returns>The converted length</returns>
        public static string Convert(long length, uint decimals, ByteBase byteBase, Dimension dimension, bool hideUnit)
        {
            string[] Unit;
            string NumberFormat = "{0:n" + decimals.ToString() + "}";
            string ReturnNumber;

            // Set the unit array by selected byteBase
            switch (byteBase)
            {
                case ByteBase.SI:
                    Unit = UnitPrefix_SI;
                    break;
                case ByteBase.IEC:
                default:
                    Unit = UnitPrefix_IEC;
                    break;
            }

            // Get Dimension, if dimension is not preset
            if (dimension == Dimension._NotSet_ || dimension == Dimension._Automatic_)
            {
                dimension = (Dimension)GetHighestDimension(length, byteBase, dimension);
            }

            // Return new length, wit or without unit
            ReturnNumber = string.Format(NumberFormat, ConvertNum(length, decimals, byteBase, dimension));

            if (hideUnit) return ReturnNumber;
            return string.Format(DEFAULT_RESULT_FORMAT, new object[] { ReturnNumber, Unit[(int)dimension] });
        }

        /// <summary>
        /// Converts a specified length to an forced dimension
        /// </summary>
        /// <param name="length">Specifies the length of an file in byte to convert</param>
        /// <param name="dimension">Specifies a forced dimension to convert to</param>
        /// <returns>The converted length</returns>
        public static decimal ConvertNum(long length, Dimension dimension)
        {
            return ConvertNum(length, DEFAULT_DECIMALS, dimension);
        }

        /// <summary>
        /// Converts a specified length to an forced dimension, an specified number of decimal digits
        /// </summary>
        /// <param name="length">Specifies the length of an file in byte to convert</param>
        /// <param name="decimals">Specifies the number of decimal digits to shown in the result</param>
        /// <param name="dimension">Specifies a forced dimension to convert to</param>
        /// <returns>The converted length</returns>
        public static decimal ConvertNum(long length, uint decimals, Dimension dimension)
        {
            return ConvertNum(length, decimals, DEFAULT_BYTE_BASE, dimension);
        }

        /// <summary>
        /// Converts a specified length to an forced dimension, with an specified byte base
        /// </summary>
        /// <param name="length">Specifies the length of an file in byte to convert</param>
        /// <param name="byteBase">Specifies the byte base for conversion</param>
        /// <param name="dimension">Specifies a forced dimension to convert to</param>
        /// <returns>The converted length</returns>
        public static decimal ConvertNum(long length, ByteBase byteBase, Dimension dimension)
        {
            return ConvertNum(length, DEFAULT_DECIMALS, byteBase, dimension);
        }

        /// <summary>
        /// Converts a specified length to an forced dimension, with an specified byte base, an specified number of decimal digits
        /// </summary>
        /// <param name="length">Specifies the length of an file in byte to convert</param>
        /// <param name="decimals">Specifies the number of decimal digits to shown in the result</param>
        /// <param name="byteBase">Specifies the byte base for conversion</param>
        /// <param name="dimension">Specifies a forced dimension to convert to</param>
        /// <returns>The converted length</returns>
        public static decimal ConvertNum(long length, uint decimals, ByteBase byteBase, Dimension dimension)
        {
            return (decimal)Math.Round(length / Math.Pow((double)byteBase, (double)dimension), (int)decimals);
        }
        #endregion

        #region GetHighestDimension
        /// <summary>
        /// Returns the highest or maximum posible dimension of a given length, with an IEC byte base of 1024
        /// </summary>
        /// <param name="length">Specifiestrhe length to calculate the dimension with it</param>
        /// <returns>The highest or maximum posible dimension of the specified length</returns>
        public static uint GetHighestDimension(long length)
        {
            return GetHighestDimension(length, DEFAULT_BYTE_BASE);
        }
        /// <summary>
        /// Returns the highest or maximum posible dimension of a given length, with an specified byte base
        /// </summary>
        /// <param name="length">Specifiestrhe length to calculate the dimension with it</param>
        /// <param name="byteBase">Specifies the byte base faor calculating the dimension</param>
        /// <returns>The highest or maximum posible dimension of the specified length</returns>
        public static uint GetHighestDimension(long length, ByteBase byteBase)
        {
            return GetHighestDimension(length, byteBase, Dimension._NotSet_);
        }
        /// <summary>
        /// Returns the specified dimension or the maximum posible dimension of a given size, with an IEC byte base of 1024
        /// </summary>
        /// <param name="length">Specifies the length to calculate the dimension with it</param>
        /// <param name="forceDimension">Specifie the forced dimension</param>
        /// <returns>The forced or maximum posible dimension</returns>
        public static uint GetHighestDimension(long length, Dimension maxDimension)
        {
            return GetHighestDimension(length, DEFAULT_BYTE_BASE, maxDimension);
        }
        /// <summary>
        /// Returns the specified dimension or the maximum posible dimension of a given size, with an specified byte base
        /// </summary>
        /// <param name="length">Specifies the length to calculate the dimension with it</param>
        /// <param name="byteBase">Specifies the byte base faor calculating the dimension</param>
        /// <param name="forceDimension">Specifie the forced dimension</param>
        /// <returns>The forced or maximum posible dimension</returns>
        public static uint GetHighestDimension(long length, ByteBase byteBase, Dimension maxDimension)
        {
            // Get the optimal dimension
            uint HighestDimension = 0;
            length = Math.Abs(length);
            if (length > 0)
            {
                HighestDimension = (uint)Math.Floor(Math.Log(length, (int)byteBase));
            }

            // Limit to maximum Dimension
            switch (maxDimension)
            {
                case Dimension._NotSet_:
                    // Nothing to do
                    break;
                case Dimension._Automatic_:
                    // Set dimension to maximum posible by UnitPrefix Array
                    int MaxDimensionByArray;
                    switch (byteBase)
                    {
                        case ByteBase.SI:
                            MaxDimensionByArray = UnitPrefix_SI.Length;
                            break;
                        case ByteBase.IEC:
                        default:
                            MaxDimensionByArray = UnitPrefix_IEC.Length;
                            break;
                    }

                    if (HighestDimension > MaxDimensionByArray - 1)
                    {
                        HighestDimension = (uint)(MaxDimensionByArray - 1);
                    }
                    break;
                default:
                    // Set to requested maximum dimension
                    HighestDimension = (uint)maxDimension;
                    break;
            }
            return HighestDimension;
        }
        #endregion

        #region SetDimensionlistToComboBox
        /// <summary>
        /// Adds the units of the specified ByteBase to the specified combobox
        /// </summary>
        /// <param name="comboBox">Specifies the combobox to set</param>
        /// <param name="byteBase">Specifies the ByteBase of the units to add</param>
        /// <param name="clearItems">Specifies if the items of the specified combobox should been cleard bevor adding the units</param>
        /// <param name="AddByteBaseName">Specifies if the mame of the specified ByteBase should been added behind the unit name</param>
        public static void SetDimensionlistToComboBox(ComboBox comboBox, ByteBase byteBase, bool clearItems, bool AddByteBaseName)
        {
            // Clear Items
            if (clearItems) comboBox.Items.Clear();

            // Add Items
            if (byteBase == ByteBase.IEC)
            {
                foreach (string Unit in UnitPrefix_IEC)
                {
                    comboBox.Items.Add(Unit + (AddByteBaseName ? "    (IEC)" : string.Empty));
                }
            }
            if (byteBase == ByteBase.SI)
            {
                foreach (string Unit in UnitPrefix_SI)
                {
                    comboBox.Items.Add(Unit + (AddByteBaseName ? "    (SI)" : string.Empty));
                }
            }
        }
        /// <summary>
        /// Adds the units of the specified ByteBases to the specified combobox
        /// </summary>
        /// <param name="comboBox">Specifies the combobox to set</param>
        /// <param name="byteBaseFirst">Specifies the first ByteBase of the units to add</param>
        /// <param name="byteBaseSecond">Specifies the second ByteBase of the units to add</param>
        /// <param name="clearItems">Specifies if the items of the specified combobox should been cleard bevor adding the units</param>
        /// <param name="AddByteBaseName">Specifies if the mame of the specified ByteBase should been added behind the unit name</param>
        public static void SetDimensionlistToComboBox(ComboBox comboBox, ByteBase byteBaseFirst, ByteBase byteBaseSecond, bool clearItems, bool AddByteBaseName)
        {
            SetDimensionlistToComboBox(comboBox, byteBaseFirst, clearItems, AddByteBaseName);
            SetDimensionlistToComboBox(comboBox, byteBaseSecond, false, AddByteBaseName);
        }
        #endregion
        #endregion
    }
}
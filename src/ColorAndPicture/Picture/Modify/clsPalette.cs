/*
 * OLKI.Toolbox.ColorAndPicture.Picture
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to change the contrast of an image
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
using System.Drawing;

namespace OLKI.Toolbox.ColorAndPicture.Picture
{
    /// <summary>
    /// Class that provides tool to modify an image
    /// </summary>
    public static partial class Modify
    {
        public static partial class Palette
        {
            #region Constants
            /// <summary>
            /// Factor red for calculating the gray value of a color
            /// </summary>
            private const double DEFAULT_COLOR_GRAYVALUE_FACTOR_R = 0.299;
            /// <summary>
            /// Factor green for calculating the gray value of a color
            /// </summary>
            private const double DEFAULT_COLOR_GRAYVALUE_FACTOR_G = 0.587;
            /// <summary>
            /// Factor blue for calculating the gray value of a color
            /// </summary>
            private const double DEFAULT_COLOR_GRAYVALUE_FACTOR_B = 0.114;
            #endregion

            #region Enums
            public enum ColorPalette
            {
                Color = 0,
                Grayscale = 1,
                BlackWhite = 2,
            }
            #endregion

            #region Methodes
            #region GetGrayValue
            /// <summary>
            /// Convert an Color to its grayvalue
            /// </summary>
            /// <param name="inColor">Color to convert to grayvalue</param>
            /// <returns>Grayvalue of an given color</returns>
            private static int GetGrayValue(System.Drawing.Color inColor)
            {
                return GetGrayValue(inColor, DEFAULT_COLOR_GRAYVALUE_FACTOR_B, DEFAULT_COLOR_GRAYVALUE_FACTOR_G, DEFAULT_COLOR_GRAYVALUE_FACTOR_R);
            }

            /// <summary>
            /// Convert an Color to its grayvalue
            /// </summary>
            /// <param name="inColor">Color to convert to grayvalue</param>
            /// <param name="factorBlue">Factor blue for calculating the grayvalue of a color</param>
            /// <param name="factorGreen">Factor green for calculating the grayvalue of a color</param>
            /// <param name="factorRead">Factor red for calculating the grayvalue of a color</param>
            /// <returns>Grayvalue of an given color</returns>
            private static int GetGrayValue(System.Drawing.Color inColor, double factorBlue, double factorGreen, double factorRead)
            {
                return (int)Math.Round(factorRead * inColor.R + factorGreen * inColor.G + factorBlue * inColor.B, 0);
            }
            #endregion

            #region ToGrayscale
            /// <summary>
            /// Convert an image to grayscale palette
            /// </summary>
            /// <param name="image">Specifies the image to convert to grayscale palette</param>
            /// <returns>The image in grayscale palette</returns>
            public static Image ToGrayscale(Image image)
            {
                return ToGrayscale(image, DEFAULT_COLOR_GRAYVALUE_FACTOR_B, DEFAULT_COLOR_GRAYVALUE_FACTOR_G, DEFAULT_COLOR_GRAYVALUE_FACTOR_R);
            }

            /// <summary>
            /// Convert an image to grayscale palette
            /// </summary>
            /// <param name="image">Specifies the image to convert to grayscale palette</param>
            /// <param name="factorBlue">Factor blue for calculating the grayvalue of a color</param>
            /// <param name="factorGreen">Factor green for calculating the grayvalue of a color</param>
            /// <param name="factorRead">Factor red for calculating the grayvalue of a color</param>
            /// <returns>The image in grayscale palette</returns>
            public static Image ToGrayscale(Image image, double factorBlue, double factorGreen, double factorRead)
            {
                Bitmap TempBmp = (Bitmap)image.Clone();
                System.Drawing.Color OrgColor;
                for (int x = 0; x < TempBmp.Width; x++)
                {
                    for (int y = 0; y < TempBmp.Height; y++)
                    {
                        OrgColor = TempBmp.GetPixel(x, y);
                        int GrayValue = GetGrayValue(OrgColor, factorBlue, factorGreen, factorRead);
                        TempBmp.SetPixel(x, y, System.Drawing.Color.FromArgb(GrayValue, GrayValue, GrayValue));
                    }
                }
                return (Image)TempBmp.Clone();
            }
            #endregion

            #region ToBlackWhite
            /// <summary>
            /// Convert an image to black and white palette
            /// </summary>
            /// <param name="image">Specifies the image to convert to black and white palette</param>
            /// <param name="threshold">Threshold to make an pixel black or white, depending on this grayscale value</param>
            /// <returns>The image in black and white palette</returns>
            public static Image ToBlackWhite(Image image, int threshold)
            {
                return ToBlackWhite(image, threshold, DEFAULT_COLOR_GRAYVALUE_FACTOR_B, DEFAULT_COLOR_GRAYVALUE_FACTOR_G, DEFAULT_COLOR_GRAYVALUE_FACTOR_R);
            }

            /// <summary>
            /// Convert an image to black and white palette
            /// </summary>
            /// <param name="image">Specifies the image to convert to black and white palette</param>
            /// <param name="threshold">Threshold to make an pixel black or white, depending on this grayscale value</param>
            /// <param name="factorBlue">Factor blue for calculating the grayvalue of a color</param>
            /// <param name="factorGreen">Factor green for calculating the grayvalue of a color</param>
            /// <param name="factorRead">Factor red for calculating the grayvalue of a color</param>
            /// <returns>The image in black and white palette</returns>
            public static Image ToBlackWhite(Image image, int threshold, double factorBlue, double factorGreen, double factorRead)
            {
                Image Image = ToGrayscale(image, factorBlue, factorGreen, factorRead);

                Bitmap TempBmp = (Bitmap)Image.Clone();
                System.Drawing.Color OrgColor;
                for (int x = 0; x < TempBmp.Width; x++)
                {
                    for (int y = 0; y < TempBmp.Height; y++)
                    {
                        OrgColor = TempBmp.GetPixel(x, y);
                        int BwColor = OrgColor.R >= 255 - threshold ? 255 : 0;
                        TempBmp.SetPixel(x, y, System.Drawing.Color.FromArgb(BwColor, BwColor, BwColor));
                    }
                }
                return (Image)TempBmp.Clone();
            }
            #endregion
            #endregion
        }
    }
}
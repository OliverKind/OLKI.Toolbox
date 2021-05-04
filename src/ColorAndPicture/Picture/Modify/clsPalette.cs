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
using System.Drawing.Imaging;
using System.Threading.Tasks;

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
            /// <remarks>
            /// Based on the Article: "Fast Image Processing in C#" by "turgay"
            /// Published at http://csharpexamples.com/fast-image-processing-c/
            /// </remarks>
            /// <param name="image">Specifies the image to convert to grayscale palette</param>
            /// <param name="factorBlue">Factor blue for calculating the grayvalue of a color</param>
            /// <param name="factorGreen">Factor green for calculating the grayvalue of a color</param>
            /// <param name="factorRead">Factor red for calculating the grayvalue of a color</param>
            /// <returns>The image in grayscale palette</returns>
            public static Image ToGrayscale(Image image, double factorBlue, double factorGreen, double factorRead)
            {
                unsafe
                {
                    Bitmap TempBmp = (Bitmap)image.Clone();
                    BitmapData BitmapData = TempBmp.LockBits(new Rectangle(0, 0, TempBmp.Width, TempBmp.Height), ImageLockMode.ReadWrite, TempBmp.PixelFormat);

                    int BytesPerPixel = Image.GetPixelFormatSize(TempBmp.PixelFormat) / 8;
                    int HeightInPixels = BitmapData.Height;
                    int WidthInBytes = BitmapData.Width * BytesPerPixel;
                    byte* PtrFirstPixel = (byte*)BitmapData.Scan0;

                    // Calculate new grayscale
                    Parallel.For(0, HeightInPixels, y =>
                    {
                        byte* CurrentLine = PtrFirstPixel + (y * BitmapData.Stride);
                        for (int x = 0; x < WidthInBytes; x += BytesPerPixel)
                        {
                            System.Drawing.Color OrgColor = new System.Drawing.Color();
                            OrgColor = System.Drawing.Color.FromArgb(CurrentLine[x + 2], CurrentLine[x + 1], CurrentLine[x + 0]);

                            byte GrayValue = (byte)GetGrayValue(OrgColor, factorBlue, factorGreen, factorRead);

                            CurrentLine[x + 2] = GrayValue;
                            CurrentLine[x + 1] = GrayValue;
                            CurrentLine[x + 0] = GrayValue;
                        }
                    });
                    TempBmp.UnlockBits(BitmapData);
                    return (Image)TempBmp;
                }
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
            /// <remarks>
            /// Based on the Article: "Fast Image Processing in C#" by "turgay"
            /// Published at http://csharpexamples.com/fast-image-processing-c/
            /// </remarks>
            /// <param name="image">Specifies the image to convert to black and white palette</param>
            /// <param name="threshold">Threshold to make an pixel black or white, depending on this grayscale value</param>
            /// <param name="factorBlue">Factor blue for calculating the grayvalue of a color</param>
            /// <param name="factorGreen">Factor green for calculating the grayvalue of a color</param>
            /// <param name="factorRead">Factor red for calculating the grayvalue of a color</param>
            /// <returns>The image in black and white palette</returns>
            public static Image ToBlackWhite(Image image, int threshold, double factorBlue, double factorGreen, double factorRead)
            {
                unsafe
                {
                    Bitmap TempBmp = (Bitmap)image.Clone();
                    BitmapData BitmapData = TempBmp.LockBits(new Rectangle(0, 0, TempBmp.Width, TempBmp.Height), ImageLockMode.ReadWrite, TempBmp.PixelFormat);

                    int BytesPerPixel = Image.GetPixelFormatSize(TempBmp.PixelFormat) / 8;
                    int HeightInPixels = BitmapData.Height;
                    int WidthInBytes = BitmapData.Width * BytesPerPixel;
                    byte* PtrFirstPixel = (byte*)BitmapData.Scan0;

                    // Calculate new brightnes and contrast
                    Parallel.For(0, HeightInPixels, y =>
                    {
                        byte* CurrentLine = PtrFirstPixel + (y * BitmapData.Stride);
                        for (int x = 0; x < WidthInBytes; x += BytesPerPixel)
                        {
                            System.Drawing.Color OrgColor = new System.Drawing.Color();
                            OrgColor = System.Drawing.Color.FromArgb(CurrentLine[x + 2], CurrentLine[x + 1], CurrentLine[x + 0]);

                            int GrayValue = GetGrayValue(OrgColor, factorBlue, factorGreen, factorRead);
                            byte BwValue = (byte)(GrayValue >= 255 - threshold ? 255 : 0);

                            CurrentLine[x + 2] = BwValue;
                            CurrentLine[x + 1] = BwValue;
                            CurrentLine[x + 0] = BwValue;
                        }
                    });
                    TempBmp.UnlockBits(BitmapData);
                    return (Image)TempBmp;
                }
            }
            #endregion
            #endregion
        }
    }
}
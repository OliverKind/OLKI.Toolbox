﻿/*
 * OLKI.Toolbox.ColorAndPicture.Picture
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to change the brightness of an image
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
        /// <summary>
        /// Change the brightness of an image
        /// </summary>
        /// <param name="image">The original image to change the brightness</param>
        /// <param name="brightness">The value to change the brightness. Betwenn -255 and 255.</param>
        /// <returns>The image with modified brightness or NULL if an exception was thrown</returns>
        public static Image Brightness(Image image, int brightness)
        {
            return Brightness(image, brightness, out _);
        }

        /// <summary>
        /// Change the brightness of an image
        /// </summary>
        /// <remarks>
        /// Based on the Article: "Fast Image Processing in C#" by "turgay"
        /// Published at http://csharpexamples.com/fast-image-processing-c/
        /// </remarks>
        /// <param name="image">The original image to change the brightness</param>
        /// <param name="brightness">The value to change the brightness. Betwenn -255 and 255.</param>
        /// <param name="exception">Exception while change brightness</param>
        /// <returns>The image with modified brightness or NULL if an exception was thrown</returns>
        public static Image Brightness(Image image, int brightness, out Exception exception)
        {
            try
            {
                exception = null;
                if (brightness == 0) return image;  //Nothing to do
                unsafe
                {
                    Bitmap TempBmp = (Bitmap)image.Clone();
                    BitmapData BitmapData = TempBmp.LockBits(new Rectangle(0, 0, TempBmp.Width, TempBmp.Height), ImageLockMode.ReadWrite, TempBmp.PixelFormat);

                    int BytesPerPixel = Image.GetPixelFormatSize(TempBmp.PixelFormat) / 8;
                    int HeightInPixels = BitmapData.Height;
                    int WidthInBytes = BitmapData.Width * BytesPerPixel;
                    byte* PtrFirstPixel = (byte*)BitmapData.Scan0;

                    brightness = Color.BrightnesChangeLimiter(brightness);

                    // Calculate new brightnes and contrast
                    Parallel.For(0, HeightInPixels, y =>
                    {
                        byte* CurrentLine = PtrFirstPixel + (y * BitmapData.Stride);
                        for (int x = 0; x < WidthInBytes; x += BytesPerPixel)
                        {
                            System.Drawing.Color OrgColor = new System.Drawing.Color();
                            OrgColor = System.Drawing.Color.FromArgb(CurrentLine[x + 2], CurrentLine[x + 1], CurrentLine[x + 0]);

                            int NewR = CurrentLine[x + 2];
                            int NewG = CurrentLine[x + 1];
                            int NewB = CurrentLine[x + 0];

                            NewR = Color.ColorLimiter(OrgColor.R + brightness);
                            NewG = Color.ColorLimiter(OrgColor.G + brightness);
                            NewB = Color.ColorLimiter(OrgColor.B + brightness);

                            CurrentLine[x + 2] = (byte)NewR;
                            CurrentLine[x + 1] = (byte)NewG;
                            CurrentLine[x + 0] = (byte)NewB;
                        }
                    });
                    TempBmp.UnlockBits(BitmapData);
                    return (Image)TempBmp;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return null;
            }
        }
    }
}
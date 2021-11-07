/*
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

namespace OLKI.Toolbox.ColorAndPicture.Picture
{
    /// <summary>
    /// Class that provides tool to modify an image
    /// </summary>
    public static partial class Modify
    {
        /// <summary>
        /// Rote an image 90 degrees Left hand wise (counterclockwise)
        /// </summary>
        /// <param name="image">The original image to rotate</param>
        /// <returns>The rotated image or NULL if an exception was thrown</returns>
        public static Image Rotate90Left(Image image)
        {
            return Rotate90Left(image, out _);
        }

        /// <summary>
        /// Rote an image 90 degrees Left hand wise (counterclockwise)
        /// </summary>
        /// <remarks>
        /// Based on the Article: "Fast Image Processing in C#" by "turgay"
        /// Published at http://csharpexamples.com/fast-image-processing-c/
        /// </remarks>
        /// <param name="image">The original image to rotate</param>
        /// <param name="exception">Exception while rotate</param>
        /// <returns>The rotated image or NULL if an exception was thrown</returns>
        public static Image Rotate90Left(Image image, out Exception exception)
        {
            try
            {
                exception = null;
                unsafe
                {
                    Bitmap TempBmp = (Bitmap)image.Clone();
                    TempBmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    return (Image)TempBmp;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return null;
            }
        }

        /// <summary>
        /// Rote an image 90 degrees Right hand wise (clockwise)
        /// </summary>
        /// <param name="image">The original image to rotate</param>
        /// <returns>The rotated image or NULL if an exception was thrown</returns>
        public static Image Rotate90Right(Image image)
        {
            return Rotate90Right(image, out _);
        }

        /// <summary>
        /// Rote an image 90 degrees Right hand wise (clockwise)
        /// </summary>
        /// <remarks>
        /// Based on the Article: "Fast Image Processing in C#" by "turgay"
        /// Published at http://csharpexamples.com/fast-image-processing-c/
        /// </remarks>
        /// <param name="image">The original image to rotate</param>
        /// <param name="exception">Exception while rotate</param>
        /// <returns>The rotated image or NULL if an exception was thrown</returns>
        public static Image Rotate90Right(Image image, out Exception exception)
        {
            try
            {
                exception = null;
                unsafe
                {
                    Bitmap TempBmp = (Bitmap)image.Clone();
                    TempBmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
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
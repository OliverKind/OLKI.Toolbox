/*
 * OLKI.Toolbox.ColorAndPicture.Picture
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to crop an image
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
        /// Crop an image the the defined area.
        /// </summary>
        /// <param name="image">Image to crop</param>
        /// <param name="cropArea">The area to keep</param>
        /// <returns>Cropped image or NULL if an exception was thrown</returns>
        public static Image Crop(Image image, Rectangle? cropArea)
        {
            return Crop(image, cropArea, out _);
        }

        /// <summary>
        /// Crop an image the the defined area
        /// </summary>
        /// <param name="image">Image to crop</param>
        /// <param name="cropArea">The area to keep</param>
        /// <param name="exception">Exception while cropping</param>
        /// <returns>Cropped image or NULL if an exception was thrown</returns>
        public static Image Crop(Image image, Rectangle? cropArea, out Exception exception)
        {
            try
            {
                exception = null;
                if (image == null || cropArea == null) return image;

                Bitmap Image = new Bitmap(image);
                return Image.Clone((Rectangle)cropArea, Image.PixelFormat);
            }
            catch (Exception ex)
            {
                exception = ex;
                return null;
            }
        }
    }
}
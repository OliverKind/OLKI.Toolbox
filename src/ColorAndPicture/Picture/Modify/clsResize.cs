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
using System.Drawing.Drawing2D;

namespace OLKI.Toolbox.ColorAndPicture.Picture
{
    /// <summary>
    /// Class that provides tool to modify an image
    /// </summary>
    public static partial class Modify
    {
        #region Resize
        /// <summary>
        /// Resize the given Image by the given factor with constant aspect ratio. The Resolution will be fittet to the new size.
        /// </summary>
        /// <param name="image">Image to resize</param>
        /// <param name="factor">Resice factor in Percentage</param>
        /// <returns>Resized image or NULL if an exception was thrown</returns>
        public static Image Resize(Image image, decimal factor)
        {
            return Resize(image, factor, true);
        }

        /// <summary>
        /// Resize the given Image by the given factor with constant aspect ratio. The Resolution will  be fittet to the new size.
        /// </summary>
        /// <param name="image">Image to resize</param>
        /// <param name="factor">Resice factor in Percentage</param>
        /// <param name="exception">Exception while resize image</param>
        /// <returns>Resized image or NULL if an exception was thrown</returns>
        public static Image Resize(Image image, decimal factor, out Exception exception)
        {
            return Resize(image, factor, true, out exception);
        }

        /// <summary>
        /// Resize the given Image by the given factor with constant aspect ratio. The Resolution can be fittet to the new size.
        /// </summary>
        /// <param name="image">Image to resize</param>
        /// <param name="factor">Resice factor in Percentage</param>
        /// <param name="fitResolution">Set if the resolution should be fittet to the new size</param>
        /// <returns>Resized image or NULL if an exception was thrown</returns>
        public static Image Resize(Image image, decimal factor, bool fitResolution)
        {
            return Resize(image, factor, fitResolution, out _);
        }

        /// <summary>
        /// Resize the given Image by the given factor with constant aspect ratio. The Resolution can be fittet to the new size.
        /// </summary>
        /// <param name="image">Image to resize</param>
        /// <param name="factor">Resice factor in Percentage</param>
        /// <param name="fitResolution">Set if the resolution should be fittet to the new size</param>
        /// <param name="exception">Exception while resize image</param>
        /// <returns>Resized image or NULL if an exception was thrown</returns>
        public static Image Resize(Image image, decimal factor, bool fitResolution, out Exception exception)
        {
            exception = null;
            if (factor == 100) return image;  //Nothing to do

            try
            {
                exception = null;
                //System.Convert.ToInt32
                int NewHeight = Convert.ToInt32(image.Size.Height * (factor / 100));
                int NewWidth = Convert.ToInt32(image.Size.Width * (factor / 100));

                return Resize(image, new Size(NewWidth, NewHeight), fitResolution, out exception);
            }
            catch (Exception ex)
            {
                exception = ex;
                return null;
            }
        }

        /// <summary>
        /// Resize the given Image to the new size. The Resolution will be fittet to the new size.
        /// </summary>
        /// <param name="image">Image to resize</param>
        /// <param name="newSize">New image Size</param>
        /// <returns>Resized image or NULL if an exception was thrown</returns>
        public static Image Resize(Image image, Size newSize)
        {
            return Resize(image, newSize, true);
        }

        /// <summary>
        /// Resize the given Image to the new size. The Resolution will be fittet to the new size.
        /// </summary>
        /// <param name="image">Image to resize</param>
        /// <param name="newSize">New image Size</param>
        /// <param name="exception">Exception while resize image</param>
        /// <returns>Resized image or NULL if an exception was thrown</returns>
        public static Image Resize(Image image, Size newSize, out Exception exception)
        {
            return Resize(image, newSize, true, out exception);
        }

        /// <summary>
        /// Resize the given Image to the new size. The Resolution can be fittet to the new size.
        /// </summary>
        /// <param name="image">Image to resize</param>
        /// <param name="newSize">New image Size</param>
        /// <param name="fitResolution">Set if the resolution should be fittet to the new size</param>
        /// <returns>Resized image or NULL if an exception was thrown</returns>
        public static Image Resize(Image image, Size newSize, bool fitResolution)
        {
            return Resize(image, newSize, fitResolution, out _);
        }

        /// <summary>
        /// Resize the given Image to the new size. The Resolution can be fittet to the new size.
        /// </summary>
        /// <param name="image">Image to resize</param>
        /// <param name="newSize">New image Size</param>
        /// <param name="fitResolution">Set if the resolution should be fittet to the new size</param>
        /// <param name="exception">Exception while resize image</param>
        /// <returns>Resized image or NULL if an exception was thrown</returns>
        public static Image Resize(Image image, Size newSize, bool fitResolution, out Exception exception)
        {
            try
            {
                exception = null;
                unsafe
                {
                    Bitmap TempBmp = new Bitmap(newSize.Width, newSize.Height);

                    if (fitResolution)
                    {
                        float NewRWidth = (image.HorizontalResolution * ((float)newSize.Width / (float)image.Size.Width));
                        float NewRHeight = (image.VerticalResolution * ((float)newSize.Height / (float)image.Size.Height));
                        TempBmp.SetResolution(NewRWidth, NewRHeight);
                    }

                    Graphics TempGra = Graphics.FromImage(TempBmp);
                    TempGra.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    TempGra.DrawImage(image, 0, 0, newSize.Width, newSize.Height);
                    TempGra.Dispose();

                    return (Image)TempBmp;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return null;
            }
        }
        #endregion

        #region GetSizeByDPI
        /// <summary>
        /// Get image size by modification of Resolution
        /// </summary>
        /// <param name="image">Image to resize</param>
        /// <param name="newDpi">New Resolution</param>
        /// <returns>Image Size to new Resolution or Size(0,0) if an exception was thrown</returns>
        public static Size GetSizeByDPI(Image image, int newDpi)
        {
            return GetSizeByDPI(image, newDpi, out _);
        }

        /// <summary>
        /// Get image size by modification of Resolution
        /// </summary>
        /// <param name="image">Image to resize</param>
        /// <param name="newDpi">New Resolution</param>
        /// <param name="exception">Exception while calculation new image Size</param>
        /// <returns>Image Size to new Resolution or Size(0,0) if an exception was thrown</returns>
        public static Size GetSizeByDPI(Image image, int newDpi, out Exception exception)
        {
            try
            {
                exception = null;
                float Factor = newDpi / image.HorizontalResolution;
                return new Size(Convert.ToInt32(image.Size.Width * Factor), Convert.ToInt32(image.Size.Height * Factor));
            }
            catch (Exception ex)
            {
                exception = ex;
                return new Size(0, 0);
            }
        }
        #endregion
    }
}
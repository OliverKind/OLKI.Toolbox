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

using System.Drawing;

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
        /// <returns>The image with modified brightness</returns>
        public static Image Brightness(Image image, int brightness)
        {
            Bitmap TempBmp = (Bitmap)image.Clone();

            // Brightnes preprocess
            if (brightness < -255) brightness = -255;
            if (brightness > 255) brightness = 255;

            // Calculate new brightness
            System.Drawing.Color OrgColor;
            int NewR;
            int NewG;
            int NewB;
            for (int x = 0; x < TempBmp.Width; x++)
            {
                for (int y = 0; y < TempBmp.Height; y++)
                {
                    OrgColor = TempBmp.GetPixel(x, y);
                    NewR = Color.LimitTo255(OrgColor.R + brightness);
                    NewG = Color.LimitTo255(OrgColor.G + brightness);
                    NewB = Color.LimitTo255(OrgColor.B + brightness);

                    TempBmp.SetPixel(x, y, System.Drawing.Color.FromArgb(NewR, NewG, NewB));
                }
            }
            return (Image)TempBmp.Clone();
        }
    }
}
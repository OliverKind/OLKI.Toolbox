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
        /// <summary>
        /// Change the contrast of an image
        /// </summary>
        /// <param name="image">The original image to change the contrast</param>
        /// <param name="contrast">The value to change the contrast. Betwenn -255 and 255.</param>
        /// <returns>The image with modified contrast</returns>
        public static Image Contrast(Image image, double contrast)
        {
            Bitmap TempBmp = (Bitmap)image.Clone();

            // Contrast preprocess
            if (contrast < -100) contrast = -100;
            if (contrast > 100) contrast = 100;
            double ContrastFactor = Math.Pow((100.0 + contrast) / 100.0, 2);

            // Calculate new contrast
            System.Drawing.Color OrgColor;
            int NewR;
            int NewG;
            int NewB;
            for (int x = 0; x < TempBmp.Width; x++)
            {
                for (int y = 0; y < TempBmp.Height; y++)
                {
                    OrgColor = TempBmp.GetPixel(x, y);
                    NewR = (int)Math.Round(((((OrgColor.R / 255.0) - 0.5) * ContrastFactor) + 0.5) * 255.0, 0);
                    if (NewR < 0) NewR = 0;
                    if (NewR > 255) NewR = 255;

                    NewG = (int)Math.Round(((((OrgColor.G / 255.0) - 0.5) * ContrastFactor) + 0.5) * 255.0, 0);
                    if (NewG < 0) NewG = 0;
                    if (NewG > 255) NewG = 255;

                    NewB = (int)Math.Round(((((OrgColor.B / 255.0) - 0.5) * ContrastFactor) + 0.5) * 255.0, 0);
                    if (NewB < 0) NewB = 0;
                    if (NewB > 255) NewB = 255;

                    TempBmp.SetPixel(x, y, System.Drawing.Color.FromArgb(NewR, NewG, NewB));
                }
            }
            return (Image)TempBmp.Clone();
        }
    }
}
/*
 * OLKI.Toolbox.Widgets
 * 
 * Initial Author: Oliver Kind - 2022
 * License:        LGPL
 * 
 * Desctiption:
 * An PictureBox to crop images
 * 
 * This code was inspired by sorce code written by Hexaholic, 15-Jun-2021
 * Original Autor:      Hexaholic, 15-Jun-2021
 * Original Source:     https://coderedirect.com/questions/145089/creating-custom-picturebox-with-draggable-and-resizable-selection-window
 * Original Titel:      Creating Custom Picturebox with Draggable and Resizable Selection Window
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

using OLKI.Toolbox.ColorAndPicture.Picture;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OLKI.Toolbox.Widgets
{
    /// <summary>
    /// A PictureBox to crop images
    /// </summary>
    public partial class PictrueBoxCrop : PictureBox
    {
        #region Events
        /// <summary>
        /// Raised if the CropFrameS Size was changed
        /// </summary>
        public event EventHandler CropFrameSizeChanged;
        /// <summary>
        /// Raised if the CropFrameS Position was changed
        /// </summary>
        public event EventHandler CropFrameMove;
        #endregion

        #region Properties
        #region Crop
        /// <summary>
        /// Get or set if the Crop Area can be created or removed with mouse click
        /// </summary>
        [Category("_Crop")]
        [DisplayName("Add and remove Crop Area with mouse")]
        [Description("Can the Crop Area be created or removed with mouse click")]
        private bool AddRemoveCropAreaWithMouseClick { get; set; } = true;

        /// <summary>
        /// Get the selected Crop Area
        /// </summary>
#if !DEBUG
        [Browsable(false)]
#endif
        [Category("_Crop")]
        [DisplayName("Selected Crop Area Frame")]
        [Description("The selected area to crop")]
        internal RectangleCreator CropAreaSelectionFrame { get; private set; } = new RectangleCreator();

        /// <summary>
        /// Get the selected Crop Area
        /// </summary>
        [Category("_Crop")]
        [DisplayName("Selected Crop Area")]
        [Description("The selected area to crop")]
        public Rectangle? CropAreaSelection
        {
            get
            {
                Rectangle CropAreaSelection = new Rectangle(this.CropAreaSelectionFrame.Location, this.CropAreaSelectionFrame.Size);
                if (!this.GetShowRectangle(CropAreaSelection)) return null;
                return CropAreaSelection;
            }
        }

        /// <summary>
        /// Get the croped area, fitted to the image
        /// </summary>
        [Category("_Crop")]
        [DisplayName("Fitted crop area")]
        [Description("The croped area, fitted to the image")]
        public Rectangle? CropAreaFitToImage
        {
            get
            {
                Rectangle CropAreaSelection = new Rectangle(this.CropAreaSelectionFrame.Location, this.CropAreaSelectionFrame.Size);
                if (!this.GetShowRectangle(CropAreaSelection)) return null;

                Rectangle FitArea = CropAreaSelection;
                FitArea.Width = (int)Math.Round(FitArea.Width / this.ScaleFactor, 0);
                FitArea.Height = (int)Math.Round(FitArea.Height / this.ScaleFactor, 0);

                if (this.RatioImage >= this.RatioBox)
                {
                    FitArea.X = (int)Math.Round(FitArea.X / this.ScaleFactor, 0);
                    FitArea.Y = (int)Math.Round((FitArea.Y - (int)Math.Round(this.Height / 2 - (Image.Height * ScaleFactor) / 2, 0)) / ScaleFactor, 0);
                }
                else
                {
                    FitArea.X = (int)Math.Round((FitArea.X - (int)Math.Round(this.Width / 2 - (Image.Width * ScaleFactor) / 2, 0)) / ScaleFactor, 0);
                    FitArea.Y = (int)Math.Round(FitArea.Y / this.ScaleFactor, 0);
                }

                if (FitArea.X < 0)
                {
                    FitArea.Width += FitArea.X;
                    FitArea.X = 0;
                }
                if (FitArea.X + FitArea.Width > Image.Width) FitArea.Width = Image.Width - FitArea.X;
                if (FitArea.Width <= 0) FitArea.Width = 0;

                if (FitArea.Y < 0)
                {
                    FitArea.Height += FitArea.Y;
                    FitArea.Y = 0;
                }
                if (FitArea.Y + FitArea.Height > Image.Height) FitArea.Height = Image.Height - FitArea.Y;
                if (FitArea.Height <= 0) FitArea.Height = 0;

                if (FitArea.Width == 0 && FitArea.Height == 0) return null;

                return FitArea;
            }
        }

        /// <summary>
        /// Get the croped image, if it was set
        /// </summary>
        [Category("_Crop")]
        [DisplayName("Croped image")]
        [Description("The croped image, if it was set")]
        public Image CropedImage
        {
            get
            {
                try
                {
                    if (this.Image == null) return null;
                    if (this.SizeMode != PictureBoxSizeMode.Zoom) return null;
                    if (!this._cropMode) return null;
                    if (this.CropAreaFitToImage == null) return null;
                    if (this.CropAreaFitToImage.Value.Width < 0) return null;
                    if (this.CropAreaFitToImage.Value.Height < 0) return null;

                    return Modify.Crop(this.Image, this.CropAreaFitToImage);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print(ex.ToString());
                    return null;
                }
            }
        }

        /// <summary>
        /// PictureBox is in crop mode
        /// </summary>
        private bool _cropMode = true;
        /// <summary>
        /// Get or set if the PictureBox is in crop mode
        /// </summary>
        [Category("_Crop")]
        [DisplayName("Crop mode active")]
        [Description("PictureBox is in crop mode")]
        public bool CropMode
        {
            get
            {
                return this._cropMode;
            }
            set
            {
                this._cropMode = value;
                if (this._cropMode) this.CropFrameAdd(null);
                if (!this._cropMode) this.CropFrameRemove();
            }
        }

        /// <summary>
        /// Get or set the initial Size of the crop area
        /// </summary>
        [Category("_Crop")]
        [DisplayName("Initial Crop Area size")]
        [Description("Initial Size of the Crop Area")]
        public Size InitialCropSize { get; set; } = new Size(100, 100);
        #endregion

        #region Crop - Additional
        /// <summary>
        /// Get the area where the image is placed, if SizeMode is Zoom
        /// </summary>
        [Category("_Crop - Additional")]
        [DisplayName("Fitted Image Area")]
        [Description("The area where the image is placed, if SizeMode is Zoom")]
        public Rectangle? ImageAreaFit
        {
            get
            {
                if (this.Image == null || this.SizeMode != PictureBoxSizeMode.Zoom || !this._cropMode) return null;
                if (this.RatioImage >= this.RatioBox)
                {
                    int ImageScaleHeigt = (int)Math.Round((float)(this.Image.Height * this.ScaleFactor), 0);
                    int ImagePosY = (int)Math.Round((float)(this.Height / 2 - ImageScaleHeigt / 2), 0);
                    return new Rectangle(new Point(0, ImagePosY), new Size(this.Width, ImageScaleHeigt));
                }
                else
                {
                    int ImageScaleWidth = (int)Math.Round((float)(this.Image.Width * this.ScaleFactor), 0);
                    int ImagePosX = (int)Math.Round((float)(this.Width / 2 - ImageScaleWidth / 2), 0);
                    return new Rectangle(new Point(ImagePosX, 0), new Size(ImageScaleWidth, this.Height));
                }
            }
        }

        /// <summary>
        /// Get the ratio of the PictureBox
        /// </summary>
        [Category("_Crop - Additional")]
        [DisplayName("PictureBox ratio")]
        [Description("The ratio of the PictureBox")]
        public float RatioBox
        {
            get
            {
                return (float)this.Width / (float)this.Height;
            }
        }

        /// <summary>
        /// Get the ratio of the image
        /// </summary>
        [Category("_Crop - Additional")]
        [DisplayName("Image ratio")]
        [Description("The ratio of the image")]
        public float RatioImage
        {
            get
            {
                if (this.Image == null) return 0;
                return (float)this.Image.Width / (float)this.Image.Height;
            }
        }

        /// <summary>
        /// Get the scale factor of the image, if SizeMode is Zoom
        /// </summary>
        [Category("_Crop - Additional")]
        [DisplayName("Image scale factor")]
        [Description("Scale factor of the image, if SizeMode is Zoom")]
        public float ScaleFactor
        {
            get
            {
                float scaleFactor = 0;
                if (this.Image == null || this.SizeMode != PictureBoxSizeMode.Zoom) return scaleFactor;

                if (this.RatioImage >= this.RatioBox)
                {
                    scaleFactor = (float)this.Width / (float)this.Image.Width;
                }
                else
                {
                    scaleFactor = (float)this.Height / (float)this.Image.Height;
                }
                return scaleFactor;
            }
        }
        #endregion

        /// <summary>
        /// Encapsulates the information needed when creating the control. Remove flickering.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                return cp;
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Initial a new PictureBox to crop images
        /// </summary>
        public PictrueBoxCrop()
        {
            CropAreaSelectionFrame.SizeChanged += new EventHandler(this.ToggleCropFrameSizeChanged);
            CropAreaSelectionFrame.Move += new EventHandler(this.ToggleCropFrameMove);
        }

        /// <summary>
        /// Add the frame to select the crop are to the PictureBox
        /// </summary>
        /// <param name="initialLocation">Initial location to place the Frame, or NULL to place it in middle</param>
        public void CropFrameAdd(Point? initialLocation)
        {
            CropAreaSelectionFrame.Size = InitialCropSize;
            if (initialLocation == null)
            {
                CropAreaSelectionFrame.Location = new Point((this.Width - this.InitialCropSize.Width) / 2, (this.Height - this.InitialCropSize.Height) / 2);
            }
            else
            {
                CropAreaSelectionFrame.Location = (Point)initialLocation;
            }

            this.Controls.Add(CropAreaSelectionFrame);
        }

        /// <summary>
        /// Remove the frame to select the crop from to the PictureBox
        /// </summary>
        public void CropFrameRemove()
        {
            this.Controls.Remove(CropAreaSelectionFrame);
        }

        /// <summary>
        /// Toggle CropFrameMove event
        /// </summary>
        /// <param name="sender">Sender of changed event</param>
        /// <param name="e">EventArgs of changed event</param>
        private void ToggleCropFrameMove(object sender, EventArgs e)
        {
            CropFrameMove?.Invoke(sender, e);
        }

        /// <summary>
        /// Toggle CropFrameSizeChanged event
        /// </summary>
        /// <param name="sender">Sender of changed event</param>
        /// <param name="e">EventArgs of changed event</param>
        private void ToggleCropFrameSizeChanged(object sender, EventArgs e)
        {
            CropFrameSizeChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Get if the cropping rectangle should been shown
        /// </summary>
        /// <param name="rectangle">Cropping rectangle</param>
        /// <returns>True if the rectangle should been shown</returns>
        private bool GetShowRectangle(Rectangle? rectangle)
        {
            if (this.Image == null) return false;
            if (this.SizeMode != PictureBoxSizeMode.Zoom) return false;
            if (!this._cropMode) return false;
            if (!rectangle.HasValue) return false;

            if (rectangle.Value.Width < 0) return false;
            if (rectangle.Value.Height < 0) return false;

            return true;
        }

        /// <summary>
        /// Create an now Crop Area with start position and no size and call base.OnMouseDown(e)
        /// </summary>
        /// <param name="e">Provides data for the MouseUp, MouseDown, and MouseMove events.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.AddRemoveCropAreaWithMouseClick && e.Button == MouseButtons.Left && this.SizeMode == PictureBoxSizeMode.Zoom && !this._cropMode)
            {
                this.CropFrameAdd(new Point(e.X, e.Y));
                this._cropMode = true;
            }
            if (this.AddRemoveCropAreaWithMouseClick && e.Button == MouseButtons.Right)
            {
                this.CropFrameRemove();
                this._cropMode = false;
            }
            base.OnMouseDown(e);

            this.ToggleCropFrameMove(this, new EventArgs());
            this.ToggleCropFrameSizeChanged(this, new EventArgs());
        }
        #endregion

        #region SubClasses
        /// <summary>
        /// Class to provide a rectangle to create the crop are
        /// </summary>
        internal class RectangleCreator : Control
        {
            #region Constants
            private const int WM_NCHITTEST = 0x84;
            private const int WM_SETCURSOR = 0x20;
            private const int WM_NCLBUTTONDBLCLK = 0xA3;
            #endregion

            #region Properties
            /// <summary>
            /// Pen to draw the rectangle
            /// </summary>
            private Pen _rectanglePen = new Pen(Color.Red, 4);

            /// <summary>
            /// Get or set the Pen to draw the rectangle
            /// </summary>
            [Category("_Drawing")]
            [DisplayName("Rectangle Pen")]
            [Description("Pen settings to draw the rectange")]
            public Pen RectanglePen
            {
                get
                {
                    return this._rectanglePen;
                }
                set
                {
                    this._rectanglePen = value;
                }
            }
            #endregion

            #region Methodes
            /// <summary>
            /// Initial an new RectangleCreator
            /// </summary>
            public RectangleCreator()
            {
                SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                this.BackColor = Color.Transparent;
                this.DoubleBuffered = true;
                this.ResizeRedraw = true;

                this._rectanglePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            }

            /// <summary>
            /// Paint the Rectable and call base.OnPaint(e)
            /// </summary>
            /// <param name="e">Provides data for the Paint event.</param>
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                e.Graphics.DrawRectangle(this._rectanglePen, 0, 0, this.Width - 1, this.Height - 1);
            }

            protected override void WndProc(ref Message m)
            {
                int SELECTION_WIDTH = 10;
                if (m.Msg == WM_SETCURSOR)  //Setting cursor to SizeAll
                {
                    if ((m.LParam.ToInt32() & 0xffff) == 0x2)  //Move
                    {
                        Cursor.Current = Cursors.SizeAll;
                        m.Result = (IntPtr)1;
                        return;
                    }
                }
                if ((m.Msg == WM_NCLBUTTONDBLCLK)) //Disable Maximize on Double click
                {
                    m.Result = (IntPtr)1;
                    return;
                }
                base.WndProc(ref m);
                if (m.Msg == WM_NCHITTEST)
                {
                    var pos = PointToClient(new Point(m.LParam.ToInt32() & 0xffff,
                        m.LParam.ToInt32() >> 16));
                    if (pos.X <= ClientRectangle.Left + SELECTION_WIDTH && pos.Y <= ClientRectangle.Top + SELECTION_WIDTH)
                    {
                        m.Result = new IntPtr(13); //TopLEft
                    }
                    else if (pos.X >= ClientRectangle.Right - SELECTION_WIDTH && pos.Y <= ClientRectangle.Top + SELECTION_WIDTH)
                    {
                        m.Result = new IntPtr(14); //TopRight
                    }
                    else if (pos.X <= ClientRectangle.Left + SELECTION_WIDTH && pos.Y >= ClientRectangle.Bottom - SELECTION_WIDTH)
                    {
                        m.Result = new IntPtr(16); //BottomLeft
                    }
                    else if (pos.X >= ClientRectangle.Right - SELECTION_WIDTH && pos.Y >= ClientRectangle.Bottom - SELECTION_WIDTH)
                    {
                        m.Result = new IntPtr(17); //BottomRight
                    }
                    else if (pos.X <= ClientRectangle.Left + SELECTION_WIDTH)
                    {
                        m.Result = new IntPtr(10); //Left
                    }
                    else if (pos.Y <= ClientRectangle.Top + SELECTION_WIDTH)
                    {
                        m.Result = new IntPtr(12); //Top
                    }
                    else if (pos.X >= ClientRectangle.Right - SELECTION_WIDTH)
                    {
                        m.Result = new IntPtr(11); //Right
                    }
                    else if (pos.Y >= ClientRectangle.Bottom - SELECTION_WIDTH)
                    {
                        m.Result = new IntPtr(15); //Bottom
                    }
                    else
                    {
                        m.Result = new IntPtr(2); //Move
                    }
                }
            }
            #endregion
        }
        #endregion
    }
}
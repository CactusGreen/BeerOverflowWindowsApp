﻿using BeerOverflowWindowsApp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace BeerOverflowWindowsApp
{
    public partial class ManualBarRating : Control
    {
        List<Rectangle> beerGlassList;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        private int imageSize;
        public int ImageSize
        {
            get { return imageSize; }
            set
            {
                this.imageSize = value;
                this.SetImages();
                this.MinimumSize = new Size(imageSize * 5, imageSize);
                this.MaximumSize = new Size(imageSize * 5, imageSize);
                for (int i = 0; i < 5; i++)
                {
                    beerGlassList[i] = (new Rectangle(i * imageSize, 0, imageSize, imageSize));
                }
                this.Refresh();
            }
        }

        public string Rating
        {
            get { return numberOfGlasses + 1 + ""; }
            private set { }
        }
        ToolTip toolTip;
        public ManualBarRating()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            this.imageSize = 100;
            this.MinimumSize = new Size(imageSize * 5, imageSize);
            this.MaximumSize = new Size(imageSize * 5, imageSize);
            this.beerGlassList = new List<Rectangle>(5);
            for(int i = 0; i < 5; i++)
            {
                beerGlassList.Add(new Rectangle(i * imageSize, 0, imageSize, imageSize));
            }
            SetImages();
            toolTip = new ToolTip();
            toolTip.ShowAlways = true;
            Refresh();
        }

        Image selectedImage;
        Image unSelectedImage;
        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            for (int i = 0; i <= numberOfGlasses; i++)
            {
                g.DrawImageUnscaledAndClipped(selectedImage, beerGlassList[i]);
            }
            for(int i = numberOfGlasses + 1; i < 5; i++)
            {
                g.DrawImageUnscaledAndClipped(unSelectedImage, beerGlassList[i]);
            }
        }

        Boolean mousePressed = false;
        private int numberOfGlasses = 0;
        private void ManualBarRating_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousePressed = true;
                SelectRating(e);
            }
        }

        private const string toolTipMessage1 = "Got in a fight with a bartender";
        private const string toolTipMessage2 = "Half empty";
        private const string toolTipMessage3 = "Meh";
        private const string toolTipMessage4 = "Good enough";
        private const string toolTipMessage5 = "Beeroverflow!";
        private string[] toolTipMessages = new string[5] {toolTipMessage1, toolTipMessage2, toolTipMessage3, toolTipMessage4, toolTipMessage5 };
        int lastLocation = -1;
        private void ManualBarRating_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
                SelectRating(e);
            if(ClientRectangle.Contains(e.Location) && (e.X / imageSize) != lastLocation)
            {
                lastLocation = e.X / imageSize;
                toolTip.Show(toolTipMessages[e.X / imageSize], this, e.X + 10, e.Y + 10, 3000);
            }
        }

        private void ManualBarRating_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mousePressed = false;
        }

        private void ManualBarRating_MouseLeave(object sender, System.EventArgs e)
        {
            lastLocation = -1;
            toolTip.Hide(this);
        }

        private void SelectRating(MouseEventArgs e)
        {
            for (int i = 4; i >= 0; i--)
            {
                if (beerGlassList[i].Contains(e.Location))
                {
                    numberOfGlasses = i;
                    Refresh();
                    break;
                }
            }
        }

        private void SetImages()
        {
            selectedImage = ResizeImage(Resources.LightBeerGlass, imageSize, imageSize);
            unSelectedImage = ResizeImage(Resources.DarkBeerGlass, imageSize, imageSize);
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }
    }
}

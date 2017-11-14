using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WebApi.BarRaters
{
    [Description("A manual beer meter")]
    public sealed partial class ManualBeerMeter : Control
    {
        private Rectangle marker;
        public string Rating { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int markerHeight
        {
            set
            {
                this.marker.Height = value;
                this.UpdateBeerMeter();
            }
            get { return this.marker.Height; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int markerWidth
        {
            set
            {
                this.marker.Width = value;
                this.UpdateBeerMeter();
            }
            get { return this.marker.Width; }
        }

        private Rectangle cr;
        public ManualBeerMeter()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            markerHeight = 10;
            markerWidth = 30;
            this.Refresh();
        }

        private int topY;
        private int botY;
        protected override void OnResize(EventArgs e)
        {
            cr = ClientRectangle;
            cr.Inflate(-1, -1);
            this.UpdateBeerMeter();
        }

        public void UpdateBeerMeter()
        {
            marker = new Rectangle((cr.Width - marker.Width) / 2, cr.Bottom - marker.Height, marker.Width, marker.Height);
            topY = cr.Top + marker.Height / 2;
            botY = cr.Bottom - marker.Height / 2;
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            g.FillRectangle(new SolidBrush(Parent.BackColor), cr);
            g.DrawLine(Pens.Black, cr.X + cr.Width / 2, topY, cr.X + cr.Width / 2, botY);
            g.DrawEllipse(Pens.Black, marker);
            g.DrawString(text, new Font("Arial", 16), new SolidBrush(Color.Black), cr.X, cr.Top + 45);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        private int difference = 0;
        private Boolean mousePressed = false;
        private void ManualBeerMeter_MouseDown(object sender, MouseEventArgs e)
        {
            if (marker.Contains(e.X, e.Y) && e.Button == MouseButtons.Left)
            {
                difference = e.Y - marker.Y;
                mousePressed = true;
            }
        }

        private string text = "0 %";
        private const string textToDisplay = "BEEROVERFLOW!!!";
        private void ManualBeerMeter_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                marker.Y = e.Y - difference;
                if (marker.Top < cr.Top)
                    marker.Y = cr.Top;
                if (marker.Bottom > cr.Bottom)
                    marker.Y = cr.Bottom - marker.Height;
                Rating = (int)(((float)(botY - marker.Y - marker.Height / 2)) / (((float)botY - (float)topY) / 100f)) + "";
                text = Rating == "100" ? textToDisplay : Rating + " %";
                this.Refresh();
                Thread.Sleep(5);
            }
        }

        private void ManualBeerMeter_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mousePressed = false;
        }
    }
}

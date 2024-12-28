using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watch
{
    public partial class Form1 : Form
    {
        private Timer timer;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Часы на Windows Forms";
            this.Width = 400;
            this.Height = 400;
            this.DoubleBuffered = true; 

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e) => this.Invalidate(); 
            timer.Start();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawClock(e.Graphics);
        }

        private void DrawClock(Graphics g)
        {
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;
            int radius = Math.Min(centerX, centerY) - 20;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillEllipse(Brushes.White, centerX - radius, centerY - radius, radius * 2, radius * 2);
            g.DrawEllipse(Pens.Black, centerX - radius, centerY - radius, radius * 2, radius * 2);

            DateTime now = DateTime.Now;
            float hour = now.Hour % 12 + now.Minute / 60f;
            float minute = now.Minute + now.Second / 60f;
            float second = now.Second;

            for (int i = 0; i < 12; i++)
            {
                double angle = Math.PI / 6 * i;
                int x1 = (int)(centerX + (radius - 10) * Math.Cos(angle));
                int y1 = (int)(centerY - (radius - 10) * Math.Sin(angle));
                int x2 = (int)(centerX + radius * Math.Cos(angle));
                int y2 = (int)(centerY - radius * Math.Sin(angle));
                g.DrawLine(Pens.Black, x1, y1, x2, y2);
            }

            DrawHand(g, centerX, centerY, hour * 30 - 90, radius * 0.5f, 6, Brushes.Black); // Часовая
            DrawHand(g, centerX, centerY, minute * 6 - 90, radius * 0.7f, 4, Brushes.Blue);  // Минутная
            DrawHand(g, centerX, centerY, second * 6 - 90, radius * 0.9f, 2, Brushes.Red);   // Секундная
        }

        private void DrawHand(Graphics g, int centerX, int centerY, double angle, float length, float width, Brush brush)
        {
            double radians = angle * Math.PI / 180;
            int x = (int)(centerX + length * Math.Cos(radians));
            int y = (int)(centerY + length * Math.Sin(radians));

            using (var pen = new Pen(brush, width) { EndCap = System.Drawing.Drawing2D.LineCap.Round })
            {
                g.DrawLine(pen, centerX, centerY, x, y);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

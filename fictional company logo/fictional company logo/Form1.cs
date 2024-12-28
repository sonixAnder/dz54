using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fictional_company_logo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.Text = "Логотип компании";
            this.Width = 600;
            this.Height = 400;
            this.BackColor = Color.White;
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Brush circleBrush = new SolidBrush(Color.DarkViolet))
            {
                g.FillEllipse(circleBrush, 200, 100, 200, 200); // x, y, width, height
            }

            using (Brush rectBrush = new SolidBrush(Color.Black))
            {
                g.FillRectangle(rectBrush, 250, 150, 100, 100);
            }

            using (Font font = new Font("TunnelFront", 25, FontStyle.Bold))
            {
                string text = "AnderStudio";
                Rectangle textBounds = new Rectangle(200, 100, 200, 200);
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                GraphicsPath path = new GraphicsPath();
                path.AddString(text, font.FontFamily, (int)font.Style, g.DpiY * font.Size / 72, textBounds, sf);

                using (Pen outlinePen = new Pen(Color.Black, 3) { LineJoin = LineJoin.Round })
                {
                    g.DrawPath(outlinePen, path);
                }

                using (Brush textBrush = new SolidBrush(Color.DarkViolet))
                {
                    g.FillPath(textBrush, path);
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

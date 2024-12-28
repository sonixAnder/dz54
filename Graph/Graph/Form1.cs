using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graph
{
    public partial class Form1 : Form
    {
        private Func<double, double> FunctionToPlot = x => Math.Sin(x);
        public Form1()
        {
            InitializeComponent();
            this.Text = "График функции";
            this.Size = new Size(800, 600);
            this.Paint += new PaintEventHandler(DrawGraph);
        }
        private void DrawGraph(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            float scale = 50;


            Pen axisPen = new Pen(Color.Black, 1);
            g.DrawLine(axisPen, 0, centerY, this.ClientSize.Width, centerY); //X
            g.DrawLine(axisPen, centerX, 0, centerX, this.ClientSize.Height); //Y

            Pen graphPen = new Pen(Color.Blue, 2);
            for (float x = -centerX / scale; x < centerX / scale; x += 0.01f)
            {
                float y1 = (float)-FunctionToPlot(x) * scale;
                float y2 = (float)-FunctionToPlot(x + 0.01) * scale;

                float screenX1 = centerX + x * scale;
                float screenY1 = centerY + y1;

                float screenX2 = centerX + (x + 0.01f) * scale;
                float screenY2 = centerY + y2;

                g.DrawLine(graphPen, screenX1, screenY1, screenX2, screenY2);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

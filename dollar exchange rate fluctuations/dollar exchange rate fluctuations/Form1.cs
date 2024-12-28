using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dollar_exchange_rate_fluctuations
{
    public partial class Form1 : Form
    {
        private List<float> dollarRates;
        public Form1()
        {
            InitializeComponent();
            this.Text = "График курса доллара";
            this.Size = new Size(800, 600);
            this.dollarRates = GenerateDollarRates();
            this.Paint += Form1_Paint;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawGraph(g, dollarRates);
        }

        private List<float> GenerateDollarRates()
        {
            Random random = new Random();
            List<float> rates = new List<float>();
            float baseRate = 75.0f;
            for (int i = 0; i < 12; i++)
            {
                rates.Add(baseRate + (float)(random.NextDouble() * 10 - 5));
            }
            return rates;
        }

        private void DrawGraph(Graphics g, List<float> rates)
        {
            int padding = 50;
            int width = this.ClientSize.Width - 2 * padding;
            int height = this.ClientSize.Height - 2 * padding;

            float maxRate = (float)Math.Ceiling(rates.Max());
            float minRate = (float)Math.Floor(rates.Min());

            float stepX = (float)width / (rates.Count - 1);
            float stepY = height / (maxRate - minRate);

            Pen axisPen = new Pen(Color.Black, 2);
            g.DrawLine(axisPen, padding, padding, padding, height + padding); // Y-ось
            g.DrawLine(axisPen, padding, height + padding, width + padding, height + padding); // X-ось

            Font font = new Font("Arial", 10);
            Brush brush = Brushes.Black;

            for (float rate = minRate; rate <= maxRate; rate += 1)
            {
                float y = height + padding - (rate - minRate) * stepY;
                g.DrawLine(Pens.Gray, padding - 5, y, width + padding, y);
                g.DrawString(rate.ToString("F2"), font, brush, padding - 40, y - 10);
            }

            for (int i = 0; i < rates.Count; i++)
            {
                float x = padding + i * stepX;
                g.DrawLine(Pens.Gray, x, height + padding + 5, x, padding);
                g.DrawString($"Месяц {i + 1}", font, brush, x - 20, height + padding + 10);
            }

            Pen graphPen = new Pen(Color.Blue, 2);
            for (int i = 0; i < rates.Count - 1; i++)
            {
                float x1 = padding + i * stepX;
                float y1 = height + padding - (rates[i] - minRate) * stepY;
                float x2 = padding + (i + 1) * stepX;
                float y2 = height + padding - (rates[i + 1] - minRate) * stepY;
                g.DrawLine(graphPen, x1, y1, x2, y2);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

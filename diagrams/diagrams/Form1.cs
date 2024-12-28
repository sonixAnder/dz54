using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace diagrams
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Генератор диаграмм";
            this.Size = new Size(800, 600);

            comboChartType = new ComboBox
            {
                Location = new Point(20, 20),
                Width = 200
            };
            comboChartType.Items.AddRange(new string[] { "Круговая", "Линейная", "Гистограмма" });
            comboChartType.SelectedIndex = 0;

            btnDraw = new Button
            {
                Text = "Построить диаграмму",
                Location = new Point(240, 20),
                Width = 150
            };
            btnDraw.Click += BtnDraw_Click;

            pictureBox = new PictureBox
            {
                Location = new Point(20, 60),
                Size = new Size(700, 400),
                BorderStyle = BorderStyle.FixedSingle
            };


            dataGridView = new DataGridView
            {
                Location = new Point(20, 480),
                Size = new Size(700, 100),
                ColumnCount = 2,
                Columns = { [0] = { HeaderText = "Категория" }, [1] = { HeaderText = "Значение" } },
                AllowUserToAddRows = true
            };

            this.Controls.Add(comboChartType);
            this.Controls.Add(btnDraw);
            this.Controls.Add(pictureBox);
            this.Controls.Add(dataGridView);
        }

        private void BtnDraw_Click(object sender, EventArgs e)
        {
            var data = GetChartData();

            if (data.Values.Count == 0)
            {
                MessageBox.Show("Введите хотя бы одно значение!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string chartType = comboChartType.SelectedItem.ToString();

            Bitmap bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                if (chartType == "Круговая")
                    DrawPieChart(g, new Rectangle(50, 50, 300, 300), data.Values, GetRandomColors(data.Values.Count));
                else if (chartType == "Линейная")
                    DrawLineChart(g, data.Values);
                else if (chartType == "Гистограмма")
                    DrawBarChart(g, data);
            }

            pictureBox.Image = bmp;
        }

        private (List<string> Categories, List<float> Values) GetChartData()
        {
            var categories = new List<string>();
            var values = new List<float>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    string category = row.Cells[0].Value.ToString();
                    if (float.TryParse(row.Cells[1].Value.ToString(), out float value))
                    {
                        categories.Add(category);
                        values.Add(value);
                    }
                }
            }

            return (categories, values);
        }

        private void DrawPieChart(Graphics g, Rectangle rect, List<float> values, Color[] colors)
        {
            float total = values.Sum();
            float startAngle = 0;

            for (int i = 0; i < values.Count; i++)
            {
                float sweepAngle = (values[i] / total) * 360;
                using (Brush brush = new SolidBrush(colors[i]))
                {
                    g.FillPie(brush, rect, startAngle, sweepAngle);
                }
                startAngle += sweepAngle;
            }
        }
        private void DrawLineChart(Graphics g, List<float> values)
        {
            if (values.Count < 2) return;

            int width = pictureBox.Width - 100;
            int height = pictureBox.Height - 100;
            int step = width / (values.Count - 1);

            Point[] points = values.Select((v, i) => new Point(50 + i * step, height - (int)(v / values.Max() * height))).ToArray();

            using (Pen pen = new Pen(Color.Blue, 2))
            {
                g.DrawLines(pen, points);
            }
        }

        private void DrawBarChart(Graphics g, (List<string> Categories, List<float> Values) data)
        {
            int width = pictureBox.Width - 100;
            int height = pictureBox.Height - 100;
            int barWidth = width / data.Values.Count - 10;

            for (int i = 0; i < data.Values.Count; i++)
            {
                int barHeight = (int)(data.Values[i] / data.Values.Max() * height);
                using (Brush brush = new SolidBrush(Color.FromArgb(100 + i * 20, 150, 200)))
                {
                    g.FillRectangle(brush, 50 + i * (barWidth + 10), height - barHeight, barWidth, barHeight);
                }

                using (Brush textBrush = new SolidBrush(Color.Black))
                {
                    g.DrawString(data.Categories[i], new Font("Arial", 8), textBrush, 50 + i * (barWidth + 10), height + 10);
                }
            }
        }

        private Color[] GetRandomColors(int count)
        {
            Random rnd = new Random();
            return Enumerable.Range(0, count).Select(_ => Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))).ToArray();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

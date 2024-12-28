using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drawing_various_geometric_shapes
{
    public partial class Form1 : Form
    {
        private enum ShapeType { Rectangle, Ellipse, Line }
        private ShapeType currentShape = ShapeType.Rectangle;
        private Point startPoint;
        private Point endPoint;
        private bool isDrawing = false;
        private Bitmap canvas;

        public Form1()
        {
            InitializeComponent();
            canvas = new Bitmap(drawingPanel.Width, drawingPanel.Height);

            drawingPanel.Paint += DrawingPanel_Paint;
            drawingPanel.MouseDown += DrawingPanel_MouseDown;
            drawingPanel.MouseMove += DrawingPanel_MouseMove;
            drawingPanel.MouseUp += DrawingPanel_MouseUp;
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(canvas, 0, 0);
            if (isDrawing)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    switch (currentShape)
                    {
                        case ShapeType.Rectangle:
                            e.Graphics.DrawRectangle(pen, GetRectangle(startPoint, endPoint));
                            break;
                        case ShapeType.Ellipse:
                            e.Graphics.DrawEllipse(pen, GetRectangle(startPoint, endPoint));
                            break;
                        case ShapeType.Line:
                            e.Graphics.DrawLine(pen, startPoint, endPoint);
                            break;
                    }
                }
            }
        }

        private void DrawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            startPoint = e.Location;
        }

        private void DrawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                endPoint = e.Location;
                drawingPanel.Invalidate();
            }
        }

        private void DrawingPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
            endPoint = e.Location;

            using (Graphics g = Graphics.FromImage(canvas))
            using (Pen pen = new Pen(Color.Black, 2))
            {
                switch (currentShape)
                {
                    case ShapeType.Rectangle:
                        g.DrawRectangle(pen, GetRectangle(startPoint, endPoint));
                        break;
                    case ShapeType.Ellipse:
                        g.DrawEllipse(pen, GetRectangle(startPoint, endPoint));
                        break;
                    case ShapeType.Line:
                        g.DrawLine(pen, startPoint, endPoint);
                        break;
                }
            }
            drawingPanel.Invalidate();
        }

        private Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y));
        }

        private void ToolStripButtonRectangle_Click(object sender, EventArgs e)
        {
            currentShape = ShapeType.Rectangle;
        }

        private void ToolStripButtonEllipse_Click(object sender, EventArgs e)
        {
            currentShape = ShapeType.Ellipse;
        }

        private void ToolStripButtonLine_Click(object sender, EventArgs e)
        {
            currentShape = ShapeType.Line;
        }

        private void ToolStripButtonSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImageFormat format = ImageFormat.Png;
                    if (saveFileDialog.FilterIndex == 2) format = ImageFormat.Jpeg;
                    else if (saveFileDialog.FilterIndex == 3) format = ImageFormat.Bmp;

                    canvas.Save(saveFileDialog.FileName, format);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

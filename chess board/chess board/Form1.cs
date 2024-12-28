using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_board
{
    public partial class Form1 : Form
    {
        private const int CellSize = 50;
        private const int BoardSize = 8;
        private Dictionary<Point, string> figures = new Dictionary<Point, string>();
        private ContextMenuStrip contextMenu;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Шахматная доска";
            this.Size = new Size(BoardSize * CellSize + 20, BoardSize * CellSize + 40);
            this.Paint += ChessBoardForm_Paint;
            this.MouseDown += ChessBoardForm_MouseDown;
            InitializeFigures();
            InitializeContextMenu();
        }

        private void InitializeFigures()
        {
            figures[new Point(0, 0)] = "Ладья";
            figures[new Point(7, 0)] = "Ладья";
            figures[new Point(1, 0)] = "Конь";
            figures[new Point(6, 0)] = "Конь";
            figures[new Point(4, 0)] = "Король";
            figures[new Point(3, 0)] = "Ферзь";
            figures[new Point(0, 1)] = "Пешка";
            figures[new Point(1, 1)] = "Пешка";
            figures[new Point(2, 1)] = "Пешка";
            figures[new Point(3, 1)] = "Пешка";
            figures[new Point(4, 1)] = "Пешка";
            figures[new Point(5, 1)] = "Пешка";
            figures[new Point(6, 1)] = "Пешка";
            figures[new Point(7, 1)] = "Пешка";

        }

        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Удалить фигуру", null, (s, e) => RemoveFigure());
            contextMenu.Items.Add("Сменить цвет", null, (s, e) => ChangeFigureColor());
        }
        //доска
        private void ChessBoardForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    Brush brush = (x + y) % 2 == 0 ? Brushes.White : Brushes.Gray;
                    g.FillRectangle(brush, x * CellSize, y * CellSize, CellSize, CellSize);
                }
            }

            //фигуры
            foreach (var figure in figures)
            {
                Point cell = figure.Key;
                string name = figure.Value;

                Brush brush = Brushes.Black;
                Rectangle rect = new Rectangle(cell.X * CellSize, cell.Y * CellSize, CellSize, CellSize);
                g.DrawString(name[0].ToString(), new Font("Arial", 20), brush, rect, new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                });
            }
        }

        private void ChessBoardForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point cell = new Point(e.X / CellSize, e.Y / CellSize);

                if (figures.ContainsKey(cell))
                {
                    contextMenu.Tag = cell;
                    contextMenu.Show(this, e.Location);
                }
            }
        }

        private void RemoveFigure()
        {
            if (contextMenu.Tag is Point cell && figures.ContainsKey(cell))
            {
                figures.Remove(cell);
                this.Invalidate();
            }
        }

        private void ChangeFigureColor()
        {
            MessageBox.Show("Цвет фигуры изменен!", "Информация");
            this.Invalidate();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

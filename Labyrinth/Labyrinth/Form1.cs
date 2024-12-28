using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labyrinth
{
    public partial class Form1 : Form
    {
        private int[,] maze = {
            { 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 1, 0, 1 },
            { 1, 0, 1, 0, 1, 0, 1 },
            { 1, 0, 1, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 3, 0 },
            { 1, 2, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1 },
        };
        private int cellSize = 40; 
        private Point playerPosition = new Point(1, 1); 
        public Form1()
        {
            InitializeComponent();
            this.Text = "Лабиринт";
            this.ClientSize = new Size(maze.GetLength(1) * cellSize, maze.GetLength(0) * cellSize);
            this.DoubleBuffered = true;
            this.KeyDown += MazeForm_KeyDown;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int y = 0; y < maze.GetLength(0); y++)
            {
                for (int x = 0; x < maze.GetLength(1); x++)
                {
                    if (maze[y, x] == 1) // Стена
                        g.FillRectangle(Brushes.Black, x * cellSize, y * cellSize, cellSize, cellSize);
                    else if (maze[y, x] == 2 || maze[y, x] == 3) // Выходы
                        g.FillRectangle(Brushes.Green, x * cellSize, y * cellSize, cellSize, cellSize);
                    else // Проходимый путь
                        g.FillRectangle(Brushes.White, x * cellSize, y * cellSize, cellSize, cellSize);

                    g.DrawRectangle(Pens.Gray, x * cellSize, y * cellSize, cellSize, cellSize);
                }
            }

            // Рисуем игрока
            g.FillEllipse(Brushes.Red, playerPosition.X * cellSize + 5, playerPosition.Y * cellSize + 5, cellSize - 10, cellSize - 10);
        }

        private void MazeForm_KeyDown(object sender, KeyEventArgs e)
        {
            Point newPosition = playerPosition;

            // Обработка управления
            switch (e.KeyCode)
            {
                case Keys.W: newPosition.Y--; break;
                case Keys.S: newPosition.Y++; break;
                case Keys.A: newPosition.X--; break;
                case Keys.D: newPosition.X++; break;
            }

            if (newPosition.X >= 0 && newPosition.X < maze.GetLength(1) &&
                newPosition.Y >= 0 && newPosition.Y < maze.GetLength(0) &&
                maze[newPosition.Y, newPosition.X] != 1)
            {
                playerPosition = newPosition;

                if (maze[playerPosition.Y, playerPosition.X] == 2 || maze[playerPosition.Y, playerPosition.X] == 3)
                {
                    MessageBox.Show("Вы нашли выход!");
                    Application.Exit();
                }
            }

            Invalidate();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

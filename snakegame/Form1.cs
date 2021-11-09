using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snakegame
{
    public partial class Form1 : Form
    {
        SolidBrush white_brush;
        SolidBrush black_brush;
        SolidBrush green_brush;
        Pen gray_pen;
        Point[] snake;
        Point apple;
        Random random;
        int length = 1;
        int width, height;
        public Form1()
        {
            random = new Random();
            snake = new Point[10000];
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            width = pictureBox1.Width / 10;
            height = pictureBox1.Height / 10;
            snake[0].X = width / 2;
            snake[0].Y = height / 2;
            white_brush = new SolidBrush(Color.White);
            black_brush = new SolidBrush(Color.Black);
            green_brush = new SolidBrush(Color.Green);
            gray_pen = new Pen(Color.LightGray);
            apple.X = random.Next(0, width - 1);
            apple.Y = random.Next(0, height - 1);


        }
        string direction = "up";
        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);
            graphics.FillRectangle(white_brush, 0, 0, pictureBox1.Width, pictureBox1.Height);
            for (int i = 1; i < width; i++)
            {
                graphics.DrawLine(gray_pen,i*10,0,i*10,height*10);
            }
            for (int i = 1; i < height; i++)
            {
                graphics.DrawLine(gray_pen,0,i * 10,width*10,i*10);
            }


            if (length > 4)
            for (int i = 1; i < length; i++)
                for (int j = i+1; j < length; j++)
                {
                    if (snake[i].X == snake[j].X && snake[i].Y == snake[j].Y)
                        length = 3;
                }
            for (int i = 0; i < length; i++)
            {
                if (snake[i].X < 0) snake[i].X += width;
                if (snake[i].X > width) snake[i].X -= width;
                if (snake[i].Y < 0) snake[i].Y += height;
                if (snake[i].Y > height) snake[i].Y -= height;

                graphics.FillEllipse(black_brush, snake[i].X * 10, snake[i].Y * 10, 10, 10);
                if(apple.X == snake[i].X && apple.Y == snake[i].Y)
                {
                    apple.X = random.Next(0, width - 1);
                    apple.Y = random.Next(0, height - 1);
                    length++; 
                }
            }
            graphics.FillEllipse(green_brush, apple.X * 10, apple.Y * 10, 10, 10);
            if (direction == "up") snake[0].Y -= 1;
            if (direction == "down") snake[0].Y += 1;
            if (direction == "left") snake[0].X -= 1;
            if (direction == "right") snake[0].X += 1;

            if (length > 10000 - 3)
            {
                length = 10000 - 3;
            }
            for (int i = length; i >= 0; i--)
            {
                snake[i + 1].X = snake[i].X;
                snake[i + 1].Y = snake[i].Y;

            }
            if (length < 4) length++;
            pictureBox1.Invalidate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           // if (timer1.Interval < 11) timer1.Interval = 11;
           //  timer1.Interval -= 10;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                direction = "up";
            }
            if (e.KeyCode == Keys.Down)
            {
                direction = "down";
            }
            if (e.KeyCode == Keys.Left)
            {
                direction = "left";
            }
            if (e.KeyCode == Keys.Right)
            {
                direction = "right";
            }


        }
    }
}

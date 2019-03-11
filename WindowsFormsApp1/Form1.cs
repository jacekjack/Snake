using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();                // creating snake and new food
        private Circle food = new Circle();

        public Form1()
        {
            InitializeComponent();

            new Settings();             // initializing game

            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += updateScreen;
            gameTimer.Start();

            startGame();

        }

        private void startGame()            //starting or restarting the game
        {

            label3.Visible = false;
            new Settings();
            Snake.Clear();
            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head);
            label2.Text = Settings.Score.ToString();
            generateFood();
        }

        private void generateFood()     //generating food on canvas randomly
        {
            int maxXpos = pbCanvas.Size.Width / Settings.Width;
            int maxYpos = pbCanvas.Size.Height / Settings.Height;
            Random rnd = new Random();
            food = new Circle { X = rnd.Next(0, maxXpos), Y = rnd.Next(0, maxYpos) };
        }

        private void eat()          // when the snakes eat the food
        {
            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };

            Snake.Add(body);
            Settings.Score += Settings.Points;
            label2.Text = Settings.Score.ToString();
            generateFood();

        }

        private void die()      // setting game over 
        {
            Settings.GameOver = true;

        }
        private void updateScreen(object sender, EventArgs e)           // update screen by timer
        {
            if (Settings.GameOver == true)      //restarting the game after die
            {
                if (Input.KeyPress(Keys.Enter))
                {
                    startGame();
                }
            }
            else
            //setting the direction of snake
            {
                if (Input.KeyPress(Keys.Right) && Settings.direction != Diretions.Left)
                {
                    Settings.direction = Diretions.Right;
                }
                else if (Input.KeyPress(Keys.Left) && Settings.direction != Diretions.Right)
                {
                    Settings.direction = Diretions.Left;
                }
                else if (Input.KeyPress(Keys.Up) && Settings.direction != Diretions.Down)
                {
                    Settings.direction = Diretions.Up;
                }
                else if (Input.KeyPress(Keys.Down) && Settings.direction != Diretions.Up)
                {
                    Settings.direction = Diretions.Down;
                }

                movePlayer(); // moving the snake
            }
            pbCanvas.Invalidate();   //redrawing the canvas
        }

        private void movePlayer()       //method to move the player
        {
            for (int i = Snake.Count - 1; i >= 0; i--)      //moving every circle in snake body
            {

                if (i==0)
                {
                    switch (Settings.direction)     //choosing the direction of moving
                    {
                        case Diretions.Right:
                            Snake[i].X++;
                            break;
                        case Diretions.Left:
                            Snake[i].X--;
                            break;
                        case Diretions.Up:
                            Snake[i].Y--;
                            break;
                        case Diretions.Down:
                            Snake[i].Y++;
                            break;
                    }

                    int maxXpos = pbCanvas.Size.Width / Settings.Width;
                    int maxYpos = pbCanvas.Size.Height / Settings.Height;
                    //checking if snake is hitting a border
                    if (
                        Snake[i].X < 0 || Snake[i].Y < 0 || 
                        Snake[i].X > maxXpos || Snake[i].Y > maxYpos
                        )
                    {

                        die();
                    }
                    // checking if snake hiting itself
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            die();
                        }

                    }
                    //checking if snake hit a food
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        eat();
                    }
                }
                else
                {

                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, true);
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, false);
        }

        private void updateGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (Settings.GameOver == false)
            {
                Brush snakeColour;
                //setting the snake, head and the body
                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                    {
                        snakeColour = Brushes.Black;
                    }
                    else
                    {
                        snakeColour = Brushes.Green;
                    }
                    //creating a circle parts of snake
                    canvas.FillEllipse(snakeColour,
                                        new Rectangle(
                                            Snake[i].X * Settings.Width,
                                            Snake[i].Y * Settings.Height,
                                            Settings.Width, Settings.Height
                                            ));
                        
                    canvas.FillEllipse(Brushes.Red,
                                        new Rectangle(
                                            food.X * Settings.Width,
                                            food.Y * Settings.Height,
                                            Settings.Width, Settings.Height
                                            ));
                }
            }
            else
            {       //setting the message game over

                string gameOver = "Game Over \n" + "Final Score is " + Settings.Score + "\n Press Enter to Restart \n";
                label3.Text = gameOver;
                label3.Visible = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

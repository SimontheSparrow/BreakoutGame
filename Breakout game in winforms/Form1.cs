using System;
using System.Drawing;
using System.Windows.Forms;

namespace Breakout_game_in_winforms
{
    public partial class Form1 : Form
    {
        bool goRight, goLeft, gameIsOver;
        int score, playerSpeed, ballX, ballY;
        Random rnd = new Random();
        PictureBox[] pictureBoxArray;
        public Form1()
        {
            InitializeComponent();
            placeBlocks();
                }

        private void setupGame()
        {
            gameIsOver = false;
            ball.Left = 401;
            ball.Top = 234;
            score = 0;
            ballX = 5;
            ballY = 5;
            playerSpeed = 12;
            label1.Text = "Score: " + score;
            gameTimer.Start();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag== "blocks")
                {
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                }
            }
        }
        private void gameOver()
        {
            gameIsOver = true;
            gameTimer.Stop();
        }
        private void placeBlocks()
        {
            pictureBoxArray = new PictureBox[15];
            int a = 0, top = 50, left = 100;
            for (int i = 0; i < pictureBoxArray.Length; i++)
            {
                pictureBoxArray[i] = new PictureBox();
                pictureBoxArray[i].Tag = "blocks";
                pictureBoxArray[i].Width = 100;
                pictureBoxArray[i].Height = 32;
                pictureBoxArray[i].BackColor= Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                
                if (a==5 )
                {
                    top += 50;
                    left = 100;
                    a = 0;
                }
                if (a < 5)
                {
                    pictureBoxArray[i].Left = left;
                    pictureBoxArray[i].Top = top;
                    this.Controls.Add(pictureBoxArray[i]);
                    left += 130;
                    a++;
                }
            }
            setupGame();
        }
        private void removeBlocks()
        {
            foreach(PictureBox x in pictureBoxArray)
            {
                this.Controls.Remove(x);
            }
        }

        private void timerEvent(object sender, EventArgs e)
        {
            label1.Text = "Score: " + score;
            if (goLeft== true && player.Left >0)
            {
                player.Left -= playerSpeed;
            }
            if (goRight ==true && player.Left< 692)
            {
                player.Left += playerSpeed;
            }

            ball.Left += ballX;
            ball.Top += ballY;
            if (ball.Left <0 || ball.Left > 722)
            {
                ballX = -ballX;
            }
            if (ball.Top< 0)
            {
                ballY = -ballY;
            }
            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                ballY = rnd.Next(5, 9) * (-1);
                if (ballX < 0)
                {
                    ballX = rnd.Next(5, 10) * (-1);
                }
                else
                {
                    ballX = rnd.Next(5, 10);
                }
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;
                        this.Controls.Remove(x);
                        ballY = -ballY;
                    }
                }
            }
            if (score ==15 || ball.Top > 400)
            {

                gameOver();
                
            }
            
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
           
        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode== Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Enter && gameIsOver ==true)
            {
                removeBlocks();
                placeBlocks();
            }
        }
    }
}

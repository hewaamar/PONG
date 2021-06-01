using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PONG
{
    public partial class Form1 : Form
    {

        // racquet ball
        Rectangle player1 = new Rectangle(100, 100, 10, 60);
        Rectangle player2 = new Rectangle(100, 200, 10, 60);
        Rectangle ball = new Rectangle(295, 195, 10, 10);

        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 4;
        int ballXSpeed = 8;
        int ballYSpeed = 8;

        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        
        int playerTurn = 1;

        bool aDown = false;
        bool dDown = false;
        bool leftDown = false;
        bool rightDown = false;

        Pen playerTurnPen = new Pen(Color.White, 2);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;

            //move players
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;

            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }

            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }



            if (aDown == true && player1.X > 0)
            {
                player1.X -= playerSpeed;
            }
            if (dDown == true && player1.X < 580)
            {
                player1.X += playerSpeed;
            }
            if (leftDown == true && player1.X > 0)
            {
                player2.X -= playerSpeed;
            }
            if (rightDown == true && player1.X < 580)
            {
                player2.X += playerSpeed;
            }


            //ball collision with top and bottom walls
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed;
            }

            //ball collision with player
            if (player1.IntersectsWith(ball) && ball.X < player1.X && playerTurn == 1)
            {
                ballXSpeed *= -1;
                ball.X = player1.X + ball.Width;
                playerTurn = 2;
            }
            else if (player2.IntersectsWith(ball) && ball.X < player1.X && playerTurn == 2)
            {
                ballXSpeed *= -1;
                ball.X = player2.X - ball.Width;
                playerTurn = 2;
            }

            //check for point scored
            if (ball.X > 580)
            {
                ballXSpeed *= -1;
            }
            // check for point scored 
            if (ball.X < 0 && playerTurn == 1)
            {
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";
            }
           if (ball.X < 0 && playerTurn == 2)
            {
                player1Score++;
                p2ScoreLabel.Text = $"{player1Score}";
            }

            //check for game over
            if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!!";
            }
            else if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!!";
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);

            if (playerTurn == 1)
            {   
                g.DrawRectangle(playerTurnPen, player1);
            }
            else if (playerTurn == 2)
            {   
                g.DrawRectangle(playerTurnPen, player2);    
            }
        }
    }
}

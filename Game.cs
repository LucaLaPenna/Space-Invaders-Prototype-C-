using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace SpacedInvadersApp
{
    class Game
    {
        private Defender defender;
        private Image buffer;
        private Graphics bufferGraphics;
        private Graphics displayGraphics;
        private Form form;
        private Font font = new Font("Impact", 14);
        private Font largefont = new Font("Impact", 26);
        private Brush fontBrush = Brushes.White;
        private double renderElapsed = 0d;
        private AlienGroup aliens;
        private Bullet bullet;
        private Shooter shooter;
        private Missile missile;        
        internal void Initialize(Form mainForm)
        {
            this.form = mainForm;
            buffer = new Bitmap(mainForm.Width, mainForm.Height);
            bufferGraphics = Graphics.FromImage(buffer);
            displayGraphics = mainForm.CreateGraphics();
        }
        private void step(double elapsed)
        {
            defender.Step(elapsed);
            aliens.Step(elapsed);
            if (Global.bulletfiring)
            {
                bullet.Step(elapsed);
            }
            if (Global.missilefiring)
            {
                missile.Step(elapsed);
            }
        }
        private void render() //double elapsed
        {
            bufferGraphics.Clear(Color.Black);
            if (!Global.GameOver)
            {
                defender.Render(bufferGraphics);
                aliens.Render(bufferGraphics);
                shooter.Render(bufferGraphics);
                    if (Global.bulletfiring)
                {
                    bullet.Render(bufferGraphics);
                }
                if (Global.missilefiring)
                {
                    missile.Render(bufferGraphics);
                }
            }
            else
            {                
                // Show game over message in the center of the screen
                bufferGraphics.DrawString("Game Over", largefont, fontBrush, 310, 290);
                bufferGraphics.DrawString("Press F5 to start a new game", font, fontBrush, 278, 350);
            }
            // display banner
            if (Global.Score >= 395)
            {
                Global.win = true;
                bufferGraphics.Clear(Color.Black);
                bufferGraphics.DrawString("You Win!!", largefont, fontBrush, 310, 290);
                bufferGraphics.DrawString("Press M to go to the next level", font, fontBrush, 278, 350);
            }
            if (Global.PlayersRemaining <= 0)
            {
                Global.GameOver = true;
                bufferGraphics.Clear(Color.Black);
                bufferGraphics.DrawString("You Lose!! Game Over", largefont, fontBrush, 230, 290);
                bufferGraphics.DrawString("Press F5 to start a new game", font, fontBrush, 278, 350);
            }

            bufferGraphics.DrawString("Score: " + Global.Score, font, fontBrush, 10, 10);
            bufferGraphics.DrawString("Level: " + Global.CurrentLevel, font, fontBrush, 630, 10);
            bufferGraphics.DrawString("Lives: " + Global.PlayersRemaining, font, fontBrush, 710, 10);

            //Blit the off-screen buffer on to the display
            displayGraphics.DrawImage(buffer, 0, 0);            
        }
        internal void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
            //if game not active
            if (Global.GameOver) 
            {
                if (e.KeyCode == Keys.F5)
                startNewGame();
            }
            if (Global.win)
            {
                if (e.KeyCode == Keys.M)
                    GoToNextLevel();
            }
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Global.DefenderDirection = Directions.Left;
                    break;
                case Keys.Right:
                    Global.DefenderDirection = Directions.Right;
                    break;
                case Keys.Down:
                    Global.DefenderDirection = Directions.None;
                    break;
                case Keys.Space:
                    {
                        if (!Global.bulletfiring)
                        {
                            Global.bulletfiring = true;
                            bullet = new Bullet(defender.GetBulletStartLocation());

                        }

                        if (!Global.missilefiring)
                        {
                            Global.missilefiring = true;
                            missile = new Missile(shooter.GetMissileStartLocation());
                        }
                         break;
                    }
            }
        }

        private void GoToNextLevel()
        {
            Global.win = false;
            Global.GameOver = false;
            Global.Score = 0;
            Global.PlayersRemaining = Global.PlayersRemaining + 1;
            Global.CurrentLevel = Global.CurrentLevel + 1 ;
            defender = new Defender(new Point(form.ClientSize.Width / 2 - 20, form.ClientSize.Height - 50));
            aliens = new AlienGroup(Global.AliensRow, Global.AliensCol);
            shooter = new Shooter(new Point(form.ClientSize.Width / 2 - 20, form.ClientSize.Height - 600));
        }
        private void startNewGame()
        {
            Global.GameOver = false;
            Global.Score = 0;
            Global.PlayersRemaining = 3;
            defender = new Defender(new Point(form.ClientSize.Width / 2 - 20, form.ClientSize.Height - 50));
            aliens = new AlienGroup(Global.AliensRow, Global.AliensCol);
            shooter = new Shooter(new Point(form.ClientSize.Width / 2 - 20, form.ClientSize.Height - 600));
        }
        internal void GameLoop()
        {
            DateTime start;
            double elapsed = 0d;
            while (form.Created)
            {
                start = DateTime.Now;
                render();
                Application.DoEvents();
                if (!Global.GameOver)
                {
                    step(elapsed);
                    detectCollision();                    
                }
                elapsed = (DateTime.Now - start).TotalMilliseconds;               
            }          
        }
        private void detectCollision()
        {
            if (Global.bulletfiring)
                aliens.CheckForCollision(bullet);
            if (Global.missilefiring)
            {
                CheckForMissileCollision(missile);
            }
        }
        internal void CheckForMissileCollision(Missile missile)
        {
            if (missile.Bounds.IntersectsWith(defender.Bounds))
            {
                HitByMissile();
                return;
            }
        }
        internal void HitByMissile()
        {
            Global.PlayersRemaining -= 1;
            Global.missilefiring = false;
        }
    }
}

     


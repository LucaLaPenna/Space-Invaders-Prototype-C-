using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SpacedInvadersApp
{
    class Defender : TokenGeneral
    {
        bool dead = false;
        internal bool Dead { get { return dead; } }
        internal Defender(PointF startLocation)
        {
            location = startLocation;
            sprite = Bitmap.FromFile("C:/Users/lucap/ship.bmp");
            bounds = new Rectangle((int)location.X,(int)location.Y, sprite.Size.Width, sprite.Size.Height);
            imgAttr.SetColorKey(Color.Black, Color.Black);
        }
        internal override void Render(Graphics g)
        {
            g.DrawImage(sprite, bounds);
        }
        internal override void Step(double elapsed)
        {
            switch (Global.DefenderDirection)
            {
                case Directions.Left:
                    location.X -= (float)(elapsed * Global.DefenderSpeed);
                    if (location.X < 10)
                    {
                        location.X = 10;
                        Global.DefenderDirection = Directions.None;
                    }
                    bounds.X = (int)location.X;
                    break;
                case Directions.Right:
                    location.X += (float)(elapsed * Global.DefenderSpeed);
                    if (location.X + Global.DefenderSize.Width > Global.FormSize.Width)
                    {
                        location.X = Global.FormSize.Width - Global.DefenderSize.Width;

                        Global.DefenderDirection = Directions.None;
                    }
                    bounds.X = (int)location.X;
                    break;
            }
        }
        internal PointF GetBulletStartLocation()
        {
            return new PointF(location.X + (sprite.Width / 2) - (Global.BulletSize.Width / 2), location.Y);
        }  
    }
}


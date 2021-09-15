using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SpacedInvadersApp
{    
        class Missile : TokenGeneral
        {
            internal Missile(PointF startLocation)
            {
                location = startLocation;
                sprite = Bitmap.FromFile("C:/Users/lucap/Alien/Bullet.bmp");
                bounds = new Rectangle((int)location.X, (int)location.Y, sprite.Size.Width, sprite.Size.Height);
            }
            internal override void Render(Graphics g)
            {
                g.DrawImage(sprite, bounds);
            }
            internal override void Step(double elapsed)
            {
                location.Y += (float)(elapsed * Global.missileSpeed);
                bounds.Y = (int)location.Y;
                Global.missilefiring = (location.Y <= 600);
            }
    }    
}

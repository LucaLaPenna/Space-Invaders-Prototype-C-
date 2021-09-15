using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SpacedInvadersApp
{
    class Shooter : TokenGeneral
    {
		internal Shooter(PointF startLocation)
		{
			location = startLocation;
			sprite = Bitmap.FromFile("C:/Users/lucap/Alien/Alien.bmp");
			bounds = new Rectangle((int)location.X, (int)location.Y, sprite.Size.Width, sprite.Size.Height);

		}
		internal override void Render(Graphics g)
		{
			g.DrawImage(sprite, bounds);
		}
		internal override void Step(double elapsed)
        {

        }
		internal PointF GetMissileStartLocation()
		{
			return new PointF(location.X + (sprite.Width / 2) - (Global.MissileSize.Width / 2), location.Y);
		}
	}
}

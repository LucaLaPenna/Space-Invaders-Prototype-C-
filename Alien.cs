using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace SpacedInvadersApp
{
	class Alien : TokenGeneral
	{
		public bool dead = false;
		internal Alien(PointF startLocation)
		{
			location = startLocation;
			sprite = Bitmap.FromFile("C:/Users/lucap/Alien/Alien.bmp");
			bounds = new Rectangle((int)location.X, (int)location.Y, sprite.Size.Width, sprite.Size.Height);
		}
		internal override void Step(double elapsed)
		{
			switch (Global.AlienDirection)
			{
				case Directions.Left:
					location.X -= (float)(elapsed * Global.AlienSpeed);
					bounds.X = (int)location.X;
					break;
				case Directions.Right:
					location.X += (float)(elapsed * Global.AlienSpeed);
					bounds.X = (int)location.X;
					break;
				default:
					throw new Exception("Invalid direction for alien!");
			}
		}	
		internal void HitByBullet()
		{
			dead = true;
			Global.Score += 10;
		}
		internal void Hit()
		{
			Global.bulletfiring = false;
		}
		internal override void Render(Graphics g)
		{
			g.DrawImage(sprite, bounds);
		}	
	}
}

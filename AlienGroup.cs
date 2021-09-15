using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SpacedInvadersApp
{
	class AlienGroup
	{
		private Alien[,] aliens;
		private int leftMostAlien, rightMostAlien;
		internal AlienGroup(int cols, int rows)
		{
			aliens = new Alien[cols, rows];
			for (int x = 0; x < cols; x++)
			{
				for (int y = 0; y < rows; y++)
				{
					PointF location = new Point(
						x * (Global.AlienSize.Width + Global.AlienSeparation.Width) + 10,
						y * (Global.AlienSize.Height + Global.AlienSeparation.Height) + 80);
					aliens[x, y] = new Alien(location);
				}
			}
			leftMostAlien = 0;
			rightMostAlien = aliens.GetLength(0);
			Global.AlienDirection = Directions.Right;
		}
		internal void Step(double elapsed)
		{
			foreach (Alien alien in aliens)
				alien.Step(elapsed);
			checkAlienDirection();
		}
		internal void Render(Graphics graphics)
		{
			foreach (Alien alien in aliens)
				if (!alien.dead)
				{
					alien.Render(graphics);
				}
		}
		private void checkAlienDirection()
		{
			if (Global.AlienDirection == Directions.Left)
			{
				for (int y = 0; y < aliens.GetLength(1); y++)
				{
					Alien alien = aliens[leftMostAlien, y];
					if (alien.Location.X <= 10)
					{						
						Global.AlienDirection = Directions.Right;
						break;
					}					
				}
			}			
			else
			{
				for (int y = 0; y < aliens.GetLength(1); y++)
				{
					Alien alien = aliens[rightMostAlien - 1, y];
					if (alien.Location.X + Global.AlienSize.Width >= Global.FormSize.Width - 20)
					{
						Global.AlienDirection = Directions.Left;
						break;
					}
				}
			}
		}

		internal void CheckForCollision(Bullet bullet)
		{
			foreach (Alien alien in aliens)
			{
				if (!alien.dead && bullet.Bounds.IntersectsWith(alien.Bounds))
				{
					alien.HitByBullet();
					bullet.Hit();
					return;
				}              
			}
		}
	}
}

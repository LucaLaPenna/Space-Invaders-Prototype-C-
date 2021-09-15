using System;
using System.Drawing;

namespace SpacedInvadersApp
{
    class Global
    {
        //Layout related constants
        internal static readonly Size FormSize = new Size(800, 600);

        //Stats related
        internal static int Score = 0;
        internal static bool GameOver = true;
        internal static bool LevelFinished = false;
        internal static int CurrentLevel = 1;
        internal static int PlayersRemaining = 3;
        internal static Directions DefenderDirection = Directions.None;
        internal static float DefenderSpeed = 1f / 5f;
        internal static Size DefenderSize = new Size(50, 50);
        internal static Directions AlienDirection = Directions.Right;
        internal static float AlienSpeed = 1f / 20f;
        internal static Size AlienSize = new Size(40, 40);
        internal static readonly Size AlienSeparation = new Size(10, 50);
        internal const int AliensRow = 10;
        internal const int AliensCol = 4;
        internal static readonly Size BulletSize = new Size(5, 20);
        internal static bool bulletfiring = false;
        internal static float bulletSpeed = 1f / 3f;
        internal static readonly Size MissileSize = new Size(5, 20);
        internal static bool missilefiring = false;
        internal static float missileSpeed = 1f / 3f;
        internal static bool dead = false;
        internal static bool win = false;

    }
}

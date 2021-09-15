using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace SpacedInvadersApp
{
    abstract class TokenGeneral
    {
        protected PointF location;
        protected Rectangle bounds;

        protected ImageAttributes imgAttr = new ImageAttributes();
        protected Image sprite;

        internal abstract void Step (double elapsed);
        internal abstract void Render (Graphics g);

        internal PointF Location { get { return location; } }
        internal Rectangle Bounds { get { return bounds; } }
    }
}

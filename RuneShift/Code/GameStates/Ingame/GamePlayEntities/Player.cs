using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace RuneShift
{
    public class Player
    {

        public Player()
        {
        }

        public void update()
        {
        }

        public void Draw(RenderWindow win, View view)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                DebugDraw(win, view);
            }
        }

        private static void DebugDraw(RenderWindow win, View view)
        {
            RectangleShape r = new RectangleShape(Vector2.One);
            r.Origin = (Vector2)r.Size / 2F;
            r.Position = Helper.ScreenToGameCoordinate(win.InternalGetMousePosition(), win, view);
            win.Draw(r);
        }
    }
}

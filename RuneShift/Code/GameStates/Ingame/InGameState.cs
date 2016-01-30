using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace RuneShift
{
    class InGameState : IGameState
    {
        Map Map;
        bool resetView;
        Player Player;

        public InGameState()
        {
            this.Map = new Map();
            resetView = true;

            Player = new Player();
        }

        public GameState update()
        {
            Map.Update();

            return GameState.InGame;
        }

        public void draw(RenderWindow win, View view)
        {
            if (resetView)
            {
                view.Center = Vector2.Zero;
                view.Zoom(100F / view.Size.Y);
                resetView = false;
            }

            Map.Draw(win);

            Player.Draw(win, view);
        }

        public void drawGUI(GUI gui)
        {
        }
    }
}

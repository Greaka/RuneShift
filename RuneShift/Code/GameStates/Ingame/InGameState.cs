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

        public InGameState()
        {
            this.Map = new Map();
            resetView = true;
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
                resetView = false;
            }

            Map.Draw(win);
        }

        public void drawGUI(GUI gui)
        {
        }
    }
}

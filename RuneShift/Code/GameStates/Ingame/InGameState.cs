using System;
using System.Reflection.Emit;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace RuneShift
{
    class InGameState : IGameState
    {
        private Sprite bg;
        Map Map;
        bool resetView;

        public InGameState()
        {
            bg = new Sprite(AssetManager.getTexture(AssetManager.TextureName.InGameBackground));
            bg.Origin = (Vector2) bg.Texture.Size / 2F;
            bg.Scale = Vector2.One * 0.08F;
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
                view.Zoom(100F / view.Size.Y);
                resetView = false;
            }
            win.Draw(bg);
            Map.Draw(win);
        }

        public void drawGUI(GUI gui)
        {
        }
    }
}

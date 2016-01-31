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
        
        bool resetView;

        Map Map;
        EnemyManager EnemyManager;
        ParticleManager ParticleManager;
        Player Player;

        public InGameState()
        {
            bg = new Sprite(AssetManager.getTexture(AssetManager.TextureName.InGameBackground));
            bg.Origin = (Vector2) bg.Texture.Size / 2F;
            bg.Scale = Vector2.One * 0.08F;
            
            resetView = true;

            this.Map = new Map();
            ParticleManager = new ParticleManager(Map);
            Player = new Player(ParticleManager, Map);
            EnemyManager = new EnemyManager(Player);
        }

        public GameState update()
        {
            Map.Update();
            EnemyManager.Update();
            ParticleManager.Update();
            Player.Update();

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
            ParticleManager.Draw(win);
            EnemyManager.Draw(win);
            Player.Draw(win);
        }

        public void drawGUI(GUI gui)
        {
        }
    }
}

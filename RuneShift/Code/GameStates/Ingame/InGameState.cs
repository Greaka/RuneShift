using RuneShift.Code.GameStates.Ingame.GamePlayEntities;
using RuneShift.Code.GameStates.Ingame.GamePlayEntities.Enemies;
using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame
{
    class InGameState : IGameState
    {
        private Sprite bg;
        
        bool resetView;

        Map Map;
        EnemyManager EnemyManager;
        ParticleManager ParticleManager;
        Player Player;
        AlignmentManager AlignmentManager;

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
            AlignmentManager = new AlignmentManager(Player, Map);
        }

        public GameState update()
        {
            Map.Update();
            EnemyManager.Update();
            ParticleManager.Update();
            AlignmentManager.Update();
            Player.Update();

            if (Player.Life <= 0)
                return GameState.MainMenu;
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
            AlignmentManager.Draw(win);
            ParticleManager.Draw(win);
            EnemyManager.Draw(win);
            Player.Draw(win);
        }

        public void drawGUI(GUI gui)
        {
            Player.DrawGUI(gui);
            EnemyManager.DrawGUI(gui);
        }
    }
}

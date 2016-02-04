using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame.GamePlayEntities
{
    class Player
    {
        Text DisplayedInfo;
        RectangleShape LifeBar;

        ParticleController ParticleController;

        public readonly float MaxLife;
        public float Life { get; protected set; }

        public Player(ParticleManager particleManager, Map map)
        {
            DisplayedInfo = new Text("Life: " + MaxLife, new Font("Fonts/calibri.ttf"));
            DisplayedInfo.Position = new Vector2(20, 200);
            LifeBar = new RectangleShape(new Vector2(20, Life));
            LifeBar.Position = new Vector2(20, 250);
            LifeBar.Size = new Vector2(20, 200);

            this.ParticleController = new ParticleController(particleManager, map);
            this.MaxLife = 100F;
            this.Life = MaxLife;
        }

        public void recieveDamage(float damage)
        {
            Life -= damage;
        }

        public void Update()
        {
            ParticleController.Update();
        }

        public void Draw(RenderWindow win)
        {
            ParticleController.Draw(win);
        }

        public void DrawGUI(GUI gui)
        {
            DisplayedInfo.DisplayedString = "Life: " + (int)Life;
            gui.Draw(DisplayedInfo);
            LifeBar.Scale = new Vector2(LifeBar.Scale.X, Life / MaxLife);
            gui.Draw(LifeBar);
        }
    }
}

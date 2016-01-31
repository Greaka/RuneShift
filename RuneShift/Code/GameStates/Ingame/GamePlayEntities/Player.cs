using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame.GamePlayEntities
{
    class Player
    {
        ParticleController ParticleController;

        public readonly float MaxLife;
        public float Life { get; protected set; }

        public Player(ParticleManager particleManager, Map map)
        {
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
            RectangleShape r = new RectangleShape(new Vector2(20, Life));
            r.Position = new Vector2(20, 70);
            gui.Draw(r);
        }
    }
}

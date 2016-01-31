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

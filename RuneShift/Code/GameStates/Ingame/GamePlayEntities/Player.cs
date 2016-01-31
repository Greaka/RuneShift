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
        ParticleController ParticleController;

        public float Life { get; protected set; }

        public Player(ParticleManager particleManager, Map map)
        {
            ParticleController = new ParticleController(particleManager, map);
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
    }
}

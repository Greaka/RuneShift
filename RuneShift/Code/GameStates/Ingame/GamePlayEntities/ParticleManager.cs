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
    public class ParticleManager
    {
        List<ParticleSwarm> ParticleSwarms = new List<ParticleSwarm>();

        public ParticleManager()
        {
            ParticleSwarms.Add(new BoundParticleSwarm(100, Vector2.Zero));
        }

        public void Update()
        {
            foreach (ParticleSwarm particleSwarm in ParticleSwarms)
            {
                particleSwarm.Update();
            }
        }

        public void Draw(RenderWindow win)
        {
            foreach (ParticleSwarm particleSwarm in ParticleSwarms)
            {
                particleSwarm.Draw(win);
            }
        }
    }
}

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
    public class BoundParticleSwarm : ParticleSwarm
    {
        public Vector2 Position { get; protected set; }

        public BoundParticleSwarm(int particleCount, Vector2 position)
            : base(particleCount, position)
        {
            Position = position;
        }

        public override void Update()
        {
            foreach(Particle particle in Particles)
            {
                particle.Update(Position);
            }
        }
    }
}

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
    class BoundParticleSwarm : ParticleSwarm
    {
        public Vector2 Position { get; protected set; }
        public Color Color;

        public BoundParticleSwarm(int particleCount, Vector2 position, Color color)
            : base(particleCount, position)
        {
            Position = position;
            Color = color;
        }

        public override void Update()
        {
            foreach(Particle particle in Particles)
            {
                particle.Update(Position);
                particle.Color = Helper.LerpClamp(particle.Color, Color, 0.3F);
            }
        }
    }
}

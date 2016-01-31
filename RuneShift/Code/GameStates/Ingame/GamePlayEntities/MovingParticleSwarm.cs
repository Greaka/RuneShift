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
    public class MovingParticleSwarm : ParticleSwarm
    {
        protected Vector2 StartPosition;
        protected Vector2 TargetPosition;

        public MovingParticleSwarm(int particleCount, Vector2 startPosition, Vector2 TargetPosition)
            : base(particleCount, startPosition)
        {
            //Position = position;
        }

        public override void Update()
        {
            foreach(Particle particle in Particles)
            {
                particle.Update(TargetPosition, Color.White);
            }
        }
    }
}

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
        protected BoundParticleSwarm TargetSwarm;
        private float t;

        public MovingParticleSwarm(int particleCount, Vector2 startPosition, BoundParticleSwarm TargetSwarm)
            : base(particleCount, startPosition)
        {
            //Position = position;
            t = (TargetSwarm.Position - startPosition).length/2;
        }

        public override void Update()
        {
            foreach(Particle particle in Particles)
            {
                particle.Update(TargetSwarm.Position, Color.White);

                var leftdistance = (TargetSwarm.Position - particle.Position).length;
                if (leftdistance > t)
                    leftdistance /= 2;
                particle.Color = Helper.LerpClamp(particle.Color, Color.White, (1/leftdistance));
            }
        }
    }
}

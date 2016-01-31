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
    class MovingParticleSwarm : ParticleSwarm
    {
        protected BoundParticleSwarm TargetSwarm;
        private float t;

        public MovingParticleSwarm(int particleCount, Vector2 startPosition, BoundParticleSwarm TargetSwarm)
            : base(particleCount, startPosition)
        {
            this.TargetSwarm = TargetSwarm;
            t = (TargetSwarm.Position - startPosition).length/2;
        }

        private void TransferParticles(List<Particle> particles)
        {
            foreach (Particle particle in particles)
                this.Particles.Remove(particle);
            TargetSwarm.AddParticles(particles);
        }

        public override void Update()
        {
            var transfer = new List<Particle>();
            foreach(Particle particle in Particles)
            {
                var color = Color.White;
                var leftdistance = (TargetSwarm.Position - particle.Position).length;
                if (leftdistance < 5F)
                {
                    transfer.Add(particle);
                    continue;
                }
                if (leftdistance > t)
                {
                    leftdistance /= 2;
                    leftdistance = (1 / leftdistance) * 0.2F;
                }
                else
                {
                    color = TargetSwarm.Color;
                    leftdistance = (1 / leftdistance) * 0.1F;
                }
                particle.Update(TargetSwarm.Position);
                particle.Color = Helper.LerpClamp(particle.Color, color, leftdistance);
            }
            TransferParticles(transfer);
        }
    }
}

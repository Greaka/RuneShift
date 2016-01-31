using System.Collections.Generic;
using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame.GamePlayEntities.Swarms
{
    class MovingParticleSwarm : ParticleSwarm
    {
        protected BoundParticleSwarm TargetSwarm;
        private Vector2 startPos;
        private float t;

        public MovingParticleSwarm(int particleCount, Vector2 startPosition, BoundParticleSwarm target)
            : base(particleCount, startPosition)
        {
            startPos = startPosition;
            if (target != null)
                SetTarget(target);
        }

        public void SetTarget(BoundParticleSwarm target)
        {
            this.TargetSwarm = target;
            t = (TargetSwarm.Position - startPos).length / 2;
        }

        private void TransferParticles(List<Particle> particles)
        {
            TargetSwarm.AddParticles(particles);
            foreach (var particle in particles)
            {
                this.Particles.Remove(particle);
            }
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
                    leftdistance = (1 / leftdistance) * (1 / leftdistance) * 4F;
                }
                else
                {
                    color = TargetSwarm.Color;
                    leftdistance = (1 / leftdistance) * 0.02F;
                }
                particle.Update(TargetSwarm.Position, ParticleSpeed);
                particle.Color = Helper.LerpClamp(particle.Color, color, leftdistance);
            }
            TransferParticles(transfer);
        }
    }
}

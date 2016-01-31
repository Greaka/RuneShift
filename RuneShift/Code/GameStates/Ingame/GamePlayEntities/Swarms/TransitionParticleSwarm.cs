using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame.GamePlayEntities.Swarms
{
    class TransitionParticleSwarm : ParticleSwarm
    {
        private BoundParticleSwarm from;
        public TransitionParticleSwarm(BoundParticleSwarm startSwarm) : base(0, startSwarm.Position)
        {
            from = startSwarm;
            ParticleSpeed = 0.1F;
        }

        public MovingParticleSwarm Release(BoundParticleSwarm target = null)
        {
            if (target == null)
                target = from;
            var movingSwarm = new MovingParticleSwarm(0, from.Position, target);
            movingSwarm.AddParticles(RemoveRandomParticles(Particles.Count));
            return movingSwarm;
        }

        public override void Update()
        {
            var maxRotation = (Particles.Count / 200F) * 2 * Helper.PI;

            Vector2 target = from.Position + Vector2.Up.rotate(maxRotation) * 7F;

            foreach (Particle particle in Particles)
            {
                particle.Update(target, ParticleSpeed);

                particle.Color = Helper.LerpClamp(particle.Color, Color.White, 0.2F);
            }
        }
    }
}

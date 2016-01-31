using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RuneShift.Code.Utility;

namespace RuneShift.Code.GameStates.Ingame.GamePlayEntities.Swarms
{
    class TransitionParticleSwarm : ParticleSwarm
    {
        private BoundParticleSwarm from;
        public TransitionParticleSwarm(BoundParticleSwarm startSwarm) : base(0, startSwarm.Position)
        {
            from = startSwarm;
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
            foreach (Particle particle in Particles)
            {
                particle.Update(from.Position);
            }
        }
    }
}

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

        public ParticleManager(Map map)
        {
            ParticleSwarms.Add(new BoundParticleSwarm(1000, Vector2.Zero, Color.Red));
            List<Rune> runes = map.GetAllRunes();
            foreach (Rune rune in runes)
            {
                ParticleSwarms.Add(new RuneBoundParticleSwarm(0, rune));
            }
        }

        public void Update()
        {
            foreach (ParticleSwarm particleSwarm in ParticleSwarms)
            {
                particleSwarm.Update();
            }
        }

        public BoundParticleSwarm GetNearestBoundSwarm(Vector2 position)
        {
            float temp;
            return GetNearestBoundSwarm(position, out temp);
        }

        public BoundParticleSwarm GetNearestBoundSwarm(Vector2 position, out float distance)
        {
            BoundParticleSwarm nearestSwarm = null;
            float minDistance = float.MaxValue;

            foreach (ParticleSwarm particleSwarm in ParticleSwarms)
            {
                BoundParticleSwarm boundSwarm = particleSwarm as BoundParticleSwarm;
                if (boundSwarm != null)
                {
                    float dist = Vector2.distance(boundSwarm.Position, position);
                    if (dist < minDistance)
                    {
                        nearestSwarm = boundSwarm;
                        minDistance = dist;
                    }
                }
            }
            distance = minDistance;
            return nearestSwarm;
        }

        public void TransferParticles(BoundParticleSwarm from, BoundParticleSwarm to, int num)
        {
            List<Particle> transferParticles = from.RemoveRandomParticles(num);
            var transitioner = new MovingParticleSwarm(0, from.Position, to);
            ParticleSwarms.Add(transitioner);
            transitioner.AddParticles(transferParticles);
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

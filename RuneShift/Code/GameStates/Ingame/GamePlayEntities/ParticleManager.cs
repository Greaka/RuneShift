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
            ParticleSwarms.Add(new BoundParticleSwarm(1000, Vector2.Zero));
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

        public void Draw(RenderWindow win)
        {
            foreach (ParticleSwarm particleSwarm in ParticleSwarms)
            {
                particleSwarm.Draw(win);
            }
        }
    }
}

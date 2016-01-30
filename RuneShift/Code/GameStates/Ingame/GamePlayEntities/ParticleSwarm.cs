﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace RuneShift
{
    public abstract class ParticleSwarm
    {
        protected Sprite ParticleSprite;

        protected List<Particle> Particles = new List<Particle>();
        protected float ParticleSpeed;

        public int Count { get { return Particles.Count; } }

        public ParticleSwarm(int particleCount, Vector2 position)
        {
            ParticleSprite = new Sprite(AssetManager.getTexture(AssetManager.TextureName.EnergyParticle));
            ParticleSprite.Origin = ((Vector2)ParticleSprite.Texture.Size) / 2F;
            ParticleSprite.Scale = Vector2.One * 0.02F;

            ParticleSpeed = 0.09F;

            for (int i = 0; i < particleCount; ++i)
            {
                Vector2 posOffset = new Vector2(2.5F * Rand.Value(-1F, 1F), 2.5F * Rand.Value(-1F, 1F));
                Vector2 dir = new Vector2(Rand.Value(-1F, 1F), Rand.Value(-1F, 1F));
                Particles.Add(new Particle(position + posOffset, dir, ParticleSpeed));
            }
        }

        public void AddParticle(Particle particle)
        {
            Particles.Add(particle);
        }

        public void AddParticles(List<Particle> particles)
        {
            Particles.AddRange(particles);
        }

        public List<Particle> RemoveRandomParticles(int count)
        {
            if (count < Count)
                throw new Exception("Wanted to remove " + count + " Particles, but Swarm only contains " + Count);

            List<Particle> result = Particles.GetRange(Particles.Count - count - 1, count);
            Particles.RemoveRange(Particles.Count - count - 1, count);
            return result;
        }

        abstract public void Update();

        public void Draw(RenderWindow win)
        {
            foreach (Particle p in Particles)
            {
                ParticleSprite.Position = p.Position;
                win.Draw(ParticleSprite);
            }
        }
    }
}
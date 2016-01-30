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
            ParticleSprite.Scale = Vector2.One * 0.03F;

            ParticleSpeed = 0.03F;

            for (int i = 0; i < particleCount; ++i)
            {
                Vector2 posOffset = new Vector2(Rand.Value(-1F, 1F), Rand.Value(-1F, 1F));
                Vector2 dir = new Vector2(Rand.Value(-1F, 1F), Rand.Value(-1F, 1F));
                Particles.Add(new Particle(position + posOffset, dir, ParticleSpeed));
            }
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

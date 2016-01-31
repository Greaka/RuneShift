using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame.GamePlayEntities.Swarms
{
    class BoundParticleSwarm : ParticleSwarm
    {
        public Vector2 Position { get; protected set; }
        public Color Color;
        private byte R = 1, B = 1, G = 1; // 2: ++; 1: +-; 0: --;

        public BoundParticleSwarm(int particleCount, Vector2 position, Color color)
            : base(particleCount, position)
        {
            Position = position;
            Color = new Color(color.R, color.G, color.B, 155);
        }

        void FancyRainbowEffect()
        {
            if (Color.R == byte.MaxValue && Color.G < byte.MaxValue && Color.B == byte.MinValue)
            {
                Color = new Color(Color.R, (byte) (Color.G + 1), Color.B, byte.MaxValue / 4);
            }
            if (Color.G == byte.MaxValue && Color.R > byte.MinValue && Color.B == byte.MinValue)
            {
                Color = new Color((byte) (Color.R - 1), Color.G, Color.B, byte.MaxValue / 4);
            }
            if (Color.G == byte.MaxValue && Color.B < byte.MaxValue && Color.R == byte.MinValue)
            {
                Color = new Color(Color.R, Color.G, (byte) (Color.B + 1), byte.MaxValue / 4);
            }
            if (Color.B == byte.MaxValue && Color.G > byte.MinValue && Color.R == byte.MinValue)
            {
                Color = new Color(Color.R, (byte) (Color.G - 1), Color.B, byte.MaxValue / 4);
            }
            if (Color.B == byte.MaxValue && Color.R < byte.MaxValue && Color.G == byte.MinValue)
            {
                Color = new Color((byte) (Color.R + 1), Color.G, Color.B, byte.MaxValue / 4);
            }
            if (Color.R == byte.MaxValue && Color.B > byte.MinValue && Color.G == byte.MinValue)
            {
                Color = new Color(Color.R, Color.G, (byte) (Color.B - 1), byte.MaxValue / 4);
            }
        }

        public override void Update()
        {
            if (Position == Vector2.Zero)
            {
                FancyRainbowEffect();
            }
            foreach (Particle particle in Particles)
            {
                particle.Update(Position);
                if (Position == Vector2.Zero)
                {
                    particle.Color = Color;
                }
                else
                {
                    particle.Color = Helper.LerpClamp(particle.Color, Color, 0.1F);
                }
            }
        }
    }
}

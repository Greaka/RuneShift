using System;
using RuneShift.Code.GameStates.Ingame.Runes;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame.GamePlayEntities.Swarms
{
    class RuneBoundParticleSwarm : BoundParticleSwarm
    {
        private Rune Rune;

        private static Color RuneColor(Type Instance)
        {
            if (Instance == typeof (FireRune))
                return new Color(236, 0, 0);
            else if (Instance == typeof (WindRune))
                return new Color(8, 255, 204);
            else if (Instance == typeof (EarthRune))
                return new Color(78, 118, 0);
            else if (Instance == typeof (WaterRune))
                return new Color(71, 0, 229);
            else
                return Color.White;
        }

        public RuneBoundParticleSwarm(int particleCount, Rune rune)
            : base(particleCount, rune.Position, RuneColor(rune.GetType()))
        {
            this.Rune = rune;
            rune.particleSwarm = this;
        }

        public override void Update()
        {
            this.Position = Rune.Position;
            base.Update();
        }
    }
}

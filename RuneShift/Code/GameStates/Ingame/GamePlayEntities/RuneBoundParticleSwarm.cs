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
    public class RuneBoundParticleSwarm : BoundParticleSwarm
    {
        private Rune Rune;

        private static Color RuneColor(Type Instance)
        {
            if (Instance == typeof (FireRune))
                return new Color(236, 0, 0);
            else if (Instance == typeof (WindRune))
                return new Color(8, 219, 204);
            else if (Instance == typeof (EarthRune))
                return new Color(78, 78, 0);
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

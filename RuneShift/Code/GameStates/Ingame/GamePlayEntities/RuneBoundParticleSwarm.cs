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
        Rune Rune;

        public RuneBoundParticleSwarm(int particleCount, Rune rune)
            : base(particleCount, rune.Position)
        {
            this.Rune = rune;
        }

        public override void Update()
        {
            this.Position = Rune.Position;
            base.Update();
        }
    }
}

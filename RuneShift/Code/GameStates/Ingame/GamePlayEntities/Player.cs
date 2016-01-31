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
    public class Player
    {
        ParticleController ParticleController;

        public Player(ParticleManager particleManager, Map map)
        {
            ParticleController = new ParticleController(particleManager, map);
        }

        public void Update()
        {
            ParticleController.Update();
        }

        public void Draw(RenderWindow win, View view)
        {
            ParticleController.Draw(win, view);
        }

        private static void DebugDraw(RenderWindow win, View view)
        {
        }
    }
}

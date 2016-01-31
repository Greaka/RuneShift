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
        ParticleManager ParticleManager;

        ParticleSwarm selectedSwarm;

        float swarmSelectRadius;

        public Player(ParticleManager particleManager)
        {
            this.ParticleManager = particleManager;
            this.selectedSwarm = null;
            this.swarmSelectRadius = 5F;
        }

        public void update()
        {
            if (selectedSwarm == null)
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    // select a swarm
                    Vector2 clickPosition = Program.MousePositionToGameCoordinate();
                    float distanceToSwarm;
                    BoundParticleSwarm nearestSwarm = ParticleManager.GetNearestBoundSwarm(clickPosition, out distanceToSwarm);
                    if(nearestSwarm != null && distanceToSwarm < swarmSelectRadius)
                    {
                        selectedSwarm = nearestSwarm;
                    }
                }
            }
            else
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {

                }
            }
        }

        public void Draw(RenderWindow win, View view)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                DebugDraw(win, view);
            }
        }

        private static void DebugDraw(RenderWindow win, View view)
        {
            RectangleShape r = new RectangleShape(Vector2.One);
            r.Origin = (Vector2)r.Size / 2F;
            r.Position = Program.MousePositionToGameCoordinate();
            win.Draw(r);
        }
    }
}

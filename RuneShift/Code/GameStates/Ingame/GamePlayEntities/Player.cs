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
        Map Map;

        BoundParticleSwarm SelectedSwarm;
        
        float NumSelectedParticles;
        float SelectingSpeed;
        float SwarmSelectRadius;

        public Player(ParticleManager particleManager, Map map)
        {
            this.ParticleManager = particleManager;
            this.Map = map;

            this.SelectedSwarm = null;

            this.NumSelectedParticles = 0F;
            this.SelectingSpeed = 0.3F;
            this.SwarmSelectRadius = 5F;
        }

        public void Update()
        {
            if (SelectedSwarm == null)
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    // select a swarm
                    Vector2 clickPosition = Program.MousePositionToGameCoordinate();
                    float distanceToSwarm;
                    BoundParticleSwarm nearestSwarm = ParticleManager.GetNearestBoundSwarm(clickPosition, out distanceToSwarm);
                    if(nearestSwarm != null && distanceToSwarm < SwarmSelectRadius)
                    {
                        SelectedSwarm = nearestSwarm;
                    }
                }
            }
            else
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    NumSelectedParticles += SelectingSpeed;
                    if (NumSelectedParticles > SelectedSwarm.Count)
                        NumSelectedParticles = SelectedSwarm.Count;
                }
                else
                {
                    Vector2 releasePosition = Program.MousePositionToGameCoordinate();
                    float distanceToSwarm;
                    BoundParticleSwarm targetSwarm = ParticleManager.GetNearestBoundSwarm(releasePosition, out distanceToSwarm);
                    if (targetSwarm != null && distanceToSwarm < SwarmSelectRadius)
                    {
                        ParticleManager.TransferParticles(SelectedSwarm, targetSwarm, (int)NumSelectedParticles);
                    }
                    SelectedSwarm = null;
                    NumSelectedParticles = 0F;
                }
            }
        }

        public void Draw(RenderWindow win, View view)
        {
            if (SelectedSwarm != null)
            {
                RectangleShape r = new RectangleShape(Vector2.One);
                r.Origin = (Vector2)r.Size / 2F;
                r.Position = SelectedSwarm.Position;
                r.FillColor = Color.Red;
                r.Scale = Vector2.One * NumSelectedParticles;
                win.Draw(r);
            }
            
            DebugDraw(win, view);
        }

        private static void DebugDraw(RenderWindow win, View view)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                RectangleShape r = new RectangleShape(Vector2.One);
                r.Origin = (Vector2)r.Size / 2F;
                r.Position = Program.MousePositionToGameCoordinate();
                win.Draw(r);
            }
        }
    }
}

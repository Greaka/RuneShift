using RuneShift.Code.GameStates.Ingame.GamePlayEntities.Swarms;
using RuneShift.Code.Utility;
using SFML.Graphics;
using SFML.Window;

namespace RuneShift.Code.GameStates.Ingame.GamePlayEntities
{
    class ParticleController
    {
        ParticleManager ParticleManager;
        Map Map;

        BoundParticleSwarm SelectedSwarm;

        private TransitionParticleSwarm Balken;
        float NumSelectedParticles;
        float SelectingSpeed;
        float SwarmSelectRadius;

        public ParticleController(ParticleManager particleManager, Map map)
        {
            this.ParticleManager = particleManager;
            this.Map = map;

            this.SelectedSwarm = null;

            this.NumSelectedParticles = 0F;
            this.SelectingSpeed = 1.3F;
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
                        Balken = new TransitionParticleSwarm(SelectedSwarm);
                        ParticleManager.ParticleSwarms.Add(Balken);
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
                    if (NumSelectedParticles > 1 || NumSelectedParticles >= SelectedSwarm.Count)
                    {
                        ParticleManager.TransferParticles(SelectedSwarm, Balken, (int) NumSelectedParticles);
                        NumSelectedParticles %= 1;
                    }
                }
                else
                {
                    Vector2 releasePosition = Program.MousePositionToGameCoordinate();
                    float distanceToSwarm;
                    BoundParticleSwarm targetSwarm = ParticleManager.GetNearestBoundSwarm(releasePosition, out distanceToSwarm);
                    if (distanceToSwarm > SwarmSelectRadius)
                    {
                        targetSwarm = null;
                    }
                    ParticleManager.ParticleSwarms.Add(Balken.Release(targetSwarm));
                    ParticleManager.ParticleSwarms.Remove(Balken);
                    SelectedSwarm = null;
                    Balken = null;
                    NumSelectedParticles = 0F;
                }
            }
        }

        public void Draw(RenderWindow win)
        {
            if (SelectedSwarm != null)
            {
                RectangleShape r = new RectangleShape(Vector2.One);
                r.Origin = (Vector2)r.Size / 2F;
                r.Position = SelectedSwarm.Position;
                r.FillColor = Color.Red;
                r.Scale = Vector2.One * NumSelectedParticles / 10F;
                win.Draw(r);
            }
            
            DebugDraw(win);
        }

        private static void DebugDraw(RenderWindow win)
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

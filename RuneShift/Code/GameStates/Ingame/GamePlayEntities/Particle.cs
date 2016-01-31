using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame.GamePlayEntities
{
    class Particle
    {
        public Vector2 Position;
        Vector2 Direction;
        public float RotationSpeed { get; private set; }
        public Color Color = Color.White;

        public Particle(Vector2 position, Vector2 direction)
        {
            this.Position = position;
            this.Direction = direction.normalized;
            this.RotationSpeed = Rand.Value(360F);
        }

        public void Update(Vector2 target, float speed)
        {
            float rotationDirection = (Vector2.isToTheRight(Position, Direction, target) ? -1F : 1F);

            Direction.rotate(1 * rotationDirection);
            
            Position += Direction.normalize() * speed;
        }
    }
}

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
    class Particle
    {
        public Vector2 Position;
        Vector2 Direction;
        float Speed;
        public float RotationSpeed { get; private set; }
        public Color Color = Color.White;

        public Particle(Vector2 position, Vector2 direction, float speed)
        {
            this.Position = position;
            this.Direction = direction.normalized;
            this.Speed = speed;
            this.RotationSpeed = Rand.Value(360F);
        }

        public void Update(Vector2 target)
        {
            float rotationDirection = (Vector2.isToTheRight(Position, Direction, target) ? -1F : 1F);

            Direction.rotate(Rand.Value(0.002F, 0.09F) * rotationDirection);
            
            Position += Direction.normalize() * Speed;
        }
    }
}

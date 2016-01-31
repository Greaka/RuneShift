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
    public abstract class Enemy
    {
        protected Sprite Sprite;
        public Vector2 Position { get { return Sprite.Position; } protected set { Sprite.Position = value; } }
        protected float Life;

        public bool IsDead;

        public Enemy(Vector2 position, Sprite sprite, float life)
        {
            this.Sprite = sprite;
            sprite.Origin = (Vector2)sprite.Texture.Size / 2F;
            sprite.Scale = Vector2.One * 0.02F;
            this.Position = position;
            this.Life = life;

            this.IsDead = false;
        }

        abstract public void Update(Player player);

        public void Draw(RenderWindow win)
        {
            win.Draw(Sprite);
        }
    }
}

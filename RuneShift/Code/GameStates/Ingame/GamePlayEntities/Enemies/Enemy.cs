using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame.GamePlayEntities.Enemies
{
    abstract class Enemy
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

            RectangleShape r = new RectangleShape(new Vector2(Life, 0.5F));
            r.Position = Position + Vector2.Up * 4F + Vector2.Left * r.Size.X / 2F;
            r.FillColor = Color.Red;
            win.Draw(r);
        }
    }
}

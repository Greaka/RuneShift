using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace RuneShift
{
    abstract class Rune
    {
        Sprite Sprite;
        public Vector2 Position { get { return Sprite.Position; } set { Sprite.Position = value; } }
        public float Rotation { get { return Sprite.Rotation; } set { Sprite.Rotation = value; } }

        public Rune(Vector2 position, Texture texture)
        {
            this.Sprite = new Sprite(texture);
            this.Sprite.Scale = Vector2.One * 0.01F;
            Sprite.Origin = ((Vector2)Sprite.Texture.Size) / 2F;
            this.Position = position;
        }

        public void Draw(RenderWindow win)
        {
            if(Sprite != null)
            {
                win.Draw(Sprite);
            }

            DebugDraw(win);
        }

        RectangleShape debugGrphic = new RectangleShape(Vector2.One);

        void DebugDraw(RenderWindow win)
        {
            debugGrphic.FillColor = Color.Red;
            debugGrphic.Position = Position - ((Vector2)debugGrphic.Size / 2F);
            win.Draw(debugGrphic);
        }
    }
}

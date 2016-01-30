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
            Sprite.Origin = (((Vector2)Sprite.Scale) * Sprite.Texture.Size) / 2F;
            this.Position = position;
        }

        public void Draw(RenderWindow win)
        {
            if(Sprite == null)
            {
                RectangleShape debugGraphic = new RectangleShape(new Vector2(10F, 10F));
                debugGraphic.Origin = debugGraphic.Size / 2F;

                win.Draw(debugGraphic);
            }
            else
            {
                win.Draw(Sprite);
            }
        }
    }
}

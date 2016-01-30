using System;
using SFML;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

namespace RuneShift
{
    abstract class Rune
    {
        public static float AdjacencyDistance = 15F;

        Sprite Sprite;
        Texture StoneTexture;
        Texture GlowTexture;

        public Vector2 Position { get { return Sprite.Position; } set { Sprite.Position = value; } }
        public float Rotation { get { return Sprite.Rotation; } set { Sprite.Rotation = value; } }

        public List<Rune> AdjacentRunes = new List<Rune>();
        
        public Rune(Vector2 position, Texture stoneTexture, Texture glowTexture)
        {
            this.Sprite = new Sprite(stoneTexture);
            this.Sprite.Scale = Vector2.One * 0.008F;
            Sprite.Origin = ((Vector2)Sprite.Texture.Size) / 2F;

            StoneTexture = stoneTexture;
            GlowTexture = glowTexture;
            
            this.Position = position;
        }

        public void Draw(RenderWindow win)
        {
            if(Sprite != null)
            {
                /*
                Sprite.Texture = StoneTexture;
                win.Draw(Sprite);
                */
                Sprite.Texture = GlowTexture;
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

            foreach (Rune adjacentRune in AdjacentRunes)
            {
                //if (this.GetType() == adjacentRune.GetType())
                {
                    VertexArray line = new VertexArray();
                    line.Append(new Vertex(Position));
                    for (float t = 0F; t < 1F; t += 0.01F)
                    {
                        line.Append(new Vertex(Vector2.lerp(Position, adjacentRune.Position, t)));
                    }
                    line.Append(new Vertex(adjacentRune.Position));
                    win.Draw(line);

                }
            }
        }
    }
}

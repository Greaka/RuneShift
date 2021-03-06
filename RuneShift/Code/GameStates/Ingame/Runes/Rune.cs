﻿using System.Collections.Generic;
using RuneShift.Code.GameStates.Ingame.GamePlayEntities.Swarms;
using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame.Runes
{
    abstract class Rune
    {
        public static float AdjacencyDistance = 15F;

        Sprite Sprite;
        Texture StoneTexture;
        Texture GlowTexture;
        Texture DotTexture;

        public Vector2 Position { get { return Sprite.Position; } set { Sprite.Position = value; } }
        public float Rotation { get { return Sprite.Rotation; } set { Sprite.Rotation = value; } }

        public List<Rune> AdjacentRunes = new List<Rune>();

        public Element Element { get; protected set; }

        public RuneBoundParticleSwarm particleSwarm;
        
        public Rune(Vector2 position, Texture stoneTexture, Texture glowTexture, Texture dotTexture)
        {
            this.Sprite = new Sprite(stoneTexture);
            this.Sprite.Scale = Vector2.One * 0.008F;
            Sprite.Origin = ((Vector2)Sprite.Texture.Size) / 2F;

            StoneTexture = stoneTexture;
            GlowTexture = glowTexture;
            DotTexture = dotTexture;
            
            this.Position = position;
        }

        public static HashSet<Rune> GetAdjacentRunesRecursively(Rune startRune)
        {
            HashSet<Rune> visitedRunes = new HashSet<Rune>();
            GetAdjacentRunesRecursivelyHelper(startRune, visitedRunes);
            return visitedRunes;
        }

        private static void GetAdjacentRunesRecursivelyHelper(Rune startRune, HashSet<Rune> visitedRunes)
        {
            visitedRunes.Add(startRune);
            foreach (Rune adjacentRune in startRune.AdjacentRunes)
            {
                if(!visitedRunes.Contains(adjacentRune))
                    GetAdjacentRunesRecursivelyHelper(adjacentRune, visitedRunes);
            }
        }

        public void Draw(RenderWindow win)
        {
            if(Sprite != null)
            {
                Sprite.Color = Color.White;
                Sprite.Texture = StoneTexture;
                win.Draw(Sprite);
                var opacity = particleSwarm.Count >= 200 ? (byte) 255 :
                                (byte) (particleSwarm.Count);
                Sprite.Color = new Color(255, 255, 255, opacity);
                Sprite.Texture = opacity == 255 ? DotTexture : GlowTexture;
                win.Draw(Sprite);
            }

            //DebugDraw(win);
        }

        RectangleShape debugGraphic = new RectangleShape(Vector2.One);

        void DebugDraw(RenderWindow win)
        {
            debugGraphic.FillColor = Color.Red;
            debugGraphic.Position = Position - ((Vector2)debugGraphic.Size / 2F);
            win.Draw(debugGraphic);

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

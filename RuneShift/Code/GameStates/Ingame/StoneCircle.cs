using System;
using SFML;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

namespace RuneShift
{
    class StoneCircle
    {
        Sprite Sprite;

        List<Rune> Runes = new List<Rune>();
        float Radius;
        float Rotation;
        float RotationSpeed;

        StoneCircle NextInnerCircle;
        StoneCircle NextOuterCircle;

        public StoneCircle(float radius, int RuneCount, RotationDirection rotationDirection, Texture texture)
        {
            Sprite = new Sprite(texture);
            Sprite.Origin = (Vector2)Sprite.Texture.Size / 2F;
            Sprite.Scale = Vector2.One * 0.08F;

            this.Radius = radius;
            this.Rotation = 0F;

            RotationSpeed = (rotationDirection == RotationDirection.Clockwise ? -1F : 1F) * Rand.Value(0.0005F, 0.001F);

            for (int i = 0; i < RuneCount; ++i)
            {
                Runes.Add(CreateRandomRune(Vector2.Zero));
            }
            SetRunesAccordingToRotation();
        }

        public void SetNeighbourCircles(StoneCircle nextInnerCircle, StoneCircle nextOuterCircle)
        {
            NextInnerCircle = nextInnerCircle;
            NextOuterCircle = nextOuterCircle;
        }

        Rune CreateRandomRune(Vector2 position)
        {
            float rand = Rand.Value();
            float probabiltiyPerRuneKind = 1F / 4F;

            if (rand < probabiltiyPerRuneKind)
                return new FireRune(position);
            else if (rand < probabiltiyPerRuneKind * 2F)
                return new EarthRune(position);
            else if (rand < probabiltiyPerRuneKind * 3F)
                return new WaterRune(position);
            else
                return new WindRune(position);
        }

        public void UpdateRotation()
        {
            Rotation += RotationSpeed;
            SetRunesAccordingToRotation();
        }

        /// <summary>
        /// Clears all Rune Adjacencies (recursevly) from this StoneCircle going outwards
        /// </summary>
        public void ClearRuneAdjacenciesRecursively()
        {
            for (int i = 0; i < Runes.Count; ++i)
            {
                Runes[i].AdjacentRunes.Clear();
            }
            if (NextOuterCircle != null)
            {
                NextOuterCircle.ClearRuneAdjacenciesRecursively();
            }
        }

        /// <summary>
        /// Create all Rune Adjacencies (recursevly) from this StoneCircle going outwards
        /// </summary>
        public void CreateRuneAdjacenciesRecursively()
        {
            for (int i = 0; i < Runes.Count; ++i)
            {
                // search for adjacent Runes within the same StoneCircle
                for (int j = i; j < Runes.Count; ++j)
                {
                    if (Vector2.distance(Runes[i].Position, Runes[j].Position) < Rune.AdjacencyDistance)
                    {
                        CreateRuneAdjacency(Runes[i], Runes[j]);
                    }
                }
                // search for adjacent Runes within the next Outer StoneCircle
                if (NextOuterCircle != null)
                {
                    List<Rune> nextOuterAdjacentRunes = NextOuterCircle.GetRunesInRadius(Runes[i].Position, Rune.AdjacencyDistance);
                    foreach (Rune rune in nextOuterAdjacentRunes)
                    {
                        CreateRuneAdjacency(Runes[i], rune);
                    }
                }
            }
            if (NextOuterCircle != null)
            {
                NextOuterCircle.CreateRuneAdjacenciesRecursively();
            }
        }

        private void CreateRuneAdjacency(Rune r1, Rune r2)
        {
            r1.AdjacentRunes.Add(r2);
            r2.AdjacentRunes.Add(r1);
        }

        void SetRunesAccordingToRotation()
        {
            float stepSize = 2F * Helper.PI / (float)Runes.Count;

            for (int i = 0; i < Runes.Count; ++i)
            {
                float t = i * stepSize + Rotation;
                Vector2 pos = new Vector2(Math.Sin(t), Math.Cos(t)) * Radius;

                Runes[i].Position = pos;
                Runes[i].Rotation = -t * Helper.RadianToDegree;
            }
        }

        public Rune GetNearestRune(Vector2 position)
        {
            float temp;
            return GetNearestRune(position, out temp);
        }

        public Rune GetNearestRune(Vector2 position, out float distance)
        {
            Rune result = null;
            float minDistance = float.MaxValue;

            foreach (Rune rune in Runes)
            {
                float dist = Vector2.distanceSqr(position, rune.Position);
                if(dist < minDistance)
                {
                    minDistance = dist;
                    result = rune;
                }
            }

            distance = minDistance;
            return result;
        }

        public List<Rune> GetRunesInRadius(Vector2 position, float radius)
        {
            List<Rune> result = new List<Rune>();

            float radiusSqr = radius * radius;
            foreach (Rune rune in Runes)
            {
                if (Vector2.distanceSqr(rune.Position, position) < radiusSqr)
                {
                    result.Add(rune);
                }
            }
            return result;
        }

        public void Draw(RenderWindow win)
        {
            Sprite.Rotation = -Rotation * Helper.RadianToDegree;
            win.Draw(Sprite);

            foreach (Rune rune in Runes)
            {
                rune.Draw(win);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using RuneShift.Code.GameStates.Ingame.Runes;
using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame
{
    class StoneCircle
    {
        Sprite Sprite;

        List<Rune> Runes = new List<Rune>();
        float Radius;
        float Rotation;
        float RotationSpeed;
        private float Direction;

        StoneCircle NextInnerCircle;
        StoneCircle NextOuterCircle;

        public StoneCircle(float radius, int RuneCount, RotationDirection rotationDirection, Texture texture)
        {
            // Sprite Stuff
            Sprite = new Sprite(texture);
            Sprite.Origin = (Vector2)Sprite.Texture.Size / 2F;
            Sprite.Scale = Vector2.One * 0.08F;

            // Rotation
            this.Radius = radius;
            this.Rotation = 0F;
            this.Direction = rotationDirection == RotationDirection.Clockwise ? -1F : 1F;

            RotationSpeed = Direction * Rand.Value(0.0005F, 0.001F);

            // Create Runes
            CreateRunes(RuneCount);
            SetRunesAccordingToRotation();
        }

        public void SetNeighbourCircles(StoneCircle nextInnerCircle, StoneCircle nextOuterCircle)
        {
            NextInnerCircle = nextInnerCircle;
            NextOuterCircle = nextOuterCircle;
        }

        void CreateRunes(int count)
        {
            Element[] elements = new Element[count];

            int offset = Rand.IntValue(4);
            for (int i = 0; i < count; ++i)
            {
                elements[i] = (Element)((i + offset) % (int)Element.Count);
            }
            Rand.Permutate(elements, count * 2);

            for (int i = 0; i < count; ++i)
            {
                CreateRune(elements[i]);
            }
        }

        void CreateRune(Element element)
        {
            switch (element)
            {
                case Element.Earth: Runes.Add(new EarthRune(Vector2.Zero)); break;
                case Element.Fire: Runes.Add(new FireRune(Vector2.Zero)); break;
                case Element.Water: Runes.Add(new WaterRune(Vector2.Zero)); break;
                case Element.Wind: Runes.Add(new WindRune(Vector2.Zero)); break;
            }
        }

        public void UpdateRotation()
        {
            var sum = 0;
            foreach (var rune in Runes)
            {
                sum += rune.particleSwarm.Count;
            }
            Rotation += RotationSpeed - sum * 0.000001F * Direction;
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

        public List<Rune> GetAllRunes()
        {
            return Runes;
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

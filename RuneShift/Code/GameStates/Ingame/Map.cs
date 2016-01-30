using System;
using SFML;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

namespace RuneShift
{
    class Map
    {
        List<StoneCircle> StoneCircles = new List<StoneCircle>();

        public Map()
        {
            StoneCircles.Add(new StoneCircle(10F, 3, RotationDirection.Clockwise));
            StoneCircles.Add(new StoneCircle(20F, 6, RotationDirection.CounterClockwise));
            StoneCircles.Add(new StoneCircle(30F, 7, RotationDirection.Clockwise));
            StoneCircles.Add(new StoneCircle(40F, 9, RotationDirection.CounterClockwise));
            for (int i = 0; i < StoneCircles.Count; ++i)
            {
                StoneCircle nextInner = i - 1 < 0 ? null : StoneCircles[i - 1];
                StoneCircle nextOuter = i + 1 >= StoneCircles.Count ? null : StoneCircles[i + 1];
                StoneCircles[i].SetNeighbourCircles(nextInner, nextOuter);
            }
        }

        public void Update()
        {
            foreach(StoneCircle stoneCircle in StoneCircles)
            {
                stoneCircle.UpdateRotation();
            }
            // recursively reset Adjacencies from inner to outer Circle
            StoneCircles[0].ClearRuneAdjacenciesRecursively();
            StoneCircles[0].CreateRuneAdjacenciesRecursively();

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

            foreach (StoneCircle stoneCircle in StoneCircles)
            {
                float dist;
                Rune rune = stoneCircle.GetNearestRune(position, out dist);
                
                if (dist < minDistance)
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
            foreach (StoneCircle stoneCircle in StoneCircles)
            {
                result.AddRange(stoneCircle.GetRunesInRadius(position, radius));
            }
            return result;
        }

        public void Draw(RenderWindow win)
        {
            foreach (StoneCircle stoneCircle in StoneCircles)
            {
                stoneCircle.Draw(win);
            }
        }
    }
}

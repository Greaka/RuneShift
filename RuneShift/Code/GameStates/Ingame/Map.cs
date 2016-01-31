using System.Collections.Generic;
using RuneShift.Code.GameStates.Ingame.Runes;
using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame
{
    class Map
    {
        List<StoneCircle> StoneCircles = new List<StoneCircle>();

        public Map()
        {
            float innerRadius = 15F;
            float circleDistance = 9.5F;
            StoneCircles.Add(new StoneCircle(innerRadius,                       3, RotationDirection.Clockwise, AssetManager.getTexture(AssetManager.TextureName.Circle0_Innermost)));
            StoneCircles.Add(new StoneCircle(innerRadius + circleDistance,      6, RotationDirection.CounterClockwise, AssetManager.getTexture(AssetManager.TextureName.Circle1)));
            StoneCircles.Add(new StoneCircle(innerRadius + circleDistance * 2F, 7, RotationDirection.Clockwise, AssetManager.getTexture(AssetManager.TextureName.Circle2)));
            StoneCircles.Add(new StoneCircle(innerRadius + circleDistance * 3F, 9, RotationDirection.CounterClockwise, AssetManager.getTexture(AssetManager.TextureName.Circle3)));
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

        public List<Rune> GetAllRunes()
        {
            List<Rune> result = new List<Rune>();
            foreach (StoneCircle stoneCircle in StoneCircles)
            {
                result.AddRange(stoneCircle.GetAllRunes());
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

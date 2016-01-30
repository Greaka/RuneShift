using System;
using SFML;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

namespace RuneShift
{
    class StoneCircle
    {
        List<Rune> Runes = new List<Rune>();
        float Radius;
        float Rotation;

        public StoneCircle(float radius, int RuneCount)
        {
            this.Radius = radius;
            this.Rotation = 0F;

            for (int i = 0; i < RuneCount; ++i)
            {
                Runes.Add(new FireRune(Vector2.Zero));
            }
            SetRunesAccordingToRotation();

        }

        public void Update()
        {
            Rotation += 0.01F;
            SetRunesAccordingToRotation();
        }

        void SetRunesAccordingToRotation()
        {

            float stepSize = 2F * Helper.PI / (float)Runes.Count;

            for (int i = 0; i < Runes.Count; ++i)
            {
                float t = i * stepSize + Rotation;
                Vector2 pos = new Vector2(Math.Sin(t), Math.Cos(t)) * Radius;

                Runes[i] = new FireRune(pos);
            }
        }

        public void Draw(RenderWindow win)
        {
            foreach (Rune rune in Runes)
            {
                rune.Draw(win);
            }
        }
    }
}

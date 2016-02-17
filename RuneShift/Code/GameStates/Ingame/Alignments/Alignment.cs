using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;
using RuneShift.Code.GameStates.Ingame.Runes;
using RuneShift.Code.Utility;
using RuneShift.Code.GameStates.Ingame;
using RuneShift.Code.GameStates.Ingame.GamePlayEntities.Swarms;

namespace RuneShift
{
    class Alignment
    {
        public List<Rune> Runes;
        public static HashSet<Element> ActiveElements = new HashSet<Element>();

        public Alignment(ICollection<Rune> runes)
        {
            if (runes.Count <= 0)
                throw new Exception("Can't create empty Alignments");

            this.Runes = new List<Rune>(runes);
        }

        public bool IsIntact()
        {
            HashSet<Rune> possiblyChangedAlignedRunes = Rune.GetAdjacentRunesRecursively(Runes[0]);

            int matchingRuneCount = 0;
            foreach (Rune rune in possiblyChangedAlignedRunes)
            {
                if (!Runes.Contains(rune))
                    return false;
                else
                    ++matchingRuneCount;
            }

            return matchingRuneCount == Runes.Count;
        }

        public void UpdateActiveElements()
        {
            Dictionary<Element, int> elementParticleCount = new Dictionary<Element, int>(4);
            Dictionary<Element, int> elementCount = new Dictionary<Element, int>(4);

            foreach (Rune r in Runes)
            {
                if (!elementParticleCount.ContainsKey(r.Element))
                {
                    elementParticleCount[r.Element] = 0;
                    elementCount[r.Element] = 0;
                }
                elementParticleCount[r.Element] += r.particleSwarm.Count;
                elementCount[r.Element]++;
            }

            fillColor = Color.White;
            foreach (Element ele in elementParticleCount.Keys)
            {
                if (elementCount[ele] >= 3 && elementParticleCount[ele] > 300)
                {
                    Alignment.ActiveElements.Add(ele);
                    fillColor = RuneBoundParticleSwarm.RuneColor(ele);
                }
            }
        }

        Color fillColor;

        public void Draw(RenderWindow win)
        {
            for (int i = 0; i < Runes.Count; ++i)
            {
                for (int j = i; j < Runes.Count; ++j)
                {
                    DrawLine(win, Runes[i].Position, Runes[j].Position);
                }
            }
        }

        public void DrawLine(RenderWindow win, Vector2 from, Vector2 to)
        {
            VertexArray line = new VertexArray();
            for (float t = 0F; t < 1F; t += 0.01F)
            {
                line.Append(new Vertex(Vector2.lerp(from, to, t)));
            }
            line.Append(new Vertex(to));
            win.Draw(line);
        }

    }
}

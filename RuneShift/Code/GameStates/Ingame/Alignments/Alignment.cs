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

namespace RuneShift
{
    class Alignment
    {
        public List<Rune> Runes;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;
using RuneShift.Code.GameStates.Ingame;
using RuneShift.Code.GameStates.Ingame.GamePlayEntities;
using RuneShift.Code.GameStates.Ingame.Runes;
using RuneShift.Code.Utility;

namespace RuneShift
{
    class AlignmentManager
    {
        Player Player;
        Map Map;

        List<Alignment> Alignments = new List<Alignment>();
        Dictionary<Rune, Alignment> RuneAlignmentLink = new Dictionary<Rune,Alignment>();

        public AlignmentManager(Player player, Map map)
        {
            Player = player;
            Map = map;
        }

        public void Update()
        {
            // gather defect alignments
            List<Alignment> cachedAlignmentsForDelete = new List<Alignment>();
            foreach (Alignment alignment in Alignments)
            {
                if (!alignment.IsIntact())
                    cachedAlignmentsForDelete.Add(alignment);
            }
            // delete defect alignments
            foreach (Alignment alignmentToDelete in cachedAlignmentsForDelete)
            {
                foreach(Rune rune in alignmentToDelete.Runes)
                    RuneAlignmentLink.Remove(rune);

                Alignments.Remove(alignmentToDelete);
            }
            // create new alignments for Runes without
            foreach (Rune rune in Map.GetAllRunes())
            {
                if(!RuneAlignmentLink.ContainsKey(rune))
                {
                    HashSet<Rune> runeAlignment = Rune.GetAdjacentRunesRecursively(rune);
                    Alignment alignment = new Alignment(runeAlignment);
                    foreach (Rune r in runeAlignment)
                    {
                        RuneAlignmentLink[r] = alignment;
                    }
                    Alignments.Add(alignment);
                }
            }
            // activate Elements
            Alignment.ActiveElements.Clear();
            foreach (Alignment alignment in Alignments)
            {
                alignment.UpdateActiveElements();
            }
        }

        public void Draw(RenderWindow win)
        {
            foreach (Alignment alignment in Alignments)
            {
                alignment.Draw(win);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

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
                //TODO: create new Alignment
                }
            }
        }

        public void Draw(RenderWindow win)
        {
        }
    }
}

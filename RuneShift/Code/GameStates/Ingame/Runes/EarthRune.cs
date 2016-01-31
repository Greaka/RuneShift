using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace RuneShift
{
    class EarthRune : Rune
    {
        public EarthRune(Vector2 position)
            : base(position,
                  AssetManager.getTexture(AssetManager.TextureName.EarthRune),
                  AssetManager.getTexture(AssetManager.TextureName.EarthRuneGlow),
                  AssetManager.getTexture(AssetManager.TextureName.EarthRuneDot))
        {

        }
    }
}

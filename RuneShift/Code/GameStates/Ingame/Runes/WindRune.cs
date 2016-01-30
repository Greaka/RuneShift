using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace RuneShift
{
    class WindRune : Rune
    {
        public WindRune(Vector2 position)
            : base(position, AssetManager.getTexture(AssetManager.TextureName.WindRune), AssetManager.getTexture(AssetManager.TextureName.WindRuneGlow))
        {

        }
    }
}

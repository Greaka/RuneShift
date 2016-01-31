using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace RuneShift
{
    class WaterRune : Rune
    {
        public WaterRune(Vector2 position) 
            : base(
            position, 
            AssetManager.getTexture(AssetManager.TextureName.WaterRune), 
            AssetManager.getTexture(AssetManager.TextureName.WaterRuneGlow),
            AssetManager.getTexture(AssetManager.TextureName.WaterRuneDot))
        {

        }
    }
}

using RuneShift.Code.Utility;

namespace RuneShift.Code.GameStates.Ingame.Runes
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

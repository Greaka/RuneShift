using RuneShift.Code.Utility;

namespace RuneShift.Code.GameStates.Ingame.Runes
{
    class EarthRune : Rune
    {
        public EarthRune(Vector2 position)
            : base(position,
                  AssetManager.getTexture(AssetManager.TextureName.EarthRune),
                  AssetManager.getTexture(AssetManager.TextureName.EarthRuneGlow),
                  AssetManager.getTexture(AssetManager.TextureName.EarthRuneDot))
        {
            Element = Element.Earth;
        }
    }
}

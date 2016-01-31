using RuneShift.Code.Utility;

namespace RuneShift.Code.GameStates.Ingame.Runes
{
    class FireRune : Rune
    {
        public FireRune(Vector2 position)
            : base(position,
                  AssetManager.getTexture(AssetManager.TextureName.FireRune),
                  AssetManager.getTexture(AssetManager.TextureName.FireRuneGlow),
                  AssetManager.getTexture(AssetManager.TextureName.FireRuneDot))
        {
            Element = Element.Fire;
        }
    }
}

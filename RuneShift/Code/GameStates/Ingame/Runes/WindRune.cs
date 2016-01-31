using RuneShift.Code.Utility;

namespace RuneShift.Code.GameStates.Ingame.Runes
{
    class WindRune : Rune
    {
        public WindRune(Vector2 position)
            : base(position,
                  AssetManager.getTexture(AssetManager.TextureName.WindRune),
                  AssetManager.getTexture(AssetManager.TextureName.WindRuneGlow),
                  AssetManager.getTexture(AssetManager.TextureName.WindRuneDot))
        {

        }
    }
}

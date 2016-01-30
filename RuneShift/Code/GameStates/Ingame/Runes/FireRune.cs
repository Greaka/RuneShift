using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace RuneShift
{
    class FireRune : Rune
    {
        public FireRune(Vector2 position)
            : base(position, AssetManager.getTexture(AssetManager.TextureName.RuneFire))
        {

        }

        public void Draw(RenderWindow win)
        {
            base.Draw(win);
        }
    }
}

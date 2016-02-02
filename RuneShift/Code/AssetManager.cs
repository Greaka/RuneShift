using System;
using System.Collections.Generic;
using System.IO;
using SFML.Graphics;

namespace RuneShift.Code
{
    class AssetManager
    {
        static Dictionary<TextureName, Texture> textures = new Dictionary<TextureName, Texture>();

        public static Texture getTexture(TextureName textureName)
        {
            if (textures.Count == 0)
            {
                LoadTextures(); 
            }
            return textures[textureName];
        }

        static void LoadTextures()
        {
            const string path = "Graphics/{0}.png";
            textures.Add(TextureName.MainMenuBackground, new Texture(string.Format(path, "screen")));
            textures.Add(TextureName.InGameBackground, new Texture(string.Format(path, "background")));

            textures.Add(TextureName.Circle0_Innermost, new Texture("Graphics/circle_0.png"));
            textures.Add(TextureName.Circle1, new Texture("Graphics/circle_1.png"));
            textures.Add(TextureName.Circle2, new Texture("Graphics/circle_2.png"));
            textures.Add(TextureName.Circle3, new Texture("Graphics/circle_3.png"));

            textures.Add(TextureName.EnergyParticle, new Texture("Graphics/Particle.png"));

            textures.Add(TextureName.FireRune, new Texture("Graphics/rune3_fx.png"));
            textures.Add(TextureName.FireRuneGlow, new Texture("Graphics/rune3_glow.png"));
            textures.Add(TextureName.FireRuneDot, new Texture("Graphics/rune3_dot.png"));
            textures.Add(TextureName.WaterRune, new Texture("Graphics/rune1_fx.png"));
            textures.Add(TextureName.WaterRuneGlow, new Texture("Graphics/rune1_glow.png"));
            textures.Add(TextureName.WaterRuneDot, new Texture("Graphics/rune1_dot.png"));
            textures.Add(TextureName.WindRune, new Texture("Graphics/rune2_fx.png"));
            textures.Add(TextureName.WindRuneGlow, new Texture("Graphics/rune2_glow.png"));
            textures.Add(TextureName.WindRuneDot, new Texture("Graphics/rune2_dot.png"));
            textures.Add(TextureName.EarthRune, new Texture("Graphics/rune5_fx.png"));
            textures.Add(TextureName.EarthRuneGlow, new Texture("Graphics/rune5_glow.png"));
            textures.Add(TextureName.EarthRuneDot, new Texture("Graphics/rune5_dot.png"));

            textures.Add(TextureName.Enemy, new Texture("Graphics/Enemy.png"));
        }

        public enum TextureName
        {
            MainMenuBackground,
            InGameBackground,

            Circle0_Innermost,
            Circle1,
            Circle2,
            Circle3,
        
            EnergyParticle,

            Enemy,

            FireRune,
            FireRuneGlow,
            FireRuneDot,
            WaterRune,
            WaterRuneGlow,
            WaterRuneDot,
            WindRune,
            WindRuneGlow,
            WindRuneDot,
            EarthRune,
            EarthRuneGlow,
            EarthRuneDot,

        }
    }
}

using System.Collections.Generic;
using SFML.Graphics;

public class AssetManager
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
        textures.Add(TextureName.InGameBackground, new Texture("Graphics/background.png"));

        textures.Add(TextureName.Circle0_Innermost, new Texture("Graphics/circle_0.png"));
        textures.Add(TextureName.Circle1, new Texture("Graphics/circle_1.png"));
        textures.Add(TextureName.Circle2, new Texture("Graphics/circle_2.png"));
        textures.Add(TextureName.Circle3, new Texture("Graphics/circle_3.png"));

        textures.Add(TextureName.EnergyParticle, new Texture("Graphics/Particle.png"));

        textures.Add(TextureName.RuneFire, new Texture("Graphics/rune1.png"));
        textures.Add(TextureName.RuneWater, new Texture("Graphics/rune2.png"));
        textures.Add(TextureName.RuneAir, new Texture("Graphics/rune3.png"));
        textures.Add(TextureName.RuneEarth, new Texture("Graphics/rune4.png"));
    }

    public enum TextureName
    {
        InGameBackground,

        Circle0_Innermost,
        Circle1,
        Circle2,
        Circle3,
        
        EnergyParticle,
        
        RuneFire,
        RuneWater,
        RuneAir,
        RuneEarth,
        
    }
}

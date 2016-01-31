using RuneShift.Code.GameStates.Ingame.GamePlayEntities.Swarms;
using RuneShift.Code.GameStates.Ingame.Runes;
using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame.GamePlayEntities.Enemies
{
    class Knight : Enemy
    {
        float DamageRadiusSqr = 2F * 2F;
        float Strength = 2F;

        public Element element;

        public Knight(Vector2 position)
            : base(position, new Sprite(AssetManager.getTexture(AssetManager.TextureName.Enemy)), 10F)
        {
            element = (Element)Rand.IntValue((int)Element.Count);
            Sprite.Color = RuneBoundParticleSwarm.RuneColor(element);
        }

        public override void Update(Player player)
        {
            if (Alignment.ActiveElements.Contains(element))
                Life -= 0.1F;
            if(Life <= 0F)
            {
                IsDead = true;
                return;
            }

            Position -= Position.normalized * 0.005F;

            if (Position.lengthSqr < DamageRadiusSqr)
            {
                player.recieveDamage(this.Strength);
                IsDead = true;
            }
        }
    }
}

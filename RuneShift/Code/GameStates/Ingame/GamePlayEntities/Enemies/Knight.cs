using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame.GamePlayEntities.Enemies
{
    class Knight : Enemy
    {
        float DamageRadiusSqr = 2F * 2F;
        float Strength = 2F;

        public Knight(Vector2 position)
            : base(position, new Sprite(AssetManager.getTexture(AssetManager.TextureName.Enemy)), 10F)
        {
        }

        public override void Update(Player player)
        {
            Position -= Position.normalized * 0.02F;

            if (Position.lengthSqr < DamageRadiusSqr)
            {
                player.recieveDamage(this.Strength);
                IsDead = true;
            }
        }
    }
}

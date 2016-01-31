using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace RuneShift
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

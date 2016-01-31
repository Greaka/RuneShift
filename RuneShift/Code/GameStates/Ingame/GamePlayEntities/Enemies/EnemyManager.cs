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
    class EnemyManager
    {
        Player Player;

        List<Enemy> Enemies = new List<Enemy>();

        float FramesPerEnemySpawn;
        float NextEnemyCountDown;

        public EnemyManager(Player player)
        {
            FramesPerEnemySpawn = 60F * 5F;
            NextEnemyCountDown = 60F * 5F;

            Player = player;
        }

        public void Update()
        {
            NextEnemyCountDown--;
            if (NextEnemyCountDown <= 0)
            {
                Vector2 spawnPosition = Vector2.Up.rotate(Rand.Value(2F * Helper.PI));
                spawnPosition *= 50F;
                Enemies.Add(new Knight(spawnPosition));

                NextEnemyCountDown = FramesPerEnemySpawn;
            }

            List<Enemy> cachedEnemiesForDelete = new List<Enemy>();
            foreach (Enemy enemy in Enemies)
            {
                if(enemy.IsDead)
                {
                    cachedEnemiesForDelete.Add(enemy);
                }
                enemy.Update(Player);
            }
            foreach (Enemy enemy in cachedEnemiesForDelete)
            {
                Enemies.Remove(enemy);
            }
        }

        public void Draw(RenderWindow win)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(win);
            }
        }
    }
}

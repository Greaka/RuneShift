using System.Collections.Generic;
using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code.GameStates.Ingame.GamePlayEntities.Enemies
{
    class EnemyManager
    {
        Text DisplayedInfo;

        Player Player;

        List<Enemy> Enemies = new List<Enemy>();

        float FramesPerEnemySpawn;
        float NextEnemyCountDown;

        int NumKilledEnemies = 0;

        public EnemyManager(Player player)
        {
            DisplayedInfo = new Text("#enemies ---, #killed ---", new Font("Fonts/calibri.ttf"));
            DisplayedInfo.Position = Vector2.Right * 700;

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
                NumKilledEnemies++;
            }
        }

        public void Draw(RenderWindow win)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(win);
            }
        }

        public void DrawGUI(GUI gui)
        {
            DisplayedInfo.DisplayedString = "#Knights: " + Enemies.Count + "\n#Killed: " + NumKilledEnemies;
            gui.Draw(DisplayedInfo);
        }
    }
}

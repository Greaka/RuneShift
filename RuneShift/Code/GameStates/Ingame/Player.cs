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
    public class Player
    {
        Sprite sprite;
        Vector2f position { get { return sprite.Position; } set { sprite.Position = value; } }
        Vector2f movement { get; set; }


        public Player(Vector2f position)
        {
            this.sprite = new Sprite(AssetManager.getTexture(AssetManager.TextureName.RuneEarth));
            this.sprite.Scale = new Vector2(0.1F, 0.1F);

            this.position = position;
            this.movement = new Vector2f(0F, 0F);
        }

        public void update()
        {
            float deltaTime = (float)Program.gameTime.EllapsedTime.TotalSeconds;
            float speed = 8F * deltaTime;
            
            Vector2f inputMovement = new Vector2f(0F, 0F);

            inputMovement.Y += Keyboard.IsKeyPressed(Keyboard.Key.Down) ? speed : 0F;
            inputMovement.Y += Keyboard.IsKeyPressed(Keyboard.Key.Up) ? -speed : 0F;

            inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.Left) ? -speed : 0F;
            inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.Right) ? speed : 0F;

            if(inputMovement.Y != 0F || inputMovement.X != 0F)
            {
                movement += inputMovement * speed / (float)Math.Sqrt(inputMovement.X * inputMovement.X + inputMovement.Y * inputMovement.Y);
            }

            movement *= (1F - deltaTime * 4F);    // friction

            position += movement;
        }

        public void draw(RenderWindow win, View view)
        {
            win.Draw(sprite);
        }
    }
}

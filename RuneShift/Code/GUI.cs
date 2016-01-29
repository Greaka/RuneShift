using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace RuneShift
{
    class GUI
    {
        Queue<Sprite> cachedSprites = new Queue<Sprite>();
        RenderWindow win;
        View view { get { return win.GetView(); } }
        
        public GUI(RenderWindow win, View view)
        {
            this.win = win;
        }

        public void Draw(Sprite sprite)
        {
            // work on a copy, instead of the original, for the original could be reused outside this scope
            Sprite spriteCopy = new Sprite(sprite);

            // modify sprite, to fit it in the gui
            float viewScale = (float)view.Size.X / win.Size.X;

            spriteCopy.Scale *= viewScale;
            spriteCopy.Position = view.Center - view.Size / 2F + spriteCopy.Position * viewScale;

            // draw the sprite
            win.Draw(spriteCopy);
        }

        public void Draw(Text text)
        {
            // work on a copy, instead of the original, for the original could be reused outside this scope
            Text textCopy = new Text(text);

            // modify sprite, to fit it in the gui
            float viewScale = (float)view.Size.X / win.Size.X;

            textCopy.Scale *= viewScale;
            textCopy.Position = view.Center - view.Size / 2F + textCopy.Position * viewScale;

            // draw the sprite
            win.Draw(textCopy);
        }
    }
}
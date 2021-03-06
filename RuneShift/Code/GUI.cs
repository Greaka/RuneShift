﻿using System.Collections.Generic;
using RuneShift.Code.Utility;
using SFML.Graphics;

namespace RuneShift.Code
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

        public void Draw<T>(T transformDrawable) where T : Transformable, Drawable
        {
            Vector2 originalScale = transformDrawable.Scale;
            Vector2 originalPosition = transformDrawable.Position;

            // modify drawable, to fit it in the gui
            float viewScale = (float)view.Size.X / win.Size.X;

            transformDrawable.Scale *= viewScale;
            transformDrawable.Position = view.Center - view.Size / 2F + transformDrawable.Position * viewScale;

            win.Draw(transformDrawable);

            // reset to originalValues
            transformDrawable.Scale = originalScale;
            transformDrawable.Position = originalPosition;
        }
    }
}
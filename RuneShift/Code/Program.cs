﻿using SFML.Graphics;
using SFML.Window;
using System;

namespace RuneShift
{
    class Program
    {
        public static GameTime gameTime;
        public static int inGameFrameCount { get; private set; }

        static bool running = true;

        static GameState currentGameState = GameState.InGame;
        static GameState prevGameState = GameState.InGame;
        static IGameState state;

        static RenderWindow win;
        static View view;
        static GUI gui;

        static void Main(string[] args)
        {
            // initialize window and view
            win = new RenderWindow(new VideoMode(800, 600), "Hadoken!!!");
            view = new View();
            resetView();
            gui = new GUI(win, view);

            // exit Program, when window is being closed
            //win.Closed += new EventHandler(closeWindow);
            win.Closed += (sender, e) => { (sender as Window).Close(); };

            // initialize GameState
            handleNewGameState();

            // initialize GameTime
            gameTime = new GameTime();
            gameTime.Start();

            // debug Text
            Text debugText = new Text("debug Text", new Font("Fonts/calibri.ttf"));

            while (running && win.IsOpen())
            {
                KeyboardInputManager.update();

                if (currentGameState == GameState.InGame) 
                { inGameFrameCount++; }
                currentGameState = state.update();

                if (currentGameState != prevGameState)
                {
                    handleNewGameState();
                }

                // gather draw-stuff
                win.Clear(new Color(100, 149, 237));    //cornflowerblue ftw!!! 1337
                state.draw(win, view);
                state.drawGUI(gui);

                // some DebugText
                debugText.DisplayedString = "fps: " + (1.0F / gameTime.EllapsedTime.TotalSeconds);
                gui.Draw(debugText);

                // do the actual drawing
                win.SetView(view);
                win.Display();

                // check for window-events. e.g. window closed        
                win.DispatchEvents();

                // update GameTime
                gameTime.Update();
                int waitTime = (int)(16.667f - gameTime.EllapsedTime.Milliseconds);
                System.Threading.Thread.Sleep(waitTime >= 0 ? waitTime : 0);
            }
        }

        static void handleNewGameState()
        {
            switch (currentGameState)
            {
                case GameState.None:
                    running = false;
                    break;

                case GameState.MainMenu:
                    state = new MainMenuState();
                    break;

                case GameState.InGame:
                    state = new InGameState();
                    break;

                case GameState.Reset:
                    currentGameState = prevGameState;
                    prevGameState = GameState.Reset;
                    handleNewGameState();
                    break;
            }

            prevGameState = currentGameState;

            resetView();
        }

        static void resetView()
        {
            view.Center = new Vector2(win.Size.X / 2F, win.Size.Y / 2F);
            view.Size = new Vector2(win.Size.X, win.Size.Y);
        }
    }
}
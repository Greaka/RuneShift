using System.Collections.Generic;
using System.Linq;
using RuneShift.Code.GameStates;
using RuneShift.Code.GameStates.Ingame;
using RuneShift.Code.Utility;
using SFML.Graphics;
using SFML.Window;
using System;

namespace RuneShift.Code
{
    class Program
    {
        public static GameTime gameTime;
        public static int inGameFrameCount { get; private set; }

        static bool running = true;

        static GameState currentGameState = GameState.MainMenu;
        static GameState prevGameState = GameState.MainMenu;
        static IGameState state;

        static RenderWindow win;
        static View view;
        static GUI gui;

        static List<float> FPS = new List<float>();

        static void Main(string[] args)
        {
            // initialize window and view
            win = new RenderWindow(new VideoMode(1000, 700), "Hadoken!!!");
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
                if (FPS.Count > 70)
                    FPS.Remove(FPS.First());
                else if (FPS.Count == 0)
                    FPS.Add(1);
                else
                    FPS.Add((float)(1.0F / gameTime.EllapsedTime.TotalSeconds));
                float sum = 0;
                for (var i = 0; i < FPS.Count; i++)
                    sum += FPS[i];
                debugText.DisplayedString = "fps: " + (int)(sum / FPS.Count);
                gui.Draw(debugText);

                // do the actual drawing
                win.SetView(view);
                win.Display();

                // check for window-events. e.g. window closed        
                win.DispatchEvents();

                // update GameTime
                gameTime.Update();
                int waitTime = (int)((1F / 60F) * 1000F - gameTime.EllapsedTime.Milliseconds);
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

        public static Vector2 MousePositionToGameCoordinate()
        {
            return ScreenToGameCoordinate(win.InternalGetMousePosition());
        }

        public static Vector2 ScreenToGameCoordinate(Vector2 ScreenCoordinate)
        {
            Vector2 coordinate = ScreenCoordinate;

            // normalize to windowSpace [0, 1]
            coordinate /= win.Size;

            // shift center [-0.5, 0.5]
            coordinate -= Vector2.One * 0.5F;

            // shift to view-center
            coordinate -= (Vector2)view.Center;

            // scale according to view
            coordinate *= view.Size;

            return coordinate;
        }
    }
}
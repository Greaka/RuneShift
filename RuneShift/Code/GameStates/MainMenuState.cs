using SFML.Graphics;
using SFML.Window;

namespace RuneShift
{
    class MainMenuState : IGameState
    {
        public MainMenuState()
        {
        }

        public GameState update()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                return GameState.InGame;
            }

            return GameState.MainMenu;
        }

        public void draw(RenderWindow win, View view)
        {
        }

        public void drawGUI(GUI gui)
        {
        }
    }
}

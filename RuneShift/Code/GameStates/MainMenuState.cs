using RuneShift.Code.Utility;
using SFML.Graphics;
using SFML.Window;

namespace RuneShift.Code.GameStates
{
    class MainMenuState : IGameState
    {
        Text PressEnterText;

        public MainMenuState()
        {
            PressEnterText = new Text("Press Enter", new Font("Fonts/calibri.ttf"));
            PressEnterText.Position = new Vector2(300, 400);
        }

        public GameState update()
        {
            if (KeyboardInputManager.upward(Keyboard.Key.Return))
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
            gui.Draw(PressEnterText);
        }
    }
}

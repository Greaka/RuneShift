using RuneShift.Code.Utility;
using SFML.Graphics;
using SFML.Window;

namespace RuneShift.Code.GameStates
{
    class MainMenuState : IGameState
    {
        Sprite Background;

        public MainMenuState()
        {
            Background = new Sprite(AssetManager.getTexture(AssetManager.TextureName.MainMenuBackground));
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
            Background.Scale = (Vector2)win.Size / (Vector2)Background.Texture.Size;
            win.Draw(Background);
        }

        public void drawGUI(GUI gui)
        {

        }
    }
}

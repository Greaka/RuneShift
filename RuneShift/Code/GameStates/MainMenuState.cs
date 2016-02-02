using RuneShift.Code.Utility;
using SFML.Graphics;
using SFML.Window;

namespace RuneShift.Code.GameStates
{
    class MainMenuState : IGameState
    {
        private Sprite bg;

        public MainMenuState()
        {
            bg = new Sprite(AssetManager.getTexture(AssetManager.TextureName.MainMenuBackground));
            //bg.Origin = (Vector2)bg.Texture.Size / 2F;
            bg.Scale = Vector2.One * 0.63F;
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
            gui.Draw(bg);
        }
    }
}

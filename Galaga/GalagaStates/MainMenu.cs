using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage = new Entity(new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f), new Image("Assets/Images/TitleImage.png"));
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        //Initialize the menu buttons
        Text newGameButton = new Text("New Game", new Vec2F(0.3f, 0.3f), new Vec2F(0.5f, 0.5f));
        Text Quit = new Text("Quit", new Vec2F(0.3f, 0.3f), new Vec2F(0.5f, 0.5f));

        public static MainMenu GetInstance() {
            if (MainMenu.instance == null) {
                MainMenu.instance = new MainMenu();
                MainMenu.instance.InitializeGameState();
            }
            return MainMenu.instance;
        }

        //Render the titile image and the menu buttons
        public void RenderState() {
            backGroundImage.RenderEntity();
            foreach (Text button in menuButtons) {
                button.RenderText();
            }
        }
    }
}
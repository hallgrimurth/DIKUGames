using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;



namespace Galaga.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        public static MainMenu GetInstance() {
            if (MainMenu.instance == null) {
                MainMenu.instance = new MainMenu();
                MainMenu.instance.InitializeGameState();
            }
            return MainMenu.instance;
        }

        private void InitializeGameState(){
            //Initialize the menu buttons
            Text newGameButton = new Text("New Game", new Vec2F(0.1f, 0.2f), new Vec2F(0.5f, 0.5f));
            Text Quit = new Text("Quit", new Vec2F(0.1f, 0.1f), new Vec2F(0.5f, 0.5f));
            activeMenuButton = 0;
            newGameButton.SetColor(new Vec3I(255, 255, 255));
            Quit.SetColor(new Vec3I(255, 255, 255));
            menuButtons = new Text[2] {newGameButton, Quit};
            maxMenuButtons = menuButtons.Length;
            backGroundImage = new Entity(new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f), new Image("Assets/Images/TitleImage.png"));
        }
        

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
            switch(action){
                case KeyboardAction.KeyRelease:
                  
                    switch(key){
                        case KeyboardKey.Up:
                            if (activeMenuButton == 0){
                                activeMenuButton = maxMenuButtons - 1;
                            } else {
                                activeMenuButton --;
                            }
                            break;
                        case KeyboardKey.Down:
                            if (activeMenuButton == maxMenuButtons - 1){
                                activeMenuButton = 0;
                            } else {
                                activeMenuButton ++;
                            }
                            break;
                        case KeyboardKey.Enter:

                            switch(activeMenuButton){
                                case 0:
                                    GalagaBus.GetBus().RegisterEvent(
                                        new GameEvent{
                                            EventType = GameEventType.GameStateEvent,
                                            Message = "CHANGE_STATE",
                                            StringArg1 = "GAME_RUNNING",
                                            StringArg2 = "NEW_GAME"

                                        }

                                        
                                    );
                                    GameRunning.GetInstance();
                                    GalagaBus.GetBus().ProcessEventsSequentially();

                                    break;
                                case 1:
                                    GalagaBus.GetBus().RegisterEvent(
                                        new GameEvent{
                                            EventType = GameEventType.WindowEvent,
                                            Message = "CLOSE_WINDOW",
                                            StringArg1 = "CLOSING_GAME"
                                        }
                                    );
                            
                                break;
                            }
                            break;
                    }
                    break;
            }
        }

         //Render the titile image and the menu buttons
        public void RenderState() {
            backGroundImage.RenderEntity();
            foreach (Text button in menuButtons) {
                button.RenderText();
            }
        }

        public void ResetState(){
            throw new System.NotImplementedException();
        }

        public void UpdateState(){
            // throw new System.NotImplementedException();
            for (int i = 0; i < maxMenuButtons; i++){
                if (i != activeMenuButton){
                    menuButtons[i].SetColor(new Vec3I(255, 255, 255));
                } else {
                    menuButtons[i].SetColor(new Vec3I(255, 0, 0));
                }
            }
        }
    }
}
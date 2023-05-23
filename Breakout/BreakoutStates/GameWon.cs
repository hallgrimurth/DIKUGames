using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;

namespace Breakout.BreakoutStates {
    public class GameWon : IGameState {
        private static GameWon instance = null;
        private Text gamewon;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        public static GameWon GetInstance() {
            if (GameWon.instance == null) {
                GameWon.instance = new GameWon();
                GameWon.instance.InitializeGameState();
            }
            return GameWon.instance;
        }

        private void InitializeGameState(){
            gamewon = new Text("You Win!", new Vec2F(0.1f, 0.3f), new Vec2F(0.5f, 0.5f));
            gamewon.SetColor(new Vec3I(255, 255, 0));

            Text mainMenuButton = new Text("Main Menu", new Vec2F(0.1f, 0.15f), new Vec2F(0.5f, 0.5f));
            Text Quit = new Text("Quit", new Vec2F(0.1f, 0.05f), new Vec2F(0.5f, 0.5f));
            activeMenuButton = 0;
            mainMenuButton.SetColor(new Vec3I(255, 255, 255));
            Quit.SetColor(new Vec3I(255, 255, 255));
            menuButtons = new Text[2] {mainMenuButton, Quit};
            maxMenuButtons = menuButtons.Length;
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
                                    BreakoutBus.GetBus().RegisterEvent(
                                        new GameEvent{
                                            EventType = GameEventType.GameStateEvent,
                                            Message = "CHANGE_STATE",
                                            StringArg1 = "MAIN_MENU",
                                            //StringArg2 = "NEW_GAME"
                                        }
                                    );
                                    BreakoutBus.GetBus().ProcessEventsSequentially();

                                    break;
                                case 1:
                                    BreakoutBus.GetBus().RegisterEvent(
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
            gamewon.RenderText();

            foreach (Text button in menuButtons) {
                button.RenderText();
            }
        }

        public void ResetState(){
            throw new System.NotImplementedException();
        }

        public void UpdateState(){
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
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;

namespace Breakout.BreakoutStates {
    public class GameOver : IGameState {
        private static GameOver instance = null;
        private Text gameover;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        public static GameOver GetInstance() {
            if (GameOver.instance == null) {
                GameOver.instance = new GameOver();
                GameOver.instance.InitializeGameState();
            }
            return GameOver.instance;
        }

        private void InitializeGameState(){
            gameover = new Text("Game Over!", new Vec2F(0.1f, 0.3f), new Vec2F(0.5f, 0.5f));
            gameover.SetColor(new Vec3I(255, 255, 0));

            Text mainMenuButton = new Text("Main Menu", new Vec2F(0.1f, 0.15f), new Vec2F(0.5f, 0.5f));
            activeMenuButton = 0;
            mainMenuButton.SetColor(new Vec3I(255, 255, 255));
            menuButtons = new Text[1] {mainMenuButton};
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
                                        }
                                    );
                                    // BreakoutBus.GetBus().ProcessEventsSequentially();

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
            gameover.RenderText();

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
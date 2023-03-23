using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;

namespace Galaga.GalagaStates {
    public class GamePaused : IGameState {
        private static GamePaused instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        public static GamePaused GetInstance() {
            if (GamePaused.instance == null) {
                GamePaused.instance = new GamePaused();
                GamePaused.instance.InitializeGameState();
            }
            return GamePaused.instance;
        }

        private void InitializeGameState(){
   
        }
        
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
            switch(action){
                case KeyboardAction.KeyRelease:       
                    switch(key){
                    //     case KeyboardKey.Up:
                    //         if (activeMenuButton == 0){
                    //             activeMenuButton = maxMenuButtons - 1;
                    //         } else {
                    //             activeMenuButton --;
                    //         }
                    //         break;
                    //     case KeyboardKey.Down:
                    //         if (activeMenuButton == maxMenuButtons - 1){
                    //             activeMenuButton = 0;
                    //         } else {
                    //             activeMenuButton ++;
                    //         }
                    //         break;
                        case KeyboardKey.P:
                            GalagaBus.GetBus().RegisterEvent(
                                new GameEvent{
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "CHANGE_STATE",
                                    StringArg1 = "GAME_RUNNING"
                                }
                            );
                            break;
                    }
                    break;
            }
        }

         //Render the titile image and the menu buttons
        public void RenderState() {
            // GameRunning.GetInstance().RenderState();
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
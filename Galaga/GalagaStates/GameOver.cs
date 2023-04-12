using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;

namespace Galaga.GalagaStates {
    public class GameOver : IGameState {
        private static GameOver instance = null;
        private Text gameover;
        public static GameOver GetInstance() {
            if (GameOver.instance == null) {
                GameOver.instance = new GameOver();
                GameOver.instance.InitializeGameState();
            }
            return GameOver.instance;
        }

        private void InitializeGameState(){
            gameover = new Text("Game Over \n Press R to try again", new Vec2F(0.1f, 0.2f), new Vec2F(0.5f, 0.5f));
            gameover.SetColor(new Vec3I(255, 255, 255));
        }
        
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
            switch(action){
                case KeyboardAction.KeyRelease:       
                    switch(key){
                        case KeyboardKey.R:
                            GalagaBus.GetBus().RegisterEvent(
                                new GameEvent{
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "CHANGE_STATE",
                                    StringArg1 = "MAIN_MENU"
                                }
                            );
                            break;
                    }
                    break;
            }
        }

         //Render the titile image and the menu buttons
        public void RenderState() {
            gameover.RenderText();
        }

        public void ResetState(){
            throw new System.NotImplementedException();
        }

        public void UpdateState(){
            throw new System.NotImplementedException();
        }
    }
}
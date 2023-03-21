//using DIKUArcade.Graphics.Text;

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

        void ResetState();
        void UpdateState();
 
        void RenderState(){
            new backGroundImage(TitleImage.png);
            new menuButtons("New Game", "Quit");
        }
    
        void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
            //action = "KEY_PRESS";
            //action = "KEY_RELEASE";

            //KEY_UP
            //KEY_DOWN
            //KEY_ENTER

            if(menuButtons.X){
                GalagaBus.GetBus().RegisterEvent(
                    new GameEvent{
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_RUNNING"
                    }
                );

            }
        }
    }   

}
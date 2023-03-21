using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;



namespace Galaga.GalagaStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;


        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }

        private void InitializeGameState(){
            //Initialize the menu buttons
            Text newGameButton = new Text("New Game", new Vec2F(0.3f, 0.3f), new Vec2F(0.5f, 0.5f));
            Text Quit = new Text("Quit", new Vec2F(0.3f, 0.3f), new Vec2F(0.5f, 0.5f));

            backGroundImage = new Entity(new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f), new Image("Assets/Images/TitleImage.png"));


        }


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
            throw new System.NotImplementedException();
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
            
        }

        
    }
}
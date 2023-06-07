using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;

namespace Breakout.BreakoutStates {
    /// <summary>
    /// Represents the main menu state of the Breakout game.
    /// </summary>
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        /// <summary>
        /// Returns the instance of the MainMenu state.
        /// If the instance is null, it initializes the state before returning.
        /// </summary>
        /// <returns>The instance of the MainMenu state.</returns>
        public static MainMenu GetInstance() {
            if (MainMenu.instance == null) {
                MainMenu.instance = new MainMenu();
                MainMenu.instance.InitializeGameState();
            }
            return MainMenu.instance;
        }

        /// <summary>
        /// Initializes the MainMenu state by setting up the background image and menu buttons.
        /// </summary>
        private void InitializeGameState(){
            backGroundImage = new Entity(new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f), 
                                         new Image("Assets/Images/BreakoutTitleScreen.png")); 

            Text newGameButton = new Text("New Game", new Vec2F(0.1f, 0.2f), new Vec2F(0.5f, 0.5f));
            Text Quit = new Text("Quit", new Vec2F(0.1f, 0.1f), new Vec2F(0.5f, 0.5f));
            activeMenuButton = 0;
            newGameButton.SetColor(new Vec3I(255, 255, 255));
            Quit.SetColor(new Vec3I(255, 255, 255));
            menuButtons = new Text[2] {newGameButton, Quit};
            maxMenuButtons = menuButtons.Length;
        }
        
        /// <summary>
        /// Handles key events for navigating the menu buttons.
        /// </summary>
        /// <param name="action">The keyboard action.</param>
        /// <param name="key">The keyboard key.</param>
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
                                    // Change state to game running
                                    GameRunning.GetInstance().ResetState();
                                    BreakoutBus.GetBus().RegisterEvent(
                                        new GameEvent{
                                            EventType = GameEventType.GameStateEvent,
                                            Message = "CHANGE_STATE",
                                            StringArg1 = "GAME_RUNNING",
                                            StringArg2 = "RESUME"
                                        }
                                    );
                                    break;
                                case 1:
                                    // Close the game window
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

        /// <summary>
        /// Renders the background image and menu buttons.
        /// </summary>
        public void RenderState() {
            backGroundImage.RenderEntity();
            foreach (Text button in menuButtons) {
                button.RenderText();
            }
        }

        /// <summary>
        /// Resets the state (not implemented in this state).
        /// </summary>
        public void ResetState(){
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates the active menu button's color based on the selected index.
        /// </summary>
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

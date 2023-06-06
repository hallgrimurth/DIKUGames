using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;

namespace Breakout.BreakoutStates {
    /// <summary>
    /// Represents the game state when the player wins the game.
    /// </summary>
    public class GameWon : IGameState {
        private static GameWon instance = null;
        private Text gamewon;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        /// <summary>
        /// Returns the instance of the GameWon state.
        /// If the instance is null, it initializes the state before returning.
        /// </summary>
        /// <returns>The instance of the GameWon state.</returns>
        public static GameWon GetInstance() {
            if (GameWon.instance == null) {
                GameWon.instance = new GameWon();
                GameWon.instance.InitializeGameState();
            }
            return GameWon.instance;
        }

        /// <summary>
        /// Initializes the GameWon state by setting up the game won text and menu buttons.
        /// </summary>
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
                                    // Change state to main menu
                                    BreakoutBus.GetBus().RegisterEvent(
                                        new GameEvent{
                                            EventType = GameEventType.GameStateEvent,
                                            Message = "CHANGE_STATE",
                                            StringArg1 = "MAIN_MENU",
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
        /// Renders the game won text and menu buttons.
        /// </summary>
        public void RenderState() {
            gamewon.RenderText();

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

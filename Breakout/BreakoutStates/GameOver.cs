using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.State;

namespace Breakout.BreakoutStates
{
    /// <summary>
    /// Represents the game over state in the Breakout game.
    /// </summary>
    public class GameOver : IGameState
    {
        private static GameOver instance = null;
        private Text gameover;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        private GameOver()
        {
            InitializeGameState();
        }

        /// <summary>
        /// Returns the instance of the GameOver state.
        /// </summary>
        /// <returns>The GameOver state instance.</returns>
        public static GameOver GetInstance()
        {
            return instance ??= new GameOver();
        }

        private void InitializeGameState()
        {
            // Initialize the game over text
            gameover = new Text("Game Over!", new Vec2F(0.1f, 0.3f), new Vec2F(0.5f, 0.5f));
            gameover.SetColor(new Vec3I(255, 255, 0));

            // Initialize the main menu button
            Text mainMenuButton = new Text("Main Menu", new Vec2F(0.1f, 0.15f), new Vec2F(0.5f, 0.5f));
            activeMenuButton = 0;
            mainMenuButton.SetColor(new Vec3I(255, 255, 255));
            menuButtons = new Text[1] { mainMenuButton };
            maxMenuButtons = menuButtons.Length;
        }

        /// <summary>
        /// Handles keyboard events in the game over state.
        /// </summary>
        /// <param name="action">The keyboard action.</param>
        /// <param name="key">The keyboard key.</param>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
        {
            if (action == KeyboardAction.KeyRelease)
            {
                switch (key)
                {
                    case KeyboardKey.Up:
                        if (activeMenuButton == 0)
                        {
                            activeMenuButton = maxMenuButtons - 1;
                        }
                        else
                        {
                            activeMenuButton--;
                        }
                        break;
                    case KeyboardKey.Down:
                        if (activeMenuButton == maxMenuButtons - 1)
                        {
                            activeMenuButton = 0;
                        }
                        else
                        {
                            activeMenuButton++;
                        }
                        break;
                    case KeyboardKey.Enter:
                        switch (activeMenuButton)
                        {
                            case 0:
                                // Register a game state event to change to the main menu state
                                BreakoutBus.GetBus().RegisterEvent(
                                    new GameEvent
                                    {
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
        }

        /// <summary>
        /// Renders the game over state.
        /// </summary>
        public void RenderState()
        {
            // Render the game over text
            gameover.RenderText();

            // Render the menu buttons
            foreach (Text button in menuButtons)
            {
                button.RenderText();
            }
        }

        /// <summary>
        /// Resets the game over state.
        /// </summary>
        public void ResetState()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates the game over state.
        /// </summary>
        public void UpdateState()
        {
            // Update the color of the menu buttons based on the active button
            for (int i = 0; i < maxMenuButtons; i++)
            {
                if (i != activeMenuButton)
                {
                    menuButtons[i].SetColor(new Vec3I(255, 255, 255));
                }
                else
                {
                    menuButtons[i].SetColor(new Vec3I(255, 0, 0));
                }
            }
        }
    }
}

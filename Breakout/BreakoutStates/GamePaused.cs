using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.State;

namespace Breakout.BreakoutStates
{
    /// <summary>
    /// Represents the game paused state in the Breakout game.
    /// </summary>
    public class GamePaused : IGameState
    {
        private static GamePaused instance = null;
        private Text pause;

        private GamePaused()
        {
            InitializeGameState();
        }

        /// <summary>
        /// Returns the instance of the GamePaused state.
        /// </summary>
        /// <returns>The GamePaused state instance.</returns>
        public static GamePaused GetInstance()
        {
            return instance ??= new GamePaused();
        }

        private void InitializeGameState()
        {
            // Initialize the pause text
            pause = new Text("Game Paused", new Vec2F(0.1f, 0.2f), new Vec2F(0.5f, 0.5f));
            pause.SetColor(new Vec3I(255, 255, 255));
        }

        /// <summary>
        /// Handles keyboard events in the game paused state.
        /// </summary>
        /// <param name="action">The keyboard action.</param>
        /// <param name="key">The keyboard key.</param>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
        {
            if (action == KeyboardAction.KeyRelease)
            {
                switch (key)
                {
                    case KeyboardKey.Escape:
                        // Register a game state event to change to the game running state
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent
                            {
                                EventType = GameEventType.GameStateEvent,
                                Message = "CHANGE_STATE",
                                StringArg1 = "GAME_RUNNING",
                                StringArg2 = "RESUME"
                            }
                        );
                        break;
                }
            }
        }

        /// <summary>
        /// Renders the game paused state.
        /// </summary>
        public void RenderState()
        {
            // Render the pause text
            pause.RenderText();
        }

        /// <summary>
        /// Resets the game paused state.
        /// </summary>
        public void ResetState()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates the game paused state.
        /// </summary>
        public void UpdateState()
        {
            // This method does not have any implementation
        }
    }
}

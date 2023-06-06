using System;

namespace Breakout.BreakoutStates
{
    /// <summary>
    /// Represents the different types of game states in the Breakout game.
    /// </summary>
    public enum GameStateType
    {
        /// <summary>
        /// The game is currently running.
        /// </summary>
        GameRunning,

        /// <summary>
        /// The game is currently paused.
        /// </summary>
        GamePaused,

        /// <summary>
        /// The game is over.
        /// </summary>
        GameOver,

        /// <summary>
        /// The main menu of the game.
        /// </summary>
        MainMenu,

        /// <summary>
        /// The game is won.
        /// </summary>
        GameWon
    }
}

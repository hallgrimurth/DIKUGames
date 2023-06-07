using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Physics;
using System;
using DIKUArcade.Timers;
using DIKUArcade.State;

namespace Breakout.BreakoutStates
{
    /// <summary>
    /// Represents the game running state in the Breakout game.
    /// </summary>
    public class GameRunning : IGameState
    {
        private static GameRunning instance = null;
        private LevelManager levelManager;
        private TimeManager timeManager;
        private Points points;

        /// <summary>
        /// Constructs a new instance of the GameRunning state.
        /// </summary>
        private GameRunning()
        {
            InitializeGameState();
        }

        /// <summary>
        /// Returns the instance of the GameRunning state.
        /// </summary>
        /// <returns>The GameRunning state instance.</returns>
        public static GameRunning GetInstance()
        {
            // return new GameRunning
            return instance ??= new GameRunning();
        }

        /// <summary>
        /// Initializes the game state by creating the level manager, time manager, and points.
        /// </summary>
        public void InitializeGameState()
        {
            levelManager = new LevelManager();
            timeManager = new TimeManager();
            SetPoints();
        }

        /// <summary>
        /// Sets up the points display.
        /// </summary>
        private void SetPoints()
        {
            points = new Points(new Vec2F(0.65f, -0.3f), new Vec2F(0.4f, 0.4f));
        }

        /// <summary>
        /// Handles the key press events and triggers corresponding game events.
        /// </summary>
        /// <param name="key">The key that was pressed.</param>
        public void KeyPress(KeyboardKey key)
        {
            switch (key)
            {
                case KeyboardKey.A:
                    // Trigger MOVE_LEFT event
                    GameEvent moveLeft = new GameEvent
                    {
                        EventType = GameEventType.MovementEvent,
                        Message = "MOVE_LEFT"
                    };
                    BreakoutBus.GetBus().RegisterEvent(moveLeft);
                    break;

                case KeyboardKey.D:
                    // Trigger MOVE_RIGHT event
                    GameEvent moveRight = new GameEvent
                    {
                        EventType = GameEventType.MovementEvent,
                        Message = "MOVE_RIGHT"
                    };
                    BreakoutBus.GetBus().RegisterEvent(moveRight);
                    break;

                case KeyboardKey.C:
                    // Trigger CLOSE_WINDOW event
                    GameEvent closeWindowEvent = new GameEvent
                    {
                        EventType = GameEventType.WindowEvent,
                        Message = "CLOSE_WINDOW"
                    };
                    BreakoutBus.GetBus().RegisterEvent(closeWindowEvent);
                    break;

                case KeyboardKey.Left:
                    // Trigger PREV_LEVEL event
                    GameEvent prevLevel = new GameEvent
                    {
                        EventType = GameEventType.StatusEvent,
                        To = levelManager,
                        Message = "PREV_LEVEL"
                    };
                    BreakoutBus.GetBus().RegisterEvent(prevLevel);
                    break;

                case KeyboardKey.Right:
                    // Trigger NEXT_LEVEL event
                    GameEvent nextLevel = new GameEvent
                    {
                        EventType = GameEventType.StatusEvent,
                        To = levelManager,
                        Message = "NEXT_LEVEL"
                    };
                    BreakoutBus.GetBus().RegisterEvent(nextLevel);
                    break;

                case KeyboardKey.Space:
                    // Trigger START_GAME event and resume the timer
                    GameEvent startGame = new GameEvent
                    {
                        EventType = GameEventType.StatusEvent,
                        To = levelManager,
                        Message = "START_GAME"
                    };
                    BreakoutBus.GetBus().RegisterEvent(startGame);
                    StaticTimer.ResumeTimer();
                    break;
            }
        }

        /// <summary>
        /// Handles the key release events and triggers corresponding game events.
        /// </summary>
        /// <param name="key">The key that was released.</param>
        public void KeyRelease(KeyboardKey key)
        {
            switch (key)
            {
                case KeyboardKey.A:
                    // Trigger STOP_LEFT event
                    GameEvent stopLeft = new GameEvent
                    {
                        EventType = GameEventType.MovementEvent,
                        Message = "STOP_LEFT"
                    };
                    BreakoutBus.GetBus().RegisterEvent(stopLeft);
                    break;

                case KeyboardKey.D:
                    // Trigger STOP_RIGHT event
                    GameEvent stopRight = new GameEvent
                    {
                        EventType = GameEventType.MovementEvent,
                        Message = "STOP_RIGHT"
                    };
                    BreakoutBus.GetBus().RegisterEvent(stopRight);
                    break;

                case KeyboardKey.Escape:
                    // Trigger CHANGE_STATE event to switch to GamePaused state with "PAUSE" message
                    GameEvent changeState = new GameEvent
                    {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_PAUSED",
                        StringArg2 = "PAUSE"
                    };
                    BreakoutBus.GetBus().RegisterEvent(changeState);
                    break;
            }
        }

        /// <summary>
        /// Handles the keyboard events.
        /// </summary>
        /// <param name="action">The keyboard action (KeyPress or KeyRelease).</param>
        /// <param name="key">The key that was pressed or released.</param>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
        {
            switch (action)
            {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;

                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;
            }
        }

        /// <summary>
        /// Renders the game state by rendering the level, points, and time.
        /// </summary>
        public void RenderState()
        {
            levelManager.RenderLevel();
            points.Render();
            timeManager.Render();
        }

        /// <summary>
        /// Resets the game state by re-initializing it.
        /// </summary>
        public void ResetState()
        {
            InitializeGameState();
        }

        /// <summary>
        /// Updates the game state by updating the time and level.
        /// </summary>
        public void UpdateState()
        {
            timeManager.Update();
            levelManager.UpdateLevel();
        }
    }
}

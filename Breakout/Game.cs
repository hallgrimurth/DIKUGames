using System;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using Breakout.BreakoutStates;

namespace Breakout
{
    /// <summary>
    /// Represents the main game class for the Breakout game.
    /// </summary>
    public class Game : DIKUGame, IGameEventProcessor
    {
        private StateMachine stateMachine;
        private Timer timer;

        /// <summary>
        /// Constructs a new instance of the Game class.
        /// </summary>
        /// <param name="windowArgs">The window arguments for the game.</param>
        public Game(WindowArgs windowArgs) : base(windowArgs)
        {
            stateMachine = new StateMachine();
            timer = new Timer();

            InitializeEventBus();
            window.SetKeyEventHandler(stateMachine.ActiveState.HandleKeyEvent);
        }

        /// <summary>
        /// Initializes the event bus by subscribing to necessary events.
        /// </summary>
        public void InitializeEventBus()
        {
            // Subscribe to window event and game state event
            BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        }

        /// <summary>
        /// Processes the incoming game event.
        /// </summary>
        /// <param name="gameEvent">The game event to process.</param>
        public void ProcessEvent(GameEvent gameEvent)
        {
            switch (gameEvent.EventType)
            {
                case GameEventType.WindowEvent:
                    // Close the game window
                    window.CloseWindow();
                    break;
                case GameEventType.GameStateEvent:
                    // Process the game state event and update the key event handler
                    stateMachine.ProcessEvent(gameEvent);
                    window.SetKeyEventHandler(stateMachine.ActiveState.HandleKeyEvent);
                    break;
            }
        }

        /// <summary>
        /// Renders the active game state.
        /// </summary>
        public override void Render()
        {
            window.Clear();
            stateMachine.ActiveState.RenderState();
        }

        /// <summary>
        /// Updates the active game state and processes events.
        /// </summary>
        public override void Update()
        {
            stateMachine.ActiveState.UpdateState();
            window.PollEvents();
            BreakoutBus.GetBus().ProcessEventsSequentially();
        }
    }
}

using DIKUArcade.Events;
using DIKUArcade.State;
using System.Collections.Generic;

namespace Breakout.BreakoutStates
{
    /// <summary>
    /// The StateMachine class manages the different states of the Breakout game.
    /// </summary>
    public class StateMachine : IGameEventProcessor
    {
        /// <summary>
        /// The currently active state.
        /// </summary>
        public IGameState ActiveState { get; private set; }

        private Dictionary<GameStateType, IGameState> StateMap;

        /// <summary>
        /// Constructs a StateMachine instance.
        /// </summary>
        public StateMachine()
        {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            StateMap = new Dictionary<GameStateType, IGameState>
            {
                { GameStateType.GameRunning, GameRunning.GetInstance() },
                { GameStateType.MainMenu, MainMenu.GetInstance() },
                { GameStateType.GamePaused, GamePaused.GetInstance() },
                { GameStateType.GameOver, GameOver.GetInstance() }
            };

            ActiveState = StateMap[GameStateType.MainMenu];
        }

        /// <summary>
        /// Switches to a new state.
        /// </summary>
        /// <param name="stateType">The type of the new state.</param>
        public void SwitchState(GameStateType stateType)
        {
            if (StateMap.ContainsKey(stateType))
            {
                ActiveState = StateMap[stateType];
            }
        }

        /// <summary>
        /// Processes game events.
        /// </summary>
        /// <param name="gameEvent">The game event to process.</param>
        public void ProcessEvent(GameEvent gameEvent)
        {
            if (gameEvent.EventType == GameEventType.GameStateEvent)
            {
                switch (gameEvent.Message)
                {
                    case "CHANGE_STATE":
                        SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                        break;
                }
            }
        }
    }
}
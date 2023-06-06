using DIKUArcade.Events;
using DIKUArcade.Timers;
using Breakout.BreakoutStates;
using System;

namespace Breakout
{
    /// <summary>
    /// The Timer class handles the game timer in the Breakout game.
    /// </summary>
    public class Timer : IGameEventProcessor
    {
        /// <summary>
        /// Constructs a Timer instance.
        /// </summary>
        public Timer()
        {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            StaticTimer.RestartTimer();
        }

        /// <summary>
        /// Processes game events.
        /// </summary>
        /// <param name="gameEvent">The game event to process.</param>
        public void ProcessEvent(GameEvent gameEvent)
        {
            switch (gameEvent.Message)
            {
                case "CHANGE_STATE":
                    if ((String)gameEvent.StringArg2 == "RESUME")
                    {
                        StaticTimer.ResumeTimer();
                    }
                    if ((String)gameEvent.StringArg2 == "PAUSE")
                    {
                        StaticTimer.PauseTimer();
                    }
                    break;
            }
        }
    }
}

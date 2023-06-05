using DIKUArcade.Events;
using DIKUArcade.Timers;
using Breakout.BreakoutStates;
using System;


namespace Breakout {
    public class Timer : IGameEventProcessor {
        public Timer() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            StaticTimer.RestartTimer();
        }
        public void ProcessEvent(GameEvent gameEvent) {

            switch (gameEvent.Message) {
                case "CHANGE_STATE":
                    
                    if ((String) gameEvent.StringArg2 == "RESUME") {
                        StaticTimer.ResumeTimer();
                    }
                    if ((String) gameEvent.StringArg2 == "PAUSE") {
                        StaticTimer.PauseTimer();
                    }
                    break;
                
                
            }
        }
    }
}
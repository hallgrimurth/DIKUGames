using DIKUArcade.Events;
using DIKUArcade.State;
using System.Collections.Generic;

namespace Breakout.BreakoutStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            // ActiveState = MainMenu.GetInstance();
        }
        
        public void SwitchState(GameStateType stateType) {
            switch (stateType) {
            }
        }
        public void ProcessEvent(GameEvent gameEvent) {
        }   
    }
}
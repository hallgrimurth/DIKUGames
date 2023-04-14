using DIKUArcade.Events;
using DIKUArcade.State;
using System.Collections.Generic;

// namespace Breakout.BreakoutStates
namespace Breakout {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            // BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            // BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            // ActiveState = MainMenu.GetInstance();
        }
        
        // public void SwitchState(GameStateType stateType) 
        public void SwitchState() {
        //     switch (stateType) {
        //     }
        }
        // public void ProcessEvent(GameEvent gameEvent) {
        public void ProcessEvent() {
        }   
    }
}
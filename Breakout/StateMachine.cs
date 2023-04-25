using DIKUArcade.Events;
using DIKUArcade.State;
using System.Collections.Generic;

namespace Breakout.BreakoutStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = GameRunning.GetInstance();
        }
        
        public void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    GameRunning.GetInstance().ResetState();
                    break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                case GameStateType.GameOver:
                    ActiveState = GameOver.GetInstance();
                    break;
            }
        }
        
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent){
                switch (gameEvent.Message) {
                    case "CHANGE_STATE":
                        SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                        break;
                    }
            }
        }   
    }
}
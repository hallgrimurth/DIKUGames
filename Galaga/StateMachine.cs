using DIKUArcade.Events;
using DIKUArcade.State;

namespace Galaga.GalagaStates {
    public class StateMachine : IGameEventProcessor {
    public IGameState ActiveState { get; private set; }
    public StateMachine() {
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        ActiveState = MainMenu.GetInstance();
    }
    
    private void SwitchState(GameStateType stateType) {
        switch (stateType) {
            case GameStateType.GameRunning:
                ActiveState = GameRunning.GetInstance();
                break;
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            // case GameStateType.GamePaused:
            //     ActiveState = GamePaused.GetInstance();
            //     break;
            // case GameStateType.GameOver:
            //     ActiveState = GameOver.GetInstance();
            //     break;
            }
        }
        public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.Message) {
                case "CHANGE_STATE":
                    ActiveState = GameRunning.GetInstance();
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                    break;
                
                
            }   

        }
    }
}
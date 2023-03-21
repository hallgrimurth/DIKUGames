using DIKUArcade.Events;
using DIKUArcade.State;
namespace Galaga.GalagaStates {
    public class StateMachine : IGameEventProcessor<object> {
    public IGameState ActiveState { get; private set; }
    public StateMachine() {
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        ActiveState = MainMenu.GetInstance();
    }
    private void SwitchState(GameStateType stateType) {
        switch (stateType) {
            case GameRunning:
                ActiveState;   
                break;
            case GamePaused:
                ActiveState;
                break;
            case MainMenu:
                ActiveState;   
                break;
            case GameStateType.GameOver:
                ActiveState = GameOver.GetInstance();
                break;
            }
        }

    }
    void ProcessEvent(GameEvent gameEvent){
        if (gameEvent.EventType == GameStateType.GameRunning) {
                switch (gameEvent.Message) {
                    case "GAME RUNNING":
                        
                    break;
                }
            }

   }
}
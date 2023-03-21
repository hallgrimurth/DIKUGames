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
<<<<<<< HEAD
            case GameStateType.GameRunning:
                ActiveState = GameRunning.GetInstance();
                break;
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            case GameStateType.GamePaused:
                ActiveState = GamePaused.GetInstance();
=======
            case GameRunning:
                ActiveState;   
                break;
            case GamePaused:
                ActiveState;
                break;
            case MainMenu:
                ActiveState;   
>>>>>>> 3ed6ce990cb4c556eefbdbc2be0b07f648cc02a9
                break;
            case GameStateType.GameOver:
                ActiveState = GameOver.GetInstance();
                break;
            }
        }

    }
<<<<<<< HEAD
=======
    void ProcessEvent(GameEvent gameEvent){
        if (gameEvent.EventType == GameStateType.GameRunning) {
                switch (gameEvent.Message) {
                    case "GAME RUNNING":
                        
                    break;
                }
            }

   }
}
>>>>>>> 3ed6ce990cb4c556eefbdbc2be0b07f648cc02a9
}
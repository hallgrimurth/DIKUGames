enum GameStateType{
    GameRunning,
    GamePaused,
     MainMenu
}
    
public class StateTransformer{
    public static GameStateType TransformStringToState(string state){
        state =  "GAME RUNNING";
        throw(new(ArgumentException));
    }

    public static string TransformStateToString(GameStateType state){
        state = GameRunning;
        throw(new(ArgumentException));
    }
} 


 



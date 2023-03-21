using System;
enum GameStateType{
    GameRunning,
    GamePaused,
    GameOver,
    MainMenu
}
    
class StateTransformer{

    public static GameStateType TransformStringToState(string state){
        switch(state){
            case "GAME RUNNING":
                return GameStateType.GameRunning;
            case "GAME PAUSED":
                return GameStateType.GamePaused;
            case "GAME OVER":
                return GameStateType.GameOver;
            case "MAIN MENU":
                return GameStateType.MainMenu;
            default:
                throw(new ArgumentException());
        }
    }

    public static string TransformStateToString(GameStateType state){
        switch(state){
            case GameStateType.GameRunning:
                return "GAME RUNNING";
            case GameStateType.GamePaused:
                return "GAME PAUSED";
            case GameStateType.GameOver:
                return "GAME OVER";
            case GameStateType.MainMenu:
                return "MAIN MENU";
            default:
                throw(new ArgumentException());
        }
    }
    
} 


 



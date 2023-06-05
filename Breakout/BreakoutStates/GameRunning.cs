using DIKUArcade.State;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Physics;
using System;
using DIKUArcade.Timers;


namespace Breakout.BreakoutStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        //Entities
        private Player player;
        private Ball ball;
        private EntityContainer<Ball> ballCon;
        private LevelManager levelManager;

        // Strides and animations
        private IBaseImage ballImage;
        private Points points;
        private Text display = new Text("Time: ", new Vec2F(0.33f, -0.3f), new Vec2F(0.4f, 0.4f));
        private int elapsedTime;


        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }
        
        public void InitializeGameState(){
            GetLevels();
            SetPoints();
            SetTimers();
        }

         private void SetTimers() {
            display.SetColor(new Vec3I(255, 255, 255));
            display.SetFontSize(30);
            StaticTimer.RestartTimer();
            StaticTimer.PauseTimer();
        }
        private void UpdateTimers(){
            if (levelManager.CurrentLevel.MetaDict.ContainsKey('T') && levelManager.Start == false) {
                // Display the given time if the level has a time limit
                int givenTime = Int32.Parse(levelManager.CurrentLevel.MetaDict['T']);
                display.SetText("Time:" + (givenTime).ToString());
            } else if (levelManager.CurrentLevel.MetaDict.ContainsKey('T') && levelManager.Start) {
                // Update the time
                int givenTime = Int32.Parse(levelManager.CurrentLevel.MetaDict['T']);
                elapsedTime = (int)(StaticTimer.GetElapsedSeconds());
                display.SetText("Time:" + (givenTime - elapsedTime).ToString());
            } else {
                // display nothing
                display.SetText("");
            }
        }

        private void GetLevels() {
            levelManager = new LevelManager();
            var levelPaths = Directory.GetFiles(Path.Combine(Constants.MAIN_PATH, "Assets/Levels/"));
        }

        private void SetPoints() {
            //define points
            points = new Points(
                new Vec2F(0.65f, -0.3f), new Vec2F(0.4f, 0.4f));
        }

        public void KeyPress(KeyboardKey key){
            switch(key) {
                case KeyboardKey.A:
                    GameEvent MoveLeft = (new GameEvent{
                        EventType = GameEventType.MovementEvent,  To = player, 
                        Message = "MOVE_LEFT" });
                    BreakoutBus.GetBus().RegisterEvent(MoveLeft);
                   
                    break;
                case KeyboardKey.D:
                     GameEvent MoveRight = (new GameEvent{
                        EventType = GameEventType.MovementEvent,  To = player, 
                        Message = "MOVE_RIGHT" });
                    BreakoutBus.GetBus().RegisterEvent(MoveRight);
                        
                    break;     
                case KeyboardKey.C:
                    GameEvent closeWindowEvent = new GameEvent{
                        EventType = GameEventType.WindowEvent,
                        Message = "CLOSE_WINDOW"};
                    BreakoutBus.GetBus().RegisterEvent(closeWindowEvent);
                    break;                     
                case KeyboardKey.Left:
                    GameEvent NextLevel = (new GameEvent{
                        EventType = GameEventType.StatusEvent, To = levelManager,
                        Message = "PREV_LEVEL" });
                    BreakoutBus.GetBus().RegisterEvent(NextLevel);
                    // SetActors();
                    SetTimers();
                    break;
                case KeyboardKey.Right:
                    GameEvent PreviousLevel = (new GameEvent{
                        EventType = GameEventType.StatusEvent, To = levelManager,
                        Message = "NEXT_LEVEL" });
                    BreakoutBus.GetBus().RegisterEvent(PreviousLevel);
                    // SetActors();
                    SetTimers();
                    break;
                case KeyboardKey.Space:
                    GameEvent StartGame = (new GameEvent{
                        EventType = GameEventType.StatusEvent, To = levelManager,
                        Message = "START_GAME" });
                    BreakoutBus.GetBus().RegisterEvent(StartGame);
                    StaticTimer.ResumeTimer();
                    break;
            }

        }

        //invokes proper game event when specified key is released
        public void KeyRelease(KeyboardKey key){
            switch(key){
                case KeyboardKey.A:
                    GameEvent StopLeft = (new GameEvent{
                        EventType = GameEventType.MovementEvent,  To = player, 
                        Message = "STOP_LEFT" });
                    BreakoutBus.GetBus().RegisterEvent(StopLeft);
                    break;

                case KeyboardKey.D:
                    GameEvent StopRight = (new GameEvent{
                        EventType = GameEventType.MovementEvent,  To = player, 
                        Message = "STOP_RIGHT" });
                    BreakoutBus.GetBus().RegisterEvent(StopRight);
                    break;

                case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_PAUSED"
                        }
                    );
                    break;
            }
        }

        // handles key events
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
        
            switch(action){
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;

                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;        
            }
        }


        public void RenderState() {
            levelManager.RenderLevel();
            points.Render();
            display.RenderText();
        }

        public void ResetState(){ 
            InitializeGameState();
        }

        public void UpdateState(){

            levelManager.UpdateLevel();
            UpdateTimers();
        }
    
    }
}
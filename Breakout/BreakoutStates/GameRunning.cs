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


namespace Breakout.BreakoutStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        //Entities
        private Player player;

        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }
        
        public void InitializeGameState(){
            player =new Player("Assets/Images/player.png");
        }


        public void KeyPress(KeyboardKey key){
            switch(key) {
                case KeyboardKey.Left:
                    GameEvent MoveLeft = (new GameEvent{
                        EventType = GameEventType.MovementEvent,  To = player, 
                        Message = "MOVE_LEFT" });
                    BreakoutBus.GetBus().RegisterEvent(MoveLeft);
                   
                    break;
                case KeyboardKey.Right:
                     GameEvent MoveRight = (new GameEvent{
                        EventType = GameEventType.MovementEvent,  To = player, 
                        Message = "MOVE_RIGHT" });
                    BreakoutBus.GetBus().RegisterEvent(MoveRight);
                        
                    break;           
                    }
        }

        //invokes proper game event when specified key is released
        public void KeyRelease(KeyboardKey key){
            switch(key){
                case KeyboardKey.Left:
                    GameEvent StopLeft = (new GameEvent{
                        EventType = GameEventType.MovementEvent,  To = player, 
                        Message = "STOP_LEFT" });
                    BreakoutBus.GetBus().RegisterEvent(StopLeft);
                    break;

                case KeyboardKey.Right:
                    GameEvent StopRight = (new GameEvent{
                        EventType = GameEventType.MovementEvent,  To = player, 
                        Message = "STOP_RIGHT" });
                    BreakoutBus.GetBus().RegisterEvent(StopRight);
                    break;
                
                case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{
                            EventType = GameEventType.WindowEvent,
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
            player.Render();
        }

        public void ResetState(){ 
            InitializeGameState();
        }

        public void UpdateState(){
            player.Move();
        }
    }
}
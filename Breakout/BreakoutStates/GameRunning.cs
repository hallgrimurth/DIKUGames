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
        // private Value level;
        // private GameEventBus eventBus;


        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }
        
        public void InitializeGameState(){
            player =new Player();
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

                // case KeyboardKey.C:
                //     GameEvent closeWindowEvent = new GameEvent{
                //         EventType = GameEventType.WindowEvent,  Message = "CLOSE_WINDOW"};
                    
                //     break;               
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
                
                // case KeyboardKey.Space:
                //     Vec2F pos = player.GetPosition().Position;
                //     Vec2F ex = player.GetPosition().Extent;
                //     playerShotImage = new Image(Path.Combine
                //         ("Assets", "Images", "BulletRed2.png"));
                //     playerShots.AddEntity(new PlayerShot(
                //         new Vec2F(pos.X+(ex.X/2), pos.Y+(ex.Y/2)), playerShotImage));      
                //     break;
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
            // level.blocks.RenderEntities();
        }

        public void ResetState(){ 
            InitializeGameState();
        }

        public void UpdateState(){
            player.Move();
        }
    }
}
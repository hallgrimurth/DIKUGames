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
        private EntityContainer<Ball> ballCon;
        private LevelManager level;

        // Strides and animations
        private IBaseImage ballImage;
        //private IBaseImage playerImage;


        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }
        
        public void InitializeGameState(){
            player =new Player();
            // FIX: The stuff below could be refactored to another method
            level = new LevelManager();
            var levelPaths = Directory.GetFiles(Path.Combine(Constants.MAIN_PATH, "Assets/Levels/"));

            //write level to console
            // foreach (var level in levelPaths) {
            //     Console.WriteLine(level);
            // }


            // Ball
            ballCon =  new EntityContainer<Ball>();
            Vec2F pos = player.GetPosition().Position;
            Vec2F ex = player.GetPosition().Extent;
            ballImage = new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "ball.png"));
            // ballCon.AddEntity(new Ball(new Vec2F(0.475f, 0.25f), ballImage)); 
            ballCon.AddEntity(new Ball(new Vec2F(0.425f, 0.25f), ballImage));
        }

        private void IterateBall() {
            ballCon.Iterate(ball => {
                ball.Shape.Move(ball.Direction); // Using the Direction property from Ball.cs
                //ball.BallMove();

                if (ball.Shape.Position.Y + ball.Shape.Extent.Y >= 1.0f) {
                    ball.Direction = new Vec2F(ball.Direction.X, -ball.Direction.Y);
                } else if (ball.Shape.Position.X <= 0.0f) {
                    ball.Direction = new Vec2F(-ball.Direction.X, ball.Direction.Y);
                } else if (ball.Shape.Position.X + ball.Shape.Extent.X >= 1.0f) {
                    ball.Direction = new Vec2F(-ball.Direction.X, ball.Direction.Y);
                }

                if (ball.Shape.Position.Y + ball.Shape.Extent.Y < 0.0f) {
                    ball.DeleteEntity();
                } else {
                    BallBlockCollision(ball);
                    BallPlayerCollision(ball);
                }
            });
        }
        private void BallBlockCollision(Ball ball){
            level.blocks.Iterate(block => {
                if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape).Collision) {
                            // FIX: Ball should change direction upon collision (not be deleted - only to test whether it works)
                            // ball.DeleteEntity();
                            Console.WriteLine("Collision with block going {0}", CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape).CollisionDir);
                            // block.DecreaseHealth();
                            // if (block.Health == 0) {
                            //     block.DeleteEntity();

                            // }     
                }
            });
        }


        private void IterateBall2() {
            ballCon.Iterate(ball => {
                ball.Shape.Move(ball.Direction); 

                HandleWallCollision(ball);

                if (ball.Shape.Position.Y + ball.Shape.Extent.Y < 0.0f) {
                    ball.DeleteEntity();
                } else {
                    Console.WriteLine((ball.Shape.AsDynamicShape().Direction.X, ball.Shape.AsDynamicShape().Direction.Y));
                    // Console.WriteLine(ball.Direction.Y);
                    // level.blocks.Iterate(block => {
                    var CollisionData = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape.AsStationaryShape());
                    if (CollisionData.Collision) {
                        // FIX: Ball should change direction upon collision (not be deleted - only to test whether it works)
                        ball.DeleteEntity();
                        Console.WriteLine("Collision with player going {0}", CollisionData.CollisionDir);
                    };     
                }
            });
        }  

        private void BallPlayerCollision(Ball ball){
            if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape.AsDynamicShape()).Collision) {
                // FIX: Ball should change direction upon collision (not be deleted - only to test whether it works)
                ball.DeleteEntity();
            }

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
                        EventType = GameEventType.StatusEvent, To = level,
                        Message = "PREV_LEVEL" });
                    BreakoutBus.GetBus().RegisterEvent(NextLevel);
                    break;
                case KeyboardKey.Right:
                    GameEvent PreviousLevel = (new GameEvent{
                        EventType = GameEventType.StatusEvent, To = level,
                        Message = "NEXT_LEVEL" });
                    BreakoutBus.GetBus().RegisterEvent(PreviousLevel);
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
            level.blocks.RenderEntities();
            ballCon.RenderEntities();
            player.Render();
        }

        public void ResetState(){ 
            InitializeGameState();
        }

        public void UpdateState(){
            IterateBall();
            //IterateBall2();
            player.Move();
        }

        public void HandleWallCollision(Ball ball) {
            if (ball.Shape.Position.Y + ball.Shape.Extent.Y >= 1.0f) {
                    ball.Direction = new Vec2F(ball.Direction.X, -ball.Direction.Y);
                } else if (ball.Shape.Position.X <= 0.0f) {
                    ball.Direction = new Vec2F(-ball.Direction.X, ball.Direction.Y);
                } else if (ball.Shape.Position.X + ball.Shape.Extent.X >= 1.0f) {
                    ball.Direction = new Vec2F(-ball.Direction.X, ball.Direction.Y);
                }
        }
    }
}
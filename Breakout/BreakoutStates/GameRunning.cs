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
        private Value score;

        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }
        
        public void InitializeGameState(){
            player =new Player();
            GetLevels();
            SetBall();
            SetScore();
        }

        private void GetLevels() {
            level = new LevelManager();
            var levelPaths = Directory.GetFiles(Path.Combine(Constants.MAIN_PATH, "Assets/Levels/"));
            //write level to console
            // foreach (var level in levelPaths) {
            //     Console.WriteLine(level);
            // }
        }

        private void SetScore() {
            //define score
            score = new Value(
                new Vec2F(0.69f, -0.3f), new Vec2F(0.4f, 0.4f), 1);
        }

        private void SetBall() {
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

                HandleCollisions(ball);

                if (ball.Shape.Position.Y + ball.Shape.Extent.Y < 0.0f) {
                    ball.DeleteEntity();
                } else {
                    // BallBlockCollision(ball);
                    // BallPlayerCollision(ball);
                }
            });
        }

        //Method that handles all collisions
        private void HandleCollisions(Ball ball){
            WallCollision(ball);
            BallBlockCollision(ball);
            BallPlayerCollision(ball);
        }

        //Method that handles collision with walls
        private Vec2F WallCollision(Ball ball){

            var normal = new Vec2F(0.0f, 0.0f); 

            if (ball.Shape.Position.Y + ball.Shape.Extent.Y >= 1.0f) {
                Console.WriteLine(ball.Direction);
                normal = new Vec2F(0.0f, -1.0f);
                ball.Direction = VectorOperations.Reflection(ball.Direction, normal);
                Console.WriteLine(ball.Direction);

            } else if (ball.Shape.Position.X <= 0.0f) {
                normal = new Vec2F(1.0f, 0.0f);
                Console.WriteLine(ball.Direction);
                ball.Direction = VectorOperations.Reflection(ball.Direction, normal);
                Console.WriteLine(ball.Direction);

            } else if (ball.Shape.Position.X + ball.Shape.Extent.X >= 1.0f) {
                normal = new Vec2F(-1.0f, 0.0f);
                Console.WriteLine(ball.Direction);
                ball.Direction = VectorOperations.Reflection(ball.Direction, normal);
                Console.WriteLine(ball.Direction);

             } //else if (ball.Shape.Position.Y <= 0.0f) {
            //     normal = new Vec2F(0.0f, 1.0f);
            //     ball.Direction = VectorOperations.Reflection(ball.Direction, normal);
            // }
            return normal;
        }

        private void BallBlockCollision(Ball ball){
            level.blocks.Iterate(block => {
                var CollData = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape);
                var CollDir = ConverteDir(CollData.CollisionDir);
                var Coll = CollData.Collision;

                if (Coll) {

                            // FIX: Ball should change direction upon collision (not be deleted - only to test whether it works)
                            // ball.DeleteEntity();
                            // Console.WriteLine("Collision with block going {0}", CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape).CollisionDir);
                            GameEvent AddScore = (new GameEvent{
                                EventType = GameEventType.ScoreEvent, To = score,
                                Message = "ADD_SCORE",
                                StringArg1 = block.ToString()});
                            BreakoutBus.GetBus().RegisterEvent(AddScore);

                            block.DecreaseHealth();
                            if (block.Health == 0) {
                                block.DeleteEntity();

                            }     
                            ball.Direction = VectorOperations.Reflection(ball.Direction, CollDir);

                }
            });
        } 

        private void BallPlayerCollision(Ball ball){
            // Console.WriteLine("Collision with player going {0}", CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape.AsDynamicShape()).CollisionDir);
                if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape.AsDynamicShape()).Collision) {
                Console.WriteLine("Collision with player going {0}", CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape.AsDynamicShape()).CollisionDir);
                var normal = new Vec2F(0.0f, 1.0f);
                ball.Direction = VectorOperations.Reflection(ball.Direction, normal);
            }

        }

        private Vec2F ConverteDir(CollisionDirection CollDir){
            // var normal = new Vec2F(0.0f, 0.0f); 
            switch(CollDir){
                case CollisionDirection.CollisionDirDown:
                    return new Vec2F(1.0f, 0.0f);
                case CollisionDirection.CollisionDirLeft:
                    return new Vec2F(-1.0f, 0.0f);
                case CollisionDirection.CollisionDirRight:
                    return new Vec2F(0.0f, -1.0f);
                case CollisionDirection.CollisionDirUp:
                    return new Vec2F(0.0f, 1.0f);
            }
            return new Vec2F(0.0f, 0.0f);
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
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_PAUSED"});
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
            score.Render();
            player.Render();
        }

        public void ResetState(){ 
            InitializeGameState();
        }

        public void UpdateState(){
            IterateBall();
            player.Move();
        }
    } 
}

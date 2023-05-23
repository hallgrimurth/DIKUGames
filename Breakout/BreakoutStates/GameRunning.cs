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
        private Points score;

        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }
        
        public void InitializeGameState(){
            GetLevels();
            SetActors();
            SetScore();
        }
        public void SetActors(){
            player = new Player();
            SetBall();
        }

        private void GetLevels() {
            level = new LevelManager();
            var levelPaths = Directory.GetFiles(Path.Combine(Constants.MAIN_PATH, "Assets/Levels/"));
        }

        private void SetScore() {
            //define score
            score = new Points(
                new Vec2F(0.69f, -0.3f), new Vec2F(0.4f, 0.4f), 1);
        }

        // Initializes one or more balls 
        private void SetBall() {
            ballCon =  new EntityContainer<Ball>();
            Vec2F pos = new Vec2F(0.495f, 0.2f);
            ballImage = new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "ball.png"));
            ballCon.AddEntity(new Ball(pos, ballImage));
        }
        private void IterateBall() {
            ballCon.Iterate(ball => {
                HandleCollisions(ball);
                ball.Shape.Move(ball.Direction); // Using the Direction property from Ball.cs
                if (ball.Shape.Position.Y + ball.Shape.Extent.Y < 0.0f) {
                    ball.DeleteEntity();
                    GameEvent gameover = (new GameEvent{
                            EventType = GameEventType.WindowEvent,
                            Message = "CANGE_STATE",
                            StringArg1 = "GAME_OVER"});
                    BreakoutBus.GetBus().RegisterEvent(gameover);

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
        private void WallCollision(Ball ball){

            var normal = new Vec2F(0.0f, 0.0f); 

            if (ball.Shape.Position.Y + ball.Shape.Extent.Y >= 1.0f) {
                normal = new Vec2F(0.0f, -1.0f);
                ball.Direction = VectorOperations.Reflection(ball.Direction, normal);

            } else if (ball.Shape.Position.X <= 0.0f) {
                normal = new Vec2F(1.0f, 0.0f);
                ball.Direction = VectorOperations.Reflection(ball.Direction, normal);

            } else if (ball.Shape.Position.X + ball.Shape.Extent.X >= 1.0f) {
                normal = new Vec2F(-1.0f, 0.0f);
                ball.Direction = VectorOperations.Reflection(ball.Direction, normal);
            }
        }

        // Detects whether the ball collides with a block
        private void BallBlockCollision(Ball ball)
        {
            level.blocks.Iterate(block =>
            {
                var CollData = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape);
                // calculate the collision data of the right and top side of the block
                var CollDir = ConvertDir(CollData.CollisionDir);
                var Coll = CollData.Collision;
                var CollPos = CollData.DirectionFactor;

                if (Coll)
                {
                    //writeline block type
                    Console.WriteLine(block.ToString());

                    // Console.WriteLine("Collision detected + {0}", CollData.CollisionDir);

                    // Adjust the ball's position based on the collision direction
                    ball.Shape.Position += CollPos * ball.Direction;

                    // Determine the normal vector based on the collision direction
                    var normal = CollDir;

                    // Reflect the ball's direction using the normal vector
                    ball.Direction = VectorOperations.Reflection(ball.Direction, normal);

                    // Handle the block's health and deletion
                    block.DecreaseHealth();


                    // Register score event
                    GameEvent AddScore = new GameEvent
                    {
                        EventType = GameEventType.ScoreEvent,
                        To = score,
                        Message = "ADD_SCORE",
                        StringArg1 = block.ToString()
                    };
                    BreakoutBus.GetBus().RegisterEvent(AddScore);
                }
            });
        }
        
        private void BallPlayerCollision(Ball ball){
            var CollData = CollisionDetection.Aabb(player.Shape.AsDynamicShape(), ball.Shape);
            var CollDir = ConvertDir(CollData.CollisionDir);
            var Coll = CollData.Collision;
            var CollPos = CollData.DirectionFactor;

                if (Coll && CollData.CollisionDir == CollisionDirection.CollisionDirDown) {
                var normal = new Vec2F(0.0f, -1.0f);
                var speed = ball.Direction.Length();
                var collision_point = ball.Shape.Position * CollPos;
                var x_bounce_directions = get_x_bounce_directions(ball, collision_point);
                var velocity = x_bounce_directions * speed + 0.02f;

                ball.Direction = VectorOperations.Reflection(ball.Direction, normal);
                ball.Direction.X = x_bounce_directions;

            }
        }
    


        private float get_x_bounce_directions(Ball ball, Vec2F collision_point){
            var relativeIntersectX = (ball.Shape.Position.X - player.Shape.Position.X );
            
            var normalizedRelativeIntersectionX = relativeIntersectX / (player.Shape.Extent.X);
            return (((normalizedRelativeIntersectionX)- 0.5f)*2.0f) * 0.02f ;//* 0.01f; //range -1 to 1  
        }
        private Vec2F ConvertDir(CollisionDirection CollDir){
            switch(CollDir){
                case CollisionDirection.CollisionDirDown:
                    return new Vec2F(0.0f, 1.0f);
                case CollisionDirection.CollisionDirUp:
                    return new Vec2F(0.0f, -1.0f);
                case CollisionDirection.CollisionDirLeft:
                    return new Vec2F(-1.0f, 0.0f);
                case CollisionDirection.CollisionDirRight:
                    return new Vec2F(1.0f, 0.0f);
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
                    SetActors();
                    break;
                case KeyboardKey.Right:
                    GameEvent PreviousLevel = (new GameEvent{
                        EventType = GameEventType.StatusEvent, To = level,
                        Message = "NEXT_LEVEL" });
                    BreakoutBus.GetBus().RegisterEvent(PreviousLevel);
                    SetActors();
                    break;
                case KeyboardKey.Space:
                    GameEvent Shoot = (new GameEvent{
                        EventType = GameEventType.StatusEvent, To = level,
                        Message = "START_GAME" });
                    BreakoutBus.GetBus().RegisterEvent(Shoot);
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
            level.blocks.RenderEntities();
            ballCon.RenderEntities();
            score.Render();
            player.Render();
        }

        public void ResetState(){ 
            InitializeGameState();
        }

        public void UpdateState(){
            if (level.Start) IterateBall();
            player.Move();
        }
    }
}
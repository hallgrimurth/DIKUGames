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
        private LevelManager level;

        // Strides and animations
        private IBaseImage ballImage;
        private Points points;
        private Text display = new Text("Time: ", new Vec2F(0.33f, -0.3f), new Vec2F(0.4f, 0.4f));
        private double elapsedTime;

        private WidePowerUp widen;
        private BigPowerUp bigball;
        private LifePickUpPowerUp life;

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
            SetPoints();
            SetTimers();
            widen = new WidePowerUp(new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)), new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "WidePowerUp.png")));
            // widen.PowerUpEffect();
            bigball = new BigPowerUp(new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)), new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "BigPowerUp.png")));
            bigball.PowerUpEffect();
            life = new LifePickUpPowerUp(new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)), new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "LifePickUp.png")));
            life.PowerUpEffect();

        }
        public void SetActors(){
            player = new Player(1);
            SetBall();
        }

         private void SetTimers() {
            // if(level.MetaDict.ContainsKey('T')) {
            StaticTimer.RestartTimer();
            // }
        }
        // Isn't working yet
        private void UpdateTimers(){
            if (level.MetaDict.ContainsKey('T')) {
                int timer = Int32.Parse(level.MetaDict['T']);
                elapsedTime = (int)(StaticTimer.GetElapsedSeconds());
                display = new Text("Time:" + (timer - elapsedTime).ToString(), new Vec2F(0.33f, -0.3f), new Vec2F(0.4f, 0.4f));
                display.SetColor(new Vec3I(255, 255, 255));
                display.SetFontSize(30);
            }
        }

        private void GetLevels() {
            level = new LevelManager();
            var levelPaths = Directory.GetFiles(Path.Combine(Constants.MAIN_PATH, "Assets/Levels/"));
        }

        private void SetPoints() {
            //define points
            points = new Points(
                new Vec2F(0.69f, -0.3f), new Vec2F(0.4f, 0.4f), 1);
        }

        // Initializes one or more balls 
        private void SetBall() {
            ballCon =  new EntityContainer<Ball>();
            // Vec2F pos = new Vec2F(0.495f, 0.2f);
            Vec2F pos = new Vec2F((player.Shape.Position.X + player.Shape.Extent.X / 2), 0.2f);
            Vec2F extent = new Vec2F(0.03f, 0.03f);
            Vec2F dir = new Vec2F(0.01f, 0.01f);
            DynamicShape ballShape = new DynamicShape(pos, extent);
            ballImage = new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "ball.png"));
            ball = new Ball(ballShape, ballImage);
            ball.ChangeDirection(dir);
            ballCon.AddEntity(ball);
        }
        private void IteratePowerUps() {
            level.powerups.Iterate(powerup => {
                powerup.Move();
                powerup.PowerDownEffect();
                if (CollisionDetection.Aabb(player.Shape.AsDynamicShape(), powerup.Shape).Collision) {
                    powerup.DeleteEntity();
                    powerup.PowerUpEffect();
                }
            });
        }
        private void IterateBall() {
            ballCon.Iterate(ball => {
                ball.Move();
                HandleCollisions(ball);
                // ball.Move();
                if (ball.Shape.Position.Y < 0.01f) {
                    ball.DeleteEntity();
                    player.DecreaseLives();

                }
                
                

                if (points.PointsValue >= 3) {
    
                    GameEvent gamewon = (new GameEvent{
                            EventType = GameEventType.GameStateEvent, 
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_WON"});
                    BreakoutBus.GetBus().RegisterEvent(gamewon);
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
            Vec2F dir = ball.GetDirection();

            if (ball.Shape.Position.Y + ball.Shape.Extent.Y >= 0.99f) {
                normal = new Vec2F(0.0f, -1.0f);
                
                ball.ChangeDirection(VectorOperations.Reflection(dir, normal));
                
            } else if (ball.Shape.Position.X <= 0.01f) {
                normal = new Vec2F(1.0f, 0.0f);
               
                ball.ChangeDirection(VectorOperations.Reflection(dir, normal));

            } else if (ball.Shape.Position.X + ball.Shape.Extent.X >= 0.99f) {
                normal = new Vec2F(-1.0f, 0.0f);
                
                ball.ChangeDirection(VectorOperations.Reflection(dir, normal));
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

                if (Coll) {
                    GameEvent AddPoints = (new GameEvent{
                        EventType = GameEventType.ScoreEvent, To = points,
                        Message = "ADD_POINTS",
                        StringArg1 = block.ToString()});
                    BreakoutBus.GetBus().RegisterEvent(AddPoints);

                    // Determine the normal vector based on the collision direction
                    var normal = CollDir;
                    // Reflect the ball's direction using the normal vector
                    ball.ChangeDirection(VectorOperations.Reflection(ball.Shape.AsDynamicShape().Direction, normal));

                    // Handle the block's health and deletion
                    block.DecreaseHealth();


                    // Register score event
                    GameEvent AddScore = new GameEvent
                    {
                        EventType = GameEventType.ScoreEvent,
                        To = points,
                        Message = "ADD_SCORE",
                        StringArg1 = block.ToString()
                    };
                    BreakoutBus.GetBus().RegisterEvent(AddScore);
                }
            });
        }
        
        private void BallPlayerCollision(Ball ball){
            var CollData = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape);
            var CollDir = ConvertDir(CollData.CollisionDir);
            var Coll = CollData.Collision;
            var CollPos = CollData.DirectionFactor;

            if (Coll) {
                
                var normal = new Vec2F(0.0f, 1.0f);
                var x_bounce_directions = get_x_bounce_directions(ball);
                var targetVelocity = 0.01f;

                ball.ChangeDirection(VectorOperations.Reflection(ball.Shape.AsDynamicShape().Direction, normal));
                ball.Shape.AsDynamicShape().Direction.X = x_bounce_directions;

                var ySquared = Math.Pow(targetVelocity, 2) - Math.Abs(Math.Pow(x_bounce_directions, 2));
                ball.Shape.AsDynamicShape().Direction.Y = (float)Math.Sqrt(ySquared);

                // var speed = ball.Direction.Length();
                // Console.WriteLine("Ball velocity: " + speed);

            }
        }
    


        private float get_x_bounce_directions(Ball ball){
            var relativeIntersectX = (ball.Shape.Position.X - player.Shape.Position.X );
            
            var normalizedRelativeIntersectionX = relativeIntersectX / (player.Shape.Extent.X);
            var norm = (normalizedRelativeIntersectionX - 0.5f) * 2.0f; // range -1 to 1

            return norm * 0.01f; //to get a reasonable speed 
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
                    // StaticTimer.RestartTimer();
                    break;
                case KeyboardKey.Right:
                    GameEvent PreviousLevel = (new GameEvent{
                        EventType = GameEventType.StatusEvent, To = level,
                        Message = "NEXT_LEVEL" });
                    BreakoutBus.GetBus().RegisterEvent(PreviousLevel);
                    SetActors();
                    // StaticTimer.RestartTimer();
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

        

        // public void ProcessEvent(GameEvent gameEvent) {
        //     if (gameEvent.EventType == GameEventType.MovementEvent) {
        //         switch (gameEvent.Message) {
        //             case "DOUBLE_SIZE":
        //                 ballCon.Iterate(ball => {
        //                     ball.Shape.Extent.X = 0.1f;
        //                     ball.Shape.Extent.Y = 0.1f;
        //                 });
        //                 break;
        //             case "NORMAL_SIZE":
        //                 ballCon.Iterate(ball => {
        //                     ball.Shape.Extent.X = 0.03f;
        //                     ball.Shape.Extent.Y = 0.03f;
        //                 });
        //                 break;
        //         }
        //     }
        // }

        public void RenderState() {
            level.blocks.RenderEntities();
            level.powerups.RenderEntities();
            ballCon.RenderEntities();
            points.Render();
            player.Render();
            display.RenderText();
        }

        public void ResetState(){ 
            InitializeGameState();
        }

        public void UpdateState(){
            // widen.PowerDownEffect();        
            if (level.Start) {
                IterateBall();
                IteratePowerUps();
            }
            player.Move();
            UpdateTimers();
        }
    }
}
using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using DIKUArcade.Physics;

namespace Breakout{   
    public class Player :Entity, IGameEventProcessor, ICollidable{

        // private Entity player;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        const float MOVEMENT_SPEED = 0.02f;
        private Image playerStride;
        private Text display;
        private double startTime;
        private static Vec2F playerPos = new Vec2F(0.4f, 0.1f);
        private static Vec2F playerExtent = new Vec2F(0.2f, 0.03f);
        private int livesCount = 3;
        private double endTime;

        public int LivesCount {
            get { return livesCount; }
            // set { livesCount = value; }
        }
        private static Vec2F livesPos = new Vec2F(0.06f, -0.3f);
        private static Vec2F livesExtent = new Vec2F(0.4f, 0.4f);

        public Player(): base(
            new DynamicShape(playerPos, playerExtent),
            new Image(Path.Combine(
                Constants.MAIN_PATH, "Assets", "Images", "player.png"))
        ) { //int livesCount

            // playerStride = new Image(Path.Combine(
            //     Constants.MAIN_PATH, "Assets", "Images", "player.png"));
            // this.Shape = new DynamicShape(playerPos, playerExtent);
            // player = new Entity(this.Shape, playerStride);
            BreakoutBus.GetBus().Subscribe(GameEventType.MovementEvent, this);

            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.StatusEvent, 
                Message = "SUBSCRIBE_COLLISION_EVENT",
                StringArg1 = "POWERUP",
                From = this
                });

            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.StatusEvent, 
                Message = "SUBSCRIBE_COLLISION_EVENT",
                StringArg1 = "BALL",
                From = this
                });
        }

        public void SetLives() {
            // Setting the lives of the player
            display = new Text("Lives:" + livesCount.ToString(), livesPos, livesExtent);
            display.SetColor(new Vec3I(255, 255, 0));
            display.SetFontSize(30);
        }


        public void DecreaseLives() {
            this.livesCount--;
            if (livesCount <= 0) {
                GameEvent gameover = (new GameEvent{
                        EventType = GameEventType.GameStateEvent, 
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_OVER"});
                BreakoutBus.GetBus().RegisterEvent(gameover);
                Console.WriteLine("GAME OVER");
            }
        }

        public void IncreaseLives() {
            this.livesCount++;
        }

        public void Collision(CollisionData collisionData, ICollidable other) {
            if (collisionData.Collision) {
            }
        }

        public void Move() {
            Shape.Move();

            if(Shape.Position.X  < 0.0f) {
                Shape.Position.X = 0.0f;
            } else if(Shape.Position.X > 1.0f - Shape.Extent.X) {
                Shape.Position.X = 1.0f - Shape.Extent.X;
            }
        }

        public void SetMoveLeft(bool val) {
            if(val) {
                moveLeft = -MOVEMENT_SPEED;
            } else {
                moveLeft = 0.0f;
            }
            UpdateDirection();
        }

        public void SetMoveRight(bool val) {
            if(val) {
                moveRight = MOVEMENT_SPEED;
            } else {
                moveRight = 0.0f;
            }
            UpdateDirection();
        }

        public void UpdateDirection() {
            this.Shape.AsDynamicShape().Direction.X = moveLeft + moveRight;
        }
        public Shape GetPosition() {
            return Shape;
        }


        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.MovementEvent) {
                switch (gameEvent.Message) {
                    case "MOVE_LEFT":
                        SetMoveLeft(true);
                        break;
                    case "MOVE_RIGHT":
                        SetMoveRight(true);
                        break;
                    case "STOP_LEFT":
                        SetMoveLeft(false);
                        break;
                    case "STOP_RIGHT":
                        SetMoveRight(false);
                        break;
                    case "Widen":
                        Console.WriteLine("Widen message received");
                        Shape.Extent.X = 0.4f;
                        break;
                    case "Narrow":
                        Shape.Extent.X = 0.2f;
                        break;
                }
            } else if (gameEvent.EventType == GameEventType.PlayerEvent) {
                // Console.WriteLine("hi");
                switch (gameEvent.Message) {
                    case "INCREASE_HEALTH":
                        IncreaseLives();
                        break;
                    case "DECREASE_HEALTH":
                        DecreaseLives();
                        break;
                    case "Widen":
                        Shape.Extent.X = 0.4f;
                        break;
                    case "Narrow":
                        Shape.Extent.X = 0.2f;
                        break;
                }
            
            }
        }

        public void Update(){
            Move();
            SetLives();
        }

        public void Render() {
            this.RenderEntity();

        }
    }
}
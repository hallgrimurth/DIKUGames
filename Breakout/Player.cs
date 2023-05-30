using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout{   
    public class Player : IGameEventProcessor{

        private Entity player;

        private DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        const float MOVEMENT_SPEED = 0.02f;
        private Image playerStride;
        private int playerLives;
        private Text display;
        public DynamicShape Shape {
            get { return shape; }
        }
        public int LivesCount {
            get { return livesCount; }
            set { livesCount = value; }
        }
        private static Vec2F livesPos = new Vec2F(0.06f, -0.3f);
        private static Vec2F livesExtent = new Vec2F(0.4f, 0.4f);
        private static Vec2F playerPos = new Vec2F(0.4f, 0.1f);
        private static Vec2F playerExtent = new Vec2F(0.2f, 0.03f);
        private int livesCount;

        public Player(int livesCount) {

            playerStride = new Image(Path.Combine(
                Constants.MAIN_PATH, "Assets", "Images", "player.png"));
            this.shape = new DynamicShape(playerPos, playerExtent);
            player = new Entity(this.shape, playerStride);
            BreakoutBus.GetBus().Subscribe(GameEventType.MovementEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);

            SetLives(livesCount);
        }

        public void SetLives(int livesCount) {
            // Setting the lives of the player
            playerLives = livesCount;
            display = new Text("Lives:" + playerLives.ToString(), livesPos, livesExtent);
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

        public void Move() {
            shape.Move();

            if(shape.Position.X  < 0.0f) {
                shape.Position.X = 0.0f;
            } else if(shape.Position.X > 1.0f - shape.Extent.X) {
                shape.Position.X = 1.0f - shape.Extent.X;
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
            shape.Direction.X = moveLeft + moveRight;
        }
        public Shape GetPosition() {
            return shape;
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
                }
            } else if (gameEvent.EventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                    case "INCREASE_HEALTH":
                        IncreaseLives();
                        break;
                    case "Widen":
                        if (shape.Extent.X < 0.9f) {
                            shape.Extent.X += 0.1f;
                            shape.Position.X -= 0.05f;
                        }
                        break;
                    case "Narrow":
                        if (shape.Extent.X > 0.1f) {
                            shape.Extent.X -= 0.1f;
                            shape.Position.X += 0.05f;
                        }
                        break;
                }
            }
        }

        public void Render() {
            player.RenderEntity();
            // Render the lives of the player
            display.RenderText();
        }
    }
}
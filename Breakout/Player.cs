using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using DIKUArcade.Physics;

namespace Breakout
{
    /// <summary>
    /// Represents a player in the Breakout game.
    /// </summary>
    public class Player : Entity, IGameEventProcessor, ICollidable
    {
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private float MOVEMENT_SPEED = 0.02f;
        private Text display;
        private static readonly Vec2F playerPos = new Vec2F(0.4f, 0.1f);
        private static readonly Vec2F playerExtent = new Vec2F(0.2f, 0.03f);
        private int livesCount = 3;

        /// <summary>
        /// Gets the number of lives remaining for the player.
        /// </summary>
        public int LivesCount => livesCount;

        private static readonly Vec2F livesPos = new Vec2F(0.06f, -0.3f);
        private static readonly Vec2F livesExtent = new Vec2F(0.4f, 0.4f);

        /// <summary>
        /// Constructs a new Player instance.
        /// </summary>
        public Player() : base(new DynamicShape(playerPos, playerExtent),
            new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "player.png")))
        {
            BreakoutBus.GetBus().Subscribe(GameEventType.MovementEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);

            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.StatusEvent,
                Message = "SUBSCRIBE_COLLISION_EVENT",
                StringArg1 = "POWERUP",
                From = this
            });

            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.StatusEvent,
                Message = "SUBSCRIBE_COLLISION_EVENT",
                StringArg1 = "HAZARD",
                From = this
            });

            BreakoutBus.GetBus().RegisterEvent(new GameEvent
            {
                EventType = GameEventType.StatusEvent,
                Message = "SUBSCRIBE_COLLISION_EVENT",
                StringArg1 = "BALL",
                From = this
            });

            SetLives();
        }

        /// <summary>
        /// Sets up the player's display for the number of lives remaining.
        /// </summary>
        public void SetLives()
        {   livesCount = 3;
            display = new Text("Lives: " + livesCount.ToString(), livesPos, livesExtent);
            display.SetColor(new Vec3I(255, 255, 0));
            display.SetFontSize(30);
        }

        /// <summary>
        /// Decreases the player's lives count by one.
        /// </summary>
        public void DecreaseLives()
        {
            livesCount--;
            display.SetText("Lives: " + livesCount.ToString());
            if (livesCount <= 0)
            {
                GameEvent gameover = new GameEvent
                {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_OVER"
                };
                BreakoutBus.GetBus().RegisterEvent(gameover);
                Console.WriteLine("GAME OVER");
            }
        }

        /// <summary>
        /// Increases the player's lives count by one.
        /// </summary>
        public void IncreaseLives()
        {   
            livesCount++;
            display.SetText("Lives: " + livesCount.ToString());


        }

        /// <summary>
        /// Handles the collision between the player and another collidable object.
        /// </summary>
        /// <param name="collisionData">Collision data.</param>
        /// <param name="other">The other collidable object.</param>
        public void Collision(CollisionData collisionData, ICollidable other)
        {
            if (collisionData.Collision)
            {
                // Handle collision logic
            }
        }

        /// <summary>
        /// Moves the player based on the current input state.
        /// </summary>
        public void Move()
        {
            Shape.Move();

            if (Shape.Position.X < 0.0f)
            {
                Shape.Position.X = 0.0f;
            }
            else if (Shape.Position.X > 1.0f - Shape.Extent.X)
            {
                Shape.Position.X = 1.0f - Shape.Extent.X;
            }
        }

        /// <summary>
        /// Sets the flag to move the player left.
        /// </summary>
        /// <param name="val">The flag value.</param>
        public void SetMoveLeft(bool val)
        {
            moveLeft = val ? -MOVEMENT_SPEED : 0.0f;
            UpdateDirection();
        }

        /// <summary>
        /// Sets the flag to move the player right.
        /// </summary>
        /// <param name="val">The flag value.</param>
        public void SetMoveRight(bool val)
        {
            moveRight = val ? MOVEMENT_SPEED : 0.0f;
            UpdateDirection();
        }

        /// <summary>
        /// Updates the movement direction of the player.
        /// </summary>
        public void UpdateDirection()
        {
            Shape.AsDynamicShape().Direction.X = moveLeft + moveRight;
        }

        /// <summary>
        /// Gets the position of the player.
        /// </summary>
        /// <returns>The shape representing the position.</returns>
        public Shape GetPosition()
        {
            return Shape;
        }

        /// <summary>
        /// Processes game events relevant to the player.
        /// </summary>
        /// <param name="gameEvent">The game event to process.</param>
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
                    case "SLOW_MOVEMENT":
                        MOVEMENT_SPEED = 0.01f;
                        break;
                    case "NORMAL_MOVEMENT":
                        MOVEMENT_SPEED = 0.02f;
                        break;
                }
            }
            else if (gameEvent.EventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                    case "INCREASE_HEALTH":
                        IncreaseLives();
                        break;
                    case "DECREASE_HEALTH":
                        DecreaseLives();
                        break;
                    case "WIDE_PADDLE":
                        // Console.WriteLine("Widen message received");
                        if (Shape.AsDynamicShape().Extent.X <= 0.8f) {
                            Shape.AsDynamicShape().Extent.X += 0.1f;
                            Shape.AsDynamicShape().Position.X -= 0.05f;
                        }
                        break;
                    case "NARROW_PADDLE":
                        // Console.WriteLine("Narrow message received");
                        if (Shape.AsDynamicShape().Extent.X >= 0.2f) {
                            Shape.AsDynamicShape().Extent.X -= 0.1f;
                            Shape.AsDynamicShape().Position.X += 0.05f;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Updates the player's state.
        /// </summary>
        public void Update()
        {
            Move();
        }

        /// <summary>
        /// Renders the player.
        /// </summary>
        public void Render()
        {
            RenderEntity();
            display.RenderText();
        }
    }
}

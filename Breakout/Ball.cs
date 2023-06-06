using System;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using Breakout;

namespace Breakout {
    /// <summary>
    /// Represents a ball in the Breakout game.
    /// </summary>
    public class Ball : Entity, IGameEventProcessor, ICollidable {
        private static Vec2F extent = new Vec2F(0.030f, 0.030f);
        private static Vec2F direction = new Vec2F(0.01f, 0.005f);

        /// <summary>
        /// The extent (size) of the ball.
        /// </summary>
        public static Vec2F Extent {
            get { return extent; }
        }

        /// <summary>
        /// The direction of the ball.
        /// </summary>
        public static Vec2F Direction {
            get { return direction; }
        }

        public Ball(DynamicShape shape) : base(shape, new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "ball.png"))) {
            BreakoutBus.GetBus().Subscribe(GameEventType.MovementEvent, this);
        }

        /// <summary>
        /// Moves the ball.
        /// </summary>
        public void Move() {
            if (Shape.Position.X > 0.0f && Shape.Position.X + Shape.Extent.X < 1.0f
                && Shape.Position.Y > 0.0f && Shape.Position.Y + Shape.Extent.Y < 1.0f) {
                base.Shape.Move();
            }
        }

        /// <summary>
        /// Gets the current direction of the ball.
        /// </summary>
        /// <returns>The direction of the ball.</returns>
        public Vec2F GetDirection() {
            return base.Shape.AsDynamicShape().Direction;
        }

        /// <summary>
        /// Changes the direction of the ball.
        /// </summary>
        /// <param name="newDir">The new direction of the ball.</param>
        public void ChangeDirection(Vec2F newDir) {
            base.Shape.AsDynamicShape().ChangeDirection(newDir);
        }

        /// <summary>
        /// Processes game events related to ball movement.
        /// </summary>
        /// <param name="gameEvent">The game event to process.</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.MovementEvent) {
                switch (gameEvent.Message) {
                    case "DOUBLE_SIZE":
                        Shape.Extent.X = 0.06f;
                        Shape.Extent.Y = 0.06f;
                        break;
                    case "NORMAL_SIZE":
                        Shape.Extent.X = 0.03f;
                        Shape.Extent.Y = 0.03f;
                        break;
                }
            }
        }

        /// <summary>
        /// Handles collisions with other entities.
        /// </summary>
        /// <param name="data">Collision data.</param>
        /// <param name="collideWith">The entity collided with.</param>
        public void Collision(CollisionData data, ICollidable collideWith) {
            WallCollision();
            if (data.Collision) {
                if (collideWith is Player) {
                    BallPlayerCollision(collideWith.Shape.Position.X, collideWith.Shape.Extent.X);
                } else {
                    BallBlockCollision(data.CollisionDir);
                }
            }
        }

        /// <summary>
        /// Handles block collisions by changing the direction of the ball.
        /// </summary>
        /// <param name="collisionDir">The collision direction.</param>
        private void BallBlockCollision(CollisionDirection collisionDir) {
            var collDir = ConvertDir(collisionDir);
            this.ChangeDirection(VectorOperations.Reflection(this.Shape.AsDynamicShape().Direction, collDir));
        }

        /// <summary>
        /// Converts a collision direction to a vector direction.
        /// </summary>
        /// <param name="collisionDir">The collision direction.</param>
        /// <returns>The vector direction.</returns>
        private Vec2F ConvertDir(CollisionDirection collisionDir) {
            switch (collisionDir) {
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

        /// <summary>
        /// Handles collisions with the player entity.
        /// </summary>
        /// <param name="playerPosX">The player's X position.</param>
        /// <param name="playerExtentX">The player's extent on the X-axis.</param>
        private void BallPlayerCollision(float playerPosX, float playerExtentX) {
            var normal = new Vec2F(0.0f, 1.0f);
            var xBounceDirections = GetXBounceDirections(playerPosX, playerExtentX);
            var targetVelocity = 0.01f;

            this.ChangeDirection(VectorOperations.Reflection(this.Shape.AsDynamicShape().Direction, normal));
            this.Shape.AsDynamicShape().Direction.X = xBounceDirections;

            var ySquared = Math.Pow(targetVelocity, 2) - Math.Abs(Math.Pow(xBounceDirections, 2));
            this.Shape.AsDynamicShape().Direction.Y = (float)Math.Sqrt(ySquared);
        }

        /// <summary>
        /// Calculates the X bounce direction based on the player's position and extent.
        /// </summary>
        /// <param name="playerPosX">The player's X position.</param>
        /// <param name="playerExtentX">The player's extent on the X-axis.</param>
        /// <returns>The X bounce direction.</returns>
        private float GetXBounceDirections(float playerPosX, float playerExtentX) {
            var relativeIntersectX = (this.Shape.Position.X - playerPosX);
            var normalizedRelativeIntersectionX = relativeIntersectX / (playerExtentX);
            var norm = (normalizedRelativeIntersectionX - 0.5f) * 2.0f; // range -1 to 1

            return norm * 0.01f; // to get a reasonable speed
        }

        /// <summary>
        /// Handles wall collisions by changing the direction of the ball.
        /// </summary>
        private void WallCollision() {
            var normal = new Vec2F(0.0f, 0.0f);
            Vec2F dir = this.GetDirection();

            if (this.Shape.Position.Y + this.Shape.Extent.Y >= 0.98f) {
                normal = new Vec2F(0.0f, -1.0f);
                this.ChangeDirection(VectorOperations.Reflection(dir, normal));
            } else if (this.Shape.Position.X <= 0.02f) {
                normal = new Vec2F(1.0f, 0.0f);
                this.ChangeDirection(VectorOperations.Reflection(dir, normal));
            } else if (this.Shape.Position.X + this.Shape.Extent.X >= 0.98f) {
                normal = new Vec2F(-1.0f, 0.0f);
                this.ChangeDirection(VectorOperations.Reflection(dir, normal));
            } else if (this.Shape.Position.Y + this.Shape.Extent.Y <= 0.1f) {
                this.Shape.Position = new Vec2F(0.5f, 0.2f);
                ChangeDirection(new Vec2F(0.1f * 10e-6f, 0.01f));
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "DECREASE_HEALTH",
                    From = this
                });
            }
        }

        /// <summary>
        /// Sends a collision data event to check for collisions.
        /// </summary>
        private void SendCollisionData() {
            if (this.IsDeleted()) {
                return;
            }
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.StatusEvent,
                Message = "CHECK_COLLISION_EVENT",
                From = this,
                StringArg1 = "BALL"
            });
        }

        /// <summary>
        /// Updates the ball's movement and handles collisions.
        /// </summary>
        public void Update() {
            Move();
            WallCollision();
            SendCollisionData();
        }

        /// <summary>
        /// Renders the ball entity.
        /// </summary>
        public void Render() {
            base.RenderEntity();
        }
    }
}

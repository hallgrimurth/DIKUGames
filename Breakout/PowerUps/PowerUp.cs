using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Events;
using DIKUArcade.Timers;


namespace Breakout {
    /// <summary>
    /// Represents an abstract base class for power-up entities.
    /// </summary>
    public abstract class PowerUp : Entity, ICollidable {
        private static Vec2F direction = new Vec2F(0.0f, -0.01f);
        private float startTime;
        private bool activated = false;


        /// <summary>
        /// Constructs a PowerUp object.
        /// </summary>
        /// <param name="shape">The shape of the power-up.</param>
        /// <param name="image">The image associated with the power-up.</param>
        public PowerUp(DynamicShape shape, IBaseImage image) : base(shape, image) {
            ChangeDirection(direction);
        }

        /// <summary>
        /// Moves the power-up.
        /// </summary>
        private void Move() {
            base.Shape.AsDynamicShape().Move();
        }

        /// <summary>
        /// Gets the direction of the power-up.
        /// </summary>
        /// <returns>The direction vector of the power-up.</returns>
        public Vec2F GetDirection() {
            return base.Shape.AsDynamicShape().Direction;
        }

        /// <summary>
        /// Changes the direction of the power-up.
        /// </summary>
        /// <param name="newDir">The new direction vector of the power-up.</param>
        public void ChangeDirection(Vec2F newDir) {
            base.Shape.AsDynamicShape().ChangeDirection(newDir);
        }

        /// <summary>
        /// Handles the collision with other collidable objects.
        /// </summary>
        /// <param name="collisionData">The collision data.</param>
        /// <param name="other">The other collidable object.</param>
        public void Collision(CollisionData collisionData, ICollidable other) {
            if (collisionData.Collision) {
                PowerUpEffect();
                Image = new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"));
                startTime = (int)StaticTimer.GetElapsedSeconds();

                PowerDownEffect();  
            }
        }

        /// <summary>
        /// Checks for collision with other entities.
        /// </summary>
        private void CheckCollision() {
            if (this.IsDeleted()) {
                return;
            }
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.StatusEvent,
                Message = "CHECK_COLLISION_EVENT",
                From = this,
                StringArg1 = "POWERUP"
            });
        }

        /// <summary>
        /// The effect of the power-up when it is activated.
        /// </summary>
        public abstract void PowerUpEffect();

        /// <summary>
        /// The effect of the power-up when it is deactivated.
        /// </summary>
        public abstract void PowerDownEffect();

        /// <summary>
        /// Updates the power-up.
        /// </summary>
        public void Update() {
            if (activated) {
                Console.WriteLine("Activated");
                if (StaticTimer.GetElapsedSeconds() - startTime > 3.0f) {
                    PowerDownEffect();
                    DeleteEntity();
                }
                return;
            }
            CheckCollision();
            Move();
        }
    }
}

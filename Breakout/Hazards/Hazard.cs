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
    /// Represents an abstract hazard in the Breakout game.
    /// </summary>
    public abstract class Hazard : Entity, ICollidable {
        private static Vec2F direction = new Vec2F(0.0f, -0.01f);
        private float startTime;
        private bool activated = false;

        /// <summary>
        /// Constructs a new instance of the Hazard class.
        /// </summary>
        /// <param name="shape">The dynamic shape of the hazard.</param>
        /// <param name="image">The image of the hazard.</param>
        public Hazard(DynamicShape shape, IBaseImage image)
            : base(shape, image) {
            ChangeDirection(direction);
        }

        /// <summary>
        /// Moves the hazard.
        /// </summary>
        private void Move() {
            base.Shape.AsDynamicShape().Move();
        }

        /// <summary>
        /// Gets the direction of the hazard.
        /// </summary>
        /// <returns>The direction vector.</returns>
        public Vec2F GetDirection() {
            return base.Shape.AsDynamicShape().Direction;
        }

        /// <summary>
        /// Changes the direction of the hazard.
        /// </summary>
        /// <param name="newDir">The new direction vector.</param>
        public void ChangeDirection(Vec2F newDir) {
            base.Shape.AsDynamicShape().ChangeDirection(newDir);
        }

        /// <summary>
        /// Handles collision with other entities.
        /// </summary>
        /// <param name="collisionData">The collision data.</param>
        /// <param name="other">The colliding entity.</param>
        public void Collision(CollisionData collisionData, ICollidable other) {
            if (collisionData.Collision) {
                HazardUpEffect();
                Image = new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"));
                startTime = (int)StaticTimer.GetElapsedSeconds();
                activated = true;
            }
        }

        /// <summary>
        /// Checks for collision with other entities.
        /// </summary>
        private void CheckCollision() {
            if (IsDeleted()) {
                return;
            }
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.StatusEvent,
                Message = "CHECK_COLLISION_EVENT",
                From = this,
                StringArg1 = "HAZARD"
            });
        }

        /// <summary>
        /// Applies the effect of the hazard.
        /// </summary>
        public abstract void HazardUpEffect();

        /// <summary>
        /// Reverts the effect of the hazard.
        /// </summary>
        public abstract void HazardDownEffect();

        /// <summary>
        /// Updates the hazard.
        /// </summary>
        public void Update() {
            if (activated) {
                if (StaticTimer.GetElapsedSeconds() - startTime > 5.0f) {
                    HazardDownEffect();
                    DeleteEntity();
                }
                return;
            }
            CheckCollision();
            Move();
        }
    }
}

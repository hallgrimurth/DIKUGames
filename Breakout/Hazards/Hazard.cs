using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
    /// <summary>
    /// Represents a hazard in the game.
    /// </summary>
    public abstract class Hazard : Entity {
        private static Vec2F direction = new Vec2F(0.0f, 0.0f);

        /// <summary>
        /// The direction of the hazard.
        /// </summary>
        public static Vec2F Direction {
            get { return direction; }
        }

        /// <summary>
        /// Constructs a Hazard object.
        /// </summary>
        /// <param name="shape">The shape of the hazard.</param>
        /// <param name="image">The image associated with the hazard.</param>
        public Hazard(DynamicShape shape, IBaseImage image) : base(shape, image) {
        }

        /// <summary>
        /// Moves the hazard.
        /// </summary>
        public void Move() {
            base.Shape.Move();
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
        /// The effect of the hazard when it is activated.
        /// </summary>
        public abstract void HazardUpEffect();

        /// <summary>
        /// The effect of the hazard when it is deactivated.
        /// </summary>
        public abstract void HazardDownEffect();
    }
}

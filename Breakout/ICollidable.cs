using DIKUArcade.Entities;
using DIKUArcade.Physics;

namespace Breakout {
    /// <summary>
    /// The ICollidable interface is implemented by objects that can collide.
    /// </summary>
    public interface ICollidable {
        /// <summary>
        /// Gets or sets the shape of the collidable object.
        /// </summary>
        Shape Shape { get; set; }

        /// <summary>
        /// Handles the collision with another collidable object.
        /// </summary>
        /// <param name="data">The collision data.</param>
        /// <param name="collideWith">The collidable object collided with.</param>
        void Collision(CollisionData data, ICollidable collideWith);
    }
}

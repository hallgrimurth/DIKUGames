using DIKUArcade.Physics;
using DIKUArcade.Entities;

namespace Breakout {

    /// <summary>
    /// The ICollidable interface is implemented by objects that can collide.
    /// </summary>
    public interface ICollidable {

        Shape Shape { get; set; }


        void Collision(CollisionData data, ICollidable collideWith);
    }
}
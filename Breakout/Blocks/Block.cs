using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;

namespace Breakout
{
    /// <summary>
    /// Represents an abstract base class for blocks in the Breakout game.
    /// </summary>
    public abstract class Block : Entity, ICollidable
    {
        private int health;
        private int value;

        /// <summary>
        /// Gets the value of the block.
        /// </summary>
        public int Value { get { return value; } }

        /// <summary>
        /// Constructs a new instance of the Block class.
        /// </summary>
        /// <param name="shape">The dynamic shape of the block.</param>
        /// <param name="image">The image of the block.</param>
        public Block(DynamicShape shape, IBaseImage image) : base(shape, image)
        {
            health = 1;

            // Register collision event subscription for the block
            BreakoutBus.GetBus().RegisterEvent(new GameEvent
            {
                EventType = GameEventType.StatusEvent,
                Message = "SUBSCRIBE_COLLISION_EVENT",
                StringArg1 = "BALL",
                From = this
            });
        }

        /// <summary>
        /// Tries to delete the block entity.
        /// </summary>
        public abstract void TryDeleteEntity();

        /// <summary>
        /// Handles the collision with other entities.
        /// </summary>
        /// <param name="collisionData">The collision data.</param>
        /// <param name="other">The other collidable entity.</param>
        public void Collision(CollisionData collisionData, ICollidable other)
        {
            if (collisionData.Collision)
            {
                DecreaseHealth();
            }
        }

        /// <summary>
        /// Decreases the health of the block.
        /// </summary>
        public abstract void DecreaseHealth();

        /// <summary>
        /// Updates the block.
        /// </summary>
        public void Update()
        {
            if (IsDeleted())
            {
                return;
            }

            // Additional update logic can be added here if needed
        }
    }
}

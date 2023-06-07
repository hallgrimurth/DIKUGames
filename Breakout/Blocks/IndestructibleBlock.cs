using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout
{
    /// <summary>
    /// Represents an indestructible block in the Breakout game.
    /// </summary>
    public class IndestructibleBlock : Block
    {
        private int value;
        public override int Value { get { return this.value; } }
        private int health;
        public override int Health { get { return this.health; } }

        /// <summary>
        /// Constructs a new instance of the IndestructibleBlock class.
        /// </summary>
        /// <param name="shape">The dynamic shape of the block.</param>
        /// <param name="image">The image of the block.</param>
        public IndestructibleBlock(DynamicShape shape, IBaseImage image)
            : base(shape, image)
        {
            this.value = 5;
            this.health = 1;
        }

        /// <summary>
        /// Tries to delete the indestructible block entity.
        /// </summary>
        public override void TryDeleteEntity()
        {
            if (health < 1)
            {
                DeleteEntity();

                BreakoutBus.GetBus().RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.StatusEvent,
                    StringArg1 = "BALL",
                    Message = "UNSUBSCRIBE_COLLISION_EVENT",
                    From = this
                });

                BreakoutBus.GetBus().RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.PlayerEvent,
                    Message = "ADD_POINTS",
                    IntArg1 = value
                });
            }
        }

        /// <summary>
        /// Decreases the health of the indestructible block.
        /// </summary>
        public override void DecreaseHealth()
        {
            // No health decrease for indestructible block
            TryDeleteEntity();
        }

        /// <summary>
        /// Increases the health of the indestructable block.
        /// </summary>
        public override void IncreaseHealth()
        {
            this.health++;
        }
    }
}

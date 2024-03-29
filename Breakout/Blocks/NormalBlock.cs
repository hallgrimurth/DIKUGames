using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout
{
    /// <summary>
    /// Represents a normal block in the Breakout game.
    /// </summary>
    public class NormalBlock : Block
    {
        private int health;
        public override int Health { get { return this.health; } }
        private int value;
        public override int Value { get { return this.value; } }

        /// <summary>
        /// Constructs a new instance of the NormalBlock class.
        /// </summary>
        /// <param name="shape">The dynamic shape of the block.</param>
        /// <param name="image">The image of the block.</param>
        public NormalBlock(DynamicShape shape, IBaseImage image)
            : base(shape, image)
        {
            this.health = 1;
            this.value = 10;
        }

        /// <summary>
        /// Decreases the health of the normal block.
        /// </summary>
        public override void DecreaseHealth()
        {
            this.health--;
            if (health < 1)
            {
                TryDeleteEntity();
            }
            // TryDeleteEntity();
        }

        /// <summary>
        /// Tries to delete the normal block entity.
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
        /// Increases the health of the normal block.
        /// </summary>
        public override void IncreaseHealth()
        {
            this.health++;
        }
    }
}

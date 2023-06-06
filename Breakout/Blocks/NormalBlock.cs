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
        private int value;

        /// <summary>
        /// Constructs a new instance of the NormalBlock class.
        /// </summary>
        /// <param name="shape">The dynamic shape of the block.</param>
        /// <param name="image">The image of the block.</param>
        public NormalBlock(DynamicShape shape, IBaseImage image)
            : base(shape, image)
        {
            health = 1;
            value = 10;
        }

        /// <summary>
        /// Decreases the health of the normal block.
        /// </summary>
        public override void DecreaseHealth()
        {
            health--;
            TryDeleteEntity();
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
    }
}

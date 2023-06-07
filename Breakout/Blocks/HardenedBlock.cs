using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout
{
    /// <summary>
    /// Represents a hardened block in the Breakout game.
    /// </summary>
    public class HardenedBlock : Block
    {
        private IBaseImage damageImage;
        private int value;
        public override int Value { get { return this.value; } }
        private int health;
        public override int Health { get { return this.health; } }

        
        /// <summary>
        /// Gets the damage image of the hardened block.
        /// </summary>
        public IBaseImage DamageImage { get { return damageImage; } }

        /// <summary>
        /// Constructs a new instance of the HardenedBlock class.
        /// </summary>
        /// <param name="shape">The dynamic shape of the block.</param>
        /// <param name="image">The image of the block.</param>
        /// <param name="damageImage">The image of the block when damaged.</param>
        public HardenedBlock(DynamicShape shape, IBaseImage image, IBaseImage damageImage)
            : base(shape, image)
        {
            this.value = 20;
            this.health = 2;
            this.damageImage = damageImage;
        }

        /// <summary>
        /// Tries to delete the hardened block entity.
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
        /// Decreases the health of the hardened block.
        /// </summary>
        public override void DecreaseHealth()
        {
            health--;
            if (health == 1)
            {
                Image = damageImage;
            }
            TryDeleteEntity();
        }

        /// <summary>
        /// Increases the health of the hardened block.
        /// </summary>
        public override void IncreaseHealth()
        {
            this.health++;
        }
    }
}

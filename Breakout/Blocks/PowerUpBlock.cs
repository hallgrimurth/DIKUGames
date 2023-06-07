using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout {
    /// <summary>
    /// Represents a power-up block in the Breakout game.
    /// </summary>
    public class PowerUpBlock : Block {
        private int value;
        public override int Value {
            get{ return value; }
            // set{ this.value = value; }
        }
        private int health; 
        public override int Health {
            get{ return health; }
            // set{ this.health = value; }
        }


        /// <summary>
        /// Constructs a new instance of the PowerUpBlock class.
        /// </summary>
        /// <param name="shape">The dynamic shape of the block.</param>
        /// <param name="image">The image of the block.</param>
        public PowerUpBlock(DynamicShape shape, IBaseImage image)
            : base(shape, image) {
            value = 20;
            health = 1;
        }

        /// <summary>
        /// Tries to delete the power-up block entity.
        /// </summary>
        public override void TryDeleteEntity() {
            if (health < 1) {
                DeleteEntity();

                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.StatusEvent,
                    StringArg1 = "BALL",
                    Message = "UNSUBSCRIBE_COLLISION_EVENT",
                    From = this
                });

                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "ADD_POINTS",
                    IntArg1 = value
                });

                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.StatusEvent,
                    Message = "SPAWN_POWERUP",
                    StringArg1 = Shape.Position.X.ToString(),
                    StringArg2 = Shape.Position.Y.ToString(),
                    From = this
                });
            }
        }

        /// <summary>
        /// Decreases the health of the power-up block.
        /// </summary>
        public override void DecreaseHealth() {
            health--;
            TryDeleteEntity();
        }

        /// <summary>
        /// Increases the health of the powerup block.
        /// </summary>
        public override void IncreaseHealth()
        {
            this.health++;
        }
    }
}

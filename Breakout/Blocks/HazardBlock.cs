using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;


namespace Breakout {
    /// <summary>
    /// Represents a hazard block in the Breakout game.
    /// </summary>
    public class HazardBlock : Block {

        private int value;
        public override int Value {
            get{ return value; }
        }
        private int health;
        public override int Health {
            get{ return health; }
        }

        //constructor for block
        public HazardBlock(DynamicShape Shape, IBaseImage image)
            : base(Shape, image) {
            this.value = 20;
            this.health = 1;
        }
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
                    IntArg1 = this.value
                });

                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.StatusEvent,
                    Message = "SPAWN_HAZARD",
                    StringArg1 = Shape.Position.X.ToString(),
                    StringArg2 = Shape.Position.Y.ToString(),
                    From = this
                });
            }
        }
        public override void DecreaseHealth() {
            this.health--;
            TryDeleteEntity();
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
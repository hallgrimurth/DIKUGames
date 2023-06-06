using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using DIKUArcade.Events;

namespace Breakout {
    /// <summary>
    /// Represents a hazard that increases the player's health.
    /// </summary>
    public class ToughenUpHazard : Hazard {
        private double startTime;
        private double endTime;

        /// <summary>
        /// Constructs a ToughenUpHazard object.
        /// </summary>
        /// <param name="shape">The shape of the hazard.</param>
        /// <param name="image">The image associated with the hazard.</param>
        public ToughenUpHazard(DynamicShape shape, IBaseImage image) : base(shape, image) {
        }

        /// <summary>
        /// The effect of the hazard when it is activated.
        /// </summary>
        public override void HazardUpEffect() {
            startTime = (int)StaticTimer.GetElapsedSeconds();
            endTime = startTime + 10;

            GameEvent toughenUpEvent = new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "INCREASE_HEALTH"
            };
            BreakoutBus.GetBus().RegisterEvent(toughenUpEvent);
        }

        /// <summary>
        /// The effect of the hazard when it is deactivated.
        /// </summary>
        public override void HazardDownEffect() {
            // No specific effect when the hazard is deactivated.
        }
    }
}

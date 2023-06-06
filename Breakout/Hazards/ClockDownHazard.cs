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
    /// Represents a hazard that causes the game clock to count down faster.
    /// </summary>
    public class ClockDownHazard : Hazard {
        private double startTime;
        private double endTime;

        /// <summary>
        /// Constructs a ClockDownHazard object.
        /// </summary>
        /// <param name="shape">The shape of the hazard.</param>
        /// <param name="image">The image associated with the hazard.</param>
        public ClockDownHazard(DynamicShape shape, IBaseImage image) : base(shape, image) {
        }

        /// <summary>
        /// The effect of the hazard when it is activated.
        /// </summary>
        public override void HazardUpEffect() {
            GameEvent clockDownEvent = new GameEvent {
                EventType = GameEventType.StatusEvent,
                Message = "CLOCK_DOWN"
            };
            BreakoutBus.GetBus().RegisterEvent(clockDownEvent);
        }

        /// <summary>
        /// This hazard has no effect when it is deactivated.
        /// </summary>
        public override void HazardDownEffect() {
        }
    }
}

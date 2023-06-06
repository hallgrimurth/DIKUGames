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
            startTime = (int)StaticTimer.GetElapsedSeconds();
            endTime = startTime + 10;

            // Register a ClockDownEvent to inform the game about the hazard.
            GameEvent clockDownEvent = new GameEvent {
                EventType = GameEventType.MovementEvent,
                Message = "CLOCK_DOWN"
            };
            BreakoutBus.GetBus().RegisterEvent(clockDownEvent);
        }

        /// <summary>
        /// The effect of the hazard when it is activated.
        /// </summary>
        public override void HazardUpEffect() {
            // Implement the effect of the hazard when it is activated.
            // This method should contain the logic to speed up the game clock.
            // Add code here...
        }

        /// <summary>
        /// The effect of the hazard when it is deactivated.
        /// </summary>
        public override void HazardDownEffect() {
            // Implement the effect of the hazard when it is deactivated.
            // This method should contain the logic to revert the changes made by HazardUpEffect.
            // Add code here...
        }
    }
}

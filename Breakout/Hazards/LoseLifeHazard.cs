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
    /// Represents a hazard that causes the player to lose a life.
    /// </summary>
    public class LoseLifeHazard : Hazard {
        private double startTime;
        private double endTime;

        /// <summary>
        /// Constructs a LoseLifeHazard object.
        /// </summary>
        /// <param name="shape">The shape of the hazard.</param>
        /// <param name="image">The image associated with the hazard.</param>
        public LoseLifeHazard(DynamicShape shape, IBaseImage image) : base(shape, image) {
        }

        /// <summary>
        /// The effect of the hazard when it is activated.
        /// </summary>
        public override void HazardUpEffect() {
            startTime = (int)StaticTimer.GetElapsedSeconds();
            endTime = startTime + 10;

            // Register a LoseLifeEvent to inform the game about the hazard.
            GameEvent loseLifeEvent = new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "DECREASE_HEALTH"
            };
            BreakoutBus.GetBus().RegisterEvent(loseLifeEvent);
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

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
    /// Represents a hazard that narrows the player's paddle.
    /// </summary>
    public class SlimJimHazard : Hazard {
        private double startTime;
        private double endTime;

        /// <summary>
        /// Constructs a SlimJimHazard object.
        /// </summary>
        /// <param name="shape">The shape of the hazard.</param>
        /// <param name="image">The image associated with the hazard.</param>
        public SlimJimHazard(DynamicShape shape, IBaseImage image) : base(shape, image) {
        }

        /// <summary>
        /// The effect of the hazard when it is activated.
        /// </summary>
        public override void HazardUpEffect() {
            GameEvent slimJimEvent = new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "NARROW_PADDLE"
            };
            BreakoutBus.GetBus().RegisterEvent(slimJimEvent);
        }

        /// <summary>
        /// The effect of the hazard when it is deactivated.
        /// </summary>
        public override void HazardDownEffect() {
            if ((int)StaticTimer.GetElapsedSeconds() > endTime) {
                GameEvent widenPaddleEvent = new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "WIDE_PADDLE"
                };
                BreakoutBus.GetBus().RegisterEvent(widenPaddleEvent);
            }
        }
    }
}

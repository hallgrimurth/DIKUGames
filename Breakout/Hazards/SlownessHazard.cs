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
    /// Represents a hazard that slows down the movement of the player's paddle.
    /// </summary>
    public class SlownessHazard : Hazard {
        private double startTime;
        private double endTime;

        /// <summary>
        /// Constructs a SlownessHazard object.
        /// </summary>
        /// <param name="shape">The shape of the hazard.</param>
        /// <param name="image">The image associated with the hazard.</param>
        public SlownessHazard(DynamicShape shape, IBaseImage image) : base(shape, image) {
        }

        /// <summary>
        /// The effect of the hazard when it is activated.
        /// </summary>
        public override void HazardUpEffect() {
            startTime = (int)StaticTimer.GetElapsedSeconds();
            endTime = startTime + 10;

            GameEvent slownessEvent = new GameEvent {
                EventType = GameEventType.MovementEvent,
                Message = "SLOW_MOVEMENT"
            };
            BreakoutBus.GetBus().RegisterEvent(slownessEvent);
        }

        /// <summary>
        /// The effect of the hazard when it is deactivated.
        /// </summary>
        public override void HazardDownEffect() {
            if ((int)StaticTimer.GetElapsedSeconds() > endTime) {
                GameEvent normalSpeedEvent = new GameEvent {
                    EventType = GameEventType.MovementEvent,
                    Message = "NORMAL_MOVEMENT"
                };
                BreakoutBus.GetBus().RegisterEvent(normalSpeedEvent);
            }
        }
    }
}

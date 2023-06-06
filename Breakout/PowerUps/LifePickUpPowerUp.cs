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
    /// Represents a power-up that increases the player's health.
    /// </summary>
    public class LifePickUpPowerUp : PowerUp {
        private double startTime;
        private double endTime;

        /// <summary>
        /// Constructs a LifePickUpPowerUp object.
        /// </summary>
        /// <param name="shape">The shape of the power-up.</param>
        /// <param name="image">The image associated with the power-up.</param>
        public LifePickUpPowerUp(DynamicShape shape, IBaseImage image) : base(shape, image) {
        }

        /// <summary>
        /// The effect of the power-up when it is activated.
        /// </summary>
        public override void PowerUpEffect() {
            startTime = (int)StaticTimer.GetElapsedSeconds();
            endTime = startTime + 10;

            GameEvent lifePickUpEvent = new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "INCREASE_HEALTH"
            };
            BreakoutBus.GetBus().RegisterEvent(lifePickUpEvent);
        }

        /// <summary>
        /// The effect of the power-up when it is deactivated.
        /// </summary>
        public override void PowerDownEffect() {
            if ((int)StaticTimer.GetElapsedSeconds() > endTime) {
                GameEvent narrowPaddleEvent = new GameEvent {
                    EventType = GameEventType.MovementEvent,
                    Message = "Narrow"
                };
                BreakoutBus.GetBus().RegisterEvent(narrowPaddleEvent);
            }
        }
    }
}

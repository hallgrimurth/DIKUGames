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
    /// Represents a power-up that doubles the size of the ball.
    /// </summary>
    public class BigPowerUp : PowerUp {
        private double startTime;
        private double endTime;

        /// <summary>
        /// Constructs a BigPowerUp object.
        /// </summary>
        /// <param name="shape">The shape of the power-up.</param>
        /// <param name="image">The image associated with the power-up.</param>
        public BigPowerUp(DynamicShape shape, IBaseImage image) : base(shape, image) {
        }

        /// <summary>
        /// The effect of the power-up when it is activated.
        /// </summary>
        public override void PowerUpEffect() {
            startTime = (int)StaticTimer.GetElapsedSeconds();
            endTime = startTime + 10;

            GameEvent bigBallEvent = new GameEvent {
                EventType = GameEventType.MovementEvent,
                Message = "DOUBLE_SIZE"
            };
            BreakoutBus.GetBus().RegisterEvent(bigBallEvent);
        }

        /// <summary>
        /// The effect of the power-up when it is deactivated.
        /// </summary>
        public override void PowerDownEffect() {
            if ((int)StaticTimer.GetElapsedSeconds() > endTime) {
                GameEvent normalBallEvent = new GameEvent {
                    EventType = GameEventType.MovementEvent,
                    Message = "NORMAL_SIZE"
                };
                BreakoutBus.GetBus().RegisterEvent(normalBallEvent);
            }
        }
    }
}

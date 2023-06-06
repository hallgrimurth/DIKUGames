using System;
using System.IO;
using System.Linq;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout {
    /// <summary>
    /// Represents a factory for creating hazards.
    /// </summary>
    public class HazardFactory {
        private static Random rand = new Random();
        private static Vec2F hazardExtent = new Vec2F(0.03f, (1 / 36.0f));

        /// <summary>
        /// Creates a hazard at the specified position.
        /// </summary>
        /// <param name="pos">The position of the hazard.</param>
        /// <returns>The created hazard.</returns>
        public static Hazard CreateHazard(Vec2F pos) {
            Vec2F hazardPos = new Vec2F(pos.X + 1 / 36.0f, pos.Y);
            DynamicShape HazardShape = new DynamicShape(hazardPos, hazardExtent);

            int random = rand.Next(0, 3);
            switch (random) {
                case 0:
                    var clockDownImage = new Image(Path.Combine("newAssets", "Images", "ClockDownHazard.png"));
                    return new ClockDownHazard(HazardShape, clockDownImage);
                case 1:
                    var slimJimImage = new Image(Path.Combine("newAssets", "Images", "SlimJim.png"));
                    return new SlimJimHazard(HazardShape, slimJimImage);
                case 2:
                    var loseLifeImage = new Image(Path.Combine("newAssets", "Images", "LoseLife.png"));
                    return new LoseLifeHazard(HazardShape, loseLifeImage);
                case 3:
                    var slownessImage = new Image(Path.Combine("newAssets", "Images", "Slowness.png"));
                    return new SlownessHazard(HazardShape, slownessImage);
                default:
                    var toughenUpImage = new Image(Path.Combine("newAssets", "Images", "ToughenUp.png"));
                    return new ToughenUpHazard(HazardShape, toughenUpImage);
            }
        }
    }
}

using System;
using System.IO;
using System.Linq;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using System.Collections.Generic;
using DIKUArcade.Graphics;

namespace Breakout {
    /// <summary>
    /// Represents a factory for creating power-up objects.
    /// </summary>
    public class PowerUpFactory {
        private static Random rand = new Random();
        private static Vec2F powerupExtent = new Vec2F(0.03f, (1 / 36.0f));

        /// <summary>
        /// Creates a power-up object at the specified position.
        /// </summary>
        /// <param name="pos">The position of the power-up.</param>
        /// <returns>The created power-up object.</returns>
        public static PowerUp CreatePowerUp(Vec2F pos) {
            Vec2F powerupPos = new Vec2F(pos.X + 1 / 36.0f, pos.Y);
            DynamicShape powerUpShape = new DynamicShape(powerupPos, powerupExtent);

            // Choose a random power-up type
            int random = rand.Next(0, 3);
            switch (random) {
                case 0:
                    var bigImage = new Image(Path.Combine("Assets", "Images", "BigPowerUp.png"));
                    return new BigPowerUp(powerUpShape, bigImage);
                case 1:
                    var wideImage = new Image(Path.Combine("Assets", "Images", "WidePowerUp.png"));
                    return new WidePowerUp(powerUpShape, wideImage);
                case 2:
                    var lifePickUpImage = new Image(Path.Combine("Assets", "Images", "LifePickUp.png"));
                    return new LifePickUpPowerUp(powerUpShape, lifePickUpImage);
                default:
                    var defaultBigImage = new Image(Path.Combine("Assets", "Images", "BigPowerUp.png"));
                    return new BigPowerUp(powerUpShape, defaultBigImage);
            }
        }
    }
}

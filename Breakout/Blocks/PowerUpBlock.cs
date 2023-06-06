using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout
{
    /// <summary>
    /// Represents a power-up block in the Breakout game.
    /// </summary>
    public class PowerUpBlock : Block
    {
        private int value;

        /// <summary>
        /// Constructs a new instance of the PowerUpBlock class.
        /// </summary>
        /// <param name="shape">The dynamic shape of the block.</param>
        /// <param name="image">The image of the block.</param>
        public PowerUpBlock(DynamicShape shape, IBaseImage image)
            : base(shape, image)
        {
            this.value = 20;
        }

        /// <summary>
        /// Decreases the health of the power-up block.
        /// </summary>
        public override void DecreaseHealth()
        {
            Health--;
            TryDeleteEntity();
        }
    }
}

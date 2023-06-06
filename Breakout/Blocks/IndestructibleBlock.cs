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
    /// Represents an indestructible block in the Breakout game.
    /// </summary>
    public class IndestructibleBlock : Block
    {
        private int value;

        /// <summary>
        /// Constructs a new instance of the IndestructibleBlock class.
        /// </summary>
        /// <param name="shape">The dynamic shape of the block.</param>
        /// <param name="image">The image of the block.</param>
        public IndestructibleBlock(DynamicShape shape, IBaseImage image)
            : base(shape, image)
        {
            this.value = 5;
        }

        /// <summary>
        /// Decreases the health of the indestructible block.
        /// </summary>
        public override void DecreaseHealth()
        {
            // Indestructible blocks cannot be destroyed, so no health decrease is performed.
            // Leaving the method empty.
        }
    }
}

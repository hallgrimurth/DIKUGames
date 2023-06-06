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
    /// Represents a normal block in the Breakout game.
    /// </summary>
    public class NormalBlock : Block
    {
        /// <summary>
        /// Constructs a new instance of the NormalBlock class.
        /// </summary>
        /// <param name="shape">The dynamic shape of the block.</param>
        /// <param name="image">The image of the block.</param>
        public NormalBlock(DynamicShape shape, IBaseImage image)
            : base(shape, image)
        {
            // No additional logic required in the constructor.
        }

        /// <summary>
        /// Decreases the health of the normal block.
        /// </summary>
        public override void DecreaseHealth()
        {
            Health--;
            TryDeleteEntity();
        }
    }
}

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
    /// Represents a hardened block in the Breakout game.
    /// </summary>
    public class HardenedBlock : Block
    {
        private IBaseImage damageImage;

        /// <summary>
        /// Gets the damage image of the hardened block.
        /// </summary>
        public IBaseImage DamageImage { get { return damageImage; } }

        private int value;

        /// <summary>
        /// Constructs a new instance of the HardenedBlock class.
        /// </summary>
        /// <param name="shape">The dynamic shape of the block.</param>
        /// <param name="image">The image of the block.</param>
        /// <param name="damageImage">The damage image of the block.</param>
        public HardenedBlock(DynamicShape shape, IBaseImage image, IBaseImage damageImage)
            : base(shape, image)
        {
            this.value = 20;
            Health = 2;
            this.damageImage = damageImage;
        }

        /// <summary>
        /// Decreases the health of the hardened block.
        /// </summary>
        public override void DecreaseHealth()
        {
            Health--;

            if (Health == 1)
            {
                Image = damageImage;
            }

            TryDeleteEntity();
        }
    }
}

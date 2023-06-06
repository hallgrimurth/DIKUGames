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

namespace Breakout
{
    /// <summary>
    /// Represents a factory class for creating blocks in the Breakout game.
    /// </summary>
    public class BlockFactory
    {
        private static Vec2F blockExtent = new Vec2F((1 / 12.0f), (1 / 36.0f));

        /// <summary>
        /// Creates a block based on the provided parameters.
        /// </summary>
        /// <param name="i">The row index of the block.</param>
        /// <param name="j">The column index of the block.</param>
        /// <param name="imageName">The image name of the block.</param>
        /// <param name="blockType">The type of the block.</param>
        /// <returns>The created block.</returns>
        public static Block CreateBlock(int i, int j, string imageName, char blockType)
        {
            IBaseImage image = new Image(
                Path.Combine("Assets", "Images", imageName));
            IBaseImage damageImage = new Image(
                Path.Combine("Assets", "Images", imageName.Substring(0, imageName.Length - 4) + "-damaged.png"));

            Vec2F blockPos = new Vec2F(blockExtent.X * j, 1.0f - blockExtent.Y * i);
            DynamicShape blockShape = new DynamicShape(blockPos, blockExtent);

            // Create different types of blocks based on the blockType
            switch (blockType)
            {
                case 'N':
                    return new NormalBlock(blockShape, image);
                case 'H':
                    return new HardenedBlock(blockShape, image, damageImage);
                case 'I':
                    return new IndestructibleBlock(blockShape, image);
                case 'P':
                    return new PowerUpBlock(blockShape, image);
                default:
                    return new NormalBlock(blockShape, image);
            }
        }
    }
}

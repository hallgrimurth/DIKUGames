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
    public abstract class BlockFactory {
        private static Vec2F blockExtent = new Vec2F((1/12.0f), (1/24.0f));

        //Creates the blocks dependent of their block type. Positions them in the intended pattern.
        public static Block CreateBlock(int i, int j, Image image, char blockType) {
            Vec2F blockPos = new Vec2F(blockExtent.X*j, 1.0f - blockExtent.Y*i - blockExtent.Y);
            DynamicShape blockShape = new DynamicShape(blockPos, blockExtent);
            //Case for each block type. Normal block, indestructible block, powerup block, hardened block
            switch (blockType) {
                case 'N':
                    return new NormalBlock(blockShape, image);
                case 'H':
                    return new HardenedBlock(blockShape, image);
                case 'I':
                    return new IndestructableBlock(blockShape, image);
                case 'P':
                    return new PowerUpBlock(blockShape, image);
                default:
                    return new NormalBlock(blockShape, image);
            }
        }
    }

}
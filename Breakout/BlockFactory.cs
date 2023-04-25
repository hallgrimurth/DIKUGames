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

        public static Block CreateBlock(int i, int j, Image image) {
            Vec2F blockPos = new Vec2F(blockExtent.X*j, 1.0f - blockExtent.Y*i - blockExtent.Y);
            DynamicShape blockShape = new DynamicShape(blockPos, blockExtent);
            Block block = new RedBlock(blockShape, image);
            // Console.WriteLine(block.Health); // testing purposes

            return block;
            // blocks.AddEntity(block);
        }
    }

}
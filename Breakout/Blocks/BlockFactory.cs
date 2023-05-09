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
    public class BlockFactory {
        private static Vec2F blockExtent = new Vec2F((1/12.0f), (1/24.0f));
        // private static DynamicShape blockShape;
        // private static Vec2F blockPos;
        // private static IBaseImage image;
        // private static IBaseImage damageimage;

        public static Block CreateBlock(int i, int j, string Imagename, char blockType) {
            IBaseImage image = new Image(
                Path.Combine("Assets", "Images", Imagename));
            IBaseImage damageimage = new Image(
                Path.Combine("Assets", "Images", Imagename.Substring(0, Imagename.Length-4) +"-damaged.png"));

            Vec2F blockPos = new Vec2F(blockExtent.X*j, 1.0f - blockExtent.Y*i - blockExtent.Y);
            DynamicShape blockShape = new DynamicShape(blockPos, blockExtent);
            
            //Case for each block type. Normal block, indestructible block, powerup block, hardened block
            switch (blockType) {
                case 'N':
                    return new NormalBlock(blockShape, image);
                case 'H':
                    return new HardenedBlock(blockShape, image, damageimage);
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
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
    public class PowerUpFactory {
        private static Random rand = new Random();
        private static Vec2F powerupExtent = new Vec2F(0.05f, 0.05f); // new Vec2F((1/12.0f), (1/36.0f));
        public static PowerUp CreatePowerUp(int i, int j) {
            // IBaseImage image = new Image(
            //     Path.Combine("Assets", "Images", Imagename));
            // IBaseImage damageimage = new Image(
            //     Path.Combine("Assets", "Images", Imagename.Substring(0, Imagename.Length-4) +"-damaged.png"));

            Vec2F powerupPos = new Vec2F(powerupExtent.X * j, 1.0f - powerupExtent.Y * i);
            DynamicShape powerupShape = new DynamicShape(powerupPos, powerupExtent);
            
            //Case for each block type. Normal block, indestructible block, powerup block, hardened block
            int random = rand.Next(0, 3);
            switch(random){
                case 0:
                    bigImage = new Image(Path.Combine("Assets", "Images", "BigPowerUp.png"));
                    powerUp = new BigPowerUp(PowerUpShape, bigImage);
                    // Console.WriteLine(BigPowerUp);
                    return powerUp;
                    // break;
                case 1:
                    wideImage = new Image(Path.Combine("Assets", "Images", "WidePowerUp.png"));
                    powerUp = new WidePowerUp(PowerUpShape, wideImage);
                    // Console.WriteLine(WidePowerUp);
                    return powerUp;
                    // break;
                case 2:
                    splitImage = new Image(Path.Combine("Assets", "Images", "SplitPowerUp.png"));
                    powerUp = new SplitPowerUp(PowerUpShape, splitImage);
                    // Console.WriteLine(SplitPowerUp);
                    return powerUp;
                    // break;
            }
        }
    }

}
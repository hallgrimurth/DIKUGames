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
        private static Vec2F powerupExtent = new Vec2F(0.03f, (1/36.0f));
        // private static DynamicShape PowerUpShape = new DynamicShape(new Vec2F(0.0f, 0.0f), powerupExtent);

        public static PowerUp CreatePowerUp(Vec2F pos){

            // Vec2F powerupPos = new Vec2F(powerupExtent.X * j, 1.0f - powerupExtent.Y * i);
            Vec2F powerupPos = new Vec2F(pos.X + 1/36.0f, pos.Y);
            DynamicShape PowerUpShape = new DynamicShape(powerupPos, powerupExtent);
            
            
            //Case for each block type. Normal block, indestructible block, powerup block, hardened block
            int random = rand.Next(0, 3);
            switch(random){
                case 0:
                    var bigImage = new Image(Path.Combine("Assets", "Images", "BigPowerUp.png"));
                    
                    return new BigPowerUp(PowerUpShape, bigImage);
                case 1:
                    var wideImage = new Image(Path.Combine("Assets", "Images", "WidePowerUp.png"));
                    return new WidePowerUp(PowerUpShape, wideImage);
                case 2:
                    var LifePickUpImage = new Image(Path.Combine("Assets", "Images", "LifePickUp.png"));
                    // Console.WriteLine(LifePickUpPowerUp);
                    return new SplitPowerUp(PowerUpShape, LifePickUpImage);
                default:
                    var defaultBigImage = new Image(Path.Combine("Assets", "Images", "BigPowerUp.png"));
                    
                    return new BigPowerUp(PowerUpShape, defaultBigImage);
            }
        }
    }

}
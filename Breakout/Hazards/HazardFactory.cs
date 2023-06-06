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
    public class HazardFactory {
        private static Random rand = new Random();
        private static Vec2F hazardExtent = new Vec2F(0.03f, (1/36.0f));
        // private static DynamicShape HazardShape = new DynamicShape(new Vec2F(0.0f, 0.0f), hazardExtent);

        public static Hazard CreateHazard(Vec2F pos){

            // Vec2F hazardPos = new Vec2F(hazardExtent.X * j, 1.0f - hazardExtent.Y * i);
            Vec2F hazardPos = new Vec2F(pos.X + 1/36.0f, pos.Y);
            DynamicShape HazardShape = new DynamicShape(hazardPos, hazardExtent);
            
            
            //Case for each hazard type
            int random = rand.Next(0, 4);
            switch(random){
                case 0:
                    var ClockDownImage = new Image(Path.Combine("newAssets", "Images", "clock-down.png"));
                    return new ClockDownHazard(HazardShape, ClockDownImage);
                case 1:
                    var SlimJimImage = new Image(Path.Combine("newAssets", "Images", "SlimJim.png"));
                    return new SlimJimHazard(HazardShape, SlimJimImage);

                case 2:
                    var LoseLifeImage = new Image(Path.Combine("newAssets", "Images", "LoseLife.png"));
                    return new LoseLifeHazard(HazardShape, LoseLifeImage);
                case 3:
                    var SlownessImage = new Image(Path.Combine("newAssets", "Images", "Slowness.png"));
                    return new SlownessHazard(HazardShape, SlownessImage);
                default:
                    var ToughenUpImage = new Image(Path.Combine("newAssets", "Images", "ToughenUp.png"));
                    return new ToughenUpHazard(HazardShape, ToughenUpImage);
                   
            }
        }
    }

}
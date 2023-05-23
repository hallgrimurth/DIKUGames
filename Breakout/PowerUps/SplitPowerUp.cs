using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public class SplitPowerUp : PowerUp  {
    
    public SplitPowerUp(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }
    public override void PowerUpEffect(){

    } 
    
}
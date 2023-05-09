using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public class PowerUpBlock : Block {

    
    //constructor for block
    public PowerUpBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        
    }

    public override void DecreaseHealth() {
        this.Health--;
        
        
        
    }
        
    
}
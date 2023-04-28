using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public class PowerUpBlock : Block {

    
    //constructor for power up block
    public PowerUpBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        
    }
        
}
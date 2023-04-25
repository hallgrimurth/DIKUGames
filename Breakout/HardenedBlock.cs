using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public class HardenedBlock : Block {

    
    //constructor for block
    public HardenedBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        
    }
        
}
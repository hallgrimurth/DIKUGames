using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public class IndestructableBlock : Block {

    
    //constructor for the hardened block
    public IndestructableBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        
    }
        
}
using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public class RedBlock : Block {

    //constructor for red block
    public RedBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
            
        }
}

using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public class NormalBlock : Block {
    private int value;
    public int Value {
        get { return value; }
    }
    
    //constructor for block
    public NormalBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        value = 1;
        
    }

    public override void DecreaseHealth() {
        this.Health--;
        
        
        
    }
        
}
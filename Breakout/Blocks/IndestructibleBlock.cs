using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public class IndestructibleBlock : Block {

    private int value;
    public int Value {
        get { return value; }
    }

    //constructor for block
    public IndestructibleBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        value = 5;
    }

    public override void DecreaseHealth() { 
        //do nothing
    }
        
}
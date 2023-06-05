using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;


namespace Breakout;
public class IndestructibleBlock : Block {

    private int value;

    //constructor for block
    public IndestructibleBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        this.value = 5;
    }
    public override void DecreaseHealth() { 
        TryDeleteEntity();
    }
        
}
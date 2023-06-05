using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;


namespace Breakout;
public class PowerUpBlock : Block {

    private int value;

    //constructor for block
    public PowerUpBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        this.value = 20;
    }
    public override void DecreaseHealth() {
        this.Health--;
        TryDeleteEntity();
    }
}
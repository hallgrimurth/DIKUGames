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
    private PowerUp powerUp;
    
    public int Value {
        get { return value; }
    }
    //constructor for block
    public PowerUpBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        value = 10;
    }
    public override void DecreaseHealth() {
        this.Health--; 
        if (Health == 0) {
            DeleteEntity();
            // Register score event
        }
    
    }

    //render powerup
    public void Render(){
        powerUp.RenderEntity();
    }
     
    
}
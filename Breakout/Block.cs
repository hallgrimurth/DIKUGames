using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public class Block : Entity {

    private int health;
    public int value;
    // public IBaseImage blockImage;
    public DynamicShape shape;
    // public Vec2F blockExtent = new Vec2F(1/12.0f,1/24.0f);
    
    public int Health {
        get { return health; }
        set { health = value; }
    }
    //constructor for block
    public Block(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        health = 3;
        value = 1;
        this.shape = Shape;
    }
        
    public void DecreaseHealth() {
        health--;
        if (health == 0) {
            DeleteEntity();
        }
    }
}
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
    public IBaseImage blockImage;
    public DynamicShape shape;
    public Vec2F blockExtent = new Vec2F(1/12.0f,1/24.0f);
    
    public int Health {
        get { return health; }
        set { health = value; }
    }
    //constructor for block
    // public Block(DynamicShape Shape, IBaseImage image)
    public Block(Vec2F Position, IBaseImage image)
        : base(new DynamicShape((Position, blockExtent)), image) {
        health = 3;
        value = 1;
        // var blockPos = new Vec2F(x*j, 0.95f - y*i);
        // var blockExtent = new Vec2F(1/12.0f,1/24.0f);//new Vec2F(x, y);
        blockImage = image;// new Image(Path.Combine(image));
    }
        
    public void DecreaseHealth() {
        health--;
        if (health == 0) {
            DeleteEntity();
        }
    }
}
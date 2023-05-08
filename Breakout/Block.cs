using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public abstract class Block : Entity {

    private int health = 3;
    public int value = 1;
    public IBaseImage blockImage;
    public DynamicShape shape;
    
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
    }

    public void DeleteBlock() {
        if (health == 0) {
            DeleteEntity();
        }
    }
}
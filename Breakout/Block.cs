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
    //Constructor for block. Defines the heath, score and position/extent of the block
    public Block(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        health = 3;
        value = 1;
        this.shape = Shape;
    }

    //Decrement in health when block is hit 
    public void DecreaseHealth() {
        health--;
    }

    //Block is being deleted when health is depleted
    public void DeleteBlock() {
        if (health == 0) {
            DeleteEntity();
        }
    }
}
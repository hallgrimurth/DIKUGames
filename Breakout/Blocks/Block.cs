using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;


namespace Breakout;
public abstract class Block : Entity {

    private int health;
    private int value;    
    public int Value {
        get { return value; }
        set { value = value; }
    }
    public int Health {
        get { return health; }
        set { health = value; }
    }

    //constructor for block
    public Block(DynamicShape Shape, IBaseImage image) : base(Shape, image) {
        health = 1;
        value = 1;
    }
        
    public abstract void DecreaseHealth() ;
     
}
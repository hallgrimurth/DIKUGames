using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public abstract class Block : Entity {

    private int health;
    public int ScoreValue = 1;    
    public int Health {
        get { return health; }
        set { health = value; }
    }
    //constructor for block
    public Block(DynamicShape Shape, IBaseImage image) : base(Shape, image) {
        health = 1;
        ScoreValue = 1;
    }
        
    public abstract void DecreaseHealth() ;
     
//     public void DeleteBlock() {
//         if (health == 0) {
//             DeleteEntity();
//         }
//     }
}
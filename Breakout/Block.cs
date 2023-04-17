using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Breakout;
public class Block : Entity {

    private int health;
    public int value;
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
<<<<<<< HEAD
        value = 1;
        blockImage = image;
        shape = Shape;
=======
        value = this.value;
>>>>>>> 8d092adf5190627c758296c67161ff59e126d6e9
    }
        
    public void DecreaseHealth() {
        health--;
        if (health == 0) {
            DeleteEntity();
        }
    }
}
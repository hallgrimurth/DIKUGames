using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Galaga;
public class Enemy : Entity {

    private int hitpoints;
    public float speed;
    public IBaseImage redImage;
    public DynamicShape shape;
    public Vec2F startPos {get;}
    
    
    public int Hitpoints {
        get { return hitpoints; }
        set { hitpoints = value; }
    }
    //constructor for enemy
    public Enemy(DynamicShape Shape, IBaseImage image, IBaseImage redImage, float speed)
        : base(Shape, image) {
        this.shape = Shape;
        this.redImage = redImage;
        this.startPos = shape.Position;
        this.speed = speed;
        hitpoints = 3;
    }
        
    public void DecreaseHitpoints() {
        hitpoints--;
        if (hitpoints == 0) {
            DeleteEntity();
        }
        // enrage when hitpoints is low
        else if (hitpoints == 1) {
            Enrage();
        }
    }

    public void Enrage() {
        Image = redImage;
        speed = speed*2.0f;
    }
}
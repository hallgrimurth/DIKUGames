using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Galaga;
public class Enemy : Entity {

    private int hitpoints = 3;
    public float speed = -0.0003f;
    public IBaseImage redImage;
    public DynamicShape shape;
    public Vec2F startPos {get;}
    
     
    public int Hitpoints {
        get { return hitpoints; }
    }
    //constructor for enemy
    public Enemy(DynamicShape Shape, IBaseImage image, IBaseImage redImage)
        : base(Shape, image) {
        this.shape = Shape;
        this.redImage = redImage;
        this.startPos = shape.Position;
    }
        
    public void DecreaseHitpoints() {
        hitpoints--;
        if (hitpoints == 0) {
            DeleteEntity();
        }
        else if (hitpoints == 1) {
            Enrage();
        }
    }
    //enrage enemies when hirpoints are low 
    public void Enrage() {
        if (hitpoints == 1) {
            Image = redImage;
            speed = -0.0002f;
        }
        
    }
        
}
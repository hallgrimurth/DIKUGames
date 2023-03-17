using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Galaga;
public class Enemy : Entity {

    private int hitpoints = 3;
    public float speed = -0.002f;
    private IBaseImage redImage;

     
    public int Hitpoints {
        get { return hitpoints; }
    }

    public DynamicShape shape;
    public Vec2F startPos {get;}
    public Enemy(DynamicShape shape, IBaseImage image, IBaseImage redImage)
        : base(shape, image) {
        this.redImage = redImage;
        startPos = new Vec2F(this.shape.Position.X, this.shape.Position.Y);
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
            speed = -0.005f;
        }
        
    }
        
}
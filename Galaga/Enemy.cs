using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;

namespace Galaga;
public class Enemy : Entity {

    private int hitpoints = 3;
    public float speed = -0.002f;
    private IBaseImage redImage;

     
    public int Hitpoints {
        get { return hitpoints; }
    }
    public Enemy(DynamicShape shape, IBaseImage image, IBaseImage redImage)
        : base(shape, image) {
        this.redImage = redImage;        
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
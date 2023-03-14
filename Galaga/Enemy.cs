using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;

namespace Galaga;
public class Enemy : Entity {

    private int hitpoints = 3;
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
    }
    //enrage enemies when hirpoints are low 
    public void Enrage() {
        if (hitpoints == 1) {
            Image = redImage;
        }
        
    }
        
}
using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public abstract class PowerUp : Entity {
    
    //constructor for block
    public PowerUp(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }

    public void Move() {
        Shape.MoveY(-0.005f);
    }


    public abstract void PowerUpEffect() ;

    public abstract void PowerDownEffect() ;

    
}
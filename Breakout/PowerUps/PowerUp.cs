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

    public abstract void PowerUpEffect() ;

    
}
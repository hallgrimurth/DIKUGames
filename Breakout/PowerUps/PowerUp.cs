using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public abstract class PowerUp : Entity {

    private static Vec2F direction = new Vec2F(0.0f, 0.0f);
    public static Vec2F Direction {
       get {return direction; }
    }
    
    //constructor for block
    public PowerUp(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }

    public void Move() {
        base.Shape.Move();
    }

    public Vec2F GetDirection(){
        return base.Shape.AsDynamicShape().Direction;
    }

    public void ChangeDirection(Vec2F newDir) {
        base.Shape.AsDynamicShape().ChangeDirection(newDir);
    }

    public abstract void PowerUpEffect() ;

    public abstract void PowerDownEffect() ;

    
}
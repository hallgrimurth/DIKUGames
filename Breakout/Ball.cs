using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;

namespace Breakout;

public class Ball : Entity {

<<<<<<< HEAD
    private static Vec2F extent = new Vec2F(0.030f, 0.030f);
    private static Vec2F direction = new Vec2F(0.01f, 0.005f);
=======
>>>>>>> master

    public Ball(DynamicShape shape, IBaseImage image) : base (shape, image) {
    }

    public void Move() {
        if (Shape.Position.X > 0.0f && Shape.Position.X + Shape.Extent.X< 1.0f
            && Shape.Position.Y > 0.0f && Shape.Position.Y + Shape.Extent.Y< 1.0f ) {
                base.Shape.Move();
            }
    }

    public Vec2F GetDirection(){
        return base.Shape.AsDynamicShape().Direction;
    }
    
    public void ChangeDirection(Vec2F newDir) {
        base.Shape.AsDynamicShape().ChangeDirection(newDir);
    }



   
}

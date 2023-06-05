using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

namespace Breakout;
public abstract class PowerUp : Entity, ICollidable {

    private static Vec2F direction = new Vec2F(0.0f, -0.01f);
   
    
    //constructor for block
    public PowerUp(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
            ChangeDirection(direction);
    }

    public void Move() {
        base.Shape.AsDynamicShape().Move();
    }

    public Vec2F GetDirection(){
        return base.Shape.AsDynamicShape().Direction;
    }

    public void ChangeDirection(Vec2F newDir) {
        base.Shape.AsDynamicShape().ChangeDirection(newDir);
    }

    public void Collision(CollisionData collisionData, ICollidable other) {
        if (collisionData.Collision) {
            PowerUpEffect();
        }
    }

    public abstract void PowerUpEffect() ;

    public abstract void PowerDownEffect() ;

    public void Update() {
        if (IsDeleted()) {
            return;
        }
        Move();
    }

    
}
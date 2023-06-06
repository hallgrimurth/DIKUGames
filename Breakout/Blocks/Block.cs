using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;


namespace Breakout;
public abstract class Block : Entity, ICollidable {

    private int health;
    int value ;
    public int Value {
        get { return value; }
    }

    public int Health {
        get { return health; }
        set { health = value; }
    }

    //constructor for block
    public Block(DynamicShape Shape, IBaseImage image) : base(Shape, image) {
        health = 1;
        value = 1;
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.StatusEvent,
                Message = "SUBSCRIBE_COLLISION_EVENT",
                StringArg1 = "BALL",
                From = this
            });
    }
        
    public abstract void DecreaseHealth() ;

    public abstract void TryDeleteEntity() ;

    public void Collision(CollisionData collisionData, ICollidable other) {
        if (collisionData.Collision) {
            DecreaseHealth();
        }
    }

    public void Update() {
        if (IsDeleted()) {
            return;
        }
    }
     
}
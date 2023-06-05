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

    public void TryDeleteEntity() {
        if (health < 1) {
            DeleteEntity();
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.StatusEvent,
                StringArg1 = "BALL",
                Message = "UNSUBSCRIBE_COLLISION_EVENT",
                From = this
            });

            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent,  
                Message = "ADD_POINTS",
                IntArg1 = this.value
            });
        }
    }

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
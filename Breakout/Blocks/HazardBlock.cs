using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;


namespace Breakout;
public class HazardBlock : Block {

    private int value;
    private int Health;

    //constructor for block
    public HazardBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        this.value = 20;
    }
    public override void TryDeleteEntity() {
        if (Health < 1) {
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

            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.StatusEvent,
                Message = "SPAWN_HAZARD",
                StringArg1 = Shape.Position.X.ToString(),
                StringArg2 = Shape.Position.Y.ToString(),
                From = this
            });
        }
    }
    public override void DecreaseHealth() {
        this.Health--;
        TryDeleteEntity();
    }
}
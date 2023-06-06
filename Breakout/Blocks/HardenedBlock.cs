using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;


namespace Breakout;
public class HardenedBlock : Block {
    private IBaseImage damageImage;
    //get for damage image
    public IBaseImage DamageImage {
        get { return damageImage; }
    }
    private int value;
    
    //constructor for block
    public HardenedBlock(DynamicShape Shape, IBaseImage image, IBaseImage DamageImage)
        : base(Shape, image) {
        this.value = 20;
        Health = 2;
        damageImage = DamageImage;
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
        }
    }
    public override void DecreaseHealth() {
        this.Health--;
        TryDeleteEntity();  
        
    }
        
}
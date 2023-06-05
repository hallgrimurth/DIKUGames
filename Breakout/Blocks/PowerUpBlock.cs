using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;


namespace Breakout;
public class PowerUpBlock : Block {
    private int value;
    
    public int Value {
        get { return value; }
    }
    //constructor for block
    public PowerUpBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        this.value = 20;
    }
    public override void DecreaseHealth() {
        TryDeleteEntity();
        this.Health--; 
        if (Health == 0) {
            DeleteEntity();
            // Register score event
            GameEvent AddScore = new GameEvent
            {
                EventType = GameEventType.PlayerEvent,  
                Message = "ADD_POINTS",
                IntArg1 = this.Value
            };
            BreakoutBus.GetBus().RegisterEvent(AddScore);
        }
    
    }

     
    
}
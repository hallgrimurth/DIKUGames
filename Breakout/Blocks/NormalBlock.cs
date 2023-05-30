using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;


namespace Breakout;
public class NormalBlock : Block {
    // private int value;
    // public int Value {
    //     get { return value; }
    // }
    
    //constructor for block
    public NormalBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        // value = 1;     
        
    }

    public override void DecreaseHealth() {
        this.Health--; 
        if (Health == 0) {
            DeleteEntity();
            // Register score event
            Console.WriteLine("normal block destroyed");
            GameEvent AddScore = new GameEvent
            {
                EventType = GameEventType.PlayerEvent,  
                Message = "ADD_SCORE",
                StringArg1 = this.ToString()
            };
            BreakoutBus.GetBus().RegisterEvent(AddScore);
        }
    
    }
        
}
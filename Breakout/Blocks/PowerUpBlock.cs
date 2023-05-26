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
    private PowerUp powerUp;
    
    public int Value {
        get { return value; }
    }
    //constructor for block
    public PowerUpBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        value = 10;
    }
    public override void DecreaseHealth() {
        this.Health--; 
        if (Health == 0) {
            DeleteEntity();
            // Register score event
            GameEvent AddScore = new GameEvent
            {
                EventType = GameEventType.ScoreEvent,
                Message = "ADD_SCORE",
                StringArg1 = this.ToString()
            };
            BreakoutBus.GetBus().RegisterEvent(AddScore);
            GameEvent AddPowerup = new GameEvent
            {
                EventType = GameEventType.StatusEvent,
                Message = "SPAWN_POWERUP",
                StringArg1 = this.Shape.Position.X.ToString(),
                
            };
            BreakoutBus.GetBus().RegisterEvent(AddPowerup);
            Console.WriteLine("Powerup sent");
            }
    
    }

    //render powerup
    public void Render(){
        powerUp.RenderEntity();
    }
     
    
}
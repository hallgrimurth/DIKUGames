using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using DIKUArcade.Events;

namespace Breakout;
public class LifePickUpPowerUp : PowerUp  {

    private double startTime;
    private double endTime;
    
    public LifePickUpPowerUp(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }

    public override void PowerUpEffect(){
        startTime = (int)StaticTimer.GetElapsedSeconds();
        endTime = startTime + 10;
        
        GameEvent LifePickUpEvent = (new GameEvent{
                        EventType = GameEventType.PlayerEvent, 
                        Message = "INCREASE_HEALTH" });
        BreakoutBus.GetBus().RegisterEvent(LifePickUpEvent);

    }

    public override void PowerDownEffect(){


        if ((int)StaticTimer.GetElapsedSeconds() > endTime){
            GameEvent NarrowPaddleEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "Narrow" });
            BreakoutBus.GetBus().RegisterEvent(NarrowPaddleEvent);
        }

       

    }
}
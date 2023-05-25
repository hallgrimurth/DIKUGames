using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using DIKUArcade.Events;

namespace Breakout;
public class WidePowerUp : PowerUp  {

    private double startTime;
    
    public WidePowerUp(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }

    public override void PowerUpEffect(){

        startTime = (int)StaticTimer.GetElapsedSeconds();

        GameEvent WidePaddleEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "Widen" });
        BreakoutBus.GetBus().RegisterEvent(WidePaddleEvent);

    }

    public override void PowerDownEffect(){


        if ((int)StaticTimer.GetElapsedSeconds() > startTime + 10){
            GameEvent NarrowPaddleEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "Narrow" });
            BreakoutBus.GetBus().RegisterEvent(NarrowPaddleEvent);
        }

       

    }
}
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
    private double endTime;
 
    public WidePowerUp(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }

    public override void PowerUpEffect(){
        startTime = (int)StaticTimer.GetElapsedSeconds();
        endTime = startTime + 10;
        //currTime = new GameTimer();
        GameEvent WidePaddleEvent = (new GameEvent{
                        EventType = GameEventType.PlayerEvent, 
                        Message = "WIDE_PADDLE" });
        BreakoutBus.GetBus().RegisterEvent(WidePaddleEvent);
    }

    public override void PowerDownEffect(){


        // if ((int)StaticTimer.GetElapsedSeconds() > endTime){
            GameEvent NarrowPaddleEvent = (new GameEvent{
                        EventType = GameEventType.PlayerEvent, 
                        Message = "NARROW_PADDLE" });
            BreakoutBus.GetBus().RegisterEvent(NarrowPaddleEvent);
        // }

       

    }
}
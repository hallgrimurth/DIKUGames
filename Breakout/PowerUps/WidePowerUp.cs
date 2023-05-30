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
    //private GameTimer currTime;
    
    public WidePowerUp(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }

    public override void PowerUpEffect(){
        // startTime = (int)StaticTimer.GetElapsedSeconds();
        // endTime = startTime + 10;
        //currTime = new GameTimer();

        GameEvent WidePaddleEvent = (new GameEvent{
                        EventType = GameEventType.PlayerEvent, 
                        Message = "Widen" });
        BreakoutBus.GetBus().RegisterEvent(WidePaddleEvent);
        // Console.WriteLine("Starttime", startTime);
        // Console.WriteLine("Endtime", endTime);

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
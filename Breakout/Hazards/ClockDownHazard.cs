using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using DIKUArcade.Events;

namespace Breakout;
public class ClockDownHazard : Hazard  {

    private double startTime;
    private double endTime;
    
    public ClockDownHazard(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        startTime = (int)StaticTimer.GetElapsedSeconds();
        endTime = startTime + 10;
        GameEvent ClockDownEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "CLOCK_DOWN" });
        BreakoutBus.GetBus().RegisterEvent(ClockDownEvent);
    }
    public override void HazardUpEffect(){

    } 

    public override void HazardDownEffect(){

    }
    
}
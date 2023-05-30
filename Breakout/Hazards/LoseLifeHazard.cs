using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using DIKUArcade.Events;

namespace Breakout;
public class LoseLifeHazard : Hazard  {

    private double startTime;
    private double endTime;
    
    public LoseLifeHazard(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }

    public override void HazardUpEffect(){
        startTime = (int)StaticTimer.GetElapsedSeconds();
        endTime = startTime + 10;
        
        GameEvent LoseLifeEvent = (new GameEvent{
                        EventType = GameEventType.PlayerEvent, 
                        Message = "DECREASE_HEALTH" });
        BreakoutBus.GetBus().RegisterEvent(LoseLifeEvent);

    }

    public override void HazardDownEffect(){
    }
}
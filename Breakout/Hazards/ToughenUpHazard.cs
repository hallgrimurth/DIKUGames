using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using DIKUArcade.Events;

namespace Breakout;
public class ToughenUpHazard : Hazard  {

    private double startTime;
    private double endTime;
    public ToughenUpHazard(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }
    

    public override void HazardUpEffect(){
        startTime = (int)StaticTimer.GetElapsedSeconds();
        endTime = startTime + 10;
        GameEvent ToughenUpEvent = (new GameEvent{
                        EventType = GameEventType.PlayerEvent, 
                        Message = "INCREASE_HEALTH" });
        BreakoutBus.GetBus().RegisterEvent(ToughenUpEvent);
    } 

    public override void HazardDownEffect(){
    }
    
}
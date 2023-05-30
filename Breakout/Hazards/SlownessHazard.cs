using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using DIKUArcade.Events;

namespace Breakout;
public class SlownessHazard : Hazard  {

    private double startTime;
    private double endTime;
    public SlownessHazard(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }
    

    public override void HazardUpEffect(){
        startTime = (int)StaticTimer.GetElapsedSeconds();
        endTime = startTime + 10;
        GameEvent SlownessEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "SLOW_MOVEMENT" });
        BreakoutBus.GetBus().RegisterEvent(SlownessEvent);
    } 

    public override void HazardDownEffect(){
        if ((int)StaticTimer.GetElapsedSeconds() > endTime){
            GameEvent NormalSpeedEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "NORMAL_MOVEMENT" });
            BreakoutBus.GetBus().RegisterEvent(NormalSpeedEvent);
        }
    }
    
}
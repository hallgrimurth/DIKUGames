using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using DIKUArcade.Events;

namespace Breakout;
public class SlimJimHazard : Hazard {

    private double startTime;
    private double endTime;
 
    public SlimJimHazard(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }

    public override void HazardUpEffect(){

        GameEvent SlimJimEvent = (new GameEvent{
                        EventType = GameEventType.PlayerEvent, 
                        Message = "Narrow" });
        BreakoutBus.GetBus().RegisterEvent(SlimJimEvent);

    }

    public override void HazardDownEffect(){
        if ((int)StaticTimer.GetElapsedSeconds() > endTime){
            GameEvent WidenPaddleEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "Widen" });
            BreakoutBus.GetBus().RegisterEvent(WidenPaddleEvent);
        }
    }
}
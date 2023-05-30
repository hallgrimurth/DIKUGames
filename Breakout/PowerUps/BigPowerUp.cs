using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using DIKUArcade.Events;

namespace Breakout;
public class BigPowerUp : PowerUp  {

    private double startTime;
    private double endTime;
    public BigPowerUp(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }
    

    public override void PowerUpEffect(){
        startTime = (int)StaticTimer.GetElapsedSeconds();
        endTime = startTime + 10;
        GameEvent BigBallEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "DOUBLE_SIZE" });
        BreakoutBus.GetBus().RegisterEvent(BigBallEvent);
    } 

    public override void PowerDownEffect(){
        if ((int)StaticTimer.GetElapsedSeconds() > endTime){
            GameEvent SmallBallEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "NORMAL_SIZE" });
            BreakoutBus.GetBus().RegisterEvent(SmallBallEvent);
        }
    }
    
}
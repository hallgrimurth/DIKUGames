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

    private int startTime;
    public BigPowerUp(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }
    

    public override void PowerUpEffect(){
        startTime = ((int)StaticTimer.GetElapsedSeconds());
        GameEvent BigBallEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "DOUBLE_SIZE" });
        BreakoutBus.GetBus().RegisterEvent(BigBallEvent);
    } 

    public override void PowerDownEffect(){
        if ((int)StaticTimer.GetElapsedSeconds() > startTime + 10){
            GameEvent SmallBallEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "NORMAL_SIZE" });
            BreakoutBus.GetBus().RegisterEvent(SmallBallEvent);
        }
    }
    
}
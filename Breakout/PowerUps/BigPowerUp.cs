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

    public BigPowerUp(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }
    

    public override void PowerUpEffect(){
        GameEvent BigBallEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "DOUBLE_SIZE" });
        BreakoutBus.GetBus().RegisterEvent(BigBallEvent);
    } 

    public override void PowerDownEffect(){
        if (StaticTimer.GetElapsedSeconds() > 10.0f){
            GameEvent SmallBallEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "NORMAL_SIZE" });
            BreakoutBus.GetBus().RegisterEvent(SmallBallEvent);
        }
    }
    
}
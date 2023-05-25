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

<<<<<<< HEAD
    private StaticTimer timer;
    private double startTime;
=======
    // private StaticTimer timer;
>>>>>>> 3a5c7c17ce8369648c7aec1eded1f906bf740624
    
    public WidePowerUp(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
    }

    public override void PowerUpEffect(){

        startTime = StaticTimer.GetElapsedSeconds();

        GameEvent WidePaddleEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "Widen" });
        BreakoutBus.GetBus().RegisterEvent(WidePaddleEvent);

    }

    public override void PowerDownEffect(){


        if (StaticTimer.GetElapsedSeconds() > startTime + 10.0){
            GameEvent NarrowPaddleEvent = (new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message = "Narrow" });
            BreakoutBus.GetBus().RegisterEvent(NarrowPaddleEvent);
        }

       

    }
}
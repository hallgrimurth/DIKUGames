using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;

namespace Breakout;

public class Ball : Entity, IGameEventProcessor {

    private static Vec2F extent = new Vec2F(0.030f, 0.030f);
    private static Vec2F direction = new Vec2F(0.01f, 0.005f);
    // private static DynamicShape shape;// = new DynamicShape(new Vec2F(0.5f, 0.5f), extent, direction);
    public static Vec2F Extent {
       get {return extent; }
    }
    public static Vec2F Direction {
       get {return direction; }
    }

    public Ball(DynamicShape shape, IBaseImage image) : base (shape, image) {
        BreakoutBus.GetBus().Subscribe(GameEventType.MovementEvent, this);
    }

    public void Move() {
        if (Shape.Position.X > 0.0f && Shape.Position.X + Shape.Extent.X< 1.0f
            && Shape.Position.Y > 0.0f && Shape.Position.Y + Shape.Extent.Y< 1.0f ) {
                base.Shape.Move();
            }
    }

    public Vec2F GetDirection(){
        return base.Shape.AsDynamicShape().Direction;
    }
    
    public void ChangeDirection(Vec2F newDir) {
        base.Shape.AsDynamicShape().ChangeDirection(newDir);
    }

    public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.MovementEvent) {
                switch (gameEvent.Message) {
                    case "DOUBLE_SIZE":
                        Shape.Extent.X = 0.06f;
                        Shape.Extent.Y = 0.06f;
                        break;
                    case "NORMAL_SIZE":
                        Shape.Extent.X = 0.03f;
                        Shape.Extent.Y = 0.03f;
                        break;
                }
            }
        }
   
}

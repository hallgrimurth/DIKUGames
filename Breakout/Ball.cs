using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Breakout;
public class Ball : Entity {

    private static Vec2F extent = new Vec2F(0.030f, 0.030f);
    private static Vec2F direction = new Vec2F(0.01f, 0.005f);

    public Vec2F Extent {
        get { return extent; }
        set { extent = value; }
    }
    public Vec2F Direction {
        get { return direction; }
        set { direction = value; }
    }

    public Ball(Vec2F pos, IBaseImage image)
        : base(new DynamicShape(pos, extent), image) {
        Shape.AsDynamicShape().Direction = direction;
        // Shape.AsDynamicShape().Extent = extent;
    }
    
}

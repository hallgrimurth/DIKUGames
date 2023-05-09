using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Breakout;
public class Ball : Entity {

    // private DynamicShape shape;
    private static Vec2F extent = new Vec2F(0.05f, 0.05f);
    private static Vec2F direction = new Vec2F(0.0f, 0.005f);
    public Vec2F Direction {
        get { return direction; }
        set { direction = value; }
    }

    public Ball(Vec2F pos, IBaseImage image)
        : base(new DynamicShape(pos, extent), image) {
        Shape.AsDynamicShape().Direction = direction;
        // this.shape = new DynamicShape(pos, extent);
    }

}

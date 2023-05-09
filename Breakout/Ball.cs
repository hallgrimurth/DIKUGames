using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Breakout;
public class Ball : Entity {

    private static Vec2F extent = new Vec2F(0.008f, 0.021f);
    private static Vec2F direction = new Vec2F(0.0f, 0.05f);
    public Vec2F Direction {
        get { return direction; }
    }

    public Ball(Vec2F pos, IBaseImage image)
        : base(new DynamicShape(pos, extent), image) {
        Shape.AsDynamicShape().Direction = direction;
    }

}

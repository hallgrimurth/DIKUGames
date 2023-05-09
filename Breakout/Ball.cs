using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Breakout;
public class Ball : Entity {

    private DynamicShape shape;
    private static Vec2F extent = new Vec2F(0.008f, 0.021f);
    private static Vec2F direction = new Vec2F(0.0f, 0.05f);
    public Vec2F Direction {
        get { return direction; }
    }

    public Ball(Vec2F pos, IBaseImage image)
        : base(new DynamicShape(pos, extent), image) {
        Shape.AsDynamicShape().Direction = direction;
        this.shape = new DynamicShape(pos, extent);
    }

    public void Move() {
        shape.Move();

        if(shape.Position.X  < 0.0f) {
            shape.Position.X = 0.0f;
        } else if(shape.Position.X > 1.0f - shape.Extent.X) {
            shape.Position.X = - 1.0f - shape.Extent.X;
        } else if(shape.Position.Y > 1.0f - shape.Extent.X) {
            shape.Position.Y = - 1.0f - shape.Extent.Y;
        } else if(shape.Position.Y > 1.0f - shape.Extent.Y) {
            shape.Position.Y = - 1.0f - shape.Extent.Y;
        }
    }
}

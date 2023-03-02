using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Galaga;
public class PlayerShot : Entity {
    private static Vec2F extent = new Vec2F(0.008f, 0.021f);
    private static Vec2F direction = new Vec2F(0.0f, 0.05f);
    public Vec2F Direction {
        get { return direction; }
    }

    public PlayerShot(Vec2F pos, IBaseImage image)
        : base(new DynamicShape(pos, extent), image) {
        Shape.AsDynamicShape().Direction = direction;
    }
}

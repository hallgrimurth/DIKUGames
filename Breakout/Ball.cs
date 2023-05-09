using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Breakout;
public class Ball : Entity {

    public float speed;
    public DynamicShape shape;

    public Ball(DynamicShape Shape, IBaseImage image, float speed)
    : base(Shape, image ){
        this.shape = Shape;
        this.speed = speed;
    }

}

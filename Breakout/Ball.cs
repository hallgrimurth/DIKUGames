using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using System.IO;
using DIKUArcade.Physics;
using System;


namespace Breakout;

public class Ball : Entity, IGameEventProcessor, ICollidable {

    private static Vec2F extent = new Vec2F(0.030f, 0.030f);
    private static Vec2F direction = new Vec2F(0.01f, 0.005f);
    public static Vec2F Extent {
       get {return extent; }
    }
    public static Vec2F Direction {
       get {return direction; }
    }

    public Ball(DynamicShape shape) : base (shape, new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "ball.png"))) {

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

    public void Collision(CollisionData data, ICollidable collideWith) {
            WallCollision();
            if (data.Collision) {
                if (collideWith is Player) {
                    // Console.WriteLine("Player collision");
                    BallPlayerCollision(collideWith.Shape.Position.X, collideWith.Shape.Extent.X);
                } else {
                    // Console.WriteLine("Block collision");
                    BallBlockCollision(data.CollisionDir);
                }
            }
        }
    
    

    private void BallBlockCollision(CollisionDirection collisionDir) {
        var CollDir = ConvertDir(collisionDir);
        this.ChangeDirection(VectorOperations.Reflection(this.Shape.AsDynamicShape().Direction, CollDir));
    }

    private Vec2F ConvertDir(CollisionDirection CollDir){
            switch(CollDir){
                case CollisionDirection.CollisionDirDown:
                    return new Vec2F(0.0f, 1.0f);
                case CollisionDirection.CollisionDirUp:
                    return new Vec2F(0.0f, -1.0f);
                case CollisionDirection.CollisionDirLeft:
                    return new Vec2F(-1.0f, 0.0f);
                case CollisionDirection.CollisionDirRight:
                    return new Vec2F(1.0f, 0.0f);
            }
            return new Vec2F(0.0f, 0.0f);
        }

    private void BallPlayerCollision(float playerPosX, float playerExtentX){
        var normal = new Vec2F(0.0f, 1.0f);
        var x_bounce_directions = get_x_bounce_directions(playerPosX, playerExtentX);
        var targetVelocity = 0.01f;

        this.ChangeDirection(VectorOperations.Reflection(this.Shape.AsDynamicShape().Direction, normal));
        this.Shape.AsDynamicShape().Direction.X = x_bounce_directions;

        var ySquared = Math.Pow(targetVelocity, 2) - Math.Abs(Math.Pow(x_bounce_directions, 2));
        this.Shape.AsDynamicShape().Direction.Y = (float)Math.Sqrt(ySquared);
    }

    private float get_x_bounce_directions(float playerPosX, float playerExtentX){
            var relativeIntersectX = (this.Shape.Position.X - playerPosX);
            
            var normalizedRelativeIntersectionX = relativeIntersectX / (playerExtentX);
            var norm = (normalizedRelativeIntersectionX - 0.5f) * 2.0f; // range -1 to 1

            return norm * 0.01f; //to get a reasonable speed 
        }
    


    private void WallCollision(){

            var normal = new Vec2F(0.0f, 0.0f); 
            Vec2F dir = this.GetDirection();
            if (this.Shape.Position.Y + this.Shape.Extent.Y >= 0.98f) {
                normal = new Vec2F(0.0f, -1.0f);
                
                this.ChangeDirection(VectorOperations.Reflection(dir, normal));
                
            } else if (this.Shape.Position.X <= 0.02f) {
                normal = new Vec2F(1.0f, 0.0f);
               
                this.ChangeDirection(VectorOperations.Reflection(dir, normal));

            } else if (this.Shape.Position.X + this.Shape.Extent.X >= 0.98f) {
                normal = new Vec2F(-1.0f, 0.0f);
                
                this.ChangeDirection(VectorOperations.Reflection(dir, normal));
            }
        }

    private void SendCollisionData() {
        if (this.IsDeleted()) {
            return;
        }
        // Console.WriteLine("Try collide");
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "CHECK_COLLISION_EVENT",
            From = this,
            StringArg1 = "BALL"
        });
    }


    
    public void Update() {
        Move();
        WallCollision();
        SendCollisionData();

    }

    public void Render() {
        base.RenderEntity();
    }
   
}

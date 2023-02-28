using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {
    public class Player {
        private Entity entity;
        private DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private float moveUp = 0.0f;
        private float moveDown = 0.0f;
        const float MOVEMENT_SPEED = 0.01f;

        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }

        public void Move() {
            // TODO: move the shape and guard against the window borders
            shape.Move();

            if(shape.Position.X  < 0.0f) {
                shape.Position.X = 0.0f;
            } else if(shape.Position.X > 0.9f) {
                shape.Position.X = 0.9f;
            }

            if(shape.Position.Y < 0.0f) {
                shape.Position.Y = 0.0f;
            } else if(shape.Position.Y > 0.9f) {
                shape.Position.Y = 0.9f;
            }
         
        }

        public void SetMoveUp(bool val) {
            if(val){
                moveUp = MOVEMENT_SPEED;
            } else {
                moveUp = 0.0f;
            }
            UpdateDirection();
        }

        public void SetMoveDown(bool val) {
            if(val) {
                moveDown = -MOVEMENT_SPEED;
            } else {
                moveDown = 0.0f;
            }
            UpdateDirection();
            
        }

        public void SetMoveLeft(bool val) {
            if(val) {
                moveLeft = -MOVEMENT_SPEED;
            } else {
                moveLeft = 0.0f;
            }
            UpdateDirection();
        }

        public void SetMoveRight(bool val) {
            // TODO:set moveRight appropriately and call UpdateDirection()
            if(val) {
                moveRight = MOVEMENT_SPEED;
            } else {
                moveRight = 0.0f;
            }
            UpdateDirection();
        }

        private void UpdateDirection() {
            shape.Direction.X = moveLeft + moveRight;
            shape.Direction.Y = moveUp + moveDown;
        }
        public Shape Get_Pos() {
            return shape; //Temporary fix for centering the bullets
        }


        public void Render() {
            // TODO: render the player entity
            entity.RenderEntity();
        }
    }
}

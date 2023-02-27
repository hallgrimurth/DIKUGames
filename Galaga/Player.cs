using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga {
    public class Player {
        private Entity entity;
        private DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        const float MOVEMENT_SPEED = 0.01f;

        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }

        public void Move() {
            // TODO: move the shape and guard against the window borders
            shape.Move();
<<<<<<< HEAD

            if(shape.Position.X < 0.0f) {
                shape.Position.X = 0.0f;
            } else if(shape.Position.X > 0.9f) {
                shape.Position.X = 0.9f;
            }
         
        }

        public void SetMoveLeft(bool val) {
            if(val) {
=======
            if (shape.Position.X < 0.0f) {
                shape.Position.X = 0.0f;
            } else if (shape.Position.X > 0.9f) {
                shape.Position.X = 0.9f;
            }
        }

        public void SetMoveLeft(bool val) {
            if (val) {
>>>>>>> workingbranch
                moveLeft = -MOVEMENT_SPEED;
            } else {
                moveLeft = 0.0f;
            }
            UpdateDirection();
        }

        public void SetMoveRight(bool val) {
            // TODO:set moveRight appropriately and call UpdateDirection()
<<<<<<< HEAD
            if(val) {
=======
            if (val) {
>>>>>>> workingbranch
                moveRight = MOVEMENT_SPEED;
            } else {
                moveRight = 0.0f;
            }
            UpdateDirection();
        }

        private void UpdateDirection() {
<<<<<<< HEAD
            shape.Direction.X = moveLeft + moveRight;
=======
            shape.Direction.X =  moveLeft + moveRight;
>>>>>>> workingbranch
        }

        public void Render() {
            // TODO: render the player entity
            entity.RenderEntity();
        }
    }
}

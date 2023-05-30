using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System;

namespace Galaga {
    public class Player : IGameEventProcessor{
        private Entity entity;
        private DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private float moveUp = 0.0f;
        private float moveDown = 0.0f;
        private int lives = 1;
        const float MOVEMENT_SPEED = 0.01f;
        public DynamicShape Shape {
            get { return shape; }
        }

        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            GalagaBus.GetBus().Subscribe(GameEventType.MovementEvent, this);
        }

        public void Move() {
            shape.Move();

            if(shape.Position.X  < 0.0f) {
                shape.Position.X = 0.0f;
            } else if(shape.Position.X > 1.0f - shape.Extent.X) {
                shape.Position.X = 1.0f - shape.Extent.X;
            }

            if(shape.Position.Y < 0.0f) {
                shape.Position.Y = 0.0f;
            } else if(shape.Position.Y > 1.0f - shape.Extent.Y) {
                shape.Position.Y = 1.0f - shape.Extent.Y;
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
        public Shape GetPosition() {
            return shape;
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.MovementEvent) {
                Console.WriteLine("Player got event");
                switch (gameEvent.Message) {
                    case "MOVE_LEFT":
                        SetMoveLeft(true);
                        break;
                    case "MOVE_RIGHT":
                        SetMoveRight(true);
                        break;
                    case "MOVE_UP":
                        SetMoveUp(true);
                        break;
                    case "MOVE_DOWN":
                        SetMoveDown(true);
                        break;
                    case "STOP_LEFT":
                        SetMoveLeft(false);
                        break;
                    case "STOP_RIGHT":
                        SetMoveRight(false);
                        break;
                    case "STOP_UP":
                        SetMoveUp(false);
                        break;
                    case "STOP_DOWN":
                        SetMoveDown(false);
                        break;
                }
            }
        }

        private void DecreaseHealth(){
            this.lives--;
            if (lives <= 0) {

                    GameEvent gameover = (new GameEvent{
                            EventType = GameEventType.GameStateEvent, 
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_OVER"});
                    GalagaBus.GetBus().RegisterEvent(gameover);
                }
        }
           
            
        

        public void Render() {
            entity.RenderEntity();
        }
    }
}

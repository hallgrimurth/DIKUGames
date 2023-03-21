using DIKUArcade.State;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Physics;
using System;
using Galaga.Squadron;
using Galaga.MovementStrategy;



namespace Galaga.GalagaStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        //Entities
        // private GameEventBus eventBus;
        private Player player;
        private Health health;
        private ISquadron squad;
        private IMovementStrategy movementStrategy;
        private EntityContainer<PlayerShot> playerShots;
        private Score level;
        
        //Strides and Animations
        private List<Image> enemyStridesBlue;
        private List<Image> enemyStridesRed;
        private List<Image> explosionStrides;
        private AnimationContainer enemyExplosions;
        private IBaseImage playerShotImage;
        private List<Image> playerStrides;
        private const int EXPLOSION_LENGTH_MS = 500;
        private int numEnemies;
        private Random rand = new Random();




        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }

        public void InitializeGameState(){

            //Loading images
            playerShotImage = new Image(Path.Combine
                ("Assets", "Images", "BulletRed2.png"));
            explosionStrides = ImageStride.CreateStrides
                (8, Path.Combine("Assets", "Images", "Explosion.png"));
            playerStrides = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "FlightAnimation.png"));
            enemyStridesRed = ImageStride.CreateStrides
                (2, Path.Combine("Assets", "Images", "RedMonster.png"));
            enemyStridesBlue = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "BlueMonster.png"));

            //Adding Entities
            health = new Health(
                new Vec2F(0.75f, -0.2f), new Vec2F(0.4f, 0.4f));
            level = new Score(
                new Vec2F(0.75f, -0.3f), new Vec2F(0.4f, 0.4f));        
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(160, playerStrides));

            numEnemies = 8;
            squad = new Row(numEnemies);
            squad.CreateEnemies(enemyStridesBlue, enemyStridesRed);
            movementStrategy = new NoMove();
            playerShots = new EntityContainer<PlayerShot>();
            enemyExplosions = new AnimationContainer(numEnemies);
            // eventBus = GalagaBus.GetBus();
        }

        private void IterateEnemies(){

            List<ISquadron> FormationShape = new List<ISquadron> {new Row(numEnemies), new Wave(numEnemies), new Circle(numEnemies), new ZigZag(numEnemies)};
            List<IMovementStrategy> movementStrategies = new List<IMovementStrategy> {new Down(), new ZigZagDown(), new SideLoop()};
         

            // add enemies if there are none
            if (squad.Enemies.CountEntities() == 0){
                //change level
                level.AddPoint();
                //change squadron shape
                int random = rand.Next(FormationShape.Count);
                squad = FormationShape[random];
                //change movement strategy
                int random2 = rand.Next(movementStrategies.Count);
                movementStrategy = movementStrategies[random2];
                //create new wave
                squad.CreateEnemies(enemyStridesBlue, enemyStridesRed);
            }
            
            //move enemies
            movementStrategy.MoveEnemies(squad.Enemies);
        }

        private void IterateHealth() {
            // Check if player is out of health and end game if they are
            if (health.HealthPoints == 0) {
                level.FinalScore();
            }

            // Check if enemies are out of bounds and delete them if they are
            squad.Enemies.Iterate(enemy => {
                if (enemy.Shape.Extent.Y + enemy.Shape.Position.Y < 0.0f) {
                    health.LoseHealth();
                    enemy.DeleteEntity();
                }
            });
        }

        private void IterateShots() {
            playerShots.Iterate(shot => {
                shot.Shape.Move(shot.Direction); // Using the Direction property from PlayerShot.cs

                if (shot.Shape.Position.Y < 0.0f || shot.Shape.Position.Y > 1.0f || 
                    shot.Shape.Position.X < 0.0f || shot.Shape.Position.X > 1.0f) {
                    shot.DeleteEntity();
                } else {
                    squad.Enemies.Iterate(enemy => {
                        if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape)
                            .Collision) {
                            shot.DeleteEntity();
                            enemy.DecreaseHitpoints();
                            if (enemy.Hitpoints == 0) {
                                enemy.DeleteEntity();
                                AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                            }
                        }
                    });     
                }
            });
        }

        public void AddExplosion(Vec2F position, Vec2F extent){
            enemyExplosions.AddAnimation(
                new StationaryShape(position, extent), 
                80,
                new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides));
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
        
            switch(action){
                case KeyboardAction.KeyPress:
                    switch(key) {

                        case KeyboardKey.Left:
                            GameEvent MoveLeft = (new GameEvent{EventType = GameEventType.MovementEvent,  To = player, Message = "MOVE_LEFT" });
                            GalagaBus.GetBus().RegisterEvent(MoveLeft);
                            break;
                        case KeyboardKey.Right:
                            GameEvent MoveRight = (new GameEvent{EventType = GameEventType.MovementEvent,  To = player, Message = "MOVE_RIGHT" });
                            GalagaBus.GetBus().RegisterEvent(MoveRight);
                            break;
                        case KeyboardKey.Up:
                            GameEvent MoveUp = (new GameEvent{EventType = GameEventType.MovementEvent, To = player, Message = "MOVE_UP" });
                            GalagaBus.GetBus().RegisterEvent(MoveUp);
                            break;
                        case KeyboardKey.Down:
                            GameEvent MoveDown = (new GameEvent{EventType = GameEventType.MovementEvent,  To = player, Message = "MOVE_DOWN" });
                            GalagaBus.GetBus().RegisterEvent(MoveDown);
                            break;
                        case KeyboardKey.Escape:
                            GameEvent closeWindowEvent = new GameEvent{EventType = GameEventType.WindowEvent,  Message = "CLOSE_WINDOW"};
                            GalagaBus.GetBus().RegisterEvent(closeWindowEvent);
                            break;
                    }

                    break;

                case KeyboardAction.KeyRelease:
                    switch(key) {
                        case KeyboardKey.Left:
                            GameEvent StopLEft = (new GameEvent{EventType = GameEventType.MovementEvent,  To = player, Message = "STOP_LEFT" });
                            GalagaBus.GetBus().RegisterEvent(StopLEft);
                            break;
                        case KeyboardKey.Right:
                            GameEvent StopRight = (new GameEvent{EventType = GameEventType.MovementEvent,  To = player, Message = "STOP_RIGHT" });
                            GalagaBus.GetBus().RegisterEvent(StopRight);
                            break;
                        case KeyboardKey.Up:
                            GameEvent StopUp = (new GameEvent{EventType = GameEventType.MovementEvent, To = player, Message = "STOP_UP" });
                            GalagaBus.GetBus().RegisterEvent(StopUp);
                            break;
                        case KeyboardKey.Down:
                            GameEvent StopDown = (new GameEvent{EventType = GameEventType.MovementEvent,  To = player, Message = "STOP_DOWN" });
                            GalagaBus.GetBus().RegisterEvent(StopDown);
                            break;
                        case KeyboardKey.Space:
                            Vec2F pos = player.GetPosition().Position;
                            Vec2F ex = player.GetPosition().Extent;
                            playerShots.AddEntity(new PlayerShot(
                                new Vec2F(pos.X+(ex.X/2), pos.Y+(ex.Y/2)), playerShotImage));        
                            break;
                    }
                    break;        
            }
        }


        public void RenderState() {

            health.RenderHealth();
            player.Render();
            playerShots.RenderEntities();
            squad.Enemies.RenderEntities();
            enemyExplosions.RenderAnimations();
            level.Render();

        }

        public void ResetState(){
            throw new NotImplementedException();
        }

        public void UpdateState(){
            IterateHealth();
            IterateEnemies();
            IterateShots();
            player.Move();
        }


        
    }
}
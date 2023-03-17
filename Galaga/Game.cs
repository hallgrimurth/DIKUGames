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

namespace Galaga {
    public class Game : DIKUGame, IGameEventProcessor {
        private GameEventBus eventBus;
        private Player player;
        private Health health;
        private EntityContainer<Enemy> enemies;
        private List<Image> enemyStridesRed;
        private int numEnemies = 8;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;
        private Score score;
        private Random rand = new Random(); // For randomizing enemy speed
        private float enemySpeed = 0.0f; // For increasing speed of enemies
        public Game(WindowArgs windowArgs) : base(windowArgs) {

            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.WindowEvent, GameEventType.PlayerEvent, GameEventType.MovementEvent });
            window.SetKeyEventHandler(KeyHandler);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            eventBus.Subscribe(GameEventType.PlayerEvent, this);
            eventBus.Subscribe(GameEventType.MovementEvent, this);

            //Adding Health
            health = new Health( new Vec2F(0.75f, -0.2f), new Vec2F(0.4f, 0.4f));
            // Adding score
            score = new Score(new Vec2F(0.75f, -0.3f), new Vec2F(0.4f, 0.4f));

            // Adding player
            List<Image> playerimages = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "FlightAnimation.png"));
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(160, playerimages));

            // Adding Enemies
            enemyStridesRed = ImageStride.CreateStrides
                (2, Path.Combine("Assets", "Images", "RedMonster.png"));
            List<Image> BlueMonster = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
           
            // enemies = new EntityContainer<Enemy>(numEnemies);
            // // normal enemies in a row
            // for (int i = 0; i < numEnemies; i++) {
            //     enemies.AddEntity(new Enemy(
            //         new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
            //         new ImageStride(80, BlueMonster), new ImageStride(80, enemyStridesRed)));
            // }

            // // zigzag pattern
            // for (int i = 0; i < numEnemies; i++) {
            //     enemies.AddEntity(new Enemy(
            //         new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, (0.9f - ((float)i*0.1f%0.2f))), new Vec2F(0.1f, 0.1f)),
            //         new ImageStride(80, BlueMonster), new ImageStride(80, enemyStridesRed)));
            // }

            // // wave pattern
            // for (int i = 0; i < numEnemies; i++) {
            //     enemies.AddEntity(new Enemy(
            //         new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, (0.9f - (float)i*0.03f)), new Vec2F(0.1f, 0.1f)),
            //         new ImageStride(80, BlueMonster), new ImageStride(80, enemyStridesRed)));
            // }

            // // enemies in a circle
            // for (int i = 0; i < numEnemies; i++) {
            //     enemies.AddEntity(new Enemy(
            //         new DynamicShape(new Vec2F(0.45f + (float)Math.Cos(i*2*Math.PI/numEnemies)*0.2f, 0.6f + (float)Math.Sin(i*2*Math.PI/numEnemies)*0.2f), new Vec2F(0.1f, 0.1f)),
            //         new ImageStride(80, BlueMonster), new ImageStride(80, enemyStridesRed)));
            // }


            // enemies in columns on the left and right
            for (int i = 0; i < numEnemies/2; i++) {
                enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, BlueMonster), new ImageStride(40, enemyStridesRed)));
            }

      
            // Adding shooting functionality
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
                
            // Adding explosions
            enemyExplosions = new AnimationContainer(numEnemies);
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));

            
        }

        // Check for collisions and delete entities if they collide - also add point to score
        // If shot is out of bounds, delete it
        private void IterateShots() {
            playerShots.Iterate(shot => {
                shot.Shape.Move(shot.Direction); // Using the Direction property from PlayerShot.cs

                if (shot.Shape.Position.Y < 0.0f || shot.Shape.Position.Y > 1.0f || 
                    shot.Shape.Position.X < 0.0f || shot.Shape.Position.X > 1.0f) {
                    shot.DeleteEntity();
                } else {
                    enemies.Iterate(enemy => {
                        if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape)
                            .Collision) {
                            enemy.DecreaseHitpoints();
                            shot.DeleteEntity();
                            if (enemy.Hitpoints == 0) {
                                enemy.DeleteEntity();
                                AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                                // score.AddPoint();         
                            }
                            //change monster to red when hitpoints are low
                            if (enemy.Hitpoints == 1) {
                                enemy.Enrage();
                            }
                        }
                    });     
                }
            });
        }

        // Adding enemies when all enemies are dead and increasing their speed
        private void AddMoreEnemies() {
            List<Image> images = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "BlueMonster.png"));

            if (enemies.CountEntities() == 0) {
                score.AddPoint();
                enemySpeed += 0.0005f;
                for (int i = 0; i < numEnemies; i++) {
                    enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 1.0f), 
                            new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, images), new ImageStride(40, enemyStridesRed)));
                }
            }
        }

        // // Moving enemies down at random speeds and deleting them if they are out of bounds
        // // Also resetting score and enemy speed if enemy is out of bounds
        // private void MoveEnemiesDown() {
        //     enemies.Iterate(enemy => {
        //         float speed = enemySpeed + rand.Next(1, 100) / 25000.0f;
        //         enemy.Shape.MoveY(-speed);

        //        if (enemy.Shape.Position.Y < -0.1f) {
        //             enemy.DeleteEntity();
                    //score.ResetScore();
        //             enemySpeed = 0.0f;
        //         }
        //     });
        // }

        public void AddExplosion(Vec2F position, Vec2F extent){
            enemyExplosions.AddAnimation(
                new StationaryShape(position, extent), 
                80,
                new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides));
        }

        private void KeyPress(KeyboardKey key) {
            switch(key) {
                case KeyboardKey.Left:
                    GameEvent MoveLeft = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "MOVE_LEFT" });
                    eventBus.RegisterEvent(MoveLeft);
                    break;
                case KeyboardKey.Right:
                    GameEvent MoveRight = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "MOVE_RIGHT" });
                    eventBus.RegisterEvent(MoveRight);
                    break;
                case KeyboardKey.Up:
                    GameEvent MoveUp = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "MOVE_UP" });
                    eventBus.RegisterEvent(MoveUp);
                    break;
                case KeyboardKey.Down:
                    GameEvent MoveDown = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "MOVE_DOWN" });
                    eventBus.RegisterEvent(MoveDown);
                    break;
                case KeyboardKey.Escape:
                    GameEvent closeWindowEvent = new GameEvent{EventType = GameEventType.WindowEvent, To = this, Message = "CLOSE_WINDOW"};
                    eventBus.RegisterEvent(closeWindowEvent);
                    break;
            }     
        }
        

        private void KeyRelease(KeyboardKey key) {
            switch(key) {
                case KeyboardKey.Left:
                    GameEvent StopLEft = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "STOP_LEFT" });
                    eventBus.RegisterEvent(StopLEft);
                    // player.SetMoveLeft(false);
                    break;
                case KeyboardKey.Right:
                    GameEvent StopRight = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "STOP_RIGHT" });
                    eventBus.RegisterEvent(StopRight);
                    // player.SetMoveRight(false);
                    break;
                case KeyboardKey.Up:
                    GameEvent StopUp = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "STOP_UP" });
                    eventBus.RegisterEvent(StopUp);
                    // player.SetMoveUp(false);
                    break;
                case KeyboardKey.Down:
                    GameEvent StopDown = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "STOP_DOWN" });
                    eventBus.RegisterEvent(StopDown);
                    // player.SetMoveDown(false);
                    break;
                case KeyboardKey.Space:
                    Vec2F pos = player.GetPosition().Position;
                    Vec2F ex = player.GetPosition().Extent;
                    playerShots.AddEntity(new PlayerShot(
                        new Vec2F(pos.X+(ex.X/2), pos.Y+(ex.Y/2)), playerShotImage));        
                    break;
            }
        }

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            switch(action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;
            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                    case "CLOSE_WINDOW":
                        window.CloseWindow();
                    break;
                }
            }
        }
            
            

        public override void Render() {
            window.Clear();
            health.RenderHealth();
            player.Render();
            playerShots.RenderEntities();
            enemies.RenderEntities();
            enemyExplosions.RenderAnimations();
            score.Render();

        }

        public override void Update() {
            IterateShots();
            eventBus.ProcessEvents();
            window.PollEvents();
            eventBus.ProcessEventsSequentially();
            player.Move();
            // AddMoreEnemies();
            // MoveEnemiesDown();
        }
    }
}

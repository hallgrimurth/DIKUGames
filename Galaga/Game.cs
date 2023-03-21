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
using Galaga.GalagaStates;



namespace Galaga {
    public class Game : DIKUGame, IGameEventProcessor {

        //Entities
        private GameEventBus eventBus;
        private Player player;
        private Health health;
        private ISquadron squad;
        private IMovementStrategy movementStrategy;
        private EntityContainer<PlayerShot> playerShots;
        private Score score;
        private Random rand = new Random(); // For randomizing enemy speed

        //Strides and Animations
        private List<Image> enemyStridesBlue;
        private List<Image> enemyStridesRed;
        private List<Image> explosionStrides;
        private AnimationContainer enemyExplosions;
        private IBaseImage playerShotImage;
        private List<Image> playerStrides;

        //Adjustable variables
        private int numEnemies = 8;
        private const int EXPLOSION_LENGTH_MS = 500;
        private List<GameEventType> eventQueue;
        private bool GameOver = false;

        public Game(WindowArgs windowArgs) : base(windowArgs) {

            // //Loading images
            // playerShotImage = new Image(Path.Combine
            //     ("Assets", "Images", "BulletRed2.png"));
            // explosionStrides = ImageStride.CreateStrides
            //     (8, Path.Combine("Assets", "Images", "Explosion.png"));
            // playerStrides = ImageStride.CreateStrides
            //     (4, Path.Combine("Assets", "Images", "FlightAnimation.png"));
            // enemyStridesRed = ImageStride.CreateStrides
            //     (2, Path.Combine("Assets", "Images", "RedMonster.png"));
            // enemyStridesBlue = ImageStride.CreateStrides
            //     (4, Path.Combine("Assets", "Images", "BlueMonster.png"));


            //Instantiation of the StateMachine

            StateMachine stateMachine = new StateMachine();
            //Setting up eventbus and subscribing to events
            eventBus = new GameEventBus();
            eventQueue = new List<GameEventType> { GameEventType.InputEvent, GameEventType.WindowEvent, GameEventType.PlayerEvent, GameEventType.MovementEvent };
            eventBus.InitializeEventBus(eventQueue);
            // window.SetKeyEventHandler(KeyHandler);
            for(int i = 0; i < eventQueue.Count; i++) {
                eventBus.Subscribe(eventQueue[i], this);
            }

            // //Adding Entities
            // health = new Health(
            //     new Vec2F(0.75f, -0.2f), new Vec2F(0.4f, 0.4f));
            // score = new Score(
            //     new Vec2F(0.75f, -0.3f), new Vec2F(0.4f, 0.4f));        
            // player = new Player(
            //     new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            //     new ImageStride(160, playerStrides));
            // squad = new Row(numEnemies);
            // squad.CreateEnemies(enemyStridesBlue, enemyStridesRed);
            // movementStrategy = new NoMove();
            // playerShots = new EntityContainer<PlayerShot>();
            // enemyExplosions = new AnimationContainer(numEnemies);

        }

        // private void IterateEnemies(){

        //     List<ISquadron> FormationShape = new List<ISquadron> {new Row(numEnemies), new Wave(numEnemies), new Circle(numEnemies), new ZigZag(numEnemies)};
        //     List<IMovementStrategy> movementStrategies = new List<IMovementStrategy> {new Down(), new ZigZagDown(), new SideLoop()};
         

        //     // add enemies if there are none
        //     if (squad.Enemies.CountEntities() == 0){
        //         //change level
        //         score.AddPoint();
        //         //change squadron shape
        //         int random = rand.Next(FormationShape.Count);
        //         squad = FormationShape[random];
        //         //change movement strategy
        //         int random2 = rand.Next(movementStrategies.Count);
        //         movementStrategy = movementStrategies[random2];
        //         //create new wave
        //         squad.CreateEnemies(enemyStridesBlue, enemyStridesRed);
        //     }
            
        //     //move enemies
        //     movementStrategy.MoveEnemies(squad.Enemies);
        // }

        // private void IterateHealth() {
        //     // Check if player is out of health and end game if they are
        //     if (health.HealthPoints == 0) {
        //         GameOver = true;
        //         score.FinalScore();
        //     }

        //     // Check if enemies are out of bounds and delete them if they are
        //     squad.Enemies.Iterate(enemy => {
        //         if (enemy.Shape.Extent.Y + enemy.Shape.Position.Y < 0.0f) {
        //             health.LoseHealth();
        //             enemy.DeleteEntity();
        //         }
        //     });
        // }

        // // Check for collisions and delete entities if they collide - also add point to score
        // // If shot is out of bounds, delete it
        // private void IterateShots() {
        //     playerShots.Iterate(shot => {
        //         shot.Shape.Move(shot.Direction); // Using the Direction property from PlayerShot.cs

        //         if (shot.Shape.Position.Y < 0.0f || shot.Shape.Position.Y > 1.0f || 
        //             shot.Shape.Position.X < 0.0f || shot.Shape.Position.X > 1.0f) {
        //             shot.DeleteEntity();
        //         } else {
        //             squad.Enemies.Iterate(enemy => {
        //                 if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape)
        //                     .Collision) {
        //                     shot.DeleteEntity();
        //                     enemy.DecreaseHitpoints();
        //                     if (enemy.Hitpoints == 0) {
        //                         enemy.DeleteEntity();
        //                         AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
        //                     }
        //                 }
        //             });     
        //         }
        //     });
        // }

        // public void AddExplosion(Vec2F position, Vec2F extent){
        //     enemyExplosions.AddAnimation(
        //         new StationaryShape(position, extent), 
        //         80,
        //         new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides));
        // }

        // private void KeyPress(KeyboardKey key) {
        //     switch(key) {
        //         case KeyboardKey.Left:
        //             GameEvent MoveLeft = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "MOVE_LEFT" });
        //             eventBus.RegisterEvent(MoveLeft);
        //             break;
        //         case KeyboardKey.Right:
        //             GameEvent MoveRight = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "MOVE_RIGHT" });
        //             eventBus.RegisterEvent(MoveRight);
        //             break;
        //         case KeyboardKey.Up:
        //             GameEvent MoveUp = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "MOVE_UP" });
        //             eventBus.RegisterEvent(MoveUp);
        //             break;
        //         case KeyboardKey.Down:
        //             GameEvent MoveDown = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "MOVE_DOWN" });
        //             eventBus.RegisterEvent(MoveDown);
        //             break;
        //         case KeyboardKey.Escape:
        //             GameEvent closeWindowEvent = new GameEvent{EventType = GameEventType.WindowEvent, To = this, Message = "CLOSE_WINDOW"};
        //             eventBus.RegisterEvent(closeWindowEvent);
        //             break;
        //     }     
        // }
        

        // private void KeyRelease(KeyboardKey key) {
        //     switch(key) {
        //         case KeyboardKey.Left:
        //             GameEvent StopLEft = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "STOP_LEFT" });
        //             eventBus.RegisterEvent(StopLEft);
        //             break;
        //         case KeyboardKey.Right:
        //             GameEvent StopRight = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "STOP_RIGHT" });
        //             eventBus.RegisterEvent(StopRight);
        //             break;
        //         case KeyboardKey.Up:
        //             GameEvent StopUp = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "STOP_UP" });
        //             eventBus.RegisterEvent(StopUp);
        //             break;
        //         case KeyboardKey.Down:
        //             GameEvent StopDown = (new GameEvent{EventType = GameEventType.MovementEvent, From = this, To = player, Message = "STOP_DOWN" });
        //             eventBus.RegisterEvent(StopDown);
        //             break;
        //         case KeyboardKey.Space:
        //             Vec2F pos = player.GetPosition().Position;
        //             Vec2F ex = player.GetPosition().Extent;
        //             playerShots.AddEntity(new PlayerShot(
        //                 new Vec2F(pos.X+(ex.X/2), pos.Y+(ex.Y/2)), playerShotImage));        
        //             break;
        //     }
        // }

        // private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        //     switch(action) {
        //         case KeyboardAction.KeyPress:
        //             KeyPress(key);
        //             break;
        //         case KeyboardAction.KeyRelease:
        //             KeyRelease(key);
        //             break;
        //     }
        // }

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
            if (GameOver) {
                score.Render();
            }
            else{
                window.Clear();
                // health.RenderHealth();
                // player.Render();
                // playerShots.RenderEntities();
                // squad.Enemies.RenderEntities();
                // enemyExplosions.RenderAnimations();
                // score.Render();
            }

        }
        

        public override void Update() {
            //make new window and display game over text
            if (GameOver) {
                squad.Enemies.ClearContainer();
                playerShots.ClearContainer();
                eventBus.ProcessEvents();
            }

            else{
                // IterateHealth();
                // IterateEnemies();
                // IterateShots();
                window.PollEvents();
                eventBus.ProcessEventsSequentially();
                // player.Move();
               // AddMoreEnemies();
                // MoveEnemiesDown();
            }
        }

    }
}

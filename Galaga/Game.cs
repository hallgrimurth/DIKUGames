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



namespace Galaga
{
    public class Game : DIKUGame, IGameEventProcessor {
        private GameEventBus eventBus;
        private Player player;
        private EntityContainer<Enemy> enemies;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;



        public Game(WindowArgs windowArgs) : base(windowArgs) {

            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
            window.SetKeyEventHandler(KeyHandler);
            eventBus.Subscribe(GameEventType.InputEvent, this);

            //Adding player
            List<Image> playerimages = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "FlightAnimation.png"));
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(160, playerimages));

            //Adding Enemies
            List<Image> images = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            const int numEnemies = 8;
            enemies = new EntityContainer<Enemy>(numEnemies);
            for (int i = 0; i < numEnemies; i++) {
                enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, images)));
            }
      
            //Adding shooting functionality
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
                
            //Adding explosions
            enemyExplosions = new AnimationContainer(numEnemies);
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
        }

        private void IterateShots() {
            playerShots.Iterate(shot => {
                // TODO: move the shot's shape
                shot.Shape.Move(new Vec2F(0.0f, 0.1f));//How to use direction to move the shot?

                if (shot.Shape.Position.Y > 1.0f ) {
                    // TODO: delete shot
                    shot.DeleteEntity();
                } else {
                    enemies.Iterate(enemy => {

                        // TODO: if collision btw shot and enemy -> delete both entities
                        if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {
                            enemy.DeleteEntity();
                            shot.DeleteEntity();
                            AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                        }
                    });     
                }
            });
        }

        public void AddExplosion(Vec2F position, Vec2F extent){
            // TODO: add explosion to the AnimationContainer
            enemyExplosions.AddAnimation(
                new StationaryShape(position, extent), 
                80,
                new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides));
        }
                
        private void KeyPress(KeyboardKey key) {
            // TODO: Close window if escape is pressed
            // TODO: switch on key string and set the player's move direction
            switch(key) {
                case KeyboardKey.Left:
                    player.SetMoveLeft(true);
                    break;
                case KeyboardKey.Right:
                    player.SetMoveRight(true);
                    break;
                case KeyboardKey.Up:
                    player.SetMoveUp(true);
                    break;
                case KeyboardKey.Down:
                    player.SetMoveDown(true);
                    break;
                case KeyboardKey.Escape:
                    window.CloseWindow();
                    break;
            }     
        }

        private void KeyRelease(KeyboardKey key) {
            // TODO: switch on key string and disable the player's move direction
            switch(key) {
                case KeyboardKey.Left:
                    player.SetMoveLeft(false);
                    break;
                case KeyboardKey.Right:
                    player.SetMoveRight(false);
                    break;
                case KeyboardKey.Up:
                    player.SetMoveUp(false);
                    break;
                case KeyboardKey.Down:
                    player.SetMoveDown(false);
                    break;
                case KeyboardKey.Space:
                    Vec2F pos = player.Get_Pos().Position;
                    Vec2F ex = player.Get_Pos().Extent;
                    playerShots.AddEntity(new PlayerShot(
                        new Vec2F(pos.X+(ex.X/2), pos.Y), playerShotImage));        
                    break;
            }
        }

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            // TODO: Switch on KeyBoardAction and call proper method
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
            // Leave this empty for now
        }

        public override void Render() {
            //TODO: Render Game Entities
            window.Clear();
            player.Render();
            playerShots.RenderEntities();
            enemies.RenderEntities();
            enemyExplosions.RenderAnimations();
        }

        public override void Update() {
            IterateShots();
            // window.PollEvents();
            player.Move();
        }
    }
}

using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;


namespace Galaga
{
    public class Game : DIKUGame, IGameEventProcessor {
        private GameEventBus eventBus;
        private Player player;
        private EntityContainer<Enemy> enemies;


        public Game(WindowArgs windowArgs) : base(windowArgs) {

            // player = new Player(
            //     new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            //     new Image(Path.Combine("Assets", "Images", "Player.png")));
            List<Image> playerimages = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "PlayerAnimation.png"));
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, playerimages));


            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
            window.SetKeyEventHandler(KeyHandler);
            eventBus.Subscribe(GameEventType.InputEvent, this);

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
            player.Render();
            enemies.RenderEntities();
        }
        public override void Update() {
            window.PollEvents();
            player.Move();
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.State;

namespace Breakout{
    class Game : DIKUGame, IGameEventProcessor {
        //state machine
        // private StateMachine stateMachine ;
        //Entities
        // private GameEventBus eventBus;
        private string fileName;
        private string path;
        private Player player;
        private LevelManager level;
        private GameEventBus eventBus;
        private List<GameEventType> eventQueue;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            fileName = "level3.txt";
            path = Path.Combine(Environment.CurrentDirectory, "Breakout/Assets/Levels/", fileName);
            level = new LevelManager();
            

            level.LoadMap(path);
            level.LoadMapEntities();
        
            //define player 
            player = new Player();

            //define event bus
            eventBus = BreakoutBus.GetBus();
            eventQueue = new List<GameEventType> {
                GameEventType.InputEvent,
                GameEventType.WindowEvent,
                GameEventType.MovementEvent,
                GameEventType.GameStateEvent
            };
            eventBus.InitializeEventBus(eventQueue);
            //subscribe to event bus
            for (int i = 0; i < eventQueue.Count; i++) {
                eventBus.Subscribe(eventQueue[i], this);
            }

            //set key event handler
            window.SetKeyEventHandler(HandleKeyEvent);
        } 

        //setting up the player characterics 
        // public Player GetPlayer(){
        //     playerStride = new Image(Path.Combine(Environment.CurrentDirectory, "Breakout/Assets/Images/player.png"));
        //     var playerPos = new Vec2F(0.4f, 0.05f);
        //     var playerExtent = new Vec2F(0.2f, 0.03f);
        //     var playerShape = new DynamicShape(Position, Extent);
        //     player = new Player(playerPos, playerStride);
        //     return player;
        // }

        //process event types
        public void ProcessEvent(GameEvent gameEvent) {  
             switch (gameEvent.EventType) {
                case GameEventType.WindowEvent:
                //send message to state machine
                    window.CloseWindow();
                    break;

                // case GameEventType.GameStateEvent:
                //     stateMachine.ProcessEvent(gameEvent);
                //     window.SetKeyEventHandler(stateMachine.ActiveState.HandleKeyEvent);
                //     break;
            }    
        }

        //invokes proper game event when specified key is pressed
         public void KeyPress(KeyboardKey key){
            switch(key) {
                case KeyboardKey.Left:
                    GameEvent MoveLeft = (new GameEvent{
                        EventType = GameEventType.MovementEvent,  To = player, 
                        Message = "MOVE_LEFT" });
                    eventBus.RegisterEvent(MoveLeft);
                   
                    break;
                case KeyboardKey.Right:
                     GameEvent MoveRight = (new GameEvent{
                        EventType = GameEventType.MovementEvent,  To = player, 
                        Message = "MOVE_RIGHT" });
                    eventBus.RegisterEvent(MoveRight);
                        
                    break;

                // case KeyboardKey.C:
                //     GameEvent closeWindowEvent = new GameEvent{
                //         EventType = GameEventType.WindowEvent,  Message = "CLOSE_WINDOW"};
                    
                //     break;               
                    }
        }

        //invokes proper game event when specified key is released
        public void KeyRelease(KeyboardKey key){
            switch(key){
                case KeyboardKey.Left:
                    GameEvent StopLeft = (new GameEvent{
                        EventType = GameEventType.MovementEvent,  To = player, 
                        Message = "STOP_LEFT" });
                    eventBus.RegisterEvent(StopLeft);
                    break;

                case KeyboardKey.Right:
                    GameEvent StopRight = (new GameEvent{
                        EventType = GameEventType.MovementEvent,  To = player, 
                        Message = "STOP_RIGHT" });
                    eventBus.RegisterEvent(StopRight);
                    break;
                
                // case KeyboardKey.Space:
                //     Vec2F pos = player.GetPosition().Position;
                //     Vec2F ex = player.GetPosition().Extent;
                //     playerShotImage = new Image(Path.Combine
                //         ("Assets", "Images", "BulletRed2.png"));
                //     playerShots.AddEntity(new PlayerShot(
                //         new Vec2F(pos.X+(ex.X/2), pos.Y+(ex.Y/2)), playerShotImage));      
                //     break;
                case KeyboardKey.Escape:
                    eventBus.RegisterEvent(
                        new GameEvent{
                            EventType = GameEventType.WindowEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_PAUSED"
                        }
                    );
                    break;
            }
        }

        // handles key events
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
        
            switch(action){
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;

                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;        
            }
        }
        public override void Render() {
            window.Clear();
            level.blocks.RenderEntities();
            player.Render();
            // stateMachine.ActiveState.RenderState();
        }
        
        public override void Update() {
            //make new window and display game over text
            // stateMachine.ActiveState.UpdateState();
            window.PollEvents();
            player.Move();

            eventBus.ProcessEventsSequentially();
        }
    }
}
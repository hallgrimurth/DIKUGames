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
using Breakout.BreakoutStates;

namespace Breakout{
    class Game : DIKUGame, IGameEventProcessor {
        //state machine
        private StateMachine stateMachine;
        //Entities
        private GameEventBus eventBus;
        private string fileName;
        private string path;
        private Player player;
        private LevelManager level;
        private List<GameEventType> eventQueue;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
<<<<<<< HEAD
            // fileName = "firstLine.txt";
            // path = Path.Combine(Environment.CurrentDirectory, "Breakout/Assets/Levels/", fileName);
=======
            fileName = "firstLine.txt";
            path = Path.Combine(Constants.MAIN_PATH, "Breakout/Assets/Levels/", fileName);
>>>>>>> 2bed3188cfc16d0c173682e2e789637f79856d1f
            level = new LevelManager();
            var levelPaths = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Breakout/Assets/Levels/"));

            //write level to console
            // foreach (var level in levelPaths) {
            //     Console.WriteLine(level);
            // }

            level.LoadMap(levelPaths[3]);
        
            //define player 
            player = new Player();

            //define event bus
            eventBus = BreakoutBus.GetBus();
            stateMachine = new StateMachine();
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
            window.SetKeyEventHandler(stateMachine.ActiveState.HandleKeyEvent);
        } 
        //process event types
        public void ProcessEvent(GameEvent gameEvent) {  
             switch (gameEvent.EventType) {
                case GameEventType.WindowEvent:
                //send message to state machine
                    window.CloseWindow();
                    break;
            }    
        }

        public override void Render() {
            window.Clear();
            level.blocks.RenderEntities();
            stateMachine.ActiveState.RenderState();
        }
        
        public override void Update() {
            //make new window and display game over text
            stateMachine.ActiveState.UpdateState();
            window.PollEvents();
            BreakoutBus.GetBus().ProcessEventsSequentially();
        }
    }
}
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

namespace Breakout{
    class Game : DIKUGame {
        //state machine
        private StateMachine stateMachine ;
        //Entities
        private GameEventBus eventBus;
        private List<GameEventType> eventQueue;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            //Setting up eventbus and subscribing to events
            eventBus = Breakout.GetBus();
            stateMachine = new StateMachine();
            eventQueue = new List<GameEventType> {GameEventType.WindowEvent, GameEventType.GameStateEvent, GameEventType.MovementEvent }; 
            eventBus.InitializeEventBus(eventQueue);
            window.SetKeyEventHandler(stateMachine.ActiveState.HandleKeyEvent);
            for(int i = 0; i < eventQueue.Count; i++) {
                eventBus.Subscribe(eventQueue[i], this);
            }
        } 

        public void ProcessEvent(GameEvent gameEvent) {  
             switch (gameEvent.EventType) {
                case GameEventType.WindowEvent:
                //send message to state machine
                    window.CloseWindow();
                    break;

                case GameEventType.GameStateEvent:
                    stateMachine.ProcessEvent(gameEvent);
                    window.SetKeyEventHandler(stateMachine.ActiveState.HandleKeyEvent);
                    break;
            }    
        }

        public override void Render() {
            window.Clear();
            stateMachine.ActiveState.RenderState();
        }
        
        public override void Update() {
            //make new window and display game over text
            stateMachine.ActiveState.UpdateState();
            window.PollEvents();
            Breakout.GetBus().ProcessEventsSequentially();
        }
    }
}
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
        private List<GameEventType> eventQueue;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
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
            BreakoutBus.GetBus().ProcessEventsSequentially();
        }
    }
}
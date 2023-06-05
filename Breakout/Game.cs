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
    public class Game : DIKUGame, IGameEventProcessor {
        //state machine
        private StateMachine stateMachine;
        //Entities
        private List<GameEventType> eventQueue;
        private Timer timer;
      
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            //define event bus
            stateMachine = new StateMachine();
            timer = new Timer();

            
            InitializeEventBus(eventQueue);

            window.SetKeyEventHandler(stateMachine.ActiveState.HandleKeyEvent);

        } 

        //Initialize Event Bus
        public void InitializeEventBus(List<GameEventType> eventQueue) {
            
            // BreakoutBus.GetBus().InitializeEventBus(eventQueue);
            //subscribe to events
            BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);

            // for (int i = 0; i < eventQueue.Count-1; i++) {
            //     BreakoutBus.GetBus().Subscribe(eventQueue[i], this);
            
        }
        
        //process event types
        public void ProcessEvent(GameEvent gameEvent) {  
             switch (gameEvent.EventType) {
                case GameEventType.PlayerEvent:
                    // Console.WriteLine(gameEvent.Message);
                    break;
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
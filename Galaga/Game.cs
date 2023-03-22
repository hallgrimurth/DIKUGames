
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;

using System.Collections.Generic;
using DIKUArcade.Physics;
using System;
using Galaga.Squadron;
using Galaga.MovementStrategy;
using Galaga.GalagaStates;
using DIKUArcade.State;




namespace Galaga {
    public class Game : DIKUGame, IGameEventProcessor {

        //state machine
        private StateMachine stateMachine ;
        //Entities
        private GameEventBus eventBus;
        private List<GameEventType> eventQueue;

        public Game(WindowArgs windowArgs) : base(windowArgs) {

        
            //Setting up eventbus and subscribing to events
            eventBus = GalagaBus.GetBus();
            stateMachine = new StateMachine();
            eventQueue = new List<GameEventType> { GameEventType.InputEvent, GameEventType.WindowEvent, GameEventType.PlayerEvent, GameEventType.MovementEvent, GameEventType.GameStateEvent };
            eventBus.InitializeEventBus(eventQueue);
            window.SetKeyEventHandler(stateMachine.ActiveState.HandleKeyEvent);
            for(int i = 0; i < eventQueue.Count; i++) {
                eventBus.Subscribe(eventQueue[i], this);
            }

        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                    case "CLOSE_WINDOW":
                        System.Console.WriteLine("CLOSE_WINDOW message received in game");
                        window.CloseWindow();
                        break;
                }
            if (gameEvent.EventType == GameEventType.GameStateEvent){
                switch (gameEvent.Message) {
                    case "CHANGE_STATE":
                        System.Console.WriteLine("CHANGE_STATE message received in game");
                        break;
                }
            }
 
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
            GalagaBus.GetBus().ProcessEventsSequentially();

            }
        }

    }


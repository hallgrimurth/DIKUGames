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
                        window.CloseWindow();
                    break;
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
            eventBus.ProcessEventsSequentially();

            }
        }

    }


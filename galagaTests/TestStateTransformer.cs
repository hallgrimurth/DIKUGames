using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using System.Collections.Generic;
using Galaga.GalagaStates;
using Galaga;
using System;


namespace GalagaTests {
    [TestFixture]
    public class TestingState {
        private StateMachine stateMachine;

        
        [SetUp]
        public void InitiateStateMachine() {
        
            Window.CreateOpenGLContext();
            // (1) Initialize a GalagaBus with proper GameEventTypes
            var eventQueue = new List<GameEventType> { GameEventType.InputEvent, GameEventType.GameStateEvent };
            // GalagaBus.GetBus().InitializeEventBus(eventQueue);

            // (2) Instantiate the StateMachine
            stateMachine = new StateMachine();
            // (3) Subscribe the GalagaBus to proper GameEventTypes and GameEventProcessors
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, stateMachine);  
        }


        [Test]
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }

        [Test]
        public void TestEventGamePaused() {
            GalagaBus.GetBus().RegisterEvent(
                new GameEvent{
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_PAUSED"
                }
            );
            GalagaBus.GetBus().ProcessEventsSequentially();
            // Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
        }
    }
}


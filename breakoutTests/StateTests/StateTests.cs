using DIKUArcade.State;
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
using Breakout;
using Breakout.BreakoutStates;

namespace BreakoutTests {

    [TestFixture]
    public class StateMachineTesting {
        private StateMachine stateMachine;
        private GameRunning gameRunning;
        [SetUp]
        public void InitiateStateMachine() {
            Window.CreateOpenGLContext();
            // (1) Initialize a BreakoutBus with proper GameEventTypes
            var eventBus = BreakoutBus.GetBus();
            var eventQueue = new List<GameEventType> {
                GameEventType.InputEvent,
                GameEventType.WindowEvent,
                GameEventType.MovementEvent,
                GameEventType.GameStateEvent,
                GameEventType.StatusEvent,
                GameEventType.ScoreEvent
            };
            // eventBus.InitializeEventBus(eventQueue);

            // (2) Instantiate the StateMachine
            stateMachine = new StateMachine();
            // (3) Subscribe the GalagaBus to proper GameEventTypes and GameEventProcessors
            var windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
            var game = new Game(windowArgs);
            for (int i = 0; i < eventQueue.Count; i++) {
                eventBus.Subscribe(eventQueue[i], game);
            }
        

            gameRunning = new GameRunning();
        }
        
        // [Test]
        // public void TestInitialState() {
        //     Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        // }

        [Test]
        public void TestEventGamePaused() {
            stateMachine.SwitchState(GameStateType.GamePaused);
            gameRunning.KeyRelease(KeyboardKey.Escape);
            BreakoutBus.GetBus().ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
        }

    }
}
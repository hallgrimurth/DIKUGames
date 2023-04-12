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
using Galaga;
using Galaga.Squadron;
using Galaga.MovementStrategy;
using Galaga.GalagaStates;

namespace GalagaTests {

    [TestFixture]
    public class StateMachineTesting {
        private StateMachine stateMachine;
        private GameRunning gameRunning;
        [SetUp]
        public void InitiateStateMachine() {
            Window.CreateOpenGLContext();
            // (1) Initialize a GalagaBus with proper GameEventTypes
            var eventBus = GalagaBus.GetBus();
            // eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.GameStateEvent, GameEventType.InputEvent});
            // (2) Instantiate the StateMachine
            stateMachine = new StateMachine();
            // (3) Subscribe the GalagaBus to proper GameEventTypes and GameEventProcessors
            eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
            eventBus.Subscribe(GameEventType.InputEvent, stateMachine);

            gameRunning = new GameRunning();
        }
        
        [Test]
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }

        [Test]
        public void TestEventGamePaused() {
            stateMachine.SwitchState(GameStateType.GamePaused);
            gameRunning.KeyRelease(KeyboardKey.Escape);
            GalagaBus.GetBus().ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
        }

    }
}
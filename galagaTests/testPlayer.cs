using NUnit.Framework;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using System.Collections.Generic;
using Galaga.GalagaStates;
using Galaga;

namespace galagaTests;

public class testPlayer {
    private StateMachine stateMachine;
        [SetUp]
        public void InitiateStateMachine() {
            Window.CreateOpenGLContext();
            // (1) Initialize a GalagaBus with proper GameEventTypes
            var eventBus = GalagaBus.GetBus();
            // eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.GameStateEvent, GameEventType.InputEvent});
            // (2) Instantiate the StateMachine
            stateMachine = new StateMachine();
            // (3) Subscribe the GalagaBus to proper GameEventTypes and GameEventProcessors
            // GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
            // GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, stateMachine);
            
        }

    [Test]
    public void TestPlayer()
    {
        // List<Image> playerStrides = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "FlightAnimation.png"));
        // var player = new Player(new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f)), new ImageStride(160, playerStrides));
        
        // player.Move();
        
        // GalagaBus.GetBus().ProcessEventsSequentially();
        // Assert.That(player.Position.X, Is.EqualTo(0.1f));
        // Assert.AreEqual(player.Shape.Position.Y, 0.1f);
    }
}
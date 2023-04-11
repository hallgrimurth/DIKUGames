using NUnit.Framework;
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

[TestFixture]
public class StateTransformerTests {

    [Test]
    public void TestTransformStringToState() {
        // Arrange
        string stateString = "GAME_RUNNING";
        
        // Act
       var stateType = StateTransformer.TransformStringToState(stateString);
        
        // Assert
        Assert.That(stateType, Is.EqualTo(GameStateType.GameRunning));
    }

    [Test]
    public void TestTransformStateToString() {
        // Arrange
       var stateType = GameStateType.GamePaused;
        
        // Act
        string stateString = StateTransformer.TransformStateToString(stateType);
        
        // Assert
        Assert.That(stateString, Is.EqualTo("GAME_PAUSED"));
    }
}


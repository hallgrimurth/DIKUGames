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

namespace galagaTests;

public class testPlayer {

    [Test]
    public void TestPlayer()
    {
        // var player = new Galaga.Player.Player(new DynamicShape(new DIKUArcade.Math.Vec2F(0.1f, 0.1f), new DIKUArcade.Math.Vec2F(0.1f, 0.1f)), new ImageStride(160, Galaga.GalagaStates.GameRunning.playerStrides));
        
        // player.Move();

        // Assert.AreEqual(player.Shape.Position.X, 0.1f);
        // Assert.AreEqual(player.Shape.Position.Y, 0.1f);
    }
}
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

public class testHealth {

    [Test]
    public void TestHealth()
    {
        var health = new Health(new DIKUArcade.Math.Vec2F(0.75f, -0.2f), new DIKUArcade.Math.Vec2F(0.4f, 0.4f));
        // var newhealth = health.LoseHealth();
        // Assert.That(newhealth, Is.Not.EqualTo(health));
    }
}
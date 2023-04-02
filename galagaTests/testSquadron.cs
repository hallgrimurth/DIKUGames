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

public class testSquadron {

    [Test]
    public void TestSquadron()
    {   
        // int numEnemies = 8;
        // var circleSquad = new Galaga.Squadron.Circle(numEnemies);
        // var waveSquad = new Galaga.Squadron.Wave(numEnemies);

        // var squadronCircleFormation = circleSquad.CreateEnemies(Galaga.GalagaStates.GameRunning.enemyStridesBlue, Galaga.GalagaStates.GameRunning.enemyStridesRed, 0.00f);
        // var squadronWaveFormation = waveSquad.CreateEnemies(Galaga.GalagaStates.GameRunning.enemyStridesBlue, Galaga.GalagaStates.GameRunning.enemyStridesRed, 0.00f);

        // Assert.AreNotEqual(squadronCircleFormation, squadronWaveFormation);
    }
}